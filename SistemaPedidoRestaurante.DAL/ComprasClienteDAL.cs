using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using SistemaPedidoRestaurante.Entities;
namespace SistemaPedidoRestaurante.DAL
{
    public class ComprasClienteDAL
    {
        private readonly GestionDAL datos = new GestionDAL();
        public DataTable Listar(int actor) { return datos.Consultar("sp_Cliente_Pedidos", "@Actor", actor); }
        public DataTable Detalle(int actor, int pedido)
        { return datos.Consultar("sp_Cliente_Detalle", "@Actor", actor, "@IdPedido", pedido); }
        public int Guardar(int actor, int id, Guid solicitud, IEnumerable<LineaPedidoBorrador> lineas)
        {
            var xml = new XElement("pedido", lineas.Select(l => new XElement("item",
                new XAttribute("producto", l.IdProducto), new XAttribute("cantidad", l.Cantidad),
                new XAttribute("precio", l.PrecioUnitario))));
            return datos.Ejecutar("sp_Cliente_Pedido_Guardar", "@Actor", actor, "@IdPedido", id,
                "@Solicitud", solicitud, "@Detalle", xml.ToString(SaveOptions.DisableFormatting));
        }
        public int Cancelar(int actor, int id)
        { return datos.Ejecutar("sp_Cliente_Pedido_Cancelar", "@Actor", actor, "@IdPedido", id); }
    }
}
