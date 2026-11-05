using SistemaPedidoRestaurante.DAL;
using SistemaPedidoRestaurante.Entities;
using System.Linq;
using System;
using System.Collections.Generic;
using SistemaPedidoRestaurante.Validaciones;
using System.Data.SqlClient;

namespace SistemaPedidoRestaurante.BLL
{
    public class ProductoBLL
    {
        public void EliminarProducto(int idProducto)
        {
            if (idProducto <= 0)
            {
                throw new ArgumentException("Seleccione un producto para eliminar.");
            }

            try
            {
                Permisos.ExigirRol(1);
                _productoDAL.Eliminar(idProducto, SesionActual.IdUsuario);
            }
            catch (SqlException error)
            {
                throw OperacionDatos.Traducir(error, "El producto tiene datos relacionados.");
            }
        }

        private readonly ProductoDAL _productoDAL = new ProductoDAL();
        private readonly CatalogoValidacion _validacion = new CatalogoValidacion();
        public List<Subcategoria> ObtenerSubcategorias()
        {
            return _productoDAL.ListarSubcategorias();
        }

        public void GuardarProducto(Producto producto)
        {
            try
            {
                Permisos.ExigirRol(1);
                _validacion.ValidarProducto(producto);
                _productoDAL.Guardar(producto, SesionActual.IdUsuario);
            }
            catch (SqlException error)
            {
                throw OperacionDatos.Traducir(error, "El nombre o el código del producto ya están registrados.");
            }
        }

        public void DesactivarProducto(int idProducto)
        {
            try
            {
                Permisos.ExigirRol(1);
                if (idProducto <= 0)
                {
                    throw new ArgumentException("Seleccione un producto.");
                }

                _productoDAL.Desactivar(idProducto, SesionActual.IdUsuario);
            }
            catch (SqlException error)
            {
                throw OperacionDatos.Traducir(error, "El nombre o el código del producto ya están registrados.");
            }
        }

        public Productos ObtenerProductos()
        {
            return _productoDAL.Listar();
        }

        public Productos ObtenerDisponiblesParaPedido()
        {
            if (!SesionActual.EstaAutenticado)
            {
                throw new UnauthorizedAccessException("Inicie sesión para consultar los productos disponibles.");
            }

            return _productoDAL.ListarDisponibles();
        }

        public Productos BuscarProductos(string filtro)
        {
            Productos productos = _productoDAL.Listar();
            if (string.IsNullOrWhiteSpace(filtro))
            {
                return productos;
            }

            filtro = filtro.Trim();
            Productos resultado = new Productos();
            var coincidencias = productos.Where(producto =>
                BusquedaLINQ.Contiene(producto.Nombre, filtro) ||
                BusquedaLINQ.Contiene(producto.CategoriaNombre, filtro) ||
                BusquedaLINQ.Contiene(producto.Codigo, filtro) ||
                BusquedaLINQ.Contiene(producto.Descripcion, filtro));
            resultado.AddRange(coincidencias);
            return resultado;
        }
    }
}
