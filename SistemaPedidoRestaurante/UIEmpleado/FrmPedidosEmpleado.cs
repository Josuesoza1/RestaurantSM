using SistemaPedidoRestaurante.UICompartida;

namespace SistemaPedidoRestaurante.UIEmpleado
{
    // Los controles y eventos comunes se heredan; el acceso pertenece a este rol.
    public partial class FrmPedidosEmpleado : FrmBandejaOperaciones
    {
        public FrmPedidosEmpleado() : this(false, "") { }
        public FrmPedidosEmpleado(bool seleccionar, string filtro = "") : base(2, "Pedidos", seleccionar, filtro)
        {
            InitializeComponent();
            ConfigurarOpciones();
        }
    }
}
