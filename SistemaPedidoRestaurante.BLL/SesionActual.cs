using System;
using SistemaPedidoRestaurante.Entities;

namespace SistemaPedidoRestaurante.BLL
{
    public static class SesionActual
    {
        public static int IdUsuario { get; private set; }
        public static string NombreUsuario { get; private set; }
        public static int IdRol { get; private set; }
        public static string NombreRol { get; private set; }
        public static bool EstaAutenticado => IdUsuario > 0;

        public static void Iniciar(Usuario usuario)
        {
            if (usuario == null || usuario.IdUsuario <= 0 || string.IsNullOrWhiteSpace(usuario.NombreUsuario) || usuario.IdRol < 1 || usuario.IdRol > 3)
            {
                throw new UnauthorizedAccessException("El usuario no tiene un acceso válido.");
            }

            IdUsuario = usuario.IdUsuario;
            NombreUsuario = usuario.NombreUsuario;
            IdRol = usuario.IdRol;
            NombreRol = RolesSistema.Nombre(usuario.IdRol);
        }

        public static bool TieneRol(int idRol)
        {
            return EstaAutenticado && IdRol == idRol;
        }

        public static void Cerrar()
        {
            IdUsuario = 0;
            NombreUsuario = null;
            IdRol = 0;
            NombreRol = null;
        }
    }
}
