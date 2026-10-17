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
    public partial class FrmFacturas : Form
    {
        public FrmFacturas()
        {
            InitializeComponent();
            PantallaPendiente.Configurar(this, 1, 2);
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {

        }
    }
}
