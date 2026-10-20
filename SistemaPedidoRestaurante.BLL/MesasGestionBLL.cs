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
    public class MesasGestionBLL
    {
        public DataTable Listar(bool cliente = false)
        {
            GestionReglas.Rol(cliente ? 3 : 1);
            var t = GestionReglas.Datos.Consultar("sp_Mesa_Listar");
            if (!cliente) return t;
            var filas = t.AsEnumerable().Where(r => r.Field<bool>("Activa") && new[] { "Disponible", "Reservada" }.Contains(r.Field<string>("Estado"))).ToArray();
            return filas.Length == 0 ? t.Clone() : filas.CopyToDataTable();
        }
        public DataTable Buscar(DataTable tabla, string filtro) { GestionReglas.Rol(1); return GestionReglas.Filtrar(tabla, filtro); }
        public int Guardar(int id, int numero, int capacidad, string estado, bool activa)
        {
            GestionReglas.Rol(1);
            GestionValidacion.Mesa(numero,capacidad,estado);
            return GestionReglas.Escribir("sp_Mesa_Guardar", "@Actor", SesionActual.IdUsuario, "@Id", id,
                "@Numero", numero, "@Capacidad", capacidad, "@Estado", estado, "@Activa", activa);
        }
    }
}
