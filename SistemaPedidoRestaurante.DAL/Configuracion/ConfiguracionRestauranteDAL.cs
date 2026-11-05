using System;
using System.Data;
using System.Data.SqlClient;

namespace SistemaPedidoRestaurante.DAL
{
    public class ConfiguracionRestauranteDAL
    {
        public DataTable ObtenerConfiguracion()
        {
            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            {
                using (SqlCommand comando = new SqlCommand("sp_Configuracion_Obtener", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    using (SqlDataAdapter adaptador = new SqlDataAdapter(comando))
                    {
                        DataTable tabla = new DataTable();
                        adaptador.Fill(tabla);
                        return tabla;
                    }
                }
            }
        }

        public void GuardarConfiguracion(int actor, string nombre, decimal tasa, decimal? tarifaEnvio, bool actualizarEnvio)
        {
            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            {
                using (SqlCommand comando = new SqlCommand("sp_Configuracion_Guardar", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.Add("@Actor", SqlDbType.Int).Value = actor;
                    comando.Parameters.Add("@Nombre", SqlDbType.NVarChar, 120).Value = nombre;
                    SqlParameter impuesto = comando.Parameters.Add("@Tasa", SqlDbType.Decimal);
                    impuesto.Precision = 5;
                    impuesto.Scale = 2;
                    impuesto.Value = tasa;
                    SqlParameter envio = comando.Parameters.Add("@TarifaEnvio", SqlDbType.Decimal);
                    envio.Precision = 10; envio.Scale = 2;
                    envio.Value = tarifaEnvio.HasValue ? (object)tarifaEnvio.Value : DBNull.Value;
                    comando.Parameters.Add("@ActualizarEnvio", SqlDbType.Bit).Value = actualizarEnvio;
                    conexion.Open();
                    comando.ExecuteNonQuery();
                }
            }
        }
    }
}
