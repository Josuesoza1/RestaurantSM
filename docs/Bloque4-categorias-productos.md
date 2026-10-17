# Bloque 4: categorías y productos

## Categorías

Se implementaron Agregar, Editar, Desactivar y búsqueda. La casilla Activa
permite reactivar una categoría desde Editar. No se borran categorías.

Para desactivar una categoría, primero deben quedar como no disponibles sus
productos. La misma regla se comprueba al desmarcar Activa mientras se edita;
no se modifican los productos automáticamente.

Nombre es obligatorio y único, con máximo 60 caracteres. La descripción es
opcional y admite 200 caracteres.

## Productos

Se implementaron Agregar, Editar, Retirar y búsqueda. Retirar marca Disponible
como falso, conservando el producto y sus relaciones con detalles de pedidos.
Editar y marcar Disponible permite volver a ofrecerlo si la categoría está activa.

El formulario incorpora la descripción y una selección opcional de subcategoría.
Las subcategorías se filtran según la categoría elegida; al cambiar categoría
se restablece Sin subcategoría. SQL también valida esa relación. No se agregó
un CRUD de subcategorías: actualmente la tabla está vacía y puede usarse la
opción Sin subcategoría.

Nombre y código son obligatorios y únicos; admiten 100 y 20 caracteres. La
descripción es opcional, con máximo 250 caracteres. El precio debe ser mayor
que cero, no superar 99999999.99 y tener hasta dos decimales.

El formulario acepta precios como `150.50` o `150,50`, sin separadores de miles.
Las categorías inactivas se identifican en el desplegable: permiten conservar
productos no disponibles, pero no ofrecer productos disponibles.

## Cómo probarlo

1. Guardar las ediciones propias, elegir Recargar todo si Visual Studio avisa
   de cambios externos, recompilar y ejecutar con F5.
2. Iniciar sesión como administrador y abrir Categorías. Agregar una categoría
   con nombre nuevo y guardarla.
3. Volver al menú y abrir Productos. Agregar un producto con esa categoría,
   nombre, precio, código y Disponible marcado; dejar Sin subcategoría si no
   existen opciones.
4. Seleccionar el producto, editarlo y cambiar precio o descripción.
5. Intentar desactivar su categoría: debe explicar que existen productos
   disponibles. Retirar el producto y luego desactivar la categoría.
6. Reactivar la categoría desde Editar y volver a marcar Disponible en el producto.

Las operaciones manuales de esta lista sí conservan los datos guardados. Los
listados son de selección, no de edición directa. Limpiar conserva la identidad
del registro en edición; Cancelar vuelve al modo neutral.

## Estructura por capas

- UI: selección, lectura de controles, conversión de texto de precio y mensajes.
- CatalogoValidacion: obligatorios, tamaños y precisión del precio.
- BLL: exige sesión administrativa y coordina guardar, retirar o desactivar.
- DAL: envía parámetros tipados a los procedimientos.
- SQL: verifica al administrador activo, unicidad y relaciones, y aplica
  transacciones para las operaciones que dependen de varios pasos.

Los métodos `ObtenerCategoriaDesdeFormulario`, `CargarCategoriaEnFormulario`,
`ObtenerProductoDesdeFormulario` y `CargarProductoEnFormulario` permanecen en
la UI porque trabajan con controles de Windows Forms.

## Migración de base aplicada

Se aplicó `sql/003_categorias_productos.sql`:

- Añade `categoria.activa` como BIT NOT NULL con valor inicial 1. Las categorías
  existentes quedaron activas; sus campos anteriores conservan los datos.
- Actualiza los procedimientos de listado y añade los de guardar/desactivar.
- Añade la consulta de subcategorías y la comprobación SQL del administrador.
- No borra ni cambia datos de los productos existentes.

El respaldo está en `sql/respaldos/003_catalogos_antes.sql`; una reversión debe
coordinarse con el código porque la DAL actual requiere las columnas nuevas.
El script original de Documentos se conserva sin cambios. Para recrear la base
en otra máquina, aplicar las migraciones 001, 002 y 003 después del script original.

## Comprobaciones

- Compilación Debug satisfactoria de los cinco proyectos.
- 33 comprobaciones de BLL/DAL/SQL con transacciones revertidas: altas, edición,
  disponibilidad, reactivación, duplicados, permisos y subcategorías compatibles.
- Se verificaron conteos y sumas de comprobación de categorías, productos,
  subcategorías, detalles de pedidos, usuarios y empleados: sin datos de prueba
  ni cambios en las filas originales durante esas pruebas.
- 17 comprobaciones de formulario y carga de sus diseñadores, más revisión
  visual de ambas pantallas. No se automatizaron los mensajes de confirmación
  de guardado o retirada; la escritura se comprobó mediante BLL/DAL.

Para repetir las pruebas de lógica, después de compilar:

```powershell
.\tests\VerificarCatalogos.ps1
```

Ejecutarlas sin ediciones simultáneas sobre esos datos. No abren formularios
ni imprimen credenciales. Las inserciones revertidas pueden avanzar las
secuencias IDENTITY y dejar saltos, sin afectar las relaciones.

Siguiente paso: revisar la reorganización pendiente de formularios antes de
conectar el flujo de pedidos, cobro y factura.
