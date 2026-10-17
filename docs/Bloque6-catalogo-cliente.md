# Catálogo y selección del cliente

Cliente > Mi pedido muestra productos disponibles de categorías activas.
Se puede filtrar por categoría, elegir un producto y una cantidad entre 1 y
999. El precio se toma del catálogo y no se puede escribir manualmente.

Agregar al pedido incorpora una línea al detalle. Si el producto ya estaba,
suma la cantidad. Quitar seleccionado y Vaciar pedido permiten corregir la
selección. La pantalla calcula subtotales y un total estimado con decimales.
Los datos del cliente siguen siendo propios y de solo lectura.

La consulta SQL está en ProductoDAL; PedidoBorrador en BLL valida cantidades,
precios y calcula importes; el formulario lee controles y muestra resultados.
El precio de cada línea se mantiene durante esta selección.

## Límite actual

La selección vive en memoria y se pierde al cerrar la ventana. Enviar pedido
está deshabilitado: todavía no se registra una venta ni se realiza un cobro.
Este bloque solo consulta la base; no requiere migraciones ni modifica datos.

El siguiente bloque será guardar pedido y detalle en una transacción,
comprobando nuevamente en la base los precios, la disponibilidad y el perfil
del cliente. Después se conectarán cobro y factura.

## Prueba manual

1. Recargar los cambios en Visual Studio y compilar.
2. Iniciar sesión con una cuenta Cliente y abrir Mi pedido.
3. Comprobar los datos propios, filtrar por categoría y agregar productos.
4. Agregar nuevamente el mismo producto para comprobar la suma de cantidades.
5. Quitar una línea o vaciar y comprobar el total.

Verificación automatizada: tests/VerificarCatalogoCliente.ps1, 27 comprobaciones
de consulta real de solo lectura, permisos, cálculos, límites, filtro, controles
y diseñador. Las pruebas visuales usan productos ficticios; no se insertan.
