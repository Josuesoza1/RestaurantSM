-- RestaurantSM local con datos de demostración.
-- Cuentas ficticias de prueba: admin_demo / DemoAdmin2026! y caja_demo / DemoCaja2026!.
-- Ejecute el archivo completo en SSMS en la misma instancia que usará la aplicación.
-- Crea RestaurantSM. Si ya existe una base con ese nombre, se detiene.
USE [master];
GO
IF DB_ID(N'RestaurantSM') IS NOT NULL
BEGIN
    RAISERROR(N'Ya existe RestaurantSM. Use la actualización si tiene el Portable anterior.', 16, 1);
    SET NOEXEC ON;
END;
GO
CREATE DATABASE [RestaurantSM] COLLATE SQL_Latin1_General_CP1_CI_AS;
GO
IF DB_ID(N'RestaurantSM') IS NULL
BEGIN
    RAISERROR(N'No se pudo crear RestaurantSM en esta instancia.', 16, 1);
    SET NOEXEC ON;
END;
GO
USE [RestaurantSM];
GO
SET XACT_ABORT ON;
SET NOCOUNT ON;
BEGIN TRANSACTION;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[persona](
	[idPersona] [int] IDENTITY(1,1) NOT NULL,
	[primerNombre] [varchar](80) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[segundoNombre] [varchar](80) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[primerApellido] [varchar](80) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[segundoApellido] [varchar](80) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[genero] [char](1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[cedula] [varchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[telefono] [varchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[email] [varchar](120) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_persona] PRIMARY KEY CLUSTERED 
(
	[idPersona] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF)
)

GO
SET ANSI_PADDING OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[cliente](
	[idCliente] [int] IDENTITY(1,1) NOT NULL,
	[idUsuario] [int] NULL,
	[activo] [bit] NOT NULL,
	[idPersona] [int] NOT NULL,
 CONSTRAINT [PK_cliente] PRIMARY KEY CLUSTERED 
(
	[idCliente] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF)
)

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Las vistas reunen Persona y su perfil para las consultas existentes.
CREATE   VIEW dbo.vw_Cliente AS
SELECT c.idCliente,c.idUsuario,c.idPersona,c.activo,
 p.primerNombre AS PNombreCl,p.segundoNombre AS SNombreCl,p.primerApellido AS PApellidoCl,
 p.segundoApellido AS SApellidoCl,p.genero,p.cedula,p.telefono,p.email
FROM dbo.cliente c JOIN dbo.persona p ON p.idPersona=c.idPersona;

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[empleado](
	[idEmpleado] [int] IDENTITY(1,1) NOT NULL,
	[idUsuario] [int] NULL,
	[cargo] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[activo] [bit] NOT NULL,
	[idPersona] [int] NOT NULL,
 CONSTRAINT [PK_empleado] PRIMARY KEY CLUSTERED 
(
	[idEmpleado] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF)
)

GO
SET ANSI_PADDING OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   VIEW dbo.vw_Empleado AS
SELECT e.idEmpleado,e.idUsuario,e.idPersona,e.cargo,e.activo,
 p.primerNombre AS pNombreEmp,p.segundoNombre AS sNombreEmp,p.primerApellido AS pApellidoEmp,
 p.segundoApellido AS sApellidoEmp,p.genero,p.cedula,p.telefono,p.email
FROM dbo.empleado e JOIN dbo.persona p ON p.idPersona=e.idPersona;

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[rol](
	[idRol] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[descripcion] [varchar](150) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_idRol] PRIMARY KEY CLUSTERED 
(
	[idRol] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF),
UNIQUE NONCLUSTERED 
(
	[nombre] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF)
)

GO
SET ANSI_PADDING OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[usuario](
	[idUsuario] [int] IDENTITY(1,1) NOT NULL,
	[nombreUsuario] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[contrasena] [varchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[idRol] [int] NOT NULL,
	[idPersona] [int] NOT NULL,
 CONSTRAINT [PK_usuario] PRIMARY KEY CLUSTERED 
(
	[idUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF),
 CONSTRAINT [UQ_usuario_nombre] UNIQUE NONCLUSTERED 
(
	[nombreUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF)
)

GO
SET ANSI_PADDING OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   VIEW dbo.vw_UsuarioAccesos
AS
SELECT u.idUsuario, u.idPersona, u.nombreUsuario, u.idRol, r.nombre AS NombreRol, e.cargo AS Cargo,
    CAST(CASE
        WHEN u.idRol = 1 AND (e.idEmpleado IS NULL OR e.activo = 1) THEN 1
        WHEN u.idRol = 2 AND e.activo = 1
            AND UPPER(LTRIM(RTRIM(e.cargo))) IN ('DESPACHADOR', 'MESERO', 'CAJERO') THEN 1
        ELSE 0 END AS bit) AS AccesoPersonal
FROM dbo.usuario u
JOIN dbo.rol r ON r.idRol = u.idRol
LEFT JOIN dbo.empleado e ON e.idUsuario = u.idUsuario;
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[configuracionRestaurante](
	[id] [int] NOT NULL,
	[nombre] [nvarchar](120) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[moneda] [varchar](5) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[tasaImpuesto] [decimal](5, 2) NOT NULL,
	[tarifaEnvio] [decimal](10, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF)
)

GO
SET ANSI_PADDING OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[mesa](
	[idMesa] [int] IDENTITY(1,1) NOT NULL,
	[numero] [int] NOT NULL,
	[capacidad] [int] NOT NULL,
	[estado] [varchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[activa] [bit] NOT NULL,
 CONSTRAINT [PK_IdMesa] PRIMARY KEY CLUSTERED 
(
	[idMesa] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF),
UNIQUE NONCLUSTERED 
(
	[numero] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF)
)

GO
SET ANSI_PADDING OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[categoria](
	[idCategoria] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](60) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[descripcion] [varchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[activa] [bit] NOT NULL,
 CONSTRAINT [PK_IdCategoria] PRIMARY KEY CLUSTERED 
(
	[idCategoria] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF),
UNIQUE NONCLUSTERED 
(
	[nombre] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF)
)

GO
SET ANSI_PADDING OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[origenPedido](
	[idOrigenPedido] [int] NOT NULL,
	[nombre] [varchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[idOrigenPedido] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF),
UNIQUE NONCLUSTERED 
(
	[nombre] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF)
)

GO
SET ANSI_PADDING OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tipoEntrega](
	[idTipoEntrega] [int] NOT NULL,
	[nombre] [varchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[idTipoEntrega] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF),
UNIQUE NONCLUSTERED 
(
	[nombre] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF)
)

GO
SET ANSI_PADDING OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[subcategoria](
	[idSubcategoria] [int] IDENTITY(1,1) NOT NULL,
	[idCategoria] [int] NOT NULL,
	[nombre] [varchar](60) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_IdSubcategoria] PRIMARY KEY CLUSTERED 
(
	[idSubcategoria] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF)
)

GO
SET ANSI_PADDING OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[producto](
	[idProducto] [int] IDENTITY(1,1) NOT NULL,
	[idCategoria] [int] NOT NULL,
	[nombre] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[precio] [decimal](10, 2) NOT NULL,
	[descripcion] [varchar](250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[codigo] [varchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[disponible] [bit] NOT NULL,
	[idSubcategoria] [int] NULL,
 CONSTRAINT [PK_idProducto] PRIMARY KEY CLUSTERED 
(
	[idProducto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF),
UNIQUE NONCLUSTERED 
(
	[codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF),
UNIQUE NONCLUSTERED 
(
	[nombre] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF)
)

GO
SET ANSI_PADDING OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[pedido](
	[idPedido] [int] IDENTITY(1,1) NOT NULL,
	[idCliente] [int] NULL,
	[idMesa] [int] NULL,
	[idEmpleado] [int] NULL,
	[idEmpleadoRepartidor] [int] NULL,
	[direccion] [varchar](250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[referencia] [varchar](150) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[telefonoDeContacto] [varchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[fechaHora] [datetime] NOT NULL,
	[estado] [varchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[costoEnvio] [decimal](10, 2) NOT NULL,
	[subtotal] [decimal](10, 2) NOT NULL,
	[total] [decimal](10, 2) NOT NULL,
	[esDelivery] [bit] NOT NULL,
	[solicitud] [uniqueidentifier] NULL,
	[idOrigenPedido] [int] NOT NULL,
	[idTipoEntrega] [int] NOT NULL,
	[idUsuarioRegistro] [int] NULL,
 CONSTRAINT [PK_pedido] PRIMARY KEY CLUSTERED 
(
	[idPedido] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF)
)

GO
SET ANSI_PADDING OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[inventarioProducto](
	[idProducto] [int] NOT NULL,
	[stockActual] [int] NOT NULL,
	[stockMinimo] [int] NOT NULL,
	[activo] [bit] NOT NULL,
	[version] [timestamp] NOT NULL,
	[revision] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[idProducto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF)
)

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[reservacion](
	[idReservacion] [int] IDENTITY(1,1) NOT NULL,
	[idCliente] [int] NOT NULL,
	[idMesa] [int] NOT NULL,
	[fechaHoraReserva] [datetime] NOT NULL,
	[cantidadPersonas] [int] NOT NULL,
	[estado] [varchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_idReservacion] PRIMARY KEY CLUSTERED 
(
	[idReservacion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF)
)

GO
SET ANSI_PADDING OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[movimientoInventario](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[idProducto] [int] NOT NULL,
	[producto] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[anterior] [int] NOT NULL,
	[nuevo] [int] NOT NULL,
	[diferencia] [int] NOT NULL,
	[idUsuario] [int] NULL,
	[usuario] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[motivo] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[fecha] [datetime2](7) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF)
)

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[detallePedido](
	[idDetalle] [int] IDENTITY(1,1) NOT NULL,
	[idPedido] [int] NOT NULL,
	[idProducto] [int] NOT NULL,
	[cantidad] [int] NOT NULL,
	[notasCocina] [varchar](180) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[precioUnitario] [decimal](10, 2) NOT NULL,
	[subtotal] [decimal](10, 2) NOT NULL,
	[total] [decimal](10, 2) NOT NULL,
	[controlInventario] [bit] NOT NULL,
 CONSTRAINT [PK_idDetalle] PRIMARY KEY CLUSTERED 
(
	[idDetalle] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF),
UNIQUE NONCLUSTERED 
(
	[idPedido] ASC,
	[idProducto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF)
)

GO
SET ANSI_PADDING OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[historialPedido](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[idPedido] [int] NOT NULL,
	[estadoAnterior] [varchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[estadoNuevo] [varchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[idUsuario] [int] NOT NULL,
	[fecha] [datetime2](7) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF)
)

GO
SET ANSI_PADDING OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[factura](
	[idFactura] [int] IDENTITY(1,1) NOT NULL,
	[idPedido] [int] NOT NULL,
	[numeroFactura] [varchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[fechaEmision] [datetime] NOT NULL,
	[estado] [varchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[subtotal] [decimal](10, 2) NOT NULL,
	[costoEnvio] [decimal](10, 2) NOT NULL,
	[descuento] [decimal](10, 2) NOT NULL,
	[impuestos] [decimal](10, 2) NOT NULL,
	[propina] [decimal](10, 2) NOT NULL,
	[total] [decimal](10, 2) NOT NULL,
	[clienteNombre] [nvarchar](330) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[clienteCedula] [varchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[empleadoNombre] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[tasaImpuesto] [decimal](5, 2) NULL,
	[restauranteNombre] [nvarchar](120) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[moneda] [varchar](5) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[mesaNumero] [int] NULL,
	[tipoPedido] [varchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[origenPedido] [varchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[registradoPor] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[atendidoPor] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[direccionEntrega] [varchar](250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[referenciaEntrega] [varchar](150) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[telefonoContacto] [varchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[tipoEntrega] [varchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_idFactura] PRIMARY KEY CLUSTERED 
(
	[idFactura] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF),
UNIQUE NONCLUSTERED 
(
	[numeroFactura] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF),
UNIQUE NONCLUSTERED 
(
	[idPedido] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF)
)

GO
SET ANSI_PADDING OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[pago](
	[idPago] [int] IDENTITY(1,1) NOT NULL,
	[idFactura] [int] NOT NULL,
	[metodoDePago] [varchar](40) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[monto] [decimal](10, 2) NOT NULL,
	[fechaPago] [datetime] NOT NULL,
	[recibido] [decimal](10, 2) NULL,
	[cambio] [decimal](10, 2) NULL,
	[solicitud] [uniqueidentifier] NULL,
	[idUsuario] [int] NULL,
	[notas] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_idPago] PRIMARY KEY CLUSTERED 
(
	[idPago] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF)
)

GO
SET ANSI_PADDING OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[facturaDetalle](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[idFactura] [int] NOT NULL,
	[idProducto] [int] NOT NULL,
	[producto] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[cantidad] [int] NOT NULL,
	[precio] [decimal](10, 2) NOT NULL,
	[subtotal] [decimal](10, 2) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF)
)

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[anulacionVenta](
	[idFactura] [int] NOT NULL,
	[idUsuario] [int] NOT NULL,
	[fecha] [datetime2](7) NOT NULL,
	[motivo] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[repusoInventario] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[idFactura] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF)
)

GO
SET ANSI_PADDING ON

GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_persona_cedula] ON [dbo].[persona]
(
	[cedula] ASC
)
WHERE ([cedula] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF)
GO
SET ANSI_PADDING ON

GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_persona_email] ON [dbo].[persona]
(
	[email] ASC
)
WHERE ([email] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF)
GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_subcategoria_categoria] ON [dbo].[subcategoria]
(
	[idSubcategoria] ASC,
	[idCategoria] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF)
GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_usuario_cuenta_persona] ON [dbo].[usuario]
(
	[idUsuario] ASC,
	[idPersona] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF)
GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_empleado_persona] ON [dbo].[empleado]
(
	[idPersona] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF)
GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_empleado_usuario] ON [dbo].[empleado]
(
	[idUsuario] ASC
)
WHERE ([idUsuario] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF)
GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_cliente_persona] ON [dbo].[cliente]
(
	[idPersona] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF)
GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_cliente_usuario] ON [dbo].[cliente]
(
	[idUsuario] ASC
)
WHERE ([idUsuario] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF)
GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_pedido_solicitud] ON [dbo].[pedido]
(
	[solicitud] ASC
)
WHERE ([solicitud] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF)
GO
CREATE UNIQUE NONCLUSTERED INDEX [UX_pago_solicitud_operacion] ON [dbo].[pago]
(
	[solicitud] ASC
)
WHERE ([solicitud] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF)
GO
ALTER TABLE [dbo].[mesa] ADD  DEFAULT ('Disponible') FOR [estado]
GO
ALTER TABLE [dbo].[mesa] ADD  CONSTRAINT [DF_mesa_activa]  DEFAULT ((1)) FOR [activa]
GO
ALTER TABLE [dbo].[categoria] ADD  CONSTRAINT [DF_categoria_activa]  DEFAULT ((1)) FOR [activa]
GO
ALTER TABLE [dbo].[empleado] ADD  DEFAULT ((1)) FOR [activo]
GO
ALTER TABLE [dbo].[producto] ADD  DEFAULT ((1)) FOR [disponible]
GO
ALTER TABLE [dbo].[cliente] ADD  CONSTRAINT [DF_cliente_activo]  DEFAULT ((1)) FOR [activo]
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
ALTER TABLE [dbo].[pedido] ADD  CONSTRAINT [DF_pedido_origen]  DEFAULT ((0)) FOR [idOrigenPedido]
GO
ALTER TABLE [dbo].[pedido] ADD  CONSTRAINT [DF_pedido_entrega]  DEFAULT ((0)) FOR [idTipoEntrega]
GO
ALTER TABLE [dbo].[inventarioProducto] ADD  DEFAULT ((1)) FOR [activo]
GO
ALTER TABLE [dbo].[inventarioProducto] ADD  DEFAULT ((1)) FOR [revision]
GO
ALTER TABLE [dbo].[reservacion] ADD  DEFAULT ('Pendiente') FOR [estado]
GO
ALTER TABLE [dbo].[movimientoInventario] ADD  DEFAULT (sysdatetime()) FOR [fecha]
GO
ALTER TABLE [dbo].[detallePedido] ADD  CONSTRAINT [DF_detalle_control]  DEFAULT ((0)) FOR [controlInventario]
GO
ALTER TABLE [dbo].[historialPedido] ADD  DEFAULT (sysdatetime()) FOR [fecha]
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
ALTER TABLE [dbo].[pago] ADD  DEFAULT (getdate()) FOR [fechaPago]
GO
ALTER TABLE [dbo].[anulacionVenta] ADD  DEFAULT (sysdatetime()) FOR [fecha]
GO
ALTER TABLE [dbo].[subcategoria]  WITH CHECK ADD  CONSTRAINT [FK_subcategoria_categoria] FOREIGN KEY([idCategoria])
REFERENCES [dbo].[categoria] ([idCategoria])
GO
ALTER TABLE [dbo].[subcategoria] CHECK CONSTRAINT [FK_subcategoria_categoria]
GO
ALTER TABLE [dbo].[usuario]  WITH CHECK ADD  CONSTRAINT [FK_usuario_persona] FOREIGN KEY([idPersona])
REFERENCES [dbo].[persona] ([idPersona])
GO
ALTER TABLE [dbo].[usuario] CHECK CONSTRAINT [FK_usuario_persona]
GO
ALTER TABLE [dbo].[usuario]  WITH CHECK ADD  CONSTRAINT [FK_usuario_rol] FOREIGN KEY([idRol])
REFERENCES [dbo].[rol] ([idRol])
GO
ALTER TABLE [dbo].[usuario] CHECK CONSTRAINT [FK_usuario_rol]
GO
ALTER TABLE [dbo].[empleado]  WITH CHECK ADD  CONSTRAINT [FK_empleado_cuenta_persona] FOREIGN KEY([idUsuario], [idPersona])
REFERENCES [dbo].[usuario] ([idUsuario], [idPersona])
GO
ALTER TABLE [dbo].[empleado] CHECK CONSTRAINT [FK_empleado_cuenta_persona]
GO
ALTER TABLE [dbo].[empleado]  WITH CHECK ADD  CONSTRAINT [FK_empleado_persona] FOREIGN KEY([idPersona])
REFERENCES [dbo].[persona] ([idPersona])
GO
ALTER TABLE [dbo].[empleado] CHECK CONSTRAINT [FK_empleado_persona]
GO
ALTER TABLE [dbo].[producto]  WITH CHECK ADD  CONSTRAINT [FK_IdCategoriaProd] FOREIGN KEY([idCategoria])
REFERENCES [dbo].[categoria] ([idCategoria])
GO
ALTER TABLE [dbo].[producto] CHECK CONSTRAINT [FK_IdCategoriaProd]
GO
ALTER TABLE [dbo].[producto]  WITH CHECK ADD  CONSTRAINT [FK_producto_subcategoria_categoria] FOREIGN KEY([idSubcategoria], [idCategoria])
REFERENCES [dbo].[subcategoria] ([idSubcategoria], [idCategoria])
GO
ALTER TABLE [dbo].[producto] CHECK CONSTRAINT [FK_producto_subcategoria_categoria]
GO
ALTER TABLE [dbo].[cliente]  WITH CHECK ADD  CONSTRAINT [FK_cliente_cuenta_persona] FOREIGN KEY([idUsuario], [idPersona])
REFERENCES [dbo].[usuario] ([idUsuario], [idPersona])
GO
ALTER TABLE [dbo].[cliente] CHECK CONSTRAINT [FK_cliente_cuenta_persona]
GO
ALTER TABLE [dbo].[cliente]  WITH CHECK ADD  CONSTRAINT [FK_cliente_persona] FOREIGN KEY([idPersona])
REFERENCES [dbo].[persona] ([idPersona])
GO
ALTER TABLE [dbo].[cliente] CHECK CONSTRAINT [FK_cliente_persona]
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
ALTER TABLE [dbo].[pedido]  WITH CHECK ADD  CONSTRAINT [FK_pedido_entrega] FOREIGN KEY([idTipoEntrega])
REFERENCES [dbo].[tipoEntrega] ([idTipoEntrega])
GO
ALTER TABLE [dbo].[pedido] CHECK CONSTRAINT [FK_pedido_entrega]
GO
ALTER TABLE [dbo].[pedido]  WITH CHECK ADD  CONSTRAINT [FK_pedido_mesa] FOREIGN KEY([idMesa])
REFERENCES [dbo].[mesa] ([idMesa])
GO
ALTER TABLE [dbo].[pedido] CHECK CONSTRAINT [FK_pedido_mesa]
GO
ALTER TABLE [dbo].[pedido]  WITH CHECK ADD  CONSTRAINT [FK_pedido_origen] FOREIGN KEY([idOrigenPedido])
REFERENCES [dbo].[origenPedido] ([idOrigenPedido])
GO
ALTER TABLE [dbo].[pedido] CHECK CONSTRAINT [FK_pedido_origen]
GO
ALTER TABLE [dbo].[pedido]  WITH CHECK ADD  CONSTRAINT [FK_pedido_registro] FOREIGN KEY([idUsuarioRegistro])
REFERENCES [dbo].[usuario] ([idUsuario])
GO
ALTER TABLE [dbo].[pedido] CHECK CONSTRAINT [FK_pedido_registro]
GO
ALTER TABLE [dbo].[pedido]  WITH CHECK ADD  CONSTRAINT [FK_pedido_repartidor] FOREIGN KEY([idEmpleadoRepartidor])
REFERENCES [dbo].[empleado] ([idEmpleado])
GO
ALTER TABLE [dbo].[pedido] CHECK CONSTRAINT [FK_pedido_repartidor]
GO
ALTER TABLE [dbo].[inventarioProducto]  WITH CHECK ADD FOREIGN KEY([idProducto])
REFERENCES [dbo].[producto] ([idProducto])
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
ALTER TABLE [dbo].[movimientoInventario]  WITH CHECK ADD  CONSTRAINT [FK_movimientoInventario_producto] FOREIGN KEY([idProducto])
REFERENCES [dbo].[producto] ([idProducto])
GO
ALTER TABLE [dbo].[movimientoInventario] CHECK CONSTRAINT [FK_movimientoInventario_producto]
GO
ALTER TABLE [dbo].[movimientoInventario]  WITH CHECK ADD  CONSTRAINT [FK_movimientoInventario_usuario] FOREIGN KEY([idUsuario])
REFERENCES [dbo].[usuario] ([idUsuario])
GO
ALTER TABLE [dbo].[movimientoInventario] CHECK CONSTRAINT [FK_movimientoInventario_usuario]
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
ALTER TABLE [dbo].[historialPedido]  WITH CHECK ADD FOREIGN KEY([idPedido])
REFERENCES [dbo].[pedido] ([idPedido])
GO
ALTER TABLE [dbo].[historialPedido]  WITH CHECK ADD FOREIGN KEY([idUsuario])
REFERENCES [dbo].[usuario] ([idUsuario])
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
ALTER TABLE [dbo].[pago]  WITH CHECK ADD  CONSTRAINT [FK_pago_usuario] FOREIGN KEY([idUsuario])
REFERENCES [dbo].[usuario] ([idUsuario])
GO
ALTER TABLE [dbo].[pago] CHECK CONSTRAINT [FK_pago_usuario]
GO
ALTER TABLE [dbo].[facturaDetalle]  WITH CHECK ADD FOREIGN KEY([idFactura])
REFERENCES [dbo].[factura] ([idFactura])
GO
ALTER TABLE [dbo].[facturaDetalle]  WITH CHECK ADD  CONSTRAINT [FK_facturaDetalle_producto] FOREIGN KEY([idProducto])
REFERENCES [dbo].[producto] ([idProducto])
GO
ALTER TABLE [dbo].[facturaDetalle] CHECK CONSTRAINT [FK_facturaDetalle_producto]
GO
ALTER TABLE [dbo].[anulacionVenta]  WITH CHECK ADD FOREIGN KEY([idFactura])
REFERENCES [dbo].[factura] ([idFactura])
GO
ALTER TABLE [dbo].[anulacionVenta]  WITH CHECK ADD FOREIGN KEY([idUsuario])
REFERENCES [dbo].[usuario] ([idUsuario])
GO
ALTER TABLE [dbo].[persona]  WITH CHECK ADD  CONSTRAINT [CK_persona_genero] CHECK  (([genero]='O' OR [genero]='F' OR [genero]='M'))
GO
ALTER TABLE [dbo].[persona] CHECK CONSTRAINT [CK_persona_genero]
GO
ALTER TABLE [dbo].[configuracionRestaurante]  WITH CHECK ADD CHECK  (([tasaImpuesto]>=(0) AND [tasaImpuesto]<=(100)))
GO
ALTER TABLE [dbo].[configuracionRestaurante]  WITH CHECK ADD CHECK  (([id]=(1)))
GO
ALTER TABLE [dbo].[configuracionRestaurante]  WITH CHECK ADD  CONSTRAINT [CK_configuracion_tarifaEnvio] CHECK  (([tarifaEnvio]>=(0)))
GO
ALTER TABLE [dbo].[configuracionRestaurante] CHECK CONSTRAINT [CK_configuracion_tarifaEnvio]
GO
ALTER TABLE dbo.mesa ADD CONSTRAINT CK_Mesa_Numero CHECK (numero BETWEEN 1 AND 999);
GO
ALTER TABLE [dbo].[mesa]  WITH CHECK ADD CHECK  (([capacidad]>=(1) AND [capacidad]<=(20)))
GO
ALTER TABLE [dbo].[mesa]  WITH CHECK ADD  CONSTRAINT [CK_Estado] CHECK  ([estado] IN ('Disponible', 'No disponible') AND ([activa] = 1 OR [estado] = 'No disponible'))
GO
ALTER TABLE [dbo].[mesa] CHECK CONSTRAINT [CK_Estado]
GO
ALTER TABLE [dbo].[producto]  WITH CHECK ADD CHECK  (([precio]>(0)))
GO
ALTER TABLE [dbo].[pedido]  WITH CHECK ADD  CONSTRAINT [CK_pedido_estado] CHECK  (([estado]='Cancelado' OR [estado]='Pagado' OR [estado]='Entregado' OR [estado]='Listo' OR [estado]='En preparación' OR [estado]='Pendiente'))
GO
ALTER TABLE [dbo].[pedido] CHECK CONSTRAINT [CK_pedido_estado]
GO
ALTER TABLE [dbo].[inventarioProducto]  WITH CHECK ADD CHECK  (([stockActual]>=(0)))
GO
ALTER TABLE [dbo].[inventarioProducto]  WITH CHECK ADD CHECK  (([stockMinimo]>=(0)))
GO
ALTER TABLE [dbo].[reservacion]  WITH CHECK ADD CHECK  (([cantidadPersonas]>(0)))
GO
ALTER TABLE [dbo].[reservacion]  WITH CHECK ADD  CONSTRAINT [CK_estadoReservacion] CHECK  (([estado]='No asistio' OR [estado]='Atendida' OR [estado]='Cancelada' OR [estado]='Completada' OR [estado]='Confirmada' OR [estado]='Pendiente'))
GO
ALTER TABLE [dbo].[reservacion] CHECK CONSTRAINT [CK_estadoReservacion]
GO
ALTER TABLE [dbo].[detallePedido]  WITH CHECK ADD CHECK  (([cantidad]>(0)))
GO
ALTER TABLE [dbo].[factura]  WITH CHECK ADD CHECK  (([total]>(0)))
GO
ALTER TABLE [dbo].[pago]  WITH CHECK ADD CHECK  (([monto]>(0)))
GO
ALTER TABLE [dbo].[pago]  WITH CHECK ADD  CONSTRAINT [CK_pago_metodo] CHECK  (([metodoDePago]='Otro' OR [metodoDePago]='Transferencia' OR [metodoDePago]='Tarjeta' OR [metodoDePago]='Efectivo'))
GO
ALTER TABLE [dbo].[pago] CHECK CONSTRAINT [CK_pago_metodo]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Procedimiento sp_Rol_Listar: consulta o modifica los datos de su gestion con parametros definidos.
CREATE   PROCEDURE sp_Rol_Listar
AS
BEGIN
    SET NOCOUNT ON;
    SELECT idRol,
           nombre,
           descripcion
    FROM rol;
END

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Este auxiliar crea datos personales; el procedimiento de registro controla su perfil y cuenta.
CREATE   PROCEDURE dbo.sp_Persona_Crear
 @PrimerNombre varchar(80),@SegundoNombre varchar(80),@PrimerApellido varchar(80),@SegundoApellido varchar(80),
 @Genero char(1),@Cedula varchar(30),@Telefono varchar(20),@Email varchar(120),@IdPersona int OUTPUT
AS
BEGIN
 SET NOCOUNT ON;
 IF NULLIF(LTRIM(RTRIM(@PrimerNombre)),'') IS NULL OR NULLIF(LTRIM(RTRIM(@PrimerApellido)),'') IS NULL
  THROW 50902,'Ingrese el nombre y apellido de la persona.',1;
 INSERT dbo.persona(primerNombre,segundoNombre,primerApellido,segundoApellido,genero,cedula,telefono,email)
 VALUES(@PrimerNombre,NULLIF(@SegundoNombre,''),@PrimerApellido,NULLIF(@SegundoApellido,''),@Genero,NULLIF(@Cedula,''),NULLIF(@Telefono,''),NULLIF(@Email,''));
 SET @IdPersona=CONVERT(int,SCOPE_IDENTITY());
END;

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Procedimiento sp_Mesa_Listar: consulta o modifica los datos de su gestion con parametros definidos.
CREATE   PROCEDURE dbo.sp_Mesa_Listar
AS
SELECT idMesa AS IdMesa,
       numero AS Numero,
       capacidad AS Capacidad,
       estado AS Estado,
       activa AS Activa
FROM dbo.mesa
ORDER BY numero;

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE dbo.sp_Configuracion_Obtener
AS
BEGIN
    SET NOCOUNT ON;
    SELECT nombre AS Nombre, moneda AS Moneda, tasaImpuesto AS TasaImpuesto
    FROM dbo.configuracionRestaurante WHERE id = 1;
END;
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Procedimiento sp_Categoria_Listar: consulta o modifica los datos de su gestion con parametros definidos.
CREATE   PROCEDURE dbo.sp_Categoria_Listar
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
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Procedimiento sp_Subcategoria_Listar: consulta o modifica los datos de su gestion con parametros definidos.
CREATE   PROCEDURE dbo.sp_Subcategoria_Listar
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
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Procedimiento sp_Producto_Listar: consulta o modifica los datos de su gestion con parametros definidos.
CREATE   PROCEDURE dbo.sp_Producto_Listar
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
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE dbo.sp_Producto_Disponibles
AS
BEGIN
    SET NOCOUNT ON;
    SELECT p.idProducto, p.idCategoria, p.idSubcategoria,
           c.nombre AS CategoriaNombre, s.nombre AS SubcategoriaNombre,
           p.nombre, p.precio, p.descripcion, p.codigo, p.disponible,
           i.stockActual, i.stockMinimo
    FROM dbo.producto p
    JOIN dbo.categoria c ON c.idCategoria = p.idCategoria
    JOIN dbo.inventarioProducto i ON i.idProducto = p.idProducto
    LEFT JOIN dbo.subcategoria s ON s.idSubcategoria = p.idSubcategoria
    WHERE p.disponible = 1 AND c.activa = 1 AND i.activo = 1 AND i.stockActual > 0
    ORDER BY c.nombre, p.nombre;
END;
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE dbo.sp_Usuario_Listar
    @Actor int
AS
BEGIN
    SET NOCOUNT ON;
    EXEC dbo.sp_Catalogo_ExigirAdministrador @Actor;
    SELECT idUsuario, idPersona, nombreUsuario, idRol, NombreRol, AccesoPersonal
    FROM dbo.vw_UsuarioAccesos
    WHERE idRol IN (1, 2)
    ORDER BY nombreUsuario;
END;
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Procedimiento sp_Empleado_Listar: consulta o modifica los datos de su gestion con parametros definidos.
CREATE   PROCEDURE dbo.sp_Empleado_Listar
    @Actor int
AS
BEGIN
    SET NOCOUNT ON;
    EXEC dbo.sp_Catalogo_ExigirAdministrador @Actor;
    SELECT e.idEmpleado,e.idPersona,e.email,
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
    FROM dbo.vw_Empleado AS e
         LEFT OUTER JOIN
         dbo.usuario AS u
         ON u.idUsuario = e.idUsuario
         LEFT OUTER JOIN
         dbo.rol AS r
         ON r.idRol = u.idRol
    ORDER BY e.idEmpleado;
END

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Procedimiento sp_Empleado_Desactivar: consulta o modifica los datos de su gestion con parametros definidos.
CREATE   PROCEDURE dbo.sp_Empleado_Desactivar
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
                  FROM dbo.vw_Empleado
                  WHERE idUsuario = @IdAdministrador
                        AND activo = 0)
        THROW 50010, 'Solo un administrador activo puede gestionar empleados.', 1;
    BEGIN TRY
        BEGIN TRANSACTION;
        IF NOT EXISTS (SELECT 1
                       FROM dbo.vw_Empleado
                       WHERE idEmpleado = @IdEmpleado)
            THROW 50013, 'El empleado seleccionado ya no existe.', 1;
        IF EXISTS (SELECT 1
                   FROM dbo.vw_Empleado
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
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE dbo.sp_Empleado_Actualizar
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
    @Activo BIT,
    @IdPersona int=0
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
                  FROM dbo.vw_Empleado
                  WHERE idUsuario = @IdAdministrador
                        AND activo = 0)
        THROW 50010, 'Solo un administrador activo puede gestionar empleados.', 1;
    IF @IdRol IS NULL
       OR @IdRol NOT IN (1, 2)
        THROW 50011, 'El rol debe ser Administrador o Empleado.', 1;
    IF @IdRol = 2 AND (@Cargo IS NULL OR UPPER(LTRIM(RTRIM(@Cargo))) <> 'DESPACHADOR')
        THROW 50802, 'Seleccione Despachador como cargo del empleado.', 1;
    IF @IdRol = 2
        SET @Cargo = 'Despachador';
    BEGIN TRY
        BEGIN TRANSACTION;
        DECLARE @IdUsuario AS INT,@PersonaActual int;
        SELECT @PersonaActual=idPersona FROM dbo.vw_Empleado WHERE idEmpleado=@IdEmpleado;
        IF @IdPersona<>0 AND @IdPersona<>@PersonaActual
            THROW 50906,'No puede reasignar el perfil a otra persona desde una edicion.',1;
        IF NOT EXISTS (SELECT 1
                       FROM dbo.vw_Empleado
                       WHERE idEmpleado = @IdEmpleado)
            THROW 50013, 'El empleado seleccionado ya no existe.', 1;
        SELECT @IdUsuario = idUsuario
        FROM dbo.vw_Empleado
        WHERE idEmpleado = @IdEmpleado;
        IF @IdUsuario = @IdAdministrador
           AND (@Activo = 0
                OR @IdRol <> 1)
            THROW 50014, 'No puede desactivar su propia cuenta ni quitarse el rol de administrador.', 1;
        IF @IdUsuario IS NULL
            BEGIN
                IF NULLIF (LTRIM(RTRIM(@Contrasena)), '') IS NULL
                    THROW 50012, 'Este empleado no tiene cuenta: ingrese una contraseña para crearla.', 1;
                INSERT INTO dbo.usuario (nombreUsuario, contrasena, idRol, idPersona)
                VALUES                 (@Usuario, @Contrasena, @IdRol, @PersonaActual);
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
        UPDATE dbo.persona SET primerNombre=@PrimerNombre,segundoNombre=NULLIF(@SegundoNombre,''),
            primerApellido=@PrimerApellido,segundoApellido=NULLIF(@SegundoApellido,''),genero=@Genero,
            cedula=@Cedula,telefono=@Telefono WHERE idPersona=@PersonaActual;
        UPDATE dbo.empleado SET idUsuario=@IdUsuario,cargo=@Cargo,activo=@Activo WHERE idEmpleado=@IdEmpleado;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK;
        THROW;
    END CATCH
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Procedimiento sp_Catalogo_ExigirAdministrador: consulta o modifica los datos de su gestion con parametros definidos.
CREATE   PROCEDURE dbo.sp_Catalogo_ExigirAdministrador
    @IdAdministrador INT
AS
BEGIN
    SET NOCOUNT ON;
    IF NOT EXISTS (SELECT 1
                   FROM dbo.usuario
                   WHERE idUsuario = @IdAdministrador
                         AND idRol = 1)
       OR EXISTS (SELECT 1
                  FROM dbo.vw_Empleado
                  WHERE idUsuario = @IdAdministrador
                        AND activo = 0)
        THROW 50020, 'Solo un administrador activo puede modificar el catálogo.', 1;
END

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE dbo.sp_Operacion_ExigirPersonal @Actor int
AS
BEGIN
    SET NOCOUNT ON;
    IF NOT EXISTS (
        SELECT 1 FROM dbo.vw_UsuarioAccesos WHERE idUsuario = @Actor AND AccesoPersonal = 1
    )
        THROW 50501, 'Esta operación requiere una cuenta activa del personal.', 1;
END;
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Registra el usuario, el producto, las unidades y el motivo recibidos por parametros.
-- Cada procedimiento recibe el usuario y el motivo de forma explicita.
CREATE   PROCEDURE dbo.sp_Inventario_RegistrarMovimiento
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
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Procedimiento sp_Categoria_Guardar: consulta o modifica los datos de su gestion con parametros definidos.
CREATE   PROCEDURE dbo.sp_Categoria_Guardar
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
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Procedimiento sp_Categoria_Eliminar: consulta o modifica los datos de su gestion con parametros definidos.
CREATE   PROCEDURE dbo.sp_Categoria_Eliminar
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
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Procedimiento sp_Categoria_Desactivar: consulta o modifica los datos de su gestion con parametros definidos.
CREATE   PROCEDURE dbo.sp_Categoria_Desactivar
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
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE dbo.sp_Configuracion_Guardar
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
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Procedimiento sp_Inventario_Movimientos: consulta o modifica los datos de su gestion con parametros definidos.
CREATE   PROCEDURE dbo.sp_Inventario_Movimientos
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
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Procedimiento sp_Inventario_Listar: consulta o modifica los datos de su gestion con parametros definidos.
CREATE   PROCEDURE dbo.sp_Inventario_Listar
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
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Crea o modifica inventario. Revision es un contador entero que aumenta en cada cambio.
CREATE   PROCEDURE dbo.sp_Inventario_Guardar
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
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Elimina el control solo si no quedan unidades ni pedidos abiertos.
CREATE   PROCEDURE dbo.sp_Inventario_Eliminar @Actor int,
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
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Registra una entrada o salida con motivo; no permite stock negativo.
CREATE   PROCEDURE dbo.sp_Inventario_Ajustar
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
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Procedimiento sp_Gestion_Clientes: consulta o modifica los datos de su gestion con parametros definidos.
CREATE   PROCEDURE dbo.sp_Gestion_Clientes
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
    FROM dbo.vw_Cliente
    ORDER BY PNombreCl, PApellidoCl;
END

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE dbo.sp_Operacion_ExigirPedidoAbierto
    @Actor int, @IdPedido int
AS
BEGIN
    SET NOCOUNT ON;
    EXEC dbo.sp_Operacion_ExigirPersonal @Actor;
    IF EXISTS (SELECT 1 FROM dbo.usuario WHERE idUsuario = @Actor AND idRol = 2)
        AND NOT EXISTS (
            SELECT 1 FROM dbo.pedido WHERE idPedido = @IdPedido AND idOrigenPedido = 2
                AND idTipoEntrega IN (1, 2) AND estado NOT IN ('Pagado', 'Cancelado')
        )
        THROW 50522, 'El despachador solo puede operar ventas locales abiertas.', 1;
END;
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE dbo.sp_Operacion_EstadoReserva
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
                                  OR estado <> 'Disponible'))
            THROW 50526, 'La mesa no esta disponible para recibir al cliente.', 1;
        UPDATE dbo.reservacion
        SET estado = @Nuevo
        WHERE idReservacion = @IdReserva;
        IF @Nuevo = 'Atendida'
            UPDATE dbo.mesa
            SET estado = 'No disponible'
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
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE dbo.sp_Cliente_Guardar
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
    EXEC dbo.sp_Catalogo_ExigirAdministrador @Actor;
    IF NULLIF (LTRIM(RTRIM(@PrimerNombre)), '') IS NULL
       OR NULLIF (LTRIM(RTRIM(@PrimerApellido)), '') IS NULL
       OR @Genero NOT IN ('M', 'F', 'O')
        THROW 50201, 'Revise nombre, apellido y género.', 1;
    BEGIN TRY
        BEGIN TRANSACTION;
        DECLARE @Existe AS INT,@IdPersona int;
        SELECT @IdPersona=idPersona,@Existe = idCliente
        FROM dbo.vw_Cliente
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
                EXEC dbo.sp_Persona_Crear @PrimerNombre,@SegundoNombre,@PrimerApellido,@SegundoApellido,@Genero,@Cedula,@Telefono,@Email,@IdPersona OUTPUT;
                INSERT dbo.cliente(idPersona,activo) VALUES(@IdPersona,@Activo);
                SET @Id = CONVERT (INT, SCOPE_IDENTITY());
            END
        ELSE
            BEGIN
                IF EXISTS(SELECT 1 FROM dbo.vw_Empleado WHERE idPersona=@IdPersona)
                    AND (NULLIF(@Cedula,'') IS NULL OR NULLIF(@Telefono,'') IS NULL)
                    THROW 50907,'Esta persona tambien es personal: conserve su cedula y telefono.',1;
                UPDATE dbo.persona SET primerNombre=@PrimerNombre,segundoNombre=NULLIF(@SegundoNombre,''),
                    primerApellido=@PrimerApellido,segundoApellido=NULLIF(@SegundoApellido,''),genero=@Genero,
                    cedula=NULLIF(@Cedula,''),telefono=NULLIF(@Telefono,''),email=NULLIF(@Email,'') WHERE idPersona=@IdPersona;
                UPDATE dbo.cliente SET activo=@Activo WHERE idCliente=@Id;
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
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Procedimiento sp_Mesa_Guardar: consulta o modifica los datos de su gestion con parametros definidos.
CREATE   PROCEDURE dbo.sp_Mesa_Guardar
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
       OR @Estado NOT IN ('Disponible', 'No disponible')
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
            THROW 50210, 'La mesa tiene ventas abiertas. Cobre o cancele las ventas antes de habilitar la mesa.', 1;
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
                VALUES (@Numero, @Capacidad, CASE WHEN @Activa = 0 THEN 'No disponible' ELSE @Estado END, @Activa);
                SET @Id = CONVERT (INT, SCOPE_IDENTITY());
            END
        ELSE
            BEGIN
                UPDATE dbo.mesa
                SET numero = @Numero,
                    capacidad = @Capacidad,
                    estado = CASE WHEN @Activa = 0 THEN 'No disponible' ELSE @Estado END,
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
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Procedimiento sp_Mesa_Eliminar: consulta o modifica los datos de su gestion con parametros definidos.
CREATE   PROCEDURE dbo.sp_Mesa_Eliminar
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
        IF EXISTS (SELECT 1
                   FROM dbo.pedido
                   WHERE idMesa = @Id)
           OR EXISTS (SELECT 1
                      FROM dbo.reservacion
                      WHERE idMesa = @Id)
            THROW 50415, 'La mesa tiene historial de ventas o reservas. Puede marcarla como No disponible.', 1;
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
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Vincular otra cuenta a una persona existente es una operacion exclusiva del administrador.
CREATE   PROCEDURE dbo.sp_Persona_CompletarVinculo
 @Actor int,@IdPersona int,@SegundoNombre varchar(80),@SegundoApellido varchar(80),@Genero char(1),
 @Cedula varchar(30),@Telefono varchar(20),@Email varchar(120)
AS
BEGIN
 SET NOCOUNT ON;
 EXEC dbo.sp_Catalogo_ExigirAdministrador @Actor;
 IF NOT EXISTS(SELECT 1 FROM dbo.persona WHERE idPersona=@IdPersona)
  THROW 50903,'La persona seleccionada ya no existe. Recargue el formulario.',1;
 -- Solo se completan campos vacios; los datos existentes se editan desde su gestion.
 UPDATE dbo.persona SET segundoNombre=COALESCE(NULLIF(segundoNombre,''),NULLIF(@SegundoNombre,'')),
 segundoApellido=COALESCE(NULLIF(segundoApellido,''),NULLIF(@SegundoApellido,'')),
 genero=COALESCE(genero,@Genero),cedula=COALESCE(NULLIF(cedula,''),NULLIF(@Cedula,'')),
 telefono=COALESCE(NULLIF(telefono,''),NULLIF(@Telefono,'')),email=COALESCE(NULLIF(email,''),NULLIF(@Email,''))
 WHERE idPersona=@IdPersona;
END;

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE dbo.sp_Pedido_Iniciar
    @Actor int, @IdCliente int = NULL, @Solicitud uniqueidentifier, @NumeroLineas int
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
                AND idOrigenPedido = 2 AND ISNULL(idCliente, 0) = ISNULL(@IdCliente, 0)
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
                       solicitud, idUsuarioRegistro, idOrigenPedido, idTipoEntrega)
    VALUES (@IdCliente, GETDATE(), 'Pendiente', 0, 0, 0, 0, @Solicitud, @Actor, 2, 2);
    SET @IdPedido = CONVERT(int, SCOPE_IDENTITY());
    INSERT dbo.historialPedido(idPedido, estadoAnterior, estadoNuevo, idUsuario)
    VALUES (@IdPedido, NULL, 'Pendiente', @Actor);
    SELECT @IdPedido AS IdPedido, CAST(0 AS bit) AS Reintento;
END;
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE dbo.sp_Pedido_Finalizar
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

    DECLARE @Subtotal decimal(18,2);
    SELECT @Subtotal = SUM(total) FROM dbo.detallePedido WHERE idPedido = @IdPedido;
    IF @Subtotal IS NULL OR @Subtotal <= 0 OR @Subtotal > 99999999.99
        THROW 50710, 'El total supera el importe permitido.', 1;
    UPDATE dbo.pedido SET subtotal = @Subtotal, total = @Subtotal + costoEnvio
    WHERE idPedido = @IdPedido AND estado = 'Pendiente';
    IF @@ROWCOUNT = 0 THROW 50704, 'El pedido ya no esta pendiente.', 1;
END;
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE dbo.sp_Usuario_CuentaParaPerfil @Actor int,@IdPersona int,@RolNuevo int AS
BEGIN
 SET NOCOUNT ON;
 EXEC dbo.sp_Catalogo_ExigirAdministrador @Actor;
 SELECT TOP(1) idUsuario,nombreUsuario,idRol FROM dbo.usuario
 WHERE idPersona=@IdPersona AND ((@RolNuevo=3 AND idRol IN(1,2)) OR (@RolNuevo IN(1,2) AND idRol=3))
 ORDER BY idUsuario;
END;

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE dbo.sp_Operacion_Detalle
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
    JOIN dbo.pedido venta ON venta.idPedido = d.idPedido
    WHERE d.idPedido = @IdPedido
        AND (EXISTS (SELECT 1 FROM dbo.usuario WHERE idUsuario = @Actor AND idRol = 1)
             OR (venta.idOrigenPedido = 2 AND venta.idTipoEntrega IN (1, 2)
                 AND venta.estado NOT IN ('Pagado', 'Cancelado')))
    ORDER BY d.idDetalle;
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE dbo.sp_Operacion_Clientes
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
    FROM dbo.vw_Cliente
    WHERE activo = 1
    ORDER BY pNombreCl, pApellidoCl;
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Procedimiento sp_Producto_Desactivar: consulta o modifica los datos de su gestion con parametros definidos.
CREATE   PROCEDURE dbo.sp_Producto_Desactivar
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
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE dbo.sp_Persona_Listar @Actor int AS
BEGIN
 SET NOCOUNT ON;
 EXEC dbo.sp_Catalogo_ExigirAdministrador @Actor;
 SELECT idPersona,primerNombre,segundoNombre,primerApellido,segundoApellido,genero,cedula,telefono,email
 FROM dbo.persona ORDER BY primerNombre,primerApellido,idPersona;
END;

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE dbo.sp_Pedido_AgregarLinea
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

    EXEC dbo.sp_Operacion_ExigirPersonal @Actor;
    IF NOT EXISTS (
        SELECT 1 FROM dbo.pedido WHERE idPedido = @IdPedido
            AND idOrigenPedido = 2 AND idUsuarioRegistro = @Actor
    )
        THROW 50706, 'La venta no corresponde a esta solicitud del personal.', 1;

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
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE dbo.sp_Operacion_Reservas
    @Actor INT
AS
BEGIN
    SET NOCOUNT ON;
    EXEC dbo.sp_Catalogo_ExigirAdministrador @Actor;
    SELECT r.idReservacion AS IdReserva,
           r.idMesa AS IdMesa,
           m.numero AS Mesa,
           CONCAT(c.pNombreCl, ' ', c.pApellidoCl) AS Cliente,
           r.fechaHoraReserva AS Fecha,
           r.cantidadPersonas AS Personas,
           r.estado AS Estado
    FROM dbo.reservacion AS r
         INNER JOIN dbo.vw_Cliente AS c
         ON c.idCliente = r.idCliente
         INNER JOIN
         dbo.mesa AS m
         ON m.idMesa = r.idMesa
    ORDER BY r.fechaHoraReserva DESC;
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE dbo.sp_Operacion_Mesas
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
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Procedimiento sp_Rol_Guardar: consulta o modifica los datos de su gestion con parametros definidos.
CREATE   PROCEDURE dbo.sp_Rol_Guardar
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
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Procedimiento sp_Rol_Eliminar: consulta o modifica los datos de su gestion con parametros definidos.
CREATE   PROCEDURE dbo.sp_Rol_Eliminar
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
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Procedimiento sp_Producto_Guardar: consulta o modifica los datos de su gestion con parametros definidos.
CREATE   PROCEDURE dbo.sp_Producto_Guardar
    @IdProducto INT,
    @IdAdministrador INT,
    @IdCategoria INT,
    @IdSubcategoria INT,
    @Nombre VARCHAR (100),
    @Precio DECIMAL (10, 2),
    @Descripcion VARCHAR (250),
    @Codigo VARCHAR (20) = NULL,
    @Disponible BIT
AS
BEGIN
    SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    EXECUTE dbo.sp_Catalogo_ExigirAdministrador @IdAdministrador;
    IF NULLIF (LTRIM(RTRIM(@Nombre)), '') IS NULL OR @Precio IS NULL OR @Precio <= 0
        THROW 50024, 'El nombre y un precio positivo son obligatorios.', 1;
    IF @IdProducto <> 0 AND NULLIF (LTRIM(RTRIM(@Codigo)), '') IS NULL
        THROW 50024, 'El código del producto es obligatorio al editar.', 1;
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
        BEGIN
            IF NULLIF(LTRIM(RTRIM(@Codigo)), '') IS NULL
            BEGIN
                DECLARE @NumeroCodigo bigint;
                SELECT @NumeroCodigo = ISNULL(CONVERT(bigint, MAX(idProducto)), 0) + 1
                FROM dbo.producto WITH (UPDLOCK);

                SET @Codigo = CONCAT('PRD-', @NumeroCodigo);
                WHILE EXISTS (SELECT 1 FROM dbo.producto WHERE codigo = @Codigo)
                BEGIN
                    SET @NumeroCodigo = @NumeroCodigo + 1;
                    SET @Codigo = CONCAT('PRD-', @NumeroCodigo);
                END;
            END;

            INSERT dbo.producto (idCategoria, idSubcategoria, nombre, precio, descripcion, codigo, disponible)
            VALUES             (@IdCategoria, @IdSubcategoria, @Nombre, @Precio, @Descripcion, @Codigo, @Disponible);
        END
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
        IF @IdProducto = 0 SELECT @Codigo;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK;
        THROW;
    END CATCH
END

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Procedimiento sp_Producto_Eliminar: consulta o modifica los datos de su gestion con parametros definidos.
CREATE   PROCEDURE dbo.sp_Producto_Eliminar
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
        IF EXISTS(SELECT 1 FROM dbo.movimientoInventario WHERE idProducto=@IdProducto)
           OR EXISTS(SELECT 1 FROM dbo.facturaDetalle WHERE idProducto=@IdProducto)
            THROW 51101,'El producto tiene movimientos o ventas historicas. Marquelo como no disponible.',1;
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
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE dbo.sp_Operacion_Pedidos
    @Actor INT
AS
BEGIN
    SET NOCOUNT ON;
    EXECUTE dbo.sp_Operacion_ExigirPersonal @Actor;
    SELECT p.idPedido AS IdPedido,
           p.fechaHora AS Fecha,
           CASE WHEN p.idCliente IS NULL THEN 'Consumidor final' ELSE CONCAT(c.pNombreCl, ' ', c.pApellidoCl) END AS Cliente,
           p.estado AS Estado,
           p.total AS Total,
           o.nombre AS Origen, CASE WHEN p.idOrigenPedido = 2 AND p.idTipoEntrega = 2 THEN 'Mostrador' ELSE t.nombre END AS Entrega, p.idTipoEntrega AS IdTipoEntrega,
           COALESCE(ur.nombreUsuario,'Sin registrar') AS RegistradoPor,
           COALESCE(ua.nombreUsuario,'Sin registrar') AS AtendidoPor,
           p.costoEnvio AS Envio, p.direccion AS Direccion, p.telefonoDeContacto AS Contacto,
           p.idMesa AS IdMesa,
           m.numero AS Mesa,
           CASE WHEN EXISTS (SELECT 1 FROM dbo.factura f WHERE f.idPedido = p.idPedido AND f.estado = 'Anulada')
                THEN 'Anulado' WHEN EXISTS (SELECT 1
                             FROM dbo.pago AS pa
                                  INNER JOIN
                                  dbo.factura AS f
                                  ON f.idFactura = pa.idFactura
                             WHERE f.idPedido = p.idPedido) THEN 'Pagado' ELSE 'Sin pagar' END AS Pago
    FROM dbo.pedido AS p
         INNER JOIN dbo.origenPedido o ON o.idOrigenPedido=p.idOrigenPedido
         INNER JOIN dbo.tipoEntrega t ON t.idTipoEntrega=p.idTipoEntrega
         LEFT JOIN dbo.usuario ur ON ur.idUsuario=p.idUsuarioRegistro
         LEFT JOIN dbo.empleado ea ON ea.idEmpleado=p.idEmpleado
         LEFT JOIN dbo.usuario ua ON ua.idUsuario=ea.idUsuario
         LEFT JOIN dbo.vw_Cliente AS c
         ON c.idCliente = p.idCliente
         LEFT OUTER JOIN
         dbo.mesa AS m
         ON m.idMesa = p.idMesa
    WHERE EXISTS (SELECT 1 FROM dbo.usuario WHERE idUsuario = @Actor AND idRol = 1)
        OR (p.idOrigenPedido = 2 AND p.idTipoEntrega IN (1, 2) AND p.estado NOT IN ('Pagado', 'Cancelado'))
    ORDER BY p.idPedido DESC;
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE dbo.sp_Operacion_AsignarMesa
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
            THROW 50810,'No se asigna mesa a un pedido histórico a domicilio.',1;
        DECLARE @Cliente int, @Anterior int, @Existe int;
        SELECT @Existe = idPedido, @Cliente = idCliente,
               @Anterior = idMesa
        FROM dbo.pedido
        WHERE idPedido = @IdPedido
              AND estado IN ('Pendiente', 'En preparación', 'Listo');
        IF @Existe IS NULL
            THROW 50531, 'Solo se asigna mesa antes de entregar el pedido.', 1;
        IF NOT EXISTS (SELECT 1
                       FROM dbo.mesa
                       WHERE idMesa = @IdMesa
                             AND activa = 1
                             AND estado IN ('Disponible', 'No disponible'))
            THROW 50532, 'La mesa no está disponible. Seleccione otra mesa.', 1;
        IF EXISTS (SELECT 1
                   FROM dbo.mesa
                   WHERE idMesa = @IdMesa
                         AND estado = 'No disponible')
           AND NOT EXISTS (SELECT 1
                           FROM dbo.pedido
                           WHERE idMesa = @IdMesa
                                 AND (idPedido = @IdPedido OR idCliente = @Cliente)
                                 AND estado NOT IN ('Pagado', 'Cancelado'))
            THROW 50533, 'La mesa no está disponible. Seleccione otra mesa.', 1;
        IF EXISTS (SELECT 1
                   FROM dbo.pedido
                   WHERE idMesa = @IdMesa
                         AND idPedido <> @IdPedido
                         AND (@Cliente IS NULL OR ISNULL(idCliente, 0) <> @Cliente)
                         AND estado NOT IN ('Pagado', 'Cancelado'))
            THROW 50533, 'La mesa tiene pedidos abiertos de otro cliente.', 1;
        IF EXISTS (SELECT 1
                   FROM dbo.reservacion
                   WHERE idMesa = @IdMesa
                         AND (@Cliente IS NULL OR ISNULL(idCliente, 0) <> @Cliente)
                         AND estado IN ('Pendiente', 'Confirmada', 'Atendida')
                         AND ABS(DATEDIFF(minute, GETDATE(), fechaHoraReserva)) < 120)
            THROW 50533, 'La mesa tiene una reserva de otro cliente en este horario.', 1;
        UPDATE dbo.pedido
        SET idMesa = @IdMesa, idTipoEntrega=1,
            idEmpleado=COALESCE(idEmpleado,(SELECT idEmpleado FROM dbo.vw_Empleado WHERE idUsuario=@Actor AND activo=1))
        WHERE idPedido = @IdPedido;
        UPDATE dbo.mesa
        SET estado = 'No disponible'
        WHERE idMesa = @IdMesa;
        IF @Anterior IS NOT NULL
           AND @Anterior <> @IdMesa
           AND NOT EXISTS (SELECT 1
                           FROM dbo.pedido
                           WHERE idMesa = @Anterior
                                 AND estado NOT IN ('Pagado', 'Cancelado'))
            UPDATE dbo.mesa
            SET estado = 'Disponible'
            WHERE idMesa = @Anterior AND activa = 1;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK;
        THROW;
    END CATCH
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE dbo.sp_Pedido_CancelarComun @Actor int,
    @IdPedido int
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;
    BEGIN TRY
        BEGIN TRANSACTION;
        EXEC dbo.sp_Operacion_ExigirPedidoAbierto @Actor, @IdPedido;
        UPDATE dbo.pedido SET estado = 'Cancelado'
        WHERE idPedido = @IdPedido AND estado = 'Pendiente';
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
        UPDATE m SET estado = 'Disponible'
        FROM dbo.mesa m JOIN dbo.pedido p ON p.idMesa = m.idMesa
        WHERE p.idPedido = @IdPedido AND m.activa = 1 AND NOT EXISTS (
            SELECT 1 FROM dbo.pedido otro WHERE otro.idMesa = m.idMesa
                AND otro.estado NOT IN ('Pagado', 'Cancelado')
        );
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE dbo.sp_Empleado_Insertar
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
    @Activo BIT,
    @IdPersona int=0, @IdUsuarioExistente int=0
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
                  FROM dbo.vw_Empleado
                  WHERE idUsuario = @IdAdministrador
                        AND activo = 0)
        THROW 50010, 'Solo un administrador activo puede gestionar empleados.', 1;
    IF @IdRol IS NULL
       OR @IdRol NOT IN (1, 2)
        THROW 50011, 'El rol debe ser Administrador o Empleado.', 1;
    IF @IdUsuarioExistente=0 AND NULLIF (LTRIM(RTRIM(@Contrasena)), '') IS NULL
        THROW 50012, 'La contraseña es obligatoria para crear la cuenta.', 1;
    IF @IdRol = 2 AND (@Cargo IS NULL OR UPPER(LTRIM(RTRIM(@Cargo))) <> 'DESPACHADOR')
        THROW 50802, 'Seleccione Despachador como cargo del empleado.', 1;
    IF @IdRol = 2
        SET @Cargo = 'Despachador';
    BEGIN TRY
        BEGIN TRANSACTION;
        IF @IdPersona=0
            EXEC dbo.sp_Persona_Crear @PrimerNombre,@SegundoNombre,@PrimerApellido,@SegundoApellido,@Genero,@Cedula,@Telefono,NULL,@IdPersona OUTPUT;
        ELSE
            EXEC dbo.sp_Persona_CompletarVinculo @IdAdministrador,@IdPersona,@SegundoNombre,@SegundoApellido,@Genero,@Cedula,@Telefono,NULL;
        IF EXISTS(SELECT 1 FROM dbo.vw_Empleado WHERE idPersona=@IdPersona AND (idUsuario IS NOT NULL OR activo=0))
            THROW 50905,'La persona ya tiene un perfil de personal con cuenta o inactivo. Revise Empleados.',1;
        DECLARE @IdUsuario int;
        IF @IdUsuarioExistente=0
        BEGIN
            IF EXISTS(SELECT 1 FROM dbo.usuario WHERE idPersona=@IdPersona)
                THROW 51006,'La persona ya tiene cuenta. Seleccionela para agregar el perfil sin duplicarla.',1;
            INSERT dbo.usuario(nombreUsuario,contrasena,idRol,idPersona) VALUES(@Usuario,@Contrasena,@IdRol,@IdPersona);
            SET @IdUsuario=CONVERT(int,SCOPE_IDENTITY());
        END
        ELSE
        BEGIN
            UPDATE dbo.usuario SET idRol=@IdRol
             WHERE idUsuario=@IdUsuarioExistente AND idPersona=@IdPersona AND idRol=3;
            IF @@ROWCOUNT<>1 THROW 51006,'Solo puede agregar personal a una cuenta de cliente de esta persona.',1;
            SET @IdUsuario=@IdUsuarioExistente;
        END;
        IF EXISTS(SELECT 1 FROM dbo.vw_Empleado WHERE idPersona=@IdPersona)
            UPDATE dbo.empleado SET idUsuario=@IdUsuario,cargo=@Cargo,activo=@Activo WHERE idPersona=@IdPersona;
        ELSE
            INSERT dbo.empleado(idUsuario,idPersona,cargo,activo) VALUES(@IdUsuario,@IdPersona,@Cargo,@Activo);
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK;
        THROW;
    END CATCH
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Procedimiento sp_Empleado_Eliminar: consulta o modifica los datos de su gestion con parametros definidos.
CREATE   PROCEDURE dbo.sp_Empleado_Eliminar
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
        DECLARE @Usuario AS INT, @Existe AS INT, @Persona int;
        SELECT @Persona=idPersona,@Existe = idEmpleado,
               @Usuario = idUsuario
        FROM dbo.vw_Empleado
        WHERE idEmpleado = @IdEmpleado;
        IF @Existe IS NULL
            THROW 50409, 'El empleado ya no existe. Recargue el listado.', 1;
        DECLARE @ConservarCuenta bit=0;
        IF EXISTS(SELECT 1 FROM dbo.cliente WHERE idPersona=@Persona) SET @ConservarCuenta=1;
        IF EXISTS (SELECT 1
                   FROM dbo.pedido
                   WHERE idEmpleado = @IdEmpleado
                         OR idEmpleadoRepartidor = @IdEmpleado OR (idUsuarioRegistro=@Usuario AND @ConservarCuenta=0))
           OR EXISTS(SELECT 1 FROM dbo.pago WHERE idUsuario=@Usuario AND @ConservarCuenta=0)
           OR EXISTS(SELECT 1 FROM dbo.historialPedido WHERE idUsuario=@Usuario AND @ConservarCuenta=0)
            THROW 50410, 'El empleado tiene historial de pedidos o repartos. Utilice Desactivar.', 1;
        IF @Usuario IS NOT NULL
            BEGIN
                IF @Usuario = @IdAdministrador
                   OR NOT EXISTS (SELECT 1
                                  FROM dbo.usuario
                                  WHERE idUsuario = @Usuario
                                        AND idRol = 2)
                    THROW 50411, 'No se pueden eliminar cuentas de administrador desde Empleados.', 1;
            END
        IF @ConservarCuenta=0 AND @Usuario IS NOT NULL AND
           EXISTS(SELECT 1 FROM dbo.movimientoInventario WHERE idUsuario=@Usuario)
            THROW 51102,'El empleado tiene movimientos de inventario. Use Desactivar para conservar la trazabilidad.',1;
        DELETE dbo.empleado
        WHERE idEmpleado = @IdEmpleado;
        IF @Usuario IS NOT NULL AND @ConservarCuenta=1
            UPDATE dbo.usuario SET idRol=3 WHERE idUsuario=@Usuario;
        IF @Usuario IS NOT NULL AND @ConservarCuenta=0
            DELETE dbo.usuario
            WHERE idUsuario = @Usuario;
        IF NOT EXISTS(SELECT 1 FROM dbo.vw_Cliente WHERE idPersona=@Persona)
           AND NOT EXISTS(SELECT 1 FROM dbo.vw_Empleado WHERE idPersona=@Persona)
           AND NOT EXISTS(SELECT 1 FROM dbo.usuario WHERE idPersona=@Persona)
            DELETE dbo.persona WHERE idPersona=@Persona;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK;
        THROW;
    END CATCH
END

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE dbo.sp_Documento_Obtener
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
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Procedimiento sp_Cliente_Eliminar: consulta o modifica los datos de su gestion con parametros definidos.
CREATE   PROCEDURE dbo.sp_Cliente_Eliminar
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
        DECLARE @Usuario AS INT, @Existe AS INT, @Persona int;
        SELECT @Persona=idPersona,@Existe = idCliente,
               @Usuario = idUsuario
        FROM dbo.vw_Cliente
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
        DECLARE @ConservarCuenta bit=0;
        IF EXISTS(SELECT 1 FROM dbo.usuario WHERE idUsuario=@Usuario AND idRol IN(1,2))
           OR EXISTS(SELECT 1 FROM dbo.empleado WHERE idUsuario=@Usuario) SET @ConservarCuenta=1;
        IF @Usuario IS NOT NULL AND @ConservarCuenta=0
            BEGIN
                IF @Usuario = @Actor
                   OR NOT EXISTS (SELECT 1
                                  FROM dbo.usuario
                                  WHERE idUsuario = @Usuario
                                        AND idRol = 3)
                    THROW 50407, 'Solo puede eliminar junto al perfil una cuenta de cliente sin historial.', 1;
                IF EXISTS (SELECT 1
                           FROM dbo.vw_Empleado
                           WHERE idUsuario = @Usuario)
                   OR EXISTS (SELECT 1
                              FROM dbo.vw_Cliente
                              WHERE idUsuario = @Usuario
                                    AND idCliente <> @Id)
                    THROW 50408, 'La cuenta tiene otros perfiles asociados. No se eliminaron datos.', 1;
            END
        IF @ConservarCuenta=0 AND @Usuario IS NOT NULL AND
           (EXISTS(SELECT 1 FROM dbo.pago WHERE idUsuario=@Usuario)
            OR EXISTS(SELECT 1 FROM dbo.movimientoInventario WHERE idUsuario=@Usuario)
            OR EXISTS(SELECT 1 FROM dbo.historialPedido WHERE idUsuario=@Usuario)
            OR EXISTS(SELECT 1 FROM dbo.pedido WHERE idUsuarioRegistro=@Usuario))
            THROW 51102,'La cuenta conserva historial de operaciones. Use Desactivar para conservar la trazabilidad.',1;
        DELETE dbo.cliente
        WHERE idCliente = @Id;
        IF @Usuario IS NOT NULL AND @ConservarCuenta=0
            DELETE dbo.usuario
            WHERE idUsuario = @Usuario;
        IF NOT EXISTS(SELECT 1 FROM dbo.vw_Cliente WHERE idPersona=@Persona)
           AND NOT EXISTS(SELECT 1 FROM dbo.vw_Empleado WHERE idPersona=@Persona)
           AND NOT EXISTS(SELECT 1 FROM dbo.usuario WHERE idPersona=@Persona)
            DELETE dbo.persona WHERE idPersona=@Persona;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK;
        THROW;
    END CATCH
END

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE dbo.sp_Operacion_EstadoPedido
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
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE dbo.sp_Facturas_Listar
    @Actor INT
AS
BEGIN
    SET NOCOUNT ON;
    EXEC dbo.sp_Catalogo_ExigirAdministrador @Actor;
    SELECT f.idFactura AS IdFactura,
           f.idPedido AS IdPedido,
           f.numeroFactura AS Numero,
           f.fechaEmision AS Fecha,
           f.clienteNombre AS Cliente,
           f.estado AS Estado,
           f.total AS Total,
           a.fecha AS FechaAnulacion,
           a.motivo AS MotivoAnulacion
    FROM dbo.factura f LEFT JOIN dbo.anulacionVenta a ON a.idFactura = f.idFactura
    ORDER BY f.idFactura DESC;
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE dbo.sp_Factura_Anular
    @Actor int, @IdPedido int, @Motivo nvarchar(200), @ReponerInventario bit
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;
    EXEC dbo.sp_Catalogo_ExigirAdministrador @Actor;
    SET @Motivo = LTRIM(RTRIM(@Motivo));
    IF NULLIF(@Motivo, '') IS NULL OR @ReponerInventario IS NULL
        THROW 50540, 'Indique el motivo de la anulación.', 1;

    BEGIN TRY
        BEGIN TRANSACTION;
        IF NOT EXISTS (SELECT 1 FROM dbo.pedido WITH (UPDLOCK) WHERE idPedido = @IdPedido
                       AND estado = 'Pagado' AND idOrigenPedido = 2 AND idTipoEntrega IN (1, 2))
            THROW 50541, 'Solo se puede anular una venta local pagada.', 1;

        DECLARE @IdFactura int;
        DECLARE @Numero varchar(30);
        SELECT @IdFactura = idFactura, @Numero = numeroFactura
        FROM dbo.factura WHERE idPedido = @IdPedido AND estado = 'Pagada';
        IF @IdFactura IS NULL OR NOT EXISTS (SELECT 1 FROM dbo.pago WHERE idFactura = @IdFactura)
            THROW 50542, 'La venta debe tener factura y pago registrados.', 1;

        IF @ReponerInventario = 1
        BEGIN
            IF EXISTS (SELECT 1 FROM dbo.detallePedido d
                       LEFT JOIN dbo.inventarioProducto i ON i.idProducto = d.idProducto
                       WHERE d.idPedido = @IdPedido AND d.controlInventario = 1 AND i.idProducto IS NULL)
                THROW 50543, 'Uno de los productos ya no tiene registro de inventario.', 1;

            INSERT dbo.movimientoInventario
                (idProducto, producto, anterior, nuevo, diferencia, idUsuario, usuario, motivo)
            SELECT i.idProducto, p.nombre, i.stockActual, i.stockActual + d.cantidad, d.cantidad,
                @Actor, u.nombreUsuario, N'Anulación de factura ' + @Numero
            FROM dbo.detallePedido d
            JOIN dbo.inventarioProducto i WITH (UPDLOCK) ON i.idProducto = d.idProducto
            JOIN dbo.producto p ON p.idProducto = d.idProducto
            JOIN dbo.usuario u ON u.idUsuario = @Actor
            WHERE d.idPedido = @IdPedido AND d.controlInventario = 1;

            UPDATE i SET stockActual = i.stockActual + d.cantidad, revision = i.revision + 1
            FROM dbo.inventarioProducto i JOIN dbo.detallePedido d ON d.idProducto = i.idProducto
            WHERE d.idPedido = @IdPedido AND d.controlInventario = 1;
        END;

        INSERT dbo.anulacionVenta(idFactura, idUsuario, motivo, repusoInventario)
        VALUES(@IdFactura, @Actor, @Motivo, @ReponerInventario);
        UPDATE dbo.factura SET estado = 'Anulada' WHERE idFactura = @IdFactura;
        UPDATE dbo.pedido SET estado = 'Cancelado' WHERE idPedido = @IdPedido;
        INSERT dbo.historialPedido(idPedido, estadoAnterior, estadoNuevo, idUsuario)
        VALUES(@IdPedido, 'Pagado', 'Cancelado', @Actor);
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE dbo.sp_Cobro_Registrar
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
        IF @Metodo = 'Efectivo' AND @Recibido - @Total > 1000
            THROW 50519, N'El vuelto máximo en efectivo es C$ 1,000.00.', 1;

        DECLARE @Tasa decimal(5,2);
        DECLARE @Impuesto decimal(10,2);
        DECLARE @Restaurante nvarchar(120);
        DECLARE @Moneda varchar(5);
        SELECT @Tasa = tasaImpuesto, @Restaurante = nombre, @Moneda = moneda
        FROM dbo.configuracionRestaurante WHERE id = 1;
        SET @Impuesto = ROUND(@Subtotal - @Subtotal / (1 + @Tasa / 100), 2);
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
             tasaImpuesto, restauranteNombre, moneda, mesaNumero, tipoPedido, origenPedido, tipoEntrega, registradoPor, atendidoPor, direccionEntrega, referenciaEntrega, telefonoContacto)
        VALUES
            (@IdPedido, 'F-' + CONVERT(varchar(20), @IdPedido), GETDATE(), 'Pagada',
             @Subtotal - @Impuesto, @Envio, 0, @Impuesto, 0, @Total, @Nombre, @Cedula,
             @Empleado, @Tasa, @Restaurante, @Moneda, @Mesa, @TipoPedido, @Origen, @Entrega, @Registro, @Atencion, @Direccion, @Referencia, @Telefono);
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
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE dbo.sp_Cobro_Cotizar
    @Actor INT,
    @IdPedido INT
AS
BEGIN
    SET NOCOUNT ON;
    EXEC dbo.sp_Operacion_ExigirPedidoAbierto @Actor, @IdPedido;
    IF NOT EXISTS (SELECT 1
                   FROM dbo.pedido
                   WHERE idPedido = @IdPedido
                         AND estado = 'Entregado')
        THROW 50514, 'El pedido debe estar entregado antes de cobrar.', 1;
    SELECT p.idPedido AS IdPedido,
           CASE WHEN p.idCliente IS NULL THEN 'Consumidor final' ELSE CONCAT(c.pNombreCl, ' ', c.pApellidoCl) END AS Cliente,
           p.subtotal AS Subtotal,
           p.costoEnvio AS CostoEnvio,
           p.total AS Total,
           cfg.tasaImpuesto AS TasaImpuesto,
           CONVERT (DECIMAL (10, 2), ROUND(p.subtotal - p.subtotal / (1 + cfg.tasaImpuesto / 100), 2)) AS Impuestos
    FROM dbo.pedido AS p
         LEFT JOIN dbo.vw_Cliente AS c
         ON c.idCliente = p.idCliente CROSS JOIN dbo.configuracionRestaurante AS cfg
    WHERE p.idPedido = @IdPedido AND p.estado = 'Entregado';
END
GO

-- Datos ficticios para revisar Caja, inventario, facturas y reportes.
-- Cambie estas dos contraseñas si va a usar la base con datos reales.
DECLARE @ClaveAdministrador varchar(255) = 'PBKDF2-SHA256-600000$XB7SyQNtZuYmUjDNGjmE9A==$X9lplXFF1LaYEXYgYUUg7jJ8Ml9NUEFo3iIXXICOue4=';
DECLARE @ClaveDespachador varchar(255) = 'PBKDF2-SHA256-600000$HVfWLJ5pi4BCTCKtOGwiaQ==$1txF6DUY0Q/lzNLRRpv/w10E88wrF+PLj+nJD0mBxrU=';

SET IDENTITY_INSERT dbo.rol ON;
INSERT dbo.rol(idRol, nombre, descripcion) VALUES
    (1, 'Administrador', 'Gestiona el personal, los catálogos, el inventario y las ventas.'),
    (2, 'Empleado', 'Registra, despacha y cobra ventas locales.'),
    (3, 'Cliente', 'Identifica cuentas de versiones anteriores.');
SET IDENTITY_INSERT dbo.rol OFF;

INSERT dbo.configuracionRestaurante(id, nombre, moneda, tasaImpuesto, tarifaEnvio)
VALUES (1, N'RestaurantSM Demo', 'C$', 15.00, NULL);

INSERT dbo.origenPedido(idOrigenPedido, nombre)
VALUES (0, 'Sin registrar'), (1, 'En linea'), (2, 'En el local');
INSERT dbo.tipoEntrega(idTipoEntrega, nombre)
VALUES (0, 'Sin registrar'), (1, 'En mesa'), (2, 'Recoger'), (3, 'Domicilio');

INSERT dbo.persona(primerNombre, primerApellido, genero, cedula, telefono)
VALUES ('Lucia', 'Ejemplo', 'F', '000-010100-0001A', '80000001');
DECLARE @PersonaAdmin int = CONVERT(int, SCOPE_IDENTITY());

INSERT dbo.persona(primerNombre, primerApellido, genero, cedula, telefono)
VALUES ('Mateo', 'Ejemplo', 'M', '000-010100-0002B', '80000002');
DECLARE @PersonaCaja int = CONVERT(int, SCOPE_IDENTITY());

INSERT dbo.usuario(nombreUsuario, contrasena, idRol, idPersona)
VALUES ('admin_demo', @ClaveAdministrador, 1, @PersonaAdmin);
DECLARE @Admin int = CONVERT(int, SCOPE_IDENTITY());

INSERT dbo.usuario(nombreUsuario, contrasena, idRol, idPersona)
VALUES ('caja_demo', @ClaveDespachador, 2, @PersonaCaja);
DECLARE @Caja int = CONVERT(int, SCOPE_IDENTITY());

INSERT dbo.empleado(idUsuario, idPersona, cargo, activo) VALUES
    (@Admin, @PersonaAdmin, 'Administrador', 1),
    (@Caja, @PersonaCaja, 'Despachador', 1);

INSERT dbo.mesa(numero, capacidad) VALUES (1, 2), (2, 4), (3, 4), (4, 6);

INSERT dbo.categoria(nombre, descripcion) VALUES ('Platos principales', 'Opciones para almuerzo y cena.');
DECLARE @Platos int = CONVERT(int, SCOPE_IDENTITY());
INSERT dbo.categoria(nombre, descripcion) VALUES ('Bebidas', 'Bebidas frías preparadas en el local.');
DECLARE @Bebidas int = CONVERT(int, SCOPE_IDENTITY());
INSERT dbo.categoria(nombre, descripcion) VALUES ('Postres', 'Porciones individuales.');
DECLARE @Postres int = CONVERT(int, SCOPE_IDENTITY());

INSERT dbo.producto(idCategoria, nombre, precio, descripcion, codigo) VALUES
    (@Platos, 'Hamburguesa de la casa', 150.00, 'Hamburguesa con papas.', 'DEMO-P01'),
    (@Platos, 'Pollo a la plancha', 175.00, 'Pollo con arroz y ensalada.', 'DEMO-P02'),
    (@Bebidas, 'Limonada natural', 45.00, 'Vaso de limonada.', 'DEMO-B01'),
    (@Bebidas, 'Té frío', 40.00, 'Bebida fría de té.', 'DEMO-B02'),
    (@Postres, 'Tres leches', 70.00, 'Porción de pastel.', 'DEMO-D01'),
    (@Postres, 'Flan de vainilla', 60.00, 'Porción individual.', 'DEMO-D02');

INSERT dbo.inventarioProducto(idProducto, stockActual, stockMinimo)
SELECT idProducto, CASE codigo WHEN 'DEMO-D01' THEN 4 WHEN 'DEMO-D02' THEN 0 ELSE 30 END, 5
FROM dbo.producto WHERE codigo LIKE 'DEMO-%';

-- Tres ejemplos de hoy. Se registran con las mismas reglas que usa la aplicación.
DECLARE @VentasDemo TABLE
(
    numero int PRIMARY KEY,
    codigo varchar(20),
    cantidad int,
    metodo varchar(40),
    anular bit
);
INSERT @VentasDemo VALUES
    (1, 'DEMO-P01', 2, 'Efectivo', 0),
    (2, 'DEMO-D01', 1, 'Tarjeta', 1),
    (3, 'DEMO-B01', 2, NULL, 0);

DECLARE @Numero int = 1;
DECLARE @Producto int, @Cantidad int, @Pedido int, @Anular bit;
DECLARE @Precio decimal(10, 2), @Total decimal(10, 2), @Recibido decimal(10, 2);
DECLARE @Metodo varchar(40), @Solicitud uniqueidentifier;
DECLARE @Registro TABLE (IdPedido int, Reintento bit);
DECLARE @Pago TABLE (IdFactura int, Recibido decimal(10, 2), Cambio decimal(10, 2));

WHILE @Numero <= 3
BEGIN
    SELECT @Producto = p.idProducto, @Precio = p.precio, @Cantidad = v.cantidad,
           @Metodo = v.metodo, @Anular = v.anular
    FROM @VentasDemo v JOIN dbo.producto p ON p.codigo = v.codigo
    WHERE v.numero = @Numero;

    SET @Solicitud = NEWID();
    DELETE FROM @Registro;
    INSERT @Registro EXEC dbo.sp_Pedido_Iniciar @Caja, NULL, @Solicitud, 1;
    SELECT @Pedido = IdPedido FROM @Registro;
    EXEC dbo.sp_Pedido_AgregarLinea @Caja, @Pedido, @Producto, @Cantidad, @Precio, 0;
    EXEC dbo.sp_Pedido_Finalizar @Caja, @Pedido, 1, 0;

    IF @Metodo IS NOT NULL
    BEGIN
        EXEC dbo.sp_Operacion_EstadoPedido @Caja, @Pedido, 'Pendiente', 'Entregado';
        SELECT @Total = total FROM dbo.pedido WHERE idPedido = @Pedido;
        SET @Recibido = CASE WHEN @Metodo = 'Efectivo' THEN @Total + 50 ELSE @Total END;
        SET @Solicitud = NEWID();
        INSERT @Pago EXEC dbo.sp_Cobro_Registrar
            @Caja, @Pedido, @Metodo, @Recibido, @Total, @Solicitud, N'Venta de demostración';

        IF @Anular = 1
            EXEC dbo.sp_Factura_Anular @Admin, @Pedido, N'Devolución de demostración', 1;
    END;
    SET @Numero += 1;
END;

COMMIT TRANSACTION;
PRINT N'RestaurantSM listo con datos de demostración.';
GO
SET NOEXEC OFF;
GO
