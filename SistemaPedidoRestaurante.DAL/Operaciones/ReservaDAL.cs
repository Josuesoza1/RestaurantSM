using System;
using System.Data;
using System.Data.SqlClient;

namespace SistemaPedidoRestaurante.DAL
{
    public class ReservaDAL
    {
        public DataTable ListarReservas(int actor)
        {
            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            {
                using (SqlCommand comando = new SqlCommand("sp_Operacion_Reservas", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.Add("@Actor", SqlDbType.Int).Value = actor;
                    using (SqlDataAdapter adaptador = new SqlDataAdapter(comando))
                    {
                        DataTable tabla = new DataTable();
                        adaptador.Fill(tabla);
                        return tabla;
                    }
                }
            }
        }

        public void CambiarEstadoReserva(int actor, int reserva, string anterior, string nuevo)
        {
            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            {
                using (SqlCommand comando = new SqlCommand("sp_Operacion_EstadoReserva", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.Add("@Actor", SqlDbType.Int).Value = actor;
                    comando.Parameters.Add("@IdReserva", SqlDbType.Int).Value = reserva;
                    comando.Parameters.Add("@Anterior", SqlDbType.VarChar, 20).Value = anterior;
                    comando.Parameters.Add("@Nuevo", SqlDbType.VarChar, 20).Value = nuevo;
                    conexion.Open();
                    comando.ExecuteNonQuery();
                }
            }
        }
    }
}
