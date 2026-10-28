using SistemaPedidoRestaurante.UICompartida;

namespace SistemaPedidoRestaurante.UIAdministracion
{
    // Los controles y eventos comunes se heredan; el acceso pertenece a este rol.
    public partial class FrmPedidosAdministracion : FrmBandejaOperaciones
    {
        public FrmPedidosAdministracion() : this(false, "") { }
        public FrmPedidosAdministracion(bool seleccionar, string filtro = "") : base(1, "Pedidos", seleccionar, filtro)
        {
            InitializeComponent();
            ConfigurarOpciones();
        }
    }
}
