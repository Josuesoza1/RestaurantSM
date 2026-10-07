using SistemaPedidoRestaurante.DAL;
using SistemaPedidoRestaurante.Entities;
using System.Linq;
namespace SistemaPedidoRestaurante.BLL
{
    public class CategoriaBLL
    {
        private readonly CategoriaDAL _categoriaDAL = new CategoriaDAL();

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
