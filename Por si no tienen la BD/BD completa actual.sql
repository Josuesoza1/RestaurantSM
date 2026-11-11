-- RestaurantSM | Esquema y datos de la base de origen
-- Abra este archivo en SSMS y ejecútelo completo, sin seleccionar un fragmento.
-- Instancia predeterminada de la aplicación: (localdb)\MSSQLLocalDB.
-- Se requiere permiso para crear una base de datos en la instancia elegida.
-- Contiene datos personales y contraseñas. Compártalo en privado.
-- Si RestaurantSM ya existe, el archivo se detiene antes de modificarla.

USE [master];
GO

IF DB_ID(N'RestaurantSM') IS NOT NULL
BEGIN
    RAISERROR(N'Ya existe RestaurantSM en esta instancia. La base existente se conservó. Ejecute este archivo en una instancia donde no exista RestaurantSM.', 16, 1);
    SET NOEXEC ON;
END;
GO

CREATE DATABASE [RestaurantSM] COLLATE SQL_Latin1_General_CP1_CI_AS;
GO

IF DB_ID(N'RestaurantSM') IS NULL
BEGIN
    RAISERROR(N'No se pudo crear RestaurantSM. Revise los permisos de la cuenta y el mensaje anterior.', 16, 1);
    SET NOEXEC ON;
END;
GO

USE [RestaurantSM];
GO

SET NOCOUNT ON;
SET XACT_ABORT ON;
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
SET ANSI_PADDING ON;
SET ANSI_WARNINGS ON;
SET ARITHABORT ON;
SET CONCAT_NULL_YIELDS_NULL ON;
SET NUMERIC_ROUNDABORT OFF;

BEGIN TRY
    BEGIN TRANSACTION;

    -- Tablas, índices, relaciones, vistas y procedimientos.
    -- Cada definición se compila por separado dentro de la misma transacción.
    EXEC sys.sp_executesql N'CREATE TABLE [dbo].[persona](
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
)';

    EXEC sys.sp_executesql N'CREATE TABLE [dbo].[cliente](
	[idCliente] [int] IDENTITY(1,1) NOT NULL,
	[idUsuario] [int] NULL,
	[activo] [bit] NOT NULL,
	[idPersona] [int] NOT NULL,
 CONSTRAINT [PK_cliente] PRIMARY KEY CLUSTERED 
(
	[idCliente] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF)
)';

    EXEC sys.sp_executesql N'-- Las vistas reunen Persona y su perfil para las consultas existentes.
CREATE   VIEW dbo.vw_Cliente AS
SELECT c.idCliente,c.idUsuario,c.idPersona,c.activo,
 p.primerNombre AS PNombreCl,p.segundoNombre AS SNombreCl,p.primerApellido AS PApellidoCl,
 p.segundoApellido AS SApellidoCl,p.genero,p.cedula,p.telefono,p.email
FROM dbo.cliente c JOIN dbo.persona p ON p.idPersona=c.idPersona;';

    EXEC sys.sp_executesql N'CREATE TABLE [dbo].[empleado](
	[idEmpleado] [int] IDENTITY(1,1) NOT NULL,
	[idUsuario] [int] NULL,
	[cargo] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[activo] [bit] NOT NULL,
	[idPersona] [int] NOT NULL,
 CONSTRAINT [PK_empleado] PRIMARY KEY CLUSTERED 
(
	[idEmpleado] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF)
)';

    EXEC sys.sp_executesql N'CREATE   VIEW dbo.vw_Empleado AS
SELECT e.idEmpleado,e.idUsuario,e.idPersona,e.cargo,e.activo,
 p.primerNombre AS pNombreEmp,p.segundoNombre AS sNombreEmp,p.primerApellido AS pApellidoEmp,
 p.segundoApellido AS sApellidoEmp,p.genero,p.cedula,p.telefono,p.email
FROM dbo.empleado e JOIN dbo.persona p ON p.idPersona=e.idPersona;';

    EXEC sys.sp_executesql N'CREATE TABLE [dbo].[rol](
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
)';

    EXEC sys.sp_executesql N'CREATE TABLE [dbo].[usuario](
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
)';

    EXEC sys.sp_executesql N'CREATE   VIEW dbo.vw_UsuarioAccesos
AS
SELECT u.idUsuario, u.idPersona, u.nombreUsuario, u.idRol, r.nombre AS NombreRol, e.cargo AS Cargo,
    CAST(CASE
        WHEN u.idRol = 1 AND (e.idEmpleado IS NULL OR e.activo = 1) THEN 1
        WHEN u.idRol = 2 AND e.activo = 1
            AND UPPER(LTRIM(RTRIM(e.cargo))) IN (''DESPACHADOR'', ''MESERO'', ''CAJERO'') THEN 1
        ELSE 0 END AS bit) AS AccesoPersonal
FROM dbo.usuario u
JOIN dbo.rol r ON r.idRol = u.idRol
LEFT JOIN dbo.empleado e ON e.idUsuario = u.idUsuario;';

    EXEC sys.sp_executesql N'CREATE TABLE [dbo].[configuracionRestaurante](
	[id] [int] NOT NULL,
	[nombre] [nvarchar](120) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[moneda] [varchar](5) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[tasaImpuesto] [decimal](5, 2) NOT NULL,
	[tarifaEnvio] [decimal](10, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF)
)';

    EXEC sys.sp_executesql N'CREATE TABLE [dbo].[mesa](
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
)';

    EXEC sys.sp_executesql N'CREATE TABLE [dbo].[categoria](
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
)';

    EXEC sys.sp_executesql N'CREATE TABLE [dbo].[origenPedido](
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
)';

    EXEC sys.sp_executesql N'CREATE TABLE [dbo].[tipoEntrega](
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
)';

    EXEC sys.sp_executesql N'CREATE TABLE [dbo].[subcategoria](
	[idSubcategoria] [int] IDENTITY(1,1) NOT NULL,
	[idCategoria] [int] NOT NULL,
	[nombre] [varchar](60) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_IdSubcategoria] PRIMARY KEY CLUSTERED 
(
	[idSubcategoria] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF)
)';

    EXEC sys.sp_executesql N'CREATE TABLE [dbo].[producto](
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
)';

    EXEC sys.sp_executesql N'CREATE TABLE [dbo].[pedido](
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
)';

    EXEC sys.sp_executesql N'CREATE TABLE [dbo].[inventarioProducto](
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
)';

    EXEC sys.sp_executesql N'CREATE TABLE [dbo].[reservacion](
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
)';

    EXEC sys.sp_executesql N'CREATE TABLE [dbo].[movimientoInventario](
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
)';

    EXEC sys.sp_executesql N'CREATE TABLE [dbo].[detallePedido](
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
)';

    EXEC sys.sp_executesql N'CREATE TABLE [dbo].[historialPedido](
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
)';

    EXEC sys.sp_executesql N'CREATE TABLE [dbo].[factura](
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
)';

    EXEC sys.sp_executesql N'CREATE TABLE [dbo].[pago](
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
)';

    EXEC sys.sp_executesql N'CREATE TABLE [dbo].[facturaDetalle](
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
)';

    EXEC sys.sp_executesql N'CREATE TABLE [dbo].[anulacionVenta](
	[idFactura] [int] NOT NULL,
	[idUsuario] [int] NOT NULL,
	[fecha] [datetime2](7) NOT NULL,
	[motivo] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[repusoInventario] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[idFactura] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF)
)';

    EXEC sys.sp_executesql N'CREATE UNIQUE NONCLUSTERED INDEX [UQ_persona_cedula] ON [dbo].[persona]
(
	[cedula] ASC
)
WHERE ([cedula] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF)';

    EXEC sys.sp_executesql N'CREATE UNIQUE NONCLUSTERED INDEX [UQ_persona_email] ON [dbo].[persona]
(
	[email] ASC
)
WHERE ([email] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF)';

    EXEC sys.sp_executesql N'CREATE UNIQUE NONCLUSTERED INDEX [UQ_subcategoria_categoria] ON [dbo].[subcategoria]
(
	[idSubcategoria] ASC,
	[idCategoria] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF)';

    EXEC sys.sp_executesql N'CREATE UNIQUE NONCLUSTERED INDEX [UQ_usuario_cuenta_persona] ON [dbo].[usuario]
(
	[idUsuario] ASC,
	[idPersona] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF)';

    EXEC sys.sp_executesql N'CREATE UNIQUE NONCLUSTERED INDEX [UQ_empleado_persona] ON [dbo].[empleado]
(
	[idPersona] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF)';

    EXEC sys.sp_executesql N'CREATE UNIQUE NONCLUSTERED INDEX [UQ_empleado_usuario] ON [dbo].[empleado]
(
	[idUsuario] ASC
)
WHERE ([idUsuario] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF)';

    EXEC sys.sp_executesql N'CREATE UNIQUE NONCLUSTERED INDEX [UQ_cliente_persona] ON [dbo].[cliente]
(
	[idPersona] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF)';

    EXEC sys.sp_executesql N'CREATE UNIQUE NONCLUSTERED INDEX [UQ_cliente_usuario] ON [dbo].[cliente]
(
	[idUsuario] ASC
)
WHERE ([idUsuario] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF)';

    EXEC sys.sp_executesql N'CREATE UNIQUE NONCLUSTERED INDEX [UQ_pedido_solicitud] ON [dbo].[pedido]
(
	[solicitud] ASC
)
WHERE ([solicitud] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF)';

    EXEC sys.sp_executesql N'CREATE UNIQUE NONCLUSTERED INDEX [UX_pago_solicitud_operacion] ON [dbo].[pago]
(
	[solicitud] ASC
)
WHERE ([solicitud] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF)';

    EXEC sys.sp_executesql N'ALTER TABLE [dbo].[mesa] ADD  DEFAULT (''Disponible'') FOR [estado]';

    EXEC sys.sp_executesql N'ALTER TABLE [dbo].[mesa] ADD  CONSTRAINT [DF_mesa_activa]  DEFAULT ((1)) FOR [activa]';

    EXEC sys.sp_executesql N'ALTER TABLE [dbo].[categoria] ADD  CONSTRAINT [DF_categoria_activa]  DEFAULT ((1)) FOR [activa]';

    EXEC sys.sp_executesql N'ALTER TABLE [dbo].[empleado] ADD  DEFAULT ((1)) FOR [activo]';

    EXEC sys.sp_executesql N'ALTER TABLE [dbo].[producto] ADD  DEFAULT ((1)) FOR [disponible]';

    EXEC sys.sp_executesql N'ALTER TABLE [dbo].[cliente] ADD  CONSTRAINT [DF_cliente_activo]  DEFAULT ((1)) FOR [activo]';

    EXEC sys.sp_executesql N'ALTER TABLE [dbo].[pedido] ADD  DEFAULT (getdate()) FOR [fechaHora]';

    EXEC sys.sp_executesql N'ALTER TABLE [dbo].[pedido] ADD  DEFAULT (''Pendiente'') FOR [estado]';

    EXEC sys.sp_executesql N'ALTER TABLE [dbo].[pedido] ADD  DEFAULT ((0.0)) FOR [costoEnvio]';

    EXEC sys.sp_executesql N'ALTER TABLE [dbo].[pedido] ADD  DEFAULT ((0.0)) FOR [subtotal]';

    EXEC sys.sp_executesql N'ALTER TABLE [dbo].[pedido] ADD  DEFAULT ((0.0)) FOR [total]';

    EXEC sys.sp_executesql N'ALTER TABLE [dbo].[pedido] ADD  DEFAULT ((0)) FOR [esDelivery]';

    EXEC sys.sp_executesql N'ALTER TABLE [dbo].[pedido] ADD  CONSTRAINT [DF_pedido_origen]  DEFAULT ((0)) FOR [idOrigenPedido]';

    EXEC sys.sp_executesql N'ALTER TABLE [dbo].[pedido] ADD  CONSTRAINT [DF_pedido_entrega]  DEFAULT ((0)) FOR [idTipoEntrega]';

    EXEC sys.sp_executesql N'ALTER TABLE [dbo].[inventarioProducto] ADD  DEFAULT ((1)) FOR [activo]';

    EXEC sys.sp_executesql N'ALTER TABLE [dbo].[inventarioProducto] ADD  DEFAULT ((1)) FOR [revision]';

    EXEC sys.sp_executesql N'ALTER TABLE [dbo].[reservacion] ADD  DEFAULT (''Pendiente'') FOR [estado]';

    EXEC sys.sp_executesql N'ALTER TABLE [dbo].[movimientoInventario] ADD  DEFAULT (sysdatetime()) FOR [fecha]';

    EXEC sys.sp_executesql N'ALTER TABLE [dbo].[detallePedido] ADD  CONSTRAINT [DF_detalle_control]  DEFAULT ((0)) FOR [controlInventario]';

    EXEC sys.sp_executesql N'ALTER TABLE [dbo].[historialPedido] ADD  DEFAULT (sysdatetime()) FOR [fecha]';

    EXEC sys.sp_executesql N'ALTER TABLE [dbo].[factura] ADD  DEFAULT (getdate()) FOR [fechaEmision]';

    EXEC sys.sp_executesql N'ALTER TABLE [dbo].[factura] ADD  DEFAULT (''Emitida'') FOR [estado]';

    EXEC sys.sp_executesql N'ALTER TABLE [dbo].[factura] ADD  DEFAULT ((0)) FOR [subtotal]';

    EXEC sys.sp_executesql N'ALTER TABLE [dbo].[factura] ADD  DEFAULT ((0.0)) FOR [costoEnvio]';

    EXEC sys.sp_executesql N'ALTER TABLE [dbo].[factura] ADD  DEFAULT ((0)) FOR [descuento]';

    EXEC sys.sp_executesql N'ALTER TABLE [dbo].[factura] ADD  DEFAULT ((0)) FOR [impuestos]';

    EXEC sys.sp_executesql N'ALTER TABLE [dbo].[factura] ADD  DEFAULT ((0)) FOR [propina]';

    EXEC sys.sp_executesql N'ALTER TABLE [dbo].[pago] ADD  DEFAULT (getdate()) FOR [fechaPago]';

    EXEC sys.sp_executesql N'ALTER TABLE [dbo].[anulacionVenta] ADD  DEFAULT (sysdatetime()) FOR [fecha]';

    EXEC sys.sp_executesql N'ALTER TABLE [dbo].[subcategoria]  WITH CHECK ADD  CONSTRAINT [FK_subcategoria_categoria] FOREIGN KEY([idCategoria])
REFERENCES [dbo].[categoria] ([idCategoria])';

    EXEC sys.sp_executesql N'ALTER TABLE [dbo].[subcategoria] CHECK CONSTRAINT [FK_subcategoria_categoria]';

    EXEC sys.sp_executesql N'ALTER TABLE [dbo].[usuario]  WITH CHECK ADD  CONSTRAINT [FK_usuario_persona] FOREIGN KEY([idPersona])
REFERENCES [dbo].[persona] ([idPersona])';

    EXEC sys.sp_executesql N'ALTER TABLE [dbo].[usuario] CHECK CONSTRAINT [FK_usuario_persona]';

    EXEC sys.sp_executesql N'ALTER TABLE [dbo].[usuario]  WITH CHECK ADD  CONSTRAINT [FK_usuario_rol] FOREIGN KEY([idRol])
REFERENCES [dbo].[rol] ([idRol])';

    EXEC sys.sp_executesql N'ALTER TABLE [dbo].[usuario] CHECK CONSTRAINT [FK_usuario_rol]';

    EXEC sys.sp_executesql N'ALTER TABLE [dbo].[empleado]  WITH CHECK ADD  CONSTRAINT [FK_empleado_cuenta_persona] FOREIGN KEY([idUsuario], [idPersona])
REFERENCES [dbo].[usuario] ([idUsuario], [idPersona])';

    EXEC sys.sp_executesql N'ALTER TABLE [dbo].[empleado] CHECK CONSTRAINT [FK_empleado_cuenta_persona]';

    EXEC sys.sp_executesql N'ALTER TABLE [dbo].[empleado]  WITH CHECK ADD  CONSTRAINT [FK_empleado_persona] FOREIGN KEY([idPersona])
REFERENCES [dbo].[persona] ([idPersona])';

    EXEC sys.sp_executesql N'ALTER TABLE [dbo].[empleado] CHECK CONSTRAINT [FK_empleado_persona]';

    EXEC sys.sp_executesql N'ALTER TABLE [dbo].[producto]  WITH CHECK ADD  CONSTRAINT [FK_IdCategoriaProd] FOREIGN KEY([idCategoria])
REFERENCES [dbo].[categoria] ([idCategoria])';

    EXEC sys.sp_executesql N'ALTER TABLE [dbo].[producto] CHECK CONSTRAINT [FK_IdCategoriaProd]';

    EXEC sys.sp_executesql N'ALTER TABLE [dbo].[producto]  WITH CHECK ADD  CONSTRAINT [FK_producto_subcategoria_categoria] FOREIGN KEY([idSubcategoria], [idCategoria])
REFERENCES [dbo].[subcategoria] ([idSubcategoria], [idCategoria])';

    EXEC sys.sp_executesql N'ALTER TABLE [dbo].[producto] CHECK CONSTRAINT [FK_producto_subcategoria_categoria]';

    EXEC sys.sp_executesql N'ALTER TABLE [dbo].[cliente]  WITH CHECK ADD  CONSTRAINT [FK_cliente_cuenta_persona] FOREIGN KEY([idUsuario], [idPersona])
REFERENCES [dbo].[usuario] ([idUsuario], [idPersona])';

    EXEC sys.sp_executesql N'ALTER TABLE [dbo].[cliente] CHECK CONSTRAINT [FK_cliente_cuenta_persona]';

    EXEC sys.sp_executesql N'ALTER TABLE [dbo].[cliente]  WITH CHECK ADD  CONSTRAINT [FK_cliente_persona] FOREIGN KEY([idPersona])
REFERENCES [dbo].[persona] ([idPersona])';

    EXEC sys.sp_executesql N'ALTER TABLE [dbo].[cliente] CHECK CONSTRAINT [FK_cliente_persona]';

    EXEC sys.sp_executesql N'ALTER TABLE [dbo].[pedido]  WITH CHECK ADD  CONSTRAINT [FK_pedido_cliente] FOREIGN KEY([idCliente])
REFERENCES [dbo].[cliente] ([idCliente])';

    EXEC sys.sp_executesql N'ALTER TABLE [dbo].[pedido] CHECK CONSTRAINT [FK_pedido_cliente]';

    EXEC sys.sp_executesql N'ALTER TABLE [dbo].[pedido]  WITH CHECK ADD  CONSTRAINT [FK_pedido_empleado] FOREIGN KEY([idEmpleado])
REFERENCES [dbo].[empleado] ([idEmpleado])';

    EXEC sys.sp_executesql N'ALTER TABLE [dbo].[pedido] CHECK CONSTRAINT [FK_pedido_empleado]';

    EXEC sys.sp_executesql N'ALTER TABLE [dbo].[pedido]  WITH CHECK ADD  CONSTRAINT [FK_pedido_entrega] FOREIGN KEY([idTipoEntrega])
REFERENCES [dbo].[tipoEntrega] ([idTipoEntrega])';

    EXEC sys.sp_executesql N'ALTER TABLE [dbo].[pedido] CHECK CONSTRAINT [FK_pedido_entrega]';

    EXEC sys.sp_executesql N'ALTER TABLE [dbo].[pedido]  WITH CHECK ADD  CONSTRAINT [FK_pedido_mesa] FOREIGN KEY([idMesa])
REFERENCES [dbo].[mesa] ([idMesa])';

    EXEC sys.sp_executesql N'ALTER TABLE [dbo].[pedido] CHECK CONSTRAINT [FK_pedido_mesa]';

    EXEC sys.sp_executesql N'ALTER TABLE [dbo].[pedido]  WITH CHECK ADD  CONSTRAINT [FK_pedido_origen] FOREIGN KEY([idOrigenPedido])
REFERENCES [dbo].[origenPedido] ([idOrigenPedido])';

    EXEC sys.sp_executesql N'ALTER TABLE [dbo].[pedido] CHECK CONSTRAINT [FK_pedido_origen]';

    EXEC sys.sp_executesql N'ALTER TABLE [dbo].[pedido]  WITH CHECK ADD  CONSTRAINT [FK_pedido_registro] FOREIGN KEY([idUsuarioRegistro])
REFERENCES [dbo].[usuario] ([idUsuario])';

    EXEC sys.sp_executesql N'ALTER TABLE [dbo].[pedido] CHECK CONSTRAINT [FK_pedido_registro]';

    EXEC sys.sp_executesql N'ALTER TABLE [dbo].[pedido]  WITH CHECK ADD  CONSTRAINT [FK_pedido_repartidor] FOREIGN KEY([idEmpleadoRepartidor])
REFERENCES [dbo].[empleado] ([idEmpleado])';

    EXEC sys.sp_executesql N'ALTER TABLE [dbo].[pedido] CHECK CONSTRAINT [FK_pedido_repartidor]';

    EXEC sys.sp_executesql N'ALTER TABLE [dbo].[inventarioProducto]  WITH CHECK ADD FOREIGN KEY([idProducto])
REFERENCES [dbo].[producto] ([idProducto])';

    EXEC sys.sp_executesql N'ALTER TABLE [dbo].[reservacion]  WITH CHECK ADD  CONSTRAINT [FK_idClienteReservacion] FOREIGN KEY([idCliente])
REFERENCES [dbo].[cliente] ([idCliente])';

    EXEC sys.sp_executesql N'ALTER TABLE [dbo].[reservacion] CHECK CONSTRAINT [FK_idClienteReservacion]';

    EXEC sys.sp_executesql N'ALTER TABLE [dbo].[reservacion]  WITH CHECK ADD  CONSTRAINT [FK_idMesaReservacion] FOREIGN KEY([idMesa])
REFERENCES [dbo].[mesa] ([idMesa])';

    EXEC sys.sp_executesql N'ALTER TABLE [dbo].[reservacion] CHECK CONSTRAINT [FK_idMesaReservacion]';

    EXEC sys.sp_executesql N'ALTER TABLE [dbo].[movimientoInventario]  WITH CHECK ADD  CONSTRAINT [FK_movimientoInventario_producto] FOREIGN KEY([idProducto])
REFERENCES [dbo].[producto] ([idProducto])';

    EXEC sys.sp_executesql N'ALTER TABLE [dbo].[movimientoInventario] CHECK CONSTRAINT [FK_movimientoInventario_producto]';

    EXEC sys.sp_executesql N'ALTER TABLE [dbo].[movimientoInventario]  WITH CHECK ADD  CONSTRAINT [FK_movimientoInventario_usuario] FOREIGN KEY([idUsuario])
REFERENCES [dbo].[usuario] ([idUsuario])';

    EXEC sys.sp_executesql N'ALTER TABLE [dbo].[movimientoInventario] CHECK CONSTRAINT [FK_movimientoInventario_usuario]';

    EXEC sys.sp_executesql N'ALTER TABLE [dbo].[detallePedido]  WITH CHECK ADD  CONSTRAINT [FK_IdPedidoDP] FOREIGN KEY([idPedido])
REFERENCES [dbo].[pedido] ([idPedido])';

    EXEC sys.sp_executesql N'ALTER TABLE [dbo].[detallePedido] CHECK CONSTRAINT [FK_IdPedidoDP]';

    EXEC sys.sp_executesql N'ALTER TABLE [dbo].[detallePedido]  WITH CHECK ADD  CONSTRAINT [FK_IdProductoDP] FOREIGN KEY([idProducto])
REFERENCES [dbo].[producto] ([idProducto])';

    EXEC sys.sp_executesql N'ALTER TABLE [dbo].[detallePedido] CHECK CONSTRAINT [FK_IdProductoDP]';

    EXEC sys.sp_executesql N'ALTER TABLE [dbo].[historialPedido]  WITH CHECK ADD FOREIGN KEY([idPedido])
REFERENCES [dbo].[pedido] ([idPedido])';

    EXEC sys.sp_executesql N'ALTER TABLE [dbo].[historialPedido]  WITH CHECK ADD FOREIGN KEY([idUsuario])
REFERENCES [dbo].[usuario] ([idUsuario])';

    EXEC sys.sp_executesql N'ALTER TABLE [dbo].[factura]  WITH CHECK ADD  CONSTRAINT [FK_idPedidoFact] FOREIGN KEY([idPedido])
REFERENCES [dbo].[pedido] ([idPedido])';

    EXEC sys.sp_executesql N'ALTER TABLE [dbo].[factura] CHECK CONSTRAINT [FK_idPedidoFact]';

    EXEC sys.sp_executesql N'ALTER TABLE [dbo].[pago]  WITH CHECK ADD  CONSTRAINT [FK_idFactura] FOREIGN KEY([idFactura])
REFERENCES [dbo].[factura] ([idFactura])';

    EXEC sys.sp_executesql N'ALTER TABLE [dbo].[pago] CHECK CONSTRAINT [FK_idFactura]';

    EXEC sys.sp_executesql N'ALTER TABLE [dbo].[pago]  WITH CHECK ADD  CONSTRAINT [FK_pago_usuario] FOREIGN KEY([idUsuario])
REFERENCES [dbo].[usuario] ([idUsuario])';

    EXEC sys.sp_executesql N'ALTER TABLE [dbo].[pago] CHECK CONSTRAINT [FK_pago_usuario]';

    EXEC sys.sp_executesql N'ALTER TABLE [dbo].[facturaDetalle]  WITH CHECK ADD FOREIGN KEY([idFactura])
REFERENCES [dbo].[factura] ([idFactura])';

    EXEC sys.sp_executesql N'ALTER TABLE [dbo].[facturaDetalle]  WITH CHECK ADD  CONSTRAINT [FK_facturaDetalle_producto] FOREIGN KEY([idProducto])
REFERENCES [dbo].[producto] ([idProducto])';

    EXEC sys.sp_executesql N'ALTER TABLE [dbo].[facturaDetalle] CHECK CONSTRAINT [FK_facturaDetalle_producto]';

    EXEC sys.sp_executesql N'ALTER TABLE [dbo].[anulacionVenta]  WITH CHECK ADD FOREIGN KEY([idFactura])
REFERENCES [dbo].[factura] ([idFactura])';

    EXEC sys.sp_executesql N'ALTER TABLE [dbo].[anulacionVenta]  WITH CHECK ADD FOREIGN KEY([idUsuario])
REFERENCES [dbo].[usuario] ([idUsuario])';

    EXEC sys.sp_executesql N'ALTER TABLE [dbo].[persona]  WITH CHECK ADD  CONSTRAINT [CK_persona_genero] CHECK  (([genero]=''O'' OR [genero]=''F'' OR [genero]=''M''))';

    EXEC sys.sp_executesql N'ALTER TABLE [dbo].[persona] CHECK CONSTRAINT [CK_persona_genero]';

    EXEC sys.sp_executesql N'ALTER TABLE [dbo].[configuracionRestaurante]  WITH CHECK ADD CHECK  (([tasaImpuesto]>=(0) AND [tasaImpuesto]<=(100)))';

    EXEC sys.sp_executesql N'ALTER TABLE [dbo].[configuracionRestaurante]  WITH CHECK ADD CHECK  (([id]=(1)))';

    EXEC sys.sp_executesql N'ALTER TABLE [dbo].[configuracionRestaurante]  WITH CHECK ADD  CONSTRAINT [CK_configuracion_tarifaEnvio] CHECK  (([tarifaEnvio]>=(0)))';

    EXEC sys.sp_executesql N'ALTER TABLE [dbo].[configuracionRestaurante] CHECK CONSTRAINT [CK_configuracion_tarifaEnvio]';

    EXEC sys.sp_executesql N'ALTER TABLE [dbo].[mesa]  WITH CHECK ADD CHECK  (([capacidad]>=(1) AND [capacidad]<=(20)))';

    EXEC sys.sp_executesql N'ALTER TABLE [dbo].[mesa]  WITH CHECK ADD  CONSTRAINT [CK_Estado] CHECK  ((([estado]=''No disponible'' OR [estado]=''Disponible'') AND ([activa]=(1) OR [estado]=''No disponible'')))';

    EXEC sys.sp_executesql N'ALTER TABLE [dbo].[mesa] CHECK CONSTRAINT [CK_Estado]';

    EXEC sys.sp_executesql N'ALTER TABLE [dbo].[mesa]  WITH CHECK ADD  CONSTRAINT [CK_Mesa_Numero] CHECK  (([numero]>=(1) AND [numero]<=(999)))';

    EXEC sys.sp_executesql N'ALTER TABLE [dbo].[mesa] CHECK CONSTRAINT [CK_Mesa_Numero]';

    EXEC sys.sp_executesql N'ALTER TABLE [dbo].[producto]  WITH CHECK ADD CHECK  (([precio]>(0)))';

    EXEC sys.sp_executesql N'ALTER TABLE [dbo].[pedido]  WITH CHECK ADD  CONSTRAINT [CK_pedido_estado] CHECK  (([estado]=''Cancelado'' OR [estado]=''Pagado'' OR [estado]=''Entregado'' OR [estado]=''Listo'' OR [estado]=''En preparación'' OR [estado]=''Pendiente''))';

    EXEC sys.sp_executesql N'ALTER TABLE [dbo].[pedido] CHECK CONSTRAINT [CK_pedido_estado]';

    EXEC sys.sp_executesql N'ALTER TABLE [dbo].[inventarioProducto]  WITH CHECK ADD CHECK  (([stockActual]>=(0)))';

    EXEC sys.sp_executesql N'ALTER TABLE [dbo].[inventarioProducto]  WITH CHECK ADD CHECK  (([stockMinimo]>=(0)))';

    EXEC sys.sp_executesql N'ALTER TABLE [dbo].[reservacion]  WITH CHECK ADD CHECK  (([cantidadPersonas]>(0)))';

    EXEC sys.sp_executesql N'ALTER TABLE [dbo].[reservacion]  WITH CHECK ADD  CONSTRAINT [CK_estadoReservacion] CHECK  (([estado]=''No asistio'' OR [estado]=''Atendida'' OR [estado]=''Cancelada'' OR [estado]=''Completada'' OR [estado]=''Confirmada'' OR [estado]=''Pendiente''))';

    EXEC sys.sp_executesql N'ALTER TABLE [dbo].[reservacion] CHECK CONSTRAINT [CK_estadoReservacion]';

    EXEC sys.sp_executesql N'ALTER TABLE [dbo].[detallePedido]  WITH CHECK ADD CHECK  (([cantidad]>(0)))';

    EXEC sys.sp_executesql N'ALTER TABLE [dbo].[factura]  WITH CHECK ADD CHECK  (([total]>(0)))';

    EXEC sys.sp_executesql N'ALTER TABLE [dbo].[pago]  WITH CHECK ADD CHECK  (([monto]>(0)))';

    EXEC sys.sp_executesql N'ALTER TABLE [dbo].[pago]  WITH CHECK ADD  CONSTRAINT [CK_pago_metodo] CHECK  (([metodoDePago]=''Otro'' OR [metodoDePago]=''Transferencia'' OR [metodoDePago]=''Tarjeta'' OR [metodoDePago]=''Efectivo''))';

    EXEC sys.sp_executesql N'ALTER TABLE [dbo].[pago] CHECK CONSTRAINT [CK_pago_metodo]';

    EXEC sys.sp_executesql N'-- Este auxiliar crea datos personales; el procedimiento de registro controla su perfil y cuenta.
CREATE   PROCEDURE dbo.sp_Persona_Crear
 @PrimerNombre varchar(80),@SegundoNombre varchar(80),@PrimerApellido varchar(80),@SegundoApellido varchar(80),
 @Genero char(1),@Cedula varchar(30),@Telefono varchar(20),@Email varchar(120),@IdPersona int OUTPUT
AS
BEGIN
 SET NOCOUNT ON;
 IF NULLIF(LTRIM(RTRIM(@PrimerNombre)),'''') IS NULL OR NULLIF(LTRIM(RTRIM(@PrimerApellido)),'''') IS NULL
  THROW 50902,''Ingrese el nombre y apellido de la persona.'',1;
 INSERT dbo.persona(primerNombre,segundoNombre,primerApellido,segundoApellido,genero,cedula,telefono,email)
 VALUES(@PrimerNombre,NULLIF(@SegundoNombre,''''),@PrimerApellido,NULLIF(@SegundoApellido,''''),@Genero,NULLIF(@Cedula,''''),NULLIF(@Telefono,''''),NULLIF(@Email,''''));
 SET @IdPersona=CONVERT(int,SCOPE_IDENTITY());
END;';

    EXEC sys.sp_executesql N'-- Procedimiento sp_Mesa_Listar: consulta o modifica los datos de su gestion con parametros definidos.
CREATE   PROCEDURE dbo.sp_Mesa_Listar
AS
SELECT idMesa AS IdMesa,
       numero AS Numero,
       capacidad AS Capacidad,
       estado AS Estado,
       activa AS Activa
FROM dbo.mesa
ORDER BY numero;';

    EXEC sys.sp_executesql N'CREATE   PROCEDURE dbo.sp_Configuracion_Obtener
AS
BEGIN
    SET NOCOUNT ON;
    SELECT nombre AS Nombre, moneda AS Moneda, tasaImpuesto AS TasaImpuesto
    FROM dbo.configuracionRestaurante WHERE id = 1;
END;';

    EXEC sys.sp_executesql N'-- Procedimiento sp_Categoria_Listar: consulta o modifica los datos de su gestion con parametros definidos.
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
END';

    EXEC sys.sp_executesql N'-- Procedimiento sp_Rol_Listar: consulta o modifica los datos de su gestion con parametros definidos.
CREATE   PROCEDURE sp_Rol_Listar
AS
BEGIN
    SET NOCOUNT ON;
    SELECT idRol,
           nombre,
           descripcion
    FROM rol;
END';

    EXEC sys.sp_executesql N'-- Procedimiento sp_Subcategoria_Listar: consulta o modifica los datos de su gestion con parametros definidos.
CREATE   PROCEDURE dbo.sp_Subcategoria_Listar
AS
BEGIN
    SET NOCOUNT ON;
    SELECT idSubcategoria,
           idCategoria,
           nombre
    FROM dbo.subcategoria
    ORDER BY nombre;
END';

    EXEC sys.sp_executesql N'-- Procedimiento sp_Producto_Listar: consulta o modifica los datos de su gestion con parametros definidos.
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
END';

    EXEC sys.sp_executesql N'CREATE   PROCEDURE dbo.sp_Producto_Disponibles
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
END;';

    EXEC sys.sp_executesql N'-- Procedimiento sp_Empleado_Desactivar: consulta o modifica los datos de su gestion con parametros definidos.
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
        THROW 50010, ''Solo un administrador activo puede gestionar empleados.'', 1;
    BEGIN TRY
        BEGIN TRANSACTION;
        IF NOT EXISTS (SELECT 1
                       FROM dbo.vw_Empleado
                       WHERE idEmpleado = @IdEmpleado)
            THROW 50013, ''El empleado seleccionado ya no existe.'', 1;
        IF EXISTS (SELECT 1
                   FROM dbo.vw_Empleado
                   WHERE idEmpleado = @IdEmpleado
                         AND idUsuario = @IdAdministrador)
            THROW 50014, ''No puede desactivar su propia cuenta.'', 1;
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
END';

    EXEC sys.sp_executesql N'CREATE   PROCEDURE dbo.sp_Empleado_Actualizar
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
        THROW 50010, ''Solo un administrador activo puede gestionar empleados.'', 1;
    IF @IdRol IS NULL
       OR @IdRol NOT IN (1, 2)
        THROW 50011, ''El rol debe ser Administrador o Empleado.'', 1;
    IF @IdRol = 2 AND (@Cargo IS NULL OR UPPER(LTRIM(RTRIM(@Cargo))) <> ''DESPACHADOR'')
        THROW 50802, ''Seleccione Despachador como cargo del empleado.'', 1;
    IF @IdRol = 2
        SET @Cargo = ''Despachador'';
    BEGIN TRY
        BEGIN TRANSACTION;
        DECLARE @IdUsuario AS INT,@PersonaActual int;
        SELECT @PersonaActual=idPersona FROM dbo.vw_Empleado WHERE idEmpleado=@IdEmpleado;
        IF @IdPersona<>0 AND @IdPersona<>@PersonaActual
            THROW 50906,''No puede reasignar el perfil a otra persona desde una edicion.'',1;
        IF NOT EXISTS (SELECT 1
                       FROM dbo.vw_Empleado
                       WHERE idEmpleado = @IdEmpleado)
            THROW 50013, ''El empleado seleccionado ya no existe.'', 1;
        SELECT @IdUsuario = idUsuario
        FROM dbo.vw_Empleado
        WHERE idEmpleado = @IdEmpleado;
        IF @IdUsuario = @IdAdministrador
           AND (@Activo = 0
                OR @IdRol <> 1)
            THROW 50014, ''No puede desactivar su propia cuenta ni quitarse el rol de administrador.'', 1;
        IF @IdUsuario IS NULL
            BEGIN
                IF NULLIF (LTRIM(RTRIM(@Contrasena)), '''') IS NULL
                    THROW 50012, ''Este empleado no tiene cuenta: ingrese una contraseña para crearla.'', 1;
                INSERT INTO dbo.usuario (nombreUsuario, contrasena, idRol, idPersona)
                VALUES                 (@Usuario, @Contrasena, @IdRol, @PersonaActual);
                SET @IdUsuario = CONVERT (INT, SCOPE_IDENTITY());
            END
        ELSE
            BEGIN
                UPDATE dbo.usuario
                SET nombreUsuario = @Usuario,
                    idRol = @IdRol,
                    contrasena = COALESCE (NULLIF (@Contrasena, ''''), contrasena)
                WHERE idUsuario = @IdUsuario;
            END
        UPDATE dbo.persona SET primerNombre=@PrimerNombre,segundoNombre=NULLIF(@SegundoNombre,''''),
            primerApellido=@PrimerApellido,segundoApellido=NULLIF(@SegundoApellido,''''),genero=@Genero,
            cedula=@Cedula,telefono=@Telefono WHERE idPersona=@PersonaActual;
        UPDATE dbo.empleado SET idUsuario=@IdUsuario,cargo=@Cargo,activo=@Activo WHERE idEmpleado=@IdEmpleado;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK;
        THROW;
    END CATCH
END';

    EXEC sys.sp_executesql N'-- Procedimiento sp_Catalogo_ExigirAdministrador: consulta o modifica los datos de su gestion con parametros definidos.
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
        THROW 50020, ''Solo un administrador activo puede modificar el catálogo.'', 1;
END';

    EXEC sys.sp_executesql N'CREATE   PROCEDURE dbo.sp_Operacion_ExigirPersonal @Actor int
AS
BEGIN
    SET NOCOUNT ON;
    IF NOT EXISTS (
        SELECT 1 FROM dbo.vw_UsuarioAccesos WHERE idUsuario = @Actor AND AccesoPersonal = 1
    )
        THROW 50501, ''Esta operación requiere una cuenta activa del personal.'', 1;
END;';

    EXEC sys.sp_executesql N'-- Registra el usuario, el producto, las unidades y el motivo recibidos por parametros.
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
    IF @@TRANCOUNT = 0 THROW 50720, ''El movimiento debe formar parte de una transaccion.'', 1;
    DECLARE @Nuevo int;
    IF @Tipo = ''Entrada'' SET @Nuevo = @Anterior + @Cantidad;
    ELSE SET @Nuevo = @Anterior - @Cantidad;
    INSERT INTO dbo.movimientoInventario
        (idProducto, producto, anterior, nuevo, diferencia, idUsuario, usuario, motivo)
    SELECT p.idProducto, p.nombre, @Anterior, @Nuevo, @Nuevo - @Anterior,
        @Actor, u.nombreUsuario, @Motivo
    FROM dbo.producto p
    INNER JOIN dbo.usuario u ON u.idUsuario = @Actor
    WHERE p.idProducto = @IdProducto;
END;';

    EXEC sys.sp_executesql N'CREATE   PROCEDURE dbo.sp_Operacion_Mesas
    @Actor INT
AS
BEGIN
    SET NOCOUNT ON;
    EXECUTE dbo.sp_Operacion_ExigirPersonal @Actor;
    SELECT idMesa AS IdMesa,
           CONCAT(''Mesa '', numero, '' - '', estado) AS Nombre,
           numero AS Numero,
           estado AS Estado
    FROM dbo.mesa
    WHERE activa = 1
    ORDER BY numero;
END';

    EXEC sys.sp_executesql N'-- Procedimiento sp_Rol_Guardar: consulta o modifica los datos de su gestion con parametros definidos.
CREATE   PROCEDURE dbo.sp_Rol_Guardar
    @Actor INT,
    @Id INT=0,
    @Nombre VARCHAR (50),
    @Descripcion VARCHAR (150)=NULL
AS
BEGIN
    EXECUTE dbo.sp_Catalogo_ExigirAdministrador @Actor;
    IF NULLIF (LTRIM(RTRIM(@Nombre)), '''') IS NULL
        THROW 50204, ''El nombre del rol es obligatorio.'', 1;
    IF @Id IN (1, 2, 3)
       AND EXISTS (SELECT 1
                   FROM dbo.rol
                   WHERE idRol = @Id
                         AND nombre <> @Nombre)
        THROW 50205, ''Los roles del sistema no se pueden renombrar; puede editar su descripción.'', 1;
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
                THROW 50206, ''El rol ya no existe.'', 1;
            SELECT @Id;
        END
END';

    EXEC sys.sp_executesql N'-- Procedimiento sp_Rol_Eliminar: consulta o modifica los datos de su gestion con parametros definidos.
CREATE   PROCEDURE dbo.sp_Rol_Eliminar
    @Actor INT,
    @Id INT
AS
BEGIN
    EXECUTE dbo.sp_Catalogo_ExigirAdministrador @Actor;
    IF @Id IN (1, 2, 3)
        THROW 50207, ''Administrador, Empleado y Cliente son roles protegidos.'', 1;
    IF EXISTS (SELECT 1
               FROM dbo.usuario
               WHERE idRol = @Id)
        THROW 50208, ''El rol tiene usuarios asociados.'', 1;
    DELETE dbo.rol
    WHERE idRol = @Id;
    IF @@ROWCOUNT = 0
        THROW 50206, ''El rol ya no existe.'', 1;
END';

    EXEC sys.sp_executesql N'-- Procedimiento sp_Categoria_Guardar: consulta o modifica los datos de su gestion con parametros definidos.
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
    IF NULLIF (LTRIM(RTRIM(@Nombre)), '''') IS NULL
        THROW 50021, ''El nombre de la categoría es obligatorio.'', 1;
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
                    THROW 50022, ''La categoría seleccionada ya no existe.'', 1;
                IF @Activa = 0
                   AND EXISTS (SELECT 1
                               FROM dbo.producto
                               WHERE idCategoria = @IdCategoria
                                     AND disponible = 1)
                    THROW 50023, ''Primero marque como no disponibles los productos de esta categoría.'', 1;
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
END';

    EXEC sys.sp_executesql N'-- Procedimiento sp_Categoria_Eliminar: consulta o modifica los datos de su gestion con parametros definidos.
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
            THROW 50400, ''La categoría ya no existe. Recargue el listado.'', 1;
        IF EXISTS (SELECT 1
                   FROM dbo.producto
                   WHERE idCategoria = @IdCategoria)
           OR EXISTS (SELECT 1
                      FROM dbo.subcategoria
                      WHERE idCategoria = @IdCategoria)
            THROW 50401, ''La categoría tiene productos o subcategorías. Conserve sus relaciones y utilice Desactivar.'', 1;
        DELETE dbo.categoria
        WHERE idCategoria = @IdCategoria;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK;
        THROW;
    END CATCH
END';

    EXEC sys.sp_executesql N'-- Procedimiento sp_Categoria_Desactivar: consulta o modifica los datos de su gestion con parametros definidos.
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
            THROW 50022, ''La categoría seleccionada ya no existe.'', 1;
        IF EXISTS (SELECT 1
                   FROM dbo.producto
                   WHERE idCategoria = @IdCategoria
                         AND disponible = 1)
            THROW 50023, ''Primero marque como no disponibles los productos de esta categoría.'', 1;
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
END';

    EXEC sys.sp_executesql N'CREATE   PROCEDURE dbo.sp_Configuracion_Guardar
    @Actor INT,
    @Nombre NVARCHAR (120),
    @Tasa DECIMAL (5, 2)
AS
BEGIN
    SET NOCOUNT ON;
    EXECUTE dbo.sp_Catalogo_ExigirAdministrador @Actor;
    IF NULLIF (LTRIM(RTRIM(@Nombre)), '''') IS NULL
       OR @Tasa IS NULL
       OR @Tasa < 0
       OR @Tasa > 100
        THROW 50530, ''Indique nombre y una tasa incluida entre 0 y 100.'', 1;
    UPDATE dbo.configuracionRestaurante
    SET nombre = LTRIM(RTRIM(@Nombre)),
        tasaImpuesto = @Tasa
    WHERE id = 1;
END';

    EXEC sys.sp_executesql N'-- Procedimiento sp_Inventario_Movimientos: consulta o modifica los datos de su gestion con parametros definidos.
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
END';

    EXEC sys.sp_executesql N'-- Procedimiento sp_Inventario_Listar: consulta o modifica los datos de su gestion con parametros definidos.
CREATE   PROCEDURE dbo.sp_Inventario_Listar
    @Actor INT
AS
BEGIN
    EXECUTE dbo.sp_Catalogo_ExigirAdministrador @Actor;
    SELECT p.idProducto AS IdProducto,
           p.nombre AS Producto,
           c.nombre AS Categoria,
           p.precio AS PrecioVenta,
           ''Unidad'' AS Unidad,
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
END';

    EXEC sys.sp_executesql N'-- Crea o modifica inventario. Revision es un contador entero que aumenta en cada cambio.
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
    IF @Stock < 0 OR @Minimo < 0 THROW 50213, ''Las existencias no pueden ser negativas.'', 1;
    BEGIN TRY
        BEGIN TRANSACTION;
        IF NOT EXISTS (SELECT 1 FROM dbo.producto WHERE idProducto = @IdProducto)
            THROW 50214, ''Seleccione un producto existente.'', 1;
        IF @Activo = 0 AND (@Stock <> 0 OR EXISTS (
            SELECT 1 FROM dbo.detallePedido d INNER JOIN dbo.pedido p ON p.idPedido = d.idPedido
            WHERE d.idProducto = @IdProducto AND d.controlInventario = 1 AND p.estado NOT IN (''Pagado'', ''Cancelado'')))
            THROW 50215, ''Retire el control solo con stock cero y sin pedidos abiertos.'', 1;

        DECLARE @Anterior int;
        SELECT @Anterior = stockActual FROM dbo.inventarioProducto WHERE idProducto = @IdProducto;
        IF @Anterior IS NULL
        BEGIN
            IF @Revision <> 0 THROW 50216, ''El inventario cambio. Recargue el listado.'', 1;
            INSERT INTO dbo.inventarioProducto(idProducto, stockActual, stockMinimo, activo, revision)
            VALUES (@IdProducto, @Stock, @Minimo, @Activo, 1);
            EXEC dbo.sp_Inventario_RegistrarMovimiento @Actor, @IdProducto, 0, @Stock, ''Entrada'', ''Alta de inventario'';
        END
        ELSE
        BEGIN
            -- Revision es un contador: 1, 2, 3... Cada cambio suma uno.
            UPDATE dbo.inventarioProducto
            SET stockActual = @Stock, stockMinimo = @Minimo, activo = @Activo, revision = revision + 1
            WHERE idProducto = @IdProducto AND revision = @Revision;
            IF @@ROWCOUNT = 0 THROW 50216, ''El inventario cambio. Recargue antes de guardar.'', 1;
            IF @Stock <> @Anterior
            BEGIN
                DECLARE @Cantidad int;
                DECLARE @Tipo varchar(20);
                SET @Cantidad = ABS(@Stock - @Anterior);
                IF @Stock > @Anterior SET @Tipo = ''Entrada''; ELSE SET @Tipo = ''Salida'';
                EXEC dbo.sp_Inventario_RegistrarMovimiento @Actor, @IdProducto, @Anterior,
                    @Cantidad, @Tipo, ''Ajuste desde gestion de inventario'';
            END;
        END;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;';

    EXEC sys.sp_executesql N'-- Elimina el control solo si no quedan unidades ni pedidos abiertos.
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
            WHERE d.idProducto = @IdProducto AND d.controlInventario = 1 AND p.estado NOT IN (''Pagado'', ''Cancelado''))
            THROW 50529, ''El producto tiene pedidos abiertos.'', 1;
        DELETE FROM dbo.inventarioProducto
        WHERE idProducto = @IdProducto AND stockActual = 0 AND revision = @Revision;
        IF @@ROWCOUNT = 0 THROW 50528, ''El inventario cambio o aun tiene existencias. Recargue.'', 1;
        EXEC dbo.sp_Inventario_RegistrarMovimiento @Actor, @IdProducto, 0, 0, ''Salida'', ''Eliminacion de inventario sin existencias'';
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;';

    EXEC sys.sp_executesql N'-- Registra una entrada o salida con motivo; no permite stock negativo.
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
    IF @Cantidad <= 0 OR @Tipo NOT IN (''Entrada'', ''Salida'') OR NULLIF(LTRIM(RTRIM(@Motivo)), '''') IS NULL
        THROW 50527, ''Indique tipo, cantidad positiva y motivo.'', 1;
    BEGIN TRY
        BEGIN TRANSACTION;
        DECLARE @Anterior int;
        DECLARE @Nuevo bigint;
        SELECT @Anterior = stockActual FROM dbo.inventarioProducto
        WHERE idProducto = @IdProducto AND activo = 1 AND revision = @Revision;
        IF @Anterior IS NULL THROW 50528, ''El inventario cambio o esta inactivo. Recargue.'', 1;
        IF @Tipo = ''Entrada'' SET @Nuevo = CONVERT(bigint, @Anterior) + @Cantidad;
        ELSE SET @Nuevo = CONVERT(bigint, @Anterior) - @Cantidad;
        IF @Nuevo < 0 OR @Nuevo > 2147483647 THROW 50529, ''Las existencias quedan fuera del rango permitido.'', 1;
        UPDATE dbo.inventarioProducto SET stockActual = @Nuevo, revision = revision + 1
        WHERE idProducto = @IdProducto AND revision = @Revision;
        IF @@ROWCOUNT = 0 THROW 50528, ''El inventario cambio. Recargue.'', 1;
        EXEC dbo.sp_Inventario_RegistrarMovimiento @Actor, @IdProducto, @Anterior, @Cantidad, @Tipo, @Motivo;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;';

    EXEC sys.sp_executesql N'-- Procedimiento sp_Gestion_Clientes: consulta o modifica los datos de su gestion con parametros definidos.
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
END';

    EXEC sys.sp_executesql N'CREATE   PROCEDURE dbo.sp_Operacion_ExigirPedidoAbierto
    @Actor int, @IdPedido int
AS
BEGIN
    SET NOCOUNT ON;
    EXEC dbo.sp_Operacion_ExigirPersonal @Actor;
    IF EXISTS (SELECT 1 FROM dbo.usuario WHERE idUsuario = @Actor AND idRol = 2)
        AND NOT EXISTS (
            SELECT 1 FROM dbo.pedido WHERE idPedido = @IdPedido AND idOrigenPedido = 2
                AND idTipoEntrega IN (1, 2) AND estado NOT IN (''Pagado'', ''Cancelado'')
        )
        THROW 50522, ''El despachador solo puede operar ventas locales abiertas.'', 1;
END;';

    EXEC sys.sp_executesql N'CREATE   PROCEDURE dbo.sp_Operacion_EstadoReserva
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
END';

    EXEC sys.sp_executesql N'CREATE   PROCEDURE dbo.sp_Cliente_Guardar
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
    IF NULLIF (LTRIM(RTRIM(@PrimerNombre)), '''') IS NULL
       OR NULLIF (LTRIM(RTRIM(@PrimerApellido)), '''') IS NULL
       OR @Genero NOT IN (''M'', ''F'', ''O'')
        THROW 50201, ''Revise nombre, apellido y género.'', 1;
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
                              AND estado NOT IN (''Cancelado'', ''Pagado''))
                OR EXISTS (SELECT 1
                           FROM dbo.reservacion
                           WHERE idCliente = @Id
                                 AND estado IN (''Pendiente'', ''Confirmada'')
                                 AND fechaHoraReserva >= GETDATE()))
            THROW 50202, ''El cliente tiene pedidos o reservas pendientes.'', 1;
        IF @Id = 0
            BEGIN
                EXEC dbo.sp_Persona_Crear @PrimerNombre,@SegundoNombre,@PrimerApellido,@SegundoApellido,@Genero,@Cedula,@Telefono,@Email,@IdPersona OUTPUT;
                INSERT dbo.cliente(idPersona,activo) VALUES(@IdPersona,@Activo);
                SET @Id = CONVERT (INT, SCOPE_IDENTITY());
            END
        ELSE
            BEGIN
                IF EXISTS(SELECT 1 FROM dbo.vw_Empleado WHERE idPersona=@IdPersona)
                    AND (NULLIF(@Cedula,'''') IS NULL OR NULLIF(@Telefono,'''') IS NULL)
                    THROW 50907,''Esta persona tambien es personal: conserve su cedula y telefono.'',1;
                UPDATE dbo.persona SET primerNombre=@PrimerNombre,segundoNombre=NULLIF(@SegundoNombre,''''),
                    primerApellido=@PrimerApellido,segundoApellido=NULLIF(@SegundoApellido,''''),genero=@Genero,
                    cedula=NULLIF(@Cedula,''''),telefono=NULLIF(@Telefono,''''),email=NULLIF(@Email,'''') WHERE idPersona=@IdPersona;
                UPDATE dbo.cliente SET activo=@Activo WHERE idCliente=@Id;
                IF @@ROWCOUNT = 0
                    THROW 50203, ''El cliente ya no existe.'', 1;
            END
        COMMIT TRANSACTION;
        SELECT @Id;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK;
        THROW;
    END CATCH
END';

    EXEC sys.sp_executesql N'CREATE   PROCEDURE dbo.sp_Mesa_Guardar
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
END';

    EXEC sys.sp_executesql N'CREATE   PROCEDURE dbo.sp_Mesa_Eliminar
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
END';

    EXEC sys.sp_executesql N'CREATE   PROCEDURE dbo.sp_Empleado_Listar
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
END';

    EXEC sys.sp_executesql N'-- Vincular otra cuenta a una persona existente es una operacion exclusiva del administrador.
CREATE   PROCEDURE dbo.sp_Persona_CompletarVinculo
 @Actor int,@IdPersona int,@SegundoNombre varchar(80),@SegundoApellido varchar(80),@Genero char(1),
 @Cedula varchar(30),@Telefono varchar(20),@Email varchar(120)
AS
BEGIN
 SET NOCOUNT ON;
 EXEC dbo.sp_Catalogo_ExigirAdministrador @Actor;
 IF NOT EXISTS(SELECT 1 FROM dbo.persona WHERE idPersona=@IdPersona)
  THROW 50903,''La persona seleccionada ya no existe. Recargue el formulario.'',1;
 -- Solo se completan campos vacios; los datos existentes se editan desde su gestion.
 UPDATE dbo.persona SET segundoNombre=COALESCE(NULLIF(segundoNombre,''''),NULLIF(@SegundoNombre,'''')),
 segundoApellido=COALESCE(NULLIF(segundoApellido,''''),NULLIF(@SegundoApellido,'''')),
 genero=COALESCE(genero,@Genero),cedula=COALESCE(NULLIF(cedula,''''),NULLIF(@Cedula,'''')),
 telefono=COALESCE(NULLIF(telefono,''''),NULLIF(@Telefono,'''')),email=COALESCE(NULLIF(email,''''),NULLIF(@Email,''''))
 WHERE idPersona=@IdPersona;
END;';

    EXEC sys.sp_executesql N'CREATE   PROCEDURE dbo.sp_Pedido_Iniciar
    @Actor int, @IdCliente int = NULL, @Solicitud uniqueidentifier, @NumeroLineas int
AS
BEGIN
    SET NOCOUNT ON;
    IF @@TRANCOUNT = 0 THROW 50700, ''Inicie una transacción antes de guardar la venta.'', 1;
    EXEC dbo.sp_Operacion_ExigirPersonal @Actor;
    IF @Solicitud IS NULL OR @NumeroLineas NOT BETWEEN 1 AND 200
        THROW 50701, ''La venta necesita entre 1 y 200 productos.'', 1;

    DECLARE @IdPedido int;
    SELECT @IdPedido = idPedido FROM dbo.pedido WHERE solicitud = @Solicitud;
    IF @IdPedido IS NOT NULL
    BEGIN
        IF NOT EXISTS (
            SELECT 1 FROM dbo.pedido WHERE idPedido = @IdPedido AND idUsuarioRegistro = @Actor
                AND idOrigenPedido = 2 AND ISNULL(idCliente, 0) = ISNULL(@IdCliente, 0)
        ) OR (SELECT COUNT(*) FROM dbo.detallePedido WHERE idPedido = @IdPedido) <> @NumeroLineas
            THROW 50703, ''La solicitud ya se utilizó con otros datos.'', 1;
        SELECT @IdPedido AS IdPedido, CAST(1 AS bit) AS Reintento;
        RETURN;
    END;

    IF @IdCliente IS NOT NULL AND NOT EXISTS (
        SELECT 1 FROM dbo.cliente WHERE idCliente = @IdCliente AND activo = 1
    )
        THROW 50702, ''Seleccione un cliente activo.'', 1;

    INSERT dbo.pedido (idCliente, fechaHora, estado, subtotal, total, esDelivery, costoEnvio,
                       solicitud, idUsuarioRegistro, idOrigenPedido, idTipoEntrega)
    VALUES (@IdCliente, GETDATE(), ''Pendiente'', 0, 0, 0, 0, @Solicitud, @Actor, 2, 2);
    SET @IdPedido = CONVERT(int, SCOPE_IDENTITY());
    INSERT dbo.historialPedido(idPedido, estadoAnterior, estadoNuevo, idUsuario)
    VALUES (@IdPedido, NULL, ''Pendiente'', @Actor);
    SELECT @IdPedido AS IdPedido, CAST(0 AS bit) AS Reintento;
END;';

    EXEC sys.sp_executesql N'CREATE   PROCEDURE dbo.sp_Pedido_Finalizar
    @Actor int,
    @IdPedido int,
    @NumeroLineas int,
    @Reintento bit
AS
BEGIN
    SET NOCOUNT ON;
    IF @@TRANCOUNT = 0 THROW 50700, ''Inicie una transaccion antes de guardar el pedido.'', 1;
    EXEC dbo.sp_Operacion_ExigirPersonal @Actor;
    IF NOT EXISTS (
        SELECT 1 FROM dbo.pedido WHERE idPedido = @IdPedido
            AND idOrigenPedido = 2 AND idUsuarioRegistro = @Actor
    )
        THROW 50706, ''La venta no corresponde a esta solicitud del personal.'', 1;

    IF @Reintento = 1 RETURN;
    IF @NumeroLineas NOT BETWEEN 1 AND 200 OR
        (SELECT COUNT(*) FROM dbo.detallePedido WHERE idPedido = @IdPedido) <> @NumeroLineas
        THROW 50709, ''El detalle del pedido esta incompleto.'', 1;

    DECLARE @Subtotal decimal(18,2);
    SELECT @Subtotal = SUM(total) FROM dbo.detallePedido WHERE idPedido = @IdPedido;
    IF @Subtotal IS NULL OR @Subtotal <= 0 OR @Subtotal > 99999999.99
        THROW 50710, ''El total supera el importe permitido.'', 1;
    UPDATE dbo.pedido SET subtotal = @Subtotal, total = @Subtotal + costoEnvio
    WHERE idPedido = @IdPedido AND estado = ''Pendiente'';
    IF @@ROWCOUNT = 0 THROW 50704, ''El pedido ya no esta pendiente.'', 1;
END;';

    EXEC sys.sp_executesql N'CREATE   PROCEDURE dbo.sp_Usuario_Listar
    @Actor int
AS
BEGIN
    SET NOCOUNT ON;
    EXEC dbo.sp_Catalogo_ExigirAdministrador @Actor;
    SELECT idUsuario, idPersona, nombreUsuario, idRol, NombreRol, AccesoPersonal
    FROM dbo.vw_UsuarioAccesos
    WHERE idRol IN (1, 2)
    ORDER BY nombreUsuario;
END;';

    EXEC sys.sp_executesql N'CREATE   PROCEDURE dbo.sp_Usuario_CuentaParaPerfil @Actor int,@IdPersona int,@RolNuevo int AS
BEGIN
 SET NOCOUNT ON;
 EXEC dbo.sp_Catalogo_ExigirAdministrador @Actor;
 SELECT TOP(1) idUsuario,nombreUsuario,idRol FROM dbo.usuario
 WHERE idPersona=@IdPersona AND ((@RolNuevo=3 AND idRol IN(1,2)) OR (@RolNuevo IN(1,2) AND idRol=3))
 ORDER BY idUsuario;
END;';

    EXEC sys.sp_executesql N'CREATE   PROCEDURE dbo.sp_Operacion_Detalle
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
                 AND venta.estado NOT IN (''Pagado'', ''Cancelado'')))
    ORDER BY d.idDetalle;
END';

    EXEC sys.sp_executesql N'CREATE   PROCEDURE dbo.sp_Operacion_Clientes
    @Actor INT
AS
BEGIN
    SET NOCOUNT ON;
    EXECUTE dbo.sp_Operacion_ExigirPersonal @Actor;
    SELECT idCliente AS IdCliente,
           CONCAT(pNombreCl, '' '', pApellidoCl) AS Nombre,
           cedula AS Cedula,
           telefono AS Telefono,
           email AS Email
    FROM dbo.vw_Cliente
    WHERE activo = 1
    ORDER BY pNombreCl, pApellidoCl;
END';

    EXEC sys.sp_executesql N'CREATE   PROCEDURE dbo.sp_Pedido_AgregarLinea
    @Actor int,
    @IdPedido int,
    @IdProducto int,
    @Cantidad int,
    @Precio decimal(10,2),
    @Reintento bit
AS
BEGIN
    SET NOCOUNT ON;
    IF @@TRANCOUNT = 0 THROW 50700, ''Inicie una transaccion antes de guardar el pedido.'', 1;
    IF @Cantidad NOT BETWEEN 1 AND 999 OR @Precio IS NULL OR @Precio <= 0
        THROW 50705, ''Revise la cantidad y el precio del producto.'', 1;

    EXEC dbo.sp_Operacion_ExigirPersonal @Actor;
    IF NOT EXISTS (
        SELECT 1 FROM dbo.pedido WHERE idPedido = @IdPedido
            AND idOrigenPedido = 2 AND idUsuarioRegistro = @Actor
    )
        THROW 50706, ''La venta no corresponde a esta solicitud del personal.'', 1;

    IF @Reintento = 1
    BEGIN
        IF NOT EXISTS (SELECT 1 FROM dbo.detallePedido WHERE idPedido = @IdPedido
            AND idProducto = @IdProducto AND cantidad = @Cantidad AND precioUnitario = @Precio)
            THROW 50703, ''La solicitud ya fue guardada con otro detalle.'', 1;
        RETURN;
    END;
    IF NOT EXISTS (SELECT 1 FROM dbo.pedido WHERE idPedido = @IdPedido AND estado = ''Pendiente'')
        THROW 50704, ''El pedido ya no esta pendiente.'', 1;
    IF EXISTS (SELECT 1 FROM dbo.detallePedido WHERE idPedido = @IdPedido AND idProducto = @IdProducto)
        THROW 50705, ''El producto ya esta incluido en el pedido.'', 1;
    IF NOT EXISTS (SELECT 1 FROM dbo.producto p INNER JOIN dbo.categoria c ON c.idCategoria = p.idCategoria
        WHERE p.idProducto = @IdProducto AND p.disponible = 1 AND c.activa = 1 AND p.precio = @Precio)
        THROW 50707, ''Cambio el precio o la disponibilidad. Recargue el catalogo.'', 1;

    DECLARE @Anterior int;
    SELECT @Anterior = stockActual FROM dbo.inventarioProducto WHERE idProducto = @IdProducto AND activo = 1;
    UPDATE dbo.inventarioProducto
    SET stockActual = stockActual - @Cantidad, revision = revision + 1
    WHERE idProducto = @IdProducto AND activo = 1 AND stockActual >= @Cantidad;
    IF @@ROWCOUNT = 0 THROW 50708, ''El producto necesita inventario activo y existencias suficientes.'', 1;

    EXEC dbo.sp_Inventario_RegistrarMovimiento @Actor, @IdProducto, @Anterior,
        @Cantidad, ''Salida'', ''Registro de pedido'';
    INSERT INTO dbo.detallePedido
        (idPedido, idProducto, cantidad, precioUnitario, subtotal, total, controlInventario)
    VALUES (@IdPedido, @IdProducto, @Cantidad, @Precio, @Cantidad * @Precio, @Cantidad * @Precio, 1);
END;';

    EXEC sys.sp_executesql N'CREATE   PROCEDURE dbo.sp_Operacion_Reservas
    @Actor INT
AS
BEGIN
    SET NOCOUNT ON;
    EXEC dbo.sp_Catalogo_ExigirAdministrador @Actor;
    SELECT r.idReservacion AS IdReserva,
           r.idMesa AS IdMesa,
           m.numero AS Mesa,
           CONCAT(c.pNombreCl, '' '', c.pApellidoCl) AS Cliente,
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
END';

    EXEC sys.sp_executesql N'-- Procedimiento sp_Producto_Desactivar: consulta o modifica los datos de su gestion con parametros definidos.
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
        THROW 50027, ''El producto seleccionado ya no existe.'', 1;
END';

    EXEC sys.sp_executesql N'CREATE   PROCEDURE dbo.sp_Persona_Listar @Actor int AS
BEGIN
 SET NOCOUNT ON;
 EXEC dbo.sp_Catalogo_ExigirAdministrador @Actor;
 SELECT idPersona,primerNombre,segundoNombre,primerApellido,segundoApellido,genero,cedula,telefono,email
 FROM dbo.persona ORDER BY primerNombre,primerApellido,idPersona;
END;';

    EXEC sys.sp_executesql N'CREATE PROCEDURE dbo.sp_Producto_Guardar
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
    IF NULLIF (LTRIM(RTRIM(@Nombre)), '''') IS NULL OR @Precio IS NULL OR @Precio <= 0
        THROW 50024, ''El nombre y un precio positivo son obligatorios.'', 1;
    IF @IdProducto <> 0 AND NULLIF (LTRIM(RTRIM(@Codigo)), '''') IS NULL
        THROW 50024, ''El código del producto es obligatorio al editar.'', 1;
    BEGIN TRY
        BEGIN TRANSACTION;
        IF NOT EXISTS (SELECT 1
                       FROM dbo.categoria
                       WHERE idCategoria = @IdCategoria)
            THROW 50022, ''La categoría seleccionada ya no existe.'', 1;
        IF @Disponible = 1
           AND EXISTS (SELECT 1
                       FROM dbo.categoria
                       WHERE idCategoria = @IdCategoria
                             AND activa = 0)
            THROW 50025, ''Active la categoría antes de ofrecer este producto.'', 1;
        IF @IdSubcategoria IS NOT NULL
           AND NOT EXISTS (SELECT 1
                           FROM dbo.subcategoria
                           WHERE idSubcategoria = @IdSubcategoria
                                 AND idCategoria = @IdCategoria)
            THROW 50026, ''La subcategoría no pertenece a la categoría seleccionada.'', 1;
        IF @IdProducto = 0
        BEGIN
            IF NULLIF(LTRIM(RTRIM(@Codigo)), '''') IS NULL
            BEGIN
                DECLARE @NumeroCodigo bigint;
                SELECT @NumeroCodigo = ISNULL(CONVERT(bigint, MAX(idProducto)), 0) + 1
                FROM dbo.producto WITH (UPDLOCK);

                SET @Codigo = CONCAT(''PRD-'', @NumeroCodigo);
                WHILE EXISTS (SELECT 1 FROM dbo.producto WHERE codigo = @Codigo)
                BEGIN
                    SET @NumeroCodigo = @NumeroCodigo + 1;
                    SET @Codigo = CONCAT(''PRD-'', @NumeroCodigo);
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
                    THROW 50027, ''El producto seleccionado ya no existe.'', 1;
            END
        COMMIT TRANSACTION;
        IF @IdProducto = 0 SELECT @Codigo;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK;
        THROW;
    END CATCH
END';

    EXEC sys.sp_executesql N'-- Procedimiento sp_Producto_Eliminar: consulta o modifica los datos de su gestion con parametros definidos.
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
            THROW 50402, ''El producto ya no existe. Recargue el listado.'', 1;
        IF EXISTS (SELECT 1
                   FROM dbo.detallePedido
                   WHERE idProducto = @IdProducto)
            THROW 50403, ''El producto forma parte del historial de pedidos. Márquelo como no disponible.'', 1;
        IF EXISTS (SELECT 1
                   FROM dbo.inventarioProducto
                   WHERE idProducto = @IdProducto)
            THROW 50404, ''El producto tiene un registro de inventario. Elimine ese registro primero si las reglas lo permiten.'', 1;
        IF EXISTS(SELECT 1 FROM dbo.movimientoInventario WHERE idProducto=@IdProducto)
           OR EXISTS(SELECT 1 FROM dbo.facturaDetalle WHERE idProducto=@IdProducto)
            THROW 51101,''El producto tiene movimientos o ventas historicas. Marquelo como no disponible.'',1;
        DELETE dbo.producto
        WHERE idProducto = @IdProducto;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK;
        THROW;
    END CATCH
END';

    EXEC sys.sp_executesql N'CREATE   PROCEDURE dbo.sp_Operacion_Pedidos
    @Actor INT
AS
BEGIN
    SET NOCOUNT ON;
    EXECUTE dbo.sp_Operacion_ExigirPersonal @Actor;
    SELECT p.idPedido AS IdPedido,
           p.fechaHora AS Fecha,
           CASE WHEN p.idCliente IS NULL THEN ''Consumidor final'' ELSE CONCAT(c.pNombreCl, '' '', c.pApellidoCl) END AS Cliente,
           p.estado AS Estado,
           p.total AS Total,
           o.nombre AS Origen, CASE WHEN p.idOrigenPedido = 2 AND p.idTipoEntrega = 2 THEN ''Mostrador'' ELSE t.nombre END AS Entrega, p.idTipoEntrega AS IdTipoEntrega,
           COALESCE(ur.nombreUsuario,''Sin registrar'') AS RegistradoPor,
           COALESCE(ua.nombreUsuario,''Sin registrar'') AS AtendidoPor,
           p.costoEnvio AS Envio, p.direccion AS Direccion, p.telefonoDeContacto AS Contacto,
           p.idMesa AS IdMesa,
           m.numero AS Mesa,
           CASE WHEN EXISTS (SELECT 1 FROM dbo.factura f WHERE f.idPedido = p.idPedido AND f.estado = ''Anulada'')
                THEN ''Anulado'' WHEN EXISTS (SELECT 1
                             FROM dbo.pago AS pa
                                  INNER JOIN
                                  dbo.factura AS f
                                  ON f.idFactura = pa.idFactura
                             WHERE f.idPedido = p.idPedido) THEN ''Pagado'' ELSE ''Sin pagar'' END AS Pago
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
        OR (p.idOrigenPedido = 2 AND p.idTipoEntrega IN (1, 2) AND p.estado NOT IN (''Pagado'', ''Cancelado''))
    ORDER BY p.idPedido DESC;
END';

    EXEC sys.sp_executesql N'CREATE   PROCEDURE dbo.sp_Operacion_AsignarMesa
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
END';

    EXEC sys.sp_executesql N'CREATE   PROCEDURE dbo.sp_Pedido_CancelarComun @Actor int,
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
END;';

    EXEC sys.sp_executesql N'CREATE   PROCEDURE dbo.sp_Empleado_Insertar
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
        THROW 50010, ''Solo un administrador activo puede gestionar empleados.'', 1;
    IF @IdRol IS NULL
       OR @IdRol NOT IN (1, 2)
        THROW 50011, ''El rol debe ser Administrador o Empleado.'', 1;
    IF @IdUsuarioExistente=0 AND NULLIF (LTRIM(RTRIM(@Contrasena)), '''') IS NULL
        THROW 50012, ''La contraseña es obligatoria para crear la cuenta.'', 1;
    IF @IdRol = 2 AND (@Cargo IS NULL OR UPPER(LTRIM(RTRIM(@Cargo))) <> ''DESPACHADOR'')
        THROW 50802, ''Seleccione Despachador como cargo del empleado.'', 1;
    IF @IdRol = 2
        SET @Cargo = ''Despachador'';
    BEGIN TRY
        BEGIN TRANSACTION;
        IF @IdPersona=0
            EXEC dbo.sp_Persona_Crear @PrimerNombre,@SegundoNombre,@PrimerApellido,@SegundoApellido,@Genero,@Cedula,@Telefono,NULL,@IdPersona OUTPUT;
        ELSE
            EXEC dbo.sp_Persona_CompletarVinculo @IdAdministrador,@IdPersona,@SegundoNombre,@SegundoApellido,@Genero,@Cedula,@Telefono,NULL;
        IF EXISTS(SELECT 1 FROM dbo.vw_Empleado WHERE idPersona=@IdPersona AND (idUsuario IS NOT NULL OR activo=0))
            THROW 50905,''La persona ya tiene un perfil de personal con cuenta o inactivo. Revise Empleados.'',1;
        DECLARE @IdUsuario int;
        IF @IdUsuarioExistente=0
        BEGIN
            IF EXISTS(SELECT 1 FROM dbo.usuario WHERE idPersona=@IdPersona)
                THROW 51006,''La persona ya tiene cuenta. Seleccionela para agregar el perfil sin duplicarla.'',1;
            INSERT dbo.usuario(nombreUsuario,contrasena,idRol,idPersona) VALUES(@Usuario,@Contrasena,@IdRol,@IdPersona);
            SET @IdUsuario=CONVERT(int,SCOPE_IDENTITY());
        END
        ELSE
        BEGIN
            UPDATE dbo.usuario SET idRol=@IdRol
             WHERE idUsuario=@IdUsuarioExistente AND idPersona=@IdPersona AND idRol=3;
            IF @@ROWCOUNT<>1 THROW 51006,''Solo puede agregar personal a una cuenta de cliente de esta persona.'',1;
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
END';

    EXEC sys.sp_executesql N'-- Procedimiento sp_Empleado_Eliminar: consulta o modifica los datos de su gestion con parametros definidos.
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
            THROW 50409, ''El empleado ya no existe. Recargue el listado.'', 1;
        DECLARE @ConservarCuenta bit=0;
        IF EXISTS(SELECT 1 FROM dbo.cliente WHERE idPersona=@Persona) SET @ConservarCuenta=1;
        IF EXISTS (SELECT 1
                   FROM dbo.pedido
                   WHERE idEmpleado = @IdEmpleado
                         OR idEmpleadoRepartidor = @IdEmpleado OR (idUsuarioRegistro=@Usuario AND @ConservarCuenta=0))
           OR EXISTS(SELECT 1 FROM dbo.pago WHERE idUsuario=@Usuario AND @ConservarCuenta=0)
           OR EXISTS(SELECT 1 FROM dbo.historialPedido WHERE idUsuario=@Usuario AND @ConservarCuenta=0)
            THROW 50410, ''El empleado tiene historial de pedidos o repartos. Utilice Desactivar.'', 1;
        IF @Usuario IS NOT NULL
            BEGIN
                IF @Usuario = @IdAdministrador
                   OR NOT EXISTS (SELECT 1
                                  FROM dbo.usuario
                                  WHERE idUsuario = @Usuario
                                        AND idRol = 2)
                    THROW 50411, ''No se pueden eliminar cuentas de administrador desde Empleados.'', 1;
            END
        IF @ConservarCuenta=0 AND @Usuario IS NOT NULL AND
           EXISTS(SELECT 1 FROM dbo.movimientoInventario WHERE idUsuario=@Usuario)
            THROW 51102,''El empleado tiene movimientos de inventario. Use Desactivar para conservar la trazabilidad.'',1;
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
END';

    EXEC sys.sp_executesql N'CREATE   PROCEDURE dbo.sp_Documento_Obtener
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
                    AND f.estado = ''Pagada'' AND p.idOrigenPedido = 2 AND p.idTipoEntrega IN (1, 2)
            ))
            THROW 50520, ''El historial está reservado al administrador.'', 1;
        IF @Factura = 1
            BEGIN
                IF NOT EXISTS (SELECT 1
                               FROM dbo.factura
                               WHERE idPedido = @IdPedido
                                     AND estado IN (''Pagada'', ''Anulada''))
                    THROW 50521, ''El pedido no tiene una factura para consultar.'', 1;
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
                                  ORDER BY idPago), '''') AS Metodo,
                       (SELECT TOP (1) COALESCE (recibido, monto)
                        FROM dbo.pago
                        WHERE idFactura = f.idFactura
                        ORDER BY idPago) AS Recibido,
                       COALESCE ((SELECT TOP (1) cambio
                                  FROM dbo.pago
                                  WHERE idFactura = f.idFactura
                                  ORDER BY idPago), 0) AS Cambio,
                       f.mesaNumero AS Mesa,
                       COALESCE(f.tipoEntrega,f.tipoPedido,''Sin registrar'') AS TipoPedido,
                       COALESCE(f.origenPedido,''Sin registrar'') AS OrigenPedido,
                       COALESCE(f.registradoPor,''Sin registrar'') AS RegistradoPor,
                       COALESCE(f.atendidoPor,''Sin registrar'') AS AtendidoPor,
                       COALESCE(f.empleadoNombre,''Sin registrar'') AS CobradoPor,
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
                    THROW 50522, ''No existe el pedido.'', 1;
                SELECT ''P-'' + CONVERT (VARCHAR (20), p.idPedido) AS Numero,
                       p.fechaHora AS Fecha,
                       CASE WHEN p.idCliente IS NULL THEN ''Consumidor final'' ELSE CONCAT(c.pNombreCl, '' '', c.pApellidoCl) END AS Cliente,
                       c.cedula AS Cedula,
                       '''' AS Empleado,
                       p.estado AS Estado,
                       p.subtotal AS Subtotal,
                       p.costoEnvio AS CostoEnvio,
                       CONVERT (DECIMAL (10, 2), 0) AS Impuestos,
                       CONVERT (DECIMAL (10, 2), 0) AS Descuento,
                       p.total AS Total,
                       cfg.nombre AS Restaurante,
                       cfg.moneda AS Moneda,
                       cfg.tasaImpuesto AS TasaImpuesto,
                       '''' AS Metodo,
                       CONVERT (DECIMAL (10, 2), 0) AS Recibido,
                       CONVERT (DECIMAL (10, 2), 0) AS Cambio,
                       (SELECT numero
                        FROM dbo.mesa
                        WHERE idMesa = p.idMesa) AS Mesa,
                       CASE WHEN p.idOrigenPedido = 2 AND p.idTipoEntrega = 2 THEN ''Mostrador'' ELSE (SELECT nombre FROM dbo.tipoEntrega WHERE idTipoEntrega=p.idTipoEntrega) END AS TipoPedido,
                       (SELECT nombre FROM dbo.origenPedido WHERE idOrigenPedido=p.idOrigenPedido) AS OrigenPedido,
                       COALESCE((SELECT nombreUsuario FROM dbo.usuario WHERE idUsuario=p.idUsuarioRegistro),''Sin registrar'') AS RegistradoPor,
                       COALESCE((SELECT u.nombreUsuario FROM dbo.empleado e JOIN dbo.usuario u ON u.idUsuario=e.idUsuario WHERE e.idEmpleado=p.idEmpleado),''Sin registrar'') AS AtendidoPor,
                       ''Sin cobrar'' AS CobradoPor, p.direccion AS DireccionEntrega, p.referencia AS ReferenciaEntrega, p.telefonoDeContacto AS TelefonoContacto
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
END';

    EXEC sys.sp_executesql N'-- Procedimiento sp_Cliente_Eliminar: consulta o modifica los datos de su gestion con parametros definidos.
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
            THROW 50405, ''El cliente ya no existe. Recargue el listado.'', 1;
        IF EXISTS (SELECT 1
                   FROM dbo.pedido
                   WHERE idCliente = @Id)
           OR EXISTS (SELECT 1
                      FROM dbo.reservacion
                      WHERE idCliente = @Id)
            THROW 50406, ''El cliente tiene historial de pedidos o reservas. Utilice Desactivar.'', 1;
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
                    THROW 50407, ''Solo puede eliminar junto al perfil una cuenta de cliente sin historial.'', 1;
                IF EXISTS (SELECT 1
                           FROM dbo.vw_Empleado
                           WHERE idUsuario = @Usuario)
                   OR EXISTS (SELECT 1
                              FROM dbo.vw_Cliente
                              WHERE idUsuario = @Usuario
                                    AND idCliente <> @Id)
                    THROW 50408, ''La cuenta tiene otros perfiles asociados. No se eliminaron datos.'', 1;
            END
        IF @ConservarCuenta=0 AND @Usuario IS NOT NULL AND
           (EXISTS(SELECT 1 FROM dbo.pago WHERE idUsuario=@Usuario)
            OR EXISTS(SELECT 1 FROM dbo.movimientoInventario WHERE idUsuario=@Usuario)
            OR EXISTS(SELECT 1 FROM dbo.historialPedido WHERE idUsuario=@Usuario)
            OR EXISTS(SELECT 1 FROM dbo.pedido WHERE idUsuarioRegistro=@Usuario))
            THROW 51102,''La cuenta conserva historial de operaciones. Use Desactivar para conservar la trazabilidad.'',1;
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
END';

    EXEC sys.sp_executesql N'CREATE   PROCEDURE dbo.sp_Operacion_EstadoPedido
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
    IF @Anterior = ''Entregado'' AND @Nuevo = ''Pendiente''
    BEGIN
        EXEC dbo.sp_Catalogo_ExigirAdministrador @Actor;
        SET @Reabrir = 1;
    END
    ELSE IF @Nuevo IS NULL OR @Anterior IS NULL OR @Nuevo <> ''Entregado''
        OR @Anterior NOT IN (''Pendiente'', ''En preparación'', ''Listo'')
        THROW 50512, ''Solo se pueden despachar ventas pendientes.'', 1;
    BEGIN TRY
        BEGIN TRANSACTION;
        EXEC dbo.sp_Operacion_ExigirPedidoAbierto @Actor, @IdPedido;
        IF @Reabrir = 1 AND (NOT EXISTS (
            SELECT 1 FROM dbo.pedido WHERE idPedido = @IdPedido AND estado = ''Entregado''
                AND idOrigenPedido = 2 AND idTipoEntrega IN (1, 2)
        ) OR EXISTS (SELECT 1 FROM dbo.factura WHERE idPedido = @IdPedido))
            THROW 50512, ''Solo puede volver a pendiente una venta local entregada y sin factura.'', 1;
        DECLARE @Empleado AS INT = (SELECT idEmpleado
                                    FROM dbo.vw_Empleado
                                    WHERE idUsuario = @Actor
                                          AND activo = 1);
        IF @Nuevo=''Entregado'' AND EXISTS(SELECT 1 FROM dbo.pedido WHERE idPedido=@IdPedido AND idTipoEntrega=1 AND idMesa IS NULL)
            THROW 50809,''Asigne una mesa antes de entregar el pedido en mesa.'',1;
        UPDATE dbo.pedido
        SET estado = @Nuevo,
            idEmpleado = CASE WHEN @Reabrir = 1 THEN idEmpleado ELSE COALESCE(idEmpleado, @Empleado) END
        WHERE idPedido = @IdPedido
              AND estado = @Anterior;
        IF @@ROWCOUNT <> 1
            THROW 50513, ''El pedido cambio. Recargue antes de continuar.'', 1;
        INSERT dbo.historialPedido (idPedido, estadoAnterior, estadoNuevo, idUsuario)
        VALUES                    (@IdPedido, @Anterior, @Nuevo, @Actor);
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK;
        THROW;
    END CATCH
END';

    EXEC sys.sp_executesql N'CREATE   PROCEDURE dbo.sp_Facturas_Listar
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
END';

    EXEC sys.sp_executesql N'CREATE   PROCEDURE dbo.sp_Factura_Anular
    @Actor int, @IdPedido int, @Motivo nvarchar(200), @ReponerInventario bit
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;
    EXEC dbo.sp_Catalogo_ExigirAdministrador @Actor;
    SET @Motivo = LTRIM(RTRIM(@Motivo));
    IF NULLIF(@Motivo, '''') IS NULL OR @ReponerInventario IS NULL
        THROW 50540, ''Indique el motivo de la anulación.'', 1;

    BEGIN TRY
        BEGIN TRANSACTION;
        IF NOT EXISTS (SELECT 1 FROM dbo.pedido WITH (UPDLOCK) WHERE idPedido = @IdPedido
                       AND estado = ''Pagado'' AND idOrigenPedido = 2 AND idTipoEntrega IN (1, 2))
            THROW 50541, ''Solo se puede anular una venta local pagada.'', 1;

        DECLARE @IdFactura int;
        DECLARE @Numero varchar(30);
        SELECT @IdFactura = idFactura, @Numero = numeroFactura
        FROM dbo.factura WHERE idPedido = @IdPedido AND estado = ''Pagada'';
        IF @IdFactura IS NULL OR NOT EXISTS (SELECT 1 FROM dbo.pago WHERE idFactura = @IdFactura)
            THROW 50542, ''La venta debe tener factura y pago registrados.'', 1;

        IF @ReponerInventario = 1
        BEGIN
            IF EXISTS (SELECT 1 FROM dbo.detallePedido d
                       LEFT JOIN dbo.inventarioProducto i ON i.idProducto = d.idProducto
                       WHERE d.idPedido = @IdPedido AND d.controlInventario = 1 AND i.idProducto IS NULL)
                THROW 50543, ''Uno de los productos ya no tiene registro de inventario.'', 1;

            INSERT dbo.movimientoInventario
                (idProducto, producto, anterior, nuevo, diferencia, idUsuario, usuario, motivo)
            SELECT i.idProducto, p.nombre, i.stockActual, i.stockActual + d.cantidad, d.cantidad,
                @Actor, u.nombreUsuario, N''Anulación de factura '' + @Numero
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
        UPDATE dbo.factura SET estado = ''Anulada'' WHERE idFactura = @IdFactura;
        UPDATE dbo.pedido SET estado = ''Cancelado'' WHERE idPedido = @IdPedido;
        INSERT dbo.historialPedido(idPedido, estadoAnterior, estadoNuevo, idUsuario)
        VALUES(@IdPedido, ''Pagado'', ''Cancelado'', @Actor);
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;';

    EXEC sys.sp_executesql N'CREATE   PROCEDURE dbo.sp_Cobro_Registrar
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
END;';

    EXEC sys.sp_executesql N'CREATE   PROCEDURE dbo.sp_Cobro_Cotizar
    @Actor INT,
    @IdPedido INT
AS
BEGIN
    SET NOCOUNT ON;
    EXEC dbo.sp_Operacion_ExigirPedidoAbierto @Actor, @IdPedido;
    IF NOT EXISTS (SELECT 1
                   FROM dbo.pedido
                   WHERE idPedido = @IdPedido
                         AND estado = ''Entregado'')
        THROW 50514, ''El pedido debe estar entregado antes de cobrar.'', 1;
    SELECT p.idPedido AS IdPedido,
           CASE WHEN p.idCliente IS NULL THEN ''Consumidor final'' ELSE CONCAT(c.pNombreCl, '' '', c.pApellidoCl) END AS Cliente,
           p.subtotal AS Subtotal,
           p.costoEnvio AS CostoEnvio,
           p.total AS Total,
           cfg.tasaImpuesto AS TasaImpuesto,
           CONVERT (DECIMAL (10, 2), ROUND(p.subtotal - p.subtotal / (1 + cfg.tasaImpuesto / 100), 2)) AS Impuestos
    FROM dbo.pedido AS p
         LEFT JOIN dbo.vw_Cliente AS c
         ON c.idCliente = p.idCliente CROSS JOIN dbo.configuracionRestaurante AS cfg
    WHERE p.idPedido = @IdPedido AND p.estado = ''Entregado'';
END';

    -- Se cargan las filas con sus identificadores originales.
    ALTER TABLE [dbo].[anulacionVenta] NOCHECK CONSTRAINT ALL;
    ALTER TABLE [dbo].[categoria] NOCHECK CONSTRAINT ALL;
    ALTER TABLE [dbo].[cliente] NOCHECK CONSTRAINT ALL;
    ALTER TABLE [dbo].[configuracionRestaurante] NOCHECK CONSTRAINT ALL;
    ALTER TABLE [dbo].[detallePedido] NOCHECK CONSTRAINT ALL;
    ALTER TABLE [dbo].[empleado] NOCHECK CONSTRAINT ALL;
    ALTER TABLE [dbo].[factura] NOCHECK CONSTRAINT ALL;
    ALTER TABLE [dbo].[facturaDetalle] NOCHECK CONSTRAINT ALL;
    ALTER TABLE [dbo].[historialPedido] NOCHECK CONSTRAINT ALL;
    ALTER TABLE [dbo].[inventarioProducto] NOCHECK CONSTRAINT ALL;
    ALTER TABLE [dbo].[mesa] NOCHECK CONSTRAINT ALL;
    ALTER TABLE [dbo].[movimientoInventario] NOCHECK CONSTRAINT ALL;
    ALTER TABLE [dbo].[origenPedido] NOCHECK CONSTRAINT ALL;
    ALTER TABLE [dbo].[pago] NOCHECK CONSTRAINT ALL;
    ALTER TABLE [dbo].[pedido] NOCHECK CONSTRAINT ALL;
    ALTER TABLE [dbo].[persona] NOCHECK CONSTRAINT ALL;
    ALTER TABLE [dbo].[producto] NOCHECK CONSTRAINT ALL;
    ALTER TABLE [dbo].[reservacion] NOCHECK CONSTRAINT ALL;
    ALTER TABLE [dbo].[rol] NOCHECK CONSTRAINT ALL;
    ALTER TABLE [dbo].[subcategoria] NOCHECK CONSTRAINT ALL;
    ALTER TABLE [dbo].[tipoEntrega] NOCHECK CONSTRAINT ALL;
    ALTER TABLE [dbo].[usuario] NOCHECK CONSTRAINT ALL;

INSERT [dbo].[origenPedido] ([idOrigenPedido], [nombre]) VALUES (2, N'En el local')
INSERT [dbo].[origenPedido] ([idOrigenPedido], [nombre]) VALUES (1, N'En linea')
INSERT [dbo].[origenPedido] ([idOrigenPedido], [nombre]) VALUES (0, N'Sin registrar')
INSERT [dbo].[tipoEntrega] ([idTipoEntrega], [nombre]) VALUES (3, N'Domicilio')
INSERT [dbo].[tipoEntrega] ([idTipoEntrega], [nombre]) VALUES (1, N'En mesa')
INSERT [dbo].[tipoEntrega] ([idTipoEntrega], [nombre]) VALUES (2, N'Recoger')
INSERT [dbo].[tipoEntrega] ([idTipoEntrega], [nombre]) VALUES (0, N'Sin registrar')
SET IDENTITY_INSERT [dbo].[persona] ON 

INSERT [dbo].[persona] ([idPersona], [primerNombre], [segundoNombre], [primerApellido], [segundoApellido], [genero], [cedula], [telefono], [email]) VALUES (1, N'Aydan', N'Jacob', N'Navarro', N'Dávila', N'M', N'001-000000-0000A', N'86971668', NULL)
INSERT [dbo].[persona] ([idPersona], [primerNombre], [segundoNombre], [primerApellido], [segundoApellido], [genero], [cedula], [telefono], [email]) VALUES (2, N'Fredy', N'Fredmar', N'Garcia', N'Moreno', N'M', N'001-000000-0000B', N'75249682', NULL)
INSERT [dbo].[persona] ([idPersona], [primerNombre], [segundoNombre], [primerApellido], [segundoApellido], [genero], [cedula], [telefono], [email]) VALUES (3, N'Katherine', N'Alexandra', N'Ramirez', N'Urbina', N'F', N'001-000000-0000D', N'12345678', NULL)
INSERT [dbo].[persona] ([idPersona], [primerNombre], [segundoNombre], [primerApellido], [segundoApellido], [genero], [cedula], [telefono], [email]) VALUES (4, N'Kimberly', N'Genoveva', N'Trejos', N'Bravo', N'F', N'001-000000-0000E', N'87654321', NULL)
INSERT [dbo].[persona] ([idPersona], [primerNombre], [segundoNombre], [primerApellido], [segundoApellido], [genero], [cedula], [telefono], [email]) VALUES (5, N'Ricardo', N'Antonio', N'Gomez', N'Ruiz', N'M', N'001-050388-0003C', N'88334455', N'ricardo.gomez@email.com')
INSERT [dbo].[persona] ([idPersona], [primerNombre], [segundoNombre], [primerApellido], [segundoApellido], [genero], [cedula], [telefono], [email]) VALUES (6, N'Carlos', N'Alberto', N'Mejia', N'Godoy', N'O', N'001-120590-0001A', N'88112233', N'carlos.mejia@email.com')
INSERT [dbo].[persona] ([idPersona], [primerNombre], [segundoNombre], [primerApellido], [segundoApellido], [genero], [cedula], [telefono], [email]) VALUES (7, N'Ana', N'Lucia', N'Martinez', N'Silva', N'F', N'001-140792-0004D', N'88445566', N'ana.martinez@email.com')
INSERT [dbo].[persona] ([idPersona], [primerNombre], [segundoNombre], [primerApellido], [segundoApellido], [genero], [cedula], [telefono], [email]) VALUES (8, N'Che', N'Josue', N'Soza', N'Campeón del mundo', N'O', N'001-203213-2021A', N'87081981', NULL)
INSERT [dbo].[persona] ([idPersona], [primerNombre], [segundoNombre], [primerApellido], [segundoApellido], [genero], [cedula], [telefono], [email]) VALUES (9, N'Maria', N'Fernanda', N'Lopez', N'Perez', N'F', N'001-220895-0002B', N'88223344', N'maria.lopez@email.com')
INSERT [dbo].[persona] ([idPersona], [primerNombre], [segundoNombre], [primerApellido], [segundoApellido], [genero], [cedula], [telefono], [email]) VALUES (10, N'Josué', N'Javier', N'Soza', N'Sánchez', N'M', N'001-290507-1031V', N'81190872', N'josuejaviersoza@gmail.com')
INSERT [dbo].[persona] ([idPersona], [primerNombre], [segundoNombre], [primerApellido], [segundoApellido], [genero], [cedula], [telefono], [email]) VALUES (11, N'Luis', N'Enrique', N'Diaz', N'Rojas', N'M', N'001-301198-0005E', N'88556677', N'luis.diaz@email.com')
INSERT [dbo].[persona] ([idPersona], [primerNombre], [segundoNombre], [primerApellido], [segundoApellido], [genero], [cedula], [telefono], [email]) VALUES (12, N'Pe', N'Causa', N'Paloma', N'Causita', N'O', N'001-102343-2131V', N'82345464', NULL)
SET IDENTITY_INSERT [dbo].[persona] OFF
SET IDENTITY_INSERT [dbo].[rol] ON 

INSERT [dbo].[rol] ([idRol], [nombre], [descripcion]) VALUES (1, N'Administrador', N'Gestiona catálogos, clientes, empleados, usuarios, mesas, inventario y configuración. Supervisa pedidos, reservas, cobros y facturas.')
INSERT [dbo].[rol] ([idRol], [nombre], [descripcion]) VALUES (2, N'Empleado', N'Atiende pedidos, asigna mesas, registra reservas atendidas o ausencias, confirma limpieza, registra cobros y consulta facturas.')
INSERT [dbo].[rol] ([idRol], [nombre], [descripcion]) VALUES (3, N'Cliente', N'Realiza y consulta sus propios pedidos y reservas, actualiza su perfil y obtiene sus comprobantes y facturas.')
SET IDENTITY_INSERT [dbo].[rol] OFF
SET IDENTITY_INSERT [dbo].[usuario] ON 

INSERT [dbo].[usuario] ([idUsuario], [nombreUsuario], [contrasena], [idRol], [idPersona]) VALUES (1, N'JosueS', N'200714', 1, 10)
INSERT [dbo].[usuario] ([idUsuario], [nombreUsuario], [contrasena], [idRol], [idPersona]) VALUES (2, N'AydanN', N'17age', 1, 1)
INSERT [dbo].[usuario] ([idUsuario], [nombreUsuario], [contrasena], [idRol], [idPersona]) VALUES (3, N'FredG', N'14age', 1, 2)
INSERT [dbo].[usuario] ([idUsuario], [nombreUsuario], [contrasena], [idRol], [idPersona]) VALUES (4, N'Alex', N'12345', 1, 3)
INSERT [dbo].[usuario] ([idUsuario], [nombreUsuario], [contrasena], [idRol], [idPersona]) VALUES (5, N'KimberlyT', N'54321', 2, 4)
INSERT [dbo].[usuario] ([idUsuario], [nombreUsuario], [contrasena], [idRol], [idPersona]) VALUES (6, N'carlosM', N'pass123', 3, 6)
INSERT [dbo].[usuario] ([idUsuario], [nombreUsuario], [contrasena], [idRol], [idPersona]) VALUES (7, N'mariaL', N'pass123', 3, 9)
INSERT [dbo].[usuario] ([idUsuario], [nombreUsuario], [contrasena], [idRol], [idPersona]) VALUES (8, N'ricardoG', N'pass123', 3, 5)
INSERT [dbo].[usuario] ([idUsuario], [nombreUsuario], [contrasena], [idRol], [idPersona]) VALUES (9, N'anaM', N'pass123', 3, 7)
INSERT [dbo].[usuario] ([idUsuario], [nombreUsuario], [contrasena], [idRol], [idPersona]) VALUES (10, N'luisD', N'pass123', 3, 11)
INSERT [dbo].[usuario] ([idUsuario], [nombreUsuario], [contrasena], [idRol], [idPersona]) VALUES (26, N'SozaS', N'123', 2, 8)
INSERT [dbo].[usuario] ([idUsuario], [nombreUsuario], [contrasena], [idRol], [idPersona]) VALUES (27, N'Causa', N'pass123', 2, 12)
SET IDENTITY_INSERT [dbo].[usuario] OFF
SET IDENTITY_INSERT [dbo].[mesa] ON 

INSERT [dbo].[mesa] ([idMesa], [numero], [capacidad], [estado], [activa]) VALUES (1, 1, 2, N'Disponible', 1)
INSERT [dbo].[mesa] ([idMesa], [numero], [capacidad], [estado], [activa]) VALUES (2, 2, 4, N'Disponible', 1)
INSERT [dbo].[mesa] ([idMesa], [numero], [capacidad], [estado], [activa]) VALUES (3, 3, 4, N'Disponible', 1)
INSERT [dbo].[mesa] ([idMesa], [numero], [capacidad], [estado], [activa]) VALUES (4, 4, 6, N'Disponible', 1)
INSERT [dbo].[mesa] ([idMesa], [numero], [capacidad], [estado], [activa]) VALUES (5, 5, 8, N'Disponible', 1)
SET IDENTITY_INSERT [dbo].[mesa] OFF
SET IDENTITY_INSERT [dbo].[cliente] ON 

INSERT [dbo].[cliente] ([idCliente], [idUsuario], [activo], [idPersona]) VALUES (1, 6, 1, 6)
INSERT [dbo].[cliente] ([idCliente], [idUsuario], [activo], [idPersona]) VALUES (2, 7, 0, 9)
INSERT [dbo].[cliente] ([idCliente], [idUsuario], [activo], [idPersona]) VALUES (3, 8, 1, 5)
INSERT [dbo].[cliente] ([idCliente], [idUsuario], [activo], [idPersona]) VALUES (4, 9, 1, 7)
INSERT [dbo].[cliente] ([idCliente], [idUsuario], [activo], [idPersona]) VALUES (5, 10, 1, 11)
INSERT [dbo].[cliente] ([idCliente], [idUsuario], [activo], [idPersona]) VALUES (9, 1, 1, 10)
SET IDENTITY_INSERT [dbo].[cliente] OFF
SET IDENTITY_INSERT [dbo].[empleado] ON 

INSERT [dbo].[empleado] ([idEmpleado], [idUsuario], [cargo], [activo], [idPersona]) VALUES (1, 1, N'Encargado', 1, 10)
INSERT [dbo].[empleado] ([idEmpleado], [idUsuario], [cargo], [activo], [idPersona]) VALUES (2, 2, N'Administrador', 1, 1)
INSERT [dbo].[empleado] ([idEmpleado], [idUsuario], [cargo], [activo], [idPersona]) VALUES (3, 3, N'Administrador', 1, 2)
INSERT [dbo].[empleado] ([idEmpleado], [idUsuario], [cargo], [activo], [idPersona]) VALUES (4, 4, N'Gerente', 1, 3)
INSERT [dbo].[empleado] ([idEmpleado], [idUsuario], [cargo], [activo], [idPersona]) VALUES (5, 5, N'Cajero', 1, 4)
INSERT [dbo].[empleado] ([idEmpleado], [idUsuario], [cargo], [activo], [idPersona]) VALUES (16, 26, N'Mesero', 1, 8)
INSERT [dbo].[empleado] ([idEmpleado], [idUsuario], [cargo], [activo], [idPersona]) VALUES (17, 27, N'Despachador', 0, 12)
SET IDENTITY_INSERT [dbo].[empleado] OFF
SET IDENTITY_INSERT [dbo].[pedido] ON 

INSERT [dbo].[pedido] ([idPedido], [idCliente], [idMesa], [idEmpleado], [idEmpleadoRepartidor], [direccion], [referencia], [telefonoDeContacto], [fechaHora], [estado], [costoEnvio], [subtotal], [total], [esDelivery], [solicitud], [idOrigenPedido], [idTipoEntrega], [idUsuarioRegistro]) VALUES (1, NULL, 5, 1, NULL, NULL, NULL, NULL, CAST(N'2026-11-09T11:41:09.477' AS DateTime), N'Pagado', CAST(0.00 AS Decimal(10, 2)), CAST(605.00 AS Decimal(10, 2)), CAST(605.00 AS Decimal(10, 2)), 0, N'cd6393a6-50e5-4424-8c62-90c267afd8d7', 2, 1, 1)
SET IDENTITY_INSERT [dbo].[pedido] OFF
SET IDENTITY_INSERT [dbo].[categoria] ON 

INSERT [dbo].[categoria] ([idCategoria], [nombre], [descripcion], [activa]) VALUES (1, N'Entradas', N'Aperitivos y piqueos ligeros para comenzar', 1)
INSERT [dbo].[categoria] ([idCategoria], [nombre], [descripcion], [activa]) VALUES (2, N'Platos Fuertes', N'Especialidades de la casa, carnes, aves y mariscos', 1)
INSERT [dbo].[categoria] ([idCategoria], [nombre], [descripcion], [activa]) VALUES (3, N'Bebidas', N'Refrescos naturales, jugos, gaseosas y licores', 1)
INSERT [dbo].[categoria] ([idCategoria], [nombre], [descripcion], [activa]) VALUES (4, N'Postres', N'Pasteles, helados y postres tradicionales', 1)
INSERT [dbo].[categoria] ([idCategoria], [nombre], [descripcion], [activa]) VALUES (5, N'Sopas y Ensaladas', N'Opciones ligeras, caldos y ensaladas frescas', 1)
INSERT [dbo].[categoria] ([idCategoria], [nombre], [descripcion], [activa]) VALUES (6, N'Comida Rapida', N'Hamburguesas, pizzas, papas fritas', 1)
INSERT [dbo].[categoria] ([idCategoria], [nombre], [descripcion], [activa]) VALUES (18, N'Desayunos', N'Gallo pinto, huevos al gusto, tostadas y café.', 1)
INSERT [dbo].[categoria] ([idCategoria], [nombre], [descripcion], [activa]) VALUES (19, N'Bebidas Alcohólica', N'Ron plata, Cervezas.', 1)
SET IDENTITY_INSERT [dbo].[categoria] OFF
SET IDENTITY_INSERT [dbo].[subcategoria] ON 

INSERT [dbo].[subcategoria] ([idSubcategoria], [idCategoria], [nombre]) VALUES (3, 1, N'Entradas calientes')
INSERT [dbo].[subcategoria] ([idSubcategoria], [idCategoria], [nombre]) VALUES (4, 1, N'Entradas frías')
INSERT [dbo].[subcategoria] ([idSubcategoria], [idCategoria], [nombre]) VALUES (5, 2, N'Aves')
INSERT [dbo].[subcategoria] ([idSubcategoria], [idCategoria], [nombre]) VALUES (6, 2, N'Carnes')
INSERT [dbo].[subcategoria] ([idSubcategoria], [idCategoria], [nombre]) VALUES (7, 3, N'Gaseosas')
INSERT [dbo].[subcategoria] ([idSubcategoria], [idCategoria], [nombre]) VALUES (8, 3, N'Refrescos naturales')
INSERT [dbo].[subcategoria] ([idSubcategoria], [idCategoria], [nombre]) VALUES (9, 4, N'Helados')
INSERT [dbo].[subcategoria] ([idSubcategoria], [idCategoria], [nombre]) VALUES (10, 4, N'Pasteles')
INSERT [dbo].[subcategoria] ([idSubcategoria], [idCategoria], [nombre]) VALUES (11, 5, N'Ensaladas')
INSERT [dbo].[subcategoria] ([idSubcategoria], [idCategoria], [nombre]) VALUES (12, 5, N'Sopas')
INSERT [dbo].[subcategoria] ([idSubcategoria], [idCategoria], [nombre]) VALUES (13, 6, N'Hamburguesas')
INSERT [dbo].[subcategoria] ([idSubcategoria], [idCategoria], [nombre]) VALUES (14, 6, N'Pizzas')
SET IDENTITY_INSERT [dbo].[subcategoria] OFF
SET IDENTITY_INSERT [dbo].[producto] ON 

INSERT [dbo].[producto] ([idProducto], [idCategoria], [nombre], [precio], [descripcion], [codigo], [disponible], [idSubcategoria]) VALUES (1, 1, N'Dedos de Queso', CAST(120.00 AS Decimal(10, 2)), N'Bastones de queso empanizados, acompañados de salsa de tomate.', N'PROD-001', 1, 3)
INSERT [dbo].[producto] ([idProducto], [idCategoria], [nombre], [precio], [descripcion], [codigo], [disponible], [idSubcategoria]) VALUES (2, 2, N'Lomito a la Plancha', CAST(320.50 AS Decimal(10, 2)), N'Lomito de res a la plancha con arroz, vegetales y tostones.', N'PROD-002', 1, 6)
INSERT [dbo].[producto] ([idProducto], [idCategoria], [nombre], [precio], [descripcion], [codigo], [disponible], [idSubcategoria]) VALUES (3, 3, N'Jugo Natural de Naranja', CAST(45.00 AS Decimal(10, 2)), N'Jugo de naranja recién preparado, servido frío en vaso.', N'PROD-003', 1, 8)
INSERT [dbo].[producto] ([idProducto], [idCategoria], [nombre], [precio], [descripcion], [codigo], [disponible], [idSubcategoria]) VALUES (4, 4, N'Tres Leches', CAST(65.00 AS Decimal(10, 2)), N'Porción de bizcocho bañado en tres leches con crema.', N'PROD-004', 1, 10)
INSERT [dbo].[producto] ([idProducto], [idCategoria], [nombre], [precio], [descripcion], [codigo], [disponible], [idSubcategoria]) VALUES (5, 5, N'Sopa de Res', CAST(150.00 AS Decimal(10, 2)), N'Caldo de res con verduras, acompañado de arroz y tortilla.', N'PROD-005', 1, 12)
INSERT [dbo].[producto] ([idProducto], [idCategoria], [nombre], [precio], [descripcion], [codigo], [disponible], [idSubcategoria]) VALUES (6, 6, N'Hamburguesa con Tocino', CAST(210.00 AS Decimal(10, 2)), N'Hamburguesa de res con tocino, queso y vegetales; incluye papas fritas.', N'PROD-006', 1, 13)
INSERT [dbo].[producto] ([idProducto], [idCategoria], [nombre], [precio], [descripcion], [codigo], [disponible], [idSubcategoria]) VALUES (15, 1, N'Tostones con queso', CAST(95.00 AS Decimal(10, 2)), N'Plátano verde crujiente con queso rallado y ensalada de repollo.', N'MENU-007', 1, 3)
INSERT [dbo].[producto] ([idProducto], [idCategoria], [nombre], [precio], [descripcion], [codigo], [disponible], [idSubcategoria]) VALUES (16, 1, N'Ceviche de pescado', CAST(180.00 AS Decimal(10, 2)), N'Pescado marinado con limón, cebolla y cilantro; acompañado de tostadas.', N'MENU-008', 1, 4)
INSERT [dbo].[producto] ([idProducto], [idCategoria], [nombre], [precio], [descripcion], [codigo], [disponible], [idSubcategoria]) VALUES (17, 1, N'Bruschettas de tomate', CAST(85.00 AS Decimal(10, 2)), N'Pan tostado con tomate fresco, albahaca y aceite de oliva.', N'MENU-009', 1, 4)
INSERT [dbo].[producto] ([idProducto], [idCategoria], [nombre], [precio], [descripcion], [codigo], [disponible], [idSubcategoria]) VALUES (18, 2, N'Pollo a la plancha', CAST(220.00 AS Decimal(10, 2)), N'Pechuga de pollo con vegetales salteados y arroz.', N'MENU-011', 1, 5)
INSERT [dbo].[producto] ([idProducto], [idCategoria], [nombre], [precio], [descripcion], [codigo], [disponible], [idSubcategoria]) VALUES (19, 2, N'Pollo en salsa de hongos', CAST(245.00 AS Decimal(10, 2)), N'Pechuga de pollo en salsa cremosa de hongos con puré de papa.', N'MENU-012', 1, 5)
INSERT [dbo].[producto] ([idProducto], [idCategoria], [nombre], [precio], [descripcion], [codigo], [disponible], [idSubcategoria]) VALUES (20, 2, N'Churrasco de res', CAST(295.00 AS Decimal(10, 2)), N'Corte de res a la parrilla con chimichurri, arroz y ensalada.', N'MENU-010', 1, 6)
INSERT [dbo].[producto] ([idProducto], [idCategoria], [nombre], [precio], [descripcion], [codigo], [disponible], [idSubcategoria]) VALUES (21, 3, N'Gaseosa de cola', CAST(35.00 AS Decimal(10, 2)), N'Gaseosa de cola personal de 355 ml.', N'MENU-014', 1, 7)
INSERT [dbo].[producto] ([idProducto], [idCategoria], [nombre], [precio], [descripcion], [codigo], [disponible], [idSubcategoria]) VALUES (22, 3, N'Gaseosa de limón', CAST(35.00 AS Decimal(10, 2)), N'Gaseosa de limón personal de 355 ml.', N'MENU-015', 1, 7)
INSERT [dbo].[producto] ([idProducto], [idCategoria], [nombre], [precio], [descripcion], [codigo], [disponible], [idSubcategoria]) VALUES (23, 3, N'Refresco de maracuyá', CAST(40.00 AS Decimal(10, 2)), N'Refresco natural de maracuyá preparado al momento.', N'MENU-013', 1, 8)
INSERT [dbo].[producto] ([idProducto], [idCategoria], [nombre], [precio], [descripcion], [codigo], [disponible], [idSubcategoria]) VALUES (24, 4, N'Helado de vainilla', CAST(55.00 AS Decimal(10, 2)), N'Dos bolas de helado de vainilla con salsa de chocolate.', N'MENU-017', 1, 9)
INSERT [dbo].[producto] ([idProducto], [idCategoria], [nombre], [precio], [descripcion], [codigo], [disponible], [idSubcategoria]) VALUES (25, 4, N'Helado de fresa', CAST(55.00 AS Decimal(10, 2)), N'Dos bolas de helado de fresa con trozos de fruta.', N'MENU-018', 1, 9)
INSERT [dbo].[producto] ([idProducto], [idCategoria], [nombre], [precio], [descripcion], [codigo], [disponible], [idSubcategoria]) VALUES (26, 4, N'Pastel de chocolate', CAST(75.00 AS Decimal(10, 2)), N'Porción de pastel de chocolate con cobertura cremosa.', N'MENU-016', 1, 10)
INSERT [dbo].[producto] ([idProducto], [idCategoria], [nombre], [precio], [descripcion], [codigo], [disponible], [idSubcategoria]) VALUES (27, 5, N'Ensalada de la casa', CAST(110.00 AS Decimal(10, 2)), N'Lechuga, tomate, pepino y zanahoria con aderezo de la casa.', N'MENU-020', 1, 11)
INSERT [dbo].[producto] ([idProducto], [idCategoria], [nombre], [precio], [descripcion], [codigo], [disponible], [idSubcategoria]) VALUES (28, 5, N'Ensalada con pollo', CAST(165.00 AS Decimal(10, 2)), N'Vegetales frescos con pechuga a la plancha y aderezo de yogur.', N'MENU-021', 1, 11)
INSERT [dbo].[producto] ([idProducto], [idCategoria], [nombre], [precio], [descripcion], [codigo], [disponible], [idSubcategoria]) VALUES (29, 5, N'Sopa de pollo', CAST(130.00 AS Decimal(10, 2)), N'Caldo de pollo con verduras frescas, arroz y tortilla.', N'MENU-019', 1, 12)
INSERT [dbo].[producto] ([idProducto], [idCategoria], [nombre], [precio], [descripcion], [codigo], [disponible], [idSubcategoria]) VALUES (30, 6, N'Hamburguesa clásica', CAST(175.00 AS Decimal(10, 2)), N'Hamburguesa de res con queso, lechuga y tomate; incluye papas fritas.', N'MENU-022', 1, 13)
INSERT [dbo].[producto] ([idProducto], [idCategoria], [nombre], [precio], [descripcion], [codigo], [disponible], [idSubcategoria]) VALUES (31, 6, N'Pizza personal margarita', CAST(160.00 AS Decimal(10, 2)), N'Pizza personal con salsa de tomate, mozzarella y albahaca.', N'MENU-023', 1, 14)
INSERT [dbo].[producto] ([idProducto], [idCategoria], [nombre], [precio], [descripcion], [codigo], [disponible], [idSubcategoria]) VALUES (32, 6, N'Pizza personal de pepperoni', CAST(185.00 AS Decimal(10, 2)), N'Pizza personal con mozzarella y pepperoni.', N'MENU-024', 1, 14)
SET IDENTITY_INSERT [dbo].[producto] OFF
SET IDENTITY_INSERT [dbo].[detallePedido] ON 

INSERT [dbo].[detallePedido] ([idDetalle], [idPedido], [idProducto], [cantidad], [notasCocina], [precioUnitario], [subtotal], [total], [controlInventario]) VALUES (1, 1, 17, 2, NULL, CAST(85.00 AS Decimal(10, 2)), CAST(170.00 AS Decimal(10, 2)), CAST(170.00 AS Decimal(10, 2)), 1)
INSERT [dbo].[detallePedido] ([idDetalle], [idPedido], [idProducto], [cantidad], [notasCocina], [precioUnitario], [subtotal], [total], [controlInventario]) VALUES (2, 1, 4, 5, NULL, CAST(65.00 AS Decimal(10, 2)), CAST(325.00 AS Decimal(10, 2)), CAST(325.00 AS Decimal(10, 2)), 1)
INSERT [dbo].[detallePedido] ([idDetalle], [idPedido], [idProducto], [cantidad], [notasCocina], [precioUnitario], [subtotal], [total], [controlInventario]) VALUES (3, 1, 27, 1, NULL, CAST(110.00 AS Decimal(10, 2)), CAST(110.00 AS Decimal(10, 2)), CAST(110.00 AS Decimal(10, 2)), 1)
SET IDENTITY_INSERT [dbo].[detallePedido] OFF
SET IDENTITY_INSERT [dbo].[factura] ON 

INSERT [dbo].[factura] ([idFactura], [idPedido], [numeroFactura], [fechaEmision], [estado], [subtotal], [costoEnvio], [descuento], [impuestos], [propina], [total], [clienteNombre], [clienteCedula], [empleadoNombre], [tasaImpuesto], [restauranteNombre], [moneda], [mesaNumero], [tipoPedido], [origenPedido], [registradoPor], [atendidoPor], [direccionEntrega], [referenciaEntrega], [telefonoContacto], [tipoEntrega]) VALUES (1, 1, N'F-1', CAST(N'2026-11-09T16:22:46.567' AS DateTime), N'Pagada', CAST(526.09 AS Decimal(10, 2)), CAST(0.00 AS Decimal(10, 2)), CAST(0.00 AS Decimal(10, 2)), CAST(78.91 AS Decimal(10, 2)), CAST(0.00 AS Decimal(10, 2)), CAST(605.00 AS Decimal(10, 2)), N'Juan', NULL, N'JosueS', CAST(15.00 AS Decimal(5, 2)), N'RestaurantSM', N'C$', 5, N'En mesa', N'En el local', N'JosueS', N'JosueS', NULL, NULL, NULL, N'En mesa')
SET IDENTITY_INSERT [dbo].[factura] OFF
SET IDENTITY_INSERT [dbo].[historialPedido] ON 

INSERT [dbo].[historialPedido] ([id], [idPedido], [estadoAnterior], [estadoNuevo], [idUsuario], [fecha]) VALUES (1, 1, NULL, N'Pendiente', 1, CAST(N'2026-11-09T11:41:09.4790619' AS DateTime2))
INSERT [dbo].[historialPedido] ([id], [idPedido], [estadoAnterior], [estadoNuevo], [idUsuario], [fecha]) VALUES (2, 1, N'Pendiente', N'Entregado', 1, CAST(N'2026-11-09T11:44:34.1664516' AS DateTime2))
INSERT [dbo].[historialPedido] ([id], [idPedido], [estadoAnterior], [estadoNuevo], [idUsuario], [fecha]) VALUES (3, 1, N'Entregado', N'Pagado', 1, CAST(N'2026-11-09T16:22:46.5698778' AS DateTime2))
SET IDENTITY_INSERT [dbo].[historialPedido] OFF
SET IDENTITY_INSERT [dbo].[pago] ON 

INSERT [dbo].[pago] ([idPago], [idFactura], [metodoDePago], [monto], [fechaPago], [recibido], [cambio], [solicitud], [idUsuario], [notas]) VALUES (1, 1, N'Tarjeta', CAST(605.00 AS Decimal(10, 2)), CAST(N'2026-11-09T16:22:46.567' AS DateTime), CAST(605.00 AS Decimal(10, 2)), CAST(0.00 AS Decimal(10, 2)), N'19914fdd-392c-4123-b717-6ad04f8cccf1', 1, N'10/10')
SET IDENTITY_INSERT [dbo].[pago] OFF
SET IDENTITY_INSERT [dbo].[facturaDetalle] ON 

INSERT [dbo].[facturaDetalle] ([id], [idFactura], [idProducto], [producto], [cantidad], [precio], [subtotal]) VALUES (1, 1, 17, N'Bruschettas de tomate', 2, CAST(85.00 AS Decimal(10, 2)), CAST(170.00 AS Decimal(10, 2)))
INSERT [dbo].[facturaDetalle] ([id], [idFactura], [idProducto], [producto], [cantidad], [precio], [subtotal]) VALUES (2, 1, 4, N'Tres Leches', 5, CAST(65.00 AS Decimal(10, 2)), CAST(325.00 AS Decimal(10, 2)))
INSERT [dbo].[facturaDetalle] ([id], [idFactura], [idProducto], [producto], [cantidad], [precio], [subtotal]) VALUES (3, 1, 27, N'Ensalada de la casa', 1, CAST(110.00 AS Decimal(10, 2)), CAST(110.00 AS Decimal(10, 2)))
SET IDENTITY_INSERT [dbo].[facturaDetalle] OFF
SET IDENTITY_INSERT [dbo].[movimientoInventario] ON 

INSERT [dbo].[movimientoInventario] ([id], [idProducto], [producto], [anterior], [nuevo], [diferencia], [idUsuario], [usuario], [motivo], [fecha]) VALUES (1, 27, N'Ensalada de la casa', 10, 9, -1, 1, N'JosueS', N'Registro de pedido', CAST(N'2026-10-24T23:40:18.6278361' AS DateTime2))
INSERT [dbo].[movimientoInventario] ([id], [idProducto], [producto], [anterior], [nuevo], [diferencia], [idUsuario], [usuario], [motivo], [fecha]) VALUES (2, 27, N'Ensalada de la casa', 9, 4, -5, 6, N'carlosM', N'Registro de pedido', CAST(N'2026-10-27T00:46:14.8421571' AS DateTime2))
INSERT [dbo].[movimientoInventario] ([id], [idProducto], [producto], [anterior], [nuevo], [diferencia], [idUsuario], [usuario], [motivo], [fecha]) VALUES (3, 4, N'Tres Leches', 0, 20, 20, 1, N'JosueS', N'Alta de inventario', CAST(N'2026-10-27T09:20:02.5688462' AS DateTime2))
INSERT [dbo].[movimientoInventario] ([id], [idProducto], [producto], [anterior], [nuevo], [diferencia], [idUsuario], [usuario], [motivo], [fecha]) VALUES (4, 4, N'Tres Leches', 20, 17, -3, 1, N'JosueS', N'Fredy queria lechita', CAST(N'2026-10-27T09:21:33.5963655' AS DateTime2))
INSERT [dbo].[movimientoInventario] ([id], [idProducto], [producto], [anterior], [nuevo], [diferencia], [idUsuario], [usuario], [motivo], [fecha]) VALUES (5, 27, N'Ensalada de la casa', 4, 2, -2, 1, N'JosueS', N'Registro de pedido', CAST(N'2026-11-05T12:53:24.9745853' AS DateTime2))
INSERT [dbo].[movimientoInventario] ([id], [idProducto], [producto], [anterior], [nuevo], [diferencia], [idUsuario], [usuario], [motivo], [fecha]) VALUES (6, 4, N'Tres Leches', 17, 11, -6, 1, N'JosueS', N'Registro de pedido', CAST(N'2026-11-08T13:32:21.9751073' AS DateTime2))
INSERT [dbo].[movimientoInventario] ([id], [idProducto], [producto], [anterior], [nuevo], [diferencia], [idUsuario], [usuario], [motivo], [fecha]) VALUES (7, 4, N'Tres Leches', 11, 10, -1, 1, N'JosueS', N'Registro de pedido', CAST(N'2026-11-08T13:36:13.4986639' AS DateTime2))
INSERT [dbo].[movimientoInventario] ([id], [idProducto], [producto], [anterior], [nuevo], [diferencia], [idUsuario], [usuario], [motivo], [fecha]) VALUES (8, 27, N'Ensalada de la casa', 2, 0, -2, 1, N'JosueS', N'Registro de pedido', CAST(N'2026-11-08T13:36:13.4986639' AS DateTime2))
INSERT [dbo].[movimientoInventario] ([id], [idProducto], [producto], [anterior], [nuevo], [diferencia], [idUsuario], [usuario], [motivo], [fecha]) VALUES (9, 4, N'Tres Leches', 10, 7, -3, 1, N'JosueS', N'Edición de venta pendiente', CAST(N'2026-11-08T13:37:00.2414966' AS DateTime2))
INSERT [dbo].[movimientoInventario] ([id], [idProducto], [producto], [anterior], [nuevo], [diferencia], [idUsuario], [usuario], [motivo], [fecha]) VALUES (10, 4, N'Tres Leches', 7, 6, -1, 1, N'JosueS', N'Registro de pedido', CAST(N'2026-11-09T08:49:37.6302776' AS DateTime2))
INSERT [dbo].[movimientoInventario] ([id], [idProducto], [producto], [anterior], [nuevo], [diferencia], [idUsuario], [usuario], [motivo], [fecha]) VALUES (11, 27, N'Ensalada de la casa', 0, 10, 10, 1, N'JosueS', N'Ajuste desde gestion de inventario', CAST(N'2026-11-09T10:37:53.1924655' AS DateTime2))
INSERT [dbo].[movimientoInventario] ([id], [idProducto], [producto], [anterior], [nuevo], [diferencia], [idUsuario], [usuario], [motivo], [fecha]) VALUES (12, 17, N'Bruschettas de tomate', 0, 10, 10, 1, N'JosueS', N'Alta de inventario', CAST(N'2026-11-09T10:40:23.7497037' AS DateTime2))
INSERT [dbo].[movimientoInventario] ([id], [idProducto], [producto], [anterior], [nuevo], [diferencia], [idUsuario], [usuario], [motivo], [fecha]) VALUES (13, 23, N'Refresco de maracuyá', 0, 0, 0, 1, N'JosueS', N'Alta de inventario', CAST(N'2026-11-09T10:42:35.9653827' AS DateTime2))
INSERT [dbo].[movimientoInventario] ([id], [idProducto], [producto], [anterior], [nuevo], [diferencia], [idUsuario], [usuario], [motivo], [fecha]) VALUES (14, 23, N'Refresco de maracuyá', 0, 15, 15, 1, N'JosueS', N'Ajuste desde gestion de inventario', CAST(N'2026-11-09T10:43:08.2227714' AS DateTime2))
INSERT [dbo].[movimientoInventario] ([id], [idProducto], [producto], [anterior], [nuevo], [diferencia], [idUsuario], [usuario], [motivo], [fecha]) VALUES (15, 17, N'Bruschettas de tomate', 10, 8, -2, 1, N'JosueS', N'Registro de pedido', CAST(N'2026-11-09T11:41:09.5136393' AS DateTime2))
INSERT [dbo].[movimientoInventario] ([id], [idProducto], [producto], [anterior], [nuevo], [diferencia], [idUsuario], [usuario], [motivo], [fecha]) VALUES (16, 4, N'Tres Leches', 6, 1, -5, 1, N'JosueS', N'Registro de pedido', CAST(N'2026-11-09T11:41:09.5427492' AS DateTime2))
INSERT [dbo].[movimientoInventario] ([id], [idProducto], [producto], [anterior], [nuevo], [diferencia], [idUsuario], [usuario], [motivo], [fecha]) VALUES (17, 27, N'Ensalada de la casa', 10, 9, -1, 1, N'JosueS', N'Registro de pedido', CAST(N'2026-11-09T11:41:09.5427492' AS DateTime2))
INSERT [dbo].[movimientoInventario] ([id], [idProducto], [producto], [anterior], [nuevo], [diferencia], [idUsuario], [usuario], [motivo], [fecha]) VALUES (18, 1, N'Dedos de Queso', 0, 50, 50, 1, N'JosueS', N'Alta de inventario', CAST(N'2026-11-09T21:54:19.3924602' AS DateTime2))
INSERT [dbo].[movimientoInventario] ([id], [idProducto], [producto], [anterior], [nuevo], [diferencia], [idUsuario], [usuario], [motivo], [fecha]) VALUES (19, 2, N'Lomito a la Plancha', 0, 50, 50, 1, N'JosueS', N'Alta de inventario', CAST(N'2026-11-09T21:54:19.3964741' AS DateTime2))
INSERT [dbo].[movimientoInventario] ([id], [idProducto], [producto], [anterior], [nuevo], [diferencia], [idUsuario], [usuario], [motivo], [fecha]) VALUES (20, 3, N'Jugo Natural de Naranja', 0, 50, 50, 1, N'JosueS', N'Alta de inventario', CAST(N'2026-11-09T21:54:19.3964741' AS DateTime2))
INSERT [dbo].[movimientoInventario] ([id], [idProducto], [producto], [anterior], [nuevo], [diferencia], [idUsuario], [usuario], [motivo], [fecha]) VALUES (21, 5, N'Sopa de Res', 0, 50, 50, 1, N'JosueS', N'Alta de inventario', CAST(N'2026-11-09T21:54:19.3964741' AS DateTime2))
INSERT [dbo].[movimientoInventario] ([id], [idProducto], [producto], [anterior], [nuevo], [diferencia], [idUsuario], [usuario], [motivo], [fecha]) VALUES (22, 6, N'Hamburguesa con Tocino', 0, 50, 50, 1, N'JosueS', N'Alta de inventario', CAST(N'2026-11-09T21:54:19.3964741' AS DateTime2))
INSERT [dbo].[movimientoInventario] ([id], [idProducto], [producto], [anterior], [nuevo], [diferencia], [idUsuario], [usuario], [motivo], [fecha]) VALUES (23, 15, N'Tostones con queso', 0, 50, 50, 1, N'JosueS', N'Alta de inventario', CAST(N'2026-11-09T21:54:19.3964741' AS DateTime2))
INSERT [dbo].[movimientoInventario] ([id], [idProducto], [producto], [anterior], [nuevo], [diferencia], [idUsuario], [usuario], [motivo], [fecha]) VALUES (24, 16, N'Ceviche de pescado', 0, 50, 50, 1, N'JosueS', N'Alta de inventario', CAST(N'2026-11-09T21:54:19.3964741' AS DateTime2))
INSERT [dbo].[movimientoInventario] ([id], [idProducto], [producto], [anterior], [nuevo], [diferencia], [idUsuario], [usuario], [motivo], [fecha]) VALUES (25, 18, N'Pollo a la plancha', 0, 50, 50, 1, N'JosueS', N'Alta de inventario', CAST(N'2026-11-09T21:54:19.3974742' AS DateTime2))
INSERT [dbo].[movimientoInventario] ([id], [idProducto], [producto], [anterior], [nuevo], [diferencia], [idUsuario], [usuario], [motivo], [fecha]) VALUES (26, 19, N'Pollo en salsa de hongos', 0, 50, 50, 1, N'JosueS', N'Alta de inventario', CAST(N'2026-11-09T21:54:19.3974742' AS DateTime2))
INSERT [dbo].[movimientoInventario] ([id], [idProducto], [producto], [anterior], [nuevo], [diferencia], [idUsuario], [usuario], [motivo], [fecha]) VALUES (27, 20, N'Churrasco de res', 0, 50, 50, 1, N'JosueS', N'Alta de inventario', CAST(N'2026-11-09T21:54:19.3974742' AS DateTime2))
INSERT [dbo].[movimientoInventario] ([id], [idProducto], [producto], [anterior], [nuevo], [diferencia], [idUsuario], [usuario], [motivo], [fecha]) VALUES (28, 21, N'Gaseosa de cola', 0, 50, 50, 1, N'JosueS', N'Alta de inventario', CAST(N'2026-11-09T21:54:19.3974742' AS DateTime2))
INSERT [dbo].[movimientoInventario] ([id], [idProducto], [producto], [anterior], [nuevo], [diferencia], [idUsuario], [usuario], [motivo], [fecha]) VALUES (29, 22, N'Gaseosa de limón', 0, 50, 50, 1, N'JosueS', N'Alta de inventario', CAST(N'2026-11-09T21:54:19.3974742' AS DateTime2))
INSERT [dbo].[movimientoInventario] ([id], [idProducto], [producto], [anterior], [nuevo], [diferencia], [idUsuario], [usuario], [motivo], [fecha]) VALUES (30, 24, N'Helado de vainilla', 0, 50, 50, 1, N'JosueS', N'Alta de inventario', CAST(N'2026-11-09T21:54:19.3974742' AS DateTime2))
INSERT [dbo].[movimientoInventario] ([id], [idProducto], [producto], [anterior], [nuevo], [diferencia], [idUsuario], [usuario], [motivo], [fecha]) VALUES (31, 25, N'Helado de fresa', 0, 50, 50, 1, N'JosueS', N'Alta de inventario', CAST(N'2026-11-09T21:54:19.3974742' AS DateTime2))
INSERT [dbo].[movimientoInventario] ([id], [idProducto], [producto], [anterior], [nuevo], [diferencia], [idUsuario], [usuario], [motivo], [fecha]) VALUES (32, 26, N'Pastel de chocolate', 0, 50, 50, 1, N'JosueS', N'Alta de inventario', CAST(N'2026-11-09T21:54:19.3984739' AS DateTime2))
INSERT [dbo].[movimientoInventario] ([id], [idProducto], [producto], [anterior], [nuevo], [diferencia], [idUsuario], [usuario], [motivo], [fecha]) VALUES (33, 28, N'Ensalada con pollo', 0, 50, 50, 1, N'JosueS', N'Alta de inventario', CAST(N'2026-11-09T21:54:19.3984739' AS DateTime2))
INSERT [dbo].[movimientoInventario] ([id], [idProducto], [producto], [anterior], [nuevo], [diferencia], [idUsuario], [usuario], [motivo], [fecha]) VALUES (34, 29, N'Sopa de pollo', 0, 50, 50, 1, N'JosueS', N'Alta de inventario', CAST(N'2026-11-09T21:54:19.3984739' AS DateTime2))
INSERT [dbo].[movimientoInventario] ([id], [idProducto], [producto], [anterior], [nuevo], [diferencia], [idUsuario], [usuario], [motivo], [fecha]) VALUES (35, 30, N'Hamburguesa clásica', 0, 50, 50, 1, N'JosueS', N'Alta de inventario', CAST(N'2026-11-09T21:54:19.3984739' AS DateTime2))
INSERT [dbo].[movimientoInventario] ([id], [idProducto], [producto], [anterior], [nuevo], [diferencia], [idUsuario], [usuario], [motivo], [fecha]) VALUES (36, 31, N'Pizza personal margarita', 0, 50, 50, 1, N'JosueS', N'Alta de inventario', CAST(N'2026-11-09T21:54:19.3984739' AS DateTime2))
INSERT [dbo].[movimientoInventario] ([id], [idProducto], [producto], [anterior], [nuevo], [diferencia], [idUsuario], [usuario], [motivo], [fecha]) VALUES (37, 32, N'Pizza personal de pepperoni', 0, 50, 50, 1, N'JosueS', N'Alta de inventario', CAST(N'2026-11-09T21:54:19.3984739' AS DateTime2))
SET IDENTITY_INSERT [dbo].[movimientoInventario] OFF
SET IDENTITY_INSERT [dbo].[reservacion] ON 

INSERT [dbo].[reservacion] ([idReservacion], [idCliente], [idMesa], [fechaHoraReserva], [cantidadPersonas], [estado]) VALUES (1, 9, 1, CAST(N'2026-11-05T13:15:20.200' AS DateTime), 2, N'Atendida')
SET IDENTITY_INSERT [dbo].[reservacion] OFF
INSERT [dbo].[inventarioProducto] ([idProducto], [stockActual], [stockMinimo], [activo], [revision]) VALUES (1, 50, 5, 1, 1)
INSERT [dbo].[inventarioProducto] ([idProducto], [stockActual], [stockMinimo], [activo], [revision]) VALUES (2, 50, 5, 1, 1)
INSERT [dbo].[inventarioProducto] ([idProducto], [stockActual], [stockMinimo], [activo], [revision]) VALUES (3, 50, 5, 1, 1)
INSERT [dbo].[inventarioProducto] ([idProducto], [stockActual], [stockMinimo], [activo], [revision]) VALUES (4, 1, 5, 1, 7)
INSERT [dbo].[inventarioProducto] ([idProducto], [stockActual], [stockMinimo], [activo], [revision]) VALUES (5, 50, 5, 1, 1)
INSERT [dbo].[inventarioProducto] ([idProducto], [stockActual], [stockMinimo], [activo], [revision]) VALUES (6, 50, 5, 1, 1)
INSERT [dbo].[inventarioProducto] ([idProducto], [stockActual], [stockMinimo], [activo], [revision]) VALUES (15, 50, 5, 1, 1)
INSERT [dbo].[inventarioProducto] ([idProducto], [stockActual], [stockMinimo], [activo], [revision]) VALUES (16, 50, 5, 1, 1)
INSERT [dbo].[inventarioProducto] ([idProducto], [stockActual], [stockMinimo], [activo], [revision]) VALUES (17, 8, 5, 1, 2)
INSERT [dbo].[inventarioProducto] ([idProducto], [stockActual], [stockMinimo], [activo], [revision]) VALUES (18, 50, 5, 1, 1)
INSERT [dbo].[inventarioProducto] ([idProducto], [stockActual], [stockMinimo], [activo], [revision]) VALUES (19, 50, 5, 1, 1)
INSERT [dbo].[inventarioProducto] ([idProducto], [stockActual], [stockMinimo], [activo], [revision]) VALUES (20, 50, 5, 1, 1)
INSERT [dbo].[inventarioProducto] ([idProducto], [stockActual], [stockMinimo], [activo], [revision]) VALUES (21, 50, 5, 1, 1)
INSERT [dbo].[inventarioProducto] ([idProducto], [stockActual], [stockMinimo], [activo], [revision]) VALUES (22, 50, 5, 1, 1)
INSERT [dbo].[inventarioProducto] ([idProducto], [stockActual], [stockMinimo], [activo], [revision]) VALUES (23, 15, 5, 1, 2)
INSERT [dbo].[inventarioProducto] ([idProducto], [stockActual], [stockMinimo], [activo], [revision]) VALUES (24, 50, 5, 1, 1)
INSERT [dbo].[inventarioProducto] ([idProducto], [stockActual], [stockMinimo], [activo], [revision]) VALUES (25, 50, 5, 1, 1)
INSERT [dbo].[inventarioProducto] ([idProducto], [stockActual], [stockMinimo], [activo], [revision]) VALUES (26, 50, 5, 1, 1)
INSERT [dbo].[inventarioProducto] ([idProducto], [stockActual], [stockMinimo], [activo], [revision]) VALUES (27, 9, 5, 1, 8)
INSERT [dbo].[inventarioProducto] ([idProducto], [stockActual], [stockMinimo], [activo], [revision]) VALUES (28, 50, 5, 1, 1)
INSERT [dbo].[inventarioProducto] ([idProducto], [stockActual], [stockMinimo], [activo], [revision]) VALUES (29, 50, 5, 1, 1)
INSERT [dbo].[inventarioProducto] ([idProducto], [stockActual], [stockMinimo], [activo], [revision]) VALUES (30, 50, 5, 1, 1)
INSERT [dbo].[inventarioProducto] ([idProducto], [stockActual], [stockMinimo], [activo], [revision]) VALUES (31, 50, 5, 1, 1)
INSERT [dbo].[inventarioProducto] ([idProducto], [stockActual], [stockMinimo], [activo], [revision]) VALUES (32, 50, 5, 1, 1)
INSERT [dbo].[configuracionRestaurante] ([id], [nombre], [moneda], [tasaImpuesto], [tarifaEnvio]) VALUES (1, N'RestaurantSM', N'C$', CAST(15.00 AS Decimal(5, 2)), NULL)

    -- Se vuelven a validar las relaciones y restricciones sobre todos los datos.
    ALTER TABLE [dbo].[anulacionVenta] WITH CHECK CHECK CONSTRAINT ALL;
    ALTER TABLE [dbo].[categoria] WITH CHECK CHECK CONSTRAINT ALL;
    ALTER TABLE [dbo].[cliente] WITH CHECK CHECK CONSTRAINT ALL;
    ALTER TABLE [dbo].[configuracionRestaurante] WITH CHECK CHECK CONSTRAINT ALL;
    ALTER TABLE [dbo].[detallePedido] WITH CHECK CHECK CONSTRAINT ALL;
    ALTER TABLE [dbo].[empleado] WITH CHECK CHECK CONSTRAINT ALL;
    ALTER TABLE [dbo].[factura] WITH CHECK CHECK CONSTRAINT ALL;
    ALTER TABLE [dbo].[facturaDetalle] WITH CHECK CHECK CONSTRAINT ALL;
    ALTER TABLE [dbo].[historialPedido] WITH CHECK CHECK CONSTRAINT ALL;
    ALTER TABLE [dbo].[inventarioProducto] WITH CHECK CHECK CONSTRAINT ALL;
    ALTER TABLE [dbo].[mesa] WITH CHECK CHECK CONSTRAINT ALL;
    ALTER TABLE [dbo].[movimientoInventario] WITH CHECK CHECK CONSTRAINT ALL;
    ALTER TABLE [dbo].[origenPedido] WITH CHECK CHECK CONSTRAINT ALL;
    ALTER TABLE [dbo].[pago] WITH CHECK CHECK CONSTRAINT ALL;
    ALTER TABLE [dbo].[pedido] WITH CHECK CHECK CONSTRAINT ALL;
    ALTER TABLE [dbo].[persona] WITH CHECK CHECK CONSTRAINT ALL;
    ALTER TABLE [dbo].[producto] WITH CHECK CHECK CONSTRAINT ALL;
    ALTER TABLE [dbo].[reservacion] WITH CHECK CHECK CONSTRAINT ALL;
    ALTER TABLE [dbo].[rol] WITH CHECK CHECK CONSTRAINT ALL;
    ALTER TABLE [dbo].[subcategoria] WITH CHECK CHECK CONSTRAINT ALL;
    ALTER TABLE [dbo].[tipoEntrega] WITH CHECK CHECK CONSTRAINT ALL;
    ALTER TABLE [dbo].[usuario] WITH CHECK CHECK CONSTRAINT ALL;

    DBCC CHECKIDENT ('[dbo].[categoria]', RESEED, 1018) WITH NO_INFOMSGS;
    DBCC CHECKIDENT ('[dbo].[cliente]', RESEED, 1008) WITH NO_INFOMSGS;
    DBCC CHECKIDENT ('[dbo].[detallePedido]', RESEED, 3) WITH NO_INFOMSGS;
    DBCC CHECKIDENT ('[dbo].[empleado]', RESEED, 17) WITH NO_INFOMSGS;
    DBCC CHECKIDENT ('[dbo].[factura]', RESEED, 1) WITH NO_INFOMSGS;
    DBCC CHECKIDENT ('[dbo].[facturaDetalle]', RESEED, 3) WITH NO_INFOMSGS;
    DBCC CHECKIDENT ('[dbo].[historialPedido]', RESEED, 3) WITH NO_INFOMSGS;
    DBCC CHECKIDENT ('[dbo].[mesa]', RESEED, 5) WITH NO_INFOMSGS;
    DBCC CHECKIDENT ('[dbo].[movimientoInventario]', RESEED, 37) WITH NO_INFOMSGS;
    DBCC CHECKIDENT ('[dbo].[pago]', RESEED, 1) WITH NO_INFOMSGS;
    DBCC CHECKIDENT ('[dbo].[pedido]', RESEED, 1) WITH NO_INFOMSGS;
    DBCC CHECKIDENT ('[dbo].[persona]', RESEED, 12) WITH NO_INFOMSGS;
    DBCC CHECKIDENT ('[dbo].[producto]', RESEED, 32) WITH NO_INFOMSGS;
    DBCC CHECKIDENT ('[dbo].[reservacion]', RESEED, 1) WITH NO_INFOMSGS;
    DBCC CHECKIDENT ('[dbo].[rol]', RESEED, 3) WITH NO_INFOMSGS;
    DBCC CHECKIDENT ('[dbo].[subcategoria]', RESEED, 14) WITH NO_INFOMSGS;
    DBCC CHECKIDENT ('[dbo].[usuario]', RESEED, 27) WITH NO_INFOMSGS;

    IF (SELECT COUNT_BIG(*) FROM [dbo].[anulacionVenta]) <> 0
        THROW 51000, N'No coincide el número de filas en [dbo].[anulacionVenta].', 1;
    IF (SELECT COUNT_BIG(*) FROM [dbo].[categoria]) <> 8
        THROW 51000, N'No coincide el número de filas en [dbo].[categoria].', 1;
    IF (SELECT COUNT_BIG(*) FROM [dbo].[cliente]) <> 6
        THROW 51000, N'No coincide el número de filas en [dbo].[cliente].', 1;
    IF (SELECT COUNT_BIG(*) FROM [dbo].[configuracionRestaurante]) <> 1
        THROW 51000, N'No coincide el número de filas en [dbo].[configuracionRestaurante].', 1;
    IF (SELECT COUNT_BIG(*) FROM [dbo].[detallePedido]) <> 3
        THROW 51000, N'No coincide el número de filas en [dbo].[detallePedido].', 1;
    IF (SELECT COUNT_BIG(*) FROM [dbo].[empleado]) <> 7
        THROW 51000, N'No coincide el número de filas en [dbo].[empleado].', 1;
    IF (SELECT COUNT_BIG(*) FROM [dbo].[factura]) <> 1
        THROW 51000, N'No coincide el número de filas en [dbo].[factura].', 1;
    IF (SELECT COUNT_BIG(*) FROM [dbo].[facturaDetalle]) <> 3
        THROW 51000, N'No coincide el número de filas en [dbo].[facturaDetalle].', 1;
    IF (SELECT COUNT_BIG(*) FROM [dbo].[historialPedido]) <> 3
        THROW 51000, N'No coincide el número de filas en [dbo].[historialPedido].', 1;
    IF (SELECT COUNT_BIG(*) FROM [dbo].[inventarioProducto]) <> 24
        THROW 51000, N'No coincide el número de filas en [dbo].[inventarioProducto].', 1;
    IF (SELECT COUNT_BIG(*) FROM [dbo].[mesa]) <> 5
        THROW 51000, N'No coincide el número de filas en [dbo].[mesa].', 1;
    IF (SELECT COUNT_BIG(*) FROM [dbo].[movimientoInventario]) <> 37
        THROW 51000, N'No coincide el número de filas en [dbo].[movimientoInventario].', 1;
    IF (SELECT COUNT_BIG(*) FROM [dbo].[origenPedido]) <> 3
        THROW 51000, N'No coincide el número de filas en [dbo].[origenPedido].', 1;
    IF (SELECT COUNT_BIG(*) FROM [dbo].[pago]) <> 1
        THROW 51000, N'No coincide el número de filas en [dbo].[pago].', 1;
    IF (SELECT COUNT_BIG(*) FROM [dbo].[pedido]) <> 1
        THROW 51000, N'No coincide el número de filas en [dbo].[pedido].', 1;
    IF (SELECT COUNT_BIG(*) FROM [dbo].[persona]) <> 12
        THROW 51000, N'No coincide el número de filas en [dbo].[persona].', 1;
    IF (SELECT COUNT_BIG(*) FROM [dbo].[producto]) <> 24
        THROW 51000, N'No coincide el número de filas en [dbo].[producto].', 1;
    IF (SELECT COUNT_BIG(*) FROM [dbo].[reservacion]) <> 1
        THROW 51000, N'No coincide el número de filas en [dbo].[reservacion].', 1;
    IF (SELECT COUNT_BIG(*) FROM [dbo].[rol]) <> 3
        THROW 51000, N'No coincide el número de filas en [dbo].[rol].', 1;
    IF (SELECT COUNT_BIG(*) FROM [dbo].[subcategoria]) <> 12
        THROW 51000, N'No coincide el número de filas en [dbo].[subcategoria].', 1;
    IF (SELECT COUNT_BIG(*) FROM [dbo].[tipoEntrega]) <> 4
        THROW 51000, N'No coincide el número de filas en [dbo].[tipoEntrega].', 1;
    IF (SELECT COUNT_BIG(*) FROM [dbo].[usuario]) <> 12
        THROW 51000, N'No coincide el número de filas en [dbo].[usuario].', 1;

    COMMIT TRANSACTION;
    PRINT N'RestaurantSM se creó correctamente con el esquema y los datos.';
END TRY
BEGIN CATCH
    IF XACT_STATE() <> 0
        ROLLBACK TRANSACTION;

    PRINT N'La carga se revirtió. La base recién creada puede quedar vacía. Revise el error antes de volver a intentar.';
    THROW;
END CATCH;
GO

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

SET NOEXEC OFF;
GO
