using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SistemaPedidoRestaurante.BLL;
using SistemaPedidoRestaurante.UIPrincipal;

namespace SistemaPedidoRestaurante.UICliente
{
    public partial class FrmMenuCliente : Form
    {
        public FrmMenuCliente()
        {
            InitializeComponent();
            hacerPedidoToolStripMenuItem.Enabled = true;
            hacerPedidoToolStripMenuItem.Text = "Mi pedido (en preparación)";
            hacerPedidoToolStripMenuItem.Click += (sender, e) =>
            {
                using (var pedido = new FrmPedidoCliente()) pedido.ShowDialog(this);
            };
            reservarMesaToolStripMenuItem.Enabled = false;
            verComprasToolStripMenuItem.Enabled = false;
            hacerPedidoToolStripMenuItem.ToolTipText = "Pedidos del cliente: en preparación.";
            reservarMesaToolStripMenuItem.ToolTipText = "Reservas: en preparación.";
            verComprasToolStripMenuItem.ToolTipText = "Historial del cliente: en preparación.";
            MenuSesion.Configurar(this, menuStrip1, 3);
            salirToolStripMenuItem.Click += (sender, e) =>
            {
                this.DialogResult = DialogResult.Abort;
                this.Close();
            };
        }

        private void FrmMenuCliente_FormClosed(object sender, FormClosedEventArgs e)
        {
            SesionActual.Cerrar();
        }
    }
}
