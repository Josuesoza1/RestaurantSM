-- Bloque 3. Guarda empleado y cuenta en una misma transacción.
-- No altera registros existentes ni el esquema de tablas.
USE [RestaurantSM];
GO
SET ANSI_NULLS ON;
GO
SET QUOTED_IDENTIFIER ON;
GO

CREATE OR ALTER PROCEDURE dbo.sp_Empleado_Listar
AS
BEGIN
    SET NOCOUNT ON;
    SELECT e.idEmpleado, e.idUsuario, e.pNombreEmp, e.sNombreEmp,
           e.pApellidoEmp, e.sApellidoEmp, e.cargo, e.genero, e.cedula,
           e.telefono, e.activo, u.nombreUsuario, u.idRol, r.nombre AS NombreRol
    FROM dbo.empleado e
    LEFT JOIN dbo.usuario u ON u.idUsuario = e.idUsuario
    LEFT JOIN dbo.rol r ON r.idRol = u.idRol
    ORDER BY e.idEmpleado;
END;
GO

CREATE OR ALTER PROCEDURE dbo.sp_Empleado_Insertar
    @IdAdministrador INT,
    @PrimerNombre VARCHAR(80), @SegundoNombre VARCHAR(80),
    @PrimerApellido VARCHAR(80), @SegundoApellido VARCHAR(80),
    @Cargo VARCHAR(50), @Genero CHAR(1), @Cedula VARCHAR(30), @Telefono VARCHAR(20),
    @Usuario VARCHAR(50), @Contrasena VARCHAR(255), @IdRol INT, @Activo BIT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    IF NOT EXISTS (SELECT 1 FROM dbo.usuario WHERE idUsuario = @IdAdministrador AND idRol = 1)
       OR EXISTS (SELECT 1 FROM dbo.empleado WHERE idUsuario = @IdAdministrador AND activo = 0)
        THROW 50010, 'Solo un administrador activo puede gestionar empleados.', 1;
    IF @IdRol IS NULL OR @IdRol NOT IN (1, 2)
        THROW 50011, 'El rol debe ser Administrador o Empleado.', 1;
    IF NULLIF(LTRIM(RTRIM(@Contrasena)), '') IS NULL
        THROW 50012, 'La contraseña es obligatoria para crear la cuenta.', 1;

    BEGIN TRY
        BEGIN TRANSACTION;
        INSERT INTO dbo.usuario (nombreUsuario, contrasena, idRol)
        VALUES (@Usuario, @Contrasena, @IdRol);
        DECLARE @IdUsuario INT = CONVERT(INT, SCOPE_IDENTITY());
        INSERT INTO dbo.empleado
            (idUsuario, pNombreEmp, sNombreEmp, pApellidoEmp, sApellidoEmp, cargo, genero, cedula, telefono, activo)
        VALUES
            (@IdUsuario, @PrimerNombre, @SegundoNombre, @PrimerApellido, @SegundoApellido, @Cargo, @Genero, @Cedula, @Telefono, @Activo);
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

CREATE OR ALTER PROCEDURE dbo.sp_Empleado_Actualizar
    @IdEmpleado INT, @IdAdministrador INT,
    @PrimerNombre VARCHAR(80), @SegundoNombre VARCHAR(80),
    @PrimerApellido VARCHAR(80), @SegundoApellido VARCHAR(80),
    @Cargo VARCHAR(50), @Genero CHAR(1), @Cedula VARCHAR(30), @Telefono VARCHAR(20),
    @Usuario VARCHAR(50), @Contrasena VARCHAR(255), @IdRol INT, @Activo BIT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    IF NOT EXISTS (SELECT 1 FROM dbo.usuario WHERE idUsuario = @IdAdministrador AND idRol = 1)
       OR EXISTS (SELECT 1 FROM dbo.empleado WHERE idUsuario = @IdAdministrador AND activo = 0)
        THROW 50010, 'Solo un administrador activo puede gestionar empleados.', 1;
    IF @IdRol IS NULL OR @IdRol NOT IN (1, 2)
        THROW 50011, 'El rol debe ser Administrador o Empleado.', 1;

    BEGIN TRY
        BEGIN TRANSACTION;
        DECLARE @IdUsuario INT;
        IF NOT EXISTS (SELECT 1 FROM dbo.empleado WITH (UPDLOCK, HOLDLOCK) WHERE idEmpleado = @IdEmpleado)
            THROW 50013, 'El empleado seleccionado ya no existe.', 1;
        SELECT @IdUsuario = idUsuario FROM dbo.empleado WHERE idEmpleado = @IdEmpleado;
        IF @IdUsuario = @IdAdministrador AND (@Activo = 0 OR @IdRol <> 1)
            THROW 50014, 'No puede desactivar su propia cuenta ni quitarse el rol de administrador.', 1;

        IF @IdUsuario IS NULL
        BEGIN
            IF NULLIF(LTRIM(RTRIM(@Contrasena)), '') IS NULL
                THROW 50012, 'Este empleado no tiene cuenta: ingrese una contraseña para crearla.', 1;
            INSERT INTO dbo.usuario (nombreUsuario, contrasena, idRol)
            VALUES (@Usuario, @Contrasena, @IdRol);
            SET @IdUsuario = CONVERT(INT, SCOPE_IDENTITY());
        END
        ELSE
        BEGIN
            UPDATE dbo.usuario
            SET nombreUsuario = @Usuario, idRol = @IdRol,
                contrasena = COALESCE(NULLIF(@Contrasena, ''), contrasena)
            WHERE idUsuario = @IdUsuario;
        END;

        UPDATE dbo.empleado
        SET idUsuario = @IdUsuario, pNombreEmp = @PrimerNombre, sNombreEmp = @SegundoNombre,
            pApellidoEmp = @PrimerApellido, sApellidoEmp = @SegundoApellido, cargo = @Cargo,
            genero = @Genero, cedula = @Cedula, telefono = @Telefono, activo = @Activo
        WHERE idEmpleado = @IdEmpleado;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

CREATE OR ALTER PROCEDURE dbo.sp_Empleado_Desactivar
    @IdEmpleado INT, @IdAdministrador INT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    IF NOT EXISTS (SELECT 1 FROM dbo.usuario WHERE idUsuario = @IdAdministrador AND idRol = 1)
       OR EXISTS (SELECT 1 FROM dbo.empleado WHERE idUsuario = @IdAdministrador AND activo = 0)
        THROW 50010, 'Solo un administrador activo puede gestionar empleados.', 1;

    BEGIN TRY
        BEGIN TRANSACTION;
        IF NOT EXISTS (SELECT 1 FROM dbo.empleado WITH (UPDLOCK, HOLDLOCK) WHERE idEmpleado = @IdEmpleado)
            THROW 50013, 'El empleado seleccionado ya no existe.', 1;
        IF EXISTS (SELECT 1 FROM dbo.empleado WHERE idEmpleado = @IdEmpleado AND idUsuario = @IdAdministrador)
            THROW 50014, 'No puede desactivar su propia cuenta.', 1;
        UPDATE dbo.empleado SET activo = 0 WHERE idEmpleado = @IdEmpleado;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

CREATE OR ALTER PROCEDURE dbo.sp_Usuario_ValidarLogin
    @NombreUsuario VARCHAR(50), @Contrasena VARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT u.idUsuario, u.nombreUsuario, u.idRol, r.nombre AS NombreRol
    FROM dbo.usuario u
    INNER JOIN dbo.rol r ON u.idRol = r.idRol
    WHERE u.nombreUsuario = @NombreUsuario AND u.contrasena = @Contrasena
      AND NOT EXISTS (SELECT 1 FROM dbo.empleado e WHERE e.idUsuario = u.idUsuario AND e.activo = 0);
END;
GO
