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
        internal const string ConsultaCatalogo = @"
            SELECT p.idProducto, p.idCategoria, p.idSubcategoria, p.nombre, p.precio,
                   p.descripcion, p.codigo, p.disponible, c.nombre AS CategoriaNombre,
                   s.nombre AS SubcategoriaNombre, c.activa AS CategoriaActiva, i.activo AS InventarioActivo,
                   COALESCE(i.stockActual, 0) AS stockActual, COALESCE(i.stockMinimo, 0) AS stockMinimo,
                   COALESCE(i.revision, 0) AS revision
            FROM dbo.producto p
            JOIN dbo.categoria c ON c.idCategoria = p.idCategoria
            LEFT JOIN dbo.subcategoria s ON s.idSubcategoria = p.idSubcategoria
            LEFT JOIN dbo.inventarioProducto i ON i.idProducto = p.idProducto";

        public void Eliminar(int idProducto, int idAdministrador)
        {
            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            {
                using (SqlCommand comando = new SqlCommand("sp_Producto_Eliminar", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.Add("@IdProducto", SqlDbType.Int).Value = idProducto;
                    comando.Parameters.Add("@IdAdministrador", SqlDbType.Int).Value = idAdministrador;
                    conexion.Open();
                    comando.ExecuteScalar();
                }
            }
        }

        public Productos Listar()
        {
            Productos lista = new Productos();
            using (SqlConnection connection = ConexionBD.ObtenerConexion())
            using (SqlCommand command = new SqlCommand(ConsultaCatalogo + " ORDER BY p.nombre;", connection))
            {
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                        lista.Add(LeerProducto(reader));
                }
            }

            return lista;
        }

        internal static Producto LeerProducto(SqlDataReader reader)
        {
            return new Producto
            {
                IdProducto = Convert.ToInt32(reader["idProducto"]),
                IdCategoria = Convert.ToInt32(reader["idCategoria"]),
                IdSubcategoria = reader["idSubcategoria"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["idSubcategoria"]),
                CategoriaNombre = reader["CategoriaNombre"].ToString(),
                SubcategoriaNombre = reader["SubcategoriaNombre"].ToString(),
                Nombre = reader["nombre"].ToString(),
                Precio = Convert.ToDecimal(reader["precio"]),
                Descripcion = reader["descripcion"].ToString(),
                Codigo = reader["codigo"].ToString(),
                Disponible = Convert.ToBoolean(reader["disponible"]),
                CategoriaActiva = Convert.ToBoolean(reader["CategoriaActiva"]),
                InventarioActivo = reader["InventarioActivo"] == DBNull.Value ? (bool?)null : Convert.ToBoolean(reader["InventarioActivo"]),
                StockActual = Convert.ToInt32(reader["stockActual"]),
                StockMinimo = Convert.ToInt32(reader["stockMinimo"])
            };
        }

        public List<Subcategoria> ListarSubcategorias()
        {
            List<Subcategoria> lista = new List<Subcategoria>();
            using (var connection = ConexionBD.ObtenerConexion())
            {
                using (SqlCommand command = new SqlCommand("sp_Subcategoria_Listar", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Subcategoria
                            {
                                IdSubcategoria = Convert.ToInt32(reader["idSubcategoria"]),
                                IdCategoria = Convert.ToInt32(reader["idCategoria"]),
                                Nombre = reader["nombre"].ToString()
                            });
                        }
                    }
                }
            }

            return lista;
        }

        public void Guardar(Producto producto, int idAdministrador)
        {
            using (var connection = ConexionBD.ObtenerConexion())
            {
                using (SqlCommand command = new SqlCommand("sp_Producto_Guardar", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@IdProducto", SqlDbType.Int).Value = producto.IdProducto;
                    command.Parameters.Add("@IdAdministrador", SqlDbType.Int).Value = idAdministrador;
                    command.Parameters.Add("@IdCategoria", SqlDbType.Int).Value = producto.IdCategoria;
                    command.Parameters.Add("@IdSubcategoria", SqlDbType.Int).Value = (object)producto.IdSubcategoria ?? DBNull.Value;
                    command.Parameters.Add("@Nombre", SqlDbType.VarChar, 100).Value = producto.Nombre;
                    var precio = command.Parameters.Add("@Precio", SqlDbType.Decimal);
                    precio.Precision = 10;
                    precio.Scale = 2;
                    precio.Value = producto.Precio;
                    command.Parameters.Add("@Descripcion", SqlDbType.VarChar, 250).Value = (object)producto.Descripcion ?? DBNull.Value;
                    command.Parameters.Add("@Codigo", SqlDbType.VarChar, 20).Value = producto.Codigo;
                    command.Parameters.Add("@Disponible", SqlDbType.Bit).Value = producto.Disponible;
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Desactivar(int idProducto, int idAdministrador)
        {
            using (var connection = ConexionBD.ObtenerConexion())
            {
                using (SqlCommand command = new SqlCommand("sp_Producto_Desactivar", connection))
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
}
