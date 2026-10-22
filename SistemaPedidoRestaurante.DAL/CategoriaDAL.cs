using System;
using System.Data;
using System.Data.SqlClient;
using SistemaPedidoRestaurante.DAL;
using SistemaPedidoRestaurante.Entities;

namespace SistemaPedidoRestaurante.DAL
{
    public class CategoriaDAL
    {
        public void Eliminar(int idCategoria, int idAdministrador)
        {
            new GestionDAL().Ejecutar("sp_Categoria_Eliminar", "@IdCategoria", idCategoria, "@IdAdministrador", idAdministrador);
        }
        public Categorias Listar()
        {
            Categorias lista = new Categorias();

            using (SqlConnection connection = ConexionBD.ObtenerConexion())
            {
                using (SqlCommand command = new SqlCommand("sp_Categoria_Listar", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Categoria
                            {
                                IdCategoria = Convert.ToInt32(reader["idCategoria"]),
                                Nombre = reader["nombre"].ToString(),
                                Descripcion = reader["descripcion"] == DBNull.Value ? string.Empty : reader["descripcion"].ToString(),
                                Activa = Convert.ToBoolean(reader["activa"])
                            });
                        }
                    }
                }
            }
            return lista;
        }

        public void Guardar(Categoria categoria, int idAdministrador)
        {
            using (var connection = ConexionBD.ObtenerConexion())
            using (var command = new SqlCommand("sp_Categoria_Guardar", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@IdCategoria", SqlDbType.Int).Value = categoria.IdCategoria;
                command.Parameters.Add("@IdAdministrador", SqlDbType.Int).Value = idAdministrador;
                command.Parameters.Add("@Nombre", SqlDbType.VarChar, 60).Value = categoria.Nombre;
                command.Parameters.Add("@Descripcion", SqlDbType.VarChar, 200).Value = (object)categoria.Descripcion ?? DBNull.Value;
                command.Parameters.Add("@Activa", SqlDbType.Bit).Value = categoria.Activa;
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Desactivar(int idCategoria, int idAdministrador)
        {
            using (var connection = ConexionBD.ObtenerConexion())
            using (var command = new SqlCommand("sp_Categoria_Desactivar", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@IdCategoria", SqlDbType.Int).Value = idCategoria;
                command.Parameters.Add("@IdAdministrador", SqlDbType.Int).Value = idAdministrador;
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
