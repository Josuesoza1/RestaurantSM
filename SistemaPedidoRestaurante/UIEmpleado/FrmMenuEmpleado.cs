using System;
using System.Windows.Forms;
using SistemaPedidoRestaurante.BLL;
using SistemaPedidoRestaurante.UIPrincipal;

namespace SistemaPedidoRestaurante.UIEmpleado
{
    public partial class FrmMenuEmpleado : Form
    {
        public FrmMenuEmpleado()
        {
            InitializeComponent();
            MenuSesion.Configurar(this, menuStrip1, 2);
            Load += ConfigurarCargo;
        }

        private void ConfigurarCargo(object sender, EventArgs e)
        {
            if (DesignMode || System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime)
                return;
            if (!SesionActual.TieneRol(2)) return;
            pedidosToolStripMenuItem.Available = Permisos.PuedeConsultarPedidos;
            pagosToolStripMenuItem.Available = facturasToolStripMenuItem.Available = Permisos.PuedeCobrar;
            reservasToolStripMenuItem.Available = mesasToolStripMenuItem.Available = Permisos.PuedeAtender;
            operacionesToolStripMenuItem.Available = Permisos.PuedeConsultarPedidos;
            lblTitulo.Text = "EMPLEADOS — " + SesionActual.Cargo.ToUpperInvariant();
            if (Permisos.PuedeAtender)
                lblEstado.Text = "Registre y atienda pedidos, asigne mesas y gestione reservas. Los cobros corresponden al cajero.";
            else if (Permisos.PuedeCobrar)
                lblEstado.Text = "Consulte los pedidos entregados, registre cobros y consulte o exporte facturas.";
            else
                lblEstado.Text = "Su cargo todavía no está habilitado. Solicite al administrador que seleccione Mesero o Cajero en Empleados y vuelva a iniciar sesión.";
        }

        private void pedidosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var formulario = new FrmPedidosEmpleado()) formulario.ShowDialog(this);
        }

        private void pagosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var formulario = new FrmPago()) formulario.ShowDialog(this);
        }

        private void facturasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var formulario = new FrmFacturas()) formulario.ShowDialog(this);
        }

        private void reservas_Click(object sender, EventArgs e)
        {
            using (FrmReservasEmpleado formulario = new FrmReservasEmpleado()) formulario.ShowDialog(this);
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Abort;
            this.Close();
        }
        private void mesas_Click(object sender, EventArgs e)
        {
            using (FrmMesasEmpleado formulario = new FrmMesasEmpleado()) formulario.ShowDialog(this);
        }

        private void FrmMenuEmpleado_FormClosed(object sender, FormClosedEventArgs e)
        {
            SesionActual.Cerrar();
        }
        private void cerrarSesionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
