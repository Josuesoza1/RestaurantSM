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
            MenuSesion.Configurar(this, menuStrip1, 3);
        }

        private void hacerPedidoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FrmPedidoCliente formulario = new FrmPedidoCliente()) formulario.ShowDialog(this);
        }

        private void reservarMesaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FrmMisReservas formulario = new FrmMisReservas()) formulario.ShowDialog(this);
        }

        private void verComprasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FrmMisCompras formulario = new FrmMisCompras()) formulario.ShowDialog(this);
        }

        private void misDatosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FrmMisDatos formulario = new FrmMisDatos()) formulario.ShowDialog(this);
        }

        private void cerrarSesionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Abort;
            Close();
        }

        private void FrmMenuCliente_FormClosed(object sender, FormClosedEventArgs e)
        {
            SesionActual.Cerrar();
        }
    }
}
