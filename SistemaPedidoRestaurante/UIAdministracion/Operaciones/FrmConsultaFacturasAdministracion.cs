using SistemaPedidoRestaurante.UICompartida;

namespace SistemaPedidoRestaurante.UIAdministracion
{
    public partial class FrmConsultaFacturasAdministracion : FrmBandejaOperaciones
    {
        public FrmConsultaFacturasAdministracion() : this(false, "") { }
        public FrmConsultaFacturasAdministracion(bool seleccionar, string filtro = "") : base(1, "Facturas", seleccionar, filtro)
        {
            InitializeComponent();
            ConfigurarOpciones();
        }
    }
}
