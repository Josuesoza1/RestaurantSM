using SistemaPedidoRestaurante.UICompartida;

namespace SistemaPedidoRestaurante.UIEmpleado
{
    // Los controles y eventos comunes se heredan; el acceso pertenece a este rol.
    public partial class FrmFacturas : FrmFacturasBase
    {
        public FrmFacturas() : this(0) { }
        public FrmFacturas(int pedido) : base(2, pedido)
        {
            InitializeComponent();
        }
    }
}
