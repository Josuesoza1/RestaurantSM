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

        public void GuardarConfiguracion(int actor, string nombre, decimal tasa, bool descuentoActivo,
            decimal porcentajeDescuento, DateTime? fechaDescuento)
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
                    comando.Parameters.Add("@DescuentoActivo", SqlDbType.Bit).Value = descuentoActivo;
                    SqlParameter descuento = comando.Parameters.Add("@PorcentajeDescuento", SqlDbType.Decimal);
                    descuento.Precision = 5;
                    descuento.Scale = 2;
                    descuento.Value = porcentajeDescuento;
                    comando.Parameters.Add("@FechaDescuento", SqlDbType.Date).Value =
                        fechaDescuento.HasValue ? (object)fechaDescuento.Value.Date : DBNull.Value;
                    conexion.Open();
                    comando.ExecuteNonQuery();
                }
            }
        }
    }
}
