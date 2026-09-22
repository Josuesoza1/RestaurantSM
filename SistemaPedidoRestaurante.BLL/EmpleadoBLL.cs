using SistemaPedidoRestaurante.DAL;
using SistemaPedidoRestaurante.Entities;

namespace SistemaPedidoRestaurante.BLL
{
    public class EmpleadoBLL
    {
        private readonly EmpleadoDAL _empleadoDAL = new EmpleadoDAL();

        public Empleados ObtenerEmpleados()
        {
            return _empleadoDAL.Listar();
        }
    }
}