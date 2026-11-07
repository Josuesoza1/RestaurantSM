using System;
using System.Data;
using System.Data.SqlClient;
using SistemaPedidoRestaurante.DAL;
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
                                IdPersona = reader["idPersona"] == DBNull.Value ? 0 : Convert.ToInt32(reader["idPersona"]),
                                NombreUsuario = reader["nombreUsuario"].ToString(),
                                IdRol = Convert.ToInt32(reader["idRol"]),
                                NombreRol = reader["NombreRol"].ToString(),
                                Cargo = reader["Cargo"].ToString(),
                                AccesoPersonal = Convert.ToBoolean(reader["AccesoPersonal"]),
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
                                IdPersona = reader["idPersona"] == DBNull.Value ? 0 : Convert.ToInt32(reader["idPersona"]),
                                NombreUsuario = reader["nombreUsuario"].ToString(),
                                IdRol = Convert.ToInt32(reader["idRol"]),
                                NombreRol = reader["NombreRol"].ToString(),
                                AccesoPersonal = Convert.ToBoolean(reader["AccesoPersonal"]),
                            });
                        }
                    }
                }
            }

            return listaDeUsuarios;
        }

        public Usuario CuentaParaPerfil(int actor, int persona, int rol)
        {
            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            using (SqlCommand comando = new SqlCommand("sp_Usuario_CuentaParaPerfil", conexion))
            {
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("@Actor", SqlDbType.Int).Value = actor;
                comando.Parameters.Add("@IdPersona", SqlDbType.Int).Value = persona;
                comando.Parameters.Add("@RolNuevo", SqlDbType.Int).Value = rol;
                conexion.Open();
                using (SqlDataReader lector = comando.ExecuteReader())
                {
                    if (!lector.Read()) return null;
                    return new Usuario { IdUsuario = Convert.ToInt32(lector["idUsuario"]),
                        IdPersona = persona, NombreUsuario = Convert.ToString(lector["nombreUsuario"]), IdRol = Convert.ToInt32(lector["idRol"]) };
                }
            }
        }


    }
}
