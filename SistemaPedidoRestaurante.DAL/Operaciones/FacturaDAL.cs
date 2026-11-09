using System;
using System.Data;
using System.Data.SqlClient;

namespace SistemaPedidoRestaurante.DAL
{
    public class FacturaDAL
    {
        public void AnularVenta(int actor, int pedido, string motivo, bool reponerInventario)
        {
            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            using (SqlCommand comando = new SqlCommand("sp_Factura_Anular", conexion))
            {
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("@Actor", SqlDbType.Int).Value = actor;
                comando.Parameters.Add("@IdPedido", SqlDbType.Int).Value = pedido;
                comando.Parameters.Add("@Motivo", SqlDbType.NVarChar, 200).Value = motivo;
                comando.Parameters.Add("@ReponerInventario", SqlDbType.Bit).Value = reponerInventario;
                conexion.Open();
                comando.ExecuteNonQuery();
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

        public DataTable ConsultarResumen(int actor, DateTime inicio, DateTime fin)
        {
            const string consulta = @"
                EXEC dbo.sp_Catalogo_ExigirAdministrador @Actor;

                DECLARE @Moneda varchar(5);
                SELECT @Moneda = moneda FROM dbo.configuracionRestaurante WHERE id = 1;

                SELECT Moneda, MedioPago, SUM(Cobrado) AS Cobrado, SUM(Devuelto) AS Devuelto,
                       SUM(Cobrado - Devuelto) AS Neto,
                       CASE WHEN MedioPago = 'Efectivo' AND COUNT(Recibido) = SUM(EsCobro)
                            THEN SUM(Recibido) END AS EfectivoRecibido,
                       CASE WHEN MedioPago = 'Efectivo' AND COUNT(Cambio) = SUM(EsCobro)
                            THEN SUM(Cambio) END AS Vuelto
                FROM (
                    SELECT COALESCE(f.moneda, @Moneda) AS Moneda,
                           COALESCE(NULLIF(LTRIM(RTRIM(p.metodoDePago)), ''), 'Sin medio identificado') AS MedioPago,
                           p.monto AS Cobrado, 0 AS Devuelto, p.recibido AS Recibido,
                           p.cambio AS Cambio, 1 AS EsCobro
                    FROM dbo.pago p
                    INNER JOIN dbo.factura f ON f.idFactura = p.idFactura
                    WHERE p.fechaPago >= @Inicio AND p.fechaPago < @Fin

                    UNION ALL

                    SELECT COALESCE(f.moneda, @Moneda),
                           COALESCE(p.MedioPago, 'Sin medio identificado'), 0, f.total, NULL, NULL, 0
                    FROM dbo.anulacionVenta a
                    INNER JOIN dbo.factura f ON f.idFactura = a.idFactura
                    LEFT JOIN (
                        SELECT idFactura,
                               CASE WHEN MIN(LTRIM(RTRIM(metodoDePago))) = MAX(LTRIM(RTRIM(metodoDePago)))
                                    THEN NULLIF(MIN(LTRIM(RTRIM(metodoDePago))), '') END AS MedioPago
                        FROM dbo.pago
                        GROUP BY idFactura
                    ) p ON p.idFactura = f.idFactura
                    WHERE a.fecha >= @Inicio AND a.fecha < @Fin
                ) AS movimientos
                GROUP BY Moneda, MedioPago
                ORDER BY Moneda, MedioPago;";

            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            {
                conexion.Open();
                using (SqlTransaction transaccion = conexion.BeginTransaction(IsolationLevel.Serializable))
                using (SqlCommand comando = new SqlCommand(consulta, conexion, transaccion))
                using (SqlDataAdapter adaptador = new SqlDataAdapter(comando))
                {
                    comando.Parameters.Add("@Actor", SqlDbType.Int).Value = actor;
                    comando.Parameters.Add("@Inicio", SqlDbType.Date).Value = inicio;
                    comando.Parameters.Add("@Fin", SqlDbType.Date).Value = fin;
                    DataTable resumen = new DataTable();
                    adaptador.Fill(resumen);
                    transaccion.Commit();
                    return resumen;
                }
            }
        }

        public DataSet ObtenerUltimoComprobantePropio(int actor)
        {
            const string consulta = @"
                EXEC dbo.sp_Operacion_ExigirPersonal @Actor;
                DECLARE @Pedido int, @Solicitud uniqueidentifier;

                SELECT TOP (1) @Pedido = f.idPedido, @Solicitud = p.solicitud
                FROM dbo.pago p
                JOIN dbo.factura f ON f.idFactura = p.idFactura
                JOIN dbo.pedido v ON v.idPedido = f.idPedido
                WHERE p.idUsuario = @Actor AND p.solicitud IS NOT NULL AND f.estado = 'Pagada'
                    AND v.idOrigenPedido = 2 AND v.idTipoEntrega IN (1, 2)
                ORDER BY p.fechaPago DESC, p.idPago DESC;

                IF @Pedido IS NOT NULL
                    EXEC dbo.sp_Documento_Obtener @Actor, @Pedido, 1, @Solicitud;";

            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            {
                conexion.Open();
                using (SqlTransaction transaccion = conexion.BeginTransaction(IsolationLevel.Serializable))
                using (SqlCommand comando = new SqlCommand(consulta, conexion, transaccion))
                {
                    comando.Parameters.Add("@Actor", SqlDbType.Int).Value = actor;
                    DataSet datos = LeerTablas(comando);
                    transaccion.Commit();
                    return datos;
                }
            }
        }

        public DataSet ObtenerDocumento(int actor, int pedido, bool factura, Guid? solicitudCobro)
        {
            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            {
                using (SqlCommand comando = new SqlCommand("sp_Documento_Obtener", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.Add("@Actor", SqlDbType.Int).Value = actor;
                    comando.Parameters.Add("@IdPedido", SqlDbType.Int).Value = pedido;
                    comando.Parameters.Add("@Factura", SqlDbType.Bit).Value = factura;
                    comando.Parameters.Add("@SolicitudCobro", SqlDbType.UniqueIdentifier).Value = (object)solicitudCobro ?? DBNull.Value;
                    return LeerTablas(comando);
                }
            }
        }

        private static DataSet LeerTablas(SqlCommand comando)
        {
            using (SqlDataAdapter adaptador = new SqlDataAdapter(comando))
            {
                DataSet datos = new DataSet();
                adaptador.Fill(datos);
                return datos;
            }
        }
    }
}
