using System.Collections.Generic;
using SistemaPedidoRestaurante.DAL;
using SistemaPedidoRestaurante.Entities;

namespace SistemaPedidoRestaurante.BLL
{
    public class MesaBLL
    {
        private readonly MesaDAL _mesaDAL = new MesaDAL();

        public List<Mesa> ObtenerMesas()
        {
            return _mesaDAL.Listar();
        }
    }
}