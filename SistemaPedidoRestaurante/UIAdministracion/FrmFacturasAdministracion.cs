using SistemaPedidoRestaurante.UICompartida;

namespace SistemaPedidoRestaurante.UIAdministracion
{
    // Los controles y eventos comunes se heredan; el acceso pertenece a este rol.
    public partial class FrmFacturasAdministracion : FrmFacturasBase
    {
        public FrmFacturasAdministracion() : this(0) { }
        public FrmFacturasAdministracion(int pedido) : base(1, pedido)
        {
            InitializeComponent();
        }
    }
}
