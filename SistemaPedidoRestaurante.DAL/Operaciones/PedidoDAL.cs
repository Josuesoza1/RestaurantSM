using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using SistemaPedidoRestaurante.Entities;

namespace SistemaPedidoRestaurante.DAL
{
    public class PedidoDAL
    {
        public DataTable OpcionesEntrega(int actor, int pedido, bool comoCliente)
        {
            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            using (SqlCommand comando = new SqlCommand("sp_Pedido_OpcionesEntrega", conexion))
            {
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("@Actor", SqlDbType.Int).Value = actor;
                comando.Parameters.Add("@ComoCliente", SqlDbType.Bit).Value = comoCliente;
                comando.Parameters.Add("@IdPedido", SqlDbType.Int).Value = pedido;
                using (SqlDataAdapter adaptador = new SqlDataAdapter(comando))
                {
                    DataTable tabla = new DataTable();
                    adaptador.Fill(tabla);
                    return tabla;
                }
            }
        }

        public DataTable ObtenerEntrega(int actor, int pedido, bool comoCliente)
        {
            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            using (SqlCommand comando = new SqlCommand("sp_Pedido_ObtenerEntrega", conexion))
            {
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("@Actor", SqlDbType.Int).Value = actor;
                comando.Parameters.Add("@ComoCliente", SqlDbType.Bit).Value = comoCliente;
                comando.Parameters.Add("@IdPedido", SqlDbType.Int).Value = pedido;
                using (SqlDataAdapter adaptador = new SqlDataAdapter(comando))
                {
                    DataTable tabla = new DataTable();
                    adaptador.Fill(tabla);
                    return tabla;
                }
            }
        }
        public int Guardar(int actor, int cliente, int idPedido, Guid solicitud, IEnumerable<LineaPedidoBorrador> lineas, EntregaPedido entrega, bool comoCliente = false)
        {
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
                            comando.Parameters.Add("@ComoCliente", SqlDbType.Bit).Value = comoCliente;
                            comando.Parameters.Add("@IdCliente", SqlDbType.Int).Value = cliente == 0 ? (object)DBNull.Value : cliente;
                            comando.Parameters.Add("@IdPedido", SqlDbType.Int).Value = idPedido;
                            comando.Parameters.Add("@Solicitud", SqlDbType.UniqueIdentifier).Value = solicitud;
                            comando.Parameters.Add("@NumeroLineas", SqlDbType.Int).Value = detalle.Count;
                            comando.Parameters.Add("@IdTipoEntrega", SqlDbType.Int).Value = entrega.IdTipoEntrega;
                            comando.Parameters.Add("@Direccion", SqlDbType.VarChar, 250).Value = (object)entrega.Direccion ?? DBNull.Value;
                            comando.Parameters.Add("@Referencia", SqlDbType.VarChar, 150).Value = (object)entrega.Referencia ?? DBNull.Value;
                            comando.Parameters.Add("@TelefonoContacto", SqlDbType.VarChar, 20).Value = (object)entrega.TelefonoContacto ?? DBNull.Value;
                            SqlParameter envio = comando.Parameters.Add("@EnvioEsperado", SqlDbType.Decimal);
                            envio.Precision = 10; envio.Scale = 2; envio.Value = entrega.CostoEnvioEsperado;
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
                            comando.Parameters.Add("@ComoCliente", SqlDbType.Bit).Value = comoCliente;
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
                            comando.Parameters.Add("@ComoCliente", SqlDbType.Bit).Value = comoCliente;
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

        public List<Pedido> ListarAbiertos(int actor)
        {
            List<Pedido> lista = new List<Pedido>();
            using (SqlConnection connection = ConexionBD.ObtenerConexion())
            {
                using (SqlCommand command = new SqlCommand("sp_Pedido_ListarAbiertos", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@Actor", SqlDbType.Int).Value = actor;
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Pedido
                            {
                                IdPedido = Convert.ToInt32(reader["idPedido"]),
                                IdCliente = Convert.ToInt32(reader["idCliente"]),
                                IdMesa = reader["idMesa"] == DBNull.Value ? (int? )null : Convert.ToInt32(reader["idMesa"]),
                                IdEmpleadoAtiende = reader["idEmpleado"] == DBNull.Value ? (int? )null : Convert.ToInt32(reader["idEmpleado"]),
                                IdUsuarioRegistro = reader["idUsuarioRegistro"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["idUsuarioRegistro"]),
                                IdOrigenPedido = Convert.ToInt32(reader["idOrigenPedido"]),
                                IdTipoEntrega = Convert.ToInt32(reader["idTipoEntrega"]),
                                IdEmpleadoRepartidor = reader["idEmpleadoRepartidor"] == DBNull.Value ? (int? )null : Convert.ToInt32(reader["idEmpleadoRepartidor"]),
                                Direccion = reader["direccion"] == DBNull.Value ? string.Empty : reader["direccion"].ToString(),
                                Referencia = reader["referencia"] == DBNull.Value ? string.Empty : reader["referencia"].ToString(),
                                TelefonoDeContacto = reader["telefonoDeContacto"] == DBNull.Value ? string.Empty : reader["telefonoDeContacto"].ToString(),
                                FechaHora = Convert.ToDateTime(reader["fechaHora"]),
                                Estado = reader["estado"].ToString(),
                                EsDelivery = Convert.ToBoolean(reader["esDelivery"]),
                                CostoEnvio = Convert.ToDecimal(reader["costoEnvio"]),
                                Subtotal = Convert.ToDecimal(reader["subtotal"]),
                                Total = Convert.ToDecimal(reader["total"])
                            });
                        }
                    }
                }
            }

            return lista;
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
