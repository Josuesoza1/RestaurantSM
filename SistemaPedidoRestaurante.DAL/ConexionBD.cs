using System.Data.SqlClient;

namespace SistemaPedidoRestaurante.Conexion
{

    public static class ConexionBD
    {
        private static readonly string cadenaConexion =
        @"Server=(localdb)\MSSQLLocalDB;Database=RestaurantSM;User Id=2M6-SIS;Password=sasa;";
        public static SqlConnection ObtenerConexion()
        {
            return new SqlConnection(cadenaConexion);
        }
    }
}
