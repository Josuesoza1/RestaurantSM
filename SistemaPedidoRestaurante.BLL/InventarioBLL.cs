using System;
using System.Linq;
using SistemaPedidoRestaurante.DAL;
using SistemaPedidoRestaurante.Entities;
using SistemaPedidoRestaurante.Validaciones;
using System.Data.SqlClient;

namespace SistemaPedidoRestaurante.BLL
{
    public class InventarioBLL
    {
        private readonly InventarioDAL inventarioDAL = new InventarioDAL();
        public void Eliminar(int idProducto, byte[] version)
        {
            Permisos.ExigirRol(1);
            if (idProducto <= 0 || version == null || version.Length != 8)
            {
                throw new ArgumentException("Seleccione un registro de inventario actualizado.");
            }

            try
            {
                inventarioDAL.Eliminar(idProducto, version, SesionActual.IdUsuario);
            }
            catch (SqlException error)
            {
                throw OperacionDatos.Traducir(error, "Ya existe un registro con ese código, nombre o dato único.");
            }
        }

        public InventarioProductos Listar()
        {
            Permisos.ExigirRol(1);
            return inventarioDAL.Listar(SesionActual.IdUsuario);
        }

        public InventarioProductos Buscar(InventarioProductos inventario, string filtro, bool soloBajos = false)
        {
            Permisos.ExigirRol(1);
            filtro = (filtro ?? string.Empty).Trim();
            InventarioProductos resultado = new InventarioProductos();
            if (inventario == null)
            {
                return resultado;
            }

            var coincidencias = inventario.Where(item => CoincideProducto(item, filtro));
            if (soloBajos)
            {
                coincidencias = coincidencias.Where(item =>
                    item.Activo && item.StockActual <= item.StockMinimo);
            }
            resultado.AddRange(coincidencias);
            return resultado;
        }

        private bool CoincideProducto(InventarioProducto producto, string filtro)
        {
            if (filtro.Length == 0)
            {
                return true;
            }
            return BusquedaLINQ.Contiene(producto.IdProducto.ToString(), filtro) ||
                BusquedaLINQ.Contiene(producto.Producto, filtro) ||
                BusquedaLINQ.Contiene(producto.Categoria, filtro) ||
                BusquedaLINQ.Contiene(producto.Unidad, filtro) ||
                BusquedaLINQ.Contiene(producto.PrecioVenta.ToString(), filtro) ||
                BusquedaLINQ.Contiene(producto.StockActual.ToString(), filtro) ||
                BusquedaLINQ.Contiene(producto.StockMinimo.ToString(), filtro) ||
                BusquedaLINQ.Contiene(producto.Activo.ToString(), filtro);
        }

        public void Guardar(int producto, int stock, int minimo, bool activo, byte[] version)
        {
            Guardar(new InventarioProducto
            {
                IdProducto = producto,
                StockActual = stock,
                StockMinimo = minimo,
                Activo = activo,
                Version = version
            });
        }

        public void Guardar(InventarioProducto inventario)
        {
            Permisos.ExigirRol(1);
            if (inventario == null)
            {
                throw new ArgumentException("Faltan los datos del inventario.");
            }

            GestionValidacion.Inventario(inventario.IdProducto, inventario.StockActual, inventario.StockMinimo);
            try
            {
                inventarioDAL.Guardar(inventario, SesionActual.IdUsuario);
            }
            catch (SqlException error)
            {
                throw OperacionDatos.Traducir(error, "Ya existe un registro con ese código, nombre o dato único.");
            }
        }
    }
}
