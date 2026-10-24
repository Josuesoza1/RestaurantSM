using System;
using System.Data;
using System.Linq;

namespace SistemaPedidoRestaurante.BLL
{
    public static class BusquedaLINQ
    {
        public static DataTable Filtrar(DataTable tabla, string filtro)
        {
            if (tabla == null)
            {
                return null;
            }

            filtro = (filtro ?? string.Empty).Trim();
            var filas = tabla.AsEnumerable().Where(fila => CoincideFila(fila, filtro)).ToArray();
            if (filas.Length == 0)
            {
                return tabla.Clone();
            }

            return filas.CopyToDataTable();
        }

        private static bool CoincideFila(DataRow fila, string filtro)
        {
            foreach (DataColumn columna in fila.Table.Columns)
            {
                // Las versiones binarias del inventario no son texto para buscar.
                if (columna.DataType == typeof(byte[]))
                {
                    continue;
                }

                string texto = Convert.ToString(fila[columna]);
                if (Contiene(texto, filtro))
                {
                    return true;
                }
            }

            return false;
        }

        public static bool Contiene(string valor, string filtro)
        {
            return !string.IsNullOrEmpty(valor) && valor.IndexOf(filtro, StringComparison.CurrentCultureIgnoreCase) >= 0;
        }
    }
}
