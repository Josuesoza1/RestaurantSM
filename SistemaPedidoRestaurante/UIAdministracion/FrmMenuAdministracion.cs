using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SistemaPedidoRestaurante.UIAdministracion;
using SistemaPedidoRestaurante.UICliente;
namespace SistemaPedidoRestaurante.UIAdministracion
{
    public partial class FrmMenuAdministracion : Form
    {
        public FrmMenuAdministracion()
        {
            InitializeComponent();
        }
        private void categoríasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmCategoria categoria = new FrmCategoria();
            categoria.Show();
            this.Hide();
        }
        private void productosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmProducto producto = new FrmProducto();
            producto.Show();
            this.Hide();
        }
        private void empleadosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmEmpleados empleados = new FrmEmpleados();
            empleados.Show();
            this.Hide();
        }
        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void rolesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            {
                FrmRoles roles = new FrmRoles();
                roles.Show();
                this.Hide();
            }
        }
        private void FrmMenuAdministracion_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
