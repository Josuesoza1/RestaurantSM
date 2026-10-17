$ErrorActionPreference = 'Stop'
$repo = Split-Path $PSScriptRoot -Parent
$dir = Join-Path $env:TEMP ('RestaurantSM-CatalogoCliente-' + [guid]::NewGuid().ToString('N'))
[void](New-Item -ItemType Directory -Path $dir)
Get-ChildItem -LiteralPath (Join-Path $repo 'SistemaPedidoRestaurante\bin\Debug') -File |
    Where-Object { $_.Extension -in '.dll', '.exe' } | Copy-Item -Destination $dir
$exe = Join-Path $dir 'VerificarCatalogoCliente.exe'
$config = $exe + '.config'
Copy-Item -LiteralPath (Join-Path $repo 'SistemaPedidoRestaurante\App.config') -Destination $config
try {
    $argsCsc = @('/nologo', '/target:exe', ('/out:' + $exe))
    $references = @('System.dll', 'System.Core.dll', 'System.Data.dll', 'System.Configuration.dll', 'System.Drawing.dll',
        'System.Windows.Forms.dll', 'System.Design.dll', (Join-Path $dir 'SistemaPedidoRestaurante.exe'),
        (Join-Path $dir 'SistemaPedidoRestaurante.Entities.dll'), (Join-Path $dir 'SistemaPedidoRestaurante.BLL.dll'))
    foreach ($reference in $references) { $argsCsc += '/reference:' + $reference }
    $argsCsc += Join-Path $PSScriptRoot 'VerificarCatalogoCliente.cs'
    & "$env:WINDIR\Microsoft.NET\Framework64\v4.0.30319\csc.exe" @argsCsc
    if ($LASTEXITCODE -ne 0) { throw 'No compilo el verificador.' }
    & $exe
    if ($LASTEXITCODE -ne 0) { throw 'Fallo la verificacion del catalogo cliente.' }
    Write-Output ('Capturas de revision: ' + $dir)
}
finally { Remove-Item -LiteralPath $config -ErrorAction SilentlyContinue }
