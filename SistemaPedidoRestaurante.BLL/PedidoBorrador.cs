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
        public IReadOnlyList<LineaPedidoBorrador> Lineas => lineas.AsReadOnly();
        public decimal Total => lineas.Sum(l => l.Subtotal);

        public void Agregar(Producto producto, int cantidad)
        {
            if (producto == null || producto.IdProducto <= 0 || !producto.Disponible)
                throw new ArgumentException("Seleccione un producto disponible.");
            if (producto.Precio <= 0 || producto.Precio > 99999999.99m || decimal.Round(producto.Precio, 2) != producto.Precio)
                throw new ArgumentException("El producto no tiene un precio válido.");
            var anterior = lineas.FirstOrDefault(l => l.IdProducto == producto.IdProducto);
            long nuevaCantidad = (long)cantidad + (anterior?.Cantidad ?? 0);
            if (cantidad <= 0 || nuevaCantidad > 999)
                throw new ArgumentException("La cantidad por producto debe estar entre 1 y 999.");
            decimal precio = anterior?.PrecioUnitario ?? producto.Precio;
            var nueva = new LineaPedidoBorrador(producto.IdProducto, producto.Nombre, precio, (int)nuevaCantidad);
            if (Total - (anterior?.Subtotal ?? 0) + nueva.Subtotal > 99999999.99m)
                throw new ArgumentException("El total supera el importe permitido para un pedido.");
            if (anterior != null) lineas.Remove(anterior);
            lineas.Add(nueva);
        }

        public void Quitar(int idProducto)
        {
            var linea = lineas.FirstOrDefault(l => l.IdProducto == idProducto);
            if (linea == null) throw new ArgumentException("Seleccione una línea del pedido.");
            lineas.Remove(linea);
        }

        public void Vaciar() { lineas.Clear(); }
    }
}
