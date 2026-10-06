using SistemaPedidoRestaurante.BLL;
using SistemaPedidoRestaurante.Entities;
using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace SistemaPedidoRestaurante.Loggin
{
    public partial class FrmCrearCuenta : Form
    {
        private readonly ClienteBLL _clienteBLL = new ClienteBLL();

        public FrmCrearCuenta()
        {
            InitializeComponent();
        }

        private void btnCrearCuenta_Click(object sender, EventArgs e)
        {
            try
            {
                Cliente nuevoCliente = new Cliente
                {
                    PrimerNombre = txtPnombre.Text.Trim(),
                    SegundoNombre = txtSnombre.Text.Trim(),
                    PrimerApellido = txtPapellido.Text.Trim(),
                    SegundoApellido = txtSapellido.Text.Trim(),
                    Telefono = mtxtTelefono.Text,
                    Email = txtEmail.Text.Trim(),
                    Cedula = mtxtCedula.Text
                };

                Usuario nuevoUsuario = new Usuario
                {
                    NombreUsuario = txtUsuario.Text.Trim(),
                    Contrasena = txtContrasena.Text.Trim()
                };

                string confirmarContrasena = txtConfirmarContrasena.Text.Trim();

                _clienteBLL.InsertarCliente(nuevoCliente, nuevoUsuario, confirmarContrasena);

                MessageBox.Show("Cuenta creada con éxito. Ya puedes iniciar sesión.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                FrmLogin login = new FrmLogin();
                login.Show();
                this.Close();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtPnombre.Clear();
            txtSnombre.Clear();
            txtPapellido.Clear();
            txtSapellido.Clear();
            mtxtTelefono.Clear();
            txtEmail.Clear();
            mtxtCedula.Clear();
            txtUsuario.Clear();
            txtContrasena.Clear();
            txtConfirmarContrasena.Clear();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            FrmLogin login = new FrmLogin();
            login.Show();
            this.Close();
        }

        private void btnIniciarSesion_Click(object sender, EventArgs e)
        {
            FrmLogin login = new FrmLogin();
            login.Show();
            this.Close();
        }
    }
}