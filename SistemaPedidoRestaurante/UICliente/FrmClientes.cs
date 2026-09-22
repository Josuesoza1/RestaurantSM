using SistemaPedidoRestaurante.BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaPedidoRestaurante.UICliente
{
    
    public partial class FrmClientes : Form
    {
        public FrmClientes()
        {
            InitializeComponent();
        }


        private void CargarDatos()
        {
            try
            {
                dgvClientes.AutoGenerateColumns = false;

                dgvClientes.Columns[0].DataPropertyName = "IdCliente";
                dgvClientes.Columns[1].DataPropertyName = "Nombre";
                dgvClientes.Columns[2].DataPropertyName = "Direccion";
                dgvClientes.Columns[3].DataPropertyName = "Telefono";
                dgvClientes.Columns[4].DataPropertyName = "Email";

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error de Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void FrmClientes_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void FrmClientes_Load(object sender, EventArgs e)
        {
            CargarDatos();
        }
    }
}
