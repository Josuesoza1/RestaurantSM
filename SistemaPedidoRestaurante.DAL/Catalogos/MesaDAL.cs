using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using SistemaPedidoRestaurante.Entities;

namespace SistemaPedidoRestaurante.DAL
{
    public class MesaDAL
    {
        public DataTable ListarGestion()
        {
            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            {
                using (SqlCommand comando = new SqlCommand("sp_Mesa_Listar", conexion))
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

        public int Guardar(int id, int numero, int capacidad, string estado, bool activa, int idAdministrador)
        {
            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            {
                using (SqlCommand comando = new SqlCommand("sp_Mesa_Guardar", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.Add("@Actor", SqlDbType.Int).Value = idAdministrador;
                    comando.Parameters.Add("@Id", SqlDbType.Int).Value = id;
                    comando.Parameters.Add("@Numero", SqlDbType.Int).Value = numero;
                    comando.Parameters.Add("@Capacidad", SqlDbType.Int).Value = capacidad;
                    comando.Parameters.Add("@Estado", SqlDbType.VarChar, 20).Value = (object)(estado) ?? DBNull.Value;
                    comando.Parameters.Add("@Activa", SqlDbType.Bit).Value = activa;
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

        public int Eliminar(int id, int idAdministrador)
        {
            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            {
                using (SqlCommand comando = new SqlCommand("sp_Mesa_Eliminar", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.Add("@Actor", SqlDbType.Int).Value = idAdministrador;
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

        public List<Mesa> Listar()
        {
            List<Mesa> lista = new List<Mesa>();
            using (SqlConnection connection = ConexionBD.ObtenerConexion())
            {
                using (SqlCommand command = new SqlCommand("sp_Mesa_Listar", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Mesa
                            {
                                IdMesa = Convert.ToInt32(reader["idMesa"]),
                                Numero = Convert.ToInt32(reader["numero"]),
                                Capacidad = Convert.ToInt32(reader["capacidad"]),
                                Estado = reader["estado"].ToString()
                            });
                        }
                    }
                }
            }

            return lista;
        }

        public DataTable ListarMesas(int actor)
        {
            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            using (SqlCommand comando = new SqlCommand("sp_Operacion_Mesas", conexion))
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

        public void CambiarMesa(int actor, int mesa, string anterior)
        {
            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            using (SqlCommand comando = new SqlCommand("sp_Operacion_CambiarMesa", conexion))
            {
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("@Actor", SqlDbType.Int).Value = actor;
                comando.Parameters.Add("@IdMesa", SqlDbType.Int).Value = mesa;
                comando.Parameters.Add("@Anterior", SqlDbType.VarChar, 20).Value = anterior;
                conexion.Open();
                comando.ExecuteNonQuery();
            }
        }

        public void AsignarMesa(int actor, int pedido, int mesa)
        {
            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            using (SqlCommand comando = new SqlCommand("sp_Operacion_AsignarMesa", conexion))
            {
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("@Actor", SqlDbType.Int).Value = actor;
                comando.Parameters.Add("@IdPedido", SqlDbType.Int).Value = pedido;
                comando.Parameters.Add("@IdMesa", SqlDbType.Int).Value = mesa;
                conexion.Open();
                comando.ExecuteNonQuery();
            }
        }
    }
}
