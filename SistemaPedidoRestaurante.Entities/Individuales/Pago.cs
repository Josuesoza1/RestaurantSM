using System;

namespace SistemaPedidoRestaurante.Entities
{
    public class Pago
    {
        public int IdPago { get; set; }
        public int IdFactura { get; set; }
        public int? IdUsuarioCobro { get; set; }
        public string MetodoDePago { get; set; }
        public decimal Monto { get; set; }
        public DateTime FechaPago { get; set; }
        public decimal? Recibido { get; set; }
        public decimal? Cambio { get; set; }
        public Guid? Solicitud { get; set; }
        public string Notas { get; set; }
    }
}
