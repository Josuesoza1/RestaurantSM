using System;
namespace SistemaPedidoRestaurante.BLL
{
    internal static class Permisos
    {
        internal static void ExigirRol(int rol)
        {
            if (!SesionActual.TieneRol(rol))
                throw new UnauthorizedAccessException("Su sesión no tiene permiso para esta operación.");
        }
    }
}
