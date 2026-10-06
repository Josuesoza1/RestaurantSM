using System;

namespace SistemaPedidoRestaurante.Entities
{
    public class Pedido
    {
        public int IdPedido { get; set; }
        public int IdCliente { get; set; }
        public int? IdMesa { get; set; }
        public int IdEmpleado { get; set; }
        public int? IdRepartidor { get; set; }
        public string Direccion { get; set; }
        public string Referencia { get; set; }
        public string TelefonoDeContacto { get; set; }
        public DateTime FechaHora { get; set; }
        public string Estado { get; set; }
        public bool EsDelivery { get; set; }
        public decimal CostoEnvio { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Total { get; set; }
    }
}