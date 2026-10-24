using System;
using SistemaPedidoRestaurante.UIPrincipal;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaPedidoRestaurante.UIEmpleado
{
    public partial class FrmPedido : Form
    {
        public FrmPedido()
        {
            InitializeComponent();
            PantallaPendiente.Configurar(this, 1, 2);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void lblPnombre_Click(object sender, EventArgs e)
        {
        }

        private void agregarToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }
    }
}
