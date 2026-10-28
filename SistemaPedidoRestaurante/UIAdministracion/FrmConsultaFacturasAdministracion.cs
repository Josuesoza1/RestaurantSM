using SistemaPedidoRestaurante.UICompartida;

namespace SistemaPedidoRestaurante.UIAdministracion
{
    // Los controles y eventos comunes se heredan; el acceso pertenece a este rol.
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
