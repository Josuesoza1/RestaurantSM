using System;
using System.Data;
using System.Linq;
using SistemaPedidoRestaurante.DAL;
using SistemaPedidoRestaurante.Entities;
using SistemaPedidoRestaurante.Validaciones;
using System.Data.SqlClient;

namespace SistemaPedidoRestaurante.BLL
{
    public class ReservasClienteBLL
    {
        private readonly ReservasClienteDAL reservasDAL = new ReservasClienteDAL();
        public DataTable Listar()
        {
            Permisos.ExigirRol(3);
            return reservasDAL.Listar(SesionActual.IdUsuario);
        }

        public DataTable Buscar(DataTable t, string filtro)
        {
            Permisos.ExigirRol(3);
            return BusquedaLINQ.Filtrar(t, filtro);
        }

        public int Guardar(int id, int mesa, DateTime fecha, int personas)
        {
            Permisos.ExigirRol(3);
            GestionValidacion.Reserva(mesa, fecha, personas);
            try
            {
                return reservasDAL.Guardar(SesionActual.IdUsuario, id, mesa, fecha, personas);
            }
            catch (SqlException error)
            {
                throw OperacionDatos.Traducir(error, "Ya existe un registro con ese código, nombre o dato único.");
            }
        }

        public void Cancelar(int id)
        {
            Permisos.ExigirRol(3);
            try
            {
                reservasDAL.Cancelar(SesionActual.IdUsuario, id);
            }
            catch (SqlException error)
            {
                throw OperacionDatos.Traducir(error, "Ya existe un registro con ese código, nombre o dato único.");
            }
        }
    }
}
