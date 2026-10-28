using SistemaPedidoRestaurante.UICompartida;

namespace SistemaPedidoRestaurante.UIAdministracion
{
    // Los controles y eventos comunes se heredan; el acceso pertenece a este rol.
    public partial class FrmPedidoAdministracion : FrmPedidoLocalBase
    {
        public FrmPedidoAdministracion() : this(0) { }
        public FrmPedidoAdministracion(int cliente) : base(1, cliente)
        {
            InitializeComponent();
        }
    }
}
