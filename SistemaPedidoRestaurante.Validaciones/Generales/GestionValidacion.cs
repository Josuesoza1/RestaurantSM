using System;
using SistemaPedidoRestaurante.Entities;

namespace SistemaPedidoRestaurante.Validaciones
{
    public static class GestionValidacion
    {
        public const int NumeroMesaMaximo = 999;
        public const int CapacidadMesaMaxima = 20;
        public const int StockMaximoManual = 10000;

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
            if (producto <= 0)
                throw new ArgumentException("Seleccione un producto para el inventario.");
            if (stock < 0 || stock > StockMaximoManual || minimo < 0 || minimo > StockMaximoManual)
                throw new ArgumentException("El stock actual y el mínimo deben ser números enteros entre 0 y " +
                    StockMaximoManual + " unidades.");
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
            if (producto == null || producto.IdProducto <= 0 || producto.Revision <= 0 || !producto.Activo)
            {
                throw new ArgumentException("Seleccione un producto con inventario activo.");
            }

            int maximo = MaximoMovimientoInventario(producto.StockActual, tipo);
            if (maximo == 0)
            {
                throw new ArgumentException(tipo == "Salida"
                    ? "Este producto tiene 0 unidades para retirar."
                    : "El producto tiene " + StockMaximoManual + " unidades o más. Solo puede registrar salidas.");
            }
            if (cantidad < 1 || cantidad > maximo)
                throw new ArgumentException("Ingrese una cantidad entera de 1 a " + maximo + " unidades.");

            return CamposValidacion.Texto(motivo, 200, "motivo", true);
        }

        public static int MaximoMovimientoInventario(int stockActual, string tipo)
        {
            if (tipo == "Entrada")
                return Math.Max(0, StockMaximoManual - stockActual);
            if (tipo == "Salida")
                return stockActual;

            throw new ArgumentException("Seleccione Entrada o Salida.");
        }
    }
}
