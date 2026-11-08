using System;
using System.Data;
using System.Data.SqlClient;

namespace SistemaPedidoRestaurante.DAL
{
    public class PagoDAL
    {
        public DataTable Cotizar(int actor, int pedido, bool desdeMostrador = false)
        {
            const string cotizarMostrador = @"
                EXEC dbo.sp_Operacion_ExigirPedidoAbierto @Actor, @IdPedido;
                SELECT p.total AS Total,
                       CASE WHEN p.idCliente IS NULL THEN 'Consumidor final'
                            ELSE CONCAT(c.pNombreCl, ' ', c.pApellidoCl) END AS Cliente
                FROM dbo.pedido p
                LEFT JOIN dbo.vw_Cliente c ON c.idCliente = p.idCliente
                WHERE p.idPedido = @IdPedido AND p.idOrigenPedido = 2
                  AND p.idTipoEntrega = 2 AND p.idMesa IS NULL
                  AND p.estado IN ('Pendiente', 'Entregado');";
            const string permisoNombre = @"
                SELECT CONVERT(bit, CASE WHEN EXISTS (
                    SELECT 1 FROM dbo.pedido WHERE idPedido = @IdPedido AND idCliente IS NULL
                        AND idOrigenPedido = 2 AND idTipoEntrega IN (1, 2)) THEN 1 ELSE 0 END);";

            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            {
                string consulta = desdeMostrador ? cotizarMostrador : "EXEC dbo.sp_Cobro_Cotizar @Actor, @IdPedido;";
                using (SqlCommand comando = new SqlCommand(consulta + permisoNombre, conexion))
                {
                    comando.Parameters.Add("@Actor", SqlDbType.Int).Value = actor;
                    comando.Parameters.Add("@IdPedido", SqlDbType.Int).Value = pedido;
                    using (SqlDataAdapter adaptador = new SqlDataAdapter(comando))
                    {
                        DataSet datos = new DataSet();
                        adaptador.Fill(datos);
                        DataTable cotizacion = datos.Tables[0];
                        cotizacion.Columns.Add("PuedeNombrarComprobante", typeof(bool));
                        foreach (DataRow fila in cotizacion.Rows)
                            fila["PuedeNombrarComprobante"] = datos.Tables[1].Rows[0][0];
                        return cotizacion;
                    }
                }
            }
        }

        public DataTable Cobrar(int actor, int pedido, string metodo, decimal recibido, decimal total, Guid solicitud, string notas,
            bool desdeMostrador = false, string nombreComprobante = null)
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
                    if (!desdeMostrador && nombreComprobante == null)
                        return LeerResultado(comando);

                    conexion.Open();
                    using (SqlTransaction transaccion = conexion.BeginTransaction(IsolationLevel.Serializable))
                    {
                        if (desdeMostrador)
                            DespacharMostrador(actor, pedido, conexion, transaccion);

                        bool guardarNombre = nombreComprobante != null &&
                            !SolicitudYaCobrada(actor, solicitud, conexion, transaccion);
                        comando.Transaction = transaccion;
                        DataTable resultado = LeerResultado(comando);
                        if (resultado.Rows.Count != 1)
                            throw new InvalidOperationException("No se recibió la confirmación del cobro.");

                        if (guardarNombre)
                            GuardarNombreComprobante(pedido, Convert.ToInt32(resultado.Rows[0]["IdFactura"]),
                                nombreComprobante, conexion, transaccion);

                        transaccion.Commit();
                        return resultado;
                    }
                }
            }
        }

        private static bool SolicitudYaCobrada(int actor, Guid solicitud, SqlConnection conexion, SqlTransaction transaccion)
        {
            const string consulta = "SELECT idPago FROM dbo.pago WITH (UPDLOCK) WHERE idUsuario = @Actor AND solicitud = @Solicitud;";
            using (SqlCommand comando = new SqlCommand(consulta, conexion, transaccion))
            {
                comando.Parameters.Add("@Actor", SqlDbType.Int).Value = actor;
                comando.Parameters.Add("@Solicitud", SqlDbType.UniqueIdentifier).Value = solicitud;
                return comando.ExecuteScalar() != null;
            }
        }

        private static void GuardarNombreComprobante(int pedido, int factura, string nombre,
            SqlConnection conexion, SqlTransaction transaccion)
        {
            const string consulta = @"
                UPDATE f SET clienteNombre = @Nombre
                FROM dbo.factura f JOIN dbo.pedido p ON p.idPedido = f.idPedido
                WHERE f.idFactura = @Factura AND p.idPedido = @IdPedido AND p.idCliente IS NULL
                    AND p.idOrigenPedido = 2 AND p.idTipoEntrega IN (1, 2);
                IF @@ROWCOUNT = 0
                    THROW 50952, N'El nombre opcional solo corresponde a ventas locales de consumidor final.', 1;";
            using (SqlCommand comando = new SqlCommand(consulta, conexion, transaccion))
            {
                comando.Parameters.Add("@IdPedido", SqlDbType.Int).Value = pedido;
                comando.Parameters.Add("@Factura", SqlDbType.Int).Value = factura;
                comando.Parameters.Add("@Nombre", SqlDbType.NVarChar, 120).Value = nombre;
                comando.ExecuteNonQuery();
            }
        }

        private static void DespacharMostrador(int actor, int pedido, SqlConnection conexion, SqlTransaction transaccion)
        {
            const string consulta = @"
                DECLARE @Estado varchar(20);
                SELECT @Estado = estado
                FROM dbo.pedido WITH (UPDLOCK)
                WHERE idPedido = @IdPedido AND idOrigenPedido = 2 AND idTipoEntrega = 2 AND idMesa IS NULL;

                IF @Estado IS NULL
                    THROW 50951, 'El cobro directo solo corresponde a una venta local de mostrador.', 1;

                IF @Estado = 'Pendiente'
                    EXEC dbo.sp_Operacion_EstadoPedido @Actor, @IdPedido, 'Pendiente', 'Entregado';";

            using (SqlCommand comando = new SqlCommand(consulta, conexion, transaccion))
            {
                comando.Parameters.Add("@Actor", SqlDbType.Int).Value = actor;
                comando.Parameters.Add("@IdPedido", SqlDbType.Int).Value = pedido;
                comando.ExecuteNonQuery();
            }
        }

        private static DataTable LeerResultado(SqlCommand comando)
        {
            using (SqlDataAdapter adaptador = new SqlDataAdapter(comando))
            {
                DataTable resultado = new DataTable();
                adaptador.Fill(resultado);
                return resultado;
            }
        }
    }
}
