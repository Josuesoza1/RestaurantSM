using System;
using System.Data;
using System.Data.SqlClient;
using SistemaPedidoRestaurante.Conexion;
using SistemaPedidoRestaurante.Entities;

namespace SistemaPedidoRestaurante.DAL
{
    public class ClienteDAL
    {
        public void InsertarCliente(Cliente cliente, Usuario usuario)
        {
            using (SqlConnection connection = ConexionBD.ObtenerConexion())
            {
                using (SqlCommand command = new SqlCommand("sp_InsertarCliente", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@PrimerNombre", cliente.PrimerNombre);
                    command.Parameters.AddWithValue("@SegundoNombre", string.IsNullOrEmpty(cliente.SegundoNombre) ? (object)DBNull.Value : cliente.SegundoNombre);
                    command.Parameters.AddWithValue("@PrimerApellido", cliente.PrimerApellido);
                    command.Parameters.AddWithValue("@SegundoApellido", string.IsNullOrEmpty(cliente.SegundoApellido) ? (object)DBNull.Value : cliente.SegundoApellido);
                    command.Parameters.AddWithValue("@Telefono", cliente.Telefono);
                    command.Parameters.AddWithValue("@Email", cliente.Email);
                    command.Parameters.AddWithValue("@Cedula", cliente.Cedula);

                    command.Parameters.AddWithValue("@Usuario", usuario.NombreUsuario);
                    command.Parameters.AddWithValue("@Contrasena", usuario.Contrasena);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}