namespace SistemaPedidoRestaurante.Entities
{
    public class LineaPedidoBorrador
    {
        public int IdProducto { get; private set; }
        public string Producto { get; private set; }
        public decimal PrecioUnitario { get; private set; }
        public int Cantidad { get; private set; }
        public decimal Subtotal => PrecioUnitario * Cantidad;

        public LineaPedidoBorrador(int idProducto, string producto, decimal precio, int cantidad)
        {
            IdProducto = idProducto;
            Producto = producto;
            PrecioUnitario = precio;
            Cantidad = cantidad;
        }
    }
}
