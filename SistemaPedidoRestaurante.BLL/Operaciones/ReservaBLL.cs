using System;
using System.Data;
using System.Data.SqlClient;
using SistemaPedidoRestaurante.DAL;

namespace SistemaPedidoRestaurante.BLL
{
    public class ReservaBLL
    {
        private readonly ReservaDAL reservasDAL = new ReservaDAL();

        public DataTable ListarReservas()
        {
            Permisos.ExigirRol(1);
            try
            {
                return reservasDAL.ListarReservas(SesionActual.IdUsuario);
            }
            catch (SqlException error)
            {
                throw OperacionDatos.Traducir(error, "No se pudo consultar el listado.");
            }
        }

    }
}
