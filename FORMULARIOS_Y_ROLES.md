# Formularios y roles

## Acceso de cada rol

| Rol | Pantallas propias | Acciones |
|---|---|---|
| 1 Administrador | UIAdministracion | Gestiona catálogos, clientes, empleados, usuarios, mesas, inventario y configuración. Supervisa pedidos, reservas, cobros y facturas. |
| 2 Empleado | UIEmpleado | Registra pedidos para clientes activos, atiende pedidos y reservas, asigna mesas, confirma limpieza, cobra y consulta facturas. |
| 3 Cliente | UICliente | Gestiona sus propios pedidos, reservas, perfil y documentos. |

Los nombres e identificadores básicos están definidos en RolesSistema. No se renumeraron roles ni se reasignaron usuarios. El script sql/010_descripciones_roles.sql actualiza únicamente sus descripciones y puede repetirse.

El menú no es el único control de acceso: cada pantalla operativa comprueba el rol que le corresponde al abrirse. Las BLL conservan sus controles de permisos y SQL sigue comprobando la cuenta y las reglas de cada operación.

## Pantallas del empleado

| Formulario | Para qué sirve |
|---|---|
| FrmMenuEmpleado | Menú de trabajo del empleado. |
| FrmPedidosEmpleado | Bandeja para consultar detalle, avanzar estados, cancelar pendientes, asignar mesa y abrir cobros o documentos. |
| FrmPedido | Pedido en el local con varios productos y un cliente seleccionado. |
| FrmReservasEmpleado | Llegada, ausencia y cancelación de reservas activas. |
| FrmMesasEmpleado | Estado de mesas e inicio o confirmación de limpieza. |
| FrmPago | Confirmación del cobro de un pedido entregado. |
| FrmFacturas | Consulta y PDF de facturas pagadas. |
| FrmConsultaFacturasEmpleado | Selección de una factura para consultarla. |

El administrador tiene formularios equivalentes con sufijo Administracion y un formulario adicional de movimientos de inventario. Sus aperturas auxiliares también conservan el rol administrativo.

FrmPedidoCliente queda reservado al cliente autenticado. El personal ya no lo utiliza para registrar pedidos para otras personas.

## Para qué está UICompartida

Contiene los diseños y eventos que son iguales en las pantallas operativas, además de diálogos auxiliares:

- FrmBandejaOperaciones: listado, detalle, búsqueda y acciones.
- FrmPedidoLocalBase: carrito de productos del pedido en el local.
- FrmPagoBase y FrmFacturasBase: controles y eventos comunes de cobro y consulta.
- FrmSeleccionCliente, FrmElegirMesa y FrmConfirmarCobro: selección o confirmación de un dato.

Por ejemplo, FrmPago hereda de FrmPagoBase. La primera establece que el acceso es del empleado; la segunda contiene los botones, campos y eventos del cobro. Así cada rol tiene su formulario sin copiar los cálculos ni la misma lógica de botones.

Los archivos de UICompartida tienen su Designer y recursos. Para modificar un control o su evento común, abra el formulario base en el diseñador. Los formularios por rol muestran esos controles heredados. Después de cambios en una base, recompile antes de volver a abrir sus formularios derivados.

PantallasPersonal elige la pantalla propia del rol cuando se abre un pedido, una bandeja, un cobro o una factura desde otra operación. Rechaza el acceso del cliente a estas rutas del personal.

## Menús y modos de gestión

Clientes, Mesas e Inventario usan el mismo ModoFormulario que las gestiones que ya lo tenían:

1. Neutral: permite seleccionar o buscar; el botón de acción está deshabilitado.
2. Agregar: habilita los campos para el nuevo registro.
3. Editar: carga la selección y conserva su identificador mientras se modifica.
4. Eliminar: muestra el registro sin permitir editar sus campos. La eliminación exige confirmación y las restricciones de SQL.
5. Cancelar: descarta la operación en pantalla y vuelve a neutral.

Limpiar durante una edición conserva el identificador. En Inventario conserva también el producto y la revisión de existencias. Agregar inventario ofrece únicamente productos que todavía no tienen registro.

Desactivar sigue conservando el historial; no equivale a eliminar. En pedidos y facturas las opciones responden a sus reglas de negocio: una factura pagada se consulta o exporta y no se elimina desde estas pantallas.

Los menús y los nuevos indicadores de operación están declarados en sus Designer. Los eventos tienen métodos con nombre. Se conserva el comportamiento de los menús al ampliar y reducir la ventana.

## Verificación del bloque

Se comprobaron el flujo de pedidos, inventario, cobros, PDF, permisos, los modos de gestión y los controles heredados. Las pruebas con datos usaron bases temporales de la versión 9. La modificación de la base de trabajo se limitó a las tres descripciones de rol, respaldadas antes de actualizarlas.

También se abrió FrmPedidosEmpleado en el diseñador de Visual Studio y se confirmó que carga sus controles. Esta comprobación puntual no sustituye revisar cualquier diseño nuevo que se agregue después.
