-- SOLO despues de instalar una base nueva. Edite los dos valores antes de ejecutar.
USE [RestaurantSM];
GO
DECLARE @Usuario varchar(50)='admin';
DECLARE @Contrasena varchar(255)='ESCRIBA_SU_CONTRASENA';
IF @Contrasena='ESCRIBA_SU_CONTRASENA' OR LEN(@Contrasena)<8
 THROW 50603,'Escriba una contrasena propia de al menos 8 caracteres antes de ejecutar.',1;
IF EXISTS(SELECT 1 FROM dbo.usuario WHERE nombreUsuario=@Usuario)
 THROW 50604,'El usuario ya existe. Este script no modifica cuentas existentes.',1;
IF NOT EXISTS(SELECT 1 FROM dbo.versionEsquema WHERE version=9)
 THROW 50605,'Primero instale el esquema 009.',1;
INSERT dbo.usuario(nombreUsuario,contrasena,idRol) VALUES(@Usuario,@Contrasena,1);
PRINT 'Administrador creado. No guarde este archivo con su contrasena real en Git.';
GO
