param([string]$Repositorio=(Split-Path $PSScriptRoot -Parent))
$ErrorActionPreference='Stop'
$repo=[IO.Path]::GetFullPath($Repositorio)
$qa=Join-Path $env:TEMP ('RestaurantSM-Operacion-Pruebas-'+[guid]::NewGuid().ToString('N'))
[void](New-Item -ItemType Directory -Path $qa)
$configuracion=Join-Path $repo 'SistemaPedidoRestaurante\bin\Debug\SistemaPedidoRestaurante.exe.config'
if(-not(Test-Path -LiteralPath $configuracion)){throw 'Compile la solucion en Debug antes de ejecutar las pruebas.'}
$cfg=[xml](Get-Content -LiteralPath $configuracion)
$original=($cfg.configuration.connectionStrings.add|Where-Object name -eq RestauranteDB).connectionString
$builder=New-Object System.Data.SqlClient.SqlConnectionStringBuilder($original)
$builder['Initial Catalog']='master'
$cn=New-Object System.Data.SqlClient.SqlConnection($builder.ConnectionString)
$db='RestaurantSM_Operacion_'+[guid]::NewGuid().ToString('N')
function Batches([string]$sql){foreach($b in [regex]::Split($sql,'(?im)^\s*GO\s*\r?$')){if([string]::IsNullOrWhiteSpace($b)){continue};$cmd=$cn.CreateCommand();try{$cmd.CommandText=$b;$cmd.CommandTimeout=90;[void]$cmd.ExecuteNonQuery()}finally{$cmd.Dispose()}}}
try {
 $cn.Open()
 $instalar=[IO.File]::ReadAllText((Join-Path $repo 'sql\INSTALAR_NUEVA.sql')).Replace("DB_ID(N'RestaurantSM')","DB_ID(N'$db')").Replace('[RestaurantSM]',"[$db]")
 Batches $instalar
 $cn.ChangeDatabase($db)
 Batches "INSERT dbo.usuario(nombreUsuario,contrasena,idRol) VALUES('qa.admin','ClaveSoloPruebas',1);"
 $actualizar=[IO.File]::ReadAllText((Join-Path $repo 'sql\ACTUALIZAR_TODO_RESTAURANTSM.sql')).Replace('[RestaurantSM]',"[$db]")
 Batches $actualizar;Batches $actualizar
 Write-Output 'OK: instalador nuevo y actualizador repetido conservan la base.'
 Get-ChildItem -LiteralPath (Join-Path $repo 'SistemaPedidoRestaurante\bin\Debug') -File|Where-Object {$_.Extension -in '.exe','.dll','.gz'}|Copy-Item -Destination $qa
 $native=Join-Path $qa 'runtimes\win-x64';[void](New-Item -ItemType Directory -Path (Split-Path $native) -Force);Copy-Item -LiteralPath (Join-Path $repo 'SistemaPedidoRestaurante\bin\Debug\runtimes\win-x64') -Destination $native -Recurse -Force
 $builder['Initial Catalog']=$db
 ($cfg.configuration.connectionStrings.add|Where-Object name -eq RestauranteDB).connectionString=$builder.ConnectionString
 $exe=Join-Path $qa 'VerificarOperacion.exe';$cfg.Save($exe+'.config')
 $argsCsc=@('/nologo','/codepage:65001',('/out:'+$exe),'/r:System.dll','/r:System.Core.dll','/r:System.Data.dll','/r:System.Data.DataSetExtensions.dll','/r:System.Configuration.dll','/r:System.Drawing.dll','/r:System.Windows.Forms.dll')
 foreach($dll in @('SistemaPedidoRestaurante.exe','SistemaPedidoRestaurante.BLL.dll','SistemaPedidoRestaurante.Entities.dll')){$argsCsc+='/r:'+(Join-Path $qa $dll)};$argsCsc+=Join-Path $PSScriptRoot 'VerificarOperacion.cs'
 & "$env:WINDIR\Microsoft.NET\Framework64\v4.0.30319\csc.exe" @argsCsc;if($LASTEXITCODE -ne 0){throw 'No compilo el verificador'}
 $salida=Join-Path $qa 'resultado.txt';$errorArchivo=Join-Path $qa 'errores.txt';$proceso=Start-Process -FilePath $exe -ArgumentList $qa -WindowStyle Hidden -PassThru -RedirectStandardOutput $salida -RedirectStandardError $errorArchivo
 # Mantener el handle abierto evita ExitCode nulo en Windows PowerShell 5.1.
 $handlePrueba=$proceso.Handle
 if(-not $proceso.WaitForExit(60000)){Stop-Process -Id $proceso.Id;Get-Content $salida;throw 'Tiempo excedido en verificacion'}
 $proceso.Refresh()
 Get-Content $salida;Get-Content $errorArchivo;if($null -eq $proceso.ExitCode -or $proceso.ExitCode -ne 0){throw 'Fallo de verificacion'}
 # Simular otra PC que carece de subtotal y total, conservando ventas y detalles.
 $consulta=$cn.CreateCommand();$consulta.CommandText='SELECT COUNT(*) FROM dbo.factura';$facturasAntes=[int]$consulta.ExecuteScalar();$consulta.Dispose()
 # Reejecutar el instalador nuevo sobre una base con ventas debe omitir todos sus cambios.
 Batches $instalar
 $consulta=$cn.CreateCommand();$consulta.CommandText='SELECT COUNT(*) FROM dbo.factura';if([int]$consulta.ExecuteScalar() -ne $facturasAntes){throw 'El instalador nuevo modifico una base existente'};$consulta.Dispose()
 Write-Output 'OK: instalador nuevo omite una base existente y conserva sus ventas.'
 Batches "DECLARE @sql nvarchar(max)='';SELECT @sql=@sql+'ALTER TABLE dbo.pedido DROP CONSTRAINT '+QUOTENAME(d.name)+';' FROM sys.default_constraints d JOIN sys.columns c ON c.object_id=d.parent_object_id AND c.column_id=d.parent_column_id WHERE d.parent_object_id=OBJECT_ID('dbo.pedido') AND c.name IN('subtotal','total');EXEC(@sql);ALTER TABLE dbo.pedido DROP COLUMN subtotal,total;"
 Batches "DECLARE @sql nvarchar(max)='';SELECT @sql=@sql+'ALTER TABLE dbo.inventarioProducto DROP CONSTRAINT '+QUOTENAME(d.name)+';' FROM sys.default_constraints d JOIN sys.columns c ON c.object_id=d.parent_object_id AND c.column_id=d.parent_column_id WHERE d.parent_object_id=OBJECT_ID('dbo.inventarioProducto') AND c.name='revision';EXEC(@sql);ALTER TABLE dbo.inventarioProducto DROP COLUMN revision;ALTER TABLE dbo.inventarioProducto ADD version rowversion;"
 Batches 'CREATE OR ALTER TRIGGER dbo.tr_Inventario_Movimientos ON dbo.inventarioProducto AFTER INSERT,UPDATE,DELETE AS BEGIN SET NOCOUNT ON; END;'
 Batches $actualizar
 $consulta=$cn.CreateCommand();$consulta.CommandText='SELECT COUNT(*) FROM dbo.factura';if([int]$consulta.ExecuteScalar() -ne $facturasAntes){throw 'Se altero el historial de facturas'}
 $consulta.CommandText='SELECT COUNT(*) FROM dbo.pedido p WHERE p.subtotal<>(SELECT SUM(d.total) FROM dbo.detallePedido d WHERE d.idPedido=p.idPedido)';if([int]$consulta.ExecuteScalar() -ne 0){throw 'No se reconstruyeron correctamente los subtotales'};$consulta.Dispose()
 $consulta=$cn.CreateCommand();$consulta.CommandText="SELECT COUNT(*) FROM dbo.inventarioProducto WHERE revision>=1";if([int]$consulta.ExecuteScalar() -ne 1){throw 'No se adapto el inventario anterior'};$consulta.CommandText="SELECT COUNT(*) FROM sys.triggers WHERE name='tr_Inventario_Movimientos'";if([int]$consulta.ExecuteScalar() -ne 0){throw 'Quedo el trigger anterior'};$consulta.Dispose()
 Write-Output 'OK: actualizacion reconstruye columnas faltantes sin eliminar pedidos ni facturas.'
 Write-Output 'OK: esquema anterior con rowversion se adapta al contador entero sin perder existencias.'
 Write-Output ('Evidencias: '+$qa)
 Set-Content -LiteralPath (Join-Path $env:TEMP 'RestaurantSM-Operacion-ultima-prueba.txt') -Value $qa
}
finally {
 if($cn.State -eq 'Open'){$cn.ChangeDatabase('master');if($db -notmatch '^RestaurantSM_Operacion_[a-f0-9]{32}$'){throw 'Nombre temporal invalido'};Batches "IF DB_ID(N'$db') IS NOT NULL BEGIN ALTER DATABASE [$db] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;DROP DATABASE [$db];END"}
 $cn.Dispose()
}
