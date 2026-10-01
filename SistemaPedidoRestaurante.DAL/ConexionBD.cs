using System.Data.SqlClient;

namespace SistemaPedidoRestaurante.Conexion
{

    public static class ConexionBD
    {
        private static readonly string cadenaConexion = @"Server=LAPTOP-OPRAUTGR;Database=RestaurantSM;Trusted_Connection=True;TrustServerCertificate=True;";
        public static SqlConnection ObtenerConexion()
        {
            return new SqlConnection(cadenaConexion);
        }
    }
}
