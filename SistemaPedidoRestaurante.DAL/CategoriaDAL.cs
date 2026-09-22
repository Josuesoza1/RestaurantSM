using System;
using System.Data;
using System.Data.SqlClient;
using SistemaPedidoRestaurante.Conexion;
using SistemaPedidoRestaurante.Entities;

namespace SistemaPedidoRestaurante.DAL
{
    public class CategoriaDAL
    {
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
                                Descripcion = reader["descripcion"] == DBNull.Value ? string.Empty : reader["descripcion"].ToString()
                            });
                        }
                    }
                }
            }
            return lista;
        }
    }
}