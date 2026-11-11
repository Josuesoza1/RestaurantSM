using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using SistemaPedidoRestaurante.DAL;
using SistemaPedidoRestaurante.Entities;
using SistemaPedidoRestaurante.Validaciones;

namespace SistemaPedidoRestaurante.BLL
{
    public class PedidoBLL
    {
        private readonly PedidoDAL pedidoDAL = new PedidoDAL();
        public DataRow ObtenerImportes(int pedido)
        {
            Permisos.ExigirAtencion();
            try
            {
                return pedidoDAL.ObtenerImportes(SesionActual.IdUsuario, pedido);
            }
            catch (SqlException error)
            {
                throw OperacionDatos.Traducir(error, "No se pudo consultar el desglose de la cuenta.");
            }
        }

        public DataTable ListarPedidos(bool soloEntregados = false, bool soloAbiertas = false)
        {
            Permisos.ExigirPersonal();
            try
            {
                DataTable tabla = pedidoDAL.ListarPedidos(SesionActual.IdUsuario);
                if (soloAbiertas)
                {
                    foreach (DataRow fila in tabla.Select("Origen <> 'En el local' OR IdTipoEntrega NOT IN (1, 2) OR Estado IN ('Pagado', 'Cancelado')"))
                        tabla.Rows.Remove(fila);
                }
                if (soloEntregados)
                {
                    foreach (DataRow fila in tabla.Select("Estado <> 'Entregado'"))
                        tabla.Rows.Remove(fila);
                }
                return tabla;
            }
            catch (SqlException error)
            {
                throw OperacionDatos.Traducir(error, "No se pudo consultar el listado.");
            }
        }

        public DataTable ListarHistorial()
        {
            Permisos.ExigirRol(RolesSistema.Administrador);
            try
            {
                DataSet historial = pedidoDAL.ListarHistorial(SesionActual.IdUsuario);
                var nombres = new Dictionary<int, string>();
                foreach (DataRow factura in historial.Tables[1].Rows)
                {
                    string nombre = factura.Field<string>("Cliente")?.Trim();
                    if (!string.IsNullOrEmpty(nombre) &&
                        !string.Equals(nombre, "Consumidor final", StringComparison.OrdinalIgnoreCase))
                        nombres.Add(factura.Field<int>("IdPedido"), nombre);
                }

                DataTable ventas = historial.Tables[0];
                foreach (DataRow venta in ventas.Rows)
                {
                    if (nombres.TryGetValue(venta.Field<int>("IdPedido"), out string nombre))
                        venta["Cliente"] = nombre;
                }

                return ventas;
            }
            catch (SqlException error)
            {
                throw OperacionDatos.Traducir(error, "No se pudo consultar el historial de ventas.");
            }
        }

        public DataTable Detalle(int pedido)
        {
            Permisos.ExigirPersonal();
            try
            {
                return pedidoDAL.Detalle(SesionActual.IdUsuario, pedido);
            }
            catch (SqlException error)
            {
                throw OperacionDatos.Traducir(error, "No se pudo consultar el pedido.");
            }
        }

        public DataTable FiltrarHistorialPorMes(DataTable historial, string filtro, DateTime mes)
        {
            Permisos.ExigirRol(RolesSistema.Administrador);
            DataTable resultado = BusquedaLINQ.Filtrar(historial, filtro);
            for (int i = resultado.Rows.Count - 1; i >= 0; i--)
            {
                DateTime? fecha = resultado.Rows[i].Field<DateTime?>("Fecha");
                if (!fecha.HasValue || fecha.Value.Year != mes.Year || fecha.Value.Month != mes.Month)
                    resultado.Rows.RemoveAt(i);
            }
            return resultado;
        }

        public int GuardarPedido(int cliente, PedidoBorrador pedido, Guid solicitud, bool enMesa = false)
        {
            Permisos.ExigirAtencion();
            PedidoValidacion.Contenido(pedido == null ? null : pedido.Lineas, solicitud);
            try
            {
                return pedidoDAL.Guardar(SesionActual.IdUsuario, cliente, solicitud, pedido.Lineas, enMesa);
            }
            catch (SqlException error)
            {
                throw OperacionDatos.Traducir(error, "No se pudo registrar el pedido.");
            }
        }

        public List<LineaPedidoBorrador> ObtenerDetallePendiente(int pedido, bool soloAgregar = false)
        {
            Permisos.ExigirAtencion();
            try
            {
                return pedidoDAL.ObtenerDetallePendiente(SesionActual.IdUsuario, pedido, soloAgregar);
            }
            catch (SqlException error)
            {
                throw OperacionDatos.Traducir(error, "No se pudo abrir la venta pendiente.");
            }
        }

        public void EditarPendiente(int pedido, IReadOnlyList<LineaPedidoBorrador> original, PedidoBorrador borrador, bool soloAgregar = false)
        {
            Permisos.ExigirAtencion();
            PedidoValidacion.Contenido(borrador?.Lineas);
            try
            {
                pedidoDAL.EditarPendiente(SesionActual.IdUsuario, pedido, original, borrador.Lineas, soloAgregar);
            }
            catch (SqlException error)
            {
                throw OperacionDatos.Traducir(error, "No se pudo guardar la corrección de la venta.");
            }
        }

        public void DespacharPedido(int pedido, string estado)
        {
            Permisos.ExigirAtencion();
            CambiarEstado(pedido, estado, "Entregado");
        }

        public void DeshacerDespacho(int pedido)
        {
            Permisos.ExigirAtencion();
            if (pedido <= 0)
                throw new ArgumentException("Seleccione una venta.");
            try
            {
                pedidoDAL.DeshacerDespacho(SesionActual.IdUsuario, pedido);
            }
            catch (SqlException error)
            {
                throw OperacionDatos.Traducir(error, "No se pudo deshacer el despacho.");
            }
        }

        private void CambiarEstado(int pedido, string anterior, string nuevo)
        {
            if (pedido <= 0)
                throw new ArgumentException("Seleccione un pedido.");
            try
            {
                pedidoDAL.CambiarEstadoPedido(SesionActual.IdUsuario, pedido, anterior, nuevo);
            }
            catch (SqlException error)
            {
                throw OperacionDatos.Traducir(error, "No se pudo cambiar el estado.");
            }
        }

        public void CancelarPedido(int pedido)
        {
            Permisos.ExigirAtencion();
            if (pedido <= 0)
                throw new ArgumentException("Seleccione un pedido.");
            try
            {
                pedidoDAL.CancelarPedido(SesionActual.IdUsuario, pedido);
            }
            catch (SqlException error)
            {
                throw OperacionDatos.Traducir(error, "No se pudo cancelar el pedido.");
            }
        }
    }
}
