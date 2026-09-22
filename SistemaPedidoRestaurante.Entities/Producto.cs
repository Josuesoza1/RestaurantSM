namespace SistemaPedidoRestaurante.Entities
{
    public class Producto
    {
        public int IdProducto { get; set; }
        public int IdCategoria { get; set; }
        public string CategoriaNombre { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public string Descripcion { get; set; }
        public string Codigo { get; set; }
        public bool Disponible { get; set; }
    }
}