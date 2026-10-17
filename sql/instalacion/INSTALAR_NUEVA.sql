:On Error exit
-- Activar Consulta > Modo SQLCMD en SSMS antes de ejecutar.
-- Se detiene si RestaurantSM ya existe. No elimina ni reemplaza ninguna base.
USE [master];
GO
IF DB_ID(N'RestaurantSM') IS NOT NULL THROW 50101, 'La base ya existe. Use ACTUALIZAR_EXISTENTE.sql.', 1;
GO
CREATE DATABASE [RestaurantSM];
GO
-- Esquema base portable. Solo para una base nueva y vacía.
USE [RestaurantSM];
GO
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
SET ANSI_PADDING ON;
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

CREATE PROCEDURE dbo.sp_Rol_Listar AS BEGIN SET NOCOUNT ON; SELECT idRol,nombre,descripcion FROM dbo.rol; END;
GO
CREATE PROCEDURE dbo.sp_Usuario_Listar AS BEGIN SET NOCOUNT ON; SELECT u.idUsuario,u.nombreUsuario,u.idRol,r.nombre NombreRol FROM dbo.usuario u JOIN dbo.rol r ON r.idRol=u.idRol; END;
GO
CREATE PROCEDURE dbo.sp_InsertarCliente AS THROW 50100, 'Aplique la migración 001 antes de usar el registro.', 1;
GO

GO
-- Datos de referencia y seis productos originales. Solo instalación nueva.
USE [RestaurantSM];
GO
SET XACT_ABORT ON;
BEGIN TRANSACTION;
SET IDENTITY_INSERT dbo.rol ON;
INSERT dbo.rol(idRol,nombre,descripcion) VALUES (1,N'Administrador',N'Permite modificar todo el sistema');
INSERT dbo.rol(idRol,nombre,descripcion) VALUES (2,N'Empleado',N'Funciones limitadas a lo basico');
INSERT dbo.rol(idRol,nombre,descripcion) VALUES (3,N'Cliente',N'Usuario común');
SET IDENTITY_INSERT dbo.rol OFF;
INSERT dbo.categoria(nombre,descripcion) VALUES (N'Entradas',N'Aperitivos y piqueos ligeros para comenzar');
INSERT dbo.categoria(nombre,descripcion) VALUES (N'Platos Fuertes',N'Especialidades de la casa, carnes, aves y mariscos');
INSERT dbo.categoria(nombre,descripcion) VALUES (N'Bebidas',N'Refrescos naturales, jugos, gaseosas y licores');
INSERT dbo.categoria(nombre,descripcion) VALUES (N'Postres',N'Pasteles, helados y postres tradicionales');
INSERT dbo.categoria(nombre,descripcion) VALUES (N'Sopas y Ensaladas',N'Opciones ligeras, caldos y ensaladas frescas');
INSERT dbo.categoria(nombre,descripcion) VALUES (N'Comida Rapida',N'Hamburguesas, pizzas, papas fritas y snacks');
INSERT dbo.producto(idCategoria,nombre,precio,descripcion,codigo,disponible) SELECT idCategoria,N'Dedos de Queso',120.00,N'Bastones de queso empanizados, acompañados de salsa de tomate.',N'PROD-001',1 FROM dbo.categoria WHERE nombre=N'Entradas';
INSERT dbo.producto(idCategoria,nombre,precio,descripcion,codigo,disponible) SELECT idCategoria,N'Lomito a la Plancha',320.50,N'Lomito de res a la plancha con arroz, vegetales y tostones.',N'PROD-002',1 FROM dbo.categoria WHERE nombre=N'Platos Fuertes';
INSERT dbo.producto(idCategoria,nombre,precio,descripcion,codigo,disponible) SELECT idCategoria,N'Jugo Natural de Naranja',45.00,N'Jugo de naranja recién preparado, servido frío en vaso.',N'PROD-003',1 FROM dbo.categoria WHERE nombre=N'Bebidas';
INSERT dbo.producto(idCategoria,nombre,precio,descripcion,codigo,disponible) SELECT idCategoria,N'Tres Leches',65.00,N'Porción de bizcocho bañado en tres leches con crema.',N'PROD-004',1 FROM dbo.categoria WHERE nombre=N'Postres';
INSERT dbo.producto(idCategoria,nombre,precio,descripcion,codigo,disponible) SELECT idCategoria,N'Sopa de Res',150.00,N'Caldo de res con verduras, acompañado de arroz y tortilla.',N'PROD-005',1 FROM dbo.categoria WHERE nombre=N'Sopas y Ensaladas';
INSERT dbo.producto(idCategoria,nombre,precio,descripcion,codigo,disponible) SELECT idCategoria,N'Hamburguesa con Tocino',210.00,N'Hamburguesa de res con tocino, queso y vegetales; incluye papas fritas.',N'PROD-006',1 FROM dbo.categoria WHERE nombre=N'Comida Rapida';
COMMIT TRANSACTION;
GO

-- Archivo: 001_corregir_rol_registro_cliente.sql
-- Bloque 1: las cuentas creadas desde el registro pertenecen al rol Cliente (3).
-- No cambia los roles de usuarios existentes.
USE [RestaurantSM];
GO

IF NOT EXISTS (SELECT 1 FROM dbo.rol WHERE idRol = 3 AND nombre = 'Cliente')
    THROW 50001, 'No existe el rol Cliente con idRol = 3. Revise los roles antes de continuar.', 1;
GO

SET ANSI_NULLS ON;
GO
SET QUOTED_IDENTIFIER ON;
GO

ALTER PROCEDURE [dbo].[sp_InsertarCliente]
    @PrimerNombre VARCHAR(80),
    @SegundoNombre VARCHAR(80),
    @PrimerApellido VARCHAR(80),
    @SegundoApellido VARCHAR(80),
    @Telefono VARCHAR(20),
    @Email VARCHAR(120),
    @Cedula VARCHAR(30),
    @Genero CHAR(1),
    @Usuario VARCHAR(50),
    @Contrasena VARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        BEGIN TRANSACTION;

        DECLARE @IdUsuarioGenerado INT;

        INSERT INTO usuario (nombreUsuario, contrasena, idRol)
        VALUES (@Usuario, @Contrasena, 3);

        SET @IdUsuarioGenerado = SCOPE_IDENTITY();

        INSERT INTO cliente (idUsuario, PNombreCl, SNombreCl, PApellidoCl, SApellidoCl, genero, cedula, telefono, email)
        VALUES (@IdUsuarioGenerado, @PrimerNombre, @SegundoNombre, @PrimerApellido, @SegundoApellido, @Genero, @Cedula, @Telefono, @Email);

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

GO

-- Archivo: 002_gestion_empleados.sql
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

GO

-- Archivo: 003_categorias_productos.sql
-- Catálogos: baja lógica y conservación de relaciones históricas.
USE [RestaurantSM];
GO
SET ANSI_NULLS ON;
GO
SET QUOTED_IDENTIFIER ON;
GO
IF COL_LENGTH('dbo.categoria', 'activa') IS NULL
    ALTER TABLE dbo.categoria ADD activa BIT NOT NULL CONSTRAINT DF_categoria_activa DEFAULT (1) WITH VALUES;
GO

CREATE OR ALTER PROCEDURE dbo.sp_Catalogo_ExigirAdministrador @IdAdministrador INT
AS
BEGIN
    SET NOCOUNT ON;
    IF NOT EXISTS (SELECT 1 FROM dbo.usuario WHERE idUsuario=@IdAdministrador AND idRol=1)
       OR EXISTS (SELECT 1 FROM dbo.empleado WHERE idUsuario=@IdAdministrador AND activo=0)
        THROW 50020, 'Solo un administrador activo puede modificar el catálogo.', 1;
END;
GO

CREATE OR ALTER PROCEDURE dbo.sp_Categoria_Listar
AS
BEGIN
    SET NOCOUNT ON;
    SELECT idCategoria,nombre,descripcion,activa FROM dbo.categoria ORDER BY nombre;
END;
GO

CREATE OR ALTER PROCEDURE dbo.sp_Categoria_Guardar
    @IdCategoria INT, @IdAdministrador INT, @Nombre VARCHAR(60), @Descripcion VARCHAR(200), @Activa BIT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    EXEC dbo.sp_Catalogo_ExigirAdministrador @IdAdministrador;
    IF NULLIF(LTRIM(RTRIM(@Nombre)), '') IS NULL
        THROW 50021, 'El nombre de la categoría es obligatorio.', 1;
    BEGIN TRY
        BEGIN TRANSACTION;
        IF @IdCategoria=0
            INSERT dbo.categoria (nombre,descripcion,activa) VALUES (@Nombre,@Descripcion,@Activa);
        ELSE
        BEGIN
            IF NOT EXISTS (SELECT 1 FROM dbo.categoria WITH (UPDLOCK,HOLDLOCK) WHERE idCategoria=@IdCategoria)
                THROW 50022, 'La categoría seleccionada ya no existe.', 1;
            IF @Activa=0 AND EXISTS (SELECT 1 FROM dbo.producto WHERE idCategoria=@IdCategoria AND disponible=1)
                THROW 50023, 'Primero marque como no disponibles los productos de esta categoría.', 1;
            UPDATE dbo.categoria SET nombre=@Nombre,descripcion=@Descripcion,activa=@Activa WHERE idCategoria=@IdCategoria;
        END;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT>0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

CREATE OR ALTER PROCEDURE dbo.sp_Categoria_Desactivar @IdCategoria INT, @IdAdministrador INT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    EXEC dbo.sp_Catalogo_ExigirAdministrador @IdAdministrador;
    BEGIN TRY
        BEGIN TRANSACTION;
        IF NOT EXISTS (SELECT 1 FROM dbo.categoria WITH (UPDLOCK,HOLDLOCK) WHERE idCategoria=@IdCategoria)
            THROW 50022, 'La categoría seleccionada ya no existe.', 1;
        IF EXISTS (SELECT 1 FROM dbo.producto WHERE idCategoria=@IdCategoria AND disponible=1)
            THROW 50023, 'Primero marque como no disponibles los productos de esta categoría.', 1;
        UPDATE dbo.categoria SET activa=0 WHERE idCategoria=@IdCategoria;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT>0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

CREATE OR ALTER PROCEDURE dbo.sp_Subcategoria_Listar
AS
BEGIN
    SET NOCOUNT ON;
    SELECT idSubcategoria,idCategoria,nombre FROM dbo.subcategoria ORDER BY nombre;
END;
GO

CREATE OR ALTER PROCEDURE dbo.sp_Producto_Listar
AS
BEGIN
    SET NOCOUNT ON;
    SELECT p.idProducto,p.idCategoria,p.idSubcategoria,c.nombre AS CategoriaNombre,
           s.nombre AS SubcategoriaNombre,p.nombre,p.precio,p.descripcion,p.codigo,p.disponible
    FROM dbo.producto p
    INNER JOIN dbo.categoria c ON c.idCategoria=p.idCategoria
    LEFT JOIN dbo.subcategoria s ON s.idSubcategoria=p.idSubcategoria
    ORDER BY p.nombre;
END;
GO

CREATE OR ALTER PROCEDURE dbo.sp_Producto_Guardar
    @IdProducto INT, @IdAdministrador INT, @IdCategoria INT, @IdSubcategoria INT,
    @Nombre VARCHAR(100), @Precio DECIMAL(10,2), @Descripcion VARCHAR(250), @Codigo VARCHAR(20), @Disponible BIT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    EXEC dbo.sp_Catalogo_ExigirAdministrador @IdAdministrador;
    IF NULLIF(LTRIM(RTRIM(@Nombre)), '') IS NULL OR NULLIF(LTRIM(RTRIM(@Codigo)), '') IS NULL OR @Precio IS NULL OR @Precio<=0
        THROW 50024, 'Nombre, código y precio positivo son obligatorios.', 1;
    BEGIN TRY
        BEGIN TRANSACTION;
        IF NOT EXISTS (SELECT 1 FROM dbo.categoria WITH (UPDLOCK,HOLDLOCK) WHERE idCategoria=@IdCategoria)
            THROW 50022, 'La categoría seleccionada ya no existe.', 1;
        IF @Disponible=1 AND EXISTS (SELECT 1 FROM dbo.categoria WHERE idCategoria=@IdCategoria AND activa=0)
            THROW 50025, 'Active la categoría antes de ofrecer este producto.', 1;
        IF @IdSubcategoria IS NOT NULL AND NOT EXISTS
            (SELECT 1 FROM dbo.subcategoria WITH (HOLDLOCK) WHERE idSubcategoria=@IdSubcategoria AND idCategoria=@IdCategoria)
            THROW 50026, 'La subcategoría no pertenece a la categoría seleccionada.', 1;
        IF @IdProducto=0
            INSERT dbo.producto (idCategoria,idSubcategoria,nombre,precio,descripcion,codigo,disponible)
            VALUES (@IdCategoria,@IdSubcategoria,@Nombre,@Precio,@Descripcion,@Codigo,@Disponible);
        ELSE
        BEGIN
            UPDATE dbo.producto SET idCategoria=@IdCategoria,idSubcategoria=@IdSubcategoria,nombre=@Nombre,
                precio=@Precio,descripcion=@Descripcion,codigo=@Codigo,disponible=@Disponible WHERE idProducto=@IdProducto;
            IF @@ROWCOUNT=0 THROW 50027, 'El producto seleccionado ya no existe.', 1;
        END;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT>0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

CREATE OR ALTER PROCEDURE dbo.sp_Producto_Desactivar @IdProducto INT, @IdAdministrador INT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    EXEC dbo.sp_Catalogo_ExigirAdministrador @IdAdministrador;
    UPDATE dbo.producto SET disponible=0 WHERE idProducto=@IdProducto;
    IF @@ROWCOUNT=0 THROW 50027, 'El producto seleccionado ya no existe.', 1;
END;
GO

GO

-- Archivo: 004_catalogo_demostracion.sql
-- Catálogo de demostración. Conserva precios e IDs existentes.
-- Puede repetirse: identifica productos por código y subcategorías por categoría/nombre.
SET NOCOUNT ON;
SET XACT_ABORT ON;
BEGIN TRY
    BEGIN TRANSACTION;
    DECLARE @Menu TABLE (
        Categoria varchar(60), Subcategoria varchar(60), Codigo varchar(20),
        Nombre varchar(100), Precio decimal(10,2), Descripcion varchar(250));
    INSERT @Menu VALUES
    ('Entradas','Entradas calientes','PROD-001','Dedos de Queso',120.00,'Bastones de queso empanizados, acompañados de salsa de tomate.'),
    ('Entradas','Entradas calientes','MENU-007','Tostones con queso',95.00,'Plátano verde crujiente con queso rallado y ensalada de repollo.'),
    ('Entradas','Entradas frías','MENU-008','Ceviche de pescado',180.00,'Pescado marinado con limón, cebolla y cilantro; acompañado de tostadas.'),
    ('Entradas','Entradas frías','MENU-009','Bruschettas de tomate',85.00,'Pan tostado con tomate fresco, albahaca y aceite de oliva.'),
    ('Platos Fuertes','Carnes','PROD-002','Lomito a la Plancha',320.50,'Lomito de res a la plancha con arroz, vegetales y tostones.'),
    ('Platos Fuertes','Carnes','MENU-010','Churrasco de res',295.00,'Corte de res a la parrilla con chimichurri, arroz y ensalada.'),
    ('Platos Fuertes','Aves','MENU-011','Pollo a la plancha',220.00,'Pechuga de pollo con vegetales salteados y arroz.'),
    ('Platos Fuertes','Aves','MENU-012','Pollo en salsa de hongos',245.00,'Pechuga de pollo en salsa cremosa de hongos con puré de papa.'),
    ('Bebidas','Refrescos naturales','PROD-003','Jugo Natural de Naranja',45.00,'Jugo de naranja recién preparado, servido frío en vaso.'),
    ('Bebidas','Refrescos naturales','MENU-013','Refresco de maracuyá',40.00,'Refresco natural de maracuyá preparado al momento.'),
    ('Bebidas','Gaseosas','MENU-014','Gaseosa de cola',35.00,'Gaseosa de cola personal de 355 ml.'),
    ('Bebidas','Gaseosas','MENU-015','Gaseosa de limón',35.00,'Gaseosa de limón personal de 355 ml.'),
    ('Postres','Pasteles','PROD-004','Tres Leches',65.00,'Porción de bizcocho bañado en tres leches con crema.'),
    ('Postres','Pasteles','MENU-016','Pastel de chocolate',75.00,'Porción de pastel de chocolate con cobertura cremosa.'),
    ('Postres','Helados','MENU-017','Helado de vainilla',55.00,'Dos bolas de helado de vainilla con salsa de chocolate.'),
    ('Postres','Helados','MENU-018','Helado de fresa',55.00,'Dos bolas de helado de fresa con trozos de fruta.'),
    ('Sopas y Ensaladas','Sopas','PROD-005','Sopa de Res',150.00,'Caldo de res con verduras, acompañado de arroz y tortilla.'),
    ('Sopas y Ensaladas','Sopas','MENU-019','Sopa de pollo',130.00,'Caldo de pollo con verduras frescas, arroz y tortilla.'),
    ('Sopas y Ensaladas','Ensaladas','MENU-020','Ensalada de la casa',110.00,'Lechuga, tomate, pepino y zanahoria con aderezo de la casa.'),
    ('Sopas y Ensaladas','Ensaladas','MENU-021','Ensalada con pollo',165.00,'Vegetales frescos con pechuga a la plancha y aderezo de yogur.'),
    ('Comida Rapida','Hamburguesas','PROD-006','Hamburguesa con Tocino',210.00,'Hamburguesa de res con tocino, queso y vegetales; incluye papas fritas.'),
    ('Comida Rapida','Hamburguesas','MENU-022','Hamburguesa clásica',175.00,'Hamburguesa de res con queso, lechuga y tomate; incluye papas fritas.'),
    ('Comida Rapida','Pizzas','MENU-023','Pizza personal margarita',160.00,'Pizza personal con salsa de tomate, mozzarella y albahaca.'),
    ('Comida Rapida','Pizzas','MENU-024','Pizza personal de pepperoni',185.00,'Pizza personal con mozzarella y pepperoni.');

    IF EXISTS (SELECT 1 FROM @Menu m WHERE NOT EXISTS
        (SELECT 1 FROM dbo.categoria c WHERE c.nombre=m.Categoria AND c.activa=1))
        THROW 50040, 'Falta una categoría activa requerida para el catálogo.', 1;
    IF EXISTS (SELECT 1 FROM @Menu m JOIN dbo.producto p ON p.codigo=m.Codigo
        JOIN dbo.categoria c ON c.idCategoria=p.idCategoria
        WHERE p.nombre<>m.Nombre OR c.nombre<>m.Categoria)
        THROW 50041, 'Un código del catálogo ya pertenece a otro producto. No se realizaron cambios.', 1;
    IF EXISTS (SELECT 1 FROM @Menu m JOIN dbo.producto p ON p.nombre=m.Nombre WHERE p.codigo<>m.Codigo)
        THROW 50042, 'Un nombre del catálogo ya tiene otro código. No se realizaron cambios.', 1;

    INSERT dbo.subcategoria (idCategoria,nombre)
    SELECT DISTINCT c.idCategoria,m.Subcategoria FROM @Menu m
    JOIN dbo.categoria c ON c.nombre=m.Categoria
    WHERE NOT EXISTS (SELECT 1 FROM dbo.subcategoria s WITH (UPDLOCK,HOLDLOCK)
        WHERE s.idCategoria=c.idCategoria AND s.nombre=m.Subcategoria);

    -- Completa solo información faltante; respeta modificaciones posteriores.
    UPDATE p SET idSubcategoria=COALESCE(p.idSubcategoria,s.idSubcategoria),
        descripcion=CASE WHEN NULLIF(LTRIM(RTRIM(p.descripcion)),'') IS NULL THEN m.Descripcion ELSE p.descripcion END
    FROM dbo.producto p JOIN @Menu m ON m.Codigo=p.codigo
    JOIN dbo.categoria c ON c.nombre=m.Categoria
    JOIN dbo.subcategoria s ON s.idCategoria=c.idCategoria AND s.nombre=m.Subcategoria;

    INSERT dbo.producto (idCategoria,idSubcategoria,nombre,precio,descripcion,codigo,disponible)
    SELECT c.idCategoria,s.idSubcategoria,m.Nombre,m.Precio,m.Descripcion,m.Codigo,1
    FROM @Menu m JOIN dbo.categoria c ON c.nombre=m.Categoria
    JOIN dbo.subcategoria s ON s.idCategoria=c.idCategoria AND s.nombre=m.Subcategoria
    WHERE NOT EXISTS (SELECT 1 FROM dbo.producto p WITH (UPDLOCK,HOLDLOCK) WHERE p.codigo=m.Codigo);

    COMMIT TRANSACTION;
END TRY
BEGIN CATCH
    IF @@TRANCOUNT>0 ROLLBACK TRANSACTION;
    THROW;
END CATCH;

GO
USE [RestaurantSM];
GO
SET NOCOUNT ON;
IF COL_LENGTH('dbo.categoria','activa') IS NULL
    THROW 50120, 'Falta categoria.activa. Aplique las migraciones.', 1;
IF EXISTS (SELECT 1 FROM (VALUES(1,'Administrador'),(2,'Empleado'),(3,'Cliente')) r(id,nombre)
    WHERE NOT EXISTS (SELECT 1 FROM dbo.rol WHERE idRol=r.id AND nombre=r.nombre))
    THROW 50121, 'Los roles no coinciden con los esperados por el proyecto.', 1;
IF EXISTS (SELECT 1 FROM dbo.producto p JOIN dbo.subcategoria s ON s.idSubcategoria=p.idSubcategoria
    WHERE p.idCategoria<>s.idCategoria)
    THROW 50122, 'Existen productos con una subcategoría de otra categoría.', 1;
IF EXISTS (SELECT 1 FROM (VALUES
    ('sp_InsertarCliente'),('sp_Usuario_ValidarLogin'),('sp_Rol_Listar'),('sp_Usuario_Listar'),
    ('sp_Empleado_Listar'),('sp_Empleado_Insertar'),('sp_Empleado_Actualizar'),('sp_Empleado_Desactivar'),
    ('sp_Catalogo_ExigirAdministrador'),('sp_Categoria_Listar'),('sp_Categoria_Guardar'),('sp_Categoria_Desactivar'),
    ('sp_Subcategoria_Listar'),('sp_Producto_Listar'),('sp_Producto_Guardar'),('sp_Producto_Desactivar')) v(nombre)
    WHERE OBJECT_ID('dbo.'+v.nombre,'P') IS NULL)
    THROW 50123, 'Faltan procedimientos requeridos por la aplicación.', 1;
SELECT 'Estructura y relaciones básicas verificadas' AS Resultado;
SELECT idRol,nombre FROM dbo.rol ORDER BY idRol;
SELECT (SELECT COUNT(*) FROM dbo.categoria) Categorias,
       (SELECT COUNT(*) FROM dbo.subcategoria) Subcategorias,
       (SELECT COUNT(*) FROM dbo.producto) Productos;
SELECT c.nombre Categoria,s.nombre Subcategoria,p.codigo,p.nombre,p.precio,p.disponible
FROM dbo.producto p JOIN dbo.categoria c ON c.idCategoria=p.idCategoria
LEFT JOIN dbo.subcategoria s ON s.idSubcategoria=p.idSubcategoria
ORDER BY c.nombre,s.nombre,p.nombre;
GO
