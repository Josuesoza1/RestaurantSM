-- Permisos por cargo sobre el esquema de la version 9.
-- Ejecutar completo en la base RestaurantSM. Se puede repetir.
-- No crea la BD, no cambia IDs de rol ni reasigna cargos o usuarios existentes.
USE [RestaurantSM];
GO
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
GO

-- Procedimiento sp_Operacion_ExigirPersonal: consulta o modifica los datos de su gestion con parametros definidos.
CREATE OR ALTER PROCEDURE dbo.sp_Operacion_ExigirPersonal
    @Actor INT,
    @Cargo varchar(50) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    IF NOT EXISTS (SELECT 1
                   FROM dbo.usuario
                   WHERE idUsuario = @Actor
                         AND idRol IN (1, 2))
        THROW 50501, 'Esta operacion requiere una cuenta del personal.', 1;
    IF EXISTS (SELECT 1
               FROM dbo.empleado
               WHERE idUsuario = @Actor
                     AND activo = 0)
        THROW 50502, 'La cuenta del empleado esta inactiva.', 1;
    IF EXISTS (SELECT 1
               FROM dbo.usuario
               WHERE idUsuario = @Actor
                     AND idRol = 2)
       AND NOT EXISTS (SELECT 1
                       FROM dbo.empleado
                       WHERE idUsuario = @Actor
                             AND activo = 1)
        THROW 50502, 'La cuenta requiere un empleado activo asociado.', 1;
    -- El rol Administrador conserva acceso completo; el empleado necesita un cargo operativo.
    IF EXISTS (SELECT 1 FROM dbo.usuario WHERE idUsuario = @Actor AND idRol = 2)
       AND NOT EXISTS (
           SELECT 1 FROM dbo.empleado WHERE idUsuario = @Actor AND activo = 1
             AND UPPER(LTRIM(RTRIM(cargo))) IN ('MESERO', 'CAJERO')
             AND (@Cargo IS NULL OR UPPER(LTRIM(RTRIM(cargo))) = UPPER(@Cargo)))
        THROW 50801, 'Su cargo no tiene permiso para esta operacion. Solicite al administrador revisar Mesero o Cajero.', 1;
END
GO

-- Procedimiento sp_Usuario_ValidarLogin: consulta o modifica los datos de su gestion con parametros definidos.
CREATE OR ALTER PROCEDURE dbo.sp_Usuario_ValidarLogin
    @NombreUsuario VARCHAR (50),
    @Contrasena VARCHAR (255)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT u.idUsuario,
           u.nombreUsuario,
           u.idRol,
           r.nombre AS NombreRol,
           (SELECT e.cargo FROM dbo.empleado e WHERE e.idUsuario = u.idUsuario) AS Cargo
    FROM dbo.usuario AS u
         INNER JOIN
         dbo.rol AS r
         ON r.idRol = u.idRol
    WHERE u.nombreUsuario = @NombreUsuario
          AND u.contrasena = @Contrasena
          AND NOT EXISTS (SELECT 1
                          FROM dbo.empleado AS e
                          WHERE e.idUsuario = u.idUsuario
                                AND e.activo = 0)
          AND NOT EXISTS (SELECT 1
                          FROM dbo.cliente AS c
                          WHERE c.idUsuario = u.idUsuario
                                AND c.activo = 0);
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
    IF @IdRol = 2 AND (@Cargo IS NULL OR UPPER(LTRIM(RTRIM(@Cargo))) NOT IN ('MESERO', 'CAJERO'))
        THROW 50802, 'Seleccione Mesero o Cajero como cargo del empleado.', 1;
    IF @IdRol = 2
        SET @Cargo = CASE WHEN UPPER(LTRIM(RTRIM(@Cargo))) = 'MESERO' THEN 'Mesero' ELSE 'Cajero' END;
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
    IF @IdRol = 2 AND (@Cargo IS NULL OR UPPER(LTRIM(RTRIM(@Cargo))) NOT IN ('MESERO', 'CAJERO'))
        THROW 50802, 'Seleccione Mesero o Cajero como cargo del empleado.', 1;
    IF @IdRol = 2
        SET @Cargo = CASE WHEN UPPER(LTRIM(RTRIM(@Cargo))) = 'MESERO' THEN 'Mesero' ELSE 'Cajero' END;
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

-- Procedimiento sp_Operacion_Clientes: consulta o modifica los datos de su gestion con parametros definidos.
CREATE OR ALTER PROCEDURE dbo.sp_Operacion_Clientes
    @Actor INT
AS
BEGIN
    SET NOCOUNT ON;
    EXECUTE dbo.sp_Operacion_ExigirPersonal @Actor, 'Mesero';
    SELECT idCliente AS IdCliente,
           CONCAT(pNombreCl, ' ', pApellidoCl) AS Nombre,
           cedula AS Cedula,
           telefono AS Telefono,
           email AS Email
    FROM dbo.cliente
    WHERE activo = 1
    ORDER BY pNombreCl, pApellidoCl;
END
GO

-- Procedimiento sp_Operacion_AsignarMesa: consulta o modifica los datos de su gestion con parametros definidos.
CREATE OR ALTER PROCEDURE dbo.sp_Operacion_AsignarMesa
    @Actor INT,
    @IdPedido INT,
    @IdMesa INT
AS
BEGIN
    SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    EXECUTE dbo.sp_Operacion_ExigirPersonal @Actor, 'Mesero';
    BEGIN TRY
        BEGIN TRANSACTION;
        DECLARE @Cliente AS INT, @Anterior AS INT;
        SELECT @Cliente = idCliente,
               @Anterior = idMesa
        FROM dbo.pedido
        WHERE idPedido = @IdPedido
              AND estado IN ('Pendiente', 'En preparación', 'Listo');
        IF @Cliente IS NULL
            THROW 50531, 'Solo se asigna mesa antes de entregar el pedido.', 1;
        IF NOT EXISTS (SELECT 1
                       FROM dbo.mesa
                       WHERE idMesa = @IdMesa
                             AND activa = 1
                             AND estado IN ('Disponible', 'Reservada', 'Ocupada'))
            THROW 50532, 'La mesa esta inactiva o en limpieza.', 1;
        IF EXISTS (SELECT 1
                   FROM dbo.mesa
                   WHERE idMesa = @IdMesa
                         AND estado = 'Ocupada')
           AND NOT EXISTS (SELECT 1
                           FROM dbo.pedido
                           WHERE idMesa = @IdMesa
                                 AND idCliente = @Cliente
                                 AND estado NOT IN ('Pagado', 'Cancelado'))
           AND NOT EXISTS (SELECT 1
                           FROM dbo.reservacion
                           WHERE idMesa = @IdMesa
                                 AND idCliente = @Cliente
                                 AND estado = 'Atendida'
                                 AND ABS(DATEDIFF(minute, GETDATE(), fechaHoraReserva)) < 120)
            THROW 50533, 'La mesa esta ocupada por otra atencion. Libere y limpie la mesa antes de reutilizarla.', 1;
        IF EXISTS (SELECT 1
                   FROM dbo.pedido
                   WHERE idMesa = @IdMesa
                         AND idPedido <> @IdPedido
                         AND idCliente <> @Cliente
                         AND estado NOT IN ('Pagado', 'Cancelado'))
            THROW 50533, 'La mesa tiene pedidos abiertos de otro cliente.', 1;
        IF EXISTS (SELECT 1
                   FROM dbo.reservacion
                   WHERE idMesa = @IdMesa
                         AND idCliente <> @Cliente
                         AND estado IN ('Pendiente', 'Confirmada', 'Atendida')
                         AND ABS(DATEDIFF(minute, GETDATE(), fechaHoraReserva)) < 120)
            THROW 50533, 'La mesa tiene una reserva de otro cliente en este horario.', 1;
        UPDATE dbo.pedido
        SET idMesa = @IdMesa
        WHERE idPedido = @IdPedido;
        UPDATE dbo.mesa
        SET estado = 'Ocupada'
        WHERE idMesa = @IdMesa;
        IF @Anterior IS NOT NULL
           AND @Anterior <> @IdMesa
           AND NOT EXISTS (SELECT 1
                           FROM dbo.pedido
                           WHERE idMesa = @Anterior
                                 AND estado NOT IN ('Pagado', 'Cancelado'))
            UPDATE dbo.mesa
            SET estado = 'En limpieza'
            WHERE idMesa = @Anterior;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK;
        THROW;
    END CATCH
END
GO

-- Procedimiento sp_Operacion_CambiarMesa: consulta o modifica los datos de su gestion con parametros definidos.
CREATE OR ALTER PROCEDURE dbo.sp_Operacion_CambiarMesa
    @Actor INT,
    @IdMesa INT,
    @Anterior VARCHAR (20)
AS
BEGIN
    SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    EXECUTE dbo.sp_Operacion_ExigirPersonal @Actor, 'Mesero';
    IF @Anterior NOT IN ('Ocupada', 'En limpieza')
        THROW 50534, 'Seleccione una mesa ocupada o en limpieza.', 1;
    BEGIN TRY
        BEGIN TRANSACTION;
        IF NOT EXISTS (SELECT 1
                       FROM dbo.mesa
                       WHERE idMesa = @IdMesa
                             AND activa = 1
                             AND estado = @Anterior)
            THROW 50535, 'La mesa cambio. Recargue antes de continuar.', 1;
        IF EXISTS (SELECT 1
                   FROM dbo.pedido
                   WHERE idMesa = @IdMesa
                         AND estado NOT IN ('Pagado', 'Cancelado'))
            THROW 50536, 'Primero cierre los pedidos pendientes de esta mesa.', 1;
        UPDATE dbo.mesa
        SET estado = CASE WHEN @Anterior = 'Ocupada' THEN 'En limpieza' ELSE 'Disponible' END
        WHERE idMesa = @IdMesa;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK;
        THROW;
    END CATCH
END
GO

-- Procedimiento sp_Operacion_EstadoPedido: consulta o modifica los datos de su gestion con parametros definidos.
CREATE OR ALTER PROCEDURE dbo.sp_Operacion_EstadoPedido
    @Actor INT,
    @IdPedido INT,
    @Anterior VARCHAR (20),
    @Nuevo VARCHAR (20)
AS
BEGIN
    SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    EXECUTE dbo.sp_Operacion_ExigirPersonal @Actor, 'Mesero';
    IF NOT ((@Anterior = 'Pendiente'
             AND @Nuevo = 'En preparación')
            OR (@Anterior = 'En preparación'
                AND @Nuevo = 'Listo')
            OR (@Anterior = 'Listo'
                AND @Nuevo = 'Entregado'))
        THROW 50512, 'La transicion de estado no esta permitida.', 1;
    BEGIN TRY
        BEGIN TRANSACTION;
        DECLARE @Empleado AS INT = (SELECT idEmpleado
                                    FROM dbo.empleado
                                    WHERE idUsuario = @Actor
                                          AND activo = 1);
        UPDATE dbo.pedido
        SET estado = @Nuevo,
            idEmpleado = COALESCE (idEmpleado, @Empleado)
        WHERE idPedido = @IdPedido
              AND estado = @Anterior;
        IF @@ROWCOUNT <> 1
            THROW 50513, 'El pedido cambio. Recargue antes de continuar.', 1;
        INSERT dbo.historialPedido (idPedido, estadoAnterior, estadoNuevo, idUsuario)
        VALUES                    (@IdPedido, @Anterior, @Nuevo, @Actor);
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK;
        THROW;
    END CATCH
END
GO

-- Procedimiento sp_Operacion_EstadoReserva: consulta o modifica los datos de su gestion con parametros definidos.
CREATE OR ALTER PROCEDURE dbo.sp_Operacion_EstadoReserva
    @Actor INT,
    @IdReserva INT,
    @Anterior VARCHAR (20),
    @Nuevo VARCHAR (20)
AS
BEGIN
    SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    EXECUTE dbo.sp_Operacion_ExigirPersonal @Actor, 'Mesero';
    IF @Nuevo NOT IN ('Atendida', 'No asistio', 'Cancelada')
        THROW 50523, 'Seleccione una accion de reserva valida.', 1;
    BEGIN TRY
        BEGIN TRANSACTION;
        DECLARE @Mesa AS INT, @Fecha AS DATETIME, @Estado AS VARCHAR (20);
        SELECT @Mesa = idMesa,
               @Fecha = fechaHoraReserva,
               @Estado = estado
        FROM dbo.reservacion
        WHERE idReservacion = @IdReserva;
        IF @Estado IS NULL
           OR @Estado <> @Anterior
           OR @Estado NOT IN ('Pendiente', 'Confirmada')
            THROW 50524, 'La reserva cambio o ya fue cerrada.', 1;
        IF @Nuevo = 'No asistio'
           AND GETDATE() < DATEADD(minute, 30, @Fecha)
            THROW 50525, 'Espere 30 minutos despues de la reserva para marcar ausencia.', 1;
        IF @Nuevo = 'Atendida'
           AND GETDATE() < DATEADD(minute, -30, @Fecha)
            THROW 50525, 'La reserva aun no esta dentro del horario de llegada.', 1;
        IF @Nuevo = 'Atendida'
           AND EXISTS (SELECT 1
                       FROM dbo.mesa
                       WHERE idMesa = @Mesa
                             AND (activa = 0
                                  OR estado NOT IN ('Disponible', 'Reservada')))
            THROW 50526, 'La mesa no esta disponible para recibir al cliente.', 1;
        UPDATE dbo.reservacion
        SET estado = @Nuevo
        WHERE idReservacion = @IdReserva;
        IF @Nuevo = 'Atendida'
            UPDATE dbo.mesa
            SET estado = 'Ocupada'
            WHERE idMesa = @Mesa;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK;
        THROW;
    END CATCH
END
GO

-- Procedimiento sp_Operacion_Mesas: consulta o modifica los datos de su gestion con parametros definidos.
CREATE OR ALTER PROCEDURE dbo.sp_Operacion_Mesas
    @Actor INT
AS
BEGIN
    SET NOCOUNT ON;
    EXECUTE dbo.sp_Operacion_ExigirPersonal @Actor, 'Mesero';
    SELECT idMesa AS IdMesa,
           CONCAT('Mesa ', numero, ' - ', estado) AS Nombre,
           numero AS Numero,
           estado AS Estado
    FROM dbo.mesa
    WHERE activa = 1
    ORDER BY numero;
END
GO

-- Procedimiento sp_Operacion_Reservas: consulta o modifica los datos de su gestion con parametros definidos.
CREATE OR ALTER PROCEDURE dbo.sp_Operacion_Reservas
    @Actor INT
AS
BEGIN
    SET NOCOUNT ON;
    EXECUTE dbo.sp_Operacion_ExigirPersonal @Actor, 'Mesero';
    SELECT r.idReservacion AS IdReserva,
           r.idMesa AS IdMesa,
           m.numero AS Mesa,
           CONCAT(c.pNombreCl, ' ', c.pApellidoCl) AS Cliente,
           r.fechaHoraReserva AS Fecha,
           r.cantidadPersonas AS Personas,
           r.estado AS Estado
    FROM dbo.reservacion AS r
         INNER JOIN
         dbo.cliente AS c
         ON c.idCliente = r.idCliente
         INNER JOIN
         dbo.mesa AS m
         ON m.idMesa = r.idMesa
    ORDER BY r.fechaHoraReserva DESC;
END
GO

-- Valida al usuario y crea la cabecera; al editar devuelve primero las unidades anteriores.
-- Un pedido se guarda en tres pasos: cabecera, lineas y total.
-- La DAL ejecuta estos pasos en una sola transaccion y hace ROLLBACK si falla alguno.
CREATE OR ALTER PROCEDURE dbo.sp_Pedido_Iniciar
    @Actor int,
    @IdCliente int = NULL,
    @IdPedido int = 0,
    @Solicitud uniqueidentifier,
    @NumeroLineas int
AS
BEGIN
    SET NOCOUNT ON;
    IF @@TRANCOUNT = 0 THROW 50700, 'Inicie una transaccion antes de guardar el pedido.', 1;
    IF @Solicitud IS NULL OR @NumeroLineas NOT BETWEEN 1 AND 200
        THROW 50701, 'El pedido necesita entre 1 y 200 productos.', 1;

    DECLARE @Rol int;
    DECLARE @Empleado int;
    SELECT @Rol = idRol FROM dbo.usuario WHERE idUsuario = @Actor;
    IF @Rol = 3
        EXEC dbo.sp_Cliente_ExigirActivo @Actor, @IdCliente OUTPUT;
    ELSE
    BEGIN
        EXEC dbo.sp_Operacion_ExigirPersonal @Actor, 'Mesero';
        SELECT @Empleado = idEmpleado FROM dbo.empleado WHERE idUsuario = @Actor AND activo = 1;
    END;
    IF NOT EXISTS (SELECT 1 FROM dbo.cliente WHERE idCliente = @IdCliente AND activo = 1)
        THROW 50702, 'Seleccione un cliente activo.', 1;

    -- Una misma solicitud no debe crear el pedido dos veces.
    DECLARE @Existente int;
    SELECT @Existente = idPedido FROM dbo.pedido WHERE solicitud = @Solicitud;
    IF @Existente IS NOT NULL
    BEGIN
        IF NOT EXISTS (SELECT 1 FROM dbo.pedido WHERE idPedido = @Existente AND idCliente = @IdCliente)
            OR (@IdPedido <> 0 AND @IdPedido <> @Existente)
            THROW 50703, 'La solicitud pertenece a otro pedido o cliente.', 1;
        IF (SELECT COUNT(*) FROM dbo.detallePedido WHERE idPedido = @Existente) <> @NumeroLineas
            THROW 50703, 'La solicitud ya fue guardada con otro detalle.', 1;
        SELECT @Existente AS IdPedido, CAST(1 AS bit) AS Reintento;
        RETURN;
    END;

    IF @IdPedido = 0
    BEGIN
        INSERT INTO dbo.pedido
            (idCliente, idEmpleado, fechaHora, estado, subtotal, total, esDelivery, costoEnvio, solicitud)
        VALUES
            (@IdCliente, @Empleado, GETDATE(), 'Pendiente', 0, 0, 0, 0, @Solicitud);
        SET @IdPedido = CONVERT(int, SCOPE_IDENTITY());
        INSERT INTO dbo.historialPedido(idPedido, estadoAnterior, estadoNuevo, idUsuario)
        VALUES (@IdPedido, NULL, 'Pendiente', @Actor);
    END
    ELSE
    BEGIN
        IF NOT EXISTS (SELECT 1 FROM dbo.pedido WHERE idPedido = @IdPedido AND idCliente = @IdCliente AND estado = 'Pendiente')
            THROW 50704, 'Solo puede editar un pedido pendiente de este cliente.', 1;
        IF EXISTS (SELECT 1 FROM dbo.factura WHERE idPedido = @IdPedido)
            THROW 50704, 'El pedido ya tiene factura.', 1;

        -- Antes de reemplazar el detalle, se devuelven sus unidades al inventario.
        INSERT INTO dbo.movimientoInventario
            (idProducto, producto, anterior, nuevo, diferencia, idUsuario, usuario, motivo)
        SELECT i.idProducto, p.nombre, i.stockActual, i.stockActual + d.cantidad, d.cantidad,
            @Actor, u.nombreUsuario, 'Devolucion para editar pedido'
        FROM dbo.detallePedido d
        INNER JOIN dbo.inventarioProducto i ON i.idProducto = d.idProducto
        INNER JOIN dbo.producto p ON p.idProducto = i.idProducto
        INNER JOIN dbo.usuario u ON u.idUsuario = @Actor
        WHERE d.idPedido = @IdPedido AND d.controlInventario = 1;

        UPDATE i SET stockActual = i.stockActual + d.cantidad, revision = revision + 1
        FROM dbo.inventarioProducto i
        INNER JOIN dbo.detallePedido d ON d.idProducto = i.idProducto
        WHERE d.idPedido = @IdPedido AND d.controlInventario = 1;
        DELETE FROM dbo.detallePedido WHERE idPedido = @IdPedido;
        UPDATE dbo.pedido SET solicitud = @Solicitud WHERE idPedido = @IdPedido;
    END;
    SELECT @IdPedido AS IdPedido, CAST(0 AS bit) AS Reintento;
END;
GO

-- Valida un producto, descuenta existencias y guarda su detalle con parametros normales.
CREATE OR ALTER PROCEDURE dbo.sp_Pedido_AgregarLinea
    @Actor int,
    @IdPedido int,
    @IdProducto int,
    @Cantidad int,
    @Precio decimal(10,2),
    @Reintento bit
AS
BEGIN
    SET NOCOUNT ON;
    IF @@TRANCOUNT = 0 THROW 50700, 'Inicie una transaccion antes de guardar el pedido.', 1;
    IF @Cantidad NOT BETWEEN 1 AND 999 OR @Precio IS NULL OR @Precio <= 0
        THROW 50705, 'Revise la cantidad y el precio del producto.', 1;

    DECLARE @Cliente int;
    IF EXISTS (SELECT 1 FROM dbo.usuario WHERE idUsuario = @Actor AND idRol = 3)
    BEGIN
        EXEC dbo.sp_Cliente_ExigirActivo @Actor, @Cliente OUTPUT;
        IF NOT EXISTS (SELECT 1 FROM dbo.pedido WHERE idPedido = @IdPedido AND idCliente = @Cliente)
            THROW 50706, 'El pedido no pertenece a su cuenta.', 1;
    END
    ELSE EXEC dbo.sp_Operacion_ExigirPersonal @Actor, 'Mesero';

    IF @Reintento = 1
    BEGIN
        IF NOT EXISTS (SELECT 1 FROM dbo.detallePedido WHERE idPedido = @IdPedido
            AND idProducto = @IdProducto AND cantidad = @Cantidad AND precioUnitario = @Precio)
            THROW 50703, 'La solicitud ya fue guardada con otro detalle.', 1;
        RETURN;
    END;
    IF NOT EXISTS (SELECT 1 FROM dbo.pedido WHERE idPedido = @IdPedido AND estado = 'Pendiente')
        THROW 50704, 'El pedido ya no esta pendiente.', 1;
    IF EXISTS (SELECT 1 FROM dbo.detallePedido WHERE idPedido = @IdPedido AND idProducto = @IdProducto)
        THROW 50705, 'El producto ya esta incluido en el pedido.', 1;
    IF NOT EXISTS (SELECT 1 FROM dbo.producto p INNER JOIN dbo.categoria c ON c.idCategoria = p.idCategoria
        WHERE p.idProducto = @IdProducto AND p.disponible = 1 AND c.activa = 1 AND p.precio = @Precio)
        THROW 50707, 'Cambio el precio o la disponibilidad. Recargue el catalogo.', 1;

    DECLARE @Anterior int;
    SELECT @Anterior = stockActual FROM dbo.inventarioProducto WHERE idProducto = @IdProducto AND activo = 1;
    -- La condicion del UPDATE impide descontar mas unidades de las disponibles.
    UPDATE dbo.inventarioProducto
    SET stockActual = stockActual - @Cantidad, revision = revision + 1
    WHERE idProducto = @IdProducto AND activo = 1 AND stockActual >= @Cantidad;
    IF @@ROWCOUNT = 0 THROW 50708, 'El producto necesita inventario activo y existencias suficientes.', 1;

    EXEC dbo.sp_Inventario_RegistrarMovimiento @Actor, @IdProducto, @Anterior,
        @Cantidad, 'Salida', 'Registro de pedido';
    INSERT INTO dbo.detallePedido
        (idPedido, idProducto, cantidad, precioUnitario, subtotal, total, controlInventario)
    VALUES (@IdPedido, @IdProducto, @Cantidad, @Precio, @Cantidad * @Precio, @Cantidad * @Precio, 1);
END;
GO

-- Verifica la cantidad de lineas y calcula subtotal y total con SUM.
CREATE OR ALTER PROCEDURE dbo.sp_Pedido_Finalizar
    @Actor int,
    @IdPedido int,
    @NumeroLineas int,
    @Reintento bit
AS
BEGIN
    SET NOCOUNT ON;
    IF @@TRANCOUNT = 0 THROW 50700, 'Inicie una transaccion antes de guardar el pedido.', 1;
    DECLARE @Cliente int;
    IF EXISTS (SELECT 1 FROM dbo.usuario WHERE idUsuario = @Actor AND idRol = 3)
    BEGIN
        EXEC dbo.sp_Cliente_ExigirActivo @Actor, @Cliente OUTPUT;
        IF NOT EXISTS (SELECT 1 FROM dbo.pedido WHERE idPedido = @IdPedido AND idCliente = @Cliente)
            THROW 50706, 'El pedido no pertenece a su cuenta.', 1;
    END
    ELSE EXEC dbo.sp_Operacion_ExigirPersonal @Actor, 'Mesero';
    IF @Reintento = 1 RETURN;
    IF @NumeroLineas NOT BETWEEN 1 AND 200 OR
        (SELECT COUNT(*) FROM dbo.detallePedido WHERE idPedido = @IdPedido) <> @NumeroLineas
        THROW 50709, 'El detalle del pedido esta incompleto.', 1;

    DECLARE @Subtotal decimal(18,2);
    SELECT @Subtotal = SUM(total) FROM dbo.detallePedido WHERE idPedido = @IdPedido;
    IF @Subtotal IS NULL OR @Subtotal <= 0 OR @Subtotal > 99999999.99
        THROW 50710, 'El total supera el importe permitido.', 1;
    UPDATE dbo.pedido SET subtotal = @Subtotal, total = @Subtotal + costoEnvio
    WHERE idPedido = @IdPedido AND estado = 'Pendiente';
    IF @@ROWCOUNT = 0 THROW 50704, 'El pedido ya no esta pendiente.', 1;
END;
GO

-- Cancela solo pedidos pendientes y devuelve las unidades dentro de una transaccion.
CREATE OR ALTER PROCEDURE dbo.sp_Pedido_CancelarComun @Actor int,
    @IdPedido int
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;
    DECLARE @Cliente int;
    DECLARE @Rol int;
    SELECT @Rol = idRol FROM dbo.usuario WHERE idUsuario = @Actor;
    IF @Rol = 3 EXEC dbo.sp_Cliente_ExigirActivo @Actor, @Cliente OUTPUT;
    ELSE EXEC dbo.sp_Operacion_ExigirPersonal @Actor, 'Mesero';
    BEGIN TRY
        BEGIN TRANSACTION;
        UPDATE dbo.pedido SET estado = 'Cancelado'
        WHERE idPedido = @IdPedido AND estado = 'Pendiente' AND (@Rol <> 3 OR idCliente = @Cliente);
        IF @@ROWCOUNT = 0 THROW 50711, 'Solo puede cancelar un pedido pendiente permitido.', 1;
        IF EXISTS (SELECT 1 FROM dbo.factura WHERE idPedido = @IdPedido)
            THROW 50704, 'El pedido ya tiene factura.', 1;

        INSERT INTO dbo.movimientoInventario
            (idProducto, producto, anterior, nuevo, diferencia, idUsuario, usuario, motivo)
        SELECT i.idProducto, p.nombre, i.stockActual, i.stockActual + d.cantidad, d.cantidad,
            @Actor, u.nombreUsuario, 'Cancelacion de pedido'
        FROM dbo.detallePedido d
        INNER JOIN dbo.inventarioProducto i ON i.idProducto = d.idProducto
        INNER JOIN dbo.producto p ON p.idProducto = i.idProducto
        INNER JOIN dbo.usuario u ON u.idUsuario = @Actor
        WHERE d.idPedido = @IdPedido AND d.controlInventario = 1;

        UPDATE i SET stockActual = i.stockActual + d.cantidad, revision = revision + 1
        FROM dbo.inventarioProducto i
        INNER JOIN dbo.detallePedido d ON d.idProducto = i.idProducto
        WHERE d.idPedido = @IdPedido AND d.controlInventario = 1;
        INSERT INTO dbo.historialPedido(idPedido, estadoAnterior, estadoNuevo, idUsuario)
        VALUES (@IdPedido, 'Pendiente', 'Cancelado', @Actor);
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO

-- Procedimiento sp_Cobro_Cotizar: consulta o modifica los datos de su gestion con parametros definidos.
CREATE OR ALTER PROCEDURE dbo.sp_Cobro_Cotizar
    @Actor INT,
    @IdPedido INT
AS
BEGIN
    SET NOCOUNT ON;
    EXECUTE dbo.sp_Operacion_ExigirPersonal @Actor, 'Cajero';
    IF NOT EXISTS (SELECT 1
                   FROM dbo.pedido
                   WHERE idPedido = @IdPedido
                         AND estado = 'Entregado')
        THROW 50514, 'El pedido debe estar entregado antes de cobrar.', 1;
    SELECT p.idPedido AS IdPedido,
           CONCAT(c.pNombreCl, ' ', c.pApellidoCl) AS Cliente,
           p.subtotal AS Subtotal,
           p.costoEnvio AS CostoEnvio,
           p.total AS Total,
           cfg.tasaImpuesto AS TasaImpuesto,
           CONVERT (DECIMAL (10, 2), ROUND(p.subtotal - p.subtotal / (1 + cfg.tasaImpuesto / 100), 2)) AS Impuestos
    FROM dbo.pedido AS p
         INNER JOIN
         dbo.cliente AS c
         ON c.idCliente = p.idCliente CROSS JOIN dbo.configuracionRestaurante AS cfg
    WHERE p.idPedido = @IdPedido;
END
GO

-- Valida el cobro y guarda factura, detalle y pago juntos. Un fallo revierte la venta.
CREATE OR ALTER PROCEDURE dbo.sp_Cobro_Registrar
    @Actor int,
    @IdPedido int,
    @Metodo varchar(40),
    @Recibido decimal(10,2),
    @TotalEsperado decimal(10,2),
    @Solicitud uniqueidentifier,
    @Notas nvarchar(200) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    -- Evita que dos cajeros confirmen el mismo cobro al mismo tiempo.
    SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;
    EXEC dbo.sp_Operacion_ExigirPersonal @Actor, 'Cajero';
    IF @Metodo NOT IN ('Efectivo', 'Tarjeta', 'Transferencia') OR @Solicitud IS NULL
        THROW 50515, 'Seleccione un metodo de pago valido.', 1;

    BEGIN TRY
        BEGIN TRANSACTION;
        -- 1. Si ya se confirmo esta solicitud, devolver el mismo resultado.
        DECLARE @Pago int;
        DECLARE @PedidoAnterior int;
        DECLARE @MetodoAnterior varchar(40);
        DECLARE @RecibidoAnterior decimal(10,2);
        DECLARE @TotalAnterior decimal(10,2);
        SELECT @Pago = pa.idPago, @PedidoAnterior = f.idPedido,
            @MetodoAnterior = pa.metodoDePago, @RecibidoAnterior = pa.recibido, @TotalAnterior = pa.monto
        FROM dbo.pago pa
        INNER JOIN dbo.factura f ON f.idFactura = pa.idFactura
        WHERE pa.solicitud = @Solicitud;
        IF @Pago IS NOT NULL
        BEGIN
            IF @PedidoAnterior <> @IdPedido OR @MetodoAnterior <> @Metodo
                OR @RecibidoAnterior <> @Recibido OR @TotalAnterior <> @TotalEsperado
                THROW 50516, 'La solicitud ya se utilizo con otros datos.', 1;
            COMMIT TRANSACTION;
            SELECT idFactura AS IdFactura, recibido AS Recibido, cambio AS Cambio
            FROM dbo.pago WHERE idPago = @Pago;
            RETURN;
        END;

        -- 2. Obtener el importe real y verificar que el pedido esta entregado.
        DECLARE @Total decimal(10,2);
        DECLARE @Subtotal decimal(10,2);
        DECLARE @Envio decimal(10,2);
        DECLARE @Cliente int;
        DECLARE @Estado varchar(20);
        DECLARE @Mesa int;
        DECLARE @TipoPedido varchar(20);
        SELECT @Total = p.total, @Subtotal = p.subtotal, @Envio = p.costoEnvio,
            @Cliente = p.idCliente, @Estado = p.estado, @Mesa = m.numero,
            @TipoPedido = CASE WHEN p.esDelivery = 1 THEN 'Entrega' ELSE 'Local' END
        FROM dbo.pedido p LEFT JOIN dbo.mesa m ON m.idMesa = p.idMesa
        WHERE p.idPedido = @IdPedido;
        IF @Estado IS NULL OR @Estado <> 'Entregado'
            THROW 50514, 'El pedido debe estar entregado y sin pagar.', 1;
        IF EXISTS (SELECT 1 FROM dbo.factura WHERE idPedido = @IdPedido)
            THROW 50517, 'El pedido ya tiene factura.', 1;
        DECLARE @SumaDetalle decimal(18,2);
        SELECT @SumaDetalle = SUM(total) FROM dbo.detallePedido WHERE idPedido = @IdPedido;
        IF @SumaDetalle IS NULL OR @Subtotal <> @SumaDetalle OR @Total <> @Subtotal + @Envio
            THROW 50518, 'Los importes del pedido no coinciden con su detalle.', 1;
        IF @Total <> @TotalEsperado OR @Total <= 0
            THROW 50518, 'El importe cambio. Recargue el pedido.', 1;
        IF @Recibido IS NULL OR @Recibido < @Total OR (@Metodo <> 'Efectivo' AND @Recibido <> @Total)
            THROW 50519, 'Revise el importe recibido.', 1;

        -- 3. Calcular el impuesto incluido y copiar los datos de la venta.
        DECLARE @Tasa decimal(5,2);
        DECLARE @Impuesto decimal(10,2);
        DECLARE @Restaurante nvarchar(120);
        DECLARE @Moneda varchar(5);
        SELECT @Tasa = tasaImpuesto, @Restaurante = nombre, @Moneda = moneda
        FROM dbo.configuracionRestaurante WHERE id = 1;
        SET @Impuesto = ROUND(@Subtotal - @Subtotal / (1 + @Tasa / 100), 2);
        DECLARE @Nombre nvarchar(330);
        DECLARE @Cedula varchar(30);
        DECLARE @Empleado nvarchar(200);
        SELECT @Nombre = CONCAT(pNombreCl, ' ', COALESCE(sNombreCl + ' ', ''), pApellidoCl, ' ', COALESCE(sApellidoCl, '')),
            @Cedula = cedula FROM dbo.cliente WHERE idCliente = @Cliente;
        SELECT @Empleado = nombreUsuario FROM dbo.usuario WHERE idUsuario = @Actor;

        -- 4. Guardar factura, productos y pago dentro de la misma transaccion.
        DECLARE @Factura int;
        INSERT INTO dbo.factura
            (idPedido, numeroFactura, fechaEmision, estado, subtotal, costoEnvio, descuento,
             impuestos, propina, total, clienteNombre, clienteCedula, empleadoNombre,
             tasaImpuesto, restauranteNombre, moneda, mesaNumero, tipoPedido)
        VALUES
            (@IdPedido, 'F-' + CONVERT(varchar(20), @IdPedido), GETDATE(), 'Pagada',
             @Subtotal - @Impuesto, @Envio, 0, @Impuesto, 0, @Total, @Nombre, @Cedula,
             @Empleado, @Tasa, @Restaurante, @Moneda, @Mesa, @TipoPedido);
        SET @Factura = CONVERT(int, SCOPE_IDENTITY());
        INSERT INTO dbo.facturaDetalle(idFactura, idProducto, producto, cantidad, precio, subtotal)
        SELECT @Factura, d.idProducto, p.nombre, d.cantidad, d.precioUnitario, d.total
        FROM dbo.detallePedido d INNER JOIN dbo.producto p ON p.idProducto = d.idProducto
        WHERE d.idPedido = @IdPedido;
        INSERT INTO dbo.pago(idFactura, metodoDePago, monto, fechaPago, recibido, cambio, solicitud, idUsuario, notas)
        VALUES (@Factura, @Metodo, @Total, GETDATE(), @Recibido, @Recibido - @Total, @Solicitud, @Actor, @Notas);
        UPDATE dbo.pedido SET estado = 'Pagado' WHERE idPedido = @IdPedido;
        INSERT INTO dbo.historialPedido(idPedido, estadoAnterior, estadoNuevo, idUsuario)
        VALUES (@IdPedido, 'Entregado', 'Pagado', @Actor);

        -- 5. Dejar la mesa en limpieza cuando ya no tiene pedidos abiertos.
        UPDATE m SET estado = 'En limpieza'
        FROM dbo.mesa m INNER JOIN dbo.pedido p ON p.idMesa = m.idMesa
        WHERE p.idPedido = @IdPedido AND NOT EXISTS (
            SELECT 1 FROM dbo.pedido otro WHERE otro.idMesa = m.idMesa
            AND otro.estado NOT IN ('Pagado', 'Cancelado'));
        COMMIT TRANSACTION;
        SELECT @Factura AS IdFactura, @Recibido AS Recibido, @Recibido - @Total AS Cambio;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO

-- Procedimiento sp_Facturas_Listar: consulta o modifica los datos de su gestion con parametros definidos.
CREATE OR ALTER PROCEDURE dbo.sp_Facturas_Listar
    @Actor INT
AS
BEGIN
    SET NOCOUNT ON;
    EXECUTE dbo.sp_Operacion_ExigirPersonal @Actor, 'Cajero';
    SELECT idFactura AS IdFactura,
           idPedido AS IdPedido,
           numeroFactura AS Numero,
           fechaEmision AS Fecha,
           clienteNombre AS Cliente,
           estado AS Estado,
           total AS Total
    FROM dbo.factura
    ORDER BY idFactura DESC;
END
GO

-- Consulta la cabecera y los productos del pedido o de su factura conservada.
CREATE OR ALTER PROCEDURE dbo.sp_Documento_Obtener
    @Actor INT,
    @IdPedido INT,
    @Factura BIT
AS
BEGIN
    SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
        DECLARE @Bloqueo AS INT;
        SELECT @Bloqueo = idPedido
        FROM dbo.pedido
        WHERE idPedido = @IdPedido;
        DECLARE @Rol AS INT = (SELECT idRol
                               FROM dbo.usuario
                               WHERE idUsuario = @Actor), @Cliente AS INT;
        IF @Rol = 3
            BEGIN
                EXECUTE dbo.sp_Cliente_ExigirActivo @Actor, @Cliente OUTPUT;
                IF NOT EXISTS (SELECT 1
                               FROM dbo.pedido
                               WHERE idPedido = @IdPedido
                                     AND idCliente = @Cliente)
                    THROW 50520, 'El pedido no pertenece a su cuenta.', 1;
            END
        ELSE
            BEGIN
                IF @Factura = 1 EXEC dbo.sp_Operacion_ExigirPersonal @Actor, 'Cajero';
                ELSE EXEC dbo.sp_Operacion_ExigirPersonal @Actor;
            END;
        IF @Factura = 1
            BEGIN
                IF NOT EXISTS (SELECT 1
                               FROM dbo.factura
                               WHERE idPedido = @IdPedido
                                     AND estado = 'Pagada')
                    THROW 50521, 'El pedido todavia no tiene factura pagada.', 1;
                SELECT f.numeroFactura AS Numero,
                       f.fechaEmision AS Fecha,
                       f.clienteNombre AS Cliente,
                       f.clienteCedula AS Cedula,
                       f.empleadoNombre AS Empleado,
                       f.estado AS Estado,
                       f.subtotal AS Subtotal,
                       f.costoEnvio AS CostoEnvio,
                       f.impuestos AS Impuestos,
                       f.descuento AS Descuento,
                       f.total AS Total,
                       COALESCE (f.restauranteNombre, cfg.nombre) AS Restaurante,
                       COALESCE (f.moneda, cfg.moneda) AS Moneda,
                       COALESCE (f.tasaImpuesto, -1) AS TasaImpuesto,
                       COALESCE ((SELECT TOP (1) metodoDePago
                                  FROM dbo.pago
                                  WHERE idFactura = f.idFactura
                                  ORDER BY idPago), '') AS Metodo,
                       (SELECT TOP (1) COALESCE (recibido, monto)
                        FROM dbo.pago
                        WHERE idFactura = f.idFactura
                        ORDER BY idPago) AS Recibido,
                       COALESCE ((SELECT TOP (1) cambio
                                  FROM dbo.pago
                                  WHERE idFactura = f.idFactura
                                  ORDER BY idPago), 0) AS Cambio,
                       f.mesaNumero AS Mesa,
                       f.tipoPedido AS TipoPedido
                FROM dbo.factura AS f
                     INNER JOIN
                     dbo.pedido AS p
                     ON p.idPedido = f.idPedido CROSS JOIN dbo.configuracionRestaurante AS cfg
                WHERE f.idPedido = @IdPedido;
                SELECT fd.producto AS Producto,
                       fd.cantidad AS Cantidad,
                       fd.precio AS PrecioUnitario,
                       fd.subtotal AS Subtotal
                FROM dbo.facturaDetalle AS fd
                     INNER JOIN
                     dbo.factura AS f
                     ON f.idFactura = fd.idFactura
                WHERE f.idPedido = @IdPedido
                ORDER BY fd.id;
            END
        ELSE
            BEGIN
                IF NOT EXISTS (SELECT 1
                               FROM dbo.pedido
                               WHERE idPedido = @IdPedido)
                    THROW 50522, 'No existe el pedido.', 1;
                SELECT 'P-' + CONVERT (VARCHAR (20), p.idPedido) AS Numero,
                       p.fechaHora AS Fecha,
                       CONCAT(c.pNombreCl, ' ', c.pApellidoCl) AS Cliente,
                       c.cedula AS Cedula,
                       '' AS Empleado,
                       p.estado AS Estado,
                       p.subtotal AS Subtotal,
                       p.costoEnvio AS CostoEnvio,
                       CONVERT (DECIMAL (10, 2), 0) AS Impuestos,
                       CONVERT (DECIMAL (10, 2), 0) AS Descuento,
                       p.total AS Total,
                       cfg.nombre AS Restaurante,
                       cfg.moneda AS Moneda,
                       cfg.tasaImpuesto AS TasaImpuesto,
                       '' AS Metodo,
                       CONVERT (DECIMAL (10, 2), 0) AS Recibido,
                       CONVERT (DECIMAL (10, 2), 0) AS Cambio,
                       (SELECT numero
                        FROM dbo.mesa
                        WHERE idMesa = p.idMesa) AS Mesa,
                       CASE WHEN p.esDelivery = 1 THEN 'Entrega' ELSE 'Local' END AS TipoPedido
                FROM dbo.pedido AS p
                     INNER JOIN
                     dbo.cliente AS c
                     ON c.idCliente = p.idCliente CROSS JOIN dbo.configuracionRestaurante AS cfg
                WHERE p.idPedido = @IdPedido;
                SELECT pr.nombre AS Producto,
                       d.cantidad AS Cantidad,
                       d.precioUnitario AS PrecioUnitario,
                       d.total AS Subtotal
                FROM dbo.detallePedido AS d
                     INNER JOIN
                     dbo.producto AS pr
                     ON pr.idProducto = d.idProducto
                WHERE d.idPedido = @IdPedido
                ORDER BY d.idDetalle;
            END
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK;
        THROW;
    END CATCH
END
GO
