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
            for (int intento = 0; intento < 2; intento++)
            {
                Usuario usuario = LeerCredencial(nombreUsuario, out string guardada);
                if (usuario == null)
                    return null;

                if (ContrasenaProtegida.EsProtegida(guardada))
                    return ContrasenaProtegida.Coincide(contrasena, guardada) ? usuario : null;

                if (!string.Equals(contrasena, guardada, StringComparison.Ordinal))
                    return null;

                if (ActualizarClaveAnterior(usuario.IdUsuario, guardada, ContrasenaProtegida.Generar(contrasena)))
                    return usuario;
            }

            return null;
        }

        private Usuario LeerCredencial(string nombreUsuario, out string guardada)
        {
            const string consulta = @"
                SELECT a.*, u.contrasena
                FROM dbo.vw_UsuarioAccesos a
                JOIN dbo.usuario u ON u.idUsuario = a.idUsuario
                WHERE a.nombreUsuario = @NombreUsuario AND a.AccesoPersonal = 1;";

            guardada = null;
            Usuario usuarioAutenticado = null;
            using (SqlConnection connection = ConexionBD.ObtenerConexion())
            {
                using (SqlCommand command = new SqlCommand(consulta, connection))
                {
                    command.Parameters.Add("@NombreUsuario", SqlDbType.VarChar, 50).Value = nombreUsuario;
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            guardada = (string)reader["contrasena"];
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

        private bool ActualizarClaveAnterior(int usuario, string anterior, string protegida)
        {
            const string consulta = @"
                UPDATE dbo.usuario
                SET contrasena = @Protegida
                WHERE idUsuario = @Usuario
                    AND DATALENGTH(contrasena) = DATALENGTH(@Anterior)
                    AND CONVERT(varbinary(255), contrasena) = CONVERT(varbinary(255), @Anterior)
                    AND EXISTS (SELECT 1 FROM dbo.vw_UsuarioAccesos WHERE idUsuario = @Usuario AND AccesoPersonal = 1);";

            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            using (SqlCommand comando = new SqlCommand(consulta, conexion))
            {
                comando.Parameters.Add("@Usuario", SqlDbType.Int).Value = usuario;
                comando.Parameters.Add("@Anterior", SqlDbType.VarChar, 255).Value = anterior;
                comando.Parameters.Add("@Protegida", SqlDbType.VarChar, 255).Value = protegida;
                conexion.Open();
                return comando.ExecuteNonQuery() == 1;
            }
        }

        public Usuarios Listar(int administrador)
        {
            Usuarios listaDeUsuarios = new Usuarios();
            using (SqlConnection connection = ConexionBD.ObtenerConexion())
            {
                using (SqlCommand command = new SqlCommand("sp_Usuario_Listar", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@Actor", SqlDbType.Int).Value = administrador;
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
