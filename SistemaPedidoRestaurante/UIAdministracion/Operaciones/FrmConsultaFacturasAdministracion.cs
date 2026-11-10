using System;
using System.Windows.Forms;
using SistemaPedidoRestaurante.UICompartida;

namespace SistemaPedidoRestaurante.UIAdministracion
{
    public partial class FrmConsultaFacturasAdministracion : FrmBandejaOperaciones
    {
        public FrmConsultaFacturasAdministracion() : this(false, "") { }
        public FrmConsultaFacturasAdministracion(bool seleccionar, string filtro = "") : base(1, "Facturas", seleccionar, filtro)
        {
            InitializeComponent();
            PrepararVista();
        }

        protected override Label Titulo => titulo;
        protected override TextBox TxtBuscar => txtBuscar;
        protected override Panel FiltroMes => filtroMes;
        protected override CheckBox ChkFiltrarMes => chkFiltrarMes;
        protected override DateTimePicker MesVenta => mesVenta;
        protected override FlowLayoutPanel Acciones => acciones;
        protected override SplitContainer Paneles => paneles;
        protected override DataGridView Listado => listado;
        protected override DataGridView Detalle => detalle;
        protected override Button BtnSeleccionar => btnSeleccionar;
        protected override Button BtnNuevo => btnNuevo;
        protected override Button BtnUltimoComprobante => btnUltimoComprobante;
        protected override Button BtnMesa => btnMesa;
        protected override Button BtnAvanzar => btnAvanzar;
        protected override Button BtnCobrar => btnCobrar;
        protected override Button BtnEditarVenta => btnEditarVenta;
        protected override Button BtnCancelar => btnCancelar;
        protected override Button BtnPedidoPdf => btnPedidoPdf;
        protected override Button BtnFacturaPdf => btnFacturaPdf;
        protected override Button BtnResumenMensual => btnResumenMensual;
        protected override Panel PanelResumenDiario => panelResumenDiario;
        protected override DateTimePicker DiaCaja => diaCaja;
        protected override Label LblGuia => lblGuia;

        private void FrmBandeja_Load(object sender, EventArgs e)
        {
            Procesar_FrmBandeja_Load(sender, e);
        }

        private void btnAvanzar_Click(object sender, EventArgs e)
        {
            Procesar_btnAvanzar_Click(sender, e);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Procesar_btnCancelar_Click(sender, e);
        }

        private void btnCobrar_Click(object sender, EventArgs e)
        {
            Procesar_btnCobrar_Click(sender, e);
        }

        private void btnEditarVenta_Click(object sender, EventArgs e)
        {
            Procesar_btnEditarVenta_Click(sender, e);
        }

        private void btnFacturaPdf_Click(object sender, EventArgs e)
        {
            Procesar_btnFacturaPdf_Click(sender, e);
        }

        private void btnMesa_Click(object sender, EventArgs e)
        {
            Procesar_btnMesa_Click(sender, e);
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Procesar_btnNuevo_Click(sender, e);
        }

        private void btnPedidoPdf_Click(object sender, EventArgs e)
        {
            Procesar_btnPedidoPdf_Click(sender, e);
        }

        private void btnRecargar_Click(object sender, EventArgs e)
        {
            Procesar_btnRecargar_Click(sender, e);
        }

        private void btnResumenDiario_Click(object sender, EventArgs e)
        {
            Procesar_btnResumenDiario_Click(sender, e);
        }

        private void btnResumenMensual_Click(object sender, EventArgs e)
        {
            Procesar_btnResumenMensual_Click(sender, e);
        }

        private void btnSeleccionar_Click(object sender, EventArgs e)
        {
            Procesar_btnSeleccionar_Click(sender, e);
        }

        private void btnUltimoComprobante_Click(object sender, EventArgs e)
        {
            Procesar_btnUltimoComprobante_Click(sender, e);
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            Procesar_btnVolver_Click(sender, e);
        }

        private void chkFiltrarMes_CheckedChanged(object sender, EventArgs e)
        {
            Procesar_chkFiltrarMes_CheckedChanged(sender, e);
        }

        private void filtro_Changed(object sender, EventArgs e)
        {
            Procesar_filtro_Changed(sender, e);
        }

        private void listado_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Procesar_listado_CellClick(sender, e);
        }

        private void listado_Enter(object sender, EventArgs e)
        {
            Procesar_listado_Enter(sender, e);
        }

        private void listado_SelectionChanged(object sender, EventArgs e)
        {
            Procesar_listado_SelectionChanged(sender, e);
        }
    }
}
