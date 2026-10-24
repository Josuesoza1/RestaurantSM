using System;
using System.Collections.Generic;
using System.Linq;
using SistemaPedidoRestaurante.Entities;

namespace SistemaPedidoRestaurante.BLL
{
    // Solo mantiene una selección en memoria. No registra pedidos ni pagos.
    public class PedidoBorrador
    {
        private readonly List<LineaPedidoBorrador> lineas = new List<LineaPedidoBorrador>();
        public IReadOnlyList<LineaPedidoBorrador> Lineas
        {
            get { return lineas.AsReadOnly(); }
        }

        public decimal Total
        {
            get { return lineas.Sum(linea => linea.Subtotal); }
        }

        public void Agregar(Producto producto, int cantidad)
        {
            if (producto == null || producto.IdProducto <= 0 || !producto.Disponible)
            {
                throw new ArgumentException("Seleccione un producto disponible.");
            }

            if (producto.Precio <= 0 || producto.Precio > 99999999.99m || decimal.Round(producto.Precio, 2) != producto.Precio)
            {
                throw new ArgumentException("El producto no tiene un precio válido.");
            }

            LineaPedidoBorrador anterior = lineas.FirstOrDefault(linea =>
                linea.IdProducto == producto.IdProducto);

            int cantidadAnterior = 0;
            decimal subtotalAnterior = 0;
            decimal precio = producto.Precio;
            if (anterior != null)
            {
                cantidadAnterior = anterior.Cantidad;
                subtotalAnterior = anterior.Subtotal;
                precio = anterior.PrecioUnitario;
            }

            // Usar long permite comprobar el limite antes de convertir a int.
            long nuevaCantidad = (long)cantidad + cantidadAnterior;
            if (cantidad <= 0 || nuevaCantidad > 999)
            {
                throw new ArgumentException("La cantidad por producto debe estar entre 1 y 999.");
            }

            LineaPedidoBorrador nueva = new LineaPedidoBorrador(producto.IdProducto, producto.Nombre, precio, (int)nuevaCantidad);
            decimal nuevoTotal = Total - subtotalAnterior + nueva.Subtotal;
            if (nuevoTotal > 99999999.99m)
            {
                throw new ArgumentException("El total supera el importe permitido para un pedido.");
            }

            if (anterior != null)
            {
                lineas.Remove(anterior);
            }

            lineas.Add(nueva);
        }

        public void Quitar(int idProducto)
        {
            var linea = lineas.FirstOrDefault(l => l.IdProducto == idProducto);
            if (linea == null)
            {
                throw new ArgumentException("Seleccione una línea del pedido.");
            }

            lineas.Remove(linea);
        }

        public void Vaciar()
        {
            lineas.Clear();
        }
    }
}
