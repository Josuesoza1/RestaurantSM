using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using SistemaPedidoRestaurante.Entities;

namespace SistemaPedidoRestaurante.DAL
{
    public class PedidoDAL
    {

        public int Guardar(int actor, int cliente, Guid solicitud, IEnumerable<LineaPedidoBorrador> lineas)
        {
            int idPedido;
            List<LineaPedidoBorrador> detalle = new List<LineaPedidoBorrador>(lineas);
            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            {
                conexion.Open();
                using (SqlTransaction transaccion = conexion.BeginTransaction(IsolationLevel.Serializable))
                {
                    try
                    {
                        bool reintento;
                        using (SqlCommand comando = new SqlCommand("sp_Pedido_Iniciar", conexion, transaccion))
                        {
                            comando.CommandType = CommandType.StoredProcedure;
                            comando.Parameters.Add("@Actor", SqlDbType.Int).Value = actor;
                            comando.Parameters.Add("@IdCliente", SqlDbType.Int).Value = cliente == 0 ? (object)DBNull.Value : cliente;
                            comando.Parameters.Add("@Solicitud", SqlDbType.UniqueIdentifier).Value = solicitud;
                            comando.Parameters.Add("@NumeroLineas", SqlDbType.Int).Value = detalle.Count;
                            using (SqlDataReader lector = comando.ExecuteReader())
                            {
                                if (!lector.Read()) throw new InvalidOperationException("No se pudo iniciar el pedido.");
                                idPedido = Convert.ToInt32(lector["IdPedido"]);
                                reintento = Convert.ToBoolean(lector["Reintento"]);
                            }
                        }

                        foreach (LineaPedidoBorrador linea in detalle)
                        {
                            using (SqlCommand comando = new SqlCommand("sp_Pedido_AgregarLinea", conexion, transaccion))
                            {
                                comando.CommandType = CommandType.StoredProcedure;
                                comando.Parameters.Add("@Actor", SqlDbType.Int).Value = actor;
                                comando.Parameters.Add("@IdPedido", SqlDbType.Int).Value = idPedido;
                                comando.Parameters.Add("@IdProducto", SqlDbType.Int).Value = linea.IdProducto;
                                comando.Parameters.Add("@Cantidad", SqlDbType.Int).Value = linea.Cantidad;
                                SqlParameter precio = comando.Parameters.Add("@Precio", SqlDbType.Decimal);
                                precio.Precision = 10;
                                precio.Scale = 2;
                                precio.Value = linea.PrecioUnitario;
                                comando.Parameters.Add("@Reintento", SqlDbType.Bit).Value = reintento;
                                comando.ExecuteNonQuery();
                            }
                        }

                        using (SqlCommand comando = new SqlCommand("sp_Pedido_Finalizar", conexion, transaccion))
                        {
                            comando.CommandType = CommandType.StoredProcedure;
                            comando.Parameters.Add("@Actor", SqlDbType.Int).Value = actor;
                            comando.Parameters.Add("@IdPedido", SqlDbType.Int).Value = idPedido;
                            comando.Parameters.Add("@NumeroLineas", SqlDbType.Int).Value = detalle.Count;
                            comando.Parameters.Add("@Reintento", SqlDbType.Bit).Value = reintento;
                            comando.ExecuteNonQuery();
                        }
                        transaccion.Commit();
                        return idPedido;
                    }
                    catch
                    {
                        if (transaccion.Connection != null) transaccion.Rollback();
                        throw;
                    }
                }
            }
        }

        public DataTable ListarPedidos(int actor)
        {
            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            {
                using (SqlCommand comando = new SqlCommand("sp_Operacion_Pedidos", conexion))
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

        public DataTable Detalle(int actor, int pedido)
        {
            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            {
                using (SqlCommand comando = new SqlCommand("sp_Operacion_Detalle", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.Add("@Actor", SqlDbType.Int).Value = actor;
                    comando.Parameters.Add("@IdPedido", SqlDbType.Int).Value = pedido;
                    using (SqlDataAdapter adaptador = new SqlDataAdapter(comando))
                    {
                        DataTable tabla = new DataTable();
                        adaptador.Fill(tabla);
                        return tabla;
                    }
                }
            }
        }

        public void CambiarEstadoPedido(int actor, int pedido, string anterior, string nuevo)
        {
            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            {
                using (SqlCommand comando = new SqlCommand("sp_Operacion_EstadoPedido", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.Add("@Actor", SqlDbType.Int).Value = actor;
                    comando.Parameters.Add("@IdPedido", SqlDbType.Int).Value = pedido;
                    comando.Parameters.Add("@Anterior", SqlDbType.VarChar, 20).Value = anterior;
                    comando.Parameters.Add("@Nuevo", SqlDbType.VarChar, 20).Value = nuevo;
                    conexion.Open();
                    comando.ExecuteNonQuery();
                }
            }
        }

        public void CancelarPedido(int actor, int pedido)
        {
            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            {
                using (SqlCommand comando = new SqlCommand("sp_Pedido_CancelarComun", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.Add("@Actor", SqlDbType.Int).Value = actor;
                    comando.Parameters.Add("@IdPedido", SqlDbType.Int).Value = pedido;
                    conexion.Open();

                    comando.ExecuteNonQuery();
                }
            }
        }
    }
}
