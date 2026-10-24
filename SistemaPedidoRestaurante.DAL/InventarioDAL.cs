using System;
using System.Data;
using System.Data.SqlClient;
using SistemaPedidoRestaurante.Entities;

namespace SistemaPedidoRestaurante.DAL
{
    public class InventarioDAL
    {
        public int Eliminar(int idProducto, byte[] version, int idAdministrador)
        {
            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            {
                using (SqlCommand comando = new SqlCommand("sp_Inventario_Eliminar", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.Add("@Actor", SqlDbType.Int).Value = idAdministrador;
                    comando.Parameters.Add("@IdProducto", SqlDbType.Int).Value = idProducto;
                    comando.Parameters.Add("@Version", SqlDbType.Binary, 8).Value = (object)(version) ?? DBNull.Value;
                    conexion.Open();
                    object resultado = comando.ExecuteScalar();
                    if (resultado == null || resultado == DBNull.Value)
                    {
                        return 0;
                    }

                    return Convert.ToInt32(resultado);
                }
            }
        }

        public InventarioProductos Listar(int idAdministrador)
        {
            InventarioProductos inventario = new InventarioProductos();
            using (var conexion = ConexionBD.ObtenerConexion())
            {
                using (SqlCommand comando = new SqlCommand("sp_Inventario_Listar", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.Add("@Actor", SqlDbType.Int).Value = idAdministrador;
                    conexion.Open();
                    using (var lector = comando.ExecuteReader())
                    {
                        while (lector.Read())
                        {
                            inventario.Add(new InventarioProducto
                            {
                                IdProducto = Convert.ToInt32(lector["IdProducto"]),
                                Producto = lector["Producto"].ToString(),
                                Categoria = lector["Categoria"].ToString(),
                                Unidad = lector["Unidad"].ToString(),
                                PrecioVenta = Convert.ToDecimal(lector["PrecioVenta"]),
                                StockActual = Convert.ToInt32(lector["StockActual"]),
                                StockMinimo = Convert.ToInt32(lector["StockMinimo"]),
                                Activo = Convert.ToBoolean(lector["Activo"]),
                                Version = (byte[])lector["Version"]
                            });
                        }
                    }
                }
            }

            return inventario;
        }

        public int Guardar(InventarioProducto inventario, int idAdministrador)
        {
            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            {
                using (SqlCommand comando = new SqlCommand("sp_Inventario_Guardar", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.Add("@Actor", SqlDbType.Int).Value = idAdministrador;
                    comando.Parameters.Add("@IdProducto", SqlDbType.Int).Value = inventario.IdProducto;
                    comando.Parameters.Add("@Stock", SqlDbType.Int).Value = inventario.StockActual;
                    comando.Parameters.Add("@Minimo", SqlDbType.Int).Value = inventario.StockMinimo;
                    comando.Parameters.Add("@Activo", SqlDbType.Bit).Value = inventario.Activo;
                    comando.Parameters.Add("@Version", SqlDbType.Binary, 8).Value = (object)(inventario.Version) ?? DBNull.Value;
                    conexion.Open();
                    object resultado = comando.ExecuteScalar();
                    if (resultado == null || resultado == DBNull.Value)
                    {
                        return 0;
                    }

                    return Convert.ToInt32(resultado);
                }
            }
        }
    }
}
