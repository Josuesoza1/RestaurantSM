using System;
using System.Data;
using System.Data.SqlClient;
using SistemaPedidoRestaurante.Conexion;
using SistemaPedidoRestaurante.Entities;

namespace SistemaPedidoRestaurante.DAL
{
    public class UsuarioDAL
    {
        public Usuario ValidarLogin(string nombreUsuario, string contrasena)
        {
            Usuario usuarioAutenticado = null;

            using (SqlConnection connection = ConexionBD.ObtenerConexion())
            {
                using (SqlCommand command = new SqlCommand("sp_Usuario_ValidarLogin", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@NombreUsuario", nombreUsuario);
                    command.Parameters.AddWithValue("@Contrasena", contrasena);

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            usuarioAutenticado = new Usuario
                            {
                                IdUsuario = Convert.ToInt32(reader["idUsuario"]),
                                NombreUsuario = reader["nombreUsuario"].ToString(),
                                IdRol = Convert.ToInt32(reader["idRol"]),
                                NombreRol = reader["NombreRol"].ToString()
                            };
                        }
                    }
                }
            }
            return usuarioAutenticado;
        }


        public Usuarios Listar()
        {
            Usuarios listaDeUsuarios = new Usuarios();

            using (SqlConnection connection = ConexionBD.ObtenerConexion())
            {
                using (SqlCommand command = new SqlCommand("sp_Usuario_Listar", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            listaDeUsuarios.Add(new Usuario
                            {
                                IdUsuario = Convert.ToInt32(reader["idUsuario"]),
                                NombreUsuario = reader["nombreUsuario"].ToString(),
                                IdRol = Convert.ToInt32(reader["idRol"]),
                                NombreRol = reader["NombreRol"].ToString()
                            });
                        }
                    }
                }
            }
            return listaDeUsuarios;
        }
    }

}