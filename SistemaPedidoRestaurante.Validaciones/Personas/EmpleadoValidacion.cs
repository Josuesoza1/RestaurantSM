using System;
using SistemaPedidoRestaurante.Entities;

namespace SistemaPedidoRestaurante.Validaciones
{
    public class EmpleadoValidacion
    {
        public void Validar(Empleado empleado, Usuario usuario, bool requiereContrasena)
        {
            if (empleado == null || usuario == null)
            {
                throw new ArgumentException("Faltan los datos del empleado y su cuenta.");
            }

            PersonaValidacion.Validar(empleado, true, true, false);
            ValidarTexto(empleado.Cargo, "cargo", 50, true);
            usuario.NombreUsuario = CamposValidacion.Usuario(usuario.NombreUsuario);
            ValidarTexto(usuario.Contrasena, "contraseña", 255, requiereContrasena);
            if (usuario.IdRol != 1 && usuario.IdRol != 2)
            {
                throw new ArgumentException("Seleccione Administrador o Empleado como rol de acceso.");
            }

            empleado.Cargo = CargosPersonal.Normalizar(empleado.Cargo);
            if (usuario.IdRol == RolesSistema.Empleado &&
                empleado.Cargo != CargosPersonal.Despachador)
                throw new ArgumentException("Seleccione Despachador como cargo del empleado.");
        }

        private void ValidarTexto(string valor, string campo, int longitud, bool obligatorio)
        {
            CamposValidacion.Texto(valor, longitud, campo, obligatorio);
        }
    }
}
