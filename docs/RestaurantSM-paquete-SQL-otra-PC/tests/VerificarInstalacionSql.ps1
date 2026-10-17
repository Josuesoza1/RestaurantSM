param([string]$Servidor = '', [string]$Configuracion = '')
$ErrorActionPreference = 'Stop'
$repo = Split-Path $PSScriptRoot -Parent
if ($Configuracion -eq '') { $Configuracion = Join-Path $repo 'SistemaPedidoRestaurante\App.config' }
$cfg = [xml](Get-Content -LiteralPath $Configuracion)
$builder = New-Object System.Data.SqlClient.SqlConnectionStringBuilder(($cfg.configuration.connectionStrings.add |
    Where-Object name -eq 'RestauranteDB').connectionString)
if ($Servidor -ne '') { $builder['Data Source'] = $Servidor }
$builder['Initial Catalog'] = 'master'
$dbPrueba = 'RestaurantSM_Verificacion_' + [guid]::NewGuid().ToString('N')
$cn = New-Object System.Data.SqlClient.SqlConnection($builder.ConnectionString)
$creada = $false
$total = 0
function Exigir($condicion, $mensaje) {
    if (-not $condicion) { throw $mensaje }
    $script:total++
    Write-Output ('OK: ' + $mensaje)
}
function EjecutarArchivo($nombre) {
    $sql = [IO.File]::ReadAllText((Join-Path $repo ('sql\instalacion\' + $nombre)))
    $sql = $sql.Replace('[RestaurantSM]', ('[' + $dbPrueba + ']')).Replace("N'RestaurantSM'", ("N'" + $dbPrueba + "'"))
    $sql = [regex]::Replace($sql, '(?m)^:On Error exit\s*$', '')
    foreach ($batch in [regex]::Split($sql, '(?im)^\s*GO\s*\r?$')) {
        if ([string]::IsNullOrWhiteSpace($batch)) { continue }
        $cmd = $cn.CreateCommand(); $cmd.CommandTimeout = 60; $cmd.CommandText = $batch
        [void]$cmd.ExecuteNonQuery()
        if ($cn.Database -eq $dbPrueba) { $script:creada = $true }
    }
}
function Escalar($sql) { $cmd = $cn.CreateCommand(); $cmd.CommandText = $sql; return $cmd.ExecuteScalar() }
try {
    $cn.Open()
    EjecutarArchivo 'INSTALAR_NUEVA.sql'
    Exigir ((Escalar 'SELECT COUNT(*) FROM sys.tables WHERE is_ms_shipped=0') -eq 13) 'Instalación nueva con 13 tablas de negocio'
    Exigir ((Escalar 'SELECT COUNT(*) FROM dbo.rol') -eq 3) 'Tres roles iniciales'
    Exigir ((Escalar 'SELECT COUNT(*) FROM dbo.categoria') -eq 6) 'Seis categorías'
    Exigir ((Escalar 'SELECT COUNT(*) FROM dbo.subcategoria') -eq 12) 'Doce subcategorías'
    Exigir ((Escalar 'SELECT COUNT(*) FROM dbo.producto') -eq 24) 'Veinticuatro productos'
    Exigir ((Escalar 'SELECT COUNT(*) FROM dbo.usuario') -eq 0) 'No copia usuarios ni claves de la PC de origen'
    Exigir ((Escalar "SELECT COUNT(*) FROM dbo.producto WHERE descripcion IS NULL OR idSubcategoria IS NULL") -eq 0) 'Catálogo con descripciones y subcategorías'
    $rechazada = $false
    try { EjecutarArchivo 'INSTALAR_NUEVA.sql' } catch { $rechazada = $_.Exception.ToString().Contains('La base ya existe') }
    $cn.ChangeDatabase($dbPrueba)
    Exigir $rechazada 'Instalación nueva rechaza reemplazar una base existente'
    EjecutarArchivo 'ACTUALIZAR_EXISTENTE.sql'
    EjecutarArchivo 'ACTUALIZAR_EXISTENTE.sql'
    Exigir ((Escalar 'SELECT COUNT(*) FROM dbo.producto') -eq 24) 'Actualización repetida sin duplicar productos'
    Exigir ((Escalar 'SELECT COUNT(*) FROM dbo.subcategoria') -eq 12) 'Actualización repetida sin duplicar subcategorías'
    $clavePrueba = [guid]::NewGuid().ToString('N')
    $cmd = $cn.CreateCommand()
    $cmd.CommandText = "INSERT dbo.usuario(nombreUsuario,contrasena,idRol) VALUES('verificador_admin',@clave,1);"
    [void]$cmd.Parameters.Add('@clave',[System.Data.SqlDbType]::VarChar,255); $cmd.Parameters['@clave'].Value=$clavePrueba
    [void]$cmd.ExecuteNonQuery()
    $cmd.CommandText = "EXEC dbo.sp_Usuario_ValidarLogin @NombreUsuario='verificador_admin',@Contrasena=@clave;"
    $rd = $cmd.ExecuteReader(); $valido = $rd.Read() -and [int]$rd['idRol'] -eq 1; $rd.Close()
    Exigir $valido 'Login administrativo funciona con el procedimiento instalado'
    $cmd.CommandText = "EXEC dbo.sp_InsertarCliente @PrimerNombre='Cliente',@SegundoNombre=NULL,@PrimerApellido='Prueba',@SegundoApellido=NULL,@Telefono='88888888',@Email='prueba@example.invalid',@Cedula='TEST-INSTALACION',@Genero='O',@Usuario='verificador_cliente',@Contrasena=@clave;"
    [void]$cmd.ExecuteNonQuery()
    Exigir ((Escalar "SELECT idRol FROM dbo.usuario WHERE nombreUsuario='verificador_cliente'") -eq 3) 'Registro público asigna rol Cliente'
    Exigir ((Escalar 'SELECT COUNT(*) FROM dbo.cliente') -eq 1) 'Registro crea el perfil asociado'
    EjecutarArchivo 'ACTUALIZAR_EXISTENTE.sql'
    Exigir ((Escalar 'SELECT COUNT(*) FROM dbo.usuario') -eq 2) 'Actualizar conserva las cuentas existentes'
    Exigir ((Escalar 'SELECT COUNT(*) FROM dbo.cliente') -eq 1) 'Actualizar conserva el perfil del cliente'
    EjecutarArchivo '030_verificar.sql'
    Exigir $true 'Consulta final de estructura y relaciones sin errores'
    Write-Output ('TOTAL: ' + $total + ' comprobaciones correctas en una base temporal independiente.')
}
finally {
    if ($cn.State -eq 'Open') {
        $cn.ChangeDatabase('master')
        # Elimina exclusivamente la base temporal generada en esta ejecución.
        if ($dbPrueba -notmatch '^RestaurantSM_Verificacion_[a-f0-9]{32}$') { throw 'Nombre temporal no válido.' }
        $cmd = $cn.CreateCommand()
        $cmd.CommandText = "IF DB_ID(N'$dbPrueba') IS NOT NULL BEGIN ALTER DATABASE [$dbPrueba] SET SINGLE_USER WITH ROLLBACK IMMEDIATE; DROP DATABASE [$dbPrueba]; END;"
        [void]$cmd.ExecuteNonQuery()
    }
    $cn.Dispose()
}
