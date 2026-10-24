using System;
using System.Data;
using System.Data.SqlClient;

namespace SistemaPedidoRestaurante.DAL
{
    public class ReservasClienteDAL
    {
        public DataTable Listar(int actor)
        {
            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            {
                using (SqlCommand comando = new SqlCommand("sp_Cliente_Reservas", conexion))
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

        public int Guardar(int actor, int id, int mesa, DateTime fecha, int personas)
        {
            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            {
                using (SqlCommand comando = new SqlCommand("sp_Cliente_Reserva_Guardar", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.Add("@Actor", SqlDbType.Int).Value = actor;
                    comando.Parameters.Add("@Id", SqlDbType.Int).Value = id;
                    comando.Parameters.Add("@IdMesa", SqlDbType.Int).Value = mesa;
                    comando.Parameters.Add("@Fecha", SqlDbType.DateTime).Value = fecha;
                    comando.Parameters.Add("@Personas", SqlDbType.Int).Value = personas;
                    conexion.Open();
                    object resultado = comando.ExecuteScalar();
                    if (resultado == null || resultado == DBNull.Value)
                    {
                        return 0;
                    }

                    return Convert.ToInt32(resultado);
                }
            }
        }

        public int Cancelar(int actor, int id)
        {
            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            {
                using (SqlCommand comando = new SqlCommand("sp_Cliente_Reserva_Cancelar", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.Add("@Actor", SqlDbType.Int).Value = actor;
                    comando.Parameters.Add("@Id", SqlDbType.Int).Value = id;
                    conexion.Open();
                    object resultado = comando.ExecuteScalar();
                    if (resultado == null || resultado == DBNull.Value)
                    {
                        return 0;
                    }

                    return Convert.ToInt32(resultado);
                }
            }
        }
    }
}
