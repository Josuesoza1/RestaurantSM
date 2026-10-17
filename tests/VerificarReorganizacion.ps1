$ErrorActionPreference = 'Stop'
$repo = Split-Path $PSScriptRoot -Parent
$dir = Join-Path $env:TEMP ('RestaurantSM-Reorganizacion-' + [guid]::NewGuid().ToString('N'))
[void](New-Item -ItemType Directory -Path $dir)
Get-ChildItem -LiteralPath (Join-Path $repo 'SistemaPedidoRestaurante\bin\Debug') -File |
    Where-Object { $_.Extension -in '.dll', '.exe' } | Copy-Item -Destination $dir
$argsCsc = @('/nologo', '/target:exe', ('/out:' + (Join-Path $dir 'VerificarReorganizacion.exe')))
$references = @('System.dll', 'System.Core.dll', 'System.Drawing.dll', 'System.Windows.Forms.dll', 'System.Design.dll',
    (Join-Path $dir 'SistemaPedidoRestaurante.exe'), (Join-Path $dir 'SistemaPedidoRestaurante.Entities.dll'),
    (Join-Path $dir 'SistemaPedidoRestaurante.BLL.dll'))
foreach ($reference in $references) { $argsCsc += '/reference:' + $reference }
$argsCsc += Join-Path $PSScriptRoot 'VerificarReorganizacion.cs'
& "$env:WINDIR\Microsoft.NET\Framework64\v4.0.30319\csc.exe" @argsCsc
if ($LASTEXITCODE -ne 0) { throw 'No compilo el verificador.' }
& (Join-Path $dir 'VerificarReorganizacion.exe')
if ($LASTEXITCODE -ne 0) { throw 'Fallo la verificacion de reorganizacion.' }
