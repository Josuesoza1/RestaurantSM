using SistemaPedidoRestaurante.UICompartida;

namespace SistemaPedidoRestaurante.UIEmpleado
{
    public partial class FrmPago : FrmPagoBase
    {
        public FrmPago() : this(0) { }
        public FrmPago(int pedido, bool desdeMostrador = false) : base(2, pedido, desdeMostrador)
        {
            InitializeComponent();
        }
    }
}
