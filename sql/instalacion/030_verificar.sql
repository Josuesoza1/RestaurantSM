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
