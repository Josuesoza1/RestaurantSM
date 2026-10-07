using SistemaPedidoRestaurante.DAL;
using SistemaPedidoRestaurante.Entities;
using System.Linq;
using System;

namespace SistemaPedidoRestaurante.BLL
{
    public class ProductoBLL
    {
        private readonly ProductoDAL _productoDAL = new ProductoDAL();

        public Productos ObtenerProductos()
        {
            return _productoDAL.Listar();
        }

        public Productos BuscarProductos(string filtro)
        {
            Productos productos = _productoDAL.Listar();

            if (string.IsNullOrWhiteSpace(filtro))
                return productos;

            filtro = filtro.Trim();

            var resultado = new Productos();
            resultado.AddRange(productos.Where(producto =>
                BusquedaLINQ.Contiene(producto.Nombre, filtro) ||
                BusquedaLINQ.Contiene(producto.CategoriaNombre, filtro) ||
                BusquedaLINQ.Contiene(producto.Codigo, filtro) ||
                BusquedaLINQ.Contiene(producto.Descripcion, filtro)));

            return resultado;
        }
    }
}