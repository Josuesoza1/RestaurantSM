using System;
using System.Windows.Forms;
using SistemaPedidoRestaurante.BLL;
using SistemaPedidoRestaurante.UICompartida;
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
            nuevaVentaToolStripMenuItem.Available = Permisos.PuedeAtender;
            lblTitulo.Text = "CAJA";
            lblEstado.Text = "Pulse Nueva venta para atender a un cliente. En Ventas abiertas puede retomar una venta guardada.";
        }

        private void nuevaVentaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var formulario = PantallasPersonal.CrearPedido())
                formulario.ShowDialog(this);
        }

        private void pedidosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var formulario = new FrmPedidosEmpleado()) formulario.ShowDialog(this);
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
