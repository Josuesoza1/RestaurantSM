namespace SistemaPedidoRestaurante.Entities
{
    public class InventarioProducto
    {
        public int IdProducto { get; set; }
        public int StockActual { get; set; }
        public int StockMinimo { get; set; }
        public bool Activo { get; set; }
        public byte[] Version { get; set; }


        public string Producto { get; set; }
        public string Categoria { get; set; }
        public string Unidad { get; set; }
        public decimal PrecioVenta { get; set; }
    }
}
