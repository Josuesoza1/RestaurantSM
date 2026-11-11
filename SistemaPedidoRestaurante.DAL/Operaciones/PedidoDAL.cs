using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using SistemaPedidoRestaurante.Entities;

namespace SistemaPedidoRestaurante.DAL
{
    public class PedidoDAL
    {
        public DataRow ObtenerImportes(int actor, int pedido)
        {
            const string consulta = @"
                EXEC dbo.sp_Operacion_ExigirPedidoAbierto @Actor,@Pedido;
                SELECT subtotal AS Subtotal,descuento AS Descuento,impuestos AS Impuestos,total AS Total,
                    tasaImpuestoAplicada AS TasaImpuesto,porcentajeDescuento AS PorcentajeDescuento
                FROM dbo.pedido WHERE idPedido=@Pedido;";
            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            using (SqlCommand comando = new SqlCommand(consulta, conexion))
            using (SqlDataAdapter adaptador = new SqlDataAdapter(comando))
            {
                comando.Parameters.Add("@Actor", SqlDbType.Int).Value = actor;
                comando.Parameters.Add("@Pedido", SqlDbType.Int).Value = pedido;
                DataTable tabla = new DataTable();
                adaptador.Fill(tabla);
                if (tabla.Rows.Count != 1) throw new ArgumentException("No se encontró la cuenta abierta.");
                return tabla.Rows[0];
            }
        }

        public int Guardar(int actor, int cliente, Guid solicitud, IEnumerable<LineaPedidoBorrador> lineas, bool enMesa = false)
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
                            comando.Parameters.Add("@EnMesa", SqlDbType.Bit).Value = enMesa;
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

        public List<LineaPedidoBorrador> ObtenerDetallePendiente(int actor, int pedido, bool soloAgregar = false)
        {
            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            {
                conexion.Open();
                return LeerDetallePendiente(conexion, null, actor, pedido, soloAgregar);
            }
        }

        public void EditarPendiente(int actor, int pedido, IReadOnlyList<LineaPedidoBorrador> original,
            IReadOnlyList<LineaPedidoBorrador> corregido, bool soloAgregar = false)
        {
            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            {
                conexion.Open();
                using (SqlTransaction transaccion = conexion.BeginTransaction(IsolationLevel.Serializable))
                {
                    List<LineaPedidoBorrador> actual = LeerDetallePendiente(conexion, transaccion, actor, pedido, soloAgregar);
                    if (soloAgregar && actual.Any(linea => !corregido.Any(nueva =>
                        nueva.IdProducto == linea.IdProducto && nueva.Cantidad >= linea.Cantidad &&
                        nueva.PrecioUnitario == linea.PrecioUnitario)))
                        throw new ArgumentException("Agregar consumo conserva los productos anteriores y solo permite aumentar cantidades o agregar productos.");
                    if (CoincideDetalle(actual, corregido))
                    {
                        transaccion.Commit();
                        return;
                    }
                    if (!CoincideDetalle(actual, original))
                        throw new ArgumentException("Otra caja cambió esta venta. Vuelva a abrirla antes de editar.");

                    var productos = actual.Select(linea => linea.IdProducto)
                        .Union(corregido.Select(linea => linea.IdProducto)).OrderBy(id => id);
                    foreach (int producto in productos)
                    {
                        var anterior = actual.FirstOrDefault(linea => linea.IdProducto == producto);
                        var nueva = corregido.FirstOrDefault(linea => linea.IdProducto == producto);
                        if (anterior != null && nueva != null && anterior.Cantidad == nueva.Cantidad &&
                            anterior.PrecioUnitario == nueva.PrecioUnitario)
                            continue;

                        ActualizarLinea(conexion, transaccion, actor, pedido, producto, nueva);
                    }

                    const string actualizarTotal = "EXEC dbo.sp_Pedido_Recalcular @Actor, @Pedido;";
                    using (SqlCommand comando = new SqlCommand(actualizarTotal, conexion, transaccion))
                    {
                        comando.Parameters.Add("@Pedido", SqlDbType.Int).Value = pedido;
                        comando.Parameters.Add("@Actor", SqlDbType.Int).Value = actor;
                        comando.ExecuteNonQuery();
                    }
                    transaccion.Commit();
                }
            }
        }

        private static List<LineaPedidoBorrador> LeerDetallePendiente(SqlConnection conexion, SqlTransaction transaccion, int actor, int pedido, bool soloAgregar)
        {
            const string consulta = @"
                EXEC dbo.sp_Operacion_ExigirPersonal @Actor;
                IF NOT EXISTS (SELECT 1 FROM dbo.pedido WITH (UPDLOCK)
                    WHERE idPedido = @Pedido AND idOrigenPedido = 2 AND idTipoEntrega IN (1, 2)
                        AND (estado = 'Pendiente' OR (@SoloAgregar = 1 AND estado = 'Entregado' AND idTipoEntrega = 1 AND idMesa IS NOT NULL)))
                    OR EXISTS (SELECT 1 FROM dbo.factura WHERE idPedido = @Pedido)
                    THROW 50704, N'La cuenta debe estar abierta y sin factura. En cuentas entregadas solo se permite agregar consumo.', 1;

                SELECT d.idProducto, p.nombre, d.precioUnitario, d.cantidad
                FROM dbo.detallePedido d JOIN dbo.producto p ON p.idProducto = d.idProducto
                WHERE d.idPedido = @Pedido ORDER BY d.idDetalle;";
            var detalle = new List<LineaPedidoBorrador>();
            using (SqlCommand comando = new SqlCommand(consulta, conexion, transaccion))
            {
                comando.Parameters.Add("@Actor", SqlDbType.Int).Value = actor;
                comando.Parameters.Add("@Pedido", SqlDbType.Int).Value = pedido;
                comando.Parameters.Add("@SoloAgregar", SqlDbType.Bit).Value = soloAgregar;
                using (SqlDataReader lector = comando.ExecuteReader())
                {
                    while (lector.Read())
                    {
                        detalle.Add(new LineaPedidoBorrador(lector.GetInt32(0), lector.GetString(1),
                            lector.GetDecimal(2), lector.GetInt32(3)));
                    }
                }
            }
            return detalle;
        }

        private static bool CoincideDetalle(IReadOnlyList<LineaPedidoBorrador> actual, IReadOnlyList<LineaPedidoBorrador> otro)
        {
            return otro != null && actual.Count == otro.Count && actual.All(linea =>
                otro.Any(copia => copia.IdProducto == linea.IdProducto && copia.Cantidad == linea.Cantidad &&
                    copia.PrecioUnitario == linea.PrecioUnitario));
        }

        private static void ActualizarLinea(SqlConnection conexion, SqlTransaction transaccion, int actor,
            int pedido, int producto, LineaPedidoBorrador nueva)
        {
            const string consulta = @"
                DECLARE @CantidadAnterior int = 0, @PrecioAnterior decimal(10,2), @Control bit = 1;
                SELECT @CantidadAnterior = cantidad, @PrecioAnterior = precioUnitario, @Control = controlInventario
                FROM dbo.detallePedido WHERE idPedido = @Pedido AND idProducto = @Producto;

                IF @CantidadAnterior > 0 AND @Cantidad > 0 AND @Precio <> @PrecioAnterior
                    THROW 50707, N'El producto debe conservar el precio original de la venta.', 1;

                DECLARE @Diferencia int = @Cantidad - @CantidadAnterior;
                IF @Diferencia > 0
                BEGIN
                    IF @Control = 0
                        THROW 50708, N'Este producto se registró sin control de inventario. Quite la línea y guarde antes de agregarlo de nuevo.', 1;
                    IF NOT EXISTS (SELECT 1 FROM dbo.producto p JOIN dbo.categoria c ON c.idCategoria = p.idCategoria
                        WHERE p.idProducto = @Producto AND p.disponible = 1 AND c.activa = 1
                            AND (@CantidadAnterior > 0 OR p.precio = @Precio))
                        THROW 50707, N'Cambió el precio o la disponibilidad. Vuelva a abrir la venta.', 1;
                END;

                IF @Diferencia <> 0 AND @Control = 1
                BEGIN
                    DECLARE @Stock int;
                    SELECT @Stock = stockActual FROM dbo.inventarioProducto WITH (UPDLOCK) WHERE idProducto = @Producto;
                    UPDATE dbo.inventarioProducto
                    SET stockActual = stockActual - @Diferencia, revision = revision + 1
                    WHERE idProducto = @Producto AND (@Diferencia < 0 OR (activo = 1 AND stockActual >= @Diferencia));
                    IF @@ROWCOUNT = 0
                        THROW 50708, N'No se pudo ajustar el inventario del producto. Revise sus existencias.', 1;

                    DECLARE @Unidades int = ABS(@Diferencia);
                    DECLARE @Tipo varchar(20) = CASE WHEN @Diferencia > 0 THEN 'Salida' ELSE 'Entrada' END;
                    EXEC dbo.sp_Inventario_RegistrarMovimiento @Actor, @Producto, @Stock, @Unidades, @Tipo, N'Edición de venta pendiente';
                END;

                IF @Cantidad = 0
                    DELETE FROM dbo.detallePedido WHERE idPedido = @Pedido AND idProducto = @Producto;
                ELSE IF @CantidadAnterior = 0
                    INSERT dbo.detallePedido (idPedido, idProducto, cantidad, precioUnitario, subtotal, total, controlInventario)
                    VALUES (@Pedido, @Producto, @Cantidad, @Precio, @Cantidad * @Precio, @Cantidad * @Precio, 1);
                ELSE
                    UPDATE dbo.detallePedido SET cantidad = @Cantidad, subtotal = @Cantidad * @Precio, total = @Cantidad * @Precio
                    WHERE idPedido = @Pedido AND idProducto = @Producto;";
            using (SqlCommand comando = new SqlCommand(consulta, conexion, transaccion))
            {
                comando.Parameters.Add("@Actor", SqlDbType.Int).Value = actor;
                comando.Parameters.Add("@Pedido", SqlDbType.Int).Value = pedido;
                comando.Parameters.Add("@Producto", SqlDbType.Int).Value = producto;
                comando.Parameters.Add("@Cantidad", SqlDbType.Int).Value = nueva?.Cantidad ?? 0;
                SqlParameter precio = comando.Parameters.Add("@Precio", SqlDbType.Decimal);
                precio.Precision = 10;
                precio.Scale = 2;
                precio.Value = nueva?.PrecioUnitario ?? 0m;
                comando.ExecuteNonQuery();
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

        public DataSet ListarHistorial(int actor)
        {
            const string consulta = @"
                EXEC dbo.sp_Catalogo_ExigirAdministrador @Actor;
                EXEC dbo.sp_Operacion_Pedidos @Actor;
                EXEC dbo.sp_Facturas_Listar @Actor;";

            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            {
                conexion.Open();
                using (SqlTransaction transaccion = conexion.BeginTransaction(IsolationLevel.Serializable))
                using (SqlCommand comando = new SqlCommand(consulta, conexion, transaccion))
                using (SqlDataAdapter adaptador = new SqlDataAdapter(comando))
                {
                    comando.Parameters.Add("@Actor", SqlDbType.Int).Value = actor;
                    var historial = new DataSet();
                    adaptador.Fill(historial);
                    transaccion.Commit();
                    return historial;
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

        public void DeshacerDespacho(int actor, int pedido)
        {
            const string consulta = @"
                EXEC dbo.sp_Operacion_ExigirPersonal @Actor;

                UPDATE dbo.pedido
                SET estado = 'Pendiente'
                WHERE idPedido = @Pedido AND estado = 'Entregado' AND idOrigenPedido = 2
                    AND idTipoEntrega IN (1, 2)
                    AND NOT EXISTS (SELECT 1 FROM dbo.factura WHERE idPedido = @Pedido);

                IF @@ROWCOUNT = 0
                    THROW 50512, N'Solo se puede deshacer el despacho de una venta local entregada y sin factura.', 1;

                INSERT dbo.historialPedido (idPedido, estadoAnterior, estadoNuevo, idUsuario)
                VALUES (@Pedido, 'Entregado', 'Pendiente', @Actor);";

            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            {
                conexion.Open();
                using (SqlTransaction transaccion = conexion.BeginTransaction(IsolationLevel.Serializable))
                using (SqlCommand comando = new SqlCommand(consulta, conexion, transaccion))
                {
                    comando.Parameters.Add("@Actor", SqlDbType.Int).Value = actor;
                    comando.Parameters.Add("@Pedido", SqlDbType.Int).Value = pedido;
                    comando.ExecuteNonQuery();
                    transaccion.Commit();
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
