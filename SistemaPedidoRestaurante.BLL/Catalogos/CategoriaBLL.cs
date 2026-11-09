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
            bool soloId = BusquedaLINQ.LeerFiltroId(filtro, out int id);
            var coincidencias = categorias.Where(categoria =>
                categoria.IdCategoria == id || (!soloId &&
                    (BusquedaLINQ.Contiene(categoria.Nombre, filtro) ||
                     BusquedaLINQ.Contiene(categoria.Descripcion, filtro))));
            Categorias resultado = new Categorias();
            resultado.AddRange(coincidencias);
            return resultado;
        }
    }
}
