-- Bloque 1: las cuentas creadas desde el registro pertenecen al rol Cliente (3).
-- No cambia los roles de usuarios existentes.
USE [RestaurantSM];
GO

IF NOT EXISTS (SELECT 1 FROM dbo.rol WHERE idRol = 3 AND nombre = 'Cliente')
    THROW 50001, 'No existe el rol Cliente con idRol = 3. Revise los roles antes de continuar.', 1;
GO

SET ANSI_NULLS ON;
GO
SET QUOTED_IDENTIFIER ON;
GO

ALTER PROCEDURE [dbo].[sp_InsertarCliente]
    @PrimerNombre VARCHAR(80),
    @SegundoNombre VARCHAR(80),
    @PrimerApellido VARCHAR(80),
    @SegundoApellido VARCHAR(80),
    @Telefono VARCHAR(20),
    @Email VARCHAR(120),
    @Cedula VARCHAR(30),
    @Genero CHAR(1),
    @Usuario VARCHAR(50),
    @Contrasena VARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        BEGIN TRANSACTION;

        DECLARE @IdUsuarioGenerado INT;

        INSERT INTO usuario (nombreUsuario, contrasena, idRol)
        VALUES (@Usuario, @Contrasena, 3);

        SET @IdUsuarioGenerado = SCOPE_IDENTITY();

        INSERT INTO cliente (idUsuario, PNombreCl, SNombreCl, PApellidoCl, SApellidoCl, genero, cedula, telefono, email)
        VALUES (@IdUsuarioGenerado, @PrimerNombre, @SegundoNombre, @PrimerApellido, @SegundoApellido, @Genero, @Cedula, @Telefono, @Email);

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO
