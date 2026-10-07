using SistemaPedidoRestaurante.DAL;
using SistemaPedidoRestaurante.Entities;
using System.Linq;

namespace SistemaPedidoRestaurante.BLL
{
    public class RolBLL
    {
        private readonly RolDAL _rolDAL = new RolDAL();

        public Roles ObtenerRoles()
        {
            return _rolDAL.Listar();
        }

        public Roles BuscarRoles(string filtro)
        {
            Roles roles = _rolDAL.Listar();

            if (string.IsNullOrWhiteSpace(filtro))
                return roles;

            filtro = filtro.Trim();

            var resultado = new Roles();
            resultado.AddRange(roles.Where(rol =>
                BusquedaLINQ.Contiene(rol.Nombre, filtro) ||
                BusquedaLINQ.Contiene(rol.Descripcion, filtro)));

            return resultado;
        }
    }
}