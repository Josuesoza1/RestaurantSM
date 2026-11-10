using System;
using System.Windows.Forms;
using SistemaPedidoRestaurante.UICompartida;

namespace SistemaPedidoRestaurante.UIEmpleado
{
    public partial class FrmPedido : FrmPedidoLocalBase
    {
        public FrmPedido() : base(2)
        {
            InitializeComponent();
            PrepararVista();
        }

        protected override NumericUpDown NudCantidad => nudCantidad;
        protected override Label EstadoPerfil => estadoPerfil;
        protected override Label LblEstadoCatalogo => lblEstadoCatalogo;
        protected override Button BtnRecargarCatalogo => btnRecargarCatalogo;
        protected override DataGridView DgvDetalle => dgvDetalle;
        protected override Button BtnQuitar => btnQuitar;
        protected override Button BtnVaciar => btnVaciar;
        protected override Button BtnEnviar => btnEnviar;
        protected override Button BtnCobrarAhora => btnCobrarAhora;
        protected override NumericUpDown NudCantidadDetalle => nudCantidadDetalle;
        protected override Button BtnCambiarCantidad => btnCambiarCantidad;
        protected override Button BtnGuardarCambios => btnGuardarCambios;
        protected override Label LblTotal => lblTotal;
        protected override TextBox TxtBuscarProducto => txtBuscarProducto;
        protected override Label LblTitulo => lblTitulo;
        protected override Label LblNombreOpcional => lblNombreOpcional;
        protected override TextBox TxtCliente => txtCliente;
        protected override Button BtnAceptar => btnAceptar;
        protected override ComboBox CmbPrecio => cmbPrecio;
        protected override ComboBox CmbProducto => cmbProducto;
        protected override ComboBox CmbCategoria => cmbCategoria;

        private void CargarPantalla(object sender, EventArgs e)
        {
            Procesar_CargarPantalla(sender, e);
        }

        private void FrmPedido_FormClosing(object sender, FormClosingEventArgs e)
        {
            Procesar_FrmPedidoLocalBase_FormClosing(sender, e);
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            Procesar_btnAceptar_Click(sender, e);
        }

        private void btnCambiarCantidad_Click(object sender, EventArgs e)
        {
            Procesar_btnCambiarCantidad_Click(sender, e);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Procesar_btnCancelar_Click(sender, e);
        }

        private void btnCobrarAhora_Click(object sender, EventArgs e)
        {
            Procesar_btnCobrarAhora_Click(sender, e);
        }

        private void btnEnviar_Click(object sender, EventArgs e)
        {
            Procesar_btnEnviar_Click(sender, e);
        }

        private void btnGuardarCambios_Click(object sender, EventArgs e)
        {
            Procesar_btnGuardarCambios_Click(sender, e);
        }

        private void btnQuitar_Click(object sender, EventArgs e)
        {
            Procesar_btnQuitar_Click(sender, e);
        }

        private void btnRecargarCatalogo_Click(object sender, EventArgs e)
        {
            Procesar_btnRecargarCatalogo_Click(sender, e);
        }

        private void btnVaciar_Click(object sender, EventArgs e)
        {
            Procesar_btnVaciar_Click(sender, e);
        }

        private void cmbCategoria_SelectedValueChanged(object sender, EventArgs e)
        {
            Procesar_cmbCategoria_SelectedValueChanged(sender, e);
        }

        private void cmbProducto_SelectedValueChanged(object sender, EventArgs e)
        {
            Procesar_cmbProducto_SelectedValueChanged(sender, e);
        }

        private void dgvDetalle_SelectionChanged(object sender, EventArgs e)
        {
            Procesar_dgvDetalle_SelectionChanged(sender, e);
        }

        private void nudCantidad_ValueChanged(object sender, EventArgs e)
        {
            Procesar_nudCantidad_ValueChanged(sender, e);
        }

        private void txtBuscarProducto_TextChanged(object sender, EventArgs e)
        {
            Procesar_txtBuscarProducto_TextChanged(sender, e);
        }
    }
}
