# Cola de mejoras

## Organización completada

Ver `Bloque5-organizacion-formularios.md`. Clientes, mesas e inventario están
ahora en UIAdministracion; pedido, pago y facturas están en UIEmpleado.
FrmCompra se renombró a FrmPedido para que empleado y administrador registren
el pedido de un cliente. Los diseños se conservaron y sus operaciones de
negocio permanecen pendientes.

## Bloques completados

1. Registro y accesos: `Bloque1-registro-accesos.md`.
2. Sesión y navegación: `Bloque2-sesion-navegacion.md`.
3. Empleados y cuentas: `Bloque3-empleados.md`.
4. Categorías y productos: `Bloque4-categorias-productos.md`.
5. Organización de formularios y accesos: `Bloque5-organizacion-formularios.md`.
6. Catálogo y selección del cliente en memoria: `Bloque6-catalogo-cliente.md`.

## Pendientes en orden

La preparación del pedido propio del cliente está en `Pedidos-cliente-y-local.md`.
El administrador y el empleado conservan la captura interna para ventas en el local.

1. Guardar pedidos y sus detalles; conectar también la captura interna con cliente y mesa opcional.
2. Implementar cobro y factura respetando las relaciones entre las tablas.
3. Conectar la consulta propia del cliente y los reportes autorizados.
4. Completar gestión de clientes, mesas, inventario y reservas según el alcance
   acordado para la defensa. Antes de inventario, definir unidades y existencias.
5. Revisión visual general y recorrido de defensa.

La secuencia principal será pedido → detalle → cobro → factura. Cambiar una
pantalla de carpeta no reemplaza las validaciones de permisos en su lógica.

El hash de contraseñas queda pospuesto por indicación del usuario, hasta que
el profesor lo solicite.
