# Bloque 5: organización y navegación

Se movieron juntos los archivos .cs, .Designer.cs y .resx y se actualizaron
los namespaces, las referencias y los recursos del proyecto. Se conservaron
las posiciones, tamaños y controles de los diseños. FrmCompra se renombró a
FrmPedido, con autorización del usuario para uso de empleado y administrador.

| Carpeta | Pantallas |
|---|---|
| Login | Inicio de sesión y creación de cuenta de cliente. |
| UIAdministracion | Menú, empleados, categorías, productos, roles, clientes, mesas e inventario. |
| UIEmpleado | Menú, pedido, pago y facturas. El administrador también puede acceder a las pantallas operativas. |
| UICliente | Menú personal; sus pedidos, reservas e historial aún están pendientes. |
| UIPrincipal | Ayudas compartidas de sesión y preparación de pantallas. |

La carpeta por sí sola no concede acceso. Las pantallas trasladadas comprueban
el rol al abrirse. Las operaciones de escritura que ya están implementadas
siguen teniendo sus validaciones de BLL y SQL.

## Dónde abrir cada pantalla

- Administrador: los módulos ya implementados mantienen sus accesos. En
  Más opciones están Clientes, Mesas e Inventario; dentro de Operaciones están
  Pedidos, Cobros y Facturas.
- Empleado: Operaciones contiene Pedidos, Cobros y Facturas.
- Cliente: se mantienen sus opciones personales, deshabilitadas hasta conectar
  sus funciones. No se le abre una gestión general de clientes o facturas.

Las seis pantallas trasladadas tienen la indicación En preparación en su título.
Se pueden revisar sus diseños y volver mediante la X o Volver cuando existe.
Sus botones y opciones de modificación están deshabilitados. No guardan ni
consultan datos de negocio en este bloque.

Se retiró de FrmClientes la carga antigua de columnas que no correspondían a
la entidad actual. Su listado se conectará cuando se implemente esa gestión.
Cerrar Clientes ya no termina la aplicación: regresa al menú y conserva la sesión.

## Secuencia que se implementará

1. Seleccionar cliente y, cuando corresponda, mesa.
2. Crear un pedido y añadir varios productos con cantidades a su detalle.
3. Calcular importes y atender el pedido.
4. Registrar el cobro y emitir la factura según las relaciones de la base.
5. Consultar el historial propio del cliente y los reportes autorizados.

La reorganización no implementa todavía estos pasos: prepara su ubicación y
accesos. No se modificó la base ni se añadieron procedimientos SQL.

## Cómo comprobarlo

Guardar cambios propios, elegir Recargar todo y recompilar después de que
termine este bloque. Si Visual Studio conserva una pestaña con la ruta antigua,
cerrarla y abrir el formulario desde su carpeta nueva en el Explorador de soluciones.

Ejecutar con F5 y abrir las rutas según el rol. Comprobar que el título indique
En preparación, que no se pueda guardar y que la X o Volver regrese al menú
manteniendo la sesión.

Verificación realizada: compilación sin errores ni advertencias y 100
comprobaciones de carpetas, permisos de los tres roles, regreso al propietario,
acciones pendientes deshabilitadas, rutas de menú y carga de los 15 diseñadores.
Se usaron identidades simuladas para navegación, sin conexión SQL y sin
mensajes de confirmación en el escritorio.

El verificador está en `tests/VerificarReorganizacion.ps1`; requiere compilar antes.
Siguiente bloque: pedidos con encabezado y detalle de varios productos.
