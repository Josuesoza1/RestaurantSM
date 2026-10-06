using System.Collections.Generic;
using SistemaPedidoRestaurante.DAL;
using SistemaPedidoRestaurante.Entities;

namespace SistemaPedidoRestaurante.BLL
{
    public class PedidoBLL
    {
        private readonly PedidoDAL _pedidoDAL = new PedidoDAL();

        public List<Pedido> ObtenerPedidosAbiertos()
        {
            return _pedidoDAL.ListarAbiertos();
        }
    }
}