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

            empleado.PrimerNombre = CamposValidacion.NombrePersona(empleado.PrimerNombre, "primer nombre", true);
            empleado.SegundoNombre = CamposValidacion.NombrePersona(empleado.SegundoNombre, "segundo nombre", false);
            empleado.PrimerApellido = CamposValidacion.NombrePersona(empleado.PrimerApellido, "primer apellido", true);
            empleado.SegundoApellido = CamposValidacion.NombrePersona(empleado.SegundoApellido, "segundo apellido", false);
            ValidarTexto(empleado.Cargo, "cargo", 50, true);
            empleado.Cedula = CamposValidacion.Cedula(empleado.Cedula, true);
            empleado.Telefono = CamposValidacion.Telefono(empleado.Telefono, true);
            usuario.NombreUsuario = CamposValidacion.Usuario(usuario.NombreUsuario);
            ValidarTexto(usuario.Contrasena, "contraseña", 255, requiereContrasena);
            if (empleado.Genero != "M" && empleado.Genero != "F" && empleado.Genero != "O")
            {
                throw new ArgumentException("Seleccione un género.");
            }

            if (usuario.IdRol != 1 && usuario.IdRol != 2)
            {
                throw new ArgumentException("Seleccione Administrador o Empleado como rol de acceso.");
            }

            empleado.Cargo = CargosPersonal.Normalizar(empleado.Cargo);
            if (usuario.IdRol == RolesSistema.Empleado &&
                empleado.Cargo != CargosPersonal.Mesero && empleado.Cargo != CargosPersonal.Cajero)
                throw new ArgumentException("Seleccione Mesero o Cajero como cargo del empleado.");
        }

        private void ValidarTexto(string valor, string campo, int longitud, bool obligatorio)
        {
            CamposValidacion.Texto(valor, longitud, campo, obligatorio);
        }
    }
}
