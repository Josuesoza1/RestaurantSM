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
        public Usuario ObtenerAccesosActuales()
        {
            if (!SesionActual.EstaAutenticado) throw new UnauthorizedAccessException("Inicie sesion.");
            return _usuarioDAL.ConsultarAccesos(SesionActual.IdUsuario);
        }

        public Usuario CuentaParaPerfil(int persona, int rol)
        {
            Permisos.ExigirRol(RolesSistema.Administrador);
            return _usuarioDAL.CuentaParaPerfil(SesionActual.IdUsuario, persona, rol);
        }

        public void HabilitarCliente(int usuario, string correo, string telefono)
        {
            if (!SesionActual.EstaAutenticado || usuario != SesionActual.IdUsuario && !SesionActual.TieneRol(1))
                throw new UnauthorizedAccessException("Solo puede habilitar su perfil o gestionar perfiles como administrador.");
            correo = CamposValidacion.Correo(correo, true);
            telefono = CamposValidacion.Telefono(telefono, true);
            try { _usuarioDAL.HabilitarCliente(SesionActual.IdUsuario, usuario, correo, telefono); }
            catch (SqlException error) { throw OperacionDatos.Traducir(error, "El correo ya pertenece a otra persona."); }
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
        public void RegistrarCliente(Cliente cliente, Usuario usuario, string confirmarContrasena)
        {
            Permisos.ExigirRol(RolesSistema.Administrador);
            _validacion.ValidarRegistro(usuario, confirmarContrasena);
            if (usuario.IdRol != RolesSistema.Cliente)
                throw new ArgumentException("El perfil de cliente debe tener el rol Cliente.");
            if (cliente == null || cliente.IdCliente != 0 || usuario.IdUsuario != 0)
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
                throw new ArgumentException("No se pudieron leer los datos de la cuenta.", error);
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
                BusquedaLINQ.Contiene(usuario.PerfilesDisponibles, filtro) ||
                BusquedaLINQ.Contiene(usuario.IdUsuario.ToString(), filtro));
            resultado.AddRange(coincidencias);
            return resultado;
        }
    }
}
