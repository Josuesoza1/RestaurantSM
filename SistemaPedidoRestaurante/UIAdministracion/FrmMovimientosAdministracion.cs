using SistemaPedidoRestaurante.UICompartida;

namespace SistemaPedidoRestaurante.UIAdministracion
{
    // Los controles y eventos comunes se heredan; el acceso pertenece a este rol.
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
