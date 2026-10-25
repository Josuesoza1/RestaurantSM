-- Procedimientos de Cobros y facturas.
USE [RestaurantSM];
GO
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
GO
-- Procedimiento sp_Cobro_Cotizar: consulta o modifica los datos de su gestion con parametros definidos.
CREATE OR ALTER PROCEDURE dbo.sp_Cobro_Cotizar
    @Actor INT,
    @IdPedido INT
AS
BEGIN
    SET NOCOUNT ON;
    EXECUTE dbo.sp_Operacion_ExigirPersonal @Actor;
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
    EXEC dbo.sp_Operacion_ExigirPersonal @Actor;
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
            EXECUTE dbo.sp_Operacion_ExigirPersonal @Actor;
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

-- Procedimiento sp_Facturas_Listar: consulta o modifica los datos de su gestion con parametros definidos.
CREATE OR ALTER PROCEDURE dbo.sp_Facturas_Listar
    @Actor INT
AS
BEGIN
    SET NOCOUNT ON;
    EXECUTE dbo.sp_Operacion_ExigirPersonal @Actor;
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
