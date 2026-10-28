using SistemaPedidoRestaurante.UICompartida;

namespace SistemaPedidoRestaurante.UIAdministracion
{
    // Los controles y eventos comunes se heredan; el acceso pertenece a este rol.
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
