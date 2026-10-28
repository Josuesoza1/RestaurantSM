using System;
using SistemaPedidoRestaurante.Entities;

namespace SistemaPedidoRestaurante.Validaciones
{
    public class UsuarioValidacion
    {
        public void ValidarRegistro(Usuario usuario, string confirmarContrasena)
        {
            if (usuario == null)
                throw new ArgumentException("Ingrese los datos de la cuenta.");

            ValidarCredenciales(usuario.NombreUsuario, usuario.Contrasena);
            usuario.NombreUsuario = CamposValidacion.Usuario(usuario.NombreUsuario);
            if (usuario.Contrasena != usuario.Contrasena.Trim())
                throw new ArgumentException("La contraseña no debe comenzar ni terminar con espacios.");
            if (usuario.Contrasena != confirmarContrasena)
                throw new ArgumentException("Las contraseñas no coinciden.");
            if (usuario.IdRol != RolesSistema.Administrador &&
                usuario.IdRol != RolesSistema.Empleado && usuario.IdRol != RolesSistema.Cliente)
                throw new ArgumentException("Seleccione Cliente, Empleado o Administrador.");
        }

        public void ValidarCredenciales(string nombreUsuario, string contrasena)
        {
            CamposValidacion.Texto(nombreUsuario, 50, "usuario", true);
            CamposValidacion.Texto(contrasena, 255, "contraseña", true);
            if (string.IsNullOrWhiteSpace(nombreUsuario))
            {
                throw new ArgumentException("El campo de usuario no puede estar vacío.");
            }

            if (string.IsNullOrWhiteSpace(contrasena))
            {
                throw new ArgumentException("El campo de contraseña no puede estar vacío.");
            }
        }
    }
}
