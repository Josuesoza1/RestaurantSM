using SistemaPedidoRestaurante.UICompartida;

namespace SistemaPedidoRestaurante.UIEmpleado
{
    // Los controles y eventos comunes se heredan; el acceso pertenece a este rol.
    public partial class FrmPedido : FrmPedidoLocalBase
    {
        public FrmPedido() : this(0) { }
        public FrmPedido(int cliente) : base(2, cliente)
        {
            InitializeComponent();
        }
    }
}
