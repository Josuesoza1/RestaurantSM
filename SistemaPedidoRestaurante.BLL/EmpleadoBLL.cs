using SistemaPedidoRestaurante.DAL;
using SistemaPedidoRestaurante.Entities;
using System.Linq;

namespace SistemaPedidoRestaurante.BLL
{
    public class EmpleadoBLL
    {
        private readonly EmpleadoDAL _empleadoDAL = new EmpleadoDAL();

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