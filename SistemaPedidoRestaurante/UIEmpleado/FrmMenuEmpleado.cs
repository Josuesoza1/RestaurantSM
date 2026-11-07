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

            operacionesToolStripMenuItem.Available = Permisos.PuedeAtender;
            lblTitulo.Text = "DESPACHO LOCAL";
            lblEstado.Text = "Registre, despache y cobre las ventas pendientes del local.";
        }

        private void pedidosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var formulario = new FrmPedidosEmpleado()) formulario.ShowDialog(this);
        }

        private void pagosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var formulario = new FrmPago()) formulario.ShowDialog(this);
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
