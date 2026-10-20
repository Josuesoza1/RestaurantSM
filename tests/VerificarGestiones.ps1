param([string]$EsquemaOriginal = 'C:\Users\Josue\Documents\RestaurantSM.sql')
$ErrorActionPreference='Stop'
$repo=Split-Path $PSScriptRoot -Parent
$qa=Join-Path $env:TEMP ('RestaurantSM-Gestiones-'+[guid]::NewGuid().ToString('N'))
[void](New-Item -ItemType Directory -Path $qa)
$cfg=[xml](Get-Content -LiteralPath (Join-Path $repo 'SistemaPedidoRestaurante\App.config'))
$original=($cfg.configuration.connectionStrings.add|Where-Object name -eq 'RestauranteDB').connectionString
$builder=New-Object System.Data.SqlClient.SqlConnectionStringBuilder($original)
$db='RestaurantSM_Gestiones_'+[guid]::NewGuid().ToString('N')
$cn=New-Object System.Data.SqlClient.SqlConnection($original)
function Batches([string]$sql) {
 foreach($batch in [regex]::Split($sql,'(?im)^\s*GO\s*\r?$')) {
  if([string]::IsNullOrWhiteSpace($batch)){continue}
  $cmd=$cn.CreateCommand();$cmd.CommandText=$batch;$cmd.CommandTimeout=60;[void]$cmd.ExecuteNonQuery()
 }
}
$testConfig=Join-Path $qa 'VerificarGestiones.exe.config'
try {
 $cn.Open()
 # Copiar definiciones, sin usuarios ni contraseñas almacenadas.
 $cmd=$cn.CreateCommand();$cmd.CommandText="SELECT sm.definition FROM sys.sql_modules sm JOIN sys.objects o ON o.object_id=sm.object_id WHERE o.type='P' AND o.name NOT LIKE '%diagram%'";
 $defs=New-Object System.Collections.Generic.List[string];$rd=$cmd.ExecuteReader();while($rd.Read()){$defs.Add($rd.GetString(0))};$rd.Close()
 $cn.ChangeDatabase('master');Batches "CREATE DATABASE [$db];"
 $cn.ChangeDatabase($db)
 $s=[IO.File]::ReadAllText($EsquemaOriginal);$a=$s.IndexOf('/****** Objeto: Table [dbo].[categoria]');$b=$s.IndexOf('/****** Objeto: StoredProcedure')
 if($a -lt 0 -or $b -le $a){throw 'No se reconoce el esquema original. Indique -EsquemaOriginal con el script base.'}
 Batches $s.Substring($a,$b-$a)
 Batches 'ALTER TABLE dbo.categoria ADD activa bit NOT NULL DEFAULT(1) WITH VALUES;'
 Batches ([IO.File]::ReadAllText((Join-Path $repo 'sql\005_gestiones_pedidos_cliente.sql')).Replace('[RestaurantSM]',"[$db]"))
 foreach($def in $defs){Batches ([regex]::Replace($def,'(?i)\b(?:CREATE\s+OR\s+ALTER|CREATE|ALTER)\s+PROCEDURE\b','CREATE OR ALTER PROCEDURE'))}
 Batches ([IO.File]::ReadAllText((Join-Path $repo 'sql\005_gestiones_pedidos_cliente.sql')).Replace('[RestaurantSM]',"[$db]"))
 Batches "SET IDENTITY_INSERT dbo.rol ON; INSERT dbo.rol(idRol,nombre) VALUES(1,'Administrador'),(2,'Empleado'),(3,'Cliente'); SET IDENTITY_INSERT dbo.rol OFF; INSERT dbo.usuario(nombreUsuario,contrasena,idRol) VALUES('admin_temporal','ClaveSoloPruebas',1); INSERT dbo.categoria(nombre,activa) VALUES('Categoria prueba',1); INSERT dbo.producto(idCategoria,nombre,precio,codigo,disponible) VALUES(1,'Controlado',10,'TEST-1',1),(1,'Sin control',10,'TEST-2',1),(1,'No disponible',10,'TEST-3',0);"
 Get-ChildItem -LiteralPath (Join-Path $repo 'SistemaPedidoRestaurante\bin\Debug') -File | Where-Object Extension -in '.exe','.dll' | Copy-Item -Destination $qa
 $builder['Initial Catalog']=$db
 ($cfg.configuration.connectionStrings.add|Where-Object name -eq 'RestauranteDB').connectionString=$builder.ConnectionString
 $cfg.Save($testConfig)
 $exe=Join-Path $qa 'VerificarGestiones.exe'
 $refs=@('System.dll','System.Core.dll','System.Data.dll','System.Configuration.dll','System.Drawing.dll','System.Windows.Forms.dll','System.Design.dll')
 $argsCsc=@('/nologo',('/out:'+$exe))
 foreach($ref in $refs){$argsCsc+='/r:'+$ref}
 foreach($dll in @('SistemaPedidoRestaurante.exe','SistemaPedidoRestaurante.BLL.dll','SistemaPedidoRestaurante.Entities.dll')){$argsCsc+='/r:'+(Join-Path $qa $dll)}
 $argsCsc+=Join-Path $PSScriptRoot 'VerificarGestiones.cs'
 & "$env:WINDIR\Microsoft.NET\Framework64\v4.0.30319\csc.exe" @argsCsc
 if($LASTEXITCODE -ne 0){throw 'No compiló el verificador.'}
 & $exe
 if($LASTEXITCODE -ne 0){throw 'Falló una comprobación. La base de trabajo no se modificó.'}
 Write-Output ('Capturas: '+$qa)
}
finally {
 Remove-Item -LiteralPath $testConfig -ErrorAction SilentlyContinue
 if($cn.State -eq 'Open'){
  $cn.ChangeDatabase('master')
  if($db -notmatch '^RestaurantSM_Gestiones_[a-f0-9]{32}$'){throw 'Nombre temporal no válido.'}
  Batches "IF DB_ID(N'$db') IS NOT NULL BEGIN ALTER DATABASE [$db] SET SINGLE_USER WITH ROLLBACK IMMEDIATE; DROP DATABASE [$db]; END;"
 }
 $cn.Dispose()
}
