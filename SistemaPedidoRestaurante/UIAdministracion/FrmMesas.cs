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

namespace SistemaPedidoRestaurante.UIAdministracion
{
    public partial class FrmMesas : Form
    {
        public FrmMesas()
        {
            InitializeComponent();
            PantallaPendiente.Configurar(this, 1);
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void volverToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
