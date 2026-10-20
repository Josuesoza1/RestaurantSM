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
    public class InventarioBLL
    {
        public DataTable Listar() { GestionReglas.Rol(1); return GestionReglas.Datos.Consultar("sp_Inventario_Listar", "@Actor", SesionActual.IdUsuario); }
        public DataTable Buscar(DataTable tabla, string filtro, bool soloBajos = false)
        {
            GestionReglas.Rol(1);
            var t = GestionReglas.Filtrar(tabla, filtro);
            if (!soloBajos) return t;
            var filas = t.AsEnumerable().Where(r => r.Field<bool>("Activo") && r.Field<int>("StockActual") <= r.Field<int>("StockMinimo")).ToArray();
            return filas.Length == 0 ? t.Clone() : filas.CopyToDataTable();
        }
        public void Guardar(int producto, int stock, int minimo, bool activo, byte[] version)
        {
            GestionReglas.Rol(1);
            GestionValidacion.Inventario(producto,stock,minimo);
            GestionReglas.Escribir("sp_Inventario_Guardar", "@Actor", SesionActual.IdUsuario, "@IdProducto", producto,
                "@Stock", stock, "@Minimo", minimo, "@Activo", activo, "@Version", version);
        }
    }
}
