# Catálogo para la defensa

Se aplicó `sql/004_catalogo_demostracion.sql` a la base configurada en la aplicación.
El catálogo queda con 6 categorías, 12 subcategorías y 24 productos, todos con
descripciones. Cada subcategoría tiene dos productos para facilitar la prueba.

| Categoría | Subcategorías |
|---|---|
| Entradas | Entradas calientes, Entradas frías |
| Platos Fuertes | Carnes, Aves |
| Bebidas | Refrescos naturales, Gaseosas |
| Postres | Pasteles, Helados |
| Sopas y Ensaladas | Sopas, Ensaladas |
| Comida Rapida | Hamburguesas, Pizzas |

Se conservaron los IDs, códigos, nombres y precios de los seis productos
originales. Se añadieron 18 productos con códigos MENU-007 a MENU-024 y precios
de demostración. No se modificaron usuarios, pedidos, pagos ni facturas.

El script trabaja en una transacción, comprueba conflictos y puede repetirse
sin duplicar los productos. Completa descripciones y subcategorías faltantes;
no sobrescribe precios ni descripciones ya editadas.

El respaldo de las tres tablas antes del cambio está en
`sql/respaldos/004_catalogo_antes_20261017_143242.json`. Es una instantánea de
datos del catálogo, no un respaldo completo de SQL Server. El archivo original
de Documents/RestaurantSM.sql se conserva como estaba; para recrear esta
versión se aplican las migraciones del repositorio y luego el script 004.

En Administración > Productos se puede ver la categoría y subcategoría.
Cliente > Mi pedido muestra los productos disponibles al volver a abrirlo.
La pantalla del cliente mantiene el filtro por categoría; todavía no incluye
un filtro independiente por subcategoría.

Verificación: segunda ejecución sin duplicados, cero productos sin subcategoría
o con categoría inconsistente y precios originales conservados.
