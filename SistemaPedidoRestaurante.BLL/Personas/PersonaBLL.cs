using System.Collections.Generic;
using SistemaPedidoRestaurante.DAL;
using SistemaPedidoRestaurante.Entities;

namespace SistemaPedidoRestaurante.BLL
{
    public class PersonaBLL
    {
        public List<Persona> ObtenerPersonasParaAlta()
        {
            Permisos.ExigirRol(RolesSistema.Administrador);
            var personas = new PersonaDAL().Listar(SesionActual.IdUsuario);
            var personasConPerfil = new HashSet<int>();
            foreach (Empleado empleado in new EmpleadoBLL().ObtenerEmpleados())
            {
                if (empleado.IdUsuario.HasValue || !empleado.Activo)
                    personasConPerfil.Add(empleado.IdPersona);
            }

            foreach (Usuario cuenta in new UsuarioBLL().ObtenerTodosLosUsuarios())
            {
                if (cuenta.IdRol == RolesSistema.Administrador || cuenta.IdRol == RolesSistema.Empleado)
                    personasConPerfil.Add(cuenta.IdPersona);
            }

            personas.RemoveAll(persona => personasConPerfil.Contains(persona.IdPersona));
            return personas;
        }
    }
}
