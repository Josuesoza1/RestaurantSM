-- Procedimientos de Pedidos.
USE [RestaurantSM];
GO
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
GO
-- Procedimiento sp_Cliente_Detalle: consulta o modifica los datos de su gestion con parametros definidos.
CREATE OR ALTER PROCEDURE dbo.sp_Cliente_Detalle
    @Actor INT,
    @IdPedido INT
AS
BEGIN
    DECLARE @Cliente AS INT;
    EXECUTE dbo.sp_Cliente_ExigirActivo @Actor, @Cliente OUTPUT;
    IF NOT EXISTS (SELECT 1
                   FROM dbo.pedido
                   WHERE idPedido = @IdPedido
                         AND idCliente = @Cliente)
        THROW 50218, 'El pedido no pertenece a su cuenta.', 1;
    SELECT d.idProducto AS IdProducto,
           p.nombre AS Producto,
           p.idCategoria AS IdCategoria,
           c.nombre AS Categoria,
           d.cantidad AS Cantidad,
           d.precioUnitario AS PrecioUnitario,
           d.total AS Subtotal
    FROM dbo.detallePedido AS d
         INNER JOIN
         dbo.producto AS p
         ON p.idProducto = d.idProducto
         INNER JOIN
         dbo.categoria AS c
         ON c.idCategoria = p.idCategoria
    WHERE d.idPedido = @IdPedido;
END
GO

-- Procedimiento sp_Cliente_Pedido_Cancelar: consulta o modifica los datos de su gestion con parametros definidos.
CREATE OR ALTER PROCEDURE dbo.sp_Cliente_Pedido_Cancelar
    @Actor INT,
    @IdPedido INT
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @Cliente AS INT;
    EXECUTE dbo.sp_Cliente_ExigirActivo @Actor, @Cliente OUTPUT;
    EXECUTE dbo.sp_Pedido_CancelarComun @Actor, @IdPedido;
END
GO

-- Procedimiento sp_Cliente_Pedidos: consulta o modifica los datos de su gestion con parametros definidos.
CREATE OR ALTER PROCEDURE dbo.sp_Cliente_Pedidos
    @Actor INT
AS
BEGIN
    DECLARE @Cliente AS INT;
    EXECUTE dbo.sp_Cliente_ExigirActivo @Actor, @Cliente OUTPUT;
    SELECT idPedido AS IdPedido,
           fechaHora AS Fecha,
           estado AS Estado,
           total AS Total
    FROM dbo.pedido
    WHERE idCliente = @Cliente
    ORDER BY idPedido DESC;
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
    EXECUTE dbo.sp_Operacion_ExigirPersonal @Actor;
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
    EXECUTE dbo.sp_Operacion_ExigirPersonal @Actor;
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

-- Procedimiento sp_Operacion_Clientes: consulta o modifica los datos de su gestion con parametros definidos.
CREATE OR ALTER PROCEDURE dbo.sp_Operacion_Clientes
    @Actor INT
AS
BEGIN
    SET NOCOUNT ON;
    EXECUTE dbo.sp_Operacion_ExigirPersonal @Actor;
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

-- Procedimiento sp_Operacion_Detalle: consulta o modifica los datos de su gestion con parametros definidos.
CREATE OR ALTER PROCEDURE dbo.sp_Operacion_Detalle
    @Actor INT,
    @IdPedido INT
AS
BEGIN
    SET NOCOUNT ON;
    EXECUTE dbo.sp_Operacion_ExigirPersonal @Actor;
    SELECT d.idProducto AS IdProducto,
           p.nombre AS Producto,
           p.idCategoria AS IdCategoria,
           c.nombre AS Categoria,
           d.cantidad AS Cantidad,
           d.precioUnitario AS PrecioUnitario,
           d.total AS Subtotal
    FROM dbo.detallePedido AS d
         INNER JOIN
         dbo.producto AS p
         ON p.idProducto = d.idProducto
         INNER JOIN
         dbo.categoria AS c
         ON c.idCategoria = p.idCategoria
    WHERE d.idPedido = @IdPedido
    ORDER BY d.idDetalle;
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
    EXECUTE dbo.sp_Operacion_ExigirPersonal @Actor;
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

-- Procedimiento sp_Operacion_Mesas: consulta o modifica los datos de su gestion con parametros definidos.
CREATE OR ALTER PROCEDURE dbo.sp_Operacion_Mesas
    @Actor INT
AS
BEGIN
    SET NOCOUNT ON;
    EXECUTE dbo.sp_Operacion_ExigirPersonal @Actor;
    SELECT idMesa AS IdMesa,
           CONCAT('Mesa ', numero, ' - ', estado) AS Nombre,
           numero AS Numero,
           estado AS Estado
    FROM dbo.mesa
    WHERE activa = 1
    ORDER BY numero;
END
GO

-- Procedimiento sp_Operacion_Pedidos: consulta o modifica los datos de su gestion con parametros definidos.
CREATE OR ALTER PROCEDURE dbo.sp_Operacion_Pedidos
    @Actor INT
AS
BEGIN
    SET NOCOUNT ON;
    EXECUTE dbo.sp_Operacion_ExigirPersonal @Actor;
    SELECT p.idPedido AS IdPedido,
           p.fechaHora AS Fecha,
           CONCAT(c.pNombreCl, ' ', c.pApellidoCl) AS Cliente,
           p.estado AS Estado,
           p.total AS Total,
           p.idMesa AS IdMesa,
           m.numero AS Mesa,
           CASE WHEN EXISTS (SELECT 1
                             FROM dbo.pago AS pa
                                  INNER JOIN
                                  dbo.factura AS f
                                  ON f.idFactura = pa.idFactura
                             WHERE f.idPedido = p.idPedido) THEN 'Pagado' ELSE 'Sin pagar' END AS Pago
    FROM dbo.pedido AS p
         INNER JOIN
         dbo.cliente AS c
         ON c.idCliente = p.idCliente
         LEFT OUTER JOIN
         dbo.mesa AS m
         ON m.idMesa = p.idMesa
    ORDER BY p.idPedido DESC;
END
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
    ELSE EXEC dbo.sp_Operacion_ExigirPersonal @Actor;

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
    ELSE EXEC dbo.sp_Operacion_ExigirPersonal @Actor;
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
    ELSE EXEC dbo.sp_Operacion_ExigirPersonal @Actor;
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
        EXEC dbo.sp_Operacion_ExigirPersonal @Actor;
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
