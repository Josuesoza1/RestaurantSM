using System;

namespace SistemaPedidoRestaurante.Entities
{
    public class Reservacion
    {
        public int IdReservacion { get; set; }
        public int IdCliente { get; set; }
        public int IdMesa { get; set; }
        public DateTime FechaHoraReserva { get; set; }
        public int CantidadPersonas { get; set; }
        public string Estado { get; set; }
    }
}