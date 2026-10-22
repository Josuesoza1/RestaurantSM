using System;
using System.Data;
using System.Linq;
using SistemaPedidoRestaurante.DAL;
using SistemaPedidoRestaurante.Entities;
using SistemaPedidoRestaurante.Validaciones;

namespace SistemaPedidoRestaurante.BLL
{
    public class ReservasClienteBLL
    {
        private readonly ReservasClienteDAL reservasDAL = new ReservasClienteDAL();
        public DataTable Listar() { Permisos.ExigirRol(3); return reservasDAL.Listar(SesionActual.IdUsuario); }
        public DataTable Buscar(DataTable t, string filtro) { Permisos.ExigirRol(3); return BusquedaLINQ.Filtrar(t, filtro); }
        public int Guardar(int id, int mesa, DateTime fecha, int personas)
        {
            Permisos.ExigirRol(3);
            GestionValidacion.Reserva(mesa,fecha,personas);
            return OperacionDatos.Ejecutar(() => reservasDAL.Guardar(SesionActual.IdUsuario,id,mesa,fecha,personas));
        }
        public void Cancelar(int id) { Permisos.ExigirRol(3); OperacionDatos.Ejecutar(() => reservasDAL.Cancelar(SesionActual.IdUsuario,id)); }
    }
}
