using SistemaPedidoRestaurante.UICompartida;

namespace SistemaPedidoRestaurante.UIAdministracion
{
    public partial class FrmPedidosAdministracion : FrmBandejaOperaciones
    {
        public FrmPedidosAdministracion() : this(false, "") { }
        public FrmPedidosAdministracion(bool seleccionar, string filtro = "") : base(1, "Pedidos", seleccionar, filtro)
        {
            InitializeComponent();
            ConfigurarOpciones();
        }
    }
}
