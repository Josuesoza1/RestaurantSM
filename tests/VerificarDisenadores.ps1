param([string]$Repositorio=(Split-Path $PSScriptRoot -Parent))
$ErrorActionPreference='Stop'
$repo=[IO.Path]::GetFullPath($Repositorio)
$qa=Join-Path $env:TEMP ('RestaurantSM-Disenadores-Pruebas-'+[guid]::NewGuid().ToString('N'))
[void](New-Item -ItemType Directory -Path $qa)
$salidaProyecto=Join-Path $repo 'SistemaPedidoRestaurante\bin\Debug'
Get-ChildItem -LiteralPath $salidaProyecto -File | Where-Object {$_.Extension -in '.exe','.dll','.gz'} | Copy-Item -Destination $qa
$exe=Join-Path $qa 'VerificarDisenadores.exe'
Copy-Item -LiteralPath (Join-Path $salidaProyecto 'SistemaPedidoRestaurante.exe.config') -Destination ($exe+'.config')
$argumentos=@('/nologo','/codepage:65001',('/out:'+$exe),'/r:System.dll','/r:System.Core.dll','/r:System.Data.dll','/r:System.Drawing.dll','/r:System.Windows.Forms.dll')
foreach($dll in @('SistemaPedidoRestaurante.exe','SistemaPedidoRestaurante.BLL.dll','SistemaPedidoRestaurante.Entities.dll')){$argumentos+='/r:'+(Join-Path $qa $dll)}
$argumentos+=Join-Path $PSScriptRoot 'VerificarDisenadores.cs'
& "$env:WINDIR\Microsoft.NET\Framework64\v4.0.30319\csc.exe" @argumentos
if($LASTEXITCODE -ne 0){throw 'No compilo la verificacion de disenadores.'}
$salida=Join-Path $qa 'resultado.txt';$errores=Join-Path $qa 'errores.txt'
$proceso=Start-Process -FilePath $exe -ArgumentList $qa -WindowStyle Hidden -PassThru -RedirectStandardOutput $salida -RedirectStandardError $errores
$handlePrueba=$proceso.Handle
if(-not $proceso.WaitForExit(60000)){Stop-Process -Id $proceso.Id;Get-Content $salida;throw 'La verificacion excedio el tiempo esperado.'}
$proceso.Refresh()
Get-Content $salida;Get-Content $errores
if($null -eq $proceso.ExitCode -or $proceso.ExitCode -ne 0){throw 'Fallo la verificacion de los disenadores.'}
Set-Content -LiteralPath (Join-Path $env:TEMP 'RestaurantSM-Disenadores-ultima-prueba.txt') -Value $qa
Write-Output ('Evidencias: '+$qa)
