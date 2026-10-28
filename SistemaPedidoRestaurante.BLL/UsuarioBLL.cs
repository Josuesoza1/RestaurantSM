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
        public void RegistrarCliente(Cliente cliente, Usuario usuario, string confirmarContrasena)
        {
            Permisos.ExigirRol(RolesSistema.Administrador);
            _validacion.ValidarRegistro(usuario, confirmarContrasena);
            if (usuario.IdRol != RolesSistema.Cliente)
                throw new ArgumentException("El perfil de cliente debe tener el rol Cliente.");
            if (cliente == null || cliente.Id != 0 || usuario.IdUsuario != 0)
                throw new ArgumentException("Este formulario registra personas nuevas. Para modificar una, utilice su gestión.");

            cliente.Telefono = CamposValidacion.Telefono(cliente.Telefono, true);
            new ClienteBLL().InsertarCliente(cliente, usuario, confirmarContrasena);
        }

        public void RegistrarPersonal(Empleado empleado, Usuario usuario, string confirmarContrasena)
        {
            Permisos.ExigirRol(RolesSistema.Administrador);
            _validacion.ValidarRegistro(usuario, confirmarContrasena);
            if (usuario.IdRol != RolesSistema.Administrador && usuario.IdRol != RolesSistema.Empleado)
                throw new ArgumentException("El personal debe tener el rol Empleado o Administrador.");
            if (empleado == null || empleado.IdEmpleado != 0 || empleado.IdUsuario.HasValue || usuario.IdUsuario != 0)
                throw new ArgumentException("Este formulario registra personas nuevas. Para modificar una, utilice su gestión.");

            empleado.Activo = true;
            new EmpleadoBLL().GuardarEmpleado(empleado, usuario, true);
        }

        public Usuario IniciarSesion(string nombreUsuario, string contrasena)
        {
            _validacion.ValidarCredenciales(nombreUsuario, contrasena);
            Usuario usuarioLogin;
            try
            {
                usuarioLogin = _usuarioDAL.ValidarLogin(nombreUsuario, contrasena);
            }
            catch (IndexOutOfRangeException error)
            {
                throw new ArgumentException("Actualice la base con sql/011_permisos_mesero_cajero.sql antes de iniciar sesión.", error);
            }
            if (usuarioLogin == null)
            {
                throw new UnauthorizedAccessException("Usuario o contraseña incorrectos.");
            }

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
            if (string.IsNullOrWhiteSpace(filtro))
            {
                return usuarios;
            }

            filtro = filtro.Trim();
            Usuarios resultado = new Usuarios();
            var coincidencias = usuarios.Where(usuario =>
                BusquedaLINQ.Contiene(usuario.NombreUsuario, filtro) ||
                BusquedaLINQ.Contiene(usuario.NombreRol, filtro) ||
                BusquedaLINQ.Contiene(usuario.IdUsuario.ToString(), filtro));
            resultado.AddRange(coincidencias);
            return resultado;
        }
    }
}
