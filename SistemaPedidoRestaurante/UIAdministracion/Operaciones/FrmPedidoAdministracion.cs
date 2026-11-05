using SistemaPedidoRestaurante.UICompartida;

namespace SistemaPedidoRestaurante.UIAdministracion
{
    public partial class FrmPedidoAdministracion : FrmPedidoLocalBase
    {
        public FrmPedidoAdministracion() : this(0) { }
        public FrmPedidoAdministracion(int cliente) : base(1, cliente)
        {
            InitializeComponent();
        }
    }
}
