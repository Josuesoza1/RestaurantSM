using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using SistemaPedidoRestaurante.DAL;
using SistemaPedidoRestaurante.Entities;

namespace SistemaPedidoRestaurante.DAL
{
    public class ProductoDAL
    {
        public Productos Listar()
        {
            return Consultar(false);
        }

        public Productos ListarDisponibles()
        {
            return Consultar(true);
        }

        private Productos Consultar(bool soloDisponibles)
        {
            Productos lista = new Productos();

            using (SqlConnection connection = ConexionBD.ObtenerConexion())
            {
                string consulta = soloDisponibles
                    ? "SELECT p.idProducto,p.idCategoria,p.idSubcategoria,c.nombre AS CategoriaNombre,s.nombre AS SubcategoriaNombre,p.nombre,p.precio,p.descripcion,p.codigo,p.disponible FROM dbo.producto p INNER JOIN dbo.categoria c ON c.idCategoria=p.idCategoria LEFT JOIN dbo.subcategoria s ON s.idSubcategoria=p.idSubcategoria WHERE p.disponible=1 AND c.activa=1 ORDER BY c.nombre,p.nombre"
                    : "sp_Producto_Listar";
                using (SqlCommand command = new SqlCommand(consulta, connection))
                {
                    command.CommandType = soloDisponibles ? CommandType.Text : CommandType.StoredProcedure;
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Producto
                            {
                                IdProducto = Convert.ToInt32(reader["idProducto"]),
                                IdCategoria = Convert.ToInt32(reader["idCategoria"]),
                                IdSubcategoria = reader["idSubcategoria"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["idSubcategoria"]),
                                CategoriaNombre = reader["CategoriaNombre"].ToString(),
                                SubcategoriaNombre = reader["SubcategoriaNombre"].ToString(),
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

        public List<Subcategoria> ListarSubcategorias()
        {
            var lista = new List<Subcategoria>();
            using (var connection = ConexionBD.ObtenerConexion())
            using (var command = new SqlCommand("sp_Subcategoria_Listar", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                connection.Open();
                using (var reader = command.ExecuteReader())
                    while (reader.Read())
                        lista.Add(new Subcategoria { IdSubcategoria = Convert.ToInt32(reader["idSubcategoria"]),
                            IdCategoria = Convert.ToInt32(reader["idCategoria"]), Nombre = reader["nombre"].ToString() });
            }
            return lista;
        }

        public void Guardar(Producto producto, int idAdministrador)
        {
            using (var connection = ConexionBD.ObtenerConexion())
            using (var command = new SqlCommand("sp_Producto_Guardar", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@IdProducto", SqlDbType.Int).Value = producto.IdProducto;
                command.Parameters.Add("@IdAdministrador", SqlDbType.Int).Value = idAdministrador;
                command.Parameters.Add("@IdCategoria", SqlDbType.Int).Value = producto.IdCategoria;
                command.Parameters.Add("@IdSubcategoria", SqlDbType.Int).Value = (object)producto.IdSubcategoria ?? DBNull.Value;
                command.Parameters.Add("@Nombre", SqlDbType.VarChar, 100).Value = producto.Nombre;
                var precio = command.Parameters.Add("@Precio", SqlDbType.Decimal);
                precio.Precision = 10; precio.Scale = 2; precio.Value = producto.Precio;
                command.Parameters.Add("@Descripcion", SqlDbType.VarChar, 250).Value = (object)producto.Descripcion ?? DBNull.Value;
                command.Parameters.Add("@Codigo", SqlDbType.VarChar, 20).Value = producto.Codigo;
                command.Parameters.Add("@Disponible", SqlDbType.Bit).Value = producto.Disponible;
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Desactivar(int idProducto, int idAdministrador)
        {
            using (var connection = ConexionBD.ObtenerConexion())
            using (var command = new SqlCommand("sp_Producto_Desactivar", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@IdProducto", SqlDbType.Int).Value = idProducto;
                command.Parameters.Add("@IdAdministrador", SqlDbType.Int).Value = idAdministrador;
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
