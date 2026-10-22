using System;
using System.Linq;
using SistemaPedidoRestaurante.DAL;
using SistemaPedidoRestaurante.Entities;
using SistemaPedidoRestaurante.Validaciones;

namespace SistemaPedidoRestaurante.BLL
{
    public class InventarioBLL
    {
        private readonly InventarioDAL inventarioDAL = new InventarioDAL();

        public void Eliminar(int idProducto, byte[] version)
        {
            Permisos.ExigirRol(1);
            if (idProducto <= 0 || version == null || version.Length != 8)
                throw new ArgumentException("Seleccione un registro de inventario actualizado.");
            OperacionDatos.Ejecutar(() => inventarioDAL.Eliminar(idProducto, version, SesionActual.IdUsuario));
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
            var resultado = new InventarioProductos();
            if (inventario == null) return resultado;
            resultado.AddRange(inventario.Where(item =>
                (filtro.Length == 0 || new[] { item.IdProducto.ToString(), item.Producto,
                    item.Categoria, item.Unidad, item.PrecioVenta.ToString(),
                    item.StockActual.ToString(), item.StockMinimo.ToString(), item.Activo.ToString() }
                    .Any(texto => BusquedaLINQ.Contiene(texto, filtro))) &&
                (!soloBajos || item.Activo && item.StockActual <= item.StockMinimo)));
            return resultado;
        }

        public void Guardar(int producto, int stock, int minimo, bool activo, byte[] version)
        {
            Guardar(new InventarioProducto { IdProducto = producto, StockActual = stock,
                StockMinimo = minimo, Activo = activo, Version = version });
        }

        public void Guardar(InventarioProducto inventario)
        {
            Permisos.ExigirRol(1);
            if (inventario == null) throw new ArgumentException("Faltan los datos del inventario.");
            GestionValidacion.Inventario(inventario.IdProducto, inventario.StockActual, inventario.StockMinimo);
            OperacionDatos.Ejecutar(() => inventarioDAL.Guardar(inventario, SesionActual.IdUsuario));
        }
    }
}
