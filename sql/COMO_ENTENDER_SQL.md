# Cómo entender el SQL del proyecto

La versión 009 utiliza procedimientos con parámetros, variables, consultas, condiciones y transacciones. Los archivos completos de instalación contienen los mismos procedimientos que aparecen separados en esta carpeta. Tener varias operaciones y tablas hace que el archivo completo sea largo; para estudiar conviene abrir un tema a la vez.

## Organización

| Archivo en procedimientos | Contenido |
|---|---|
| 01_Acceso.sql | Inicio de sesión, comprobación de roles y configuración. |
| 02_Catalogos.sql | Categorías, productos, subcategorías y roles. |
| 03_Personas.sql | Clientes, empleados y mesas administrativas. |
| 04_Inventario.sql | Existencias, entradas, salidas e historial. |
| 05_Pedidos.sql | Cabecera, productos, total, estados y mesas de los pedidos. |
| 06_Reservas.sql | Reservas del cliente y atención por el personal. |
| 07_Cobros_y_facturas.sql | Cálculo de cobros, pagos y consulta de documentos. |

Los procedimientos para comprobar permisos se comparten entre las gestiones. Por ejemplo, `sp_Catalogo_ExigirAdministrador` verifica que el usuario tenga el rol necesario antes de cambiar el catálogo. No representan tablas nuevas.

## Guardar un pedido

1. El formulario lee los controles y llama a la BLL. La BLL valida los datos y permisos.
2. `PedidoDAL.Guardar` abre una conexión y una transacción de SQL Server.
3. `sp_Pedido_Iniciar` valida al usuario y crea la cabecera. Si es una edición, devuelve las unidades anteriores y retira el detalle anterior.
4. Un `foreach` de C# llama a `sp_Pedido_AgregarLinea` por cada producto. Recibe identificador, cantidad y precio como parámetros normales. Valida disponibilidad, descuenta existencias y guarda el detalle.
5. `sp_Pedido_Finalizar` comprueba las líneas y calcula subtotal y total con `SUM`.
6. La DAL hace `Commit` si todo termina bien. Si falla cualquier producto, hace `Rollback`: no quedan pedidos a medias ni descuentos parciales de stock.

Los tres procedimientos requieren la misma transacción. No se deben ejecutar como operaciones independientes sin una transacción que los agrupe. El código no envía XML ni usa un tipo de tabla personalizado.

## Cobrar

`sp_Cobro_Registrar` tiene pasos comentados: verificar que la solicitud no esté registrada, obtener el importe del pedido entregado, comprobar el pago, calcular el impuesto incluido y guardar factura, productos y pago. Utiliza `BEGIN TRY`, `BEGIN TRANSACTION`, `COMMIT TRANSACTION`, `BEGIN CATCH` y `ROLLBACK TRANSACTION`. Un error revierte el conjunto de cambios.

La factura guarda una copia de los datos de esa venta. Así, editar después el nombre o precio de un producto no cambia las facturas ya emitidas.

## Inventario

`sp_Inventario_Guardar` crea o modifica las existencias. `sp_Inventario_Ajustar` recibe entrada o salida, cantidad y motivo. `sp_Inventario_RegistrarMovimiento` registra explícitamente el antes, el después, el usuario y el motivo dentro de la misma transacción.

`revision` es un número entero que comienza en 1 y aumenta con cada cambio. Si una pantalla cargó la revisión 3 pero el registro ya está en 4, su actualización se rechaza para que recargue los datos. Reemplaza el uso de un arreglo de bytes y del tipo `rowversion`.

## Protecciones adicionales que se conservan

Estos dos conceptos pueden ir más allá del último tema de Base de Datos I que mencionaste. Se mantienen para conservar el funcionamiento del programa y están separados de las consultas ordinarias:

- **Aislamiento SERIALIZABLE:** la transacción mantiene protegidos los datos consultados hasta terminar. Ayuda a evitar que dos personas vendan la misma última unidad o cobren el mismo pedido. Sustituye las pistas manuales `UPDLOCK` y `HOLDLOCK` que había en varias consultas. Si hay un conflicto, se informa que debe recargar e intentar otra vez.
- **Identificador de solicitud:** C# genera un `Guid` para cada envío o cobro. Si se repite la misma solicitud, se comprueban sus datos y se devuelve el resultado existente. Sirve para evitar duplicados ante reintentos; no sustituye al identificador numérico del pedido o la factura.

El contador entero de inventario también requiere entender por qué pueden cambiar los datos entre consultar y guardar, aunque su implementación usa solamente comparaciones y sumas.

## Código de instalación

`GO` separa los bloques que ejecuta SQL Server Management Studio. `CREATE OR ALTER PROCEDURE` permite crear un procedimiento o actualizar su definición al repetir el archivo.

La adaptación de una base anterior comprueba si faltan columnas y contiene dos instrucciones dinámicas breves para reconstruir subtotal y total después de agregarlos. Estas instrucciones están únicamente en la compatibilidad del esquema; los procedimientos usados por el programa no están envueltos en cadenas `EXEC(N'...')` ni necesitan duplicar todas sus comillas.

El instalador nuevo utiliza `NOEXEC` para omitir sus bloques cuando ya existen tablas. Los archivos de instalación no cambian la conexión del programa: esa configuración sigue en `App.config`.
