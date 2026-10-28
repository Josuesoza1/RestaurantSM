# Formularios y roles

## Acceso de cada rol

| Rol | Pantallas propias | Acciones |
|---|---|---|
| 1 Administrador | UIAdministracion | Gestiona catálogos, clientes, empleados, usuarios, mesas, inventario y configuración. Supervisa pedidos, reservas, cobros y facturas. |
| 2 Empleado | UIEmpleado | Sus opciones dependen del cargo Mesero o Cajero. |
| 3 Cliente | UICliente | Gestiona sus propios pedidos, reservas, perfil y documentos. |

Los nombres e identificadores básicos están definidos en RolesSistema. No se renumeraron roles ni se reasignaron usuarios.

El menú no es el único control de acceso: cada pantalla operativa comprueba el rol que le corresponde al abrirse. Las BLL conservan sus controles de permisos y SQL sigue comprobando la cuenta y las reglas de cada operación.

## Registro de personas desde administración

### Cargos y permisos de los empleados

| Cargo | Puede hacer | Acceso restringido |
|---|---|---|
| Mesero | Registrar pedidos, consultar su detalle, avanzar estados, cancelar pendientes, atender reservas y asignar o limpiar mesas. Exportar el comprobante de pedido. | Cobrar y consultar facturas pagadas. |
| Cajero | Consultar pedidos y detalle, cobrar pedidos entregados, consultar facturas y exportar documentos. | Crear, cancelar o avanzar pedidos; gestionar reservas, asignación o limpieza de mesas. |

Se conservan los tres roles. El administrador tiene acceso completo independientemente de su cargo; el cliente conserva sus propios pedidos, reservas y documentos. Por ahora Cocina y Reparto no se ofrecen como cargos de empleado porque todavía no tienen su flujo propio.

Cargo es un ComboBox en Registrar persona y Empleados. En cuentas nuevas se elige de la lista; al editar un administrador antiguo, su cargo anterior se conserva. Un empleado con cargo desconocido puede ingresar para ver la indicación de solicitar una corrección, pero no obtiene permisos operativos por defecto.

Al iniciar sesión, el cargo viene de la base de datos. Permisos comprueba cada acción en BLL y las pantallas muestran las opciones correspondientes. Los procedimientos también consultan el rol y cargo reales; cambiar una etiqueta o una variable en la interfaz no autoriza una operación. Después de cambiar cargo o rol, vuelva a iniciar sesión para actualizar las opciones visibles.

La actualización correspondiente es [011_permisos_mesero_cajero.sql](sql/011_permisos_mesero_cajero.sql). Se ejecuta completa sobre la base preparada con la versión 9 y puede repetirse. Actualiza procedimientos; no reasigna los roles de cuentas existentes ni altera ventas, inventario o datos personales. No es un instalador de base nueva.

El administrador puede abrir **Registrar persona** desde su menú (Más opciones cuando la ventana es pequeña) o desde el botón del listado **Usuarios**. Un solo formulario, FrmRegistrarPersona, adapta sus campos al tipo elegido:

- Cliente: datos personales, correo y cuenta con rol Cliente.
- Empleado: datos personales, cargo Mesero o Cajero y cuenta con rol Empleado.
- Administrador: datos personales, cargo y cuenta con rol Administrador; solicita confirmar la concesión de acceso completo.

El rol no sustituye al cargo: un administrador se guarda como personal en empleado y su acceso se identifica en usuario. No existe una tabla adicional de administradores. Las nuevas personas se crean activas, con cuenta para iniciar sesión; para editar personas ya registradas se mantienen Clientes y Empleados.

Los controles están declarados en FrmRegistrarPersona.Designer.cs y sus eventos en FrmRegistrarPersona.cs. UsuarioBLL comprueba el permiso administrativo y dirige el registro a ClienteBLL o EmpleadoBLL. Las validaciones reutilizan la capa Validaciones. Los procedimientos existentes sp_InsertarCliente y sp_Empleado_Insertar guardan perfil y cuenta dentro de una transacción: si falla el perfil, no queda una cuenta incompleta. El registro reutiliza estos procedimientos; la separación por cargo requiere aplicar la actualización 011 indicada arriba.

Se validan nombres, cédula, teléfono, correo de cliente, cargo del personal, usuario, contraseña y confirmación. Los duplicados se informan sin mostrar contraseñas. Al registrar desde Usuarios, el listado se recarga después del guardado.

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

Para separar Mesero y Cajero se verificaron permisos en BLL, acceso directo a formularios, menús al maximizar y llamadas directas a SQL. Las pruebas usaron una base temporal y repitieron la actualización para comprobar que puede ejecutarse de nuevo. La actualización local de procedimientos se aplicó con copia de seguridad verificada y comparación de los datos antes y después; conservó cuentas, roles, personal, clientes, ventas, inventario, reservas y mesas.
