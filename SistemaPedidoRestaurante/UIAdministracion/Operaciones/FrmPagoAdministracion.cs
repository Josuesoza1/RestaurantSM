using SistemaPedidoRestaurante.UICompartida;

namespace SistemaPedidoRestaurante.UIAdministracion
{
    public partial class FrmPagoAdministracion : FrmPagoBase
    {
        public FrmPagoAdministracion() : this(0) { }
        public FrmPagoAdministracion(int pedido, bool desdeMostrador = false) : base(1, pedido, desdeMostrador)
        {
            InitializeComponent();
        }
    }
}
