using SistemaPedidoRestaurante.UICompartida;

namespace SistemaPedidoRestaurante.UIAdministracion
{
    public partial class FrmMovimientosAdministracion : FrmBandejaOperaciones
    {
        public FrmMovimientosAdministracion() : this(false, "") { }
        public FrmMovimientosAdministracion(bool seleccionar, string filtro = "") : base(1, "Movimientos", seleccionar, filtro)
        {
            InitializeComponent();
            ConfigurarOpciones();
        }
    }
}
