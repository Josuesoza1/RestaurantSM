using System;

namespace SistemaPedidoRestaurante.BLL
{
    public static class BusquedaLINQ
    {
        public static bool Contiene(string valor, string filtro)
        {
            return !string.IsNullOrEmpty(valor) &&
                   valor.IndexOf(
                       filtro,
                       StringComparison.CurrentCultureIgnoreCase) >= 0;
        }
    }
}