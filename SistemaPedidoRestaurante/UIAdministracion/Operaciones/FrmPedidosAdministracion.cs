using SistemaPedidoRestaurante.UICompartida;

namespace SistemaPedidoRestaurante.UIAdministracion
{
    public partial class FrmPedidosAdministracion : FrmBandejaOperaciones
    {
        public FrmPedidosAdministracion() : this(false, "") { }
        public FrmPedidosAdministracion(bool seleccionar, string filtro = "", bool historial = false)
            : base(1, "Pedidos", seleccionar, filtro, historial)
        {
            InitializeComponent();
            ConfigurarOpciones();
        }
    }
}
