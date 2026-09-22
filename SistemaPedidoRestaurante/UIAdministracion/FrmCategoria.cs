using System;
using System.Windows.Forms;
using SistemaPedidoRestaurante.BLL;

namespace SistemaPedidoRestaurante.UIAdministracion
{
    public partial class FrmCategoria : Form
    {
        private readonly CategoriaBLL _categoriaBLL = new CategoriaBLL();

        public FrmCategoria()
        {
            InitializeComponent();
        }

        


        private void CargarDatos()
        {
            try
            {
                dgvCategorias.AutoGenerateColumns = false;
                dgvCategorias.Columns[0].DataPropertyName = "IdCategoria";
                dgvCategorias.Columns[1].DataPropertyName = "Nombre";
                dgvCategorias.Columns[2].DataPropertyName = "Descripcion";
                dgvCategorias.DataSource = _categoriaBLL.ObtenerCategorias();
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

        private void FrmCategoria_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void FrmCategoria_Load(object sender, EventArgs e)
        {
            CargarDatos();
        }
    }
}