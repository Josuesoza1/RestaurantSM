USE [RestaurantSM];
GO
SET ANSI_NULLS ON;
GO
SET QUOTED_IDENTIFIER ON;
GO
--1. Nuevo Insertar Cliente.
ALTER PROCEDURE sp_InsertarCliente
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
        VALUES (@Usuario, @Contrasena, 2);

        SET @IdUsuarioGenerado = SCOPE_IDENTITY();

        INSERT INTO cliente (idUsuario, PNombreCl, SNombreCl, PApellidoCl, SApellidoCl, genero, cedula, telefono, email)
        VALUES (@IdUsuarioGenerado, @PrimerNombre, @SegundoNombre, @PrimerApellido, @SegundoApellido, @Genero, @Cedula, @Telefono, @Email);

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END

GO
