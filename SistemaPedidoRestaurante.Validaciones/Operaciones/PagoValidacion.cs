using System;
using System.Globalization;

namespace SistemaPedidoRestaurante.Validaciones
{
    public static class PagoValidacion
    {
        public const decimal VueltoMaximo = 1000m;

        public static string NombreParaComprobante(string nombre)
        {
            nombre = CamposValidacion.Texto(nombre, 120, "nombre para el comprobante");
            if (nombre != null)
            {
                foreach (char caracter in nombre)
                    if (char.IsControl(caracter))
                        throw new ArgumentException("Escriba el nombre para el comprobante en una sola línea.");
            }
            return nombre;
        }

        public static bool IntentarLeerRecibido(string texto, out decimal recibido)
        {
            texto = (texto ?? "").Trim().Replace(',', '.');
            int separador = texto.IndexOf('.');
            return decimal.TryParse(texto, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out recibido)
                && recibido <= CamposValidacion.ImporteMaximo
                && (separador < 0 || texto.Length - separador - 1 <= 2);
        }

        public static string AvisoRangoEfectivo(decimal total)
        {
            decimal maximo = Math.Min(total + VueltoMaximo, CamposValidacion.ImporteMaximo);
            return "Ingrese entre C$ " + total.ToString("N2") + " y C$ " + maximo.ToString("N2") +
                " (hasta dos decimales).";
        }

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
                throw new ArgumentException(metodo == "Efectivo"
                    ? AvisoRangoEfectivo(total)
                    : "El monto recibido debe cubrir el total de C$ " + total.ToString("N2") + ".");
            }
            if (metodo != "Efectivo" && metodo != "Tarjeta" && metodo != "Transferencia")
            {
                throw new ArgumentException("Seleccione efectivo, tarjeta o transferencia.");
            }
            if (metodo != "Efectivo" && recibido != total)
            {
                throw new ArgumentException("El pago electrónico debe coincidir con el total.");
            }
            if (metodo == "Efectivo" && recibido - total > VueltoMaximo)
            {
                throw new ArgumentException("El vuelto máximo en efectivo es C$ " + VueltoMaximo.ToString("N2") +
                    ". " + AvisoRangoEfectivo(total));
            }
            return CamposValidacion.Texto(notas, 200, "notas");
        }
    }
}
