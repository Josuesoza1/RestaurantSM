using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using SistemaPedidoRestaurante.DAL;
using SistemaPedidoRestaurante.Entities;
using SistemaPedidoRestaurante.Validaciones;

namespace SistemaPedidoRestaurante.BLL
{
    internal static class GestionReglas
    {
        internal static readonly GestionDAL Datos = new GestionDAL();
        internal static void Rol(int rol)
        {
            if (!SesionActual.TieneRol(rol)) throw new UnauthorizedAccessException("Su sesión no tiene permiso para esta operación.");
        }
        internal static string Texto(string valor, int maximo, string campo, bool requerido = false)
        {
            return GestionValidacion.Texto(valor,maximo,campo,requerido);
        }
        internal static int Escribir(string proc, params object[] valores)
        {
            try { return Datos.Ejecutar(proc, valores); }
            catch (SqlException e) when (e.Number == 2601 || e.Number == 2627)
            { throw new ArgumentException("Ya existe un registro con ese código, nombre o dato único."); }
            catch (SqlException e) when (e.Number == 1205)
            { throw new ArgumentException("Otra operación modificó estos datos al mismo tiempo. Recargue e inténtelo de nuevo."); }
            catch (SqlException e) when (e.Number == 547)
            { throw new ArgumentException("El registro tiene relaciones o valores incompatibles. No se realizó el cambio."); }
            catch (SqlException e) when (e.Number >= 50000)
            { throw new ArgumentException(e.Message); }
        }
        public static DataTable Filtrar(DataTable tabla, string filtro)
        {
            if (tabla == null) return null;
            var filas = tabla.AsEnumerable().Where(r => tabla.Columns.Cast<DataColumn>()
                .Where(c => c.DataType != typeof(byte[])).Any(c => BusquedaLINQ.Contiene(Convert.ToString(r[c]), (filtro ?? "").Trim()))).ToArray();
            return filas.Length == 0 ? tabla.Clone() : filas.CopyToDataTable();
        }
    }
}
