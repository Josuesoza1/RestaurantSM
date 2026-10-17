# Cambios y traslado de RestaurantSM

Informe técnico explicado para el desarrollo y la defensa

Josue · 17 de octubre de 2026

## Situación actual del proyecto

RestaurantSM ya tiene una base funcional de acceso por roles, sesión, gestión de empleados y catálogo. El cliente puede consultar los productos disponibles y preparar una selección con cantidades e importes. El proyecto aún necesita implementar el guardado de pedidos, el cobro y la factura para completar una venta de principio a fin.

Este informe reúne los cambios de la sesión de trabajo, explica por qué se hicieron y describe cómo reproducir la base en otra computadora. Se revisaron los archivos reales del repositorio, los documentos de cada bloque y la base configurada. Los resultados de pruebas anteriores se distinguen de las comprobaciones repetidas para esta entrega.

## Qué está terminado y qué falta

| Módulo | Estado comprobado |
|---|---|
| Registro e inicio de sesión | Registro público de Cliente y menú específico para cada rol |
| Sesión y navegación | Identidad compartida, cierre de sesión y retorno al mismo login |
| Empleados | Alta con cuenta, edición, búsqueda, desactivación y reactivación |
| Categorías y productos | Alta, edición, búsqueda y baja lógica |
| Catálogo de demostración | 6 categorías, 12 subcategorías y 24 productos |
| Pedido propio del cliente | Perfil propio, catálogo y selección en memoria con total estimado |
| Roles | Listado y búsqueda; operaciones de guardado pendientes |
| Pedido interno y venta | Pantallas preparadas; guardado, cobro y factura pendientes |
| Clientes, mesas e inventario | Formularios organizados; gestión pendiente |

La existencia de una pantalla o de una tabla no significa que su operación ya esté implementada. En particular, el botón Enviar pedido permanece deshabilitado y la selección del cliente se pierde al cerrar su ventana.

<!-- pagebreak -->

## Acceso y navegación

El login dejó de tratar como cliente a cualquier usuario que no fuera administrador. Ahora evalúa expresamente los roles 1 Administrador, 2 Empleado y 3 Cliente; un identificador no reconocido se rechaza. Esta correspondencia entre IDs y nombres debe mantenerse en la base de la otra PC.

El registro público tenía otro problema: el procedimiento sp_InsertarCliente asignaba el rol de Empleado. La migración 001 lo corrige a Cliente y conserva la transacción que crea juntos el usuario y su perfil. También se conectó la selección de género a los valores M, F y O, con su validación antes de guardar. Las cuentas antiguas no cambiaron de rol automáticamente.

La clase SesionActual mantiene el ID, nombre de usuario y rol en memoria. No guarda la contraseña. MenuSesion agrega el cierre de sesión y muestra la identidad en el título de los menús. Cerrar sesión limpia la identidad y vuelve al mismo formulario de login.

Se cambió la navegación para abrir los menús y las pantallas mediante diálogos asociados a su propietario. Volver cierra la ventana actual y reutiliza la anterior. Así se evita acumular menús ocultos o crear otro login en cada regreso.

## Organización de formularios

Login contiene el acceso y el registro. UIAdministracion reúne el menú y las gestiones administrativas. UIEmpleado contiene el pedido interno, pago y facturas, accesibles también para el administrador. UICliente contiene el menú y el pedido propio. UIPrincipal reúne ayudas de sesión y preparación de pantallas.

FrmCompra se renombró a FrmPedido para expresar su uso interno por empleado y administrador. FrmPedidoCliente se separó para usar el perfil del cliente autenticado. La carpeta ayuda a entender la responsabilidad; los controles de rol y las validaciones de las operaciones son los que comprueban el acceso.

## Correcciones del diseñador

Visual Studio mostraba un fallo al cargar System.Windows.Forms.Form. Se verificaron las referencias y la compilación, se añadieron comprobaciones del modo de diseño y se regeneraron archivos temporales con respaldo. La lógica de carga ya no debe consultar la base o preparar la sesión durante la apertura del diseñador.

No se confirmó una causa única del mensaje original. Las ventanas repetidas de referencia a objeto aparecieron durante pruebas de carga de formularios y no son, por sí solas, evidencia de daño a los datos. Después se comprobaron los diseñadores compilados y el usuario confirmó que los formularios volvían a abrir.

<!-- pagebreak -->

## Separación de responsabilidades

El proyecto conserva cinco capas. UI trabaja con TextBox, ComboBox, DataGridView y mensajes. Validaciones comprueba los campos y sus límites. BLL coordina las reglas y los permisos de la aplicación. DAL ejecuta consultas y procedimientos mediante parámetros. Entities representa los datos que circulan entre esas capas.

Por eso los métodos que leen controles o muestran un empleado permanecen en el formulario. Moverlos a DAL haría que la base dependiera de Windows Forms. En cambio, las reglas para aceptar una cantidad, calcular importes o permitir una operación administrativa pertenecen a BLL y Validaciones.

## Empleados y cuentas

El formulario de empleados se alineó con la entidad y la estructura real de SQL. Incluye cargo, género, cédula, estado y datos de la cuenta. El alta guarda empleado y usuario dentro de una misma transacción: si una parte falla, no debe quedar la otra guardada por separado.

Editar con la contraseña vacía conserva la existente; escribir una nueva la reemplaza. La contraseña almacenada no se carga en el formulario. Desactivar conserva al empleado y sus relaciones, pero el procedimiento de login rechaza su cuenta. Editar y marcar Activo permite reactivarlo. También se evita que el administrador se desactive o se quite su propio rol.

empleado.idUsuario sigue siendo nullable en SQL y la entidad usa int?. Esto permite leer registros antiguos sin cuenta. Los nuevos empleados se crean con cuenta y al editar un registro antiguo sin usuario se exige una contraseña para crearla.

## Categorías y productos

Se implementaron altas, edición, búsqueda y baja lógica. Desactivar una categoría exige retirar primero sus productos disponibles. Un producto disponible necesita una categoría activa. Retirar un producto cambia su disponibilidad y conserva los registros que puedan referenciarlo.

La subcategoría es opcional, se filtra por la categoría elegida y se comprueba su correspondencia también en SQL. Todavía no hay un formulario de mantenimiento de subcategorías. El precio acepta hasta dos decimales y los campos se limitan a los tamaños que admite la base.

Las escrituras administrativas ya implementadas comprueban la sesión en BLL y el administrador activo en procedimientos SQL. Esto mejora la consistencia del proyecto académico; no equivale a una auditoría completa ni reemplaza los permisos del motor SQL Server.

<!-- pagebreak -->

## Pedido del cliente y catálogo

ClienteBLL.ObtenerMiPerfil obtiene la identidad desde SesionActual y exige el rol Cliente. El formulario no permite escribir el ID de otro usuario. Los datos personales se cargan mediante una consulta parametrizada y se muestran como solo lectura; la búsqueda de otros clientes y el registro de pagos no aparecen en esa pantalla.

El catálogo devuelve productos disponibles de categorías activas. El cliente filtra por categoría, elige producto y cantidad, y el precio se toma de la base sin permitir su edición manual. Puede añadir varios productos, quitar una línea o vaciar la selección.

PedidoBorrador, en BLL, calcula con decimal los subtotales y el total estimado. Si se agrega otra vez un producto, acumula la cantidad en una misma línea. Comprueba cantidades entre 1 y 999, precios válidos y el límite del importe antes de modificar la selección. LineaPedidoBorrador representa cada línea del detalle.

El precio de una línea se conserva durante esta selección en memoria. Cuando se implemente el guardado habrá que consultar otra vez disponibilidad y precios en SQL, identificar el perfil correcto y guardar pedido y detalle dentro de una transacción. Por ahora no se reserva inventario, se cobra ni se emite una factura.

## Datos de demostración

Se conservaron los IDs, nombres, códigos y precios de los seis productos originales. Se completaron descripciones y subcategorías, y se añadieron 18 productos de demostración. No se cambiaron usuarios, pedidos, pagos ni facturas con este ajuste del catálogo.

| Categoría | Subcategorías |
|---|---|
| Entradas | Entradas calientes y Entradas frías |
| Platos Fuertes | Carnes y Aves |
| Bebidas | Refrescos naturales y Gaseosas |
| Postres | Pasteles y Helados |
| Sopas y Ensaladas | Sopas y Ensaladas |
| Comida Rapida | Hamburguesas y Pizzas |

Cada subcategoría tiene dos productos. El administrador ya puede ver la subcategoría en Productos. En Mi pedido el filtro actual sigue siendo por categoría; un filtro específico por subcategoría queda como posible mejora de presentación.

<!-- pagebreak -->

## Cambios realizados en la base

La modificación estructural de estos bloques es categoria.activa, BIT NOT NULL con valor inicial 1. Las demás migraciones actualizan procedimientos o datos. No se transformó empleado.idUsuario a obligatorio ni se cambiaron las tablas de pedidos, pagos o facturas para completar una venta.

| Archivo | Efecto |
|---|---|
| 001_corregir_rol_registro_cliente.sql | Asigna Cliente al registro público |
| 002_gestion_empleados.sql | Listado, alta, edición, desactivación y rechazo de empleados inactivos en login |
| 003_categorias_productos.sql | Añade activa y procedimientos del catálogo con reglas de categoría y subcategoría |
| 004_catalogo_demostracion.sql | Completa y amplía productos y subcategorías sin duplicarlos al repetir |

Se conservaron copias anteriores de procedimientos y una instantánea JSON del catálogo antes de ampliarlo. Son respaldos parciales de cambios concretos, no un respaldo completo de SQL Server. Para trasladar los datos personales y operativos exactos se necesita un respaldo completo y una restauración; el paquete entregado crea una base limpia con datos de demostración.

## Qué contiene el paquete portable

sql/instalacion/INSTALAR_NUEVA.sql contiene el esquema base, los datos de referencia, las migraciones 001 a 004 y una verificación final. Incluye las 13 tablas de negocio y los procedimientos requeridos. No necesita el archivo de Documents, rutas de archivos de esta PC ni cuentas personales.

ACTUALIZAR_EXISTENTE.sql aplica las migraciones y la verificación sobre una base anterior del mismo proyecto. No recrea las tablas ni elimina la información existente. Exige los roles esperados y se detiene ante conflictos del catálogo. Las migraciones se confirman por bloques; un error posterior no revierte automáticamente los anteriores.

020_primer_administrador.sql es una plantilla separada para elegir el usuario y la clave de la primera cuenta administrativa de una instalación nueva. 030_verificar.sql comprueba roles, procedimientos y relaciones básicas y muestra el catálogo.

Ambos archivos principales se ejecutan en SSMS con Consulta > Modo SQLCMD activado. La directiva de detención ante errores evita continuar con bloques posteriores después de un fallo. El instalador nuevo se niega a reemplazar una base que ya exista.

<!-- pagebreak -->

## Pasos para otra computadora

1. Instalar el motor SQL Server o LocalDB, SSMS y las herramientas de escritorio .NET de Visual Studio con .NET Framework 4.8. Usar una versión de Visual Studio que abra RestaurantSM.slnx.
2. Clonar el repositorio y conectarse desde SSMS a la instancia elegida. Para LocalDB local se puede usar (localdb)\MSSQLLocalDB con autenticación de Windows.
3. Si no existe la base, ejecutar INSTALAR_NUEVA.sql con Modo SQLCMD. Si ya existe una versión anterior, hacer primero un respaldo completo y ejecutar ACTUALIZAR_EXISTENTE.sql.
4. Solo en la instalación nueva, editar y ejecutar la plantilla del primer administrador con valores locales. No subir la plantilla con una clave real escrita.
5. Configurar la entrada RestauranteDB en SistemaPedidoRestaurante/App.config. El ejemplo App.config.ejemplo usa autenticación de Windows y no contiene contraseñas.
6. Abrir la solución, seleccionar el proyecto UI como inicio, compilar y entrar con la cuenta administrativa. Crear empleados desde su gestión y clientes desde Crear cuenta.
7. Comprobar los tres accesos y abrir Mi pedido con un cliente. Deben aparecer 24 productos de demostración si ninguno se retiró después de la instalación.

## Conexión y repositorio

La cuenta que conecta con SQL Server es distinta de las cuentas de administrador, empleado y cliente de RestaurantSM. Crear un usuario en la tabla usuario no crea un login del motor. Si se usa autenticación SQL, el equipo tendrá que configurar ese acceso localmente; el paquete no crea credenciales del servidor.

GitHub transporta el código y los scripts, pero no instala ni sincroniza la base. El commit debe incluir los archivos nuevos de SQL, las clases, formularios, recursos y proyectos modificados. Los archivos nuevos sin seguimiento no quedan incluidos por el solo hecho de existir en la carpeta.

App.config es un archivo seguido por Git y la configuración local puede contener credenciales. Antes de subirlo, sustituir cualquier secreto por una configuración compartible y guardar los valores privados fuera del commit. No se cambió la conexión actual para preparar esta entrega. Tampoco se hizo commit ni push.

En Visual Studio puede aparecer Recargar todo por cambios externos. Primero guardar los cambios propios. Si existe un conflicto con ediciones sin guardar, conservar una copia antes de descartar o sobrescribir. bin, obj y .vs son archivos locales de compilación o del IDE y no se necesitan para compartir el código fuente.

<!-- pagebreak -->

## Comprobaciones de esta entrega

La solución compiló en Debug con MSBuild de Visual Studio 2026 y .NET Framework 4.8, sin errores ni advertencias. Se repitieron las 27 comprobaciones del catálogo del cliente, que incluyen consulta real de solo lectura, cálculos, límites, controles y carga del diseñador.

El nuevo verificador de instalación pasó 16 comprobaciones en una base temporal independiente. Verificó 13 tablas, tres roles, seis categorías, doce subcategorías, veinticuatro productos, ausencia de cuentas copiadas, login, registro con rol Cliente, conservación de cuentas y perfiles y actualización repetida sin duplicados. También comprobó que el instalador nuevo rechaza una base existente. La base temporal se eliminó al finalizar.

Estas pruebas del paquete se ejecutaron en SQL Server 2025 LocalDB de esta PC. Se retiraron las rutas MDF y opciones específicas del exportador original para facilitar el traslado, pero no se comprobó la instalación en otro equipo físico ni en versiones anteriores del motor. El primer recorrido en destino debe confirmar la conexión y el entorno de Visual Studio.

Las pruebas de empleados, catálogos, navegación y reorganización de bloques anteriores están documentadas en los archivos Bloque1 a Bloque5. Sus resultados son evidencia de esas revisiones; no se presentan como pruebas completas repetidas en esta entrega.

## Análisis y próximo bloque recomendado

La separación por roles, los procedimientos parametrizados y las bajas lógicas dan una base más consistente para la defensa. El proyecto conserva las relaciones históricas y diferencia la gestión administrativa de la atención de un cliente. El catálogo ampliado facilita mostrar categorías y subcategorías con ejemplos reales de uso.

El siguiente cambio de mayor utilidad es guardar pedido y detalle. Debe servir tanto al cliente, con su perfil fijo, como al empleado o administrador que atiende una venta. Será necesario definir mesa opcional, estado inicial e identificación del empleado, revalidar precios y disponibilidad y evitar guardar encabezados sin detalle si ocurre un error.

Después corresponde conectar el cobro y la factura según las relaciones existentes, y finalmente historial y reportes. Roles todavía necesita su lógica de modificación; clientes, mesas e inventario también necesitan sus operaciones. Antes de inventario hay que definir unidades y existencias.

El hash de contraseñas permanece pospuesto por indicación de Josue, hasta que el profesor lo solicite. El formato actual no se presenta como una mejora de seguridad terminada. Tampoco se implementó una auditoría de acciones ni una validación completa de un flujo de venta.

La guía operativa con archivos y solución de errores está en sql/instalacion/LEEME.md. Este informe y sus fuentes se incluyen con el proyecto para mantener el análisis junto al código.
