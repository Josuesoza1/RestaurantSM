using SistemaPedidoRestaurante.DAL;
using SistemaPedidoRestaurante.Entities;
using System.Linq;
using SistemaPedidoRestaurante.Validaciones;
using System.Data.SqlClient;

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
            Rol rol = new Rol
            {
                IdRol = id,
                Nombre = GestionValidacion.Texto(nombre, 50, "Nombre", true),
                Descripcion = GestionValidacion.Texto(descripcion, 150, "Descripción")
            };
            try
            {
                return _rolDAL.Guardar(rol, SesionActual.IdUsuario);
            }
            catch (SqlException error)
            {
                throw OperacionDatos.Traducir(error, "Ya existe un registro con ese código, nombre o dato único.");
            }
        }

        public void Eliminar(int id)
        {
            Permisos.ExigirRol(1);
            if (id <= 3)
            {
                throw new System.ArgumentException("Los tres roles del sistema están protegidos.");
            }

            try
            {
                _rolDAL.Eliminar(id, SesionActual.IdUsuario);
            }
            catch (SqlException error)
            {
                throw OperacionDatos.Traducir(error, "Ya existe un registro con ese código, nombre o dato único.");
            }
        }

        public Roles BuscarRoles(string filtro)
        {
            Roles roles = _rolDAL.Listar();
            if (string.IsNullOrWhiteSpace(filtro))
            {
                return roles;
            }

            filtro = filtro.Trim();
            Roles resultado = new Roles();
            resultado.AddRange(roles.Where(rol => BusquedaLINQ.Contiene(rol.Nombre, filtro) || BusquedaLINQ.Contiene(rol.Descripcion, filtro)));
            return resultado;
        }
    }
}
