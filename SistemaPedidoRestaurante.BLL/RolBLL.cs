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

        public int Guardar(int id, string nombre, string descripcion)
        {
            GestionReglas.Rol(1);
            return GestionReglas.Escribir("sp_Rol_Guardar", "@Actor", SesionActual.IdUsuario, "@Id", id,
                "@Nombre", GestionReglas.Texto(nombre, 50, "Nombre", true),
                "@Descripcion", GestionReglas.Texto(descripcion, 150, "Descripción"));
        }

        public void Eliminar(int id)
        {
            GestionReglas.Rol(1);
            if (id <= 3) throw new System.ArgumentException("Los tres roles del sistema están protegidos.");
            GestionReglas.Escribir("sp_Rol_Eliminar", "@Actor", SesionActual.IdUsuario, "@Id", id);
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
