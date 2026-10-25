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
