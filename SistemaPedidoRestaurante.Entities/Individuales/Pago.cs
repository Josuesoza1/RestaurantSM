using System;

namespace SistemaPedidoRestaurante.Entities
{
    public class Pago
    {
        public int IdPago { get; set; }
        public int IdFactura { get; set; }
        public string MetodoDePago { get; set; }
        public decimal Monto { get; set; }
        public DateTime FechaPago { get; set; }
    }
}