using System;
using SistemaPedidoRestaurante.DAL;
using SistemaPedidoRestaurante.Entities;
using System.Linq;
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
            Permisos.ExigirRol(1);
            return _usuarioDAL.Listar();
        }

        public Usuarios BuscarUsuarios(string filtro)
        {
            Usuarios usuarios = ObtenerTodosLosUsuarios();
            if (string.IsNullOrWhiteSpace(filtro)) return usuarios;
            filtro = filtro.Trim();
            var resultado = new Usuarios();
            resultado.AddRange(usuarios.Where(usuario =>
                BusquedaLINQ.Contiene(usuario.NombreUsuario, filtro) ||
                BusquedaLINQ.Contiene(usuario.NombreRol, filtro) ||
                BusquedaLINQ.Contiene(usuario.IdUsuario.ToString(), filtro)));
            return resultado;
        }
    }
}
