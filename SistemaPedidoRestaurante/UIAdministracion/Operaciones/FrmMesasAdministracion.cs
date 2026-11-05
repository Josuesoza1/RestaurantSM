using SistemaPedidoRestaurante.UICompartida;

namespace SistemaPedidoRestaurante.UIAdministracion
{
    public partial class FrmMesasAdministracion : FrmBandejaOperaciones
    {
        public FrmMesasAdministracion() : this(false, "") { }
        public FrmMesasAdministracion(bool seleccionar, string filtro = "") : base(1, "Mesas", seleccionar, filtro)
        {
            InitializeComponent();
            ConfigurarOpciones();
        }
    }
}
