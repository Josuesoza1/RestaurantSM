namespace SistemaPedidoRestaurante.Entities
{
    public class InventarioProducto
    {
        public int IdProducto { get; set; }
        public int StockActual { get; set; }
        public int StockMinimo { get; set; }
        public bool Activo { get; set; }
        public int Revision { get; set; }
        public byte[] Version { get; set; }


        public string Producto { get; set; }
        public string Categoria { get; set; }
        public string Unidad { get; set; }
        public decimal PrecioVenta { get; set; }
        public bool TieneControl { get; set; }
        public string EstadoVenta { get; set; }

        public bool StockBajo => Activo && StockActual <= StockMinimo;

        public string EstadoStock
        {
            get
            {
                if (!TieneControl) return "Sin inventario";
                if (!Activo) return "Inactivo";
                if (StockActual == 0) return "Agotado";
                return StockBajo ? "Stock bajo" : "Disponible";
            }
        }
    }
}
