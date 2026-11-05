using SistemaPedidoRestaurante.UICompartida;

namespace SistemaPedidoRestaurante.UIEmpleado
{
    public partial class FrmPedidosEmpleado : FrmBandejaOperaciones
    {
        public FrmPedidosEmpleado() : this(false, "") { }
        public FrmPedidosEmpleado(bool seleccionar, string filtro = "") : base(2, "Pedidos", seleccionar, filtro)
        {
            InitializeComponent();
            ConfigurarOpciones();
        }
    }
}
