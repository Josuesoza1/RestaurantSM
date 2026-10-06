using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using SistemaPedidoRestaurante.Entities;

namespace SistemaPedidoRestaurante.DAL
{
    public class PedidoDAL
    {
        public List<Pedido> ListarAbiertos()
        {
            List<Pedido> lista = new List<Pedido>();

            using (SqlConnection connection = ConexionBD.ObtenerConexion())
            {
                using (SqlCommand command = new SqlCommand("sp_Pedido_ListarAbiertos", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Pedido
                            {
                                IdPedido = Convert.ToInt32(reader["idPedido"]),
                                IdCliente = Convert.ToInt32(reader["idCliente"]),
                                IdMesa = reader["idMesa"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["idMesa"]),
                                IdEmpleado = Convert.ToInt32(reader["idEmpleado"]),
                                IdRepartidor = reader["idRepartidor"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["idRepartidor"]),
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
    }
}