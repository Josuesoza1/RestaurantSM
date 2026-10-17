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
            menuStrip1.Items.Remove(pedidosToolStripMenuItem);
            menuStrip1.Items.Remove(cobrarToolStripMenuItem);
            pedidosToolStripMenuItem.Dispose();
            cobrarToolStripMenuItem.Dispose();
            var operaciones = new ToolStripMenuItem("Operaciones") { Name = "operacionesToolStripMenuItem" };
            operaciones.DropDownItems.Add(PantallaPendiente.Opcion(this, "pedidosToolStripMenuItem", "Pedidos en el local", () => new FrmPedido()));
            operaciones.DropDownItems.Add(PantallaPendiente.Opcion(this, "pagosToolStripMenuItem", "Cobros", () => new FrmPago()));
            operaciones.DropDownItems.Add(PantallaPendiente.Opcion(this, "facturasToolStripMenuItem", "Facturas", () => new FrmFacturas()));
            menuStrip1.Items.Insert(0, operaciones);
            lblEstado.Text = "Seleccione una pantalla desde Operaciones.\r\n\r\nEl registro de pedidos, cobros y facturas está en preparación.";
            MenuSesion.Configurar(this, menuStrip1, 2);
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
