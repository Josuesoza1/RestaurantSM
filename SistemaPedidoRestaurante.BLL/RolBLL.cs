using SistemaPedidoRestaurante.DAL;
using SistemaPedidoRestaurante.Entities;
using System.Linq;
using SistemaPedidoRestaurante.Validaciones;

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
            Permisos.ExigirRol(1);
            var rol = new Rol { IdRol = id, Nombre = GestionValidacion.Texto(nombre, 50, "Nombre", true),
                Descripcion = GestionValidacion.Texto(descripcion, 150, "Descripción") };
            return OperacionDatos.Ejecutar(() => _rolDAL.Guardar(rol, SesionActual.IdUsuario));
        }

        public void Eliminar(int id)
        {
            Permisos.ExigirRol(1);
            if (id <= 3) throw new System.ArgumentException("Los tres roles del sistema están protegidos.");
            OperacionDatos.Ejecutar(() => _rolDAL.Eliminar(id, SesionActual.IdUsuario));
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
