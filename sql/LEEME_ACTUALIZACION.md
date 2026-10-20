# Actualizar la base existente en otra PC

Usar `ACTUALIZAR_TODO_RESTAURANTSM.sql` completo. Es autocontenido: no requiere ejecutar antes los archivos 001, 002, 003, 004 ni 005. Está preparado para la base original `RestaurantSM` que ya tiene el compañero, o una versión parcialmente actualizada de ella, con los roles 1 Administrador, 2 Empleado y 3 Cliente. No instala una base desde cero.

## Pasos

1. Hacer commit y push de los cambios del proyecto, incluyendo este archivo SQL y esta guía. Después, en la otra PC, hacer pull. Los cambios locales sin subir todavía no llegan mediante pull.
2. En SQL Server Management Studio, conectarse a la instancia donde está la base anterior `RestaurantSM` y hacer una copia de seguridad de esa base.
3. Abrir `sql/ACTUALIZAR_TODO_RESTAURANTSM.sql`. Activar **Consulta > Modo SQLCMD**. La primera línea `:On Error exit` detiene el archivo si encuentra un error.
4. Ejecutar TODO el archivo. Comprobar que aparezca **RestaurantSM actualizada correctamente** y que no haya mensajes de error.
5. Configurar `SistemaPedidoRestaurante/App.config` con el servidor de esa PC. Mantener el nombre de la conexión `RestauranteDB` y la base `RestaurantSM`. Hay una plantilla sin contraseñas en `App.config.ejemplo`. Usar LocalDB solo si allí está la base; si está en SQL Express u otra instancia, indicar esa instancia.
6. Abrir `RestaurantSM.slnx`, compilar e iniciar el proyecto de interfaz. Probar una cuenta existente y consultar el catálogo.

El equipo necesita SQL Server 2016 SP1 o posterior (por `CREATE OR ALTER PROCEDURE`), y Visual Studio compatible con la solución y las herramientas de escritorio .NET Framework 4.8. Conectarse desde SSMS a una instancia distinta de la indicada en App.config haría que el programa consulte otra base.

## Qué incluye y qué conserva

Instala o actualiza 35 procedimientos almacenados, el registro con rol Cliente, las gestiones de empleados, categorías, productos, clientes, roles y mesas; el inventario por producto terminado; los pedidos, historial, perfil y reservas del cliente. Agrega columnas, índices y la tabla de inventario necesarios.

Incluye el catálogo al 19 de octubre de 2026: 7 categorías, 12 subcategorías y 24 productos. Busca categorías y subcategorías por nombre, y productos por código; agrega faltantes y completa descripciones o subcategorías vacías. Conserva precios y disponibilidad de productos existentes. También conserva las categorías y productos propios de la otra PC. Por ello, el total puede ser mayor.

No copia cuentas ni contraseñas de esta PC, ni nuestros clientes, empleados, pedidos, reservas, pagos, facturas o cantidades de inventario. Conserva los registros que ya tenga la otra PC. Las existencias se configuran desde Inventario según corresponda: no se inventan cantidades. El script actualiza la estructura y las funciones del programa; no clona todos los datos personales o las operaciones de nuestra base.

Puede repetirse sin duplicar este catálogo. Los cambios se realizan por bloques, no en una única transacción global: si aparece un error, los bloques anteriores pueden haber quedado aplicados. Corregir la causa y repetir el archivo completo; no borrar la base. Si informa códigos o nombres de catálogo incompatibles, revisar esos registros antes de continuar.

## Comprobación realizada

`tests/VerificarActualizacionCompleta.ps1` reconstruye el esquema original en una base temporal aislada y ejecuta el archivo dos veces. Pasó 52 comprobaciones: catálogo sin duplicados, conservación de datos previos y precios propios, consultas de pantallas, login, registro con rol Cliente, alta de mesa y envío/cancelación de pedido con descuento/devolución de inventario. La base temporal se elimina al finalizar. No se ejecutó esta actualización sobre la base de trabajo ni sobre el equipo del compañero.

Los cobros y la facturación de las pantallas internas continúan pendientes; el archivo no implementa esas funciones en la interfaz.
