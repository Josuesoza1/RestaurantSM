# RestaurantSM

Sistema académico de restaurante en C# Windows Forms, .NET Framework 4.8 y SQL Server. La solución separa UI, BLL, DAL, entidades y validaciones. Las operaciones usan procedimientos almacenados y las búsquedas de listados utilizan LINQ.

## Abrir el proyecto

1. Abra RestaurantSM.slnx en Visual Studio con las herramientas de escritorio .NET y el paquete de destino .NET Framework 4.8.
2. Configure la conexión RestauranteDB de SistemaPedidoRestaurante/App.config para una base RestaurantSM preparada con el esquema y los procedimientos de la versión 9.
3. Restaure NuGet y recompile la solución. QuestPDF está incluido como dependencia del proyecto UI.
4. Abra el diseñador del formulario que vaya a editar. Los formularios operativos por rol heredan sus controles de UICompartida; es necesario compilar las bases antes de abrir los derivados.

El archivo sql/010_descripciones_roles.sql actualiza las descripciones de los tres roles existentes. No crea la base ni sustituye los scripts completos de instalación del esquema.

## Formularios y permisos

Consulte [Formularios y roles](FORMULARIOS_Y_ROLES.md) para identificar cada pantalla, sus eventos y el recorrido de navegación.

- UIAdministracion contiene las gestiones y las pantallas de supervisión del administrador.
- UIEmpleado contiene las pantallas operativas propias del empleado: pedidos, reservas, mesas y limpieza, cobros y facturas.
- UICliente contiene los pedidos, reservas, perfil y documentos propios del cliente.
- UICompartida conserva los diseños y eventos comunes, y los diálogos de selección o confirmación.
- UIPrincipal contiene los ajustes de presentación, menús y otras ayudas de interfaz.

Los roles conservan sus identificadores: 1 Administrador, 2 Empleado y 3 Cliente. Las pantallas comprueban su rol; BLL y SQL mantienen los controles de permisos. La separación visual no sustituye esas comprobaciones.

## Operaciones

El administrador prepara catálogo e inventario. El personal y el cliente pueden registrar pedidos con varios productos según sus permisos. Un producto sin inventario activo o sin unidades no se ofrece para pedidos.

El personal atiende Pendiente → En preparación → Listo → Entregado → Pagado. Cobrar guarda pago y factura juntos. Los comprobantes de pedido y las facturas pagadas se exportan con QuestPDF. Al trasladar el ejecutable, conserve todos sus archivos de salida y subcarpetas, incluida runtimes.

Los pagos pueden registrarse como efectivo, tarjeta o transferencia. La comprobación bancaria de tarjeta y transferencia es manual. La configuración permite indicar el impuesto incluido en los precios finales.

Clientes, Mesas e Inventario utilizan los modos Neutral, Agregar, Editar y Eliminar, con confirmación y cancelación. Se conserva la adaptación de la interfaz al maximizar.

## Validación del último bloque

La regresión con datos comprobó pedidos, inventario, cancelaciones, mesas, reservas, permisos, cobros simultáneos y PDF en bases temporales. Las comprobaciones de interfaz revisaron controles, eventos, menús, modos y el acceso directo a formularios de otros roles.

El checkout actual no incluye los verificadores ni el instalador completo que se usaron en entregas anteriores. La base debe estar preparada antes de ejecutar la aplicación; el archivo de descripciones de rol no realiza esa instalación.
