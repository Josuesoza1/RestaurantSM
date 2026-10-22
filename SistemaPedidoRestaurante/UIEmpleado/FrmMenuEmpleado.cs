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
        }

        private void pedidosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var formulario = new FrmPedido()) formulario.ShowDialog(this);
        }

        private void pagosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var formulario = new FrmPago()) formulario.ShowDialog(this);
        }

        private void facturasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var formulario = new FrmFacturas()) formulario.ShowDialog(this);
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Abort;
            this.Close();
        }

        private void FrmMenuEmpleado_FormClosed(object sender, FormClosedEventArgs e)
        {
            SesionActual.Cerrar();
        }
    }
}
