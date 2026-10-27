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
            Permisos.ExigirPersonal();
            try
            {
                return reservasDAL.ListarReservas(SesionActual.IdUsuario);
            }
            catch (SqlException error)
            {
                throw OperacionDatos.Traducir(error, "No se pudo consultar el listado.");
            }
        }

        public void CambiarReserva(int reserva, string anterior, string nuevo)
        {
            Permisos.ExigirPersonal();
            if (reserva <= 0)
                throw new ArgumentException("Seleccione una reserva.");
            if (anterior != "Pendiente" && anterior != "Confirmada")
                throw new ArgumentException("La reserva ya está cerrada. Recargue el listado.");
            if (nuevo != "Atendida" && nuevo != "No asistio" && nuevo != "Cancelada")
                throw new ArgumentException("Seleccione una acción de reserva válida.");
            try
            {
                reservasDAL.CambiarEstadoReserva(SesionActual.IdUsuario, reserva, anterior, nuevo);
            }
            catch (SqlException error)
            {
                throw OperacionDatos.Traducir(error, "No se pudo actualizar la reserva.");
            }
        }
    }
}
