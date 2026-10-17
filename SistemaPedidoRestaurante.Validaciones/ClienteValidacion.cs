using System;
using System.Text.RegularExpressions;
using SistemaPedidoRestaurante.Entities;

namespace SistemaPedidoRestaurante.Validaciones
{
    public class ClienteValidacion
    {
        public void ValidarRegistro(Cliente cliente, string contrasena, string confirmarContrasena)
        {
            if (string.IsNullOrWhiteSpace(cliente.PrimerNombre) || string.IsNullOrWhiteSpace(cliente.PrimerApellido))
                throw new ArgumentException("El primer nombre y el primer apellido son obligatorios.");

            if (string.IsNullOrWhiteSpace(cliente.Cedula))
                throw new ArgumentException("La cédula es obligatoria.");

            if (cliente.Genero != "M" && cliente.Genero != "F" && cliente.Genero != "O")
                throw new ArgumentException("Seleccione un género.");

            if (contrasena != confirmarContrasena)
                throw new ArgumentException("Las contraseñas no coinciden.");

            string patronEmail = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            if (!Regex.IsMatch(cliente.Email, patronEmail))
                throw new ArgumentException("El formato del correo electrónico no es válido.");
        }
    }
}
