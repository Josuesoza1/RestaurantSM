using SistemaPedidoRestaurante.UICompartida;

namespace SistemaPedidoRestaurante.UIEmpleado
{
    // Los controles y eventos comunes se heredan; el acceso pertenece a este rol.
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
