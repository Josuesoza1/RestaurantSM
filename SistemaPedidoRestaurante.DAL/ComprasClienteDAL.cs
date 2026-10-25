using System;
using System.Data;
using System.Collections.Generic;
using SistemaPedidoRestaurante.Entities;
using System.Data.SqlClient;

namespace SistemaPedidoRestaurante.DAL
{
    public class ComprasClienteDAL
    {
        public DataTable Listar(int actor)
        {
            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            {
                using (SqlCommand comando = new SqlCommand("sp_Cliente_Pedidos", conexion))
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
                using (SqlCommand comando = new SqlCommand("sp_Cliente_Detalle", conexion))
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

        public int Guardar(int actor, int id, Guid solicitud, IEnumerable<LineaPedidoBorrador> lineas)
        {
            return new PedidoDAL().Guardar(actor, 0, id, solicitud, lineas);
        }

        public int Cancelar(int actor, int id)
        {
            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            {
                using (SqlCommand comando = new SqlCommand("sp_Cliente_Pedido_Cancelar", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.Add("@Actor", SqlDbType.Int).Value = actor;
                    comando.Parameters.Add("@IdPedido", SqlDbType.Int).Value = id;
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
