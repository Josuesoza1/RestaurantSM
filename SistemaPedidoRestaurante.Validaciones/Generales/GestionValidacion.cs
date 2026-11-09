using System;
using SistemaPedidoRestaurante.Entities;

namespace SistemaPedidoRestaurante.Validaciones
{
    public static class GestionValidacion
    {
        public const int NumeroMesaMaximo = 999;
        public const int CapacidadMesaMaxima = 20;

        public static string Texto(string valor, int maximo, string campo, bool requerido = false)
        {
            return CamposValidacion.Texto(valor, maximo, campo, requerido);
        }

        public static void Cliente(Cliente cliente)
        {
            PersonaValidacion.Validar(cliente, false, false, false);
        }

        public static void Mesa(int numero, int capacidad, string estado)
        {
            if (numero < 1 || numero > NumeroMesaMaximo)
            {
                throw new ArgumentException("El número de mesa debe estar entre 1 y " + NumeroMesaMaximo + ".");
            }
            if (capacidad < 1 || capacidad > CapacidadMesaMaxima)
            {
                throw new ArgumentException("La capacidad debe estar entre 1 y " + CapacidadMesaMaxima + " personas.");
            }
            if (estado != "Disponible" && estado != "No disponible")
            {
                throw new ArgumentException("Seleccione un estado válido para la mesa.");
            }
        }

        public static void Inventario(int producto, int stock, int minimo)
        {
            if (producto <= 0 || stock < 0 || minimo < 0)
            {
                throw new ArgumentException("Seleccione un producto y cantidades enteras no negativas.");
            }
        }

        public static void Reserva(int mesa, DateTime fecha, int personas)
        {
            if (mesa <= 0 || fecha <= DateTime.Now || personas < 1 || personas > 20)
            {
                throw new ArgumentException("Seleccione una mesa, fecha futura y de 1 a 20 personas.");
            }
        }

        public static string MovimientoInventario(InventarioProducto producto, int cantidad, string tipo, string motivo)
        {
            if (producto == null || producto.IdProducto <= 0 || producto.Revision <= 0)
            {
                throw new ArgumentException("Seleccione un producto de inventario actualizado.");
            }
            if (cantidad <= 0 || (tipo != "Entrada" && tipo != "Salida"))
            {
                throw new ArgumentException("Seleccione entrada o salida y una cantidad positiva.");
            }
            return CamposValidacion.Texto(motivo, 200, "motivo", true);
        }
    }
}
