using System;
using System.Data;
using System.Data.SqlClient;
using SistemaPedidoRestaurante.Conexion;
using SistemaPedidoRestaurante.Entities;

namespace SistemaPedidoRestaurante.DAL
{
    public class ProductoDAL
    {
        public Productos Listar()
        {
            Productos lista = new Productos();

            using (SqlConnection connection = ConexionBD.ObtenerConexion())
            {
                using (SqlCommand command = new SqlCommand("sp_Producto_Listar", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Producto
                            {
                                IdProducto = Convert.ToInt32(reader["idProducto"]),
                                IdCategoria = Convert.ToInt32(reader["idCategoria"]),
                                CategoriaNombre = reader["CategoriaNombre"].ToString(),
                                Nombre = reader["nombre"].ToString(),
                                Precio = Convert.ToDecimal(reader["precio"]),
                                Descripcion = reader["descripcion"] == DBNull.Value ? string.Empty : reader["descripcion"].ToString(),
                                Codigo = reader["codigo"].ToString(),
                                Disponible = Convert.ToBoolean(reader["disponible"])
                            });
                        }
                    }
                }
            }
            return lista;
        }
    }
}