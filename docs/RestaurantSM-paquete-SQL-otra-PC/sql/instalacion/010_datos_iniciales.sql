-- Datos de referencia y seis productos originales. Solo instalación nueva.
USE [RestaurantSM];
GO
SET XACT_ABORT ON;
BEGIN TRANSACTION;
SET IDENTITY_INSERT dbo.rol ON;
INSERT dbo.rol(idRol,nombre,descripcion) VALUES (1,N'Administrador',N'Permite modificar todo el sistema');
INSERT dbo.rol(idRol,nombre,descripcion) VALUES (2,N'Empleado',N'Funciones limitadas a lo basico');
INSERT dbo.rol(idRol,nombre,descripcion) VALUES (3,N'Cliente',N'Usuario común');
SET IDENTITY_INSERT dbo.rol OFF;
INSERT dbo.categoria(nombre,descripcion) VALUES (N'Entradas',N'Aperitivos y piqueos ligeros para comenzar');
INSERT dbo.categoria(nombre,descripcion) VALUES (N'Platos Fuertes',N'Especialidades de la casa, carnes, aves y mariscos');
INSERT dbo.categoria(nombre,descripcion) VALUES (N'Bebidas',N'Refrescos naturales, jugos, gaseosas y licores');
INSERT dbo.categoria(nombre,descripcion) VALUES (N'Postres',N'Pasteles, helados y postres tradicionales');
INSERT dbo.categoria(nombre,descripcion) VALUES (N'Sopas y Ensaladas',N'Opciones ligeras, caldos y ensaladas frescas');
INSERT dbo.categoria(nombre,descripcion) VALUES (N'Comida Rapida',N'Hamburguesas, pizzas, papas fritas y snacks');
INSERT dbo.producto(idCategoria,nombre,precio,descripcion,codigo,disponible) SELECT idCategoria,N'Dedos de Queso',120.00,N'Bastones de queso empanizados, acompañados de salsa de tomate.',N'PROD-001',1 FROM dbo.categoria WHERE nombre=N'Entradas';
INSERT dbo.producto(idCategoria,nombre,precio,descripcion,codigo,disponible) SELECT idCategoria,N'Lomito a la Plancha',320.50,N'Lomito de res a la plancha con arroz, vegetales y tostones.',N'PROD-002',1 FROM dbo.categoria WHERE nombre=N'Platos Fuertes';
INSERT dbo.producto(idCategoria,nombre,precio,descripcion,codigo,disponible) SELECT idCategoria,N'Jugo Natural de Naranja',45.00,N'Jugo de naranja recién preparado, servido frío en vaso.',N'PROD-003',1 FROM dbo.categoria WHERE nombre=N'Bebidas';
INSERT dbo.producto(idCategoria,nombre,precio,descripcion,codigo,disponible) SELECT idCategoria,N'Tres Leches',65.00,N'Porción de bizcocho bañado en tres leches con crema.',N'PROD-004',1 FROM dbo.categoria WHERE nombre=N'Postres';
INSERT dbo.producto(idCategoria,nombre,precio,descripcion,codigo,disponible) SELECT idCategoria,N'Sopa de Res',150.00,N'Caldo de res con verduras, acompañado de arroz y tortilla.',N'PROD-005',1 FROM dbo.categoria WHERE nombre=N'Sopas y Ensaladas';
INSERT dbo.producto(idCategoria,nombre,precio,descripcion,codigo,disponible) SELECT idCategoria,N'Hamburguesa con Tocino',210.00,N'Hamburguesa de res con tocino, queso y vegetales; incluye papas fritas.',N'PROD-006',1 FROM dbo.categoria WHERE nombre=N'Comida Rapida';
COMMIT TRANSACTION;
GO
