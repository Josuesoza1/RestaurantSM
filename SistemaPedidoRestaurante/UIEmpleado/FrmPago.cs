using SistemaPedidoRestaurante.UICompartida;

namespace SistemaPedidoRestaurante.UIEmpleado
{
    public partial class FrmPago : FrmPagoBase
    {
        public FrmPago() : this(0) { }
        public FrmPago(int pedido) : base(2, pedido)
        {
            InitializeComponent();
        }
    }
}
