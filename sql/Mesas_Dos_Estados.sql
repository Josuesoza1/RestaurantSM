USE [RestaurantSM];
GO
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
SET NOCOUNT ON;
SET XACT_ABORT ON;

BEGIN TRY
    BEGIN TRANSACTION;

    IF DB_NAME() <> N'RestaurantSM'
       OR OBJECT_ID(N'dbo.mesa', N'U') IS NULL
       OR OBJECT_ID(N'dbo.sp_Operacion_ExigirPedidoAbierto', N'P') IS NULL
       OR COL_LENGTH(N'dbo.pago', N'solicitud') IS NULL
        THROW 51000, N'Ejecute este cambio en la base local actual de RestaurantSM.', 1;

    IF OBJECT_ID(N'dbo.CK_Estado', N'C') IS NOT NULL
        ALTER TABLE dbo.mesa DROP CONSTRAINT CK_Estado;

    UPDATE m
    SET estado = CASE
        WHEN m.activa = 0 OR EXISTS (SELECT 1 FROM dbo.pedido p
            WHERE p.idMesa = m.idMesa AND p.estado NOT IN ('Pagado', 'Cancelado')) THEN 'No disponible'
        WHEN m.estado IN ('Disponible', 'En limpieza') THEN 'Disponible'
        ELSE 'No disponible'
    END
    FROM dbo.mesa m;

    ALTER TABLE dbo.mesa WITH CHECK ADD CONSTRAINT CK_Estado
        CHECK (estado IN ('Disponible', 'No disponible') AND (activa = 1 OR estado = 'No disponible'));

    DROP PROCEDURE IF EXISTS dbo.sp_Operacion_CambiarMesa;

    EXEC(N'CREATE OR ALTER PROCEDURE dbo.sp_Mesa_Guardar
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
    IF @Numero IS NULL OR @Numero NOT BETWEEN 1 AND 999
       OR @Capacidad NOT BETWEEN 1 AND 20
       OR @Estado IS NULL OR @Activa IS NULL
       OR @Estado NOT IN (''Disponible'', ''No disponible'')
        THROW 50209, ''Número, capacidad o estado de mesa no válido.'', 1;
    BEGIN TRY
        BEGIN TRANSACTION;
        IF @Id <> 0
           AND (@Activa = 0
                OR @Estado = ''Disponible'')
           AND EXISTS (SELECT 1
                       FROM dbo.pedido
                       WHERE idMesa = @Id
                             AND estado NOT IN (''Cancelado'', ''Pagado''))
            THROW 50210, ''La mesa tiene ventas abiertas. Cobre o cancele las ventas antes de habilitar la mesa.'', 1;
        IF @Id <> 0
           AND EXISTS (SELECT 1
                       FROM dbo.reservacion
                       WHERE idMesa = @Id
                             AND estado IN (''Pendiente'', ''Confirmada'')
                             AND fechaHoraReserva >= GETDATE()
                             AND (@Activa = 0
                                  OR cantidadPersonas > @Capacidad))
            THROW 50211, ''La mesa tiene reservas incompatibles con este cambio.'', 1;
        IF @Id = 0
            BEGIN
                INSERT dbo.mesa (numero, capacidad, estado, activa)
                VALUES (@Numero, @Capacidad, CASE WHEN @Activa = 0 THEN ''No disponible'' ELSE @Estado END, @Activa);
                SET @Id = CONVERT (INT, SCOPE_IDENTITY());
            END
        ELSE
            BEGIN
                UPDATE dbo.mesa
                SET numero = @Numero,
                    capacidad = @Capacidad,
                    estado = CASE WHEN @Activa = 0 THEN ''No disponible'' ELSE @Estado END,
                    activa = @Activa
                WHERE idMesa = @Id;
                IF @@ROWCOUNT = 0
                    THROW 50212, ''La mesa ya no existe.'', 1;
            END
        COMMIT TRANSACTION;
        SELECT @Id;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK;
        THROW;
    END CATCH
END');

    EXEC(N'CREATE OR ALTER PROCEDURE dbo.sp_Mesa_Eliminar
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
            THROW 50413, ''La mesa ya no existe. Recargue el listado.'', 1;
        IF EXISTS (SELECT 1
                   FROM dbo.pedido
                   WHERE idMesa = @Id)
           OR EXISTS (SELECT 1
                      FROM dbo.reservacion
                      WHERE idMesa = @Id)
            THROW 50415, ''La mesa tiene historial de ventas o reservas. Puede marcarla como No disponible.'', 1;
        DELETE dbo.mesa
        WHERE idMesa = @Id;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK;
        THROW;
    END CATCH
END');

    EXEC(N'CREATE OR ALTER PROCEDURE dbo.sp_Operacion_EstadoReserva
    @Actor INT,
    @IdReserva INT,
    @Anterior VARCHAR (20),
    @Nuevo VARCHAR (20)
AS
BEGIN
    SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    EXEC dbo.sp_Catalogo_ExigirAdministrador @Actor;
    IF @Nuevo NOT IN (''Atendida'', ''No asistio'', ''Cancelada'')
        THROW 50523, ''Seleccione una accion de reserva valida.'', 1;
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
           OR @Estado NOT IN (''Pendiente'', ''Confirmada'')
            THROW 50524, ''La reserva cambio o ya fue cerrada.'', 1;
        IF @Nuevo = ''No asistio''
           AND GETDATE() < DATEADD(minute, 30, @Fecha)
            THROW 50525, ''Espere 30 minutos despues de la reserva para marcar ausencia.'', 1;
        IF @Nuevo = ''Atendida''
           AND GETDATE() < DATEADD(minute, -30, @Fecha)
            THROW 50525, ''La reserva aun no esta dentro del horario de llegada.'', 1;
        IF @Nuevo = ''Atendida''
           AND EXISTS (SELECT 1
                       FROM dbo.mesa
                       WHERE idMesa = @Mesa
                             AND (activa = 0
                                  OR estado <> ''Disponible''))
            THROW 50526, ''La mesa no esta disponible para recibir al cliente.'', 1;
        UPDATE dbo.reservacion
        SET estado = @Nuevo
        WHERE idReservacion = @IdReserva;
        IF @Nuevo = ''Atendida''
            UPDATE dbo.mesa
            SET estado = ''No disponible''
            WHERE idMesa = @Mesa;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK;
        THROW;
    END CATCH
END');

    EXEC(N'CREATE OR ALTER PROCEDURE dbo.sp_Operacion_AsignarMesa
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
        EXEC dbo.sp_Operacion_ExigirPedidoAbierto @Actor, @IdPedido;
        IF EXISTS(SELECT 1 FROM dbo.pedido WHERE idPedido=@IdPedido AND idTipoEntrega = 3)
            THROW 50810,''No se asigna mesa a un pedido histórico a domicilio.'',1;
        DECLARE @Cliente int, @Anterior int, @Existe int;
        SELECT @Existe = idPedido, @Cliente = idCliente,
               @Anterior = idMesa
        FROM dbo.pedido
        WHERE idPedido = @IdPedido
              AND estado IN (''Pendiente'', ''En preparación'', ''Listo'');
        IF @Existe IS NULL
            THROW 50531, ''Solo se asigna mesa antes de entregar el pedido.'', 1;
        IF NOT EXISTS (SELECT 1
                       FROM dbo.mesa
                       WHERE idMesa = @IdMesa
                             AND activa = 1
                             AND estado IN (''Disponible'', ''No disponible''))
            THROW 50532, ''La mesa no está disponible. Seleccione otra mesa.'', 1;
        IF EXISTS (SELECT 1
                   FROM dbo.mesa
                   WHERE idMesa = @IdMesa
                         AND estado = ''No disponible'')
           AND NOT EXISTS (SELECT 1
                           FROM dbo.pedido
                           WHERE idMesa = @IdMesa
                                 AND (idPedido = @IdPedido OR idCliente = @Cliente)
                                 AND estado NOT IN (''Pagado'', ''Cancelado''))
            THROW 50533, ''La mesa no está disponible. Seleccione otra mesa.'', 1;
        IF EXISTS (SELECT 1
                   FROM dbo.pedido
                   WHERE idMesa = @IdMesa
                         AND idPedido <> @IdPedido
                         AND (@Cliente IS NULL OR ISNULL(idCliente, 0) <> @Cliente)
                         AND estado NOT IN (''Pagado'', ''Cancelado''))
            THROW 50533, ''La mesa tiene pedidos abiertos de otro cliente.'', 1;
        IF EXISTS (SELECT 1
                   FROM dbo.reservacion
                   WHERE idMesa = @IdMesa
                         AND (@Cliente IS NULL OR ISNULL(idCliente, 0) <> @Cliente)
                         AND estado IN (''Pendiente'', ''Confirmada'', ''Atendida'')
                         AND ABS(DATEDIFF(minute, GETDATE(), fechaHoraReserva)) < 120)
            THROW 50533, ''La mesa tiene una reserva de otro cliente en este horario.'', 1;
        UPDATE dbo.pedido
        SET idMesa = @IdMesa, idTipoEntrega=1,
            idEmpleado=COALESCE(idEmpleado,(SELECT idEmpleado FROM dbo.vw_Empleado WHERE idUsuario=@Actor AND activo=1))
        WHERE idPedido = @IdPedido;
        UPDATE dbo.mesa
        SET estado = ''No disponible''
        WHERE idMesa = @IdMesa;
        IF @Anterior IS NOT NULL
           AND @Anterior <> @IdMesa
           AND NOT EXISTS (SELECT 1
                           FROM dbo.pedido
                           WHERE idMesa = @Anterior
                                 AND estado NOT IN (''Pagado'', ''Cancelado''))
            UPDATE dbo.mesa
            SET estado = ''Disponible''
            WHERE idMesa = @Anterior AND activa = 1;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK;
        THROW;
    END CATCH
END');

    EXEC(N'CREATE OR ALTER PROCEDURE dbo.sp_Pedido_CancelarComun @Actor int,
    @IdPedido int
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;
    BEGIN TRY
        BEGIN TRANSACTION;
        EXEC dbo.sp_Operacion_ExigirPedidoAbierto @Actor, @IdPedido;
        UPDATE dbo.pedido SET estado = ''Cancelado''
        WHERE idPedido = @IdPedido AND estado = ''Pendiente'';
        IF @@ROWCOUNT = 0 THROW 50711, ''Solo puede cancelar un pedido pendiente permitido.'', 1;
        IF EXISTS (SELECT 1 FROM dbo.factura WHERE idPedido = @IdPedido)
            THROW 50704, ''El pedido ya tiene factura.'', 1;

        INSERT INTO dbo.movimientoInventario
            (idProducto, producto, anterior, nuevo, diferencia, idUsuario, usuario, motivo)
        SELECT i.idProducto, p.nombre, i.stockActual, i.stockActual + d.cantidad, d.cantidad,
            @Actor, u.nombreUsuario, ''Cancelacion de pedido''
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
        VALUES (@IdPedido, ''Pendiente'', ''Cancelado'', @Actor);
        UPDATE m SET estado = ''Disponible''
        FROM dbo.mesa m JOIN dbo.pedido p ON p.idMesa = m.idMesa
        WHERE p.idPedido = @IdPedido AND m.activa = 1 AND NOT EXISTS (
            SELECT 1 FROM dbo.pedido otro WHERE otro.idMesa = m.idMesa
                AND otro.estado NOT IN (''Pagado'', ''Cancelado'')
        );
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;');

    EXEC(N'CREATE OR ALTER PROCEDURE dbo.sp_Cobro_Registrar
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
    IF @Metodo NOT IN (''Efectivo'', ''Tarjeta'', ''Transferencia'') OR @Solicitud IS NULL
        THROW 50515, ''Seleccione un metodo de pago valido.'', 1;

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
                       WHERE pa.idPago = @Pago AND f.estado = ''Anulada'')
                THROW 50516, ''La factura de este cobro ya fue anulada.'', 1;
            IF @PedidoAnterior <> @IdPedido OR @MetodoAnterior <> @Metodo
                OR @RecibidoAnterior <> @Recibido OR @TotalAnterior <> @TotalEsperado
                THROW 50516, ''La solicitud ya se utilizo con otros datos.'', 1;
            COMMIT TRANSACTION;
            SELECT idFactura AS IdFactura, recibido AS Recibido, cambio AS Cambio
            FROM dbo.pago WHERE idPago = @Pago;
            RETURN;
        END;

        EXEC dbo.sp_Operacion_ExigirPedidoAbierto @Actor, @IdPedido;
        DECLARE @Total decimal(10,2);
        DECLARE @Subtotal decimal(10,2);
        DECLARE @Envio decimal(10,2);
        DECLARE @Cliente int;
        DECLARE @Estado varchar(20);
        DECLARE @Mesa int;
        DECLARE @TipoPedido varchar(20);
        SELECT @Total = p.total, @Subtotal = p.subtotal, @Envio = p.costoEnvio,
            @Cliente = p.idCliente, @Estado = p.estado, @Mesa = m.numero,
            @TipoPedido = CASE WHEN p.esDelivery = 1 THEN ''Entrega'' ELSE ''Local'' END
        FROM dbo.pedido p LEFT JOIN dbo.mesa m ON m.idMesa = p.idMesa
        WHERE p.idPedido = @IdPedido;
        IF @Estado IS NULL OR @Estado <> ''Entregado''
            THROW 50514, ''El pedido debe estar entregado y sin pagar.'', 1;
        IF EXISTS (SELECT 1 FROM dbo.factura WHERE idPedido = @IdPedido)
            THROW 50517, ''El pedido ya tiene factura.'', 1;
        DECLARE @SumaDetalle decimal(18,2);
        SELECT @SumaDetalle = SUM(total) FROM dbo.detallePedido WHERE idPedido = @IdPedido;
        IF @SumaDetalle IS NULL OR @Subtotal <> @SumaDetalle OR @Total <> @Subtotal + @Envio
            THROW 50518, ''Los importes del pedido no coinciden con su detalle.'', 1;
        IF @Total <> @TotalEsperado OR @Total <= 0
            THROW 50518, ''El importe cambio. Recargue el pedido.'', 1;
        IF @Recibido IS NULL OR @Recibido < @Total OR (@Metodo <> ''Efectivo'' AND @Recibido <> @Total)
            THROW 50519, ''Revise el importe recibido.'', 1;
        IF @Metodo = ''Efectivo'' AND @Recibido - @Total > 1000
            THROW 50519, N''El vuelto máximo en efectivo es C$ 1,000.00.'', 1;

        DECLARE @Tasa decimal(5,2);
        DECLARE @Impuesto decimal(10,2);
        DECLARE @Restaurante nvarchar(120);
        DECLARE @Moneda varchar(5);
        SELECT @Tasa = tasaImpuesto, @Restaurante = nombre, @Moneda = moneda
        FROM dbo.configuracionRestaurante WHERE id = 1;
        SET @Impuesto = ROUND(@Subtotal - @Subtotal / (1 + @Tasa / 100), 2);
        DECLARE @Nombre nvarchar(330) = ''Consumidor final'';
        DECLARE @Cedula varchar(30);
        DECLARE @Empleado nvarchar(200);
        SELECT @Nombre = CONCAT(pNombreCl, '' '', COALESCE(sNombreCl + '' '', ''''), pApellidoCl, '' '', COALESCE(sApellidoCl, '''')),
            @Cedula = cedula FROM dbo.vw_Cliente WHERE idCliente = @Cliente;
        SELECT @Empleado = nombreUsuario FROM dbo.usuario WHERE idUsuario = @Actor;

        DECLARE @Origen varchar(30),@Entrega varchar(30),@Registro nvarchar(200),@Atencion nvarchar(200),
            @Direccion varchar(250),@Referencia varchar(150),@Telefono varchar(20);
        SELECT @Origen=o.nombre,@Entrega=t.nombre,@Registro=COALESCE(ur.nombreUsuario,''Sin registrar''),
            @Atencion=COALESCE(ua.nombreUsuario,''Sin registrar''),@Direccion=p.direccion,@Referencia=p.referencia,@Telefono=p.telefonoDeContacto
        FROM dbo.pedido p JOIN dbo.origenPedido o ON o.idOrigenPedido=p.idOrigenPedido
        JOIN dbo.tipoEntrega t ON t.idTipoEntrega=p.idTipoEntrega
        LEFT JOIN dbo.usuario ur ON ur.idUsuario=p.idUsuarioRegistro
        LEFT JOIN dbo.empleado e ON e.idEmpleado=p.idEmpleado LEFT JOIN dbo.usuario ua ON ua.idUsuario=e.idUsuario
        WHERE p.idPedido=@IdPedido;
        IF @Origen = ''En el local'' AND @Entrega = ''Recoger''
            SET @Entrega = ''Mostrador'';
        SET @TipoPedido=@Entrega;
        DECLARE @Factura int;
        INSERT INTO dbo.factura
            (idPedido, numeroFactura, fechaEmision, estado, subtotal, costoEnvio, descuento,
             impuestos, propina, total, clienteNombre, clienteCedula, empleadoNombre,
             tasaImpuesto, restauranteNombre, moneda, mesaNumero, tipoPedido, origenPedido, tipoEntrega, registradoPor, atendidoPor, direccionEntrega, referenciaEntrega, telefonoContacto)
        VALUES
            (@IdPedido, ''F-'' + CONVERT(varchar(20), @IdPedido), GETDATE(), ''Pagada'',
             @Subtotal - @Impuesto, @Envio, 0, @Impuesto, 0, @Total, @Nombre, @Cedula,
             @Empleado, @Tasa, @Restaurante, @Moneda, @Mesa, @TipoPedido, @Origen, @Entrega, @Registro, @Atencion, @Direccion, @Referencia, @Telefono);
        SET @Factura = CONVERT(int, SCOPE_IDENTITY());
        INSERT INTO dbo.facturaDetalle(idFactura, idProducto, producto, cantidad, precio, subtotal)
        SELECT @Factura, d.idProducto, p.nombre, d.cantidad, d.precioUnitario, d.total
        FROM dbo.detallePedido d INNER JOIN dbo.producto p ON p.idProducto = d.idProducto
        WHERE d.idPedido = @IdPedido;
        INSERT INTO dbo.pago(idFactura, metodoDePago, monto, fechaPago, recibido, cambio, solicitud, idUsuario, notas)
        VALUES (@Factura, @Metodo, @Total, GETDATE(), @Recibido, @Recibido - @Total, @Solicitud, @Actor, @Notas);
        UPDATE dbo.pedido SET estado = ''Pagado'' WHERE idPedido = @IdPedido;
        INSERT INTO dbo.historialPedido(idPedido, estadoAnterior, estadoNuevo, idUsuario)
        VALUES (@IdPedido, ''Entregado'', ''Pagado'', @Actor);

        UPDATE m SET estado = ''Disponible''
        FROM dbo.mesa m INNER JOIN dbo.pedido p ON p.idMesa = m.idMesa
        WHERE p.idPedido = @IdPedido AND m.activa = 1 AND NOT EXISTS (
            SELECT 1 FROM dbo.pedido otro WHERE otro.idMesa = m.idMesa
            AND otro.estado NOT IN (''Pagado'', ''Cancelado''));
        COMMIT TRANSACTION;
        SELECT @Factura AS IdFactura, @Recibido AS Recibido, @Recibido - @Total AS Cambio;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;');

    COMMIT TRANSACTION;
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
    THROW;
END CATCH;
GO