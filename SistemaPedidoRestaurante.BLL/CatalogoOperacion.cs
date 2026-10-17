using System;
using System.Data.SqlClient;

namespace SistemaPedidoRestaurante.BLL
{
    internal static class CatalogoOperacion
    {
        public static void Ejecutar(Action operacion, string mensajeDuplicado)
        {
            if (!SesionActual.TieneRol(1))
                throw new UnauthorizedAccessException("Solo el administrador puede modificar el catálogo.");
            try { operacion(); }
            catch (SqlException ex) when (ex.Number == 2601 || ex.Number == 2627)
            {
                throw new ArgumentException(mensajeDuplicado);
            }
            catch (SqlException ex) when (ex.Number >= 50000 && ex.Number < 51000)
            {
                throw new ArgumentException(ex.Message);
            }
        }
    }
}
