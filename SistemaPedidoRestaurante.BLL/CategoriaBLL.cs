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
            if (idCategoria <= 0)
            {
                throw new ArgumentException("Seleccione una categoría para eliminar.");
            }

            Permisos.ExigirRol(1);
            _categoriaDAL.Eliminar(idCategoria, SesionActual.IdUsuario);
        }

        public void GuardarCategoria(Categoria categoria)
        {
            Permisos.ExigirRol(1);
            _validacion.ValidarCategoria(categoria);
            // El procedimiento inserta si el ID es cero y actualiza si ya existe.
            _categoriaDAL.Guardar(categoria, SesionActual.IdUsuario);
        }

        public void DesactivarCategoria(int idCategoria)
        {
            Permisos.ExigirRol(1);
            if (idCategoria <= 0)
            {
                throw new ArgumentException("Seleccione una categoría.");
            }

            _categoriaDAL.Desactivar(idCategoria, SesionActual.IdUsuario);
        }

        public Categorias ObtenerCategorias()
        {
            return _categoriaDAL.Listar();
        }

        public Categorias BuscarCategorias(string filtro)
        {
            Categorias categorias = _categoriaDAL.Listar();
            if (string.IsNullOrWhiteSpace(filtro))
            {
                return categorias;
            }

            filtro = filtro.Trim();
            // LINQ busca en el nombre o la descripción, sin modificar la BD.
            var coincidencias = categorias.Where(categoria => BusquedaLINQ.Contiene(categoria.Nombre, filtro) || BusquedaLINQ.Contiene(categoria.Descripcion, filtro)).ToList();
            Categorias resultado = new Categorias();
            resultado.AddRange(coincidencias);
            return resultado;
        }
    }
}
