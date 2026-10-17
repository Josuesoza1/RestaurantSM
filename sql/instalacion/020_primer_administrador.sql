-- SOLO para una instalación nueva. Cambiar estos valores localmente.
-- Es un usuario de la aplicación, no un login de SQL Server.
USE [RestaurantSM];
GO
SET XACT_ABORT ON;
DECLARE @Usuario varchar(50) = 'CAMBIAR_USUARIO';
DECLARE @Contrasena varchar(255) = 'CAMBIAR_CLAVE_LOCAL';
IF @Usuario='CAMBIAR_USUARIO' OR @Contrasena='CAMBIAR_CLAVE_LOCAL'
    THROW 50110, 'Edite el usuario y la clave local antes de ejecutar este archivo.', 1;
IF NULLIF(LTRIM(RTRIM(@Usuario)),'') IS NULL OR NULLIF(LTRIM(RTRIM(@Contrasena)),'') IS NULL
    THROW 50111, 'Usuario y clave son obligatorios.', 1;
IF EXISTS (SELECT 1 FROM dbo.usuario WHERE idRol=1)
    THROW 50112, 'Ya existe un administrador. Use la gestión de empleados para crear más cuentas.', 1;
IF NOT EXISTS (SELECT 1 FROM dbo.rol WHERE idRol=1 AND nombre='Administrador')
    THROW 50113, 'El rol administrativo no coincide con el proyecto.', 1;
INSERT dbo.usuario (nombreUsuario,contrasena,idRol) VALUES (@Usuario,@Contrasena,1);
PRINT 'Administrador inicial creado. Ya puede iniciar sesión en la aplicación.';
GO
