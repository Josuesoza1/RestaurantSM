using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using SistemaPedidoRestaurante.DAL;
using SistemaPedidoRestaurante.Entities;
using SistemaPedidoRestaurante.Validaciones;

namespace SistemaPedidoRestaurante.BLL
{
    public class ComprasClienteBLL
    {
        public DataTable Listar() { GestionReglas.Rol(3); return GestionReglas.Datos.Consultar("sp_Cliente_Pedidos", "@Actor", SesionActual.IdUsuario); }
        public DataTable Buscar(DataTable t, string filtro) { GestionReglas.Rol(3); return GestionReglas.Filtrar(t, filtro); }
        public DataTable Detalle(int pedido) { GestionReglas.Rol(3); return GestionReglas.Datos.Consultar("sp_Cliente_Detalle", "@Actor", SesionActual.IdUsuario, "@IdPedido", pedido); }
        public int Guardar(PedidoBorrador pedido, Guid solicitud, int id = 0)
        {
            GestionReglas.Rol(3);
            if (pedido == null || pedido.Lineas.Count == 0 || pedido.Lineas.Count > 200 || solicitud == Guid.Empty)
                throw new ArgumentException("Agregue entre 1 y 200 productos diferentes antes de guardar.");
            var xml = new XElement("pedido", pedido.Lineas.Select(l => new XElement("item",
                new XAttribute("producto", l.IdProducto), new XAttribute("cantidad", l.Cantidad), new XAttribute("precio", l.PrecioUnitario))));
            return GestionReglas.Escribir("sp_Cliente_Pedido_Guardar", "@Actor", SesionActual.IdUsuario,
                "@IdPedido", id, "@Solicitud", solicitud, "@Detalle", xml.ToString(SaveOptions.DisableFormatting));
        }
        public void Cancelar(int id)
        {
            GestionReglas.Rol(3);
            GestionReglas.Escribir("sp_Cliente_Pedido_Cancelar", "@Actor", SesionActual.IdUsuario, "@IdPedido", id);
        }
    }
}
