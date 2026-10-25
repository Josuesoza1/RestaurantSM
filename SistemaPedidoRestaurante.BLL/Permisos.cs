using System;

namespace SistemaPedidoRestaurante.BLL
{
    internal static class Permisos
    {
        internal static void ExigirPersonal()
        {
            if (!SesionActual.TieneRol(1) && !SesionActual.TieneRol(2))
            {
                throw new UnauthorizedAccessException("Esta operacion requiere una cuenta del personal.");
            }
        }
        internal static void ExigirRol(int rol)
        {
            if (!SesionActual.TieneRol(rol))
            {
                throw new UnauthorizedAccessException("Su sesión no tiene permiso para esta operación.");
            }
        }
    }
}
