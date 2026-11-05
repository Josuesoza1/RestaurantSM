using SistemaPedidoRestaurante.UICompartida;

namespace SistemaPedidoRestaurante.UIEmpleado
{
    public partial class FrmConsultaFacturasEmpleado : FrmBandejaOperaciones
    {
        public FrmConsultaFacturasEmpleado() : this(false, "") { }
        public FrmConsultaFacturasEmpleado(bool seleccionar, string filtro = "") : base(2, "Facturas", seleccionar, filtro)
        {
            InitializeComponent();
            ConfigurarOpciones();
        }
    }
}
