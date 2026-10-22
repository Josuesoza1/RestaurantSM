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
            return new GestionDAL().Ejecutar("sp_Inventario_Eliminar", "@Actor", idAdministrador,
                "@IdProducto", idProducto, "@Version", version);
        }
        public InventarioProductos Listar(int idAdministrador)
        {
            var inventario = new InventarioProductos();
            using (var conexion = ConexionBD.ObtenerConexion())
            using (var comando = new SqlCommand("sp_Inventario_Listar", conexion))
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
            return inventario;
        }

        public int Guardar(InventarioProducto inventario, int idAdministrador)
        {
            return new GestionDAL().Ejecutar("sp_Inventario_Guardar",
                "@Actor", idAdministrador, "@IdProducto", inventario.IdProducto,
                "@Stock", inventario.StockActual, "@Minimo", inventario.StockMinimo,
                "@Activo", inventario.Activo, "@Version", inventario.Version);
        }
    }
}
