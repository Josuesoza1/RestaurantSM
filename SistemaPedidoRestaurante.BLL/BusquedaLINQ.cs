using System;

using System.Data;
using System.Linq;

namespace SistemaPedidoRestaurante.BLL
{
    public static class BusquedaLINQ
    {
        public static DataTable Filtrar(DataTable tabla, string filtro)
        {
            if (tabla == null) return null;
            filtro = (filtro ?? string.Empty).Trim();
            var filas = tabla.AsEnumerable().Where(fila => tabla.Columns.Cast<DataColumn>()
                .Where(columna => columna.DataType != typeof(byte[]))
                .Any(columna => Contiene(Convert.ToString(fila[columna]), filtro))).ToArray();
            return filas.Length == 0 ? tabla.Clone() : filas.CopyToDataTable();
        }
        public static bool Contiene(string valor, string filtro)
        {
            return !string.IsNullOrEmpty(valor) &&
                   valor.IndexOf(
                       filtro,
                       StringComparison.CurrentCultureIgnoreCase) >= 0;
        }
    }
}
