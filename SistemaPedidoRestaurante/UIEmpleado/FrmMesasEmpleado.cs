using SistemaPedidoRestaurante.UICompartida;

namespace SistemaPedidoRestaurante.UIEmpleado
{
    // Los controles y eventos comunes se heredan; el acceso pertenece a este rol.
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
