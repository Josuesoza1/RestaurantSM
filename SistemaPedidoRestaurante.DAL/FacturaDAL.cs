using System;
using System.Data;
using System.Data.SqlClient;

namespace SistemaPedidoRestaurante.DAL
{
    public class FacturaDAL
    {
        public DataTable ListarFacturas(int actor)
        {
            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            {
                using (SqlCommand comando = new SqlCommand("sp_Facturas_Listar", conexion))
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

        public DataSet ObtenerDocumento(int actor, int pedido, bool factura)
        {
            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            {
                using (SqlCommand comando = new SqlCommand("sp_Documento_Obtener", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.Add("@Actor", SqlDbType.Int).Value = actor;
                    comando.Parameters.Add("@IdPedido", SqlDbType.Int).Value = pedido;
                    comando.Parameters.Add("@Factura", SqlDbType.Bit).Value = factura;
                    using (SqlDataAdapter adaptador = new SqlDataAdapter(comando))
                    {
                        DataSet datos = new DataSet();
                        adaptador.Fill(datos);
                        return datos;
                    }
                }
            }
        }
    }
}
