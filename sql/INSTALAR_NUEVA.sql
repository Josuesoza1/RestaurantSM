-- BASE NUEVA. No reemplaza bases que ya contienen tablas.
IF DB_ID(N'RestaurantSM') IS NULL CREATE DATABASE [RestaurantSM];
GO
USE [RestaurantSM];
GO
IF EXISTS(SELECT 1 FROM sys.tables)
BEGIN
    PRINT N'La base ya tiene tablas. No se ejecutara el instalador. Use ACTUALIZAR_TODO_RESTAURANTSM.sql.';
    SET NOEXEC ON;
END;
GO
-- Solo para una base vacia; conserva las tablas previstas para otras etapas.
USE [RestaurantSM];
GO
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
GO
/****** Objeto: Table [dbo].[categoria] Fecha de script: 17/10/2026 11:42:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[categoria](
	[idCategoria] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](60) NOT NULL,
	[descripcion] [varchar](200) NULL,
 CONSTRAINT [PK_IdCategoria] PRIMARY KEY CLUSTERED 
(
	[idCategoria] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[nombre] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Objeto: Table [dbo].[cliente] Fecha de script: 17/10/2026 11:42:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[cliente](
	[idCliente] [int] IDENTITY(1,1) NOT NULL,
	[idUsuario] [int] NULL,
	[PNombreCl] [varchar](80) NOT NULL,
	[SNombreCl] [varchar](80) NULL,
	[PApellidoCl] [varchar](80) NOT NULL,
	[SApellidoCl] [varchar](80) NULL,
	[genero] [char](1) NULL,
	[cedula] [varchar](30) NULL,
	[telefono] [varchar](20) NULL,
	[email] [varchar](120) NULL,
 CONSTRAINT [PK_cliente] PRIMARY KEY CLUSTERED 
(
	[idCliente] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Objeto: Table [dbo].[detallePedido] Fecha de script: 17/10/2026 11:42:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[detallePedido](
	[idDetalle] [int] IDENTITY(1,1) NOT NULL,
	[idPedido] [int] NOT NULL,
	[idProducto] [int] NOT NULL,
	[cantidad] [int] NOT NULL,
	[notasCocina] [varchar](180) NULL,
	[precioUnitario] [decimal](10, 2) NOT NULL,
	[subtotal] [decimal](10, 2) NOT NULL,
	[total] [decimal](10, 2) NOT NULL,
 CONSTRAINT [PK_idDetalle] PRIMARY KEY CLUSTERED 
(
	[idDetalle] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[idPedido] ASC,
	[idProducto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Objeto: Table [dbo].[empleado] Fecha de script: 17/10/2026 11:42:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[empleado](
	[idEmpleado] [int] IDENTITY(1,1) NOT NULL,
	[idUsuario] [int] NULL,
	[pNombreEmp] [varchar](80) NOT NULL,
	[sNombreEmp] [varchar](80) NULL,
	[pApellidoEmp] [varchar](80) NOT NULL,
	[sApellidoEmp] [varchar](80) NULL,
	[cargo] [varchar](50) NOT NULL,
	[genero] [char](1) NOT NULL,
	[cedula] [varchar](30) NOT NULL,
	[telefono] [varchar](20) NOT NULL,
	[activo] [bit] NOT NULL,
 CONSTRAINT [PK_empleado] PRIMARY KEY CLUSTERED 
(
	[idEmpleado] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UQ_empleado_cedula] UNIQUE NONCLUSTERED 
(
	[cedula] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Objeto: Table [dbo].[factura] Fecha de script: 17/10/2026 11:42:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[factura](
	[idFactura] [int] IDENTITY(1,1) NOT NULL,
	[idPedido] [int] NOT NULL,
	[numeroFactura] [varchar](30) NOT NULL,
	[fechaEmision] [datetime] NOT NULL,
	[estado] [varchar](20) NOT NULL,
	[subtotal] [decimal](10, 2) NOT NULL,
	[costoEnvio] [decimal](10, 2) NOT NULL,
	[descuento] [decimal](10, 2) NOT NULL,
	[impuestos] [decimal](10, 2) NOT NULL,
	[propina] [decimal](10, 2) NOT NULL,
	[total] [decimal](10, 2) NOT NULL,
 CONSTRAINT [PK_idFactura] PRIMARY KEY CLUSTERED 
(
	[idFactura] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[numeroFactura] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[idPedido] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Objeto: Table [dbo].[mesa] Fecha de script: 17/10/2026 11:42:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[mesa](
	[idMesa] [int] IDENTITY(1,1) NOT NULL,
	[numero] [int] NOT NULL,
	[capacidad] [int] NOT NULL,
	[estado] [varchar](20) NOT NULL,
 CONSTRAINT [PK_IdMesa] PRIMARY KEY CLUSTERED 
(
	[idMesa] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[numero] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Objeto: Table [dbo].[pago] Fecha de script: 17/10/2026 11:42:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[pago](
	[idPago] [int] IDENTITY(1,1) NOT NULL,
	[idFactura] [int] NOT NULL,
	[metodoDePago] [varchar](40) NOT NULL,
	[monto] [decimal](10, 2) NOT NULL,
	[fechaPago] [datetime] NOT NULL,
 CONSTRAINT [PK_idPago] PRIMARY KEY CLUSTERED 
(
	[idPago] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Objeto: Table [dbo].[pedido] Fecha de script: 17/10/2026 11:42:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[pedido](
	[idPedido] [int] IDENTITY(1,1) NOT NULL,
	[idCliente] [int] NOT NULL,
	[idMesa] [int] NULL,
	[idEmpleado] [int] NOT NULL,
	[idRepartidor] [int] NULL,
	[direccion] [varchar](250) NULL,
	[referencia] [varchar](150) NULL,
	[telefonoDeContacto] [varchar](20) NULL,
	[fechaHora] [datetime] NOT NULL,
	[estado] [varchar](20) NOT NULL,
	[costoEnvio] [decimal](10, 2) NOT NULL,
	[subtotal] [decimal](10, 2) NOT NULL,
	[total] [decimal](10, 2) NOT NULL,
	[esDelivery] [bit] NOT NULL,
 CONSTRAINT [PK_pedido] PRIMARY KEY CLUSTERED 
(
	[idPedido] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Objeto: Table [dbo].[producto] Fecha de script: 17/10/2026 11:42:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[producto](
	[idProducto] [int] IDENTITY(1,1) NOT NULL,
	[idCategoria] [int] NOT NULL,
	[nombre] [varchar](100) NOT NULL,
	[precio] [decimal](10, 2) NOT NULL,
	[descripcion] [varchar](250) NULL,
	[codigo] [varchar](20) NOT NULL,
	[disponible] [bit] NOT NULL,
	[idSubcategoria] [int] NULL,
 CONSTRAINT [PK_idProducto] PRIMARY KEY CLUSTERED 
(
	[idProducto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[nombre] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Objeto: Table [dbo].[reservacion] Fecha de script: 17/10/2026 11:42:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[reservacion](
	[idReservacion] [int] IDENTITY(1,1) NOT NULL,
	[idCliente] [int] NOT NULL,
	[idMesa] [int] NOT NULL,
	[fechaHoraReserva] [datetime] NOT NULL,
	[cantidadPersonas] [int] NOT NULL,
	[estado] [varchar](20) NOT NULL,
 CONSTRAINT [PK_idReservacion] PRIMARY KEY CLUSTERED 
(
	[idReservacion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Objeto: Table [dbo].[rol] Fecha de script: 17/10/2026 11:42:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[rol](
	[idRol] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](50) NOT NULL,
	[descripcion] [varchar](150) NULL,
 CONSTRAINT [PK_idRol] PRIMARY KEY CLUSTERED 
(
	[idRol] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[nombre] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Objeto: Table [dbo].[subcategoria] Fecha de script: 17/10/2026 11:42:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[subcategoria](
	[idSubcategoria] [int] IDENTITY(1,1) NOT NULL,
	[idCategoria] [int] NOT NULL,
	[nombre] [varchar](60) NOT NULL,
 CONSTRAINT [PK_IdSubcategoria] PRIMARY KEY CLUSTERED 
(
	[idSubcategoria] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Objeto: Table [dbo].[usuario] Fecha de script: 17/10/2026 11:42:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[usuario](
	[idUsuario] [int] IDENTITY(1,1) NOT NULL,
	[nombreUsuario] [varchar](50) NOT NULL,
	[contrasena] [varchar](255) NOT NULL,
	[idRol] [int] NOT NULL,
 CONSTRAINT [PK_usuario] PRIMARY KEY CLUSTERED 
(
	[idUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UQ_usuario_nombre] UNIQUE NONCLUSTERED 
(
	[nombreUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Objeto: Index [UQ_cliente_cedula] Fecha de script: 17/10/2026 11:42:58 ******/
CREATE UNIQUE NONCLUSTERED INDEX [UQ_cliente_cedula] ON [dbo].[cliente]
(
	[cedula] ASC
)
WHERE ([cedula] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Objeto: Index [UQ_cliente_email] Fecha de script: 17/10/2026 11:42:58 ******/
CREATE UNIQUE NONCLUSTERED INDEX [UQ_cliente_email] ON [dbo].[cliente]
(
	[email] ASC
)
WHERE ([email] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Objeto: Index [UQ_cliente_usuario] Fecha de script: 17/10/2026 11:42:58 ******/
CREATE UNIQUE NONCLUSTERED INDEX [UQ_cliente_usuario] ON [dbo].[cliente]
(
	[idUsuario] ASC
)
WHERE ([idUsuario] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Objeto: Index [UQ_empleado_usuario] Fecha de script: 17/10/2026 11:42:58 ******/
CREATE UNIQUE NONCLUSTERED INDEX [UQ_empleado_usuario] ON [dbo].[empleado]
(
	[idUsuario] ASC
)
WHERE ([idUsuario] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[empleado] ADD  DEFAULT ((1)) FOR [activo]
GO
ALTER TABLE [dbo].[factura] ADD  DEFAULT (getdate()) FOR [fechaEmision]
GO
ALTER TABLE [dbo].[factura] ADD  DEFAULT ('Emitida') FOR [estado]
GO
ALTER TABLE [dbo].[factura] ADD  DEFAULT ((0)) FOR [subtotal]
GO
ALTER TABLE [dbo].[factura] ADD  DEFAULT ((0.0)) FOR [costoEnvio]
GO
ALTER TABLE [dbo].[factura] ADD  DEFAULT ((0)) FOR [descuento]
GO
ALTER TABLE [dbo].[factura] ADD  DEFAULT ((0)) FOR [impuestos]
GO
ALTER TABLE [dbo].[factura] ADD  DEFAULT ((0)) FOR [propina]
GO
ALTER TABLE [dbo].[mesa] ADD  DEFAULT ('Disponible') FOR [estado]
GO
ALTER TABLE [dbo].[pago] ADD  DEFAULT (getdate()) FOR [fechaPago]
GO
ALTER TABLE [dbo].[pedido] ADD  DEFAULT (getdate()) FOR [fechaHora]
GO
ALTER TABLE [dbo].[pedido] ADD  DEFAULT ('Pendiente') FOR [estado]
GO
ALTER TABLE [dbo].[pedido] ADD  DEFAULT ((0.0)) FOR [costoEnvio]
GO
ALTER TABLE [dbo].[pedido] ADD  DEFAULT ((0.0)) FOR [subtotal]
GO
ALTER TABLE [dbo].[pedido] ADD  DEFAULT ((0.0)) FOR [total]
GO
ALTER TABLE [dbo].[pedido] ADD  DEFAULT ((0)) FOR [esDelivery]
GO
ALTER TABLE [dbo].[producto] ADD  DEFAULT ((1)) FOR [disponible]
GO
ALTER TABLE [dbo].[reservacion] ADD  DEFAULT ('Pendiente') FOR [estado]
GO
ALTER TABLE [dbo].[cliente]  WITH CHECK ADD  CONSTRAINT [FK_cliente_usuario] FOREIGN KEY([idUsuario])
REFERENCES [dbo].[usuario] ([idUsuario])
GO
ALTER TABLE [dbo].[cliente] CHECK CONSTRAINT [FK_cliente_usuario]
GO
ALTER TABLE [dbo].[detallePedido]  WITH CHECK ADD  CONSTRAINT [FK_IdPedidoDP] FOREIGN KEY([idPedido])
REFERENCES [dbo].[pedido] ([idPedido])
GO
ALTER TABLE [dbo].[detallePedido] CHECK CONSTRAINT [FK_IdPedidoDP]
GO
ALTER TABLE [dbo].[detallePedido]  WITH CHECK ADD  CONSTRAINT [FK_IdProductoDP] FOREIGN KEY([idProducto])
REFERENCES [dbo].[producto] ([idProducto])
GO
ALTER TABLE [dbo].[detallePedido] CHECK CONSTRAINT [FK_IdProductoDP]
GO
ALTER TABLE [dbo].[empleado]  WITH CHECK ADD  CONSTRAINT [FK_empleado_usuario] FOREIGN KEY([idUsuario])
REFERENCES [dbo].[usuario] ([idUsuario])
GO
ALTER TABLE [dbo].[empleado] CHECK CONSTRAINT [FK_empleado_usuario]
GO
ALTER TABLE [dbo].[factura]  WITH CHECK ADD  CONSTRAINT [FK_idPedidoFact] FOREIGN KEY([idPedido])
REFERENCES [dbo].[pedido] ([idPedido])
GO
ALTER TABLE [dbo].[factura] CHECK CONSTRAINT [FK_idPedidoFact]
GO
ALTER TABLE [dbo].[pago]  WITH CHECK ADD  CONSTRAINT [FK_idFactura] FOREIGN KEY([idFactura])
REFERENCES [dbo].[factura] ([idFactura])
GO
ALTER TABLE [dbo].[pago] CHECK CONSTRAINT [FK_idFactura]
GO
ALTER TABLE [dbo].[pedido]  WITH CHECK ADD  CONSTRAINT [FK_pedido_cliente] FOREIGN KEY([idCliente])
REFERENCES [dbo].[cliente] ([idCliente])
GO
ALTER TABLE [dbo].[pedido] CHECK CONSTRAINT [FK_pedido_cliente]
GO
ALTER TABLE [dbo].[pedido]  WITH CHECK ADD  CONSTRAINT [FK_pedido_empleado] FOREIGN KEY([idEmpleado])
REFERENCES [dbo].[empleado] ([idEmpleado])
GO
ALTER TABLE [dbo].[pedido] CHECK CONSTRAINT [FK_pedido_empleado]
GO
ALTER TABLE [dbo].[pedido]  WITH CHECK ADD  CONSTRAINT [FK_pedido_mesa] FOREIGN KEY([idMesa])
REFERENCES [dbo].[mesa] ([idMesa])
GO
ALTER TABLE [dbo].[pedido] CHECK CONSTRAINT [FK_pedido_mesa]
GO
ALTER TABLE [dbo].[pedido]  WITH CHECK ADD  CONSTRAINT [FK_pedido_repartidor] FOREIGN KEY([idRepartidor])
REFERENCES [dbo].[empleado] ([idEmpleado])
GO
ALTER TABLE [dbo].[pedido] CHECK CONSTRAINT [FK_pedido_repartidor]
GO
ALTER TABLE [dbo].[producto]  WITH CHECK ADD  CONSTRAINT [FK_IdCategoriaProd] FOREIGN KEY([idCategoria])
REFERENCES [dbo].[categoria] ([idCategoria])
GO
ALTER TABLE [dbo].[producto] CHECK CONSTRAINT [FK_IdCategoriaProd]
GO
ALTER TABLE [dbo].[producto]  WITH CHECK ADD  CONSTRAINT [FK_producto_subcategoria] FOREIGN KEY([idSubcategoria])
REFERENCES [dbo].[subcategoria] ([idSubcategoria])
GO
ALTER TABLE [dbo].[producto] CHECK CONSTRAINT [FK_producto_subcategoria]
GO
ALTER TABLE [dbo].[reservacion]  WITH CHECK ADD  CONSTRAINT [FK_idClienteReservacion] FOREIGN KEY([idCliente])
REFERENCES [dbo].[cliente] ([idCliente])
GO
ALTER TABLE [dbo].[reservacion] CHECK CONSTRAINT [FK_idClienteReservacion]
GO
ALTER TABLE [dbo].[reservacion]  WITH CHECK ADD  CONSTRAINT [FK_idMesaReservacion] FOREIGN KEY([idMesa])
REFERENCES [dbo].[mesa] ([idMesa])
GO
ALTER TABLE [dbo].[reservacion] CHECK CONSTRAINT [FK_idMesaReservacion]
GO
ALTER TABLE [dbo].[subcategoria]  WITH CHECK ADD  CONSTRAINT [FK_subcategoria_categoria] FOREIGN KEY([idCategoria])
REFERENCES [dbo].[categoria] ([idCategoria])
GO
ALTER TABLE [dbo].[subcategoria] CHECK CONSTRAINT [FK_subcategoria_categoria]
GO
ALTER TABLE [dbo].[usuario]  WITH CHECK ADD  CONSTRAINT [FK_usuario_rol] FOREIGN KEY([idRol])
REFERENCES [dbo].[rol] ([idRol])
GO
ALTER TABLE [dbo].[usuario] CHECK CONSTRAINT [FK_usuario_rol]
GO
ALTER TABLE [dbo].[cliente]  WITH CHECK ADD  CONSTRAINT [CK_cliente_genero] CHECK  (([genero]='O' OR [genero]='F' OR [genero]='M'))
GO
ALTER TABLE [dbo].[cliente] CHECK CONSTRAINT [CK_cliente_genero]
GO
ALTER TABLE [dbo].[detallePedido]  WITH CHECK ADD CHECK  (([cantidad]>(0)))
GO
ALTER TABLE [dbo].[empleado]  WITH CHECK ADD  CONSTRAINT [CK_empleado_genero] CHECK  (([genero]='O' OR [genero]='F' OR [genero]='M'))
GO
ALTER TABLE [dbo].[empleado] CHECK CONSTRAINT [CK_empleado_genero]
GO
ALTER TABLE [dbo].[factura]  WITH CHECK ADD CHECK  (([total]>(0)))
GO
ALTER TABLE [dbo].[mesa]  WITH CHECK ADD CHECK  (([capacidad]>=(1) AND [capacidad]<=(20)))
GO
ALTER TABLE [dbo].[mesa]  WITH CHECK ADD  CONSTRAINT [CK_Estado] CHECK  (([estado]='En limpieza' OR [estado]='Reservada' OR [estado]='Ocupada' OR [estado]='Disponible'))
GO
ALTER TABLE [dbo].[mesa] CHECK CONSTRAINT [CK_Estado]
GO
ALTER TABLE [dbo].[pago]  WITH CHECK ADD CHECK  (([monto]>(0)))
GO
ALTER TABLE [dbo].[pago]  WITH CHECK ADD  CONSTRAINT [CK_pago_metodo] CHECK  (([metodoDePago]='Otro' OR [metodoDePago]='Transferencia' OR [metodoDePago]='Tarjeta' OR [metodoDePago]='Efectivo'))
GO
ALTER TABLE [dbo].[pago] CHECK CONSTRAINT [CK_pago_metodo]
GO
ALTER TABLE [dbo].[pedido]  WITH CHECK ADD  CONSTRAINT [CK_pedido_estado] CHECK  (([estado]='Cancelado' OR [estado]='Entregado' OR [estado]='Listo' OR [estado]='En preparación' OR [estado]='Pendiente'))
GO
ALTER TABLE [dbo].[pedido] CHECK CONSTRAINT [CK_pedido_estado]
GO
ALTER TABLE [dbo].[producto]  WITH CHECK ADD CHECK  (([precio]>(0)))
GO
ALTER TABLE [dbo].[reservacion]  WITH CHECK ADD CHECK  (([cantidadPersonas]>(0)))
GO
ALTER TABLE [dbo].[reservacion]  WITH CHECK ADD  CONSTRAINT [CK_estadoReservacion] CHECK  (([estado]='Completada' OR [estado]='Cancelada' OR [estado]='Confirmada' OR [estado]='Pendiente'))
GO
ALTER TABLE [dbo].[reservacion] CHECK CONSTRAINT [CK_estadoReservacion]
GO

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

-- Procedimientos de Acceso.
USE [RestaurantSM];
GO
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
GO
-- Procedimiento sp_Catalogo_ExigirAdministrador: consulta o modifica los datos de su gestion con parametros definidos.
CREATE OR ALTER PROCEDURE dbo.sp_Catalogo_ExigirAdministrador
    @IdAdministrador INT
AS
BEGIN
    SET NOCOUNT ON;
    IF NOT EXISTS (SELECT 1
                   FROM dbo.usuario
                   WHERE idUsuario = @IdAdministrador
                         AND idRol = 1)
       OR EXISTS (SELECT 1
                  FROM dbo.empleado
                  WHERE idUsuario = @IdAdministrador
                        AND activo = 0)
        THROW 50020, 'Solo un administrador activo puede modificar el catálogo.', 1;
END
GO

-- Procedimiento sp_Cliente_ExigirActivo: consulta o modifica los datos de su gestion con parametros definidos.
CREATE OR ALTER PROCEDURE dbo.sp_Cliente_ExigirActivo
    @Actor INT,
    @IdCliente INT OUTPUT
AS
BEGIN
    SET @IdCliente = NULL;
    SELECT @IdCliente = c.idCliente
    FROM dbo.cliente AS c
         INNER JOIN
         dbo.usuario AS u
         ON u.idUsuario = c.idUsuario
    WHERE c.idUsuario = @Actor
          AND u.idRol = 3
          AND c.activo = 1;
    IF @IdCliente IS NULL
        THROW 50217, 'Necesita una cuenta de cliente activa con perfil asociado.', 1;
END
GO

-- Procedimiento sp_Configuracion_Guardar: consulta o modifica los datos de su gestion con parametros definidos.
CREATE OR ALTER PROCEDURE dbo.sp_Configuracion_Guardar
    @Actor INT,
    @Nombre NVARCHAR (120),
    @Tasa DECIMAL (5, 2)
AS
BEGIN
    SET NOCOUNT ON;
    EXECUTE dbo.sp_Catalogo_ExigirAdministrador @Actor;
    IF NULLIF (LTRIM(RTRIM(@Nombre)), '') IS NULL
       OR @Tasa IS NULL
       OR @Tasa < 0
       OR @Tasa > 100
        THROW 50530, 'Indique nombre y una tasa incluida entre 0 y 100.', 1;
    UPDATE dbo.configuracionRestaurante
    SET nombre = LTRIM(RTRIM(@Nombre)),
        tasaImpuesto = @Tasa
    WHERE id = 1;
END
GO

-- Procedimiento sp_Operacion_ExigirPersonal: consulta o modifica los datos de su gestion con parametros definidos.
CREATE OR ALTER PROCEDURE dbo.sp_Operacion_ExigirPersonal
    @Actor INT
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
END
GO

-- Comprueba que el programa y la base utilizan el esquema de procedimientos simplificados.
CREATE OR ALTER PROCEDURE dbo.sp_Sistema_Verificar
AS
BEGIN
    SET NOCOUNT ON;
    IF NOT EXISTS (SELECT 1
                   FROM dbo.versionEsquema
                   WHERE version = 9)
       OR COL_LENGTH('dbo.pedido', 'total') IS NULL
       OR COL_LENGTH('dbo.pedido', 'subtotal') IS NULL
       OR COL_LENGTH('dbo.pedido', 'solicitud') IS NULL
       OR OBJECT_ID('dbo.facturaDetalle', 'U') IS NULL
       OR OBJECT_ID('dbo.sp_Cobro_Registrar', 'P') IS NULL
       OR OBJECT_ID('dbo.sp_Documento_Obtener', 'P') IS NULL
       OR OBJECT_ID('dbo.sp_Operacion_CambiarMesa', 'P') IS NULL
       OR OBJECT_ID('dbo.sp_Pedido_Iniciar', 'P') IS NULL
       OR COL_LENGTH('dbo.inventarioProducto', 'revision') IS NULL
        THROW 50500, 'Ejecute sql/ACTUALIZAR_TODO_RESTAURANTSM.sql completo antes de usar las operaciones.', 1;
    SELECT nombre AS Nombre,
           moneda AS Moneda,
           tasaImpuesto AS TasaImpuesto
    FROM dbo.configuracionRestaurante
    WHERE id = 1;
END
GO

-- Procedimiento sp_Usuario_Listar: consulta o modifica los datos de su gestion con parametros definidos.
CREATE OR ALTER PROCEDURE sp_Usuario_Listar
AS
BEGIN
    SET NOCOUNT ON;
    SELECT u.idUsuario,
           u.nombreUsuario,
           u.idRol,
           r.nombre AS NombreRol
    FROM usuario AS u
         INNER JOIN
         rol AS r
         ON u.idRol = r.idRol;
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
           r.nombre AS NombreRol
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

-- Procedimientos de Catalogos.
USE [RestaurantSM];
GO
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
GO
-- Procedimiento sp_Categoria_Desactivar: consulta o modifica los datos de su gestion con parametros definidos.
CREATE OR ALTER PROCEDURE dbo.sp_Categoria_Desactivar
    @IdCategoria INT,
    @IdAdministrador INT
AS
BEGIN
    SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    EXECUTE dbo.sp_Catalogo_ExigirAdministrador @IdAdministrador;
    BEGIN TRY
        BEGIN TRANSACTION;
        IF NOT EXISTS (SELECT 1
                       FROM dbo.categoria
                       WHERE idCategoria = @IdCategoria)
            THROW 50022, 'La categoría seleccionada ya no existe.', 1;
        IF EXISTS (SELECT 1
                   FROM dbo.producto
                   WHERE idCategoria = @IdCategoria
                         AND disponible = 1)
            THROW 50023, 'Primero marque como no disponibles los productos de esta categoría.', 1;
        UPDATE dbo.categoria
        SET activa = 0
        WHERE idCategoria = @IdCategoria;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK;
        THROW;
    END CATCH
END
GO

-- Procedimiento sp_Categoria_Eliminar: consulta o modifica los datos de su gestion con parametros definidos.
CREATE OR ALTER PROCEDURE dbo.sp_Categoria_Eliminar
    @IdCategoria INT,
    @IdAdministrador INT
AS
BEGIN
    SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    EXECUTE dbo.sp_Catalogo_ExigirAdministrador @IdAdministrador;
    BEGIN TRY
        BEGIN TRANSACTION;
        IF NOT EXISTS (SELECT 1
                       FROM dbo.categoria
                       WHERE idCategoria = @IdCategoria)
            THROW 50400, 'La categoría ya no existe. Recargue el listado.', 1;
        IF EXISTS (SELECT 1
                   FROM dbo.producto
                   WHERE idCategoria = @IdCategoria)
           OR EXISTS (SELECT 1
                      FROM dbo.subcategoria
                      WHERE idCategoria = @IdCategoria)
            THROW 50401, 'La categoría tiene productos o subcategorías. Conserve sus relaciones y utilice Desactivar.', 1;
        DELETE dbo.categoria
        WHERE idCategoria = @IdCategoria;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK;
        THROW;
    END CATCH
END
GO

-- Procedimiento sp_Categoria_Guardar: consulta o modifica los datos de su gestion con parametros definidos.
CREATE OR ALTER PROCEDURE dbo.sp_Categoria_Guardar
    @IdCategoria INT,
    @IdAdministrador INT,
    @Nombre VARCHAR (60),
    @Descripcion VARCHAR (200),
    @Activa BIT
AS
BEGIN
    SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    EXECUTE dbo.sp_Catalogo_ExigirAdministrador @IdAdministrador;
    IF NULLIF (LTRIM(RTRIM(@Nombre)), '') IS NULL
        THROW 50021, 'El nombre de la categoría es obligatorio.', 1;
    BEGIN TRY
        BEGIN TRANSACTION;
        IF @IdCategoria = 0
            INSERT dbo.categoria (nombre, descripcion, activa)
            VALUES              (@Nombre, @Descripcion, @Activa);
        ELSE
            BEGIN
                IF NOT EXISTS (SELECT 1
                               FROM dbo.categoria
                               WHERE idCategoria = @IdCategoria)
                    THROW 50022, 'La categoría seleccionada ya no existe.', 1;
                IF @Activa = 0
                   AND EXISTS (SELECT 1
                               FROM dbo.producto
                               WHERE idCategoria = @IdCategoria
                                     AND disponible = 1)
                    THROW 50023, 'Primero marque como no disponibles los productos de esta categoría.', 1;
                UPDATE dbo.categoria
                SET nombre = @Nombre,
                    descripcion = @Descripcion,
                    activa = @Activa
                WHERE idCategoria = @IdCategoria;
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

-- Procedimiento sp_Categoria_Listar: consulta o modifica los datos de su gestion con parametros definidos.
CREATE OR ALTER PROCEDURE dbo.sp_Categoria_Listar
AS
BEGIN
    SET NOCOUNT ON;
    SELECT idCategoria,
           nombre,
           descripcion,
           activa
    FROM dbo.categoria
    ORDER BY nombre;
END
GO

-- Procedimiento sp_Producto_Desactivar: consulta o modifica los datos de su gestion con parametros definidos.
CREATE OR ALTER PROCEDURE dbo.sp_Producto_Desactivar
    @IdProducto INT,
    @IdAdministrador INT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    EXECUTE dbo.sp_Catalogo_ExigirAdministrador @IdAdministrador;
    UPDATE dbo.producto
    SET disponible = 0
    WHERE idProducto = @IdProducto;
    IF @@ROWCOUNT = 0
        THROW 50027, 'El producto seleccionado ya no existe.', 1;
END
GO

-- Procedimiento sp_Producto_Disponibles: consulta o modifica los datos de su gestion con parametros definidos.
CREATE OR ALTER PROCEDURE dbo.sp_Producto_Disponibles
AS
BEGIN
    SET NOCOUNT ON;
    SELECT p.idProducto,
           p.idCategoria,
           p.idSubcategoria,
           c.nombre AS CategoriaNombre,
           s.nombre AS SubcategoriaNombre,
           p.nombre,
           p.precio,
           p.descripcion,
           p.codigo,
           p.disponible
    FROM dbo.producto AS p
         INNER JOIN
         dbo.categoria AS c
         ON c.idCategoria = p.idCategoria
         LEFT OUTER JOIN
         dbo.subcategoria AS s
         ON s.idSubcategoria = p.idSubcategoria
    WHERE p.disponible = 1
          AND c.activa = 1
          AND EXISTS (SELECT 1
                      FROM dbo.inventarioProducto AS i
                      WHERE i.idProducto = p.idProducto
                            AND i.activo = 1
                            AND i.stockActual > 0)
    ORDER BY c.nombre, p.nombre;
END
GO

-- Procedimiento sp_Producto_Eliminar: consulta o modifica los datos de su gestion con parametros definidos.
CREATE OR ALTER PROCEDURE dbo.sp_Producto_Eliminar
    @IdProducto INT,
    @IdAdministrador INT
AS
BEGIN
    SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    EXECUTE dbo.sp_Catalogo_ExigirAdministrador @IdAdministrador;
    BEGIN TRY
        BEGIN TRANSACTION;
        IF NOT EXISTS (SELECT 1
                       FROM dbo.producto
                       WHERE idProducto = @IdProducto)
            THROW 50402, 'El producto ya no existe. Recargue el listado.', 1;
        IF EXISTS (SELECT 1
                   FROM dbo.detallePedido
                   WHERE idProducto = @IdProducto)
            THROW 50403, 'El producto forma parte del historial de pedidos. Márquelo como no disponible.', 1;
        IF EXISTS (SELECT 1
                   FROM dbo.inventarioProducto
                   WHERE idProducto = @IdProducto)
            THROW 50404, 'El producto tiene un registro de inventario. Elimine ese registro primero si las reglas lo permiten.', 1;
        DELETE dbo.producto
        WHERE idProducto = @IdProducto;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK;
        THROW;
    END CATCH
END
GO

-- Procedimiento sp_Producto_Guardar: consulta o modifica los datos de su gestion con parametros definidos.
CREATE OR ALTER PROCEDURE dbo.sp_Producto_Guardar
    @IdProducto INT,
    @IdAdministrador INT,
    @IdCategoria INT,
    @IdSubcategoria INT,
    @Nombre VARCHAR (100),
    @Precio DECIMAL (10, 2),
    @Descripcion VARCHAR (250),
    @Codigo VARCHAR (20),
    @Disponible BIT
AS
BEGIN
    SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    EXECUTE dbo.sp_Catalogo_ExigirAdministrador @IdAdministrador;
    IF NULLIF (LTRIM(RTRIM(@Nombre)), '') IS NULL
       OR NULLIF (LTRIM(RTRIM(@Codigo)), '') IS NULL
       OR @Precio IS NULL
       OR @Precio <= 0
        THROW 50024, 'Nombre, código y precio positivo son obligatorios.', 1;
    BEGIN TRY
        BEGIN TRANSACTION;
        IF NOT EXISTS (SELECT 1
                       FROM dbo.categoria
                       WHERE idCategoria = @IdCategoria)
            THROW 50022, 'La categoría seleccionada ya no existe.', 1;
        IF @Disponible = 1
           AND EXISTS (SELECT 1
                       FROM dbo.categoria
                       WHERE idCategoria = @IdCategoria
                             AND activa = 0)
            THROW 50025, 'Active la categoría antes de ofrecer este producto.', 1;
        IF @IdSubcategoria IS NOT NULL
           AND NOT EXISTS (SELECT 1
                           FROM dbo.subcategoria
                           WHERE idSubcategoria = @IdSubcategoria
                                 AND idCategoria = @IdCategoria)
            THROW 50026, 'La subcategoría no pertenece a la categoría seleccionada.', 1;
        IF @IdProducto = 0
            INSERT dbo.producto (idCategoria, idSubcategoria, nombre, precio, descripcion, codigo, disponible)
            VALUES             (@IdCategoria, @IdSubcategoria, @Nombre, @Precio, @Descripcion, @Codigo, @Disponible);
        ELSE
            BEGIN
                UPDATE dbo.producto
                SET idCategoria = @IdCategoria,
                    idSubcategoria = @IdSubcategoria,
                    nombre = @Nombre,
                    precio = @Precio,
                    descripcion = @Descripcion,
                    codigo = @Codigo,
                    disponible = @Disponible
                WHERE idProducto = @IdProducto;
                IF @@ROWCOUNT = 0
                    THROW 50027, 'El producto seleccionado ya no existe.', 1;
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

-- Procedimiento sp_Producto_Listar: consulta o modifica los datos de su gestion con parametros definidos.
CREATE OR ALTER PROCEDURE dbo.sp_Producto_Listar
AS
BEGIN
    SET NOCOUNT ON;
    SELECT p.idProducto,
           p.idCategoria,
           p.idSubcategoria,
           c.nombre AS CategoriaNombre,
           s.nombre AS SubcategoriaNombre,
           p.nombre,
           p.precio,
           p.descripcion,
           p.codigo,
           p.disponible
    FROM dbo.producto AS p
         INNER JOIN
         dbo.categoria AS c
         ON c.idCategoria = p.idCategoria
         LEFT OUTER JOIN
         dbo.subcategoria AS s
         ON s.idSubcategoria = p.idSubcategoria
    ORDER BY p.nombre;
END
GO

-- Procedimiento sp_Rol_Eliminar: consulta o modifica los datos de su gestion con parametros definidos.
CREATE OR ALTER PROCEDURE dbo.sp_Rol_Eliminar
    @Actor INT,
    @Id INT
AS
BEGIN
    EXECUTE dbo.sp_Catalogo_ExigirAdministrador @Actor;
    IF @Id IN (1, 2, 3)
        THROW 50207, 'Administrador, Empleado y Cliente son roles protegidos.', 1;
    IF EXISTS (SELECT 1
               FROM dbo.usuario
               WHERE idRol = @Id)
        THROW 50208, 'El rol tiene usuarios asociados.', 1;
    DELETE dbo.rol
    WHERE idRol = @Id;
    IF @@ROWCOUNT = 0
        THROW 50206, 'El rol ya no existe.', 1;
END
GO

-- Procedimiento sp_Rol_Guardar: consulta o modifica los datos de su gestion con parametros definidos.
CREATE OR ALTER PROCEDURE dbo.sp_Rol_Guardar
    @Actor INT,
    @Id INT=0,
    @Nombre VARCHAR (50),
    @Descripcion VARCHAR (150)=NULL
AS
BEGIN
    EXECUTE dbo.sp_Catalogo_ExigirAdministrador @Actor;
    IF NULLIF (LTRIM(RTRIM(@Nombre)), '') IS NULL
        THROW 50204, 'El nombre del rol es obligatorio.', 1;
    IF @Id IN (1, 2, 3)
       AND EXISTS (SELECT 1
                   FROM dbo.rol
                   WHERE idRol = @Id
                         AND nombre <> @Nombre)
        THROW 50205, 'Los roles del sistema no se pueden renombrar; puede editar su descripción.', 1;
    IF @Id = 0
        BEGIN
            INSERT dbo.rol (nombre, descripcion)
            VALUES        (@Nombre, @Descripcion);
            SELECT CONVERT (INT, SCOPE_IDENTITY());
        END
    ELSE
        BEGIN
            UPDATE dbo.rol
            SET nombre = @Nombre,
                descripcion = @Descripcion
            WHERE idRol = @Id;
            IF @@ROWCOUNT = 0
                THROW 50206, 'El rol ya no existe.', 1;
            SELECT @Id;
        END
END
GO

-- Procedimiento sp_Rol_Listar: consulta o modifica los datos de su gestion con parametros definidos.
CREATE OR ALTER PROCEDURE sp_Rol_Listar
AS
BEGIN
    SET NOCOUNT ON;
    SELECT idRol,
           nombre,
           descripcion
    FROM rol;
END
GO

-- Procedimiento sp_Subcategoria_Listar: consulta o modifica los datos de su gestion con parametros definidos.
CREATE OR ALTER PROCEDURE dbo.sp_Subcategoria_Listar
AS
BEGIN
    SET NOCOUNT ON;
    SELECT idSubcategoria,
           idCategoria,
           nombre
    FROM dbo.subcategoria
    ORDER BY nombre;
END
GO

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

-- Procedimientos de Inventario.
USE [RestaurantSM];
GO
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
GO
-- Registra una entrada o salida con motivo; no permite stock negativo.
CREATE OR ALTER PROCEDURE dbo.sp_Inventario_Ajustar
    @Actor int,
    @IdProducto int,
    @Cantidad int,
    @Tipo varchar(20),
    @Motivo nvarchar(200),
    @Revision int
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;
    EXEC dbo.sp_Catalogo_ExigirAdministrador @Actor;
    IF @Cantidad <= 0 OR @Tipo NOT IN ('Entrada', 'Salida') OR NULLIF(LTRIM(RTRIM(@Motivo)), '') IS NULL
        THROW 50527, 'Indique tipo, cantidad positiva y motivo.', 1;
    BEGIN TRY
        BEGIN TRANSACTION;
        DECLARE @Anterior int;
        DECLARE @Nuevo bigint;
        SELECT @Anterior = stockActual FROM dbo.inventarioProducto
        WHERE idProducto = @IdProducto AND activo = 1 AND revision = @Revision;
        IF @Anterior IS NULL THROW 50528, 'El inventario cambio o esta inactivo. Recargue.', 1;
        IF @Tipo = 'Entrada' SET @Nuevo = CONVERT(bigint, @Anterior) + @Cantidad;
        ELSE SET @Nuevo = CONVERT(bigint, @Anterior) - @Cantidad;
        IF @Nuevo < 0 OR @Nuevo > 2147483647 THROW 50529, 'Las existencias quedan fuera del rango permitido.', 1;
        UPDATE dbo.inventarioProducto SET stockActual = @Nuevo, revision = revision + 1
        WHERE idProducto = @IdProducto AND revision = @Revision;
        IF @@ROWCOUNT = 0 THROW 50528, 'El inventario cambio. Recargue.', 1;
        EXEC dbo.sp_Inventario_RegistrarMovimiento @Actor, @IdProducto, @Anterior, @Cantidad, @Tipo, @Motivo;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO

-- Elimina el control solo si no quedan unidades ni pedidos abiertos.
CREATE OR ALTER PROCEDURE dbo.sp_Inventario_Eliminar @Actor int,
    @IdProducto int,
    @Revision int
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;
    EXEC dbo.sp_Catalogo_ExigirAdministrador @Actor;
    BEGIN TRY
        BEGIN TRANSACTION;
        IF EXISTS (SELECT 1 FROM dbo.detallePedido d INNER JOIN dbo.pedido p ON p.idPedido = d.idPedido
            WHERE d.idProducto = @IdProducto AND d.controlInventario = 1 AND p.estado NOT IN ('Pagado', 'Cancelado'))
            THROW 50529, 'El producto tiene pedidos abiertos.', 1;
        DELETE FROM dbo.inventarioProducto
        WHERE idProducto = @IdProducto AND stockActual = 0 AND revision = @Revision;
        IF @@ROWCOUNT = 0 THROW 50528, 'El inventario cambio o aun tiene existencias. Recargue.', 1;
        EXEC dbo.sp_Inventario_RegistrarMovimiento @Actor, @IdProducto, 0, 0, 'Salida', 'Eliminacion de inventario sin existencias';
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO

-- Crea o modifica inventario. Revision es un contador entero que aumenta en cada cambio.
CREATE OR ALTER PROCEDURE dbo.sp_Inventario_Guardar
    @Actor int,
    @IdProducto int,
    @Stock int,
    @Minimo int,
    @Activo bit,
    @Revision int = 0
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;
    EXEC dbo.sp_Catalogo_ExigirAdministrador @Actor;
    IF @Stock < 0 OR @Minimo < 0 THROW 50213, 'Las existencias no pueden ser negativas.', 1;
    BEGIN TRY
        BEGIN TRANSACTION;
        IF NOT EXISTS (SELECT 1 FROM dbo.producto WHERE idProducto = @IdProducto)
            THROW 50214, 'Seleccione un producto existente.', 1;
        IF @Activo = 0 AND (@Stock <> 0 OR EXISTS (
            SELECT 1 FROM dbo.detallePedido d INNER JOIN dbo.pedido p ON p.idPedido = d.idPedido
            WHERE d.idProducto = @IdProducto AND d.controlInventario = 1 AND p.estado NOT IN ('Pagado', 'Cancelado')))
            THROW 50215, 'Retire el control solo con stock cero y sin pedidos abiertos.', 1;

        DECLARE @Anterior int;
        SELECT @Anterior = stockActual FROM dbo.inventarioProducto WHERE idProducto = @IdProducto;
        IF @Anterior IS NULL
        BEGIN
            IF @Revision <> 0 THROW 50216, 'El inventario cambio. Recargue el listado.', 1;
            INSERT INTO dbo.inventarioProducto(idProducto, stockActual, stockMinimo, activo, revision)
            VALUES (@IdProducto, @Stock, @Minimo, @Activo, 1);
            EXEC dbo.sp_Inventario_RegistrarMovimiento @Actor, @IdProducto, 0, @Stock, 'Entrada', 'Alta de inventario';
        END
        ELSE
        BEGIN
            -- Revision es un contador: 1, 2, 3... Cada cambio suma uno.
            UPDATE dbo.inventarioProducto
            SET stockActual = @Stock, stockMinimo = @Minimo, activo = @Activo, revision = revision + 1
            WHERE idProducto = @IdProducto AND revision = @Revision;
            IF @@ROWCOUNT = 0 THROW 50216, 'El inventario cambio. Recargue antes de guardar.', 1;
            IF @Stock <> @Anterior
            BEGIN
                DECLARE @Cantidad int;
                DECLARE @Tipo varchar(20);
                SET @Cantidad = ABS(@Stock - @Anterior);
                IF @Stock > @Anterior SET @Tipo = 'Entrada'; ELSE SET @Tipo = 'Salida';
                EXEC dbo.sp_Inventario_RegistrarMovimiento @Actor, @IdProducto, @Anterior,
                    @Cantidad, @Tipo, 'Ajuste desde gestion de inventario';
            END;
        END;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO

-- Procedimiento sp_Inventario_Listar: consulta o modifica los datos de su gestion con parametros definidos.
CREATE OR ALTER PROCEDURE dbo.sp_Inventario_Listar
    @Actor INT
AS
BEGIN
    EXECUTE dbo.sp_Catalogo_ExigirAdministrador @Actor;
    SELECT p.idProducto AS IdProducto,
           p.nombre AS Producto,
           c.nombre AS Categoria,
           p.precio AS PrecioVenta,
           'Unidad' AS Unidad,
           i.stockActual AS StockActual,
           i.stockMinimo AS StockMinimo,
           i.activo AS Activo,
           i.revision AS Revision
    FROM dbo.inventarioProducto AS i
         INNER JOIN
         dbo.producto AS p
         ON p.idProducto = i.idProducto
         INNER JOIN
         dbo.categoria AS c
         ON c.idCategoria = p.idCategoria
    ORDER BY p.nombre;
END
GO

-- Procedimiento sp_Inventario_Movimientos: consulta o modifica los datos de su gestion con parametros definidos.
CREATE OR ALTER PROCEDURE dbo.sp_Inventario_Movimientos
    @Actor INT
AS
BEGIN
    SET NOCOUNT ON;
    EXECUTE dbo.sp_Catalogo_ExigirAdministrador @Actor;
    SELECT id AS Id,
           fecha AS Fecha,
           producto AS Producto,
           anterior AS Anterior,
           nuevo AS Nuevo,
           diferencia AS Diferencia,
           usuario AS Usuario,
           motivo AS Motivo
    FROM dbo.movimientoInventario
    ORDER BY id DESC;
END
GO

-- Registra el usuario, el producto, las unidades y el motivo recibidos por parametros.
-- Cada procedimiento recibe el usuario y el motivo de forma explicita.
CREATE OR ALTER PROCEDURE dbo.sp_Inventario_RegistrarMovimiento
    @Actor int,
    @IdProducto int,
    @Anterior int,
    @Cantidad int,
    @Tipo varchar(20),
    @Motivo nvarchar(200)
AS
BEGIN
    SET NOCOUNT ON;
    IF @@TRANCOUNT = 0 THROW 50720, 'El movimiento debe formar parte de una transaccion.', 1;
    DECLARE @Nuevo int;
    IF @Tipo = 'Entrada' SET @Nuevo = @Anterior + @Cantidad;
    ELSE SET @Nuevo = @Anterior - @Cantidad;
    INSERT INTO dbo.movimientoInventario
        (idProducto, producto, anterior, nuevo, diferencia, idUsuario, usuario, motivo)
    SELECT p.idProducto, p.nombre, @Anterior, @Nuevo, @Nuevo - @Anterior,
        @Actor, u.nombreUsuario, @Motivo
    FROM dbo.producto p
    INNER JOIN dbo.usuario u ON u.idUsuario = @Actor
    WHERE p.idProducto = @IdProducto;
END;
GO

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

-- Procedimientos de Reservas.
USE [RestaurantSM];
GO
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
GO
-- Procedimiento sp_Cliente_Reserva_Cancelar: consulta o modifica los datos de su gestion con parametros definidos.
CREATE OR ALTER PROCEDURE dbo.sp_Cliente_Reserva_Cancelar
    @Actor INT,
    @Id INT
AS
BEGIN
    DECLARE @Cliente AS INT;
    EXECUTE dbo.sp_Cliente_ExigirActivo @Actor, @Cliente OUTPUT;
    UPDATE dbo.reservacion
    SET estado = 'Cancelada'
    WHERE idReservacion = @Id
          AND idCliente = @Cliente
          AND estado IN ('Pendiente', 'Confirmada')
          AND fechaHoraReserva > GETDATE();
    IF @@ROWCOUNT = 0
        THROW 50228, 'Solo puede cancelar sus reservas futuras activas.', 1;
END
GO

-- Procedimiento sp_Cliente_Reserva_Guardar: consulta o modifica los datos de su gestion con parametros definidos.
CREATE OR ALTER PROCEDURE dbo.sp_Cliente_Reserva_Guardar
    @Actor INT,
    @Id INT=0,
    @IdMesa INT,
    @Fecha DATETIME,
    @Personas INT
AS
BEGIN
    SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    DECLARE @Cliente AS INT;
    IF @Fecha <= GETDATE()
       OR @Personas NOT BETWEEN 1 AND 20
        THROW 50226, 'Seleccione una fecha futura y una cantidad válida.', 1;
    BEGIN TRY
        BEGIN TRANSACTION;
        EXECUTE dbo.sp_Cliente_ExigirActivo @Actor, @Cliente OUTPUT;
        IF NOT EXISTS (SELECT 1
                       FROM dbo.mesa
                       WHERE idMesa = @IdMesa
                             AND activa = 1
                             AND capacidad >= @Personas
                             AND estado IN ('Disponible', 'Reservada'))
            THROW 50227, 'La mesa no está disponible o no tiene la capacidad necesaria.', 1;
        IF @Id <> 0
           AND NOT EXISTS (SELECT 1
                           FROM dbo.reservacion
                           WHERE idReservacion = @Id
                                 AND idCliente = @Cliente
                                 AND estado IN ('Pendiente', 'Confirmada')
                                 AND fechaHoraReserva > GETDATE())
            THROW 50228, 'Solo puede modificar sus reservas futuras activas.', 1;
        IF EXISTS (SELECT 1
                   FROM dbo.reservacion
                   WHERE idMesa = @IdMesa
                         AND idReservacion <> @Id
                         AND estado IN ('Pendiente', 'Confirmada')
                         AND fechaHoraReserva > DATEADD(hour, -2, @Fecha)
                         AND fechaHoraReserva < DATEADD(hour, 2, @Fecha))
            THROW 50229, 'La mesa tiene otra reserva dentro del intervalo de dos horas.', 1;
        IF @Id = 0
            BEGIN
                INSERT dbo.reservacion (idCliente, idMesa, fechaHoraReserva, cantidadPersonas, estado)
                VALUES                (@Cliente, @IdMesa, @Fecha, @Personas, 'Pendiente');
                SET @Id = CONVERT (INT, SCOPE_IDENTITY());
            END
        ELSE
            UPDATE dbo.reservacion
            SET idMesa = @IdMesa,
                fechaHoraReserva = @Fecha,
                cantidadPersonas = @Personas
            WHERE idReservacion = @Id
                  AND idCliente = @Cliente;
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

-- Procedimiento sp_Cliente_Reservas: consulta o modifica los datos de su gestion con parametros definidos.
CREATE OR ALTER PROCEDURE dbo.sp_Cliente_Reservas
    @Actor INT
AS
BEGIN
    DECLARE @Cliente AS INT;
    EXECUTE dbo.sp_Cliente_ExigirActivo @Actor, @Cliente OUTPUT;
    SELECT r.idReservacion AS IdReserva,
           r.idMesa AS IdMesa,
           m.numero AS Mesa,
           r.fechaHoraReserva AS Fecha,
           r.cantidadPersonas AS Personas,
           r.estado AS Estado
    FROM dbo.reservacion AS r
         INNER JOIN
         dbo.mesa AS m
         ON m.idMesa = r.idMesa
    WHERE r.idCliente = @Cliente
    ORDER BY r.fechaHoraReserva DESC;
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
    EXECUTE dbo.sp_Operacion_ExigirPersonal @Actor;
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

-- Procedimiento sp_Operacion_Reservas: consulta o modifica los datos de su gestion con parametros definidos.
CREATE OR ALTER PROCEDURE dbo.sp_Operacion_Reservas
    @Actor INT
AS
BEGIN
    SET NOCOUNT ON;
    EXECUTE dbo.sp_Operacion_ExigirPersonal @Actor;
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


UPDATE f SET clienteNombre=CONCAT(c.pNombreCl,' ',c.pApellidoCl),clienteCedula=c.cedula,empleadoNombre=COALESCE(u.nombreUsuario,'Registro anterior'),tasaImpuesto=NULL FROM dbo.factura f JOIN dbo.pedido p ON p.idPedido=f.idPedido JOIN dbo.cliente c ON c.idCliente=p.idCliente LEFT JOIN dbo.empleado e ON e.idEmpleado=p.idEmpleado LEFT JOIN dbo.usuario u ON u.idUsuario=e.idUsuario WHERE f.clienteNombre IS NULL;
INSERT dbo.facturaDetalle(idFactura,idProducto,producto,cantidad,precio,subtotal) SELECT f.idFactura,d.idProducto,p.nombre,d.cantidad,d.precioUnitario,d.total FROM dbo.factura f JOIN dbo.detallePedido d ON d.idPedido=f.idPedido JOIN dbo.producto p ON p.idProducto=d.idProducto WHERE NOT EXISTS(SELECT 1 FROM dbo.facturaDetalle fd WHERE fd.idFactura=f.idFactura);
IF NOT EXISTS(SELECT 1 FROM dbo.versionEsquema WHERE version=9) INSERT dbo.versionEsquema(version,descripcion) VALUES(9,N'Procedimientos legibles, pedidos por lineas e inventario con contador entero');
GO

EXEC dbo.sp_Sistema_Verificar;
PRINT N'Base preparada. Version 009: procedimientos simplificados.';
GO

SET NOEXEC OFF;
GO
