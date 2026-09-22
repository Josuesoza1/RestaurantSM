using SistemaPedidoRestaurante.DAL;
using SistemaPedidoRestaurante.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaPedidoRestaurante.BLL
{
    public class ClienteBLL
    {
        private readonly ClienteDAL _clienteDAL = new ClienteDAL();

        public void InsertarCliente(Cliente cliente, Usuario usuario)
        {
            _clienteDAL.InsertarCliente(cliente, usuario);
        }
    }
}
