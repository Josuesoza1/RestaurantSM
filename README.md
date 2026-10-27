# RestaurantSM

Sistema académico de restaurante en C# Windows Forms, .NET Framework 4.8 y SQL Server. La solución separa interfaz (UI), reglas de negocio (BLL), acceso a datos (DAL), entidades y validaciones. Las consultas y escrituras de las operaciones usan procedimientos almacenados; las búsquedas de los listados utilizan LINQ en memoria.

La [guía de arquitectura](ARQUITECTURA.md) muestra qué hace cada clase y cómo seguir un evento desde el formulario hasta SQL. Las operaciones se reparten por área; la gestión de clientes y mesas está integrada en sus BLL respectivas.

## Preparar una computadora

1. Abra `RestaurantSM.slnx` en Visual Studio con las herramientas de escritorio .NET y el paquete de destino .NET Framework 4.8.
2. Siga la [guía de instalación y actualización](sql/LEEME.md). Para una base existente, ejecute el [actualizador completo](sql/ACTUALIZAR_TODO_RESTAURANTSM.sql); para una base nueva, use [INSTALAR_NUEVA.sql](sql/INSTALAR_NUEVA.sql).
3. Ajuste únicamente la conexión `RestauranteDB` de `SistemaPedidoRestaurante/App.config` a su servidor y base. Hay un ejemplo sin contraseñas en `App.config.ejemplo`.
4. Restaure los paquetes NuGet y compile. QuestPDF se restaura desde el proyecto UI; conserve sus archivos y la carpeta `runtimes` al trasladar el ejecutable.

## Funciones

- Gestión de categorías, productos, roles, empleados, clientes, mesas e inventario. Consulta de usuarios sin mostrar contraseñas.
- Pedidos del cliente y del personal para clientes registrados, con carrito de varios productos, consulta, edición y cancelación mientras estén pendientes.
- Bandeja del personal: preparación, entrega, asignación de mesas, cobros y consultas. Estados: **Pendiente → En preparación → Listo → Entregado → Pagado**.
- Cobros en efectivo, tarjeta o transferencia. El sistema registra el método; la tarjeta y la transferencia se confirman manualmente, sin una pasarela bancaria.
- Facturas pagadas y comprobantes de pedido en PDF con QuestPDF. Las facturas conservan los nombres, precios e importes del momento del cobro.
- Inventario obligatorio por producto terminado, control de existencias, entradas/salidas justificadas e historial de movimientos. Un producto sin inventario activo o sin unidades no se ofrece para pedidos.
- Reservas del cliente y atención por el personal, incluyendo llegada, cancelación, ausencia y limpieza de mesas.
- Configuración administrativa del nombre del restaurante y del impuesto incluido. La tasa inicial es **0 %** y los precios del menú se mantienen como precios finales.
- La bandeja habilita las acciones según el estado del registro y muestra el detalle al seleccionar también con el teclado.

Se conservan los archivos de diseño, los modos de gestión y el comportamiento existente al maximizar. Las pantallas auxiliares nuevas tienen sus propios eventos de botones.

Los controles de las pantallas auxiliares y las opciones de los menús están declarados en sus archivos `.Designer.cs`, con sus recursos `.resx`. Se pueden seleccionar y editar desde el diseñador de Visual Studio; la propiedad **Eventos → Click** muestra el método asociado en el archivo principal del formulario. El menú adaptable sigue agrupando las opciones cuando falta espacio y mostrándolas al ampliar la ventana.

## SQL para estudiar

La versión 009 organiza los procedimientos por tema en `sql/procedimientos`, con parámetros normales, validaciones y transacciones. El pedido se guarda como cabecera, líneas y total; C# recorre sus productos dentro de una misma transacción. Se eliminó el envío por XML y la auditoría mediante contexto de sesión. El inventario usa un contador entero para detectar cambios.

La [guía para entender el SQL](sql/COMO_ENTENDER_SQL.md) explica el recorrido y las protecciones que se conservan para operaciones simultáneas y solicitudes repetidas. No es necesario borrar la base para pasar a esta versión.

## Verificación

Después de compilar en Debug, `tests/VerificarDisenadores.ps1` comprueba los controles, sus eventos y los menús al cambiar el tamaño. Genera capturas en una carpeta temporal y no necesita conexión a SQL Server. Esta prueba comprueba la construcción de los formularios; no automatiza la apertura del diseñador de Visual Studio.

Después de compilar en Debug, ejecute `tests/VerificarOperacion.ps1` desde Windows PowerShell. Instala y actualiza una base temporal, prueba el flujo con datos ficticios, genera PDF y captura las pantallas. Elimina esa base al terminar; no modifica la base de trabajo. Requiere permiso para crear y eliminar bases temporales en el servidor configurado.

La prueba incluye falta de inventario, permisos, estados, pagos duplicados, operaciones simultáneas, reversión ante fallos, conservación de facturas y actualización de otra base que carezca de subtotal y total. Los PDF se generan con [QuestPDF](https://www.questpdf.com/quick-start.html), aislado en `UIPrincipal/DocumentoPdf.cs`.
