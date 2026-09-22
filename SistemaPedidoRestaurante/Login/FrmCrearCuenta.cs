using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using SistemaPedidoRestaurante.BLL;
using SistemaPedidoRestaurante.Entities;

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
                if (txtContrasena.Text.Trim() != txtConfirmarContrasena.Text.Trim())
                {
                    MessageBox.Show("Las contraseñas no coinciden.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string patronEmail = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
                if (!Regex.IsMatch(txtEmail.Text.Trim(), patronEmail))
                {
                    MessageBox.Show("El formato del correo no es válido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

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

                _clienteBLL.InsertarCliente(nuevoCliente, nuevoUsuario);

                MessageBox.Show("Cuenta creada con éxito. Ya puedes iniciar sesión.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                FrmLogin login = new FrmLogin();
                login.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error fatal", MessageBoxButtons.OK, MessageBoxIcon.Error);
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