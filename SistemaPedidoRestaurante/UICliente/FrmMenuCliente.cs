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
            hacerPedidoToolStripMenuItem.Text = "Mi pedido";
            hacerPedidoToolStripMenuItem.Click += (sender, e) =>
            {
                using (var pedido = new FrmPedidoCliente()) pedido.ShowDialog(this);
            };
            reservarMesaToolStripMenuItem.Enabled = true;
            verComprasToolStripMenuItem.Enabled = true;
            hacerPedidoToolStripMenuItem.ToolTipText = "Elegir productos y enviar mi pedido.";
            reservarMesaToolStripMenuItem.ToolTipText = "Crear, consultar, editar y cancelar mis reservas.";
            verComprasToolStripMenuItem.ToolTipText = "Consultar, editar y cancelar mis pedidos pendientes.";
            reservarMesaToolStripMenuItem.Click+=(s,e)=>{using(var f=new FrmMisReservas())f.ShowDialog(this);};
            verComprasToolStripMenuItem.Text="Mis compras";
            verComprasToolStripMenuItem.Click+=(s,e)=>{using(var f=new FrmMisCompras())f.ShowDialog(this);};
            var datos=new ToolStripMenuItem("Mis datos");
            datos.Click+=(s,e)=>{using(var f=new FrmMisDatos())f.ShowDialog(this);};
            menuStrip1.Items.Insert(menuStrip1.Items.Count-1,datos);
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
