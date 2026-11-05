using System;
using SistemaPedidoRestaurante.Entities;

namespace SistemaPedidoRestaurante.BLL
{
    public static class Permisos
    {
        public static bool PuedeAtender
        {
            get { return SesionActual.TieneRol(1) || SesionActual.TieneRol(2) && SesionActual.Cargo == CargosPersonal.Mesero; }
        }

        public static bool PuedeCobrar
        {
            get { return SesionActual.TieneRol(1) || SesionActual.TieneRol(2) && SesionActual.Cargo == CargosPersonal.Cajero; }
        }

        public static bool PuedeConsultarPedidos
        {
            get { return PuedeAtender || PuedeCobrar; }
        }

        public static void ExigirAtencion()
        {
            if (!PuedeAtender)
                throw new UnauthorizedAccessException("Esta operación requiere un mesero o administrador.");
        }

        public static void ExigirCobro()
        {
            if (!PuedeCobrar)
                throw new UnauthorizedAccessException("Esta operación requiere un cajero o administrador.");
        }

        internal static void ExigirPersonal()
        {
            if (!PuedeConsultarPedidos)
            {
                throw new UnauthorizedAccessException("Su cuenta necesita un cargo habilitado: Mesero o Cajero. Solicite al administrador que revise su registro.");
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
