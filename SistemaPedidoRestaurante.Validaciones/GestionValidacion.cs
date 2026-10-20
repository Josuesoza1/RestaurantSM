using System;
using System.Linq;
using System.Text.RegularExpressions;
using SistemaPedidoRestaurante.Entities;

namespace SistemaPedidoRestaurante.Validaciones
{
    public static class GestionValidacion
    {
        public static string Texto(string valor, int maximo, string campo, bool requerido = false)
        {
            valor = (valor ?? "").Trim();
            if ((requerido && valor.Length == 0) || valor.Length > maximo)
                throw new ArgumentException(campo + ": revise el valor (máximo " + maximo + " caracteres).");
            return valor.Length == 0 ? null : valor;
        }
        public static void Cliente(Cliente cliente)
        {
            if (cliente == null) throw new ArgumentException("Ingrese los datos del cliente.");
            if (!new[] { "M", "F", "O" }.Contains(cliente.Genero)) throw new ArgumentException("Seleccione el género.");
            string correo = Texto(cliente.Email, 120, "Correo");
            if (correo != null && !Regex.IsMatch(correo, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                throw new ArgumentException("El correo no es válido.");
        }
        public static void Mesa(int numero, int capacidad, string estado)
        {
            if (numero < 1 || capacidad < 1 || capacidad > 20 || !new[] { "Disponible", "Ocupada", "Reservada", "En limpieza" }.Contains(estado))
                throw new ArgumentException("Revise número, capacidad (1 a 20) y estado de la mesa.");
        }
        public static void Inventario(int producto, int stock, int minimo)
        {
            if (producto <= 0 || stock < 0 || minimo < 0)
                throw new ArgumentException("Seleccione un producto y cantidades enteras no negativas.");
        }
        public static void Reserva(int mesa, DateTime fecha, int personas)
        {
            if (mesa <= 0 || fecha <= DateTime.Now || personas < 1 || personas > 20)
                throw new ArgumentException("Seleccione una mesa, fecha futura y de 1 a 20 personas.");
        }
    }
}
