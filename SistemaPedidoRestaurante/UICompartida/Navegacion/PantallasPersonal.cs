using System;
using System.Windows.Forms;
using SistemaPedidoRestaurante.BLL;
using SistemaPedidoRestaurante.UIAdministracion;
using SistemaPedidoRestaurante.UIEmpleado;

namespace SistemaPedidoRestaurante.UICompartida
{
    public static class PantallasPersonal
    {
        public static Form CrearPedido()
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
