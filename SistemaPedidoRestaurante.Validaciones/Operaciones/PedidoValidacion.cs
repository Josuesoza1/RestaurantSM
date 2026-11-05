using System;
using System.Collections.Generic;
using SistemaPedidoRestaurante.Entities;

namespace SistemaPedidoRestaurante.Validaciones
{
    public static class PedidoValidacion
    {
        public static void Entrega(EntregaPedido entrega)
        {
            if (entrega == null || entrega.IdTipoEntrega < 1 || entrega.IdTipoEntrega > 3)
            {
                throw new ArgumentException("Seleccione como recibira el pedido.");
            }
            CamposValidacion.Importe(entrega.CostoEnvioEsperado, "costo de envío", true);
            if (entrega.IdTipoEntrega == EntregaPedido.Domicilio)
            {
                entrega.Direccion = CamposValidacion.Texto(entrega.Direccion, 250, "direccion de entrega", true);
                entrega.Referencia = CamposValidacion.Texto(entrega.Referencia, 150, "referencia");
                entrega.TelefonoContacto = CamposValidacion.Telefono(entrega.TelefonoContacto, true);
            }
            else
            {
                entrega.Direccion = null;
                entrega.Referencia = null;
                entrega.TelefonoContacto = null;
                if (entrega.CostoEnvioEsperado != 0)
                {
                    throw new ArgumentException("Solo los pedidos a domicilio tienen costo de envio.");
                }
            }
        }

        public static void Contenido(IReadOnlyList<LineaPedidoBorrador> lineas, Guid solicitud)
        {
            if (lineas == null || lineas.Count < 1 || lineas.Count > 200 || solicitud == Guid.Empty)
            {
                throw new ArgumentException("Agregue entre 1 y 200 productos diferentes antes de guardar.");
            }

            decimal total = 0;
            List<int> productos = new List<int>();
            foreach (LineaPedidoBorrador linea in lineas)
            {
                if (linea == null || linea.IdProducto <= 0 || productos.Contains(linea.IdProducto))
                {
                    throw new ArgumentException("El detalle debe contener productos válidos, sin líneas repetidas.");
                }
                productos.Add(linea.IdProducto);
                if (linea.Cantidad < 1 || linea.Cantidad > 999)
                {
                    throw new ArgumentException("La cantidad por producto debe estar entre 1 y 999.");
                }
                CamposValidacion.Importe(linea.PrecioUnitario, "precio del producto", false);
                total += linea.Subtotal;
            }
            if (total > 99999999.99m)
            {
                throw new ArgumentException("El total supera el importe permitido para un pedido.");
            }
        }
    }
}
