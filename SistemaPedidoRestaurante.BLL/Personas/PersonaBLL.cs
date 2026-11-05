using System.Collections.Generic;
using SistemaPedidoRestaurante.DAL;
using SistemaPedidoRestaurante.Entities;

namespace SistemaPedidoRestaurante.BLL
{
    public class PersonaBLL
    {
        public List<Persona> ObtenerPersonas()
        {
            Permisos.ExigirRol(RolesSistema.Administrador);
            return new PersonaDAL().Listar(SesionActual.IdUsuario);
        }
    }
}
