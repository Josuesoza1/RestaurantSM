using System;
using System.Windows.Forms;
using SistemaPedidoRestaurante.BLL;
using SistemaPedidoRestaurante.Entities;
using SistemaPedidoRestaurante.UIPrincipal;

namespace SistemaPedidoRestaurante.UICliente
{
    public partial class FrmMisDatos : Form
    {
        public FrmMisDatos()
        {
            InitializeComponent();
            ValidacionEntradaUI.Nombres(txtNombre, txtSegundoNombre, txtApellido, txtSegundoApellido);
            ValidacionEntradaUI.Telefonos(txtTelefono);
            ValidacionEntradaUI.Cedulas(txtCedula);
            txtEmail.MaxLength = 120;
            TemaUI.Aplicar(this);
            Load += FrmMisDatos_CargarDatos;
        }

        private void btnVolver_Click(object s, EventArgs e)
        {
            Close();
        }

        private void btnGuardar_Click(object s, EventArgs e)
        {
            try
            {
                new ClienteBLL().ActualizarMiPerfil(new Cliente
                {
                    PrimerNombre = txtNombre.Text,
                    SegundoNombre = txtSegundoNombre.Text,
                    PrimerApellido = txtApellido.Text,
                    SegundoApellido = txtSegundoApellido.Text,
                    Cedula = txtCedula.Text,
                    Telefono = txtTelefono.Text,
                    Email = txtEmail.Text,
                    Genero = cmbGenero.SelectedItem as string
                });
                MessageBox.Show(this, "Datos actualizados.", "Mis datos");
            }
            catch (Exception error)
            {
                MessageBox.Show(this, error.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void FrmMisDatos_CargarDatos(object sender, EventArgs e)
        {
            if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime || this.Site?.DesignMode == true)
            {
                return;
            }

            if (!SesionActual.TieneRol(3))
            {
                Close();
                return;
            }

            try
            {
                var c = new ClienteBLL().ObtenerMiPerfil();
                if (c == null)
                {
                    btnGuardar.Enabled = false;
                    throw new ArgumentException("La cuenta no tiene perfil activo.");
                }

                txtNombre.Text = c.PrimerNombre;
                txtSegundoNombre.Text = c.SegundoNombre;
                txtApellido.Text = c.PrimerApellido;
                txtSegundoApellido.Text = c.SegundoApellido;
                txtCedula.Text = c.Cedula;
                txtTelefono.Text = c.Telefono;
                txtEmail.Text = c.Email;
                cmbGenero.SelectedItem = c.Genero;
            }
            catch (Exception error)
            {
                MessageBox.Show(this, error.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
