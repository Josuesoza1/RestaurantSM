namespace SistemaPedidoRestaurante.Entities
{
    public class Producto
    {
        public int IdProducto { get; set; }
        public int IdCategoria { get; set; }
        public int? IdSubcategoria { get; set; }
        public string CategoriaNombre { get; set; }
        public string SubcategoriaNombre { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public string Descripcion { get; set; }
        public string Codigo { get; set; }
        public bool Disponible { get; set; }
        public bool CategoriaActiva { get; set; }
        public bool? InventarioActivo { get; set; }
        public int StockActual { get; set; }
        public int StockMinimo { get; set; }

        private string MotivoSinVenta
        {
            get
            {
                if (!Disponible) return "Producto deshabilitado";
                if (!CategoriaActiva) return "Categoría inactiva";
                if (!InventarioActivo.HasValue) return "Sin inventario";
                if (!InventarioActivo.Value) return "Control inactivo";
                if (StockActual <= 0) return "Agotado";
                return null;
            }
        }

        public bool PuedeVenderse => MotivoSinVenta == null;
        public string EstadoVenta => MotivoSinVenta ?? "Disponible";
    }
}
