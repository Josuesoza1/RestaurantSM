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
