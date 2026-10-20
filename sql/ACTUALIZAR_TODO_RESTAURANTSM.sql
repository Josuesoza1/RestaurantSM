:On Error exit
-- RestaurantSM actualización completa al 19 de octubre de 2026.
-- Para la base EXISTENTE del proyecto. No crea ni elimina bases de datos.
-- Hacer un respaldo antes. SSMS: activar Consulta > Modo SQLCMD.
-- Ejecutar TODO el archivo. No necesita ejecutar 001 a 005 por separado.
-- Conserva cuentas, clientes, empleados, pedidos, reservas, pagos y facturas.
-- El catálogo agrega faltantes y completa vacíos; no sobrescribe precios existentes.
USE [master];
GO
IF DB_ID(N'RestaurantSM') IS NULL
 THROW 50300,'No existe RestaurantSM en esta instancia. Conéctese a la instancia con su base anterior.',1;
GO
USE [RestaurantSM];
GO
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
SET ANSI_PADDING ON;
SET XACT_ABORT ON;
GO
IF EXISTS(SELECT 1 FROM (VALUES('rol'),('usuario'),('cliente'),('empleado'),('categoria'),('subcategoria'),('producto'),
 ('mesa'),('pedido'),('detallePedido'),('reservacion'),('factura'),('pago')) t(nombre) WHERE OBJECT_ID('dbo.'+t.nombre,'U') IS NULL)
 THROW 50301,'La base no tiene el esquema original de RestaurantSM. Revise antes de continuar.',1;
IF EXISTS(SELECT 1 FROM (VALUES(1,'Administrador'),(2,'Empleado'),(3,'Cliente')) r(id,nombre)
 WHERE NOT EXISTS(SELECT 1 FROM dbo.rol WHERE idRol=r.id AND nombre=r.nombre))
 THROW 50302,'Los roles deben ser 1 Administrador, 2 Empleado y 3 Cliente.',1;
IF COL_LENGTH('dbo.categoria','activa') IS NULL
 ALTER TABLE dbo.categoria ADD activa bit NOT NULL CONSTRAINT DF_categoria_activa DEFAULT(1) WITH VALUES;
GO

-- Esquema y procedimientos de las gestiones nuevas
-- Las migraciones anteriores necesarias ya están integradas en este archivo.
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

GO

SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
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

SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
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

SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
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

SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
GO

CREATE OR ALTER PROCEDURE dbo.sp_Categoria_Listar
AS
BEGIN
    SET NOCOUNT ON;
    SELECT idCategoria,nombre,descripcion,activa FROM dbo.categoria ORDER BY nombre;
END;

GO

SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
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

SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
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

SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
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

SET ANSI_NULLS ON;
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

SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
GO

CREATE OR ALTER PROCEDURE [dbo].[sp_InsertarCliente]
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

SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
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

SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
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

SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
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

SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
GO

CREATE OR ALTER PROCEDURE sp_Rol_Listar
AS
BEGIN
    SET NOCOUNT ON;
    SELECT idRol, nombre, descripcion FROM rol;
END

GO

SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
GO

CREATE OR ALTER PROCEDURE dbo.sp_Subcategoria_Listar
AS
BEGIN
    SET NOCOUNT ON;
    SELECT idSubcategoria,idCategoria,nombre FROM dbo.subcategoria ORDER BY nombre;
END;

GO

SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
GO

CREATE OR ALTER PROCEDURE sp_Usuario_Listar
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT u.idUsuario, u.nombreUsuario, u.idRol, r.nombre AS NombreRol
    FROM usuario u
    INNER JOIN rol r ON u.idRol = r.idRol;
END

GO

CREATE OR ALTER PROCEDURE dbo.sp_Pedido_ListarAbiertos
AS
BEGIN
 SET NOCOUNT ON;
 SELECT idPedido,idCliente,idMesa,idEmpleado,idRepartidor,direccion,referencia,telefonoDeContacto,
 fechaHora,estado,esDelivery,costoEnvio,subtotal,total FROM dbo.pedido
 WHERE estado NOT IN('Cancelado','Entregado') ORDER BY fechaHora,idPedido;
END;
GO

-- Catálogo compartido. Resuelve IDs por nombre de categoría/subcategoría y código.
-- No copia cuentas personales ni stock de otra PC.
SET XACT_ABORT ON;
BEGIN TRY
BEGIN TRANSACTION;
DECLARE @Categorias TABLE(nombre nvarchar(60) PRIMARY KEY,descripcion nvarchar(200),activa bit);
DECLARE @Subcategorias TABLE(categoria nvarchar(60),nombre nvarchar(60));
DECLARE @Productos TABLE(codigo nvarchar(20) PRIMARY KEY,nombre nvarchar(100),descripcion nvarchar(250),precio decimal(10,2),disponible bit,categoria nvarchar(60),subcategoria nvarchar(60));
INSERT @Categorias VALUES(N'Bebidas',N'Refrescos naturales, jugos, gaseosas y licores',1);
INSERT @Categorias VALUES(N'Comida Rapida',N'Hamburguesas, pizzas, papas fritas y snacks',1);
INSERT @Categorias VALUES(N'Desayunos',N'Gallo pinto, huevos al gusto, tostadas y café para comenzar el día.',1);
INSERT @Categorias VALUES(N'Entradas',N'Aperitivos y piqueos ligeros para comenzar',1);
INSERT @Categorias VALUES(N'Platos Fuertes',N'Especialidades de la casa, carnes, aves y mariscos',1);
INSERT @Categorias VALUES(N'Postres',N'Pasteles, helados y postres tradicionales',1);
INSERT @Categorias VALUES(N'Sopas y Ensaladas',N'Opciones ligeras, caldos y ensaladas frescas',1);
INSERT @Subcategorias VALUES(N'Bebidas',N'Gaseosas');
INSERT @Subcategorias VALUES(N'Bebidas',N'Refrescos naturales');
INSERT @Subcategorias VALUES(N'Comida Rapida',N'Hamburguesas');
INSERT @Subcategorias VALUES(N'Comida Rapida',N'Pizzas');
INSERT @Subcategorias VALUES(N'Entradas',N'Entradas calientes');
INSERT @Subcategorias VALUES(N'Entradas',N'Entradas frías');
INSERT @Subcategorias VALUES(N'Platos Fuertes',N'Aves');
INSERT @Subcategorias VALUES(N'Platos Fuertes',N'Carnes');
INSERT @Subcategorias VALUES(N'Postres',N'Helados');
INSERT @Subcategorias VALUES(N'Postres',N'Pasteles');
INSERT @Subcategorias VALUES(N'Sopas y Ensaladas',N'Ensaladas');
INSERT @Subcategorias VALUES(N'Sopas y Ensaladas',N'Sopas');
INSERT @Productos VALUES(N'MENU-007',N'Tostones con queso',N'Plátano verde crujiente con queso rallado y ensalada de repollo.',95.0,1,N'Entradas',N'Entradas calientes');
INSERT @Productos VALUES(N'MENU-008',N'Ceviche de pescado',N'Pescado marinado con limón, cebolla y cilantro; acompañado de tostadas.',180.0,1,N'Entradas',N'Entradas frías');
INSERT @Productos VALUES(N'MENU-009',N'Bruschettas de tomate',N'Pan tostado con tomate fresco, albahaca y aceite de oliva.',85.0,1,N'Entradas',N'Entradas frías');
INSERT @Productos VALUES(N'MENU-010',N'Churrasco de res',N'Corte de res a la parrilla con chimichurri, arroz y ensalada.',295.0,1,N'Platos Fuertes',N'Carnes');
INSERT @Productos VALUES(N'MENU-011',N'Pollo a la plancha',N'Pechuga de pollo con vegetales salteados y arroz.',220.0,1,N'Platos Fuertes',N'Aves');
INSERT @Productos VALUES(N'MENU-012',N'Pollo en salsa de hongos',N'Pechuga de pollo en salsa cremosa de hongos con puré de papa.',245.0,1,N'Platos Fuertes',N'Aves');
INSERT @Productos VALUES(N'MENU-013',N'Refresco de maracuyá',N'Refresco natural de maracuyá preparado al momento.',40.0,1,N'Bebidas',N'Refrescos naturales');
INSERT @Productos VALUES(N'MENU-014',N'Gaseosa de cola',N'Gaseosa de cola personal de 355 ml.',35.0,1,N'Bebidas',N'Gaseosas');
INSERT @Productos VALUES(N'MENU-015',N'Gaseosa de limón',N'Gaseosa de limón personal de 355 ml.',35.0,1,N'Bebidas',N'Gaseosas');
INSERT @Productos VALUES(N'MENU-016',N'Pastel de chocolate',N'Porción de pastel de chocolate con cobertura cremosa.',75.0,1,N'Postres',N'Pasteles');
INSERT @Productos VALUES(N'MENU-017',N'Helado de vainilla',N'Dos bolas de helado de vainilla con salsa de chocolate.',55.0,1,N'Postres',N'Helados');
INSERT @Productos VALUES(N'MENU-018',N'Helado de fresa',N'Dos bolas de helado de fresa con trozos de fruta.',55.0,1,N'Postres',N'Helados');
INSERT @Productos VALUES(N'MENU-019',N'Sopa de pollo',N'Caldo de pollo con verduras frescas, arroz y tortilla.',130.0,1,N'Sopas y Ensaladas',N'Sopas');
INSERT @Productos VALUES(N'MENU-020',N'Ensalada de la casa',N'Lechuga, tomate, pepino y zanahoria con aderezo de la casa.',110.0,1,N'Sopas y Ensaladas',N'Ensaladas');
INSERT @Productos VALUES(N'MENU-021',N'Ensalada con pollo',N'Vegetales frescos con pechuga a la plancha y aderezo de yogur.',165.0,1,N'Sopas y Ensaladas',N'Ensaladas');
INSERT @Productos VALUES(N'MENU-022',N'Hamburguesa clásica',N'Hamburguesa de res con queso, lechuga y tomate; incluye papas fritas.',175.0,1,N'Comida Rapida',N'Hamburguesas');
INSERT @Productos VALUES(N'MENU-023',N'Pizza personal margarita',N'Pizza personal con salsa de tomate, mozzarella y albahaca.',160.0,1,N'Comida Rapida',N'Pizzas');
INSERT @Productos VALUES(N'MENU-024',N'Pizza personal de pepperoni',N'Pizza personal con mozzarella y pepperoni.',185.0,1,N'Comida Rapida',N'Pizzas');
INSERT @Productos VALUES(N'PROD-001',N'Dedos de Queso',N'Bastones de queso empanizados, acompañados de salsa de tomate.',120.0,1,N'Entradas',N'Entradas calientes');
INSERT @Productos VALUES(N'PROD-002',N'Lomito a la Plancha',N'Lomito de res a la plancha con arroz, vegetales y tostones.',320.5,1,N'Platos Fuertes',N'Carnes');
INSERT @Productos VALUES(N'PROD-003',N'Jugo Natural de Naranja',N'Jugo de naranja recién preparado, servido frío en vaso.',45.0,1,N'Bebidas',N'Refrescos naturales');
INSERT @Productos VALUES(N'PROD-004',N'Tres Leches',N'Porción de bizcocho bañado en tres leches con crema.',65.0,1,N'Postres',N'Pasteles');
INSERT @Productos VALUES(N'PROD-005',N'Sopa de Res',N'Caldo de res con verduras, acompañado de arroz y tortilla.',150.0,1,N'Sopas y Ensaladas',N'Sopas');
INSERT @Productos VALUES(N'PROD-006',N'Hamburguesa con Tocino',N'Hamburguesa de res con tocino, queso y vegetales; incluye papas fritas.',210.0,1,N'Comida Rapida',N'Hamburguesas');

IF EXISTS(SELECT 1 FROM @Productos m JOIN dbo.producto p ON p.codigo=m.codigo JOIN dbo.categoria c ON c.idCategoria=p.idCategoria
 WHERE p.nombre<>m.nombre OR c.nombre<>m.categoria)
 THROW 50303,'Un código del catálogo pertenece a otro nombre o categoría. Revise el conflicto sin borrar datos.',1;
IF EXISTS(SELECT 1 FROM @Productos m JOIN dbo.producto p ON p.nombre=m.nombre WHERE p.codigo<>m.codigo)
 THROW 50304,'Un producto del catálogo ya existe con otro código. Revise el conflicto.',1;
IF EXISTS(SELECT 1 FROM dbo.subcategoria GROUP BY idCategoria,nombre HAVING COUNT(*)>1)
 THROW 50305,'Hay subcategorías duplicadas en una categoría. Revise sus relaciones.',1;
INSERT dbo.categoria(nombre,descripcion,activa)
 SELECT m.nombre,m.descripcion,m.activa FROM @Categorias m WHERE NOT EXISTS(SELECT 1 FROM dbo.categoria c WITH(UPDLOCK,HOLDLOCK) WHERE c.nombre=m.nombre);
UPDATE c SET descripcion=m.descripcion FROM dbo.categoria c JOIN @Categorias m ON m.nombre=c.nombre
 WHERE NULLIF(LTRIM(RTRIM(c.descripcion)),'') IS NULL;
INSERT dbo.subcategoria(idCategoria,nombre)
 SELECT c.idCategoria,m.nombre FROM @Subcategorias m JOIN dbo.categoria c ON c.nombre=m.categoria
 WHERE NOT EXISTS(SELECT 1 FROM dbo.subcategoria s WITH(UPDLOCK,HOLDLOCK) WHERE s.idCategoria=c.idCategoria AND s.nombre=m.nombre);
UPDATE p SET descripcion=CASE WHEN NULLIF(LTRIM(RTRIM(p.descripcion)),'') IS NULL THEN m.descripcion ELSE p.descripcion END,
 idSubcategoria=COALESCE(p.idSubcategoria,s.idSubcategoria)
 FROM dbo.producto p JOIN @Productos m ON m.codigo=p.codigo JOIN dbo.categoria c ON c.nombre=m.categoria
 LEFT JOIN dbo.subcategoria s ON s.idCategoria=c.idCategoria AND s.nombre=m.subcategoria;
INSERT dbo.producto(idCategoria,idSubcategoria,codigo,nombre,descripcion,precio,disponible)
 SELECT c.idCategoria,s.idSubcategoria,m.codigo,m.nombre,m.descripcion,m.precio,
 CASE WHEN c.activa=1 THEN m.disponible ELSE 0 END
 FROM @Productos m JOIN dbo.categoria c ON c.nombre=m.categoria
 LEFT JOIN dbo.subcategoria s ON s.idCategoria=c.idCategoria AND s.nombre=m.subcategoria
 WHERE NOT EXISTS(SELECT 1 FROM dbo.producto p WITH(UPDLOCK,HOLDLOCK) WHERE p.codigo=m.codigo);
IF EXISTS(SELECT 1 FROM dbo.producto p JOIN dbo.subcategoria s ON s.idSubcategoria=p.idSubcategoria WHERE s.idCategoria<>p.idCategoria)
 THROW 50306,'Hay productos relacionados con subcategorías de otra categoría.',1;
COMMIT;
END TRY BEGIN CATCH IF @@TRANCOUNT>0 ROLLBACK; THROW; END CATCH;
GO
-- Verificación sin mostrar cuentas, claves o datos personales.
IF EXISTS(SELECT 1 FROM (VALUES (N'sp_Catalogo_ExigirAdministrador'),(N'sp_Categoria_Desactivar'),(N'sp_Categoria_Guardar'),(N'sp_Categoria_Listar'),(N'sp_Cliente_Detalle'),(N'sp_Cliente_ExigirActivo'),(N'sp_Cliente_Guardar'),(N'sp_Cliente_ObtenerMiPerfil'),(N'sp_Cliente_Pedido_Cancelar'),(N'sp_Cliente_Pedido_Guardar'),(N'sp_Cliente_Pedidos'),(N'sp_Cliente_Reserva_Cancelar'),(N'sp_Cliente_Reserva_Guardar'),(N'sp_Cliente_Reservas'),(N'sp_Empleado_Actualizar'),(N'sp_Empleado_Desactivar'),(N'sp_Empleado_Insertar'),(N'sp_Empleado_Listar'),(N'sp_Gestion_Clientes'),(N'sp_InsertarCliente'),(N'sp_Inventario_Guardar'),(N'sp_Inventario_Listar'),(N'sp_Mesa_Guardar'),(N'sp_Mesa_Listar'),(N'sp_Pedido_ListarAbiertos'),(N'sp_Producto_Desactivar'),(N'sp_Producto_Disponibles'),(N'sp_Producto_Guardar'),(N'sp_Producto_Listar'),(N'sp_Rol_Eliminar'),(N'sp_Rol_Guardar'),(N'sp_Rol_Listar'),(N'sp_Subcategoria_Listar'),(N'sp_Usuario_Listar'),(N'sp_Usuario_ValidarLogin')) p(nombre) WHERE OBJECT_ID('dbo.'+p.nombre,'P') IS NULL) THROW 50307,'Faltan procedimientos requeridos.',1;

IF COL_LENGTH('dbo.categoria','activa') IS NULL OR COL_LENGTH('dbo.cliente','activo') IS NULL
 OR COL_LENGTH('dbo.mesa','activa') IS NULL OR COL_LENGTH('dbo.pedido','solicitud') IS NULL
 OR COL_LENGTH('dbo.detallePedido','controlInventario') IS NULL OR OBJECT_ID('dbo.inventarioProducto','U') IS NULL
 THROW 50308,'La actualización de estructura está incompleta.',1;
SELECT 'RestaurantSM actualizada correctamente' Resultado;
SELECT (SELECT COUNT(*) FROM dbo.categoria) Categorias,(SELECT COUNT(*) FROM dbo.subcategoria) Subcategorias,
 (SELECT COUNT(*) FROM dbo.producto) Productos,(SELECT COUNT(*) FROM dbo.inventarioProducto) RegistrosInventario;
GO
