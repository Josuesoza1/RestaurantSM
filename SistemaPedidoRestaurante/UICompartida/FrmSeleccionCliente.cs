using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using SistemaPedidoRestaurante.BLL;
using SistemaPedidoRestaurante.UIPrincipal;

namespace SistemaPedidoRestaurante.UICompartida
{
    public partial class FrmSeleccionCliente : Form
    {

        private DataTable clientes;
        private ClienteBLL servicio = new ClienteBLL();
        public DataRow Cliente { get; private set; }

        public FrmSeleccionCliente()
        {
            InitializeComponent();
        }

        private void cargar(object sender, EventArgs e)
        {
            if (DesignMode || System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime)
                return;
            if (!AccesoFormulario.PermitirPersonal(this)) return;
            try
            {
                clientes = servicio.ListarClientes();
                listado.DataSource = clientes;
                listado.ClearSelection();
            }
            catch (Exception error)
            {
                MessageBox.Show(this, error.Message);
            }
        }

        private void buscar_TextChanged(object sender, EventArgs e)
        {
            if (clientes != null)
            {
                listado.DataSource = BusquedaLINQ.Filtrar(clientes, buscar.Text);
                listado.ClearSelection();
            }
        }

        private void elegir_Click(object sender, EventArgs e)
        {
            Cliente = VistaGestion.Fila(listado);
            if (Cliente == null)
            {
                MessageBox.Show(this, "Seleccione un cliente.");
                return;
            }

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
