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
            return CamposValidacion.Texto(valor,maximo,campo,requerido);
        }
        public static void Cliente(Cliente cliente)
        {
            if (cliente == null) throw new ArgumentException("Ingrese los datos del cliente.");
            cliente.PrimerNombre=CamposValidacion.NombrePersona(cliente.PrimerNombre,"primer nombre",true);
            cliente.SegundoNombre=CamposValidacion.NombrePersona(cliente.SegundoNombre,"segundo nombre",false);
            cliente.PrimerApellido=CamposValidacion.NombrePersona(cliente.PrimerApellido,"primer apellido",true);
            cliente.SegundoApellido=CamposValidacion.NombrePersona(cliente.SegundoApellido,"segundo apellido",false);
            cliente.Cedula=CamposValidacion.Cedula(cliente.Cedula,false);
            cliente.Telefono=CamposValidacion.Telefono(cliente.Telefono,false);
            cliente.Email=CamposValidacion.Correo(cliente.Email,false);
            if (!new[] { "M", "F", "O" }.Contains(cliente.Genero)) throw new ArgumentException("Seleccione el género.");
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
