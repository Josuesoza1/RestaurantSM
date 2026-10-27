using System;
using System.Data;
using System.Data.SqlClient;
using SistemaPedidoRestaurante.Entities;

namespace SistemaPedidoRestaurante.DAL
{
    public class ClienteDAL
    {
        public DataTable ListarGestion(int idAdministrador)
        {
            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            {
                using (SqlCommand comando = new SqlCommand("sp_Gestion_Clientes", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.Add("@Actor", SqlDbType.Int).Value = idAdministrador;
                    using (SqlDataAdapter adaptador = new SqlDataAdapter(comando))
                    {
                        DataTable tabla = new DataTable();
                        adaptador.Fill(tabla);
                        return tabla;
                    }
                }
            }
        }

        public int GuardarGestion(Cliente cliente, bool activo, int idActor)
        {
            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            {
                using (SqlCommand comando = new SqlCommand("sp_Cliente_Guardar", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.Add("@Actor", SqlDbType.Int).Value = idActor;
                    comando.Parameters.Add("@Id", SqlDbType.Int).Value = cliente.Id;
                    comando.Parameters.Add("@PrimerNombre", SqlDbType.VarChar, 80).Value = (object)(cliente.PrimerNombre) ?? DBNull.Value;
                    comando.Parameters.Add("@SegundoNombre", SqlDbType.VarChar, 80).Value = (object)(cliente.SegundoNombre) ?? DBNull.Value;
                    comando.Parameters.Add("@PrimerApellido", SqlDbType.VarChar, 80).Value = (object)(cliente.PrimerApellido) ?? DBNull.Value;
                    comando.Parameters.Add("@SegundoApellido", SqlDbType.VarChar, 80).Value = (object)(cliente.SegundoApellido) ?? DBNull.Value;
                    comando.Parameters.Add("@Genero", SqlDbType.Char, 1).Value = (object)(cliente.Genero) ?? DBNull.Value;
                    comando.Parameters.Add("@Cedula", SqlDbType.VarChar, 30).Value = (object)(cliente.Cedula) ?? DBNull.Value;
                    comando.Parameters.Add("@Telefono", SqlDbType.VarChar, 20).Value = (object)(cliente.Telefono) ?? DBNull.Value;
                    comando.Parameters.Add("@Email", SqlDbType.VarChar, 120).Value = (object)(cliente.Email) ?? DBNull.Value;
                    comando.Parameters.Add("@Activo", SqlDbType.Bit).Value = activo;
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

        public int Eliminar(int idCliente, int idAdministrador)
        {
            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            {
                using (SqlCommand comando = new SqlCommand("sp_Cliente_Eliminar", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.Add("@Actor", SqlDbType.Int).Value = idAdministrador;
                    comando.Parameters.Add("@Id", SqlDbType.Int).Value = idCliente;
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

        public Cliente ObtenerPorUsuario(int idUsuario)
        {
            using (var connection = ConexionBD.ObtenerConexion())
            {
                using (SqlCommand command = new SqlCommand("sp_Cliente_ObtenerMiPerfil", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@IdUsuario", SqlDbType.Int).Value = idUsuario;
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        if (!reader.Read())
                        {
                            return null;
                        }

                        return new Cliente
                        {
                            Id = Convert.ToInt32(reader["idCliente"]),
                            PrimerNombre = reader["pNombreCl"].ToString(),
                            SegundoNombre = reader["sNombreCl"].ToString(),
                            PrimerApellido = reader["pApellidoCl"].ToString(),
                            SegundoApellido = reader["sApellidoCl"].ToString(),
                            Genero = reader["genero"].ToString(),
                            Cedula = reader["cedula"].ToString(),
                            Telefono = reader["telefono"].ToString(),
                            Email = reader["email"].ToString()
                        };
                    }
                }
            }
        }

        public void InsertarCliente(Cliente cliente, Usuario usuario)
        {
            using (SqlConnection connection = ConexionBD.ObtenerConexion())
            {
                using (SqlCommand command = new SqlCommand("sp_InsertarCliente", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Usuario", usuario.NombreUsuario);
                    command.Parameters.AddWithValue("@Contrasena", usuario.Contrasena);
                    command.Parameters.AddWithValue("@PrimerNombre", cliente.PrimerNombre);
                    command.Parameters.AddWithValue("@SegundoNombre", string.IsNullOrEmpty(cliente.SegundoNombre) ? (object)DBNull.Value : cliente.SegundoNombre);
                    command.Parameters.AddWithValue("@PrimerApellido", cliente.PrimerApellido);
                    command.Parameters.AddWithValue("@SegundoApellido", string.IsNullOrEmpty(cliente.SegundoApellido) ? (object)DBNull.Value : cliente.SegundoApellido);
                    command.Parameters.AddWithValue("@Genero", cliente.Genero);
                    command.Parameters.AddWithValue("@Telefono", (object)cliente.Telefono ?? DBNull.Value);
                    command.Parameters.AddWithValue("@Email", cliente.Email);
                    command.Parameters.AddWithValue("@Cedula", cliente.Cedula);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public DataTable ListarClientes(int actor)
        {
            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            {
                using (SqlCommand comando = new SqlCommand("sp_Operacion_Clientes", conexion))
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
    }
}
