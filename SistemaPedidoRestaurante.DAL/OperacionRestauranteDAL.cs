using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using SistemaPedidoRestaurante.Entities;

namespace SistemaPedidoRestaurante.DAL
{
    public class OperacionRestauranteDAL
    {
        public DataTable VerificarInstalacion()
        {
            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            {
                using (SqlCommand comando = new SqlCommand("sp_Sistema_Verificar", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    using (SqlDataAdapter adaptador = new SqlDataAdapter(comando))
                    {
                        DataTable tabla = new DataTable();
                        adaptador.Fill(tabla);
                        return tabla;
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

        public DataTable ListarClientes(int actor)
        {
            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            {
                using (SqlCommand comando = new SqlCommand("sp_Operacion_Clientes", conexion))
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

        public DataTable ListarReservas(int actor)
        {
            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            {
                using (SqlCommand comando = new SqlCommand("sp_Operacion_Reservas", conexion))
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

        public DataTable Cotizar(int actor, int pedido)
        {
            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            {
                using (SqlCommand comando = new SqlCommand("sp_Cobro_Cotizar", conexion))
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

        public int GuardarPedido(int actor, int cliente, Guid solicitud, IEnumerable<LineaPedidoBorrador> lineas)
        {
            return new PedidoDAL().Guardar(actor, cliente, 0, solicitud, lineas);
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

        public DataTable Cobrar(int actor, int pedido, string metodo, decimal recibido, decimal total, Guid solicitud, string notas)
        {
            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            {
                using (SqlCommand comando = new SqlCommand("sp_Cobro_Registrar", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.Add("@Actor", SqlDbType.Int).Value = actor;
                    comando.Parameters.Add("@IdPedido", SqlDbType.Int).Value = pedido;
                    comando.Parameters.Add("@Metodo", SqlDbType.VarChar, 40).Value = metodo;
                    SqlParameter monto = comando.Parameters.Add("@Recibido", SqlDbType.Decimal);
                    monto.Precision = 10;
                    monto.Scale = 2;
                    monto.Value = recibido;
                    SqlParameter importe = comando.Parameters.Add("@TotalEsperado", SqlDbType.Decimal);
                    importe.Precision = 10;
                    importe.Scale = 2;
                    importe.Value = total;
                    comando.Parameters.Add("@Solicitud", SqlDbType.UniqueIdentifier).Value = solicitud;
                    comando.Parameters.Add("@Notas", SqlDbType.NVarChar, 200).Value = (object)notas ?? DBNull.Value;
                    using (SqlDataAdapter adaptador = new SqlDataAdapter(comando))
                    {
                        DataTable resultado = new DataTable();
                        adaptador.Fill(resultado);
                        return resultado;
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

        public void CambiarEstadoReserva(int actor, int reserva, string anterior, string nuevo)
        {
            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            {
                using (SqlCommand comando = new SqlCommand("sp_Operacion_EstadoReserva", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.Add("@Actor", SqlDbType.Int).Value = actor;
                    comando.Parameters.Add("@IdReserva", SqlDbType.Int).Value = reserva;
                    comando.Parameters.Add("@Anterior", SqlDbType.VarChar, 20).Value = anterior;
                    comando.Parameters.Add("@Nuevo", SqlDbType.VarChar, 20).Value = nuevo;
                    conexion.Open();
                    comando.ExecuteNonQuery();
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

        public void GuardarConfiguracion(int actor, string nombre, decimal tasa)
        {
            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            {
                using (SqlCommand comando = new SqlCommand("sp_Configuracion_Guardar", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.Add("@Actor", SqlDbType.Int).Value = actor;
                    comando.Parameters.Add("@Nombre", SqlDbType.NVarChar, 120).Value = nombre;
                    SqlParameter impuesto = comando.Parameters.Add("@Tasa", SqlDbType.Decimal);
                    impuesto.Precision = 5;
                    impuesto.Scale = 2;
                    impuesto.Value = tasa;
                    conexion.Open();
                    comando.ExecuteNonQuery();
                }
            }
        }

        public DataTable ListarMesas(int actor)
        {
            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            using (SqlCommand comando = new SqlCommand("sp_Operacion_Mesas", conexion))
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

        public void CambiarMesa(int actor, int mesa, string anterior)
        {
            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            using (SqlCommand comando = new SqlCommand("sp_Operacion_CambiarMesa", conexion))
            {
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("@Actor", SqlDbType.Int).Value = actor;
                comando.Parameters.Add("@IdMesa", SqlDbType.Int).Value = mesa;
                comando.Parameters.Add("@Anterior", SqlDbType.VarChar, 20).Value = anterior;
                conexion.Open();
                comando.ExecuteNonQuery();
            }
        }

        public void AsignarMesa(int actor, int pedido, int mesa)
        {
            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            using (SqlCommand comando = new SqlCommand("sp_Operacion_AsignarMesa", conexion))
            {
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("@Actor", SqlDbType.Int).Value = actor;
                comando.Parameters.Add("@IdPedido", SqlDbType.Int).Value = pedido;
                comando.Parameters.Add("@IdMesa", SqlDbType.Int).Value = mesa;
                conexion.Open();
                comando.ExecuteNonQuery();
            }
        }
    }
}
