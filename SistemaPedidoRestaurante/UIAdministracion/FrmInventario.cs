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
    public partial class FrmInventario : Form
    {
        public FrmInventario()
        {
            InitializeComponent();
            PantallaPendiente.Configurar(this, 1);
        }

        private void FrmInventario_Load(object sender, EventArgs e)
        {

        }
    }
}
