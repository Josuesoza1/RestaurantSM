using SistemaPedidoRestaurante.UICompartida;

namespace SistemaPedidoRestaurante.UIAdministracion
{
    public partial class FrmPagoAdministracion : FrmPagoBase
    {
        public FrmPagoAdministracion() : this(0) { }
        public FrmPagoAdministracion(int pedido) : base(1, pedido)
        {
            InitializeComponent();
        }
    }
}
