using System;
using SistemaPedidoRestaurante.DAL;
using SistemaPedidoRestaurante.Entities;
using System.Linq;
using SistemaPedidoRestaurante.Validaciones;
using System.Data.SqlClient;

namespace SistemaPedidoRestaurante.BLL
{
    public class UsuarioBLL
    {
        private readonly UsuarioDAL _usuarioDAL = new UsuarioDAL();
        private readonly UsuarioValidacion _validacion = new UsuarioValidacion();
        public Usuario CuentaParaPerfil(int persona, int rol)
        {
            Permisos.ExigirRol(RolesSistema.Administrador);
            return _usuarioDAL.CuentaParaPerfil(SesionActual.IdUsuario, persona, rol);
        }

        public void AgregarPerfilPersonal(Empleado empleado, Usuario cuenta)
        {
            Permisos.ExigirRol(1);
            if (empleado == null || empleado.IdPersona <= 0 || cuenta == null || cuenta.IdUsuario <= 0)
                throw new ArgumentException("Seleccione una persona con cuenta de cliente existente.");
            new EmpleadoValidacion().Validar(empleado, cuenta, false);
            empleado.Activo = true;
            try { new EmpleadoDAL().Guardar(empleado, cuenta, SesionActual.IdUsuario, true, cuenta.IdUsuario); }
            catch (SqlException error) { throw OperacionDatos.Traducir(error, "Ya existe una cuenta o perfil con esos datos."); }
        }
        public void RegistrarPersonal(Empleado empleado, Usuario usuario, string confirmarContrasena)
        {
            Permisos.ExigirRol(RolesSistema.Administrador);
            _validacion.ValidarRegistro(usuario, confirmarContrasena);
            if (usuario.IdRol != RolesSistema.Administrador && usuario.IdRol != RolesSistema.Empleado)
                throw new ArgumentException("El personal debe tener el rol Empleado o Administrador.");
            if (empleado == null || empleado.IdEmpleado != 0 || empleado.IdUsuario.HasValue || usuario.IdUsuario != 0)
                throw new ArgumentException("Para editar una cuenta de personal, utilice Empleados y acceso.");

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
                throw new ArgumentException("No se pudieron leer los datos de la cuenta.", error);
            }
            if (usuarioLogin == null || !usuarioLogin.AccesoPersonal ||
                (usuarioLogin.IdRol != RolesSistema.Administrador && usuarioLogin.IdRol != RolesSistema.Empleado))
            {
                throw new UnauthorizedAccessException("Usuario o contraseña incorrectos.");
            }

            return usuarioLogin;
        }

        public Usuarios ObtenerTodosLosUsuarios()
        {
            Permisos.ExigirRol(1);
            return _usuarioDAL.Listar(SesionActual.IdUsuario);
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
                BusquedaLINQ.Contiene(usuario.EstadoAcceso, filtro) ||
                BusquedaLINQ.Contiene(usuario.IdUsuario.ToString(), filtro));
            resultado.AddRange(coincidencias);
            return resultado;
        }
    }
}
