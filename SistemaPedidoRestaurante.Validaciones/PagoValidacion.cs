using System;

namespace SistemaPedidoRestaurante.Validaciones
{
    public static class PagoValidacion
    {
        public static string Validar(int pedido, string metodo, decimal recibido, decimal total, Guid solicitud, string notas)
        {
            if (pedido <= 0 || solicitud == Guid.Empty)
                throw new ArgumentException("Seleccione un pedido para cobrar.");
            if (total <= 0 || total > 99999999.99m || decimal.Round(total, 2) != total)
                throw new ArgumentException("El total debe ser positivo y tener hasta dos decimales.");
            if (recibido < total || recibido > 99999999.99m || decimal.Round(recibido, 2) != recibido)
                throw new ArgumentException("El monto recibido debe cubrir el total y tener hasta dos decimales.");
            if (metodo != "Efectivo" && metodo != "Tarjeta" && metodo != "Transferencia")
                throw new ArgumentException("Seleccione efectivo, tarjeta o transferencia.");
            if (metodo != "Efectivo" && recibido != total)
                throw new ArgumentException("El pago electrónico debe coincidir con el total.");
            return CamposValidacion.Texto(notas, 200, "notas");
        }
    }
}
