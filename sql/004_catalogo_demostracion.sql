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
