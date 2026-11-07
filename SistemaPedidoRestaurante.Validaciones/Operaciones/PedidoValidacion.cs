using System;
using System.Collections.Generic;
using SistemaPedidoRestaurante.Entities;

namespace SistemaPedidoRestaurante.Validaciones
{
    public static class PedidoValidacion
    {
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
