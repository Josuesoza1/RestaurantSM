using System;

namespace SistemaPedidoRestaurante.Validaciones
{
    public static class PagoValidacion
    {
        public static string Validar(int pedido, string metodo, decimal recibido, decimal total, Guid solicitud, string notas)
        {
            if (pedido <= 0 || solicitud == Guid.Empty)
            {
                throw new ArgumentException("Seleccione un pedido para cobrar.");
            }
            CamposValidacion.Importe(total, "total", false);
            CamposValidacion.Importe(recibido, "monto recibido", true);
            if (recibido < total)
            {
                throw new ArgumentException("El monto recibido debe cubrir el total.");
            }
            if (metodo != "Efectivo" && metodo != "Tarjeta" && metodo != "Transferencia")
            {
                throw new ArgumentException("Seleccione efectivo, tarjeta o transferencia.");
            }
            if (metodo != "Efectivo" && recibido != total)
            {
                throw new ArgumentException("El pago electrónico debe coincidir con el total.");
            }
            return CamposValidacion.Texto(notas, 200, "notas");
        }
    }
}
