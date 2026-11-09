using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using SistemaPedidoRestaurante.BLL;

namespace SistemaPedidoRestaurante.UICompartida
{
    public partial class FrmElegirMesa : Form
    {

        private MesaBLL servicio = new MesaBLL();
        private readonly int pedido;
        public FrmElegirMesa() : this(0)
        {
        }

        public FrmElegirMesa(int pedido)
        {
            this.pedido = pedido;
            InitializeComponent();
            if (pedido > 0) Text = "Asignar mesa a la venta " + pedido;
        }

        private void cargar(object sender, EventArgs e)
        {
            if (DesignMode || System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime)
                return;
            if (!AccesoFormulario.PermitirAtencion(this)) return;
            try
            {
                DataView disponibles = servicio.ListarMesas().DefaultView;
                disponibles.RowFilter = "Estado = 'Disponible'";
                mesas.DataSource = disponibles;
                asignar.Enabled = disponibles.Count > 0;
                if (disponibles.Count == 0)
                    lblMesa.Text = "No hay mesas disponibles.";
            }
            catch (Exception error)
            {
                asignar.Enabled = false;
                MessageBox.Show(this, error.Message);
            }
        }

        private void asignar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!(mesas.SelectedValue is int))
                    throw new ArgumentException("Seleccione una mesa.");
                servicio.AsignarMesa(pedido, (int)mesas.SelectedValue);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception error)
            {
                MessageBox.Show(this, error.Message);
            }
        }
    }
}
