using System;
using System.Data;
using System.Data.SqlClient;
using SistemaPedidoRestaurante.DAL;
using SistemaPedidoRestaurante.Entities;

namespace SistemaPedidoRestaurante.DAL
{
    public class ClienteDAL
    {
        public Cliente ObtenerPorUsuario(int idUsuario)
        {
            using (var connection = ConexionBD.ObtenerConexion())
            using (var command = new SqlCommand("sp_Cliente_ObtenerMiPerfil", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@IdUsuario", SqlDbType.Int).Value = idUsuario;
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    if (!reader.Read()) return null;
                    return new Cliente { Id = Convert.ToInt32(reader["idCliente"]),
                        PrimerNombre = reader["pNombreCl"].ToString(), SegundoNombre = reader["sNombreCl"].ToString(),
                        PrimerApellido = reader["pApellidoCl"].ToString(), SegundoApellido = reader["sApellidoCl"].ToString(),
                        Genero = reader["genero"].ToString(), Cedula = reader["cedula"].ToString(),
                        Telefono = reader["telefono"].ToString(), Email = reader["email"].ToString() };
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
                    command.Parameters.AddWithValue("@Telefono", cliente.Telefono);
                    command.Parameters.AddWithValue("@Email", cliente.Email);
                    command.Parameters.AddWithValue("@Cedula", cliente.Cedula);


                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
