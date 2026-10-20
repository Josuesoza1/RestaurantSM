# Gestiones y compras del cliente

Este bloque conecta los formularios de UIAdministracion y UICliente que estaban
pendientes. Las gestiones ya funcionales de empleados, categorías y productos
se conservan. No se implementaron cobros ni facturas en este bloque.

## Administración

- Clientes permite agregar perfiles de mostrador, seleccionar y actualizar,
  buscar con LINQ, desactivar y reactivar mediante Activo. La baja conserva
  el historial y se bloquea si existen pedidos o reservas pendientes.
  Crear un perfil de mostrador no crea una cuenta de acceso: el registro de
  cuentas de cliente sigue en Crear cuenta.
- Mesas permite agregar, seleccionar y editar, buscar, desactivar y reactivar.
  Valida número, estado y capacidad entre 1 y 20. Una mesa con pedidos abiertos
  o reservas futuras incompatibles no puede desactivarse.
- Roles permite altas, edición, búsqueda LINQ y eliminación de roles adicionales
  sin usuarios. Los roles 1 Administrador, 2 Empleado y 3 Cliente no se eliminan
  ni renombran; sí puede cambiarse su descripción. Crear un rol adicional no
  añade automáticamente otro menú ni lo habilita en la creación de empleados.
- Inventario trabaja por unidades de producto terminado. Agregar permite elegir
  un producto existente, indicar stock actual y mínimo y guardar. No se crea
  otro producto desde Inventario: su precio y categoría vienen de Productos.
  Elegir un registro permite actualizar sus existencias y reactivar su control.
  Retirar control exige stock cero y ningún pedido pendiente que lo utilice.
  Recargar actualiza los datos; Solo existencias bajas usa LINQ para comparar
  el stock actual con el mínimo. Una versión de fila evita sobrescribir cambios
  de stock realizados por un pedido mientras el formulario estaba abierto.

## Cliente

- Mi pedido guarda el encabezado y sus líneas en una transacción. El cliente
  y los precios se verifican en SQL. Solo se ofrecen pedidos sin entrega a
  domicilio en este bloque. El empleado se deja sin asignar hasta la atención
  del pedido: no se inventa una cuenta o empleado para completar ese campo.
- Mis compras muestra exclusivamente pedidos propios y su detalle. Seleccionar
  una fila permite ver los productos. Un pedido Pendiente sin factura se puede
  editar o cancelar. Cancelar conserva el pedido y devuelve las unidades que
  se habían descontado. Solicitudes repetidas no duplican el pedido ni el stock.
- Mis datos permite actualizar solamente el perfil de la sesión actual.
  No permite cambiar el rol, editar otra cuenta o recuperar contraseñas.
- Reservar mesa abre Mis reservas, con alta, consulta, edición de reservas
  futuras y cancelación. Se exige una mesa activa y con capacidad suficiente.
  Para este alcance cada reserva ocupa dos horas; SQL impide otra reserva
  activa de la misma mesa dentro de ese intervalo. No se crea un pago de reserva.

Los productos sin registro de inventario activo no limitan existencias. Esto
permite mantener el catálogo existente mientras se cargan cantidades reales.
Al activar el control, guardar un pedido descuenta las unidades y rechaza
cantidades insuficientes. Editar ajusta la diferencia; cancelar las devuelve.
El catálogo puede seguir mostrando un producto disponible aunque su stock esté
en cero: el guardado valida el stock y explica que no alcanza. Retirarlo del
catálogo sigue siendo una operación del formulario Productos.

## Capas y LINQ

UI lee controles, confirma acciones y muestra resultados. Los servicios BLL
están separados en ClientesGestionBLL, MesasGestionBLL, InventarioBLL,
ComprasClienteBLL y ReservasClienteBLL; RolBLL incorpora las escrituras.
GestionValidacion comprueba campos y límites. GestionDAL comparte la ejecución
parametrizada de procedimientos. La base conserva las reglas de relaciones,
transacciones, disponibilidad y permisos administrativos.

Todas las llamadas a SQL de la DAL usan CommandType.StoredProcedure. El perfil
propio se obtiene con sp_Cliente_ObtenerMiPerfil y el catálogo disponible con
sp_Producto_Disponibles; tampoco esas lecturas contienen SELECT embebidos en C#.

LINQ filtra clientes, mesas, roles e inventario; detecta existencias bajas,
filtra el historial y construye el detalle que se envía al procedimiento.
Los filtros operan sobre las tablas de datos ya obtenidas y las consultas del
cliente se restringen a su identidad antes de filtrar en memoria.

## SQL y traslado a otra PC

Aplicar sql/005_gestiones_pedidos_cliente.sql después de 001 a 004, sobre la
base existente. Primero realizar un respaldo. Este archivo no crea la base
desde cero. Detenerse y revisar si SQL Server informa un error.

La migración añade cliente.activo y mesa.activa, inventarioProducto,
pedido.solicitud para evitar duplicados, detallePedido.controlInventario y
permite pedido.idEmpleado NULL para pedidos todavía no asignados. Añade los
procedimientos de estas gestiones y actualiza el login para rechazar clientes
inactivos. Puede repetirse sin duplicar las columnas o tablas añadidas.

En esta PC se aplicó el 19 de octubre de 2026. Los conteos y las huellas de los
campos anteriores se conservaron. El inventario inicial no recibió cantidades
de prueba. Se guardó un respaldo completo verificado en la carpeta local de
respaldos de RestaurantSM dentro de AppData/Local, separado del repositorio.
También se conservó allí la definición anterior del procedimiento de login.

Las migraciones anteriores y el ZIP generado el 17 de octubre no están en este
checkout actual. La migración 005 se entrega como actualización adicional:
llevarla también a la otra PC junto con los archivos nuevos del proyecto.
Los informes y paquetes de aquella fecha no incluyen este bloque.

## Cómo probar desde la aplicación

1. Guardar cambios propios en Visual Studio y recargar los archivos modificados.
2. Compilar y entrar como administrador. En Más opciones abrir Clientes, Mesas
   e Inventario. Registrar una mesa y, si se desea controlar stock, cantidades
   reales para un producto. Estas operaciones manuales sí guardan datos.
3. Entrar con una cuenta Cliente y abrir Mis datos. Guardar un cambio propio.
4. Abrir Mi pedido, agregar productos y pulsar Enviar pedido. Consultar el
   número guardado en Mis compras y seleccionar la fila para ver el detalle.
5. Editar un pedido pendiente o cancelarlo. Revisar la diferencia de stock
   desde Inventario con Recargar. Cancelar dos veces no devuelve dos veces.
6. Crear, editar y cancelar una reserva futura de una mesa con capacidad suficiente.

## Verificación

Compilación Debug de los cinco proyectos sin errores ni advertencias.
tests/VerificarGestiones.ps1 crea una base temporal con esquema independiente,
ejecuta 63 comprobaciones y elimina esa base al finalizar. Prueba CRUD, filtros,
permisos, rollback, stock concurrente, duplicados, datos propios, reservas y
ocho diseñadores. Incluye clics reales de los botones de guardar de Clientes,
Mesas e Inventario dentro de la base de prueba. Las ventanas se muestran fuera
de pantalla y no se usan cuentas ni contraseñas reales en sus datos de prueba.

El verificador necesita el esquema original de RestaurantSM.sql y permisos
CREATE DATABASE. Puede indicarse su ruta con -EsquemaOriginal. Las pruebas
no ejecutan cobros o facturas y no sustituyen el recorrido manual de confirmaciones.
