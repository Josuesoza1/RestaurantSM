using System;
using System.Data;
using System.Data.SqlClient;

namespace SistemaPedidoRestaurante.DAL
{
    public class PagoDAL
    {
        public DataTable Cotizar(int actor, int pedido)
        {
            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            {
                using (SqlCommand comando = new SqlCommand("sp_Cobro_Cotizar", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.Add("@Actor", SqlDbType.Int).Value = actor;
                    comando.Parameters.Add("@IdPedido", SqlDbType.Int).Value = pedido;
                    using (SqlDataAdapter adaptador = new SqlDataAdapter(comando))
                    {
                        DataTable tabla = new DataTable();
                        adaptador.Fill(tabla);
                        return tabla;
                    }
                }
            }
        }

        public DataTable Cobrar(int actor, int pedido, string metodo, decimal recibido, decimal total, Guid solicitud, string notas)
        {
            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            {
                using (SqlCommand comando = new SqlCommand("sp_Cobro_Registrar", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.Add("@Actor", SqlDbType.Int).Value = actor;
                    comando.Parameters.Add("@IdPedido", SqlDbType.Int).Value = pedido;
                    comando.Parameters.Add("@Metodo", SqlDbType.VarChar, 40).Value = metodo;
                    SqlParameter monto = comando.Parameters.Add("@Recibido", SqlDbType.Decimal);
                    monto.Precision = 10;
                    monto.Scale = 2;
                    monto.Value = recibido;
                    SqlParameter importe = comando.Parameters.Add("@TotalEsperado", SqlDbType.Decimal);
                    importe.Precision = 10;
                    importe.Scale = 2;
                    importe.Value = total;
                    comando.Parameters.Add("@Solicitud", SqlDbType.UniqueIdentifier).Value = solicitud;
                    comando.Parameters.Add("@Notas", SqlDbType.NVarChar, 200).Value = (object)notas ?? DBNull.Value;
                    using (SqlDataAdapter adaptador = new SqlDataAdapter(comando))
                    {
                        DataTable resultado = new DataTable();
                        adaptador.Fill(resultado);
                        return resultado;
                    }
                }
            }
        }
    }
}
