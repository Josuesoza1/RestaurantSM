using SistemaPedidoRestaurante.UICompartida;

namespace SistemaPedidoRestaurante.UIEmpleado
{
    public partial class FrmPedido : FrmPedidoLocalBase
    {
        public FrmPedido() : this(0) { }
        public FrmPedido(int cliente) : base(2, cliente)
        {
            InitializeComponent();
        }
    }
}
