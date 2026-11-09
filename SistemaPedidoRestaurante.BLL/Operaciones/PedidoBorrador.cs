using System;
using System.Collections.Generic;
using System.Linq;
using SistemaPedidoRestaurante.Entities;

namespace SistemaPedidoRestaurante.BLL
{
    public class PedidoBorrador
    {
        private const decimal ImporteMaximo = 99999999.99m;
        private readonly List<LineaPedidoBorrador> lineas = new List<LineaPedidoBorrador>();
        public IReadOnlyList<LineaPedidoBorrador> Lineas
        {
            get { return lineas.AsReadOnly(); }
        }

        public decimal Total
        {
            get { return lineas.Sum(linea => linea.Subtotal); }
        }

        public void CargarDetalle(IEnumerable<LineaPedidoBorrador> detalle)
        {
            lineas.Clear();
            lineas.AddRange(detalle);
        }

        public void Agregar(Producto producto, int cantidad, decimal? precioOriginal = null)
        {
            if (producto == null || producto.IdProducto <= 0 || !producto.Disponible)
            {
                throw new ArgumentException("Seleccione un producto disponible.");
            }

            if (producto.Precio <= 0 || producto.Precio > ImporteMaximo || decimal.Round(producto.Precio, 2) != producto.Precio)
            {
                throw new ArgumentException("El precio del producto debe estar entre " + 0.01m.ToString("N2") +
                    " y " + ImporteMaximo.ToString("N2") + ", con hasta dos decimales.");
            }

            LineaPedidoBorrador anterior = lineas.FirstOrDefault(linea =>
                linea.IdProducto == producto.IdProducto);

            int cantidadAnterior = 0;
            decimal precio = precioOriginal ?? producto.Precio;
            if (anterior != null)
            {
                cantidadAnterior = anterior.Cantidad;
                precio = anterior.PrecioUnitario;
            }

            long nuevaCantidad = (long)cantidad + cantidadAnterior;
            if (cantidad <= 0 || nuevaCantidad > 999)
            {
                throw new ArgumentException("La cantidad por producto debe estar entre 1 y 999.");
            }

            LineaPedidoBorrador nueva = new LineaPedidoBorrador(producto.IdProducto, producto.Nombre, precio, (int)nuevaCantidad);
            GuardarLinea(nueva, anterior);
        }

        public bool ActualizarPrecios(Productos catalogo, IReadOnlyList<LineaPedidoBorrador> originales)
        {
            var actualizadas = new List<LineaPedidoBorrador>();
            bool cambio = false;
            foreach (LineaPedidoBorrador linea in lineas)
            {
                var producto = catalogo.FirstOrDefault(item => item.IdProducto == linea.IdProducto);
                if (producto != null && producto.Precio > 0 && producto.Precio != linea.PrecioUnitario &&
                    !originales.Any(original => original.IdProducto == linea.IdProducto))
                {
                    actualizadas.Add(new LineaPedidoBorrador(linea.IdProducto, linea.Producto, producto.Precio, linea.Cantidad));
                    cambio = true;
                }
                else
                {
                    actualizadas.Add(linea);
                }
            }

            if (!cambio || actualizadas.Sum(linea => linea.Subtotal) > ImporteMaximo)
                return false;

            CargarDetalle(actualizadas);
            return true;
        }

        public LineaPedidoBorrador CambiarCantidad(LineaPedidoBorrador linea, int cantidad)
        {
            if (!lineas.Contains(linea))
                throw new ArgumentException("Seleccione una línea del pedido.");
            if (cantidad < 1 || cantidad > 999)
                throw new ArgumentException("La cantidad por producto debe estar entre 1 y 999.");

            var nueva = new LineaPedidoBorrador(linea.IdProducto, linea.Producto, linea.PrecioUnitario, cantidad);
            GuardarLinea(nueva, linea);
            return nueva;
        }

        private void GuardarLinea(LineaPedidoBorrador nueva, LineaPedidoBorrador anterior)
        {
            decimal nuevoTotal = Total - (anterior?.Subtotal ?? 0m) + nueva.Subtotal;
            if (nuevoTotal > ImporteMaximo)
            {
                throw new ArgumentException("El total del pedido no puede superar " + ImporteMaximo.ToString("N2") + ".");
            }

            if (anterior == null)
                lineas.Add(nueva);
            else
                lineas[lineas.IndexOf(anterior)] = nueva;
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
