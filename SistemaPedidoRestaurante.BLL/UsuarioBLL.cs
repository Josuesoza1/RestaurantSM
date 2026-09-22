using System;
using SistemaPedidoRestaurante.DAL;
using SistemaPedidoRestaurante.Entities;
using SistemaPedidoRestaurante.Validaciones;

namespace SistemaPedidoRestaurante.BLL
{
    public class UsuarioBLL
    {
        private readonly UsuarioDAL _usuarioDAL = new UsuarioDAL();
        private readonly UsuarioValidacion _validacion = new UsuarioValidacion();

        public Usuario IniciarSesion(string nombreUsuario, string contrasena)
        {
            _validacion.ValidarCredenciales(nombreUsuario, contrasena);

            Usuario usuarioLogin = _usuarioDAL.ValidarLogin(nombreUsuario, contrasena);

            if (usuarioLogin == null)
                throw new UnauthorizedAccessException("Usuario o contraseña incorrectos.");

            return usuarioLogin;
        }

        


        public Usuarios ObtenerTodosLosUsuarios()
        {
            return _usuarioDAL.Listar();
        }
    }
}