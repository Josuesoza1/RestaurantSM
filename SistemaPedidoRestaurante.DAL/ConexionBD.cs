using System.Data.SqlClient;
using System.Configuration; // Necesario para leer el App.config

namespace SistemaPedidoRestaurante.DAL
{
    public static class ConexionBD
    {
        public static SqlConnection ObtenerConexion()
        {
            // Leemos la cadena de conexión usando el nombre que le dimos en el App.config
            string cadenaConexion = ConfigurationManager.ConnectionStrings["RestauranteDB"].ConnectionString;

            return new SqlConnection(cadenaConexion);
        }
    }
}