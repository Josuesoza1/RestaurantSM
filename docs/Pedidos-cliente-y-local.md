# Pedido del cliente y pedido en el local

Se separaron las pantallas para preparar ambos recorridos:

| Acceso | Pantalla | Alcance actual |
|---|---|---|
| Cliente > Mi pedido | UICliente/FrmPedidoCliente | Carga sus propios datos; búsqueda de otros clientes y registro de cobros ocultos. |
| Empleado > Operaciones > Pedidos en el local | UIEmpleado/FrmPedido | Preparación de la captura interna para atender a un cliente. |
| Administrador > Más opciones > Operaciones > Pedidos en el local | UIEmpleado/FrmPedido | Mismo acceso interno para preparar una venta en el local. |

El login sigue abriendo el menú correspondiente a cada rol. Un cliente abre
su pantalla desde Mi pedido; no se le muestra la captura interna del restaurante.

Los datos personales del cliente son de solo lectura. ClienteBLL.ObtenerMiPerfil
exige rol Cliente y toma el ID de SesionActual, sin aceptar otro ID desde el
formulario. ClienteDAL consulta con un parámetro SQL y no modifica registros.
Una cuenta sin perfil asociado muestra esa situación en la propia pantalla.

Se reutilizó el diseño del pedido para mantener su aspecto. El cliente no ve
las acciones administrativas ni el botón de registrar pago. Enviar pedido está
deshabilitado mientras se implementa el guardado. El catálogo y la selección
en memoria ya están disponibles; ver `Bloque6-catalogo-cliente.md`.
Por ahora no se registran pedidos ni ventas desde estas pantallas.

Mantener el acceso interno del administrador es útil para la venta básica:
seleccionará a quién atiende y guardará el pedido. La captura propia del cliente
usará su perfil. Ambos recorridos compartirán las reglas y la persistencia en
BLL/DAL, sin duplicar las operaciones de negocio.

Para probar: recargar los cambios, compilar, iniciar sesión con una cuenta de
Cliente y abrir Mi pedido. Su nombre, cédula, teléfono y correo deben cargarse
sin permitir cambiarlos. Volver regresa al menú y conserva la sesión.

Verificación: compilación sin errores ni advertencias, 101 comprobaciones de
organización y diseñadores y consulta de un perfil real con rol Cliente. Se
comprobó también el rechazo de la consulta sin sesión. Solo se ejecutaron
consultas SELECT; no hubo migraciones ni cambios de datos en este ajuste.
