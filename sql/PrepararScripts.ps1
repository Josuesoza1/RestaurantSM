# Reune los archivos de estudio en los dos SQL completos. No se conecta a SQL Server.
$ErrorActionPreference = 'Stop'
$carpetaSql = $PSScriptRoot
$codificacion = New-Object Text.UTF8Encoding($true)
$cabecera = "USE [RestaurantSM];`r`nGO`r`nSET ANSI_NULLS ON;`r`nSET QUOTED_IDENTIFIER ON;`r`nGO`r`n"
$tablas = [IO.File]::ReadAllText((Join-Path $carpetaSql 'estructura\00_Tablas_originales.sql'))
$compatibilidad = [IO.File]::ReadAllText((Join-Path $carpetaSql 'estructura\01_Actualizar_tablas.sql'))
$finalizar = [IO.File]::ReadAllText((Join-Path $carpetaSql 'estructura\02_Finalizar_actualizacion.sql'))
$procedimientos = ''
foreach ($archivo in Get-ChildItem -LiteralPath (Join-Path $carpetaSql 'procedimientos') -Filter '*.sql' | Sort-Object Name) {
    $procedimientos += [IO.File]::ReadAllText($archivo.FullName) + "`r`n"
}
$proteccionActualizar = @'
-- BASE EXISTENTE. Ejecutar completo; si hay errores, corregir y repetir antes de abrir la aplicacion.
IF OBJECT_ID('dbo.pedido','U') IS NULL
BEGIN
    PRINT N'Faltan las tablas base. Use INSTALAR_NUEVA.sql.';
    SET NOEXEC ON;
END;
GO
IF EXISTS(SELECT 1 FROM dbo.rol WHERE (idRol=1 AND nombre<>'Administrador') OR (idRol=2 AND nombre<>'Empleado') OR (idRol=3 AND nombre<>'Cliente'))
BEGIN
    PRINT N'Los roles 1, 2 y 3 deben ser Administrador, Empleado y Cliente. Revise la base.';
    SET NOEXEC ON;
END;
GO
'@
$proteccionInstalar = @'
-- BASE NUEVA. No reemplaza bases que ya contienen tablas.
IF DB_ID(N'RestaurantSM') IS NULL CREATE DATABASE [RestaurantSM];
GO
USE [RestaurantSM];
GO
IF EXISTS(SELECT 1 FROM sys.tables)
BEGIN
    PRINT N'La base ya tiene tablas. No se ejecutara el instalador. Use ACTUALIZAR_TODO_RESTAURANTSM.sql.';
    SET NOEXEC ON;
END;
GO
'@
$terminar = "`r`nSET NOEXEC OFF;`r`nGO`r`n"
$actualizar = $cabecera + $proteccionActualizar + "`r`n" + $compatibilidad + "`r`n" + $procedimientos + "`r`n" + $finalizar + $terminar
$instalar = $proteccionInstalar + "`r`n" + $tablas + "`r`n" + $compatibilidad + "`r`n" + $procedimientos + "`r`n" + $finalizar + $terminar
[IO.File]::WriteAllText((Join-Path $carpetaSql 'ACTUALIZAR_TODO_RESTAURANTSM.sql'), $actualizar, $codificacion)
[IO.File]::WriteAllText((Join-Path $carpetaSql 'INSTALAR_NUEVA.sql'), $instalar, $codificacion)
Write-Output 'Preparados INSTALAR_NUEVA.sql y ACTUALIZAR_TODO_RESTAURANTSM.sql. No se modifico ninguna base.'
