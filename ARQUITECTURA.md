# Cómo está organizado RestaurantSM

El objetivo es seguir una operación desde el botón hasta la base, sin una clase que se encargue de todo. Los diseños, los modos de gestión y la adaptación al maximizar se conservan.

## Responsabilidad de cada capa

| Capa | Qué hace | Ejemplo |
|---|---|---|
| UI | Lee controles, llama a la BLL y muestra resultados. | FrmPago recoge el método y el importe recibido. |
| BLL | Exige permisos y aplica las reglas de la operación. | PagoBLL solo permite cobrar al personal. |
| Validaciones | Revisa campos, formatos y límites reutilizables. | PagoValidacion comprueba importes y método. |
| DAL | Abre conexiones y ejecuta procedimientos con parámetros. | PagoDAL llama a sp_Cobro_Registrar. |
| Entities | Representa registros, colecciones y datos de una operación. | Producto, Productos, DocumentoVenta. |

SQL vuelve a comprobar las reglas que afectan a los datos compartidos: existencias, estado actual, permisos de la cuenta y duplicados. Las transacciones evitan guardar una parte del pedido o del cobro cuando otra parte falla.

## Clases de negocio

| Clase | Responsabilidad |
|---|---|
| ClienteBLL | Registro, gestión administrativa, perfil propio y selección de clientes para pedidos. Los métodos comprueban el permiso correspondiente. |
| MesaBLL | Gestión de mesas, consulta para reservas, asignación a pedidos y limpieza. |
| PedidoBLL | Pedidos del personal, detalle, cambios de estado y cancelación. |
| ComprasClienteBLL | Pedidos del cliente autenticado: listar, editar y cancelar los propios. |
| PagoBLL | Cotización y registro del cobro. |
| FacturaBLL | Consulta de facturas y preparación de los datos de facturas o comprobantes de pedido. No dibuja el PDF. |
| InventarioBLL | Existencias, mínimos, búsquedas, entradas, salidas e historial. |
| ReservaBLL | Atención de reservas por el personal. |
| ReservasClienteBLL | Creación, consulta y cancelación de reservas propias del cliente. |
| ConfiguracionRestauranteBLL | Comprobación de instalación y configuración del nombre e impuesto incluido. |

Cada área tiene su DAL. Se retiraron OperacionRestauranteBLL y OperacionRestauranteDAL; sus métodos se distribuyeron entre estas clases. También se retiraron ClientesGestionBLL y MesasGestionBLL, integradas en ClienteBLL y MesaBLL.

Los flujos del cliente y del personal se distinguen porque tienen permisos y acciones diferentes. Compartir una pantalla no autoriza a consultar registros de otros clientes.

## Ejemplo: pulsar Registrar pago

1. El evento btnRegistrarPago_Click de FrmPago solicita confirmación.
2. PagoBLL.Cobrar comprueba que la sesión corresponde al personal.
3. PagoValidacion revisa el pedido, el método, los decimales, el monto recibido y las notas.
4. PagoDAL.Cobrar envía parámetros a sp_Cobro_Registrar.
5. SQL comprueba el importe real y el estado del pedido. Guarda pago y factura en una misma transacción.
6. El formulario muestra la confirmación. FacturaBLL recupera los datos; DocumentoPdf genera el PDF con QuestPDF.

El Guid solicitud identifica el intento: repetir el mismo envío no debe crear otro pedido ni otro cobro. No sustituye al identificador del pedido.

## Entidades, colecciones y listados

- Una entidad individual representa un registro, como Producto.
- Una colección plural agrupa entidades, como Productos, que hereda de List de Producto.
- Los catálogos que ya usan entidades conservan sus colecciones tipadas.
- Las consultas que combinan cliente, pedido, mesa e importes devuelven DataTable para los listados. Una fila de ese resultado no equivale a una nueva tabla de la base.
- DocumentoVenta reúne cabecera y detalle para un PDF. LineaPedidoBorrador representa una selección todavía en memoria. Son modelos de trabajo, no nuevas tablas.
- Las búsquedas usan BusquedaLINQ; filtran en memoria y no construyen consultas SQL concatenando el texto introducido.

No se agregaron clases plurales vacías para cada tabla. Se mantienen las colecciones que realmente se usan.

## Cambios de uso

La bandeja habilita cobrar, avanzar, cancelar y exportar según la selección y su estado. El detalle se actualiza también al seleccionar con el teclado. Una búsqueda sin coincidencias limpia el detalle anterior y deshabilita las acciones.

Actualizar el perfil propio usa ActualizarMiPerfil, cuyo identificador se obtiene de la sesión. Consultar mesas para reservar usa ListarParaReservar. Estos nombres sustituyen combinaciones de indicadores booleanos difíciles de interpretar.

## Evidencia y límites

tests/VerificarOperacion.ps1 prueba el flujo con datos ficticios en bases temporales, incluyendo las gestiones consolidadas, validaciones, permisos y cobros simultáneos. tests/VerificarDisenadores.ps1 comprueba controles, eventos y menús. Estas comprobaciones no sustituyen una prueba manual en la computadora que recibirá la aplicación.

La reorganización no exige una migración nueva: conserva los procedimientos y la versión 009. Se recuperó el actualizador completo que faltaba en sql; su contenido se genera desde estructura y procedimientos.

Antes de una entrega para uso real hay una mejora de seguridad pendiente: sp_Usuario_ValidarLogin todavía compara directamente la contraseña guardada. Debe sustituirse por almacenamiento y verificación de contraseñas con hash y una transición para las cuentas existentes. Es un cambio de autenticación; no se presenta esta reorganización como una solución a ese problema.
