using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
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
            // El procedimiento recibe todas las lineas juntas para guardarlas en una transaccion.
            XElement xml = new XElement("pedido");
            foreach (LineaPedidoBorrador linea in lineas)
            {
                XElement item = new XElement("item");
                item.Add(new XAttribute("producto", linea.IdProducto));
                item.Add(new XAttribute("cantidad", linea.Cantidad));
                item.Add(new XAttribute("precio", linea.PrecioUnitario));
                xml.Add(item);
            }

            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            {
                using (SqlCommand comando = new SqlCommand("sp_Cliente_Pedido_Guardar", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.Add("@Actor", SqlDbType.Int).Value = actor;
                    comando.Parameters.Add("@IdPedido", SqlDbType.Int).Value = id;
                    comando.Parameters.Add("@Solicitud", SqlDbType.UniqueIdentifier).Value = solicitud;
                    comando.Parameters.Add("@Detalle", SqlDbType.Xml).Value = (object)(xml.ToString(SaveOptions.DisableFormatting)) ?? DBNull.Value;
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
