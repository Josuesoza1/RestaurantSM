using SistemaPedidoRestaurante.UICompartida;

namespace SistemaPedidoRestaurante.UIAdministracion
{
    // Los controles y eventos comunes se heredan; el acceso pertenece a este rol.
    public partial class FrmPagoAdministracion : FrmPagoBase
    {
        public FrmPagoAdministracion() : this(0) { }
        public FrmPagoAdministracion(int pedido) : base(1, pedido)
        {
            InitializeComponent();
        }
    }
}
