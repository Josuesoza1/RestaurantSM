using SistemaPedidoRestaurante.UICompartida;

namespace SistemaPedidoRestaurante.UIEmpleado
{
    public partial class FrmFacturas : FrmFacturasBase
    {
        public FrmFacturas() : this(0) { }
        public FrmFacturas(int pedido) : base(2, pedido)
        {
            InitializeComponent();
        }
    }
}
