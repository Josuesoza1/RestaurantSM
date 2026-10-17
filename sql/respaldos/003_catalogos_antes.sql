USE [RestaurantSM];
GO
SET ANSI_NULLS ON;
GO
SET QUOTED_IDENTIFIER ON;
GO

CREATE OR ALTER PROCEDURE sp_Categoria_Listar
AS
BEGIN
    SET NOCOUNT ON;
    SELECT idCategoria, nombre, descripcion FROM categoria;
END

GO
DROP PROCEDURE IF EXISTS dbo.sp_Categoria_Guardar;
GO
DROP PROCEDURE IF EXISTS dbo.sp_Categoria_Desactivar;
GO
CREATE OR ALTER PROCEDURE [dbo].[sp_Producto_Listar]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT p.idProducto, p.idCategoria, p.idSubcategoria, c.nombre AS CategoriaNombre, 
           p.nombre, p.precio, p.descripcion, p.codigo, p.disponible
    FROM producto p
    INNER JOIN categoria c ON p.idCategoria = c.idCategoria;
END

GO
DROP PROCEDURE IF EXISTS dbo.sp_Producto_Guardar;
GO
DROP PROCEDURE IF EXISTS dbo.sp_Producto_Desactivar;
GO
DROP PROCEDURE IF EXISTS dbo.sp_Subcategoria_Listar;
GO
DROP PROCEDURE IF EXISTS dbo.sp_Catalogo_ExigirAdministrador;
GO
ALTER TABLE dbo.categoria DROP CONSTRAINT DF_categoria_activa;
ALTER TABLE dbo.categoria DROP COLUMN activa;
GO
