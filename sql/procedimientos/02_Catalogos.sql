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
