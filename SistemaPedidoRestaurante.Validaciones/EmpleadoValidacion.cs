using System;
using SistemaPedidoRestaurante.Entities;

namespace SistemaPedidoRestaurante.Validaciones
{
    public class EmpleadoValidacion
    {
        public void Validar(Empleado empleado, Usuario usuario, bool requiereContrasena)
        {
            if (empleado == null || usuario == null)
                throw new ArgumentException("Faltan los datos del empleado y su cuenta.");

            ValidarTexto(empleado.PrimerNombre, "primer nombre", 80, true);
            ValidarTexto(empleado.SegundoNombre, "segundo nombre", 80, false);
            ValidarTexto(empleado.PrimerApellido, "primer apellido", 80, true);
            ValidarTexto(empleado.SegundoApellido, "segundo apellido", 80, false);
            ValidarTexto(empleado.Cargo, "cargo", 50, true);
            ValidarTexto(empleado.Cedula, "cédula", 30, true);
            ValidarTexto(empleado.Telefono, "teléfono", 20, true);
            ValidarTexto(usuario.NombreUsuario, "usuario", 50, true);
            ValidarTexto(usuario.Contrasena, "contraseña", 255, requiereContrasena);

            if (empleado.Genero != "M" && empleado.Genero != "F" && empleado.Genero != "O")
                throw new ArgumentException("Seleccione un género.");
            if (usuario.IdRol != 1 && usuario.IdRol != 2)
                throw new ArgumentException("Seleccione Administrador o Empleado como rol de acceso.");
        }

        private void ValidarTexto(string valor, string campo, int longitud, bool obligatorio)
        {
            if (obligatorio && string.IsNullOrWhiteSpace(valor))
                throw new ArgumentException("El campo " + campo + " es obligatorio.");
            if (valor != null && valor.Length > longitud)
                throw new ArgumentException("El campo " + campo + " admite hasta " + longitud + " caracteres.");
        }
    }
}
