using SistemaPedidoRestaurante.UICompartida;

namespace SistemaPedidoRestaurante.UIEmpleado
{
    public partial class FrmMesasEmpleado : FrmBandejaOperaciones
    {
        public FrmMesasEmpleado() : this(false, "") { }
        public FrmMesasEmpleado(bool seleccionar, string filtro = "") : base(2, "Mesas", seleccionar, filtro)
        {
            InitializeComponent();
            ConfigurarOpciones();
        }
    }
}
