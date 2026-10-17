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
