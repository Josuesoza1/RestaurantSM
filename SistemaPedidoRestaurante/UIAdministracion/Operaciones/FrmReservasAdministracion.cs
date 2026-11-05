using SistemaPedidoRestaurante.UICompartida;

namespace SistemaPedidoRestaurante.UIAdministracion
{
    public partial class FrmReservasAdministracion : FrmBandejaOperaciones
    {
        public FrmReservasAdministracion() : this(false, "") { }
        public FrmReservasAdministracion(bool seleccionar, string filtro = "") : base(1, "Reservas", seleccionar, filtro)
        {
            InitializeComponent();
            ConfigurarOpciones();
        }
    }
}
