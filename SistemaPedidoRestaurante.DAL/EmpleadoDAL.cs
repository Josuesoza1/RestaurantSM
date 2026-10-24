using System;
using System.Data;
using System.Data.SqlClient;
using SistemaPedidoRestaurante.DAL;
using SistemaPedidoRestaurante.Entities;

namespace SistemaPedidoRestaurante.DAL
{
    public class EmpleadoDAL
    {
        public void Eliminar(int idEmpleado, int idAdministrador)
        {
            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            {
                using (SqlCommand comando = new SqlCommand("sp_Empleado_Eliminar", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.Add("@IdEmpleado", SqlDbType.Int).Value = idEmpleado;
                    comando.Parameters.Add("@IdAdministrador", SqlDbType.Int).Value = idAdministrador;
                    conexion.Open();
                    comando.ExecuteScalar();
                }
            }
        }

        public Empleados Listar()
        {
            Empleados lista = new Empleados();
            using (SqlConnection connection = ConexionBD.ObtenerConexion())
            {
                using (SqlCommand command = new SqlCommand("sp_Empleado_Listar", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Empleado
                            {
                                IdEmpleado = Convert.ToInt32(reader["idEmpleado"]),
                                IdUsuario = reader["idUsuario"] == DBNull.Value ? (int? )null : Convert.ToInt32(reader["idUsuario"]),
                                NombreUsuario = reader["nombreUsuario"].ToString(),
                                IdRol = reader["idRol"] == DBNull.Value ? (int? )null : Convert.ToInt32(reader["idRol"]),
                                NombreRol = reader["NombreRol"].ToString(),
                                PrimerNombre = reader["pNombreEmp"].ToString(),
                                SegundoNombre = reader["sNombreEmp"] == DBNull.Value ? string.Empty : reader["sNombreEmp"].ToString(),
                                PrimerApellido = reader["pApellidoEmp"].ToString(),
                                SegundoApellido = reader["sApellidoEmp"] == DBNull.Value ? string.Empty : reader["sApellidoEmp"].ToString(),
                                Cargo = reader["cargo"].ToString(),
                                Genero = reader["genero"].ToString(),
                                Cedula = reader["cedula"].ToString(),
                                Telefono = reader["telefono"].ToString(),
                                Activo = Convert.ToBoolean(reader["activo"])
                            });
                        }
                    }
                }
            }

            return lista;
        }

        public void Guardar(Empleado empleado, Usuario usuario, int idAdministrador, bool nuevo)
        {
            using (SqlConnection connection = ConexionBD.ObtenerConexion())
            {
                using (SqlCommand command = new SqlCommand(nuevo ? "sp_Empleado_Insertar" : "sp_Empleado_Actualizar", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    if (!nuevo)
                    {
                        command.Parameters.Add("@IdEmpleado", SqlDbType.Int).Value = empleado.IdEmpleado;
                    }

                    command.Parameters.Add("@IdAdministrador", SqlDbType.Int).Value = idAdministrador;
                    Texto(command, "@PrimerNombre", 80, empleado.PrimerNombre);
                    Texto(command, "@SegundoNombre", 80, empleado.SegundoNombre);
                    Texto(command, "@PrimerApellido", 80, empleado.PrimerApellido);
                    Texto(command, "@SegundoApellido", 80, empleado.SegundoApellido);
                    Texto(command, "@Cargo", 50, empleado.Cargo);
                    command.Parameters.Add("@Genero", SqlDbType.Char, 1).Value = empleado.Genero;
                    Texto(command, "@Cedula", 30, empleado.Cedula);
                    Texto(command, "@Telefono", 20, empleado.Telefono);
                    Texto(command, "@Usuario", 50, usuario.NombreUsuario);
                    Texto(command, "@Contrasena", 255, usuario.Contrasena);
                    command.Parameters.Add("@IdRol", SqlDbType.Int).Value = usuario.IdRol;
                    command.Parameters.Add("@Activo", SqlDbType.Bit).Value = empleado.Activo;
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Desactivar(int idEmpleado, int idAdministrador)
        {
            using (SqlConnection connection = ConexionBD.ObtenerConexion())
            {
                using (SqlCommand command = new SqlCommand("sp_Empleado_Desactivar", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@IdEmpleado", SqlDbType.Int).Value = idEmpleado;
                    command.Parameters.Add("@IdAdministrador", SqlDbType.Int).Value = idAdministrador;
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        private void Texto(SqlCommand command, string nombre, int longitud, string valor)
        {
            command.Parameters.Add(nombre, SqlDbType.VarChar, longitud).Value = string.IsNullOrEmpty(valor) ? (object)DBNull.Value : valor;
        }
    }
}
