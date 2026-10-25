USE [RestaurantSM];
GO
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
GO
-- Compatibilidad con la base original: no elimina tablas ni registros.
IF OBJECT_ID('dbo.pedido','U') IS NULL THROW 50600,'Faltan las tablas base. Use INSTALAR_NUEVA.sql.',1;
IF COL_LENGTH('dbo.categoria','activa') IS NULL
    ALTER TABLE dbo.categoria ADD activa bit NOT NULL DEFAULT(1) WITH VALUES;
IF COL_LENGTH('dbo.pedido','subtotal') IS NULL
BEGIN
    ALTER TABLE dbo.pedido ADD subtotal decimal(10,2) NOT NULL DEFAULT(0) WITH VALUES;
    EXEC(N'UPDATE p SET subtotal=COALESCE((SELECT SUM(CONVERT(decimal(18,2),d.precioUnitario)*d.cantidad) FROM dbo.detallePedido d WHERE d.idPedido=p.idPedido),(SELECT TOP(1) f.subtotal FROM dbo.factura f WHERE f.idPedido=p.idPedido),0) FROM dbo.pedido p');
END;
IF COL_LENGTH('dbo.pedido','total') IS NULL
BEGIN
    ALTER TABLE dbo.pedido ADD total decimal(10,2) NOT NULL DEFAULT(0) WITH VALUES;
    EXEC(N'UPDATE p SET total=COALESCE((SELECT TOP(1) f.total FROM dbo.factura f WHERE f.idPedido=p.idPedido),p.subtotal+COALESCE(p.costoEnvio,0)) FROM dbo.pedido p');
END;
GO
-- Aplicar sobre RestaurantSM después de las migraciones 001 a 004.
-- Clientes y mesas: baja lógica. Inventario: unidades de producto terminado.
-- Todo el acceso SQL de la DAL se realiza mediante procedimientos almacenados.

GO
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
SET XACT_ABORT ON;
GO
IF COL_LENGTH('dbo.cliente','activo') IS NULL
    ALTER TABLE dbo.cliente ADD activo bit NOT NULL CONSTRAINT DF_cliente_activo DEFAULT(1) WITH VALUES;
IF COL_LENGTH('dbo.mesa','activa') IS NULL
    ALTER TABLE dbo.mesa ADD activa bit NOT NULL CONSTRAINT DF_mesa_activa DEFAULT(1) WITH VALUES;
-- El cliente puede enviar un pedido antes de que un empleado lo tome.
ALTER TABLE dbo.pedido ALTER COLUMN idEmpleado int NULL;
IF COL_LENGTH('dbo.pedido','solicitud') IS NULL
    ALTER TABLE dbo.pedido ADD solicitud uniqueidentifier NULL;
GO
IF NOT EXISTS(SELECT 1 FROM sys.indexes WHERE name='UQ_pedido_solicitud' AND object_id=OBJECT_ID('dbo.pedido'))
    CREATE UNIQUE INDEX UQ_pedido_solicitud ON dbo.pedido(solicitud) WHERE solicitud IS NOT NULL;
IF COL_LENGTH('dbo.detallePedido','controlInventario') IS NULL
    ALTER TABLE dbo.detallePedido ADD controlInventario bit NOT NULL CONSTRAINT DF_detalle_control DEFAULT(0) WITH VALUES;
IF OBJECT_ID('dbo.inventarioProducto','U') IS NULL
    CREATE TABLE dbo.inventarioProducto(
      idProducto int NOT NULL PRIMARY KEY REFERENCES dbo.producto(idProducto),
      stockActual int NOT NULL CHECK(stockActual>=0),
      stockMinimo int NOT NULL CHECK(stockMinimo>=0),
      activo bit NOT NULL DEFAULT(1), revision int NOT NULL DEFAULT(1));
GO

GO
IF OBJECT_ID('dbo.pedido','U') IS NULL OR OBJECT_ID('dbo.inventarioProducto','U') IS NULL
 THROW 50500,'Falta el esquema base o la actualizacion 005. Use el instalador completo en una base nueva.',1;
IF COL_LENGTH('dbo.pedido','solicitud') IS NULL ALTER TABLE dbo.pedido ADD solicitud uniqueidentifier NULL;
IF COL_LENGTH('dbo.pedido','subtotal') IS NULL ALTER TABLE dbo.pedido ADD subtotal decimal(10,2) NOT NULL DEFAULT(0) WITH VALUES;
IF COL_LENGTH('dbo.pedido','total') IS NULL ALTER TABLE dbo.pedido ADD total decimal(10,2) NOT NULL DEFAULT(0) WITH VALUES;
IF OBJECT_ID('dbo.versionEsquema','U') IS NULL CREATE TABLE dbo.versionEsquema(version int PRIMARY KEY,descripcion nvarchar(200) NOT NULL,fecha datetime2 NOT NULL DEFAULT SYSDATETIME());
IF OBJECT_ID('dbo.configuracionRestaurante','U') IS NULL
 CREATE TABLE dbo.configuracionRestaurante(id int NOT NULL PRIMARY KEY CHECK(id=1),nombre nvarchar(120) NOT NULL,moneda varchar(5) NOT NULL,tasaImpuesto decimal(5,2) NOT NULL CHECK(tasaImpuesto BETWEEN 0 AND 100));
IF NOT EXISTS(SELECT 1 FROM dbo.configuracionRestaurante) INSERT dbo.configuracionRestaurante VALUES(1,N'RestaurantSM','C$',0);
IF OBJECT_ID('dbo.historialPedido','U') IS NULL CREATE TABLE dbo.historialPedido(id bigint IDENTITY PRIMARY KEY,idPedido int NOT NULL REFERENCES dbo.pedido(idPedido),estadoAnterior varchar(20) NULL,estadoNuevo varchar(20) NOT NULL,idUsuario int NOT NULL REFERENCES dbo.usuario(idUsuario),fecha datetime2 NOT NULL DEFAULT SYSDATETIME());
IF OBJECT_ID('dbo.movimientoInventario','U') IS NULL CREATE TABLE dbo.movimientoInventario(id bigint IDENTITY PRIMARY KEY,idProducto int NOT NULL,producto nvarchar(100) NOT NULL,anterior int NOT NULL,nuevo int NOT NULL,diferencia int NOT NULL,idUsuario int NULL,usuario nvarchar(50) NOT NULL,motivo nvarchar(200) NOT NULL,fecha datetime2 NOT NULL DEFAULT SYSDATETIME());
IF OBJECT_ID('dbo.facturaDetalle','U') IS NULL CREATE TABLE dbo.facturaDetalle(id bigint IDENTITY PRIMARY KEY,idFactura int NOT NULL REFERENCES dbo.factura(idFactura),idProducto int NOT NULL,producto nvarchar(100) NOT NULL,cantidad int NOT NULL,precio decimal(10,2) NOT NULL,subtotal decimal(10,2) NOT NULL);
IF COL_LENGTH('dbo.factura','clienteNombre') IS NULL ALTER TABLE dbo.factura ADD clienteNombre nvarchar(330) NULL,clienteCedula varchar(30) NULL,empleadoNombre nvarchar(200) NULL,tasaImpuesto decimal(5,2) NULL;
IF COL_LENGTH('dbo.pago','recibido') IS NULL ALTER TABLE dbo.pago ADD recibido decimal(10,2) NULL,cambio decimal(10,2) NULL,solicitud uniqueidentifier NULL,idUsuario int NULL,notas nvarchar(200) NULL;
GO
IF NOT EXISTS(SELECT 1 FROM sys.indexes WHERE object_id=OBJECT_ID('dbo.pago') AND name='UX_pago_solicitud_operacion') CREATE UNIQUE INDEX UX_pago_solicitud_operacion ON dbo.pago(solicitud) WHERE solicitud IS NOT NULL;
GO
IF OBJECT_ID('dbo.CK_pedido_estado','C') IS NOT NULL ALTER TABLE dbo.pedido DROP CONSTRAINT CK_pedido_estado;
ALTER TABLE dbo.pedido WITH CHECK ADD CONSTRAINT CK_pedido_estado CHECK(estado IN('Pendiente','En preparación','Listo','Entregado','Pagado','Cancelado'));
IF OBJECT_ID('dbo.CK_estadoReservacion','C') IS NOT NULL ALTER TABLE dbo.reservacion DROP CONSTRAINT CK_estadoReservacion;
ALTER TABLE dbo.reservacion WITH CHECK ADD CONSTRAINT CK_estadoReservacion CHECK(estado IN('Pendiente','Confirmada','Completada','Cancelada','Atendida','No asistio'));
IF COL_LENGTH('dbo.factura','restauranteNombre') IS NULL ALTER TABLE dbo.factura ADD restauranteNombre nvarchar(120) NULL,moneda varchar(5) NULL;
IF COL_LENGTH('dbo.factura','mesaNumero') IS NULL ALTER TABLE dbo.factura ADD mesaNumero int NULL,tipoPedido varchar(20) NULL;
GO

-- El contador entero sustituye al tipo rowversion de versiones anteriores.
-- Si la columna antigua existe, se conserva para no destruir datos al actualizar.
IF COL_LENGTH('dbo.inventarioProducto','revision') IS NULL
    ALTER TABLE dbo.inventarioProducto ADD revision int NOT NULL DEFAULT(1) WITH VALUES;
IF OBJECT_ID('dbo.tr_Inventario_Movimientos','TR') IS NOT NULL
    DROP TRIGGER dbo.tr_Inventario_Movimientos;
GO
-- Procedimientos antiguos reemplazados por cabecera, linea y total.
IF OBJECT_ID('dbo.sp_Pedido_GuardarComun','P') IS NOT NULL DROP PROCEDURE dbo.sp_Pedido_GuardarComun;
IF OBJECT_ID('dbo.sp_Cliente_Pedido_Guardar','P') IS NOT NULL DROP PROCEDURE dbo.sp_Cliente_Pedido_Guardar;
IF OBJECT_ID('dbo.sp_Operacion_Pedido_Guardar','P') IS NOT NULL DROP PROCEDURE dbo.sp_Operacion_Pedido_Guardar;
GO
IF EXISTS(SELECT 1 FROM dbo.rol WHERE (idRol=1 AND nombre<>'Administrador') OR (idRol=2 AND nombre<>'Empleado') OR (idRol=3 AND nombre<>'Cliente'))
    THROW 50601,'Los roles 1, 2 y 3 deben ser Administrador, Empleado y Cliente.',1;
SET IDENTITY_INSERT dbo.rol ON;
IF NOT EXISTS(SELECT 1 FROM dbo.rol WHERE idRol=1) INSERT dbo.rol(idRol,nombre) VALUES(1,'Administrador');
IF NOT EXISTS(SELECT 1 FROM dbo.rol WHERE idRol=2) INSERT dbo.rol(idRol,nombre) VALUES(2,'Empleado');
IF NOT EXISTS(SELECT 1 FROM dbo.rol WHERE idRol=3) INSERT dbo.rol(idRol,nombre) VALUES(3,'Cliente');
SET IDENTITY_INSERT dbo.rol OFF;
GO
