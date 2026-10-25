using System.Data.SqlClient;
using System.Configuration;

namespace SistemaPedidoRestaurante.DAL
{
    public static class ConexionBD
    {
        public static SqlConnection ObtenerConexion()
        {
            string cadenaConexion = ConfigurationManager.ConnectionStrings["RestauranteDB"].ConnectionString;
            return new SqlConnection(cadenaConexion);
        }
    }
}
