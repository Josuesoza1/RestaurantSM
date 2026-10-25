# Instalar o actualizar RestaurantSM

Los archivos son para SQL Server 2017 o posterior, incluida una instancia LocalDB compatible. Ejecútelos completos en SQL Server Management Studio, conectándose al mismo servidor que utilizará la aplicación. El nombre predeterminado de la base es `RestaurantSM`.

## Ya tengo la base

1. Conserve un respaldo de la base antes de actualizar.
2. Abra y ejecute **ACTUALIZAR_TODO_RESTAURANTSM.sql** completo. Prepara la versión 009 simplificada e incluye las actualizaciones necesarias; no ejecute los scripts anteriores 005 ni 008.
3. Compruebe que no aparezcan errores y que se muestre el mensaje final de versión 009. El instalador trabaja por bloques separados con `GO`: si falla uno, revise el primer error y repita el archivo completo antes de iniciar el programa. La instalación no es una única transacción global; las operaciones de pedidos y cobros sí tienen su propia transacción.

El actualizador conserva los registros. Agrega los campos de `pedido` que falten, incluidos `subtotal`, `total` y `solicitud`. Reconstruye los importes únicamente cuando agrega columnas faltantes. No cambia importes que ya estaban guardados. Las existencias actuales se conservan y el historial de movimientos comienza con los cambios posteriores a esta actualización.

Los roles deben corresponder a 1 Administrador, 2 Empleado y 3 Cliente. Si la base usa otros significados para esos identificadores, el script se detiene para evitar asignar permisos incorrectos.

## Es una instalación nueva

1. Ejecute **INSTALAR_NUEVA.sql**. Crea la base si no existe y construye tablas y procedimientos. Si ya hay tablas, omite la instalación y muestra que debe usar el actualizador.
2. Abra **CREAR_ADMINISTRADOR.sql** y escriba un nombre de usuario y una contraseña propia en las dos variables indicadas. Ejecútelo una vez. El script no modifica cuentas existentes. No guarde la contraseña real en Git.
3. Entre como administrador y registre categorías, productos, clientes, empleados y mesas según corresponda. Cree inventario activo con existencias para cada producto que vaya a vender.

No hay usuarios ni contraseñas de demostración preconfigurados en estos instaladores.

## Conectar la aplicación

En `SistemaPedidoRestaurante/App.config`, ajuste la entrada `RestauranteDB`. Puede usar el contenido de `App.config.ejemplo` si utiliza autenticación de Windows, cambiando el servidor cuando corresponda. El archivo de ejemplo no sustituye la configuración del servidor SQL.

Abra `RestaurantSM.slnx`, restaure NuGet y compile. El proyecto UI incluye QuestPDF 2026.9.1. Al copiar el programa compilado, incluya todos sus archivos de salida y subcarpetas, en particular `runtimes`; copiar solo el EXE no es suficiente.

## Recorrido de uso

1. **Administrador:** registre el catálogo y cargue inventario. En Inventario, use Ajustar existencias para entradas y salidas con motivo; Movimientos muestra quién hizo cada cambio.
2. **Cliente:** agregue productos y envíe el pedido. Puede editarlo o cancelarlo mientras siga pendiente. El stock se descuenta al registrarlo y se devuelve una sola vez al cancelarlo.
3. **Personal:** en Operaciones → Pedidos consulte el detalle, cree pedidos para clientes registrados y asigne una mesa cuando corresponda. Avance Pendiente → En preparación → Listo → Entregado.
4. **Cobro:** seleccione un pedido entregado. El importe viene del servidor. En efectivo se calcula el cambio; tarjeta y transferencia requieren el importe exacto. Confirme estos pagos después de verificarlos externamente. El cobro crea juntos el pago y la factura, y cierra el pedido como Pagado.
5. **PDF:** en Facturas consulte la venta y pulse PDF. La bandeja de pedidos también permite exportar. El cliente puede hacer clic derecho en su pedido para obtener el comprobante o su factura pagada. Un comprobante de pedido no acredita pago.
6. **Mesas:** el cobro del último pedido abierto deja la mesa En limpieza. Desde Mesas y limpieza, el personal confirma que está limpia para dejarla Disponible. No puede liberar mesas con pedidos sin cerrar.
7. **Reservas:** el personal puede registrar llegada desde 30 minutos antes de la hora reservada, ausencia desde 30 minutos después, o cancelar una reserva pendiente/confirmada. La llegada ocupa la mesa y conserva el registro de la reserva.

## Importes e impuesto

La tasa incluida comienza en **0 %**. El administrador puede cambiarla desde Configuración. Los precios del menú son finales: si se configura un 15 % incluido y un pedido cuesta 115, la factura desglosa base 100 e impuesto 15 y el cliente sigue pagando 115. Este ejemplo explica el cálculo; la tasa elegida debe corresponder a la definición del proyecto.

La tasa se aplica a los productos al cobrar. El envío, si existe en un registro anterior, se muestra separado. No se añade un segundo impuesto sobre el total. La configuración afecta a nuevas facturas; no recalcula las emitidas. En facturas anteriores cuya tasa se desconoce se muestra el impuesto registrado, sin inventar una tasa histórica.

Los cobros nuevos exigen que el subtotal coincida con los detalles y que el total corresponda a productos más envío. Si una venta antigua contiene importes inconsistentes, debe revisarse antes de cobrarla. No se borran ni se corrigen silenciosamente ventas anteriores.

## Comprobar sin tocar la base de trabajo

Compile en Debug y ejecute `tests/VerificarOperacion.ps1` desde Windows PowerShell. Usa una base temporal con datos ficticios, instala desde cero, repite el actualizador y comprueba pedidos, permisos, cobros, documentos, reservas, mesas y concurrencia. También simula la falta de subtotal y total. Requiere permisos para crear bases temporales. Las evidencias se guardan en la carpeta temporal indicada al finalizar.

La carpeta `procedimientos` contiene los mismos procedimientos organizados por tema para estudiarlos. No necesita ejecutarlos por separado para instalar: los dos archivos completos ya los incluyen. La carpeta `estructura` separa la creación original de tablas y la adaptación de un esquema anterior. Se conservan las tablas previstas para etapas futuras.

La aplicación ya no usa XML para enviar pedidos, contexto de sesión ni un trigger para registrar movimientos. Los movimientos se insertan explícitamente desde los procedimientos de inventario y pedidos. Los cambios hechos directamente por fuera de estos procedimientos no se auditan automáticamente. El control de cambios usa un contador entero `revision`; si la base anterior tiene una columna `rowversion`, se conserva sin utilizarla.

Consulte [Cómo entender el SQL](COMO_ENTENDER_SQL.md) para seguir los procedimientos y las dos protecciones adicionales que se conservaron.

Si modifica un archivo de `procedimientos` o `estructura`, ejecute `PrepararScripts.ps1` desde PowerShell para actualizar las dos copias completas de instalación. Este preparador solo reúne archivos; no se conecta ni ejecuta cambios en SQL Server. Los archivos completos que se entregan ya están preparados.
