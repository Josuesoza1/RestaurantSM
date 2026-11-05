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
                                TienePerfilCliente = Convert.ToBoolean(reader["TienePerfilCliente"]),
                                ClienteActivo = Convert.ToBoolean(reader["ClienteActivo"]),
                                Email = Convert.ToString(reader["Email"]), Telefono = Convert.ToString(reader["Telefono"])
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
                                TienePerfilCliente = Convert.ToBoolean(reader["TienePerfilCliente"]),
                                ClienteActivo = Convert.ToBoolean(reader["ClienteActivo"])
                            });
                        }
                    }
                }
            }

            return listaDeUsuarios;
        }

        public Usuario ConsultarAccesos(int actor)
        {
            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            using (SqlCommand comando = new SqlCommand("sp_Usuario_Accesos", conexion))
            {
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("@Actor", SqlDbType.Int).Value = actor;
                conexion.Open();
                using (SqlDataReader lector = comando.ExecuteReader())
                {
                    if (!lector.Read()) return null;
                    return new Usuario
                    {
                        IdUsuario = actor, IdPersona = Convert.ToInt32(lector["idPersona"]),
                        NombreUsuario = Convert.ToString(lector["nombreUsuario"]), IdRol = Convert.ToInt32(lector["idRol"]),
                        NombreRol = Convert.ToString(lector["NombreRol"]), Cargo = Convert.ToString(lector["Cargo"]),
                        AccesoPersonal = Convert.ToBoolean(lector["AccesoPersonal"]),
                        TienePerfilCliente = Convert.ToBoolean(lector["TienePerfilCliente"]), ClienteActivo = Convert.ToBoolean(lector["ClienteActivo"]),
                        Email = Convert.ToString(lector["Email"]), Telefono = Convert.ToString(lector["Telefono"])
                    };
                }
            }
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

        public void HabilitarCliente(int actor, int usuario, string correo, string telefono)
        {
            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            using (SqlCommand comando = new SqlCommand("sp_Usuario_HabilitarCliente", conexion))
            {
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("@Actor", SqlDbType.Int).Value = actor;
                comando.Parameters.Add("@IdUsuario", SqlDbType.Int).Value = usuario;
                comando.Parameters.Add("@Email", SqlDbType.VarChar, 120).Value = correo;
                comando.Parameters.Add("@Telefono", SqlDbType.VarChar, 20).Value = telefono;
                conexion.Open();
                comando.ExecuteNonQuery();
            }
        }
    }
}
