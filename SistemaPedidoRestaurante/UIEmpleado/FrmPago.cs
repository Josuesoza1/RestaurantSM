using SistemaPedidoRestaurante.UICompartida;

namespace SistemaPedidoRestaurante.UIEmpleado
{
    // Los controles y eventos comunes se heredan; el acceso pertenece a este rol.
    public partial class FrmPago : FrmPagoBase
    {
        public FrmPago() : this(0) { }
        public FrmPago(int pedido) : base(2, pedido)
        {
            InitializeComponent();
        }
    }
}
