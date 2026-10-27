using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using SistemaPedidoRestaurante.BLL;

namespace SistemaPedidoRestaurante.UIAdministracion
{
    public partial class FrmConfiguracionRestaurante : Form
    {

        private ConfiguracionRestauranteBLL servicio = new ConfiguracionRestauranteBLL();
        public FrmConfiguracionRestaurante()
        {
            InitializeComponent();
        }

        private void cargar(object sender, EventArgs e)
        {
            if (DesignMode || System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime)
                return;
            if (!SesionActual.TieneRol(1))
            {
                Close();
                return;
            }

            try
            {
                DataRow datos = servicio.VerificarInstalacion().Rows[0];
                nombre.Text = Convert.ToString(datos["Nombre"]);
                tasa.Value = Convert.ToDecimal(datos["TasaImpuesto"]);
            }
            catch (Exception error)
            {
                MessageBox.Show(this, error.Message);
            }
        }

        private void guardar_Click(object sender, EventArgs e)
        {
            try
            {
                servicio.GuardarConfiguracion(nombre.Text, tasa.Value);
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
