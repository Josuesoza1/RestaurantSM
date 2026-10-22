using System;
using System.Data;
using System.Linq;
using SistemaPedidoRestaurante.DAL;
using SistemaPedidoRestaurante.Entities;
using SistemaPedidoRestaurante.Validaciones;

namespace SistemaPedidoRestaurante.BLL
{
    public class MesasGestionBLL
    {
        private readonly MesaDAL mesaDAL = new MesaDAL();

        public void Eliminar(int id)
        {
            Permisos.ExigirRol(1);
            if (id <= 0) throw new ArgumentException("Seleccione una mesa para eliminar.");
            OperacionDatos.Ejecutar(() => mesaDAL.Eliminar(id, SesionActual.IdUsuario));
        }
        public DataTable Listar(bool cliente = false)
        {
            Permisos.ExigirRol(cliente ? 3 : 1);
            var t = mesaDAL.ListarGestion();
            if (!cliente) return t;
            var filas = t.AsEnumerable().Where(r => r.Field<bool>("Activa") && new[] { "Disponible", "Reservada" }.Contains(r.Field<string>("Estado"))).ToArray();
            return filas.Length == 0 ? t.Clone() : filas.CopyToDataTable();
        }
        public DataTable Buscar(DataTable tabla, string filtro) { Permisos.ExigirRol(1); return BusquedaLINQ.Filtrar(tabla, filtro); }
        public int Guardar(int id, int numero, int capacidad, string estado, bool activa)
        {
            Permisos.ExigirRol(1);
            GestionValidacion.Mesa(numero,capacidad,estado);
            return OperacionDatos.Ejecutar(() => mesaDAL.Guardar(id, numero, capacidad, estado, activa, SesionActual.IdUsuario));
        }
    }
}
