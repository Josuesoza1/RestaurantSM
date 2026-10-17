$ErrorActionPreference = 'Stop'
$repo = Split-Path $PSScriptRoot -Parent
$bin = Join-Path $repo 'SistemaPedidoRestaurante\bin\Debug'
$testDir = Join-Path $env:TEMP ('RestaurantSM-Empleados-' + [guid]::NewGuid().ToString('N'))
[void](New-Item -ItemType Directory -Path $testDir)
Get-ChildItem -LiteralPath $bin -Filter *.dll | Copy-Item -Destination $testDir
$exe = Join-Path $testDir 'VerificarEmpleados.exe'
$configPath = $exe + '.config'
Copy-Item -LiteralPath (Join-Path $repo 'SistemaPedidoRestaurante\App.config') -Destination $configPath
try {
    $references = @('System.dll', 'System.Core.dll', 'System.Data.dll', 'System.Configuration.dll', 'System.Transactions.dll',
        (Join-Path $testDir 'SistemaPedidoRestaurante.Entities.dll'),
        (Join-Path $testDir 'SistemaPedidoRestaurante.DAL.dll'),
        (Join-Path $testDir 'SistemaPedidoRestaurante.BLL.dll'))
    $compilerArgs = @('/nologo', '/target:exe', ('/out:' + $exe))
    foreach ($reference in $references) { $compilerArgs += '/reference:' + $reference }
    $compilerArgs += Join-Path $PSScriptRoot 'VerificarEmpleados.cs'
    & "$env:WINDIR\Microsoft.NET\Framework64\v4.0.30319\csc.exe" @compilerArgs
    if ($LASTEXITCODE -ne 0) { throw 'No compilo el verificador.' }
    & $exe
    if ($LASTEXITCODE -ne 0) { throw 'Fallo la verificacion de empleados.' }
}
finally {
    # Eliminar la copia temporal de la configuración de conexión.
    Remove-Item -LiteralPath $configPath -ErrorAction SilentlyContinue
}
