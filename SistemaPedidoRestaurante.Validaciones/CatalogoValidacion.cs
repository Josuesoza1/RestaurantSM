using System;
using SistemaPedidoRestaurante.Entities;

namespace SistemaPedidoRestaurante.Validaciones
{
    public class CatalogoValidacion
    {
        public void ValidarCategoria(Categoria categoria)
        {
            if (categoria == null) throw new ArgumentException("Faltan los datos de la categoría.");
            Texto(categoria.Nombre, "nombre", 60, true);
            Texto(categoria.Descripcion, "descripción", 200, false);
            if (categoria.IdCategoria < 0) throw new ArgumentException("Categoría inválida.");
        }

        public void ValidarProducto(Producto producto)
        {
            if (producto == null) throw new ArgumentException("Faltan los datos del producto.");
            Texto(producto.Nombre, "nombre", 100, true);
            Texto(producto.Codigo, "código", 20, true);
            Texto(producto.Descripcion, "descripción", 250, false);
            if (producto.IdProducto < 0 || producto.IdCategoria <= 0)
                throw new ArgumentException("Seleccione una categoría válida.");
            if (producto.IdSubcategoria.HasValue && producto.IdSubcategoria <= 0)
                throw new ArgumentException("Seleccione una subcategoría válida o Sin subcategoría.");
            if (producto.Precio <= 0 || producto.Precio > 99999999.99m || decimal.Round(producto.Precio, 2) != producto.Precio)
                throw new ArgumentException("El precio debe ser positivo, con hasta dos decimales y máximo 99999999.99.");
        }

        private void Texto(string valor, string campo, int longitud, bool obligatorio)
        {
            CamposValidacion.Texto(valor,longitud,campo,obligatorio);
        }
    }
}
