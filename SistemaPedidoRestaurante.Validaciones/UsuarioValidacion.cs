using System;

namespace SistemaPedidoRestaurante.Validaciones
{
    public class UsuarioValidacion
    {
        public void ValidarCredenciales(string nombreUsuario, string contrasena)
        {
            if (string.IsNullOrWhiteSpace(nombreUsuario))
                throw new ArgumentException("El campo de usuario no puede estar vacío.");

            if (string.IsNullOrWhiteSpace(contrasena))
                throw new ArgumentException("El campo de contraseña no puede estar vacío.");
        }
    }
}