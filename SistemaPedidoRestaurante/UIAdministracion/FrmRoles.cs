using System;
using System.Windows.Forms;
using SistemaPedidoRestaurante.BLL;

namespace SistemaPedidoRestaurante.UIAdministracion
{
    public partial class FrmRoles : Form
    {
        private readonly RolBLL _rolBLL = new RolBLL();

        public FrmRoles()
        {
            InitializeComponent();
        }

        

        private void CargarDatos()
        {
            try
            {
                dgvRoles.AutoGenerateColumns = false;
                dgvRoles.Columns[0].DataPropertyName = "IdRol";
                dgvRoles.Columns[1].DataPropertyName = "Nombre";
                dgvRoles.DataSource = _rolBLL.ObtenerRoles();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            FrmMenuAdministracion menu = new FrmMenuAdministracion();
            menu.Show();
            this.Close();
        }

        private void FrmRoles_Load(object sender, EventArgs e)
        {
            CargarDatos();
        }

        private void FrmRoles_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}