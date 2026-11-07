using System;
using SistemaPedidoRestaurante.Entities;

namespace SistemaPedidoRestaurante.BLL
{
    public static class Permisos
    {
        public static bool PuedeAtender
        {
            get { return SesionActual.TieneRol(1) || SesionActual.TieneRol(2) && SesionActual.Cargo == CargosPersonal.Despachador; }
        }

        public static bool PuedeCobrar
        {
            get { return PuedeAtender; }
        }

        public static bool PuedeConsultarPedidos
        {
            get { return PuedeAtender; }
        }

        public static bool PuedeVerHistorial => SesionActual.TieneRol(RolesSistema.Administrador);

        public static void ExigirAtencion()
        {
            if (!PuedeAtender)
                throw new UnauthorizedAccessException("Esta operación requiere un despachador o administrador.");
        }

        public static void ExigirCobro()
        {
            if (!PuedeCobrar)
                throw new UnauthorizedAccessException("Esta operación requiere un despachador o administrador.");
        }

        internal static void ExigirPersonal()
        {
            if (!PuedeConsultarPedidos)
            {
                throw new UnauthorizedAccessException("Su cuenta necesita el cargo de Despachador.");
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
