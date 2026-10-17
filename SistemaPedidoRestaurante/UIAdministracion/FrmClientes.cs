using SistemaPedidoRestaurante.BLL;
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
    
    public partial class FrmClientes : Form
    {
        public FrmClientes()
        {
            InitializeComponent();
            PantallaPendiente.Configurar(this, 1);
        }


        private void FrmClientes_Load(object sender, EventArgs e)
        {
            if (DesignMode || System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime)
                return;

            // El listado se conectará al implementar la gestión de clientes.
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {

        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {

        }
    }
}
