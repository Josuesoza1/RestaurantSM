using System;
using System.Data;
using System.Data.SqlClient;
using SistemaPedidoRestaurante.DAL;
using SistemaPedidoRestaurante.Entities;

namespace SistemaPedidoRestaurante.DAL
{
    public class RolDAL
    {
        public int Guardar(Rol rol, int idAdministrador)
        {
            return new GestionDAL().Ejecutar("sp_Rol_Guardar", "@Actor", idAdministrador,
                "@Id", rol.IdRol, "@Nombre", rol.Nombre, "@Descripcion", rol.Descripcion);
        }

        public int Eliminar(int idRol, int idAdministrador)
        {
            return new GestionDAL().Ejecutar("sp_Rol_Eliminar", "@Actor", idAdministrador, "@Id", idRol);
        }
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
