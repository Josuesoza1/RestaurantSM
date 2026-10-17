using SistemaPedidoRestaurante.DAL;
using SistemaPedidoRestaurante.Entities;
using System.Linq;
using System;
using System.Data.SqlClient;
using SistemaPedidoRestaurante.Validaciones;

namespace SistemaPedidoRestaurante.BLL
{
    public class EmpleadoBLL
    {
        private readonly EmpleadoDAL _empleadoDAL = new EmpleadoDAL();
        private readonly EmpleadoValidacion _validacion = new EmpleadoValidacion();

        private void ExigirAdministrador()
        {
            if (!SesionActual.TieneRol(1))
                throw new UnauthorizedAccessException("Solo el administrador puede gestionar empleados.");
        }

        public void GuardarEmpleado(Empleado empleado, Usuario usuario, bool nuevo)
        {
            ExigirAdministrador();
            _validacion.Validar(empleado, usuario, nuevo || empleado == null || !empleado.IdUsuario.HasValue);
            if (!nuevo && empleado.IdEmpleado <= 0)
                throw new ArgumentException("Seleccione un empleado para editar.");
            if (empleado.IdUsuario == SesionActual.IdUsuario && (!empleado.Activo || usuario.IdRol != 1))
                throw new ArgumentException("No puede desactivar su propia cuenta ni quitarse el rol de administrador.");

            Ejecutar(() => _empleadoDAL.Guardar(empleado, usuario, SesionActual.IdUsuario, nuevo));
        }

        public void DesactivarEmpleado(int idEmpleado)
        {
            ExigirAdministrador();
            if (idEmpleado <= 0)
                throw new ArgumentException("Seleccione un empleado para desactivar.");
            Ejecutar(() => _empleadoDAL.Desactivar(idEmpleado, SesionActual.IdUsuario));
        }

        private void Ejecutar(Action operacion)
        {
            try { operacion(); }
            catch (SqlException ex) when (ex.Number == 2601 || ex.Number == 2627)
            {
                throw new ArgumentException("La cédula o el nombre de usuario ya están registrados.");
            }
            catch (SqlException ex) when (ex.Number >= 50000 && ex.Number < 51000)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        public Empleados ObtenerEmpleados()
        {
            return _empleadoDAL.Listar();
        }



        public Empleados BuscarEmpleados(string filtro)
        {
            Empleados empleados = _empleadoDAL.Listar();

            if (string.IsNullOrWhiteSpace(filtro))
                return empleados;

            filtro = filtro.Trim();

            var resultado = new Empleados();
            resultado.AddRange(empleados.Where(empleado =>
                BusquedaLINQ.Contiene(
                    string.Join(" ", new[]
                    {
                empleado.PrimerNombre,
                empleado.SegundoNombre,
                empleado.PrimerApellido,
                empleado.SegundoApellido
                    }.Where(nombre => !string.IsNullOrWhiteSpace(nombre))),
                    filtro) ||
                BusquedaLINQ.Contiene(empleado.Cargo, filtro) ||
                BusquedaLINQ.Contiene(empleado.Genero, filtro) ||
                BusquedaLINQ.Contiene(empleado.Cedula, filtro) ||
                BusquedaLINQ.Contiene(empleado.Telefono, filtro)));

            return resultado;
        }
    }
}
