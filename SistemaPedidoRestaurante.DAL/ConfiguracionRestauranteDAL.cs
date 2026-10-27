using System;
using System.Data;
using System.Data.SqlClient;

namespace SistemaPedidoRestaurante.DAL
{
    public class ConfiguracionRestauranteDAL
    {
        public DataTable VerificarInstalacion()
        {
            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            {
                using (SqlCommand comando = new SqlCommand("sp_Sistema_Verificar", conexion))
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

        public void GuardarConfiguracion(int actor, string nombre, decimal tasa)
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
                    conexion.Open();
                    comando.ExecuteNonQuery();
                }
            }
        }
    }
}
