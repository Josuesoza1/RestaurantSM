using System;
using System.Data;
using System.Data.SqlClient;
using SistemaPedidoRestaurante.DAL;
using SistemaPedidoRestaurante.Validaciones;

namespace SistemaPedidoRestaurante.BLL
{
    public class PagoBLL
    {
        private readonly PagoDAL pagoDAL = new PagoDAL();

        public DataRow Cotizar(int pedido)
        {
            Permisos.ExigirCobro();
            if (pedido <= 0)
                throw new ArgumentException("Seleccione un pedido para cobrar.");
            try
            {
                DataTable tabla = pagoDAL.Cotizar(SesionActual.IdUsuario, pedido);
                if (tabla.Rows.Count != 1)
                    throw new ArgumentException("No se encontro el pedido.");
                return tabla.Rows[0];
            }
            catch (SqlException error)
            {
                throw OperacionDatos.Traducir(error, "No se pudo obtener el importe.");
            }
        }

        public DataRow Cobrar(int pedido, string metodo, decimal recibido, decimal total, Guid solicitud, string notas)
        {
            Permisos.ExigirCobro();
            notas = PagoValidacion.Validar(pedido, metodo, recibido, total, solicitud, notas);
            try
            {
                DataTable tabla = pagoDAL.Cobrar(SesionActual.IdUsuario, pedido, metodo, recibido, total, solicitud, notas);
                if (tabla.Rows.Count != 1)
                    throw new InvalidOperationException("No se recibió la confirmación del cobro. Consulte el pedido antes de intentarlo nuevamente.");
                return tabla.Rows[0];
            }
            catch (SqlException error)
            {
                throw OperacionDatos.Traducir(error, "La solicitud de cobro ya fue registrada.");
            }
        }
    }
}
