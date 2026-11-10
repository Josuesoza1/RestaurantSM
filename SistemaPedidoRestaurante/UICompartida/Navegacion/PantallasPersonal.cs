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
                }
            }
            throw new UnauthorizedAccessException("La sesión no tiene acceso a esta pantalla.");
        }

        public static FrmPedidoLocalBase CrearPedido()
        {
            Permisos.ExigirAtencion();
            if (SesionActual.TieneRol(1)) return new FrmPedidoAdministracion();
            if (SesionActual.TieneRol(2)) return new FrmPedido();
            throw new UnauthorizedAccessException("Solo el personal puede registrar ventas.");
        }

        public static Form CrearCobro(int pedido = 0, bool desdeMostrador = false)
        {
            Permisos.ExigirCobro();
            if (SesionActual.TieneRol(1)) return new FrmPagoAdministracion(pedido, desdeMostrador);
            if (SesionActual.TieneRol(2)) return new FrmPago(pedido, desdeMostrador);
            throw new UnauthorizedAccessException("Solo el personal puede abrir cobros.");
        }


    }
}
