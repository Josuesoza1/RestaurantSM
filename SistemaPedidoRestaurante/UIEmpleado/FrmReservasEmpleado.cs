using SistemaPedidoRestaurante.UICompartida;

namespace SistemaPedidoRestaurante.UIEmpleado
{
    // Los controles y eventos comunes se heredan; el acceso pertenece a este rol.
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
