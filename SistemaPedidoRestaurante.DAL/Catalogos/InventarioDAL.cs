using System;
using System.Data;
using System.Data.SqlClient;
using SistemaPedidoRestaurante.Entities;

namespace SistemaPedidoRestaurante.DAL
{
    public class InventarioDAL
    {
        public int Eliminar(int idProducto, int revision, int idAdministrador)
        {
            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            {
                using (SqlCommand comando = new SqlCommand("sp_Inventario_Eliminar", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.Add("@Actor", SqlDbType.Int).Value = idAdministrador;
                    comando.Parameters.Add("@IdProducto", SqlDbType.Int).Value = idProducto;
                    comando.Parameters.Add("@Revision", SqlDbType.Int).Value = revision;
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

        public InventarioProductos Listar(int idAdministrador, bool incluirSinControl)
        {
            const string consulta = "EXEC dbo.sp_Catalogo_ExigirAdministrador @Actor; " + ProductoDAL.ConsultaCatalogo +
                " WHERE @IncluirSinControl = 1 OR i.idProducto IS NOT NULL ORDER BY p.nombre;";
            InventarioProductos inventario = new InventarioProductos();
            using (var conexion = ConexionBD.ObtenerConexion())
            {
                using (SqlCommand comando = new SqlCommand(consulta, conexion))
                {
                    comando.Parameters.Add("@Actor", SqlDbType.Int).Value = idAdministrador;
                    comando.Parameters.Add("@IncluirSinControl", SqlDbType.Bit).Value = incluirSinControl;
                    conexion.Open();
                    using (var lector = comando.ExecuteReader())
                    {
                        while (lector.Read())
                        {
                            Producto producto = ProductoDAL.LeerProducto(lector);
                            inventario.Add(new InventarioProducto
                            {
                                IdProducto = producto.IdProducto,
                                Producto = producto.Nombre,
                                Categoria = producto.CategoriaNombre,
                                Unidad = "Unidad",
                                PrecioVenta = producto.Precio,
                                StockActual = producto.StockActual,
                                StockMinimo = producto.StockMinimo,
                                Activo = producto.InventarioActivo == true,
                                TieneControl = producto.InventarioActivo.HasValue,
                                EstadoVenta = producto.EstadoVenta,
                                Revision = Convert.ToInt32(lector["Revision"])
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
                    comando.Parameters.Add("@Revision", SqlDbType.Int).Value = inventario.Revision;
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

        public DataTable ListarMovimientos(int actor)
        {
            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            {
                using (SqlCommand comando = new SqlCommand("sp_Inventario_Movimientos", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.Add("@Actor", SqlDbType.Int).Value = actor;
                    using (SqlDataAdapter adaptador = new SqlDataAdapter(comando))
                    {
                        DataTable tabla = new DataTable();
                        adaptador.Fill(tabla);
                        return tabla;
                    }
                }
            }
        }

        public void AjustarInventario(int actor, int producto, int cantidad, string tipo, string motivo, int revision)
        {
            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            {
                using (SqlCommand comando = new SqlCommand("sp_Inventario_Ajustar", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.Add("@Actor", SqlDbType.Int).Value = actor;
                    comando.Parameters.Add("@IdProducto", SqlDbType.Int).Value = producto;
                    comando.Parameters.Add("@Cantidad", SqlDbType.Int).Value = cantidad;
                    comando.Parameters.Add("@Tipo", SqlDbType.VarChar, 20).Value = tipo;
                    comando.Parameters.Add("@Motivo", SqlDbType.NVarChar, 200).Value = motivo;
                    comando.Parameters.Add("@Revision", SqlDbType.Int).Value = revision;
                    conexion.Open();

                    comando.ExecuteNonQuery();
                }
            }
        }
    }
}
