-- Aplicar sobre RestaurantSM después de las migraciones 001 a 004.
-- Clientes y mesas: baja lógica. Inventario: unidades de producto terminado.
-- Todo el acceso SQL de la DAL se realiza mediante procedimientos almacenados.
USE [RestaurantSM];
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
      activo bit NOT NULL DEFAULT(1), version rowversion NOT NULL);
GO
CREATE OR ALTER PROCEDURE dbo.sp_Gestion_Clientes @Actor int
AS
BEGIN
 EXEC dbo.sp_Catalogo_ExigirAdministrador @Actor;
 SELECT idCliente Id,PNombreCl PrimerNombre,SNombreCl SegundoNombre,PApellidoCl PrimerApellido,
 SApellidoCl SegundoApellido,genero Genero,cedula Cedula,telefono Telefono,email Email,activo Activo
 FROM dbo.cliente ORDER BY PNombreCl,PApellidoCl;
END;
GO
CREATE OR ALTER PROCEDURE dbo.sp_Cliente_Guardar
 @Actor int,@Id int=0,@PrimerNombre varchar(80),@SegundoNombre varchar(80)=NULL,
 @PrimerApellido varchar(80),@SegundoApellido varchar(80)=NULL,@Genero char(1),
 @Cedula varchar(30)=NULL,@Telefono varchar(20)=NULL,@Email varchar(120)=NULL,@Activo bit=1
AS
BEGIN
 SET NOCOUNT ON; SET XACT_ABORT ON;
 IF NOT EXISTS(SELECT 1 FROM dbo.usuario WHERE idUsuario=@Actor AND idRol=1)
 BEGIN
   IF NOT EXISTS(SELECT 1 FROM dbo.cliente c JOIN dbo.usuario u ON u.idUsuario=c.idUsuario
     WHERE c.idCliente=@Id AND c.idUsuario=@Actor AND u.idRol=3 AND c.activo=1 AND @Activo=1)
     THROW 50200,'Solo puede modificar sus propios datos de cliente.',1;
 END
 ELSE EXEC dbo.sp_Catalogo_ExigirAdministrador @Actor;
 IF NULLIF(LTRIM(RTRIM(@PrimerNombre)),'') IS NULL OR NULLIF(LTRIM(RTRIM(@PrimerApellido)),'') IS NULL OR @Genero NOT IN('M','F','O')
   THROW 50201,'Revise nombre, apellido y género.',1;
 BEGIN TRY
 BEGIN TRANSACTION;
 DECLARE @Existe int;
 SELECT @Existe=idCliente FROM dbo.cliente WITH(UPDLOCK,HOLDLOCK) WHERE idCliente=@Id;
 IF @Activo=0 AND (EXISTS(SELECT 1 FROM dbo.pedido WHERE idCliente=@Id AND estado NOT IN('Cancelado','Entregado'))
   OR EXISTS(SELECT 1 FROM dbo.reservacion WHERE idCliente=@Id AND estado IN('Pendiente','Confirmada') AND fechaHoraReserva>=GETDATE()))
   THROW 50202,'El cliente tiene pedidos o reservas pendientes.',1;
 IF @Id=0
 BEGIN
   INSERT dbo.cliente(PNombreCl,SNombreCl,PApellidoCl,SApellidoCl,genero,cedula,telefono,email,activo)
   VALUES(@PrimerNombre,@SegundoNombre,@PrimerApellido,@SegundoApellido,@Genero,NULLIF(@Cedula,''),NULLIF(@Telefono,''),NULLIF(@Email,''),@Activo);
   SET @Id=CONVERT(int,SCOPE_IDENTITY());
 END
 ELSE
 BEGIN
   UPDATE dbo.cliente SET PNombreCl=@PrimerNombre,SNombreCl=@SegundoNombre,PApellidoCl=@PrimerApellido,
   SApellidoCl=@SegundoApellido,genero=@Genero,cedula=NULLIF(@Cedula,''),telefono=NULLIF(@Telefono,''),email=NULLIF(@Email,''),activo=@Activo WHERE idCliente=@Id;
   IF @@ROWCOUNT=0 THROW 50203,'El cliente ya no existe.',1;
 END
 COMMIT; SELECT @Id;
 END TRY BEGIN CATCH IF @@TRANCOUNT>0 ROLLBACK; THROW; END CATCH
END;
GO
CREATE OR ALTER PROCEDURE dbo.sp_Usuario_ValidarLogin @NombreUsuario varchar(50),@Contrasena varchar(255)
AS
BEGIN
 SET NOCOUNT ON;
 SELECT u.idUsuario,u.nombreUsuario,u.idRol,r.nombre NombreRol FROM dbo.usuario u JOIN dbo.rol r ON r.idRol=u.idRol
 WHERE u.nombreUsuario=@NombreUsuario AND u.contrasena=@Contrasena
 AND NOT EXISTS(SELECT 1 FROM dbo.empleado e WHERE e.idUsuario=u.idUsuario AND e.activo=0)
 AND NOT EXISTS(SELECT 1 FROM dbo.cliente c WHERE c.idUsuario=u.idUsuario AND c.activo=0);
END;
GO
CREATE OR ALTER PROCEDURE dbo.sp_Rol_Guardar @Actor int,@Id int=0,@Nombre varchar(50),@Descripcion varchar(150)=NULL
AS
BEGIN
 EXEC dbo.sp_Catalogo_ExigirAdministrador @Actor;
 IF NULLIF(LTRIM(RTRIM(@Nombre)),'') IS NULL THROW 50204,'El nombre del rol es obligatorio.',1;
 IF @Id IN(1,2,3) AND EXISTS(SELECT 1 FROM dbo.rol WHERE idRol=@Id AND nombre<>@Nombre)
   THROW 50205,'Los roles del sistema no se pueden renombrar; puede editar su descripción.',1;
 IF @Id=0
 BEGIN
   INSERT dbo.rol(nombre,descripcion) VALUES(@Nombre,@Descripcion);
   SELECT CONVERT(int,SCOPE_IDENTITY());
 END
 ELSE
 BEGIN
   UPDATE dbo.rol SET nombre=@Nombre,descripcion=@Descripcion WHERE idRol=@Id;
   IF @@ROWCOUNT=0 THROW 50206,'El rol ya no existe.',1;
   SELECT @Id;
 END
END;
GO
CREATE OR ALTER PROCEDURE dbo.sp_Rol_Eliminar @Actor int,@Id int
AS
BEGIN
 EXEC dbo.sp_Catalogo_ExigirAdministrador @Actor;
 IF @Id IN(1,2,3) THROW 50207,'Administrador, Empleado y Cliente son roles protegidos.',1;
 IF EXISTS(SELECT 1 FROM dbo.usuario WHERE idRol=@Id) THROW 50208,'El rol tiene usuarios asociados.',1;
 DELETE dbo.rol WHERE idRol=@Id;
 IF @@ROWCOUNT=0 THROW 50206,'El rol ya no existe.',1;
END;
GO
CREATE OR ALTER PROCEDURE dbo.sp_Mesa_Listar AS
 SELECT idMesa IdMesa,numero Numero,capacidad Capacidad,estado Estado,activa Activa FROM dbo.mesa ORDER BY numero;
GO
CREATE OR ALTER PROCEDURE dbo.sp_Mesa_Guardar @Actor int,@Id int=0,@Numero int,@Capacidad int,@Estado varchar(20),@Activa bit
AS
BEGIN
 SET XACT_ABORT ON;
 EXEC dbo.sp_Catalogo_ExigirAdministrador @Actor;
 IF @Numero<1 OR @Capacidad NOT BETWEEN 1 AND 20 OR @Estado NOT IN('Disponible','Ocupada','Reservada','En limpieza')
   THROW 50209,'Número, capacidad o estado de mesa no válido.',1;
 BEGIN TRY
 BEGIN TRANSACTION;
 IF @Id<>0 AND (@Activa=0 OR @Estado='Disponible') AND EXISTS(SELECT 1 FROM dbo.pedido WITH(UPDLOCK,HOLDLOCK) WHERE idMesa=@Id AND estado NOT IN('Cancelado','Entregado'))
   THROW 50210,'La mesa tiene pedidos pendientes.',1;
 IF @Id<>0 AND EXISTS(SELECT 1 FROM dbo.reservacion WITH(UPDLOCK,HOLDLOCK) WHERE idMesa=@Id AND estado IN('Pendiente','Confirmada') AND fechaHoraReserva>=GETDATE() AND (@Activa=0 OR cantidadPersonas>@Capacidad))
   THROW 50211,'La mesa tiene reservas incompatibles con este cambio.',1;
 IF @Id=0
 BEGIN
   INSERT dbo.mesa(numero,capacidad,estado,activa) VALUES(@Numero,@Capacidad,@Estado,@Activa);
   SET @Id=CONVERT(int,SCOPE_IDENTITY());
 END
 ELSE
 BEGIN
   UPDATE dbo.mesa SET numero=@Numero,capacidad=@Capacidad,estado=@Estado,activa=@Activa WHERE idMesa=@Id;
   IF @@ROWCOUNT=0 THROW 50212,'La mesa ya no existe.',1;
 END
 COMMIT; SELECT @Id;
 END TRY BEGIN CATCH IF @@TRANCOUNT>0 ROLLBACK; THROW; END CATCH
END;
GO
CREATE OR ALTER PROCEDURE dbo.sp_Inventario_Listar @Actor int
AS
BEGIN
 EXEC dbo.sp_Catalogo_ExigirAdministrador @Actor;
 SELECT p.idProducto IdProducto,p.nombre Producto,c.nombre Categoria,p.precio PrecioVenta,
 'Unidad' Unidad,i.stockActual StockActual,i.stockMinimo StockMinimo,i.activo Activo,i.version Version
 FROM dbo.inventarioProducto i JOIN dbo.producto p ON p.idProducto=i.idProducto JOIN dbo.categoria c ON c.idCategoria=p.idCategoria
 ORDER BY p.nombre;
END;
GO
CREATE OR ALTER PROCEDURE dbo.sp_Inventario_Guardar @Actor int,@IdProducto int,@Stock int,@Minimo int,@Activo bit,@Version binary(8)=NULL
AS
BEGIN
 SET NOCOUNT ON; SET XACT_ABORT ON;
 EXEC dbo.sp_Catalogo_ExigirAdministrador @Actor;
 IF @Stock<0 OR @Minimo<0 THROW 50213,'Las existencias no pueden ser negativas.',1;
 BEGIN TRY
 BEGIN TRANSACTION;
 IF NOT EXISTS(SELECT 1 FROM dbo.producto WITH(UPDLOCK,HOLDLOCK) WHERE idProducto=@IdProducto) THROW 50214,'Seleccione un producto existente.',1;
 IF @Activo=0 AND (@Stock<>0 OR EXISTS(SELECT 1 FROM dbo.detallePedido d JOIN dbo.pedido p ON p.idPedido=d.idPedido
   WHERE d.idProducto=@IdProducto AND d.controlInventario=1 AND p.estado NOT IN('Entregado','Cancelado')))
   THROW 50215,'Solo puede retirar el control de inventario con stock cero y sin pedidos pendientes.',1;
 IF EXISTS(SELECT 1 FROM dbo.inventarioProducto WITH(UPDLOCK,HOLDLOCK) WHERE idProducto=@IdProducto)
 BEGIN
   UPDATE dbo.inventarioProducto SET stockActual=@Stock,stockMinimo=@Minimo,activo=@Activo
   WHERE idProducto=@IdProducto AND version=@Version;
   IF @@ROWCOUNT=0 THROW 50216,'Las existencias cambiaron. Recargue antes de guardar.',1;
 END
 ELSE
 BEGIN
   IF @Version IS NOT NULL THROW 50216,'Las existencias cambiaron. Recargue antes de guardar.',1;
   INSERT dbo.inventarioProducto(idProducto,stockActual,stockMinimo,activo) VALUES(@IdProducto,@Stock,@Minimo,@Activo);
 END
 COMMIT;
 END TRY BEGIN CATCH IF @@TRANCOUNT>0 ROLLBACK; THROW; END CATCH
END;
GO
CREATE OR ALTER PROCEDURE dbo.sp_Cliente_ExigirActivo @Actor int,@IdCliente int OUTPUT
AS
BEGIN
 SET @IdCliente=NULL;
 SELECT @IdCliente=c.idCliente FROM dbo.cliente c WITH(UPDLOCK,HOLDLOCK) JOIN dbo.usuario u ON u.idUsuario=c.idUsuario
 WHERE c.idUsuario=@Actor AND u.idRol=3 AND c.activo=1;
 IF @IdCliente IS NULL THROW 50217,'Necesita una cuenta de cliente activa con perfil asociado.',1;
END;
GO
CREATE OR ALTER PROCEDURE dbo.sp_Cliente_Pedidos @Actor int
AS
BEGIN
 DECLARE @Cliente int; EXEC dbo.sp_Cliente_ExigirActivo @Actor,@Cliente OUTPUT;
 SELECT idPedido IdPedido,fechaHora Fecha,estado Estado,total Total FROM dbo.pedido WHERE idCliente=@Cliente ORDER BY idPedido DESC;
END;
GO
CREATE OR ALTER PROCEDURE dbo.sp_Cliente_Detalle @Actor int,@IdPedido int
AS
BEGIN
 DECLARE @Cliente int; EXEC dbo.sp_Cliente_ExigirActivo @Actor,@Cliente OUTPUT;
 IF NOT EXISTS(SELECT 1 FROM dbo.pedido WHERE idPedido=@IdPedido AND idCliente=@Cliente) THROW 50218,'El pedido no pertenece a su cuenta.',1;
 SELECT d.idProducto IdProducto,p.nombre Producto,p.idCategoria IdCategoria,c.nombre Categoria,
 d.cantidad Cantidad,d.precioUnitario PrecioUnitario,d.total Subtotal FROM dbo.detallePedido d
 JOIN dbo.producto p ON p.idProducto=d.idProducto JOIN dbo.categoria c ON c.idCategoria=p.idCategoria WHERE d.idPedido=@IdPedido;
END;
GO
CREATE OR ALTER PROCEDURE dbo.sp_Cliente_Pedido_Guardar @Actor int,@IdPedido int=0,@Solicitud uniqueidentifier,@Detalle xml
AS
BEGIN
 SET NOCOUNT ON; SET XACT_ABORT ON;
 DECLARE @Cliente int;
 IF @Solicitud IS NULL THROW 50230,'Falta el identificador de la solicitud.',1;
 DECLARE @Lineas TABLE(idProducto int PRIMARY KEY,cantidad int,precio decimal(10,2),control bit DEFAULT(0));
 IF @Detalle IS NULL OR @Detalle.value('count(/pedido/item)','int') NOT BETWEEN 1 AND 200
   THROW 50219,'El pedido debe tener entre 1 y 200 productos distintos.',1;
 INSERT @Lineas(idProducto,cantidad,precio)
 SELECT n.value('@producto','int'),n.value('@cantidad','int'),n.value('@precio','decimal(10,2)') FROM @Detalle.nodes('/pedido/item') t(n);
 IF EXISTS(SELECT 1 FROM @Lineas WHERE cantidad NOT BETWEEN 1 AND 999) THROW 50220,'Cantidad no válida.',1;
 BEGIN TRY
 BEGIN TRANSACTION;
 EXEC dbo.sp_Cliente_ExigirActivo @Actor,@Cliente OUTPUT;
 IF @IdPedido=0
 BEGIN
   DECLARE @Existente int;
   SELECT @Existente=idPedido FROM dbo.pedido WITH(UPDLOCK,HOLDLOCK) WHERE solicitud=@Solicitud AND idCliente=@Cliente;
   IF @Existente IS NOT NULL BEGIN COMMIT; SELECT @Existente; RETURN; END;
 END
 ELSE
 BEGIN
   IF NOT EXISTS(SELECT 1 FROM dbo.pedido WITH(UPDLOCK,HOLDLOCK) WHERE idPedido=@IdPedido AND idCliente=@Cliente AND estado='Pendiente')
     THROW 50221,'Solo puede editar sus pedidos pendientes.',1;
   IF EXISTS(SELECT 1 FROM dbo.factura WHERE idPedido=@IdPedido) THROW 50222,'El pedido ya tiene factura.',1;
   UPDATE i SET stockActual=i.stockActual+d.cantidad FROM dbo.inventarioProducto i
   JOIN dbo.detallePedido d ON d.idProducto=i.idProducto WHERE d.idPedido=@IdPedido AND d.controlInventario=1;
 END
 DECLARE @Cuenta int;
 SELECT @Cuenta=COUNT(*) FROM dbo.producto p WITH(UPDLOCK,HOLDLOCK)
 JOIN dbo.categoria c WITH(UPDLOCK,HOLDLOCK) ON c.idCategoria=p.idCategoria
 JOIN @Lineas l ON l.idProducto=p.idProducto WHERE p.disponible=1 AND c.activa=1 AND p.precio=l.precio;
 IF @Cuenta<>(SELECT COUNT(*) FROM @Lineas) THROW 50223,'Cambió el precio o la disponibilidad de un producto. Revise el pedido antes de enviarlo.',1;
 SELECT @Cuenta=COUNT(*) FROM dbo.inventarioProducto i WITH(UPDLOCK,HOLDLOCK) JOIN @Lineas l ON l.idProducto=i.idProducto;
 IF EXISTS(SELECT 1 FROM dbo.inventarioProducto i JOIN @Lineas l ON l.idProducto=i.idProducto WHERE i.activo=1 AND i.stockActual<l.cantidad)
   THROW 50224,'No hay existencias suficientes para completar el pedido.',1;
 UPDATE l SET control=1 FROM @Lineas l JOIN dbo.inventarioProducto i ON i.idProducto=l.idProducto WHERE i.activo=1;
 DECLARE @Total decimal(18,2)=(SELECT SUM(CONVERT(decimal(18,2),precio)*cantidad) FROM @Lineas);
 IF @Total<=0 OR @Total>99999999.99 THROW 50225,'El importe del pedido no es válido.',1;
 IF @IdPedido=0
 BEGIN
   INSERT dbo.pedido(idCliente,idEmpleado,estado,subtotal,total,esDelivery,costoEnvio,solicitud)
   VALUES(@Cliente,NULL,'Pendiente',@Total,@Total,0,0,@Solicitud);
   SET @IdPedido=CONVERT(int,SCOPE_IDENTITY());
 END
 ELSE
 BEGIN
   UPDATE dbo.pedido SET subtotal=@Total,total=@Total WHERE idPedido=@IdPedido;
   DELETE dbo.detallePedido WHERE idPedido=@IdPedido;
 END
 INSERT dbo.detallePedido(idPedido,idProducto,cantidad,precioUnitario,subtotal,total,controlInventario)
 SELECT @IdPedido,idProducto,cantidad,precio,precio*cantidad,precio*cantidad,control FROM @Lineas;
 UPDATE i SET stockActual=i.stockActual-l.cantidad FROM dbo.inventarioProducto i JOIN @Lineas l ON l.idProducto=i.idProducto WHERE l.control=1;
 COMMIT; SELECT @IdPedido;
 END TRY BEGIN CATCH IF @@TRANCOUNT>0 ROLLBACK; THROW; END CATCH
END;
GO
CREATE OR ALTER PROCEDURE dbo.sp_Cliente_Pedido_Cancelar @Actor int,@IdPedido int
AS
BEGIN
 SET XACT_ABORT ON;
 DECLARE @Cliente int; EXEC dbo.sp_Cliente_ExigirActivo @Actor,@Cliente OUTPUT;
 BEGIN TRY
 BEGIN TRANSACTION;
 IF NOT EXISTS(SELECT 1 FROM dbo.pedido WITH(UPDLOCK,HOLDLOCK) WHERE idPedido=@IdPedido AND idCliente=@Cliente AND estado='Pendiente')
   THROW 50221,'Solo puede cancelar sus pedidos pendientes.',1;
 IF EXISTS(SELECT 1 FROM dbo.factura WHERE idPedido=@IdPedido) THROW 50222,'El pedido ya tiene factura.',1;
 UPDATE i SET stockActual=i.stockActual+d.cantidad FROM dbo.inventarioProducto i
 JOIN dbo.detallePedido d ON d.idProducto=i.idProducto WHERE d.idPedido=@IdPedido AND d.controlInventario=1;
 UPDATE dbo.pedido SET estado='Cancelado' WHERE idPedido=@IdPedido;
 COMMIT;
 END TRY BEGIN CATCH IF @@TRANCOUNT>0 ROLLBACK; THROW; END CATCH
END;
GO
CREATE OR ALTER PROCEDURE dbo.sp_Cliente_Reservas @Actor int
AS
BEGIN
 DECLARE @Cliente int; EXEC dbo.sp_Cliente_ExigirActivo @Actor,@Cliente OUTPUT;
 SELECT r.idReservacion IdReserva,r.idMesa IdMesa,m.numero Mesa,r.fechaHoraReserva Fecha,
 r.cantidadPersonas Personas,r.estado Estado FROM dbo.reservacion r JOIN dbo.mesa m ON m.idMesa=r.idMesa
 WHERE r.idCliente=@Cliente ORDER BY r.fechaHoraReserva DESC;
END;
GO
CREATE OR ALTER PROCEDURE dbo.sp_Cliente_Reserva_Guardar @Actor int,@Id int=0,@IdMesa int,@Fecha datetime,@Personas int
AS
BEGIN
 SET NOCOUNT ON; SET XACT_ABORT ON;
 DECLARE @Cliente int;
 IF @Fecha<=GETDATE() OR @Personas NOT BETWEEN 1 AND 20 THROW 50226,'Seleccione una fecha futura y una cantidad válida.',1;
 BEGIN TRY
 BEGIN TRANSACTION;
 EXEC dbo.sp_Cliente_ExigirActivo @Actor,@Cliente OUTPUT;
 IF NOT EXISTS(SELECT 1 FROM dbo.mesa WITH(UPDLOCK,HOLDLOCK) WHERE idMesa=@IdMesa AND activa=1 AND capacidad>=@Personas AND estado IN('Disponible','Reservada'))
   THROW 50227,'La mesa no está disponible o no tiene la capacidad necesaria.',1;
 IF @Id<>0 AND NOT EXISTS(SELECT 1 FROM dbo.reservacion WHERE idReservacion=@Id AND idCliente=@Cliente AND estado IN('Pendiente','Confirmada') AND fechaHoraReserva>GETDATE())
   THROW 50228,'Solo puede modificar sus reservas futuras activas.',1;
 IF EXISTS(SELECT 1 FROM dbo.reservacion WITH(UPDLOCK,HOLDLOCK) WHERE idMesa=@IdMesa AND idReservacion<>@Id AND estado IN('Pendiente','Confirmada')
   AND fechaHoraReserva>DATEADD(hour,-2,@Fecha) AND fechaHoraReserva<DATEADD(hour,2,@Fecha))
   THROW 50229,'La mesa tiene otra reserva dentro del intervalo de dos horas.',1;
 IF @Id=0
 BEGIN
   INSERT dbo.reservacion(idCliente,idMesa,fechaHoraReserva,cantidadPersonas,estado) VALUES(@Cliente,@IdMesa,@Fecha,@Personas,'Pendiente');
   SET @Id=CONVERT(int,SCOPE_IDENTITY());
 END
 ELSE UPDATE dbo.reservacion SET idMesa=@IdMesa,fechaHoraReserva=@Fecha,cantidadPersonas=@Personas
 WHERE idReservacion=@Id AND idCliente=@Cliente;
 COMMIT; SELECT @Id;
 END TRY BEGIN CATCH IF @@TRANCOUNT>0 ROLLBACK; THROW; END CATCH
END;
GO
CREATE OR ALTER PROCEDURE dbo.sp_Cliente_Reserva_Cancelar @Actor int,@Id int
AS
BEGIN
 DECLARE @Cliente int; EXEC dbo.sp_Cliente_ExigirActivo @Actor,@Cliente OUTPUT;
 UPDATE dbo.reservacion SET estado='Cancelada' WHERE idReservacion=@Id AND idCliente=@Cliente
 AND estado IN('Pendiente','Confirmada') AND fechaHoraReserva>GETDATE();
 IF @@ROWCOUNT=0 THROW 50228,'Solo puede cancelar sus reservas futuras activas.',1;
END;
GO
CREATE OR ALTER PROCEDURE dbo.sp_Cliente_ObtenerMiPerfil @IdUsuario int
AS
BEGIN
 SET NOCOUNT ON;
 SELECT c.idCliente,c.PNombreCl,c.SNombreCl,c.PApellidoCl,c.SApellidoCl,
 c.genero,c.cedula,c.telefono,c.email
 FROM dbo.cliente c JOIN dbo.usuario u ON u.idUsuario=c.idUsuario
 WHERE c.idUsuario=@IdUsuario AND c.activo=1 AND u.idRol=3;
END;
GO
CREATE OR ALTER PROCEDURE dbo.sp_Producto_Disponibles
AS
BEGIN
 SET NOCOUNT ON;
 SELECT p.idProducto,p.idCategoria,p.idSubcategoria,c.nombre CategoriaNombre,s.nombre SubcategoriaNombre,
 p.nombre,p.precio,p.descripcion,p.codigo,p.disponible
 FROM dbo.producto p JOIN dbo.categoria c ON c.idCategoria=p.idCategoria
 LEFT JOIN dbo.subcategoria s ON s.idSubcategoria=p.idSubcategoria
 WHERE p.disponible=1 AND c.activa=1
 ORDER BY c.nombre,p.nombre;
END;
GO
