using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using SistemaPedidoRestaurante.BLL;
using SistemaPedidoRestaurante.UIPrincipal;

namespace SistemaPedidoRestaurante.UIAdministracion
{
    public partial class FrmConfiguracionRestaurante : Form
    {

        private ConfiguracionRestauranteBLL servicio = new ConfiguracionRestauranteBLL();
        private (string Nombre, decimal Tasa) datosIniciales;
        private bool configuracionCargada;
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
                DataRow datos = servicio.ObtenerConfiguracion().Rows[0];
                nombre.Text = Convert.ToString(datos["Nombre"]);
                tasa.Value = Convert.ToDecimal(datos["TasaImpuesto"]);
                datosIniciales = (nombre.Text, tasa.Value);
                configuracionCargada = true;
            }
            catch (Exception error)
            {
                guardar.Enabled = false;
                MessageBox.Show(this, error.Message);
            }
        }

        private void FrmConfiguracionRestaurante_FormClosing(object sender, FormClosingEventArgs e)
        {
            bool hayCambios = configuracionCargada && DialogResult != DialogResult.OK
                && !datosIniciales.Equals((nombre.Text, tasa.Value));
            if (!VistaGestion.ConfirmarDescarte(this, hayCambios))
                e.Cancel = true;
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
