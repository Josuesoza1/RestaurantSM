USE [RestaurantSM];
GO
SET XACT_ABORT ON;
SET QUOTED_IDENTIFIER ON;
SET ANSI_NULLS ON;
SET ANSI_PADDING ON;
SET ANSI_WARNINGS ON;
SET CONCAT_NULL_YIELDS_NULL ON;
SET ARITHABORT ON;
SET NUMERIC_ROUNDABORT OFF;
GO

IF COL_LENGTH('dbo.configuracionRestaurante','descuentoActivo') IS NULL
    ALTER TABLE dbo.configuracionRestaurante ADD descuentoActivo bit NOT NULL CONSTRAINT DF_Configuracion_descuentoActivo DEFAULT 0,
        porcentajeDescuento decimal(5,2) NOT NULL CONSTRAINT DF_Configuracion_porcentajeDescuento DEFAULT 0,
        fechaDescuento date NULL;
IF COL_LENGTH('dbo.pedido','impuestoAgregado') IS NULL
    ALTER TABLE dbo.pedido ADD impuestoAgregado bit NOT NULL CONSTRAINT DF_Pedido_impuestoAgregado DEFAULT 0,
        tasaImpuestoAplicada decimal(5,2) NOT NULL CONSTRAINT DF_Pedido_tasaAplicada DEFAULT 0,
        porcentajeDescuento decimal(5,2) NOT NULL CONSTRAINT DF_Pedido_porcentajeDescuento DEFAULT 0,
        descuento decimal(10,2) NOT NULL CONSTRAINT DF_Pedido_descuento DEFAULT 0,
        impuestos decimal(10,2) NOT NULL CONSTRAINT DF_Pedido_impuestos DEFAULT 0;
IF COL_LENGTH('dbo.factura','impuestoAgregado') IS NULL
    ALTER TABLE dbo.factura ADD impuestoAgregado bit NOT NULL CONSTRAINT DF_Factura_impuestoAgregado DEFAULT 0;
IF OBJECT_ID('dbo.CK__factura__total__47A6A41B','C') IS NOT NULL
    ALTER TABLE dbo.factura DROP CONSTRAINT CK__factura__total__47A6A41B;
IF OBJECT_ID('dbo.CK__pago__monto__489AC854','C') IS NOT NULL
    ALTER TABLE dbo.pago DROP CONSTRAINT CK__pago__monto__489AC854;
IF OBJECT_ID('dbo.CK_factura_total_local','C') IS NULL
    ALTER TABLE dbo.factura WITH CHECK ADD CONSTRAINT CK_factura_total_local CHECK(total BETWEEN 0 AND 99999999.99);
IF OBJECT_ID('dbo.CK_pago_monto_local','C') IS NULL
    ALTER TABLE dbo.pago WITH CHECK ADD CONSTRAINT CK_pago_monto_local CHECK(monto BETWEEN 0 AND 99999999.99);
IF COL_LENGTH('dbo.configuracionRestaurante','tarifaEnvio') IS NOT NULL
BEGIN
    IF OBJECT_ID('dbo.CK_configuracion_tarifaEnvio','C') IS NOT NULL
        ALTER TABLE dbo.configuracionRestaurante DROP CONSTRAINT CK_configuracion_tarifaEnvio;
    DECLARE @Restriccion sysname;
    SELECT @Restriccion=d.name FROM sys.default_constraints d JOIN sys.columns c
        ON c.object_id=d.parent_object_id AND c.column_id=d.parent_column_id
        WHERE c.object_id=OBJECT_ID('dbo.configuracionRestaurante') AND c.name='tarifaEnvio';
    IF @Restriccion IS NOT NULL EXEC('ALTER TABLE dbo.configuracionRestaurante DROP CONSTRAINT '+@Restriccion);
    ALTER TABLE dbo.configuracionRestaurante DROP COLUMN tarifaEnvio;
END;
GO

CREATE OR ALTER PROCEDURE dbo.sp_Configuracion_Obtener
AS
BEGIN
    SET NOCOUNT ON;
    SELECT nombre AS Nombre,moneda AS Moneda,tasaImpuesto AS TasaImpuesto,
        descuentoActivo AS DescuentoActivo,porcentajeDescuento AS PorcentajeDescuento,fechaDescuento AS FechaDescuento
    FROM dbo.configuracionRestaurante WHERE id=1;
END;
GO
CREATE OR ALTER PROCEDURE dbo.sp_Configuracion_Guardar
    @Actor int,@Nombre nvarchar(120),@Tasa decimal(5,2),@DescuentoActivo bit=0,
    @PorcentajeDescuento decimal(5,2)=0,@FechaDescuento date=NULL
AS
BEGIN
    SET NOCOUNT ON;
    EXEC dbo.sp_Catalogo_ExigirAdministrador @Actor;
    IF NULLIF(LTRIM(RTRIM(@Nombre)),'') IS NULL OR @Tasa IS NULL OR @Tasa NOT BETWEEN 0 AND 100
        THROW 50530,N'Indique el nombre y un impuesto entre 0 y 100 %.',1;
    IF @DescuentoActivo IS NULL OR @PorcentajeDescuento IS NULL OR @PorcentajeDescuento NOT BETWEEN 0 AND 100
        OR (@DescuentoActivo=1 AND (@FechaDescuento IS NULL OR @PorcentajeDescuento=0))
        THROW 50530,N'El descuento debe ser mayor que 0 y hasta 100 %, con una fecha de aplicación.',1;
    UPDATE dbo.configuracionRestaurante SET nombre=LTRIM(RTRIM(@Nombre)),tasaImpuesto=@Tasa,
        descuentoActivo=@DescuentoActivo,porcentajeDescuento=@PorcentajeDescuento,fechaDescuento=@FechaDescuento WHERE id=1;
END;
GO
CREATE OR ALTER PROCEDURE dbo.sp_Pedido_Recalcular @Actor int,@IdPedido int
AS
BEGIN
    SET NOCOUNT ON;
    EXEC dbo.sp_Operacion_ExigirPedidoAbierto @Actor,@IdPedido;
    DECLARE @Subtotal decimal(18,2),@Descuento decimal(18,2),@Impuesto decimal(18,2),@Total decimal(18,2);
    SELECT @Subtotal=SUM(total) FROM dbo.detallePedido WHERE idPedido=@IdPedido;
    IF @Subtotal IS NULL OR @Subtotal<=0 OR @Subtotal>99999999.99
        THROW 50710,N'El subtotal debe ser mayor que 0 y hasta C$ 99,999,999.99.',1;
    SELECT @Descuento=ROUND(@Subtotal*porcentajeDescuento/100,2),
        @Impuesto=ROUND((@Subtotal-ROUND(@Subtotal*porcentajeDescuento/100,2))*tasaImpuestoAplicada/100,2)
    FROM dbo.pedido WHERE idPedido=@IdPedido;
    SET @Total=@Subtotal-@Descuento+@Impuesto;
    IF @Total NOT BETWEEN 0 AND 99999999.99
        THROW 50710,N'El total debe estar entre C$ 0.00 y C$ 99,999,999.99.',1;
    UPDATE dbo.pedido SET subtotal=@Subtotal,descuento=@Descuento,impuestos=@Impuesto,total=@Total,costoEnvio=0
    WHERE idPedido=@IdPedido;
END;
GO

CREATE OR ALTER PROCEDURE dbo.sp_Pedido_Iniciar
    @Actor int, @IdCliente int = NULL, @Solicitud uniqueidentifier, @NumeroLineas int, @EnMesa bit=0
AS
BEGIN
    SET NOCOUNT ON;
    IF @@TRANCOUNT = 0 THROW 50700, 'Inicie una transacción antes de guardar la venta.', 1;
    EXEC dbo.sp_Operacion_ExigirPersonal @Actor;
    IF @Solicitud IS NULL OR @NumeroLineas NOT BETWEEN 1 AND 200
        THROW 50701, 'La venta necesita entre 1 y 200 productos.', 1;

    DECLARE @IdPedido int;
    SELECT @IdPedido = idPedido FROM dbo.pedido WHERE solicitud = @Solicitud;
    IF @IdPedido IS NOT NULL
    BEGIN
        IF NOT EXISTS (
            SELECT 1 FROM dbo.pedido WHERE idPedido = @IdPedido AND idUsuarioRegistro = @Actor
                AND idOrigenPedido = 2 AND idTipoEntrega=CASE WHEN @EnMesa=1 THEN 1 ELSE 2 END AND ISNULL(idCliente, 0) = ISNULL(@IdCliente, 0)
        ) OR (SELECT COUNT(*) FROM dbo.detallePedido WHERE idPedido = @IdPedido) <> @NumeroLineas
            THROW 50703, 'La solicitud ya se utilizó con otros datos.', 1;
        SELECT @IdPedido AS IdPedido, CAST(1 AS bit) AS Reintento;
        RETURN;
    END;

    IF @IdCliente IS NOT NULL AND NOT EXISTS (
        SELECT 1 FROM dbo.cliente WHERE idCliente = @IdCliente AND activo = 1
    )
        THROW 50702, 'Seleccione un cliente activo.', 1;

    INSERT dbo.pedido (idCliente, fechaHora, estado, subtotal, total, esDelivery, costoEnvio,
                       solicitud, idUsuarioRegistro, idOrigenPedido, idTipoEntrega,impuestoAgregado,tasaImpuestoAplicada,porcentajeDescuento)
    SELECT @IdCliente,GETDATE(),'Pendiente',0,0,0,0,@Solicitud,@Actor,2,CASE WHEN @EnMesa=1 THEN 1 ELSE 2 END,1,tasaImpuesto,
        CASE WHEN descuentoActivo=1 AND fechaDescuento=CONVERT(date,GETDATE()) THEN porcentajeDescuento ELSE 0 END
    FROM dbo.configuracionRestaurante WHERE id=1;
    SET @IdPedido = CONVERT(int, SCOPE_IDENTITY());
    INSERT dbo.historialPedido(idPedido, estadoAnterior, estadoNuevo, idUsuario)
    VALUES (@IdPedido, NULL, 'Pendiente', @Actor);
    SELECT @IdPedido AS IdPedido, CAST(0 AS bit) AS Reintento;
END;
GO

CREATE OR ALTER PROCEDURE dbo.sp_Pedido_Finalizar
    @Actor int,
    @IdPedido int,
    @NumeroLineas int,
    @Reintento bit
AS
BEGIN
    SET NOCOUNT ON;
    IF @@TRANCOUNT = 0 THROW 50700, 'Inicie una transaccion antes de guardar el pedido.', 1;
    EXEC dbo.sp_Operacion_ExigirPersonal @Actor;
    IF NOT EXISTS (
        SELECT 1 FROM dbo.pedido WHERE idPedido = @IdPedido
            AND idOrigenPedido = 2 AND idUsuarioRegistro = @Actor
    )
        THROW 50706, 'La venta no corresponde a esta solicitud del personal.', 1;

    IF @Reintento = 1 RETURN;
    IF @NumeroLineas NOT BETWEEN 1 AND 200 OR
        (SELECT COUNT(*) FROM dbo.detallePedido WHERE idPedido = @IdPedido) <> @NumeroLineas
        THROW 50709, 'El detalle del pedido esta incompleto.', 1;

    EXEC dbo.sp_Pedido_Recalcular @Actor,@IdPedido;
END;

GO

CREATE OR ALTER PROCEDURE dbo.sp_Cobro_Cotizar @Actor int,@IdPedido int,@DesdeMostrador bit=0
AS
BEGIN
    SET NOCOUNT ON;
    EXEC dbo.sp_Operacion_ExigirPedidoAbierto @Actor,@IdPedido;
    IF NOT EXISTS(SELECT 1 FROM dbo.pedido WHERE idPedido=@IdPedido AND
        ((estado='Entregado' AND (idTipoEntrega=2 OR idMesa IS NOT NULL)) OR
         (@DesdeMostrador=1 AND idTipoEntrega=2 AND idMesa IS NULL AND estado='Pendiente')))
        THROW 50514,N'Despache la cuenta a una mesa antes de cobrar, o use el cobro de mostrador.',1;
    SELECT p.idPedido AS IdPedido,CASE WHEN p.idCliente IS NULL THEN 'Consumidor final' ELSE CONCAT(c.pNombreCl,' ',c.pApellidoCl) END AS Cliente,
        p.subtotal AS Subtotal,p.descuento AS Descuento,p.impuestos AS Impuestos,p.total AS Total,
        p.tasaImpuestoAplicada AS TasaImpuesto,p.porcentajeDescuento AS PorcentajeDescuento
    FROM dbo.pedido p LEFT JOIN dbo.vw_Cliente c ON c.idCliente=p.idCliente WHERE p.idPedido=@IdPedido;
END;
GO

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
    SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;
    EXEC dbo.sp_Operacion_ExigirPersonal @Actor;
    IF @Metodo NOT IN ('Efectivo', 'Tarjeta', 'Transferencia') OR @Solicitud IS NULL
        THROW 50515, 'Seleccione un metodo de pago valido.', 1;

    BEGIN TRY
        BEGIN TRANSACTION;
        DECLARE @Pago int;
        DECLARE @PedidoAnterior int;
        DECLARE @MetodoAnterior varchar(40);
        DECLARE @RecibidoAnterior decimal(10,2);
        DECLARE @TotalAnterior decimal(10,2);
        SELECT @Pago = pa.idPago, @PedidoAnterior = f.idPedido,
            @MetodoAnterior = pa.metodoDePago, @RecibidoAnterior = pa.recibido, @TotalAnterior = pa.monto
        FROM dbo.pago pa
        INNER JOIN dbo.factura f ON f.idFactura = pa.idFactura
        WHERE pa.solicitud = @Solicitud AND pa.idUsuario = @Actor;
        IF @Pago IS NOT NULL
        BEGIN
            IF EXISTS (SELECT 1 FROM dbo.factura f JOIN dbo.pago pa ON pa.idFactura = f.idFactura
                       WHERE pa.idPago = @Pago AND f.estado = 'Anulada')
                THROW 50516, 'La factura de este cobro ya fue anulada.', 1;
            IF @PedidoAnterior <> @IdPedido OR @MetodoAnterior <> @Metodo
                OR @RecibidoAnterior <> @Recibido OR @TotalAnterior <> @TotalEsperado
                THROW 50516, 'La solicitud ya se utilizo con otros datos.', 1;
            COMMIT TRANSACTION;
            SELECT idFactura AS IdFactura, recibido AS Recibido, cambio AS Cambio
            FROM dbo.pago WHERE idPago = @Pago;
            RETURN;
        END;

        EXEC dbo.sp_Operacion_ExigirPedidoAbierto @Actor, @IdPedido;
        DECLARE @Total decimal(10,2);
        DECLARE @Subtotal decimal(10,2);
        DECLARE @Envio decimal(10,2);
        DECLARE @Descuento decimal(10,2),@Impuesto decimal(10,2),@Tasa decimal(5,2);
        DECLARE @Cliente int;
        DECLARE @Estado varchar(20);
        DECLARE @Mesa int;
        DECLARE @TipoPedido varchar(20);
        SELECT @Total = p.total, @Subtotal = p.subtotal, @Envio = p.costoEnvio,
            @Cliente = p.idCliente, @Estado = p.estado, @Mesa = m.numero,
            @Descuento=p.descuento,@Impuesto=p.impuestos,@Tasa=p.tasaImpuestoAplicada,
            @TipoPedido = CASE WHEN p.esDelivery = 1 THEN 'Entrega' ELSE 'Local' END
        FROM dbo.pedido p LEFT JOIN dbo.mesa m ON m.idMesa = p.idMesa
        WHERE p.idPedido = @IdPedido;
        IF @Estado IS NULL OR @Estado <> 'Entregado'
            THROW 50514, 'El pedido debe estar entregado y sin pagar.', 1;
        IF EXISTS (SELECT 1 FROM dbo.factura WHERE idPedido = @IdPedido)
            THROW 50517, 'El pedido ya tiene factura.', 1;
        DECLARE @SumaDetalle decimal(18,2);
        SELECT @SumaDetalle = SUM(total) FROM dbo.detallePedido WHERE idPedido = @IdPedido;
        IF @SumaDetalle IS NULL OR @Subtotal <> @SumaDetalle OR @Total <> @Subtotal - @Descuento + @Impuesto
            THROW 50518, 'Los importes del pedido no coinciden con su detalle.', 1;
        IF @Total <> @TotalEsperado OR @Total < 0
            THROW 50518, 'El importe cambio. Recargue el pedido.', 1;
        IF @Recibido IS NULL OR @Recibido < @Total OR (@Metodo <> 'Efectivo' AND @Recibido <> @Total)
            THROW 50519, 'Revise el importe recibido.', 1;
        IF @Metodo = 'Efectivo' AND @Recibido - @Total > 1000
            THROW 50519, N'El vuelto máximo en efectivo es C$ 1,000.00.', 1;

        DECLARE @Restaurante nvarchar(120);
        DECLARE @Moneda varchar(5);
        SELECT @Restaurante = nombre, @Moneda = moneda
        FROM dbo.configuracionRestaurante WHERE id = 1;
        DECLARE @Nombre nvarchar(330) = 'Consumidor final';
        DECLARE @Cedula varchar(30);
        DECLARE @Empleado nvarchar(200);
        SELECT @Nombre = CONCAT(pNombreCl, ' ', COALESCE(sNombreCl + ' ', ''), pApellidoCl, ' ', COALESCE(sApellidoCl, '')),
            @Cedula = cedula FROM dbo.vw_Cliente WHERE idCliente = @Cliente;
        SELECT @Empleado = nombreUsuario FROM dbo.usuario WHERE idUsuario = @Actor;

        DECLARE @Origen varchar(30),@Entrega varchar(30),@Registro nvarchar(200),@Atencion nvarchar(200),
            @Direccion varchar(250),@Referencia varchar(150),@Telefono varchar(20);
        SELECT @Origen=o.nombre,@Entrega=t.nombre,@Registro=COALESCE(ur.nombreUsuario,'Sin registrar'),
            @Atencion=COALESCE(ua.nombreUsuario,'Sin registrar'),@Direccion=p.direccion,@Referencia=p.referencia,@Telefono=p.telefonoDeContacto
        FROM dbo.pedido p JOIN dbo.origenPedido o ON o.idOrigenPedido=p.idOrigenPedido
        JOIN dbo.tipoEntrega t ON t.idTipoEntrega=p.idTipoEntrega
        LEFT JOIN dbo.usuario ur ON ur.idUsuario=p.idUsuarioRegistro
        LEFT JOIN dbo.empleado e ON e.idEmpleado=p.idEmpleado LEFT JOIN dbo.usuario ua ON ua.idUsuario=e.idUsuario
        WHERE p.idPedido=@IdPedido;
        IF @Origen = 'En el local' AND @Entrega = 'Recoger'
            SET @Entrega = 'Mostrador';
        SET @TipoPedido=@Entrega;
        DECLARE @Factura int;
        INSERT INTO dbo.factura
            (idPedido, numeroFactura, fechaEmision, estado, subtotal, costoEnvio, descuento,
             impuestos, propina, total, clienteNombre, clienteCedula, empleadoNombre,
             tasaImpuesto, restauranteNombre, moneda, mesaNumero, tipoPedido, origenPedido, tipoEntrega, registradoPor, atendidoPor, direccionEntrega, referenciaEntrega, telefonoContacto,impuestoAgregado)
        VALUES
            (@IdPedido, 'F-' + CONVERT(varchar(20), @IdPedido), GETDATE(), 'Pagada',
             @Subtotal, 0, @Descuento, @Impuesto, 0, @Total, @Nombre, @Cedula,
             @Empleado, @Tasa, @Restaurante, @Moneda, @Mesa, @TipoPedido, @Origen, @Entrega, @Registro, @Atencion, @Direccion, @Referencia, @Telefono,1);
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

        UPDATE m SET estado = 'Disponible'
        FROM dbo.mesa m INNER JOIN dbo.pedido p ON p.idMesa = m.idMesa
        WHERE p.idPedido = @IdPedido AND m.activa = 1 AND NOT EXISTS (
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
    DECLARE @Reabrir bit = 0;
    IF @Anterior = 'Entregado' AND @Nuevo = 'Pendiente'
    BEGIN
        EXEC dbo.sp_Catalogo_ExigirAdministrador @Actor;
        SET @Reabrir = 1;
    END
    ELSE IF @Nuevo IS NULL OR @Anterior IS NULL OR @Nuevo <> 'Entregado'
        OR @Anterior NOT IN ('Pendiente', 'En preparación', 'Listo')
        THROW 50512, 'Solo se pueden despachar ventas pendientes.', 1;
    BEGIN TRY
        BEGIN TRANSACTION;
        EXEC dbo.sp_Operacion_ExigirPedidoAbierto @Actor, @IdPedido;
        IF @Reabrir = 1 AND (NOT EXISTS (
            SELECT 1 FROM dbo.pedido WHERE idPedido = @IdPedido AND estado = 'Entregado'
                AND idOrigenPedido = 2 AND idTipoEntrega IN (1, 2)
        ) OR EXISTS (SELECT 1 FROM dbo.factura WHERE idPedido = @IdPedido))
            THROW 50512, 'Solo puede volver a pendiente una venta local entregada y sin factura.', 1;
        DECLARE @Empleado AS INT = (SELECT idEmpleado
                                    FROM dbo.vw_Empleado
                                    WHERE idUsuario = @Actor
                                          AND activo = 1);
        IF @Nuevo='Entregado' AND EXISTS(SELECT 1 FROM dbo.pedido WHERE idPedido=@IdPedido AND idTipoEntrega=1 AND idMesa IS NULL)
            THROW 50809,'Asigne una mesa antes de entregar el pedido en mesa.',1;
        UPDATE dbo.pedido
        SET estado = @Nuevo,
            idEmpleado = CASE WHEN @Reabrir = 1 THEN idEmpleado ELSE COALESCE(idEmpleado, @Empleado) END
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

CREATE OR ALTER PROCEDURE dbo.sp_Documento_Obtener
    @Actor INT,
    @IdPedido INT,
    @Factura bit, @SolicitudCobro uniqueidentifier = NULL
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
        EXEC dbo.sp_Operacion_ExigirPersonal @Actor;
        IF EXISTS (SELECT 1 FROM dbo.usuario WHERE idUsuario = @Actor AND idRol = 2)
            AND (@Factura = 0 OR @SolicitudCobro IS NULL OR NOT EXISTS (
                SELECT 1 FROM dbo.pago pa JOIN dbo.factura f ON f.idFactura = pa.idFactura
                JOIN dbo.pedido p ON p.idPedido = f.idPedido
                WHERE f.idPedido = @IdPedido AND pa.idUsuario = @Actor AND pa.solicitud = @SolicitudCobro
                    AND f.estado = 'Pagada' AND p.idOrigenPedido = 2 AND p.idTipoEntrega IN (1, 2)
            ))
            THROW 50520, 'El historial está reservado al administrador.', 1;
        IF @Factura = 1
            BEGIN
                IF NOT EXISTS (SELECT 1
                               FROM dbo.factura
                               WHERE idPedido = @IdPedido
                                     AND estado IN ('Pagada', 'Anulada'))
                    THROW 50521, 'El pedido no tiene una factura para consultar.', 1;
                SELECT f.numeroFactura AS Numero,
                       f.fechaEmision AS Fecha,
                       f.clienteNombre AS Cliente,
                       f.clienteCedula AS Cedula,
                       f.empleadoNombre AS Empleado,
                       f.estado AS Estado,
                       a.motivo AS MotivoAnulacion,
                       f.subtotal AS Subtotal, f.impuestoAgregado AS ImpuestoAgregado,
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
                       COALESCE(f.tipoEntrega,f.tipoPedido,'Sin registrar') AS TipoPedido,
                       COALESCE(f.origenPedido,'Sin registrar') AS OrigenPedido,
                       COALESCE(f.registradoPor,'Sin registrar') AS RegistradoPor,
                       COALESCE(f.atendidoPor,'Sin registrar') AS AtendidoPor,
                       COALESCE(f.empleadoNombre,'Sin registrar') AS CobradoPor,
                       f.direccionEntrega AS DireccionEntrega, f.referenciaEntrega AS ReferenciaEntrega, f.telefonoContacto AS TelefonoContacto
                FROM dbo.factura AS f
                     INNER JOIN
                     dbo.pedido AS p
                     ON p.idPedido = f.idPedido
                     LEFT JOIN dbo.anulacionVenta AS a ON a.idFactura = f.idFactura
                     CROSS JOIN dbo.configuracionRestaurante AS cfg
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
                       CASE WHEN p.idCliente IS NULL THEN 'Consumidor final' ELSE CONCAT(c.pNombreCl, ' ', c.pApellidoCl) END AS Cliente,
                       c.cedula AS Cedula,
                       '' AS Empleado,
                       p.estado AS Estado,
                       p.subtotal AS Subtotal, p.impuestoAgregado AS ImpuestoAgregado,
                       p.costoEnvio AS CostoEnvio,
                       p.impuestos AS Impuestos,
                       p.descuento AS Descuento,
                       p.total AS Total,
                       cfg.nombre AS Restaurante,
                       cfg.moneda AS Moneda,
                       p.tasaImpuestoAplicada AS TasaImpuesto,
                       '' AS Metodo,
                       CONVERT (DECIMAL (10, 2), 0) AS Recibido,
                       CONVERT (DECIMAL (10, 2), 0) AS Cambio,
                       (SELECT numero
                        FROM dbo.mesa
                        WHERE idMesa = p.idMesa) AS Mesa,
                       CASE WHEN p.idOrigenPedido = 2 AND p.idTipoEntrega = 2 THEN 'Mostrador' ELSE (SELECT nombre FROM dbo.tipoEntrega WHERE idTipoEntrega=p.idTipoEntrega) END AS TipoPedido,
                       (SELECT nombre FROM dbo.origenPedido WHERE idOrigenPedido=p.idOrigenPedido) AS OrigenPedido,
                       COALESCE((SELECT nombreUsuario FROM dbo.usuario WHERE idUsuario=p.idUsuarioRegistro),'Sin registrar') AS RegistradoPor,
                       COALESCE((SELECT u.nombreUsuario FROM dbo.empleado e JOIN dbo.usuario u ON u.idUsuario=e.idUsuario WHERE e.idEmpleado=p.idEmpleado),'Sin registrar') AS AtendidoPor,
                       'Sin cobrar' AS CobradoPor, p.direccion AS DireccionEntrega, p.referencia AS ReferenciaEntrega, p.telefonoDeContacto AS TelefonoContacto
                FROM dbo.pedido AS p
                     LEFT JOIN dbo.vw_Cliente AS c
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

UPDATE p SET impuestoAgregado=1,tasaImpuestoAplicada=c.tasaImpuesto,porcentajeDescuento=0,
    descuento=0,impuestos=ROUND(p.subtotal*c.tasaImpuesto/100,2),
    total=p.subtotal+ROUND(p.subtotal*c.tasaImpuesto/100,2),costoEnvio=0
FROM dbo.pedido p CROSS JOIN dbo.configuracionRestaurante c
WHERE p.idOrigenPedido=2 AND p.estado NOT IN ('Pagado','Cancelado') AND p.impuestoAgregado=0;
GO
