using System;
using System.Windows.Forms;
using SistemaPedidoRestaurante.BLL;
using SistemaPedidoRestaurante.UIAdministracion;
using SistemaPedidoRestaurante.UIEmpleado;

namespace SistemaPedidoRestaurante.UICompartida
{
    public static class PantallasPersonal
    {
        public static FrmBandejaOperaciones CrearBandeja(string tipo, bool seleccionar = false, string filtro = "")
        {
            if (tipo == "Pedidos" && !Permisos.PuedeConsultarPedidos ||
                tipo == "Mesas" && !Permisos.PuedeAtender ||
                (tipo == "Reservas" || tipo == "Facturas" || tipo == "Movimientos") && !Permisos.PuedeVerHistorial)
                throw new UnauthorizedAccessException("Su cargo no tiene acceso a esta pantalla.");
            if (SesionActual.TieneRol(1))
            {
                switch (tipo)
                {
                    case "Pedidos": return new FrmPedidosAdministracion(seleccionar, filtro);
                    case "Reservas": return new FrmReservasAdministracion(seleccionar, filtro);
                    case "Mesas": return new FrmMesasAdministracion(seleccionar, filtro);
                    case "Facturas": return new FrmConsultaFacturasAdministracion(seleccionar, filtro);
                    case "Movimientos": return new FrmMovimientosAdministracion(seleccionar, filtro);
                }
            }
            if (SesionActual.TieneRol(2))
            {
                switch (tipo)
                {
                    case "Pedidos": return new FrmPedidosEmpleado(seleccionar, filtro);
                    case "Mesas": return new FrmMesasEmpleado(seleccionar, filtro);
                }
            }
            throw new UnauthorizedAccessException("La sesión no tiene acceso a esta pantalla.");
        }

        public static FrmPedidoLocalBase CrearPedido(int cliente = 0)
        {
            Permisos.ExigirAtencion();
            if (SesionActual.TieneRol(1)) return new FrmPedidoAdministracion(cliente);
            if (SesionActual.TieneRol(2)) return new FrmPedido(cliente);
            throw new UnauthorizedAccessException("Solo el personal puede registrar pedidos para otros clientes.");
        }

        public static Form CrearCobro(int pedido = 0)
        {
            Permisos.ExigirCobro();
            if (SesionActual.TieneRol(1)) return new FrmPagoAdministracion(pedido);
            if (SesionActual.TieneRol(2)) return new FrmPago(pedido);
            throw new UnauthorizedAccessException("Solo el personal puede abrir cobros.");
        }


    }
}
