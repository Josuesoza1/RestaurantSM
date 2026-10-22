using SistemaPedidoRestaurante.DAL;
using SistemaPedidoRestaurante.Entities;
using System.Linq;
using System;
using SistemaPedidoRestaurante.Validaciones;
namespace SistemaPedidoRestaurante.BLL
{
    public class CategoriaBLL
    {
        private readonly CategoriaDAL _categoriaDAL = new CategoriaDAL();
        private readonly CatalogoValidacion _validacion = new CatalogoValidacion();

        public void EliminarCategoria(int idCategoria)
        {
            if (idCategoria <= 0) throw new ArgumentException("Seleccione una categoría para eliminar.");
            CatalogoOperacion.Ejecutar(() => _categoriaDAL.Eliminar(idCategoria, SesionActual.IdUsuario),
                "La categoría tiene datos relacionados.");
        }

        public void GuardarCategoria(Categoria categoria)
        {
            CatalogoOperacion.Ejecutar(() =>
            {
                _validacion.ValidarCategoria(categoria);
                _categoriaDAL.Guardar(categoria, SesionActual.IdUsuario);
            }, "Ya existe una categoría con ese nombre.");
        }

        public void DesactivarCategoria(int idCategoria)
        {
            CatalogoOperacion.Ejecutar(() =>
            {
                if (idCategoria <= 0) throw new ArgumentException("Seleccione una categoría.");
                _categoriaDAL.Desactivar(idCategoria, SesionActual.IdUsuario);
            }, "Ya existe una categoría con ese nombre.");
        }

        public Categorias ObtenerCategorias()
        {
            return _categoriaDAL.Listar();
        }


        public Categorias BuscarCategorias(string filtro)
        {
            Categorias categorias = _categoriaDAL.Listar();

            if (string.IsNullOrWhiteSpace(filtro))
                return categorias;

            filtro = filtro.Trim();

            var resultado = new Categorias();
            resultado.AddRange(categorias.Where(categoria =>
                BusquedaLINQ.Contiene(categoria.Nombre, filtro) ||
                BusquedaLINQ.Contiene(categoria.Descripcion, filtro)));

            return resultado;
        }
    }
}
