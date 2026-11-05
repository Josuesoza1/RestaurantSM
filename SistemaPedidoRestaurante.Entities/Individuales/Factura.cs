using System;

namespace SistemaPedidoRestaurante.Entities
{
    public class Factura
    {
        public int IdFactura { get; set; }
        public int IdPedido { get; set; }
        public string NumeroFactura { get; set; }
        public DateTime FechaEmision { get; set; }
        public string Estado { get; set; }
        public decimal Subtotal { get; set; }
        public decimal CostoEnvio { get; set; }
        public decimal Descuento { get; set; }
        public decimal Impuestos { get; set; }
        public decimal Propina { get; set; }
        public decimal Total { get; set; }
        public string ClienteNombre { get; set; }
        public string ClienteCedula { get; set; }
        public string EmpleadoNombre { get; set; }
        public decimal? TasaImpuesto { get; set; }
        public string RestauranteNombre { get; set; }
        public string Moneda { get; set; }
        public int? MesaNumero { get; set; }
        public string TipoPedido { get; set; }
        public string OrigenPedido { get; set; }
        public string RegistradoPor { get; set; }
        public string AtendidoPor { get; set; }
        public string DireccionEntrega { get; set; }
        public string ReferenciaEntrega { get; set; }
        public string TelefonoContacto { get; set; }
        public string TipoEntrega { get; set; }
    }
}
