using SistemaPedidoRestaurante.UICompartida;

namespace SistemaPedidoRestaurante.UIAdministracion
{
    public partial class FrmFacturasAdministracion : FrmFacturasBase
    {
        public FrmFacturasAdministracion() : this(0) { }
        public FrmFacturasAdministracion(int pedido) : base(1, pedido)
        {
            InitializeComponent();
        }
    }
}
