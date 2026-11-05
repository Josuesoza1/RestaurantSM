using SistemaPedidoRestaurante.UICompartida;

namespace SistemaPedidoRestaurante.UIEmpleado
{
    public partial class FrmReservasEmpleado : FrmBandejaOperaciones
    {
        public FrmReservasEmpleado() : this(false, "") { }
        public FrmReservasEmpleado(bool seleccionar, string filtro = "") : base(2, "Reservas", seleccionar, filtro)
        {
            InitializeComponent();
            ConfigurarOpciones();
        }
    }
}
