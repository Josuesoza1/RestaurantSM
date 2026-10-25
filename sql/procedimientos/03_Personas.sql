-- Procedimientos de Personas.
USE [RestaurantSM];
GO
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
GO
-- Procedimiento sp_Cliente_Eliminar: consulta o modifica los datos de su gestion con parametros definidos.
CREATE OR ALTER PROCEDURE dbo.sp_Cliente_Eliminar
    @Actor INT,
    @Id INT
AS
BEGIN
    SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    EXECUTE dbo.sp_Catalogo_ExigirAdministrador @Actor;
    BEGIN TRY
        BEGIN TRANSACTION;
        DECLARE @Usuario AS INT, @Existe AS INT;
        SELECT @Existe = idCliente,
               @Usuario = idUsuario
        FROM dbo.cliente
        WHERE idCliente = @Id;
        IF @Existe IS NULL
            THROW 50405, 'El cliente ya no existe. Recargue el listado.', 1;
        IF EXISTS (SELECT 1
                   FROM dbo.pedido
                   WHERE idCliente = @Id)
           OR EXISTS (SELECT 1
                      FROM dbo.reservacion
                      WHERE idCliente = @Id)
            THROW 50406, 'El cliente tiene historial de pedidos o reservas. Utilice Desactivar.', 1;
        IF @Usuario IS NOT NULL
            BEGIN
                IF @Usuario = @Actor
                   OR NOT EXISTS (SELECT 1
                                  FROM dbo.usuario
                                  WHERE idUsuario = @Usuario
                                        AND idRol = 3)
                    THROW 50407, 'Solo puede eliminar junto al perfil una cuenta de cliente sin historial.', 1;
                IF EXISTS (SELECT 1
                           FROM dbo.empleado
                           WHERE idUsuario = @Usuario)
                   OR EXISTS (SELECT 1
                              FROM dbo.cliente
                              WHERE idUsuario = @Usuario
                                    AND idCliente <> @Id)
                    THROW 50408, 'La cuenta tiene otros perfiles asociados. No se eliminaron datos.', 1;
            END
        DELETE dbo.cliente
        WHERE idCliente = @Id;
        IF @Usuario IS NOT NULL
            DELETE dbo.usuario
            WHERE idUsuario = @Usuario;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK;
        THROW;
    END CATCH
END
GO

-- Procedimiento sp_Cliente_Guardar: consulta o modifica los datos de su gestion con parametros definidos.
CREATE OR ALTER PROCEDURE dbo.sp_Cliente_Guardar
    @Actor INT,
    @Id INT=0,
    @PrimerNombre VARCHAR (80),
    @SegundoNombre VARCHAR (80)=NULL,
    @PrimerApellido VARCHAR (80),
    @SegundoApellido VARCHAR (80)=NULL,
    @Genero CHAR (1),
    @Cedula VARCHAR (30)=NULL,
    @Telefono VARCHAR (20)=NULL,
    @Email VARCHAR (120)=NULL,
    @Activo BIT=1
AS
BEGIN
    SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    IF NOT EXISTS (SELECT 1
                   FROM dbo.usuario
                   WHERE idUsuario = @Actor
                         AND idRol = 1)
        BEGIN
            IF NOT EXISTS (SELECT 1
                           FROM dbo.cliente AS c
                                INNER JOIN
                                dbo.usuario AS u
                                ON u.idUsuario = c.idUsuario
                           WHERE c.idCliente = @Id
                                 AND c.idUsuario = @Actor
                                 AND u.idRol = 3
                                 AND c.activo = 1
                                 AND @Activo = 1)
                THROW 50200, 'Solo puede modificar sus propios datos de cliente.', 1;
        END
    ELSE
        EXECUTE dbo.sp_Catalogo_ExigirAdministrador @Actor;
    IF NULLIF (LTRIM(RTRIM(@PrimerNombre)), '') IS NULL
       OR NULLIF (LTRIM(RTRIM(@PrimerApellido)), '') IS NULL
       OR @Genero NOT IN ('M', 'F', 'O')
        THROW 50201, 'Revise nombre, apellido y género.', 1;
    BEGIN TRY
        BEGIN TRANSACTION;
        DECLARE @Existe AS INT;
        SELECT @Existe = idCliente
        FROM dbo.cliente
        WHERE idCliente = @Id;
        IF @Activo = 0
           AND (EXISTS (SELECT 1
                        FROM dbo.pedido
                        WHERE idCliente = @Id
                              AND estado NOT IN ('Cancelado', 'Pagado'))
                OR EXISTS (SELECT 1
                           FROM dbo.reservacion
                           WHERE idCliente = @Id
                                 AND estado IN ('Pendiente', 'Confirmada')
                                 AND fechaHoraReserva >= GETDATE()))
            THROW 50202, 'El cliente tiene pedidos o reservas pendientes.', 1;
        IF @Id = 0
            BEGIN
                INSERT dbo.cliente (PNombreCl, SNombreCl, PApellidoCl, SApellidoCl, genero, cedula, telefono, email, activo)
                VALUES            (@PrimerNombre, @SegundoNombre, @PrimerApellido, @SegundoApellido, @Genero, NULLIF (@Cedula, ''), NULLIF (@Telefono, ''), NULLIF (@Email, ''), @Activo);
                SET @Id = CONVERT (INT, SCOPE_IDENTITY());
            END
        ELSE
            BEGIN
                UPDATE dbo.cliente
                SET PNombreCl = @PrimerNombre,
                    SNombreCl = @SegundoNombre,
                    PApellidoCl = @PrimerApellido,
                    SApellidoCl = @SegundoApellido,
                    genero = @Genero,
                    cedula = NULLIF (@Cedula, ''),
                    telefono = NULLIF (@Telefono, ''),
                    email = NULLIF (@Email, ''),
                    activo = @Activo
                WHERE idCliente = @Id;
                IF @@ROWCOUNT = 0
                    THROW 50203, 'El cliente ya no existe.', 1;
            END
        COMMIT TRANSACTION;
        SELECT @Id;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK;
        THROW;
    END CATCH
END
GO

-- Procedimiento sp_Cliente_ObtenerMiPerfil: consulta o modifica los datos de su gestion con parametros definidos.
CREATE OR ALTER PROCEDURE dbo.sp_Cliente_ObtenerMiPerfil
    @IdUsuario INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT c.idCliente,
           c.PNombreCl,
           c.SNombreCl,
           c.PApellidoCl,
           c.SApellidoCl,
           c.genero,
           c.cedula,
           c.telefono,
           c.email
    FROM dbo.cliente AS c
         INNER JOIN
         dbo.usuario AS u
         ON u.idUsuario = c.idUsuario
    WHERE c.idUsuario = @IdUsuario
          AND c.activo = 1
          AND u.idRol = 3;
END
GO

-- Procedimiento sp_Empleado_Actualizar: consulta o modifica los datos de su gestion con parametros definidos.
CREATE OR ALTER PROCEDURE dbo.sp_Empleado_Actualizar
    @IdEmpleado INT,
    @IdAdministrador INT,
    @PrimerNombre VARCHAR (80),
    @SegundoNombre VARCHAR (80),
    @PrimerApellido VARCHAR (80),
    @SegundoApellido VARCHAR (80),
    @Cargo VARCHAR (50),
    @Genero CHAR (1),
    @Cedula VARCHAR (30),
    @Telefono VARCHAR (20),
    @Usuario VARCHAR (50),
    @Contrasena VARCHAR (255),
    @IdRol INT,
    @Activo BIT
AS
BEGIN
    SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    IF NOT EXISTS (SELECT 1
                   FROM dbo.usuario
                   WHERE idUsuario = @IdAdministrador
                         AND idRol = 1)
       OR EXISTS (SELECT 1
                  FROM dbo.empleado
                  WHERE idUsuario = @IdAdministrador
                        AND activo = 0)
        THROW 50010, 'Solo un administrador activo puede gestionar empleados.', 1;
    IF @IdRol IS NULL
       OR @IdRol NOT IN (1, 2)
        THROW 50011, 'El rol debe ser Administrador o Empleado.', 1;
    BEGIN TRY
        BEGIN TRANSACTION;
        DECLARE @IdUsuario AS INT;
        IF NOT EXISTS (SELECT 1
                       FROM dbo.empleado
                       WHERE idEmpleado = @IdEmpleado)
            THROW 50013, 'El empleado seleccionado ya no existe.', 1;
        SELECT @IdUsuario = idUsuario
        FROM dbo.empleado
        WHERE idEmpleado = @IdEmpleado;
        IF @IdUsuario = @IdAdministrador
           AND (@Activo = 0
                OR @IdRol <> 1)
            THROW 50014, 'No puede desactivar su propia cuenta ni quitarse el rol de administrador.', 1;
        IF @IdUsuario IS NULL
            BEGIN
                IF NULLIF (LTRIM(RTRIM(@Contrasena)), '') IS NULL
                    THROW 50012, 'Este empleado no tiene cuenta: ingrese una contraseña para crearla.', 1;
                INSERT INTO dbo.usuario (nombreUsuario, contrasena, idRol)
                VALUES                 (@Usuario, @Contrasena, @IdRol);
                SET @IdUsuario = CONVERT (INT, SCOPE_IDENTITY());
            END
        ELSE
            BEGIN
                UPDATE dbo.usuario
                SET nombreUsuario = @Usuario,
                    idRol = @IdRol,
                    contrasena = COALESCE (NULLIF (@Contrasena, ''), contrasena)
                WHERE idUsuario = @IdUsuario;
            END
        UPDATE dbo.empleado
        SET idUsuario = @IdUsuario,
            pNombreEmp = @PrimerNombre,
            sNombreEmp = @SegundoNombre,
            pApellidoEmp = @PrimerApellido,
            sApellidoEmp = @SegundoApellido,
            cargo = @Cargo,
            genero = @Genero,
            cedula = @Cedula,
            telefono = @Telefono,
            activo = @Activo
        WHERE idEmpleado = @IdEmpleado;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK;
        THROW;
    END CATCH
END
GO

-- Procedimiento sp_Empleado_Desactivar: consulta o modifica los datos de su gestion con parametros definidos.
CREATE OR ALTER PROCEDURE dbo.sp_Empleado_Desactivar
    @IdEmpleado INT,
    @IdAdministrador INT
AS
BEGIN
    SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    IF NOT EXISTS (SELECT 1
                   FROM dbo.usuario
                   WHERE idUsuario = @IdAdministrador
                         AND idRol = 1)
       OR EXISTS (SELECT 1
                  FROM dbo.empleado
                  WHERE idUsuario = @IdAdministrador
                        AND activo = 0)
        THROW 50010, 'Solo un administrador activo puede gestionar empleados.', 1;
    BEGIN TRY
        BEGIN TRANSACTION;
        IF NOT EXISTS (SELECT 1
                       FROM dbo.empleado
                       WHERE idEmpleado = @IdEmpleado)
            THROW 50013, 'El empleado seleccionado ya no existe.', 1;
        IF EXISTS (SELECT 1
                   FROM dbo.empleado
                   WHERE idEmpleado = @IdEmpleado
                         AND idUsuario = @IdAdministrador)
            THROW 50014, 'No puede desactivar su propia cuenta.', 1;
        UPDATE dbo.empleado
        SET activo = 0
        WHERE idEmpleado = @IdEmpleado;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK;
        THROW;
    END CATCH
END
GO

-- Procedimiento sp_Empleado_Eliminar: consulta o modifica los datos de su gestion con parametros definidos.
CREATE OR ALTER PROCEDURE dbo.sp_Empleado_Eliminar
    @IdEmpleado INT,
    @IdAdministrador INT
AS
BEGIN
    SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    EXECUTE dbo.sp_Catalogo_ExigirAdministrador @IdAdministrador;
    BEGIN TRY
        BEGIN TRANSACTION;
        DECLARE @Usuario AS INT, @Existe AS INT;
        SELECT @Existe = idEmpleado,
               @Usuario = idUsuario
        FROM dbo.empleado
        WHERE idEmpleado = @IdEmpleado;
        IF @Existe IS NULL
            THROW 50409, 'El empleado ya no existe. Recargue el listado.', 1;
        IF EXISTS (SELECT 1
                   FROM dbo.pedido
                   WHERE idEmpleado = @IdEmpleado
                         OR idRepartidor = @IdEmpleado)
            THROW 50410, 'El empleado tiene historial de pedidos o repartos. Utilice Desactivar.', 1;
        IF @Usuario IS NOT NULL
            BEGIN
                IF @Usuario = @IdAdministrador
                   OR NOT EXISTS (SELECT 1
                                  FROM dbo.usuario
                                  WHERE idUsuario = @Usuario
                                        AND idRol = 2)
                    THROW 50411, 'No se pueden eliminar cuentas de administrador desde Empleados.', 1;
                IF EXISTS (SELECT 1
                           FROM dbo.cliente
                           WHERE idUsuario = @Usuario)
                   OR EXISTS (SELECT 1
                              FROM dbo.empleado
                              WHERE idUsuario = @Usuario
                                    AND idEmpleado <> @IdEmpleado)
                    THROW 50412, 'La cuenta tiene otros perfiles asociados. No se eliminaron datos.', 1;
            END
        DELETE dbo.empleado
        WHERE idEmpleado = @IdEmpleado;
        IF @Usuario IS NOT NULL
            DELETE dbo.usuario
            WHERE idUsuario = @Usuario;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK;
        THROW;
    END CATCH
END
GO

-- Procedimiento sp_Empleado_Insertar: consulta o modifica los datos de su gestion con parametros definidos.
CREATE OR ALTER PROCEDURE dbo.sp_Empleado_Insertar
    @IdAdministrador INT,
    @PrimerNombre VARCHAR (80),
    @SegundoNombre VARCHAR (80),
    @PrimerApellido VARCHAR (80),
    @SegundoApellido VARCHAR (80),
    @Cargo VARCHAR (50),
    @Genero CHAR (1),
    @Cedula VARCHAR (30),
    @Telefono VARCHAR (20),
    @Usuario VARCHAR (50),
    @Contrasena VARCHAR (255),
    @IdRol INT,
    @Activo BIT
AS
BEGIN
    SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    IF NOT EXISTS (SELECT 1
                   FROM dbo.usuario
                   WHERE idUsuario = @IdAdministrador
                         AND idRol = 1)
       OR EXISTS (SELECT 1
                  FROM dbo.empleado
                  WHERE idUsuario = @IdAdministrador
                        AND activo = 0)
        THROW 50010, 'Solo un administrador activo puede gestionar empleados.', 1;
    IF @IdRol IS NULL
       OR @IdRol NOT IN (1, 2)
        THROW 50011, 'El rol debe ser Administrador o Empleado.', 1;
    IF NULLIF (LTRIM(RTRIM(@Contrasena)), '') IS NULL
        THROW 50012, 'La contraseña es obligatoria para crear la cuenta.', 1;
    BEGIN TRY
        BEGIN TRANSACTION;
        INSERT INTO dbo.usuario (nombreUsuario, contrasena, idRol)
        VALUES                 (@Usuario, @Contrasena, @IdRol);
        DECLARE @IdUsuario AS INT = CONVERT (INT, SCOPE_IDENTITY());
        INSERT INTO dbo.empleado (idUsuario, pNombreEmp, sNombreEmp, pApellidoEmp, sApellidoEmp, cargo, genero, cedula, telefono, activo)
        VALUES                  (@IdUsuario, @PrimerNombre, @SegundoNombre, @PrimerApellido, @SegundoApellido, @Cargo, @Genero, @Cedula, @Telefono, @Activo);
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK;
        THROW;
    END CATCH
END
GO

-- Procedimiento sp_Empleado_Listar: consulta o modifica los datos de su gestion con parametros definidos.
CREATE OR ALTER PROCEDURE dbo.sp_Empleado_Listar
AS
BEGIN
    SET NOCOUNT ON;
    SELECT e.idEmpleado,
           e.idUsuario,
           e.pNombreEmp,
           e.sNombreEmp,
           e.pApellidoEmp,
           e.sApellidoEmp,
           e.cargo,
           e.genero,
           e.cedula,
           e.telefono,
           e.activo,
           u.nombreUsuario,
           u.idRol,
           r.nombre AS NombreRol
    FROM dbo.empleado AS e
         LEFT OUTER JOIN
         dbo.usuario AS u
         ON u.idUsuario = e.idUsuario
         LEFT OUTER JOIN
         dbo.rol AS r
         ON r.idRol = u.idRol
    ORDER BY e.idEmpleado;
END
GO

-- Procedimiento sp_Gestion_Clientes: consulta o modifica los datos de su gestion con parametros definidos.
CREATE OR ALTER PROCEDURE dbo.sp_Gestion_Clientes
    @Actor INT
AS
BEGIN
    EXECUTE dbo.sp_Catalogo_ExigirAdministrador @Actor;
    SELECT idCliente AS Id,
           PNombreCl AS PrimerNombre,
           SNombreCl AS SegundoNombre,
           PApellidoCl AS PrimerApellido,
           SApellidoCl AS SegundoApellido,
           genero AS Genero,
           cedula AS Cedula,
           telefono AS Telefono,
           email AS Email,
           activo AS Activo
    FROM dbo.cliente
    ORDER BY PNombreCl, PApellidoCl;
END
GO

-- Procedimiento sp_InsertarCliente: consulta o modifica los datos de su gestion con parametros definidos.
CREATE OR ALTER PROCEDURE [dbo].[sp_InsertarCliente]
    @PrimerNombre VARCHAR (80),
    @SegundoNombre VARCHAR (80),
    @PrimerApellido VARCHAR (80),
    @SegundoApellido VARCHAR (80),
    @Telefono VARCHAR (20),
    @Email VARCHAR (120),
    @Cedula VARCHAR (30),
    @Genero CHAR (1),
    @Usuario VARCHAR (50),
    @Contrasena VARCHAR (255)
AS
BEGIN
    SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
        DECLARE @IdUsuarioGenerado AS INT;
        INSERT INTO usuario (nombreUsuario, contrasena, idRol)
        VALUES             (@Usuario, @Contrasena, 3);
        SET @IdUsuarioGenerado = SCOPE_IDENTITY();
        INSERT INTO cliente (idUsuario, PNombreCl, SNombreCl, PApellidoCl, SApellidoCl, genero, cedula, telefono, email)
        VALUES             (@IdUsuarioGenerado, @PrimerNombre, @SegundoNombre, @PrimerApellido, @SegundoApellido, @Genero, @Cedula, @Telefono, @Email);
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK;
        THROW;
    END CATCH
END
GO

-- Procedimiento sp_Mesa_Eliminar: consulta o modifica los datos de su gestion con parametros definidos.
CREATE OR ALTER PROCEDURE dbo.sp_Mesa_Eliminar
    @Actor INT,
    @Id INT
AS
BEGIN
    SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    EXECUTE dbo.sp_Catalogo_ExigirAdministrador @Actor;
    BEGIN TRY
        BEGIN TRANSACTION;
        DECLARE @Estado AS VARCHAR (20);
        SELECT @Estado = estado
        FROM dbo.mesa
        WHERE idMesa = @Id;
        IF @Estado IS NULL
            THROW 50413, 'La mesa ya no existe. Recargue el listado.', 1;
        IF @Estado IN ('Ocupada', 'Reservada')
            THROW 50414, 'No puede eliminar una mesa ocupada o reservada.', 1;
        IF EXISTS (SELECT 1
                   FROM dbo.pedido
                   WHERE idMesa = @Id)
           OR EXISTS (SELECT 1
                      FROM dbo.reservacion
                      WHERE idMesa = @Id)
            THROW 50415, 'La mesa tiene historial de pedidos o reservas. Utilice Desactivar.', 1;
        DELETE dbo.mesa
        WHERE idMesa = @Id;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK;
        THROW;
    END CATCH
END
GO

-- Procedimiento sp_Mesa_Guardar: consulta o modifica los datos de su gestion con parametros definidos.
CREATE OR ALTER PROCEDURE dbo.sp_Mesa_Guardar
    @Actor INT,
    @Id INT=0,
    @Numero INT,
    @Capacidad INT,
    @Estado VARCHAR (20),
    @Activa BIT
AS
BEGIN
    SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;
    SET XACT_ABORT ON;
    EXECUTE dbo.sp_Catalogo_ExigirAdministrador @Actor;
    IF @Numero < 1
       OR @Capacidad NOT BETWEEN 1 AND 20
       OR @Estado NOT IN ('Disponible', 'Ocupada', 'Reservada', 'En limpieza')
        THROW 50209, 'Número, capacidad o estado de mesa no válido.', 1;
    BEGIN TRY
        BEGIN TRANSACTION;
        IF @Id <> 0
           AND (@Activa = 0
                OR @Estado = 'Disponible')
           AND EXISTS (SELECT 1
                       FROM dbo.pedido
                       WHERE idMesa = @Id
                             AND estado NOT IN ('Cancelado', 'Pagado'))
            THROW 50210, 'La mesa tiene pedidos pendientes.', 1;
        IF @Id <> 0
           AND EXISTS (SELECT 1
                       FROM dbo.reservacion
                       WHERE idMesa = @Id
                             AND estado IN ('Pendiente', 'Confirmada')
                             AND fechaHoraReserva >= GETDATE()
                             AND (@Activa = 0
                                  OR cantidadPersonas > @Capacidad))
            THROW 50211, 'La mesa tiene reservas incompatibles con este cambio.', 1;
        IF @Id = 0
            BEGIN
                INSERT dbo.mesa (numero, capacidad, estado, activa)
                VALUES         (@Numero, @Capacidad, @Estado, @Activa);
                SET @Id = CONVERT (INT, SCOPE_IDENTITY());
            END
        ELSE
            BEGIN
                UPDATE dbo.mesa
                SET numero = @Numero,
                    capacidad = @Capacidad,
                    estado = @Estado,
                    activa = @Activa
                WHERE idMesa = @Id;
                IF @@ROWCOUNT = 0
                    THROW 50212, 'La mesa ya no existe.', 1;
            END
        COMMIT TRANSACTION;
        SELECT @Id;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK;
        THROW;
    END CATCH
END
GO

-- Procedimiento sp_Mesa_Listar: consulta o modifica los datos de su gestion con parametros definidos.
CREATE OR ALTER PROCEDURE dbo.sp_Mesa_Listar
AS
SELECT idMesa AS IdMesa,
       numero AS Numero,
       capacidad AS Capacidad,
       estado AS Estado,
       activa AS Activa
FROM dbo.mesa
ORDER BY numero;
GO
