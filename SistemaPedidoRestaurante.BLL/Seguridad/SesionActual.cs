using System;
using SistemaPedidoRestaurante.Entities;

namespace SistemaPedidoRestaurante.BLL
{
    public static class SesionActual
    {
        public static int IdUsuario { get; private set; }
        public static int IdPersona { get; private set; }
        public static string NombreUsuario { get; private set; }
        public static int IdRol { get; private set; }
        public static string NombreRol { get; private set; }
        public static string Cargo { get; private set; }
        public static bool EstaAutenticado => IdUsuario > 0;

        public static void Iniciar(Usuario usuario)
        {
            Iniciar(usuario, usuario == null ? 0 : usuario.IdRol);
        }

        public static void Iniciar(Usuario usuario, int perfil)
        {
            if (usuario == null || usuario.IdUsuario <= 0 || string.IsNullOrWhiteSpace(usuario.NombreUsuario) || usuario.IdRol < 1 || usuario.IdRol > 3)
            {
                throw new UnauthorizedAccessException("El usuario no tiene un acceso válido.");
            }
            bool permitido = false;
            if (perfil == RolesSistema.Cliente)
                permitido = usuario.ClienteActivo;
            else if (perfil == usuario.IdRol && (perfil == RolesSistema.Administrador || perfil == RolesSistema.Empleado))
                permitido = usuario.AccesoPersonal;
            if (!permitido)
                throw new UnauthorizedAccessException("El perfil seleccionado no esta habilitado en su cuenta.");

            IdUsuario = usuario.IdUsuario;
            IdPersona = usuario.IdPersona;
            NombreUsuario = usuario.NombreUsuario;
            IdRol = perfil;
            NombreRol = RolesSistema.Nombre(perfil);
            Cargo = perfil == RolesSistema.Cliente ? null : CargosPersonal.Normalizar(usuario.Cargo);
        }

        public static bool TieneRol(int idRol)
        {
            return EstaAutenticado && IdRol == idRol;
        }

        public static void Cerrar()
        {
            IdUsuario = 0;
            IdPersona = 0;
            NombreUsuario = null;
            IdRol = 0;
            NombreRol = null;
            Cargo = null;
        }
    }
}
