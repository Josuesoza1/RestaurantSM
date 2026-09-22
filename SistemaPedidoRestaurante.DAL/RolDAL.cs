using System;
using System.Data;
using System.Data.SqlClient;
using SistemaPedidoRestaurante.Conexion;
using SistemaPedidoRestaurante.Entities;

namespace SistemaPedidoRestaurante.DAL
{
    public class RolDAL
    {
        public Roles Listar()
        {
            Roles lista = new Roles();

            using (SqlConnection connection = ConexionBD.ObtenerConexion())
            {
                using (SqlCommand command = new SqlCommand("sp_Rol_Listar", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Rol
                            {
                                IdRol = Convert.ToInt32(reader["idRol"]),
                                Nombre = reader["nombre"].ToString(),
                                Descripcion = reader["descripcion"] == DBNull.Value ? null : reader["descripcion"].ToString()
                            });
                        }
                    }
                }
            }
            return lista;
        }
    }
}