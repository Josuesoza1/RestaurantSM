using SistemaPedidoRestaurante.BLL;
using System;
using System.Windows.Forms;


namespace SistemaPedidoRestaurante.UIAdministracion
{
    public partial class FrmProducto : Form
    {
        private readonly ProductoBLL _productoBLL = new ProductoBLL();

        public FrmProducto()
        {
            InitializeComponent();
        }

        

        private void CargarDatos()
        {
            try
            {
                dgvProductos.AutoGenerateColumns = false;

                dgvProductos.Columns[0].DataPropertyName = "IdProducto";
                dgvProductos.Columns[1].DataPropertyName = "CategoriaNombre";
                dgvProductos.Columns[2].DataPropertyName = "Nombre";
                dgvProductos.Columns[3].DataPropertyName = "Precio";
                dgvProductos.Columns[4].DataPropertyName = "Codigo";
                dgvProductos.Columns[5].DataPropertyName = "Disponible";

                dgvProductos.DataSource = _productoBLL.ObtenerProductos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error de Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnVolver_Click(object sender, EventArgs e)
        {
            FrmMenuAdministracion menu = new FrmMenuAdministracion();
            menu.Show();
            this.Close();
        }

        private void FrmProducto_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void FrmProducto_Load_1(object sender, EventArgs e)
        {
            CargarDatos();
        }
    }
}