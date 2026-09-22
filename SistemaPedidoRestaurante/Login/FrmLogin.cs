using System;
using System.Windows.Forms;
using SistemaPedidoRestaurante.BLL;
using SistemaPedidoRestaurante.Entities;
using SistemaPedidoRestaurante.UIAdministracion;
using SistemaPedidoRestaurante.UICliente;
namespace SistemaPedidoRestaurante.Loggin
{
    public partial class FrmLogin : Form
    {
        private readonly UsuarioBLL _usuarioBLL = new UsuarioBLL();

        public FrmLogin()
        {
            InitializeComponent();
        }

        private void btnIniciarSesion_Click(object sender, EventArgs e)
        {
            try
            {
                string usuario = txtUsuario.Text.Trim();
                string contrasena = txtContrasena.Text.Trim();

                Usuario usuarioLogueado = _usuarioBLL.IniciarSesion(usuario, contrasena);    

                if (usuarioLogueado.IdRol == 1)
                {
                    FrmMenuAdministracion ventanaPrincipal = new FrmMenuAdministracion();
                    ventanaPrincipal.Show();
                }
                else
                {
                    FrmMenuCliente clientes = new FrmMenuCliente();
                    clientes.Show();
                }

                this.Hide();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsuario.Focus();
            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show(ex.Message, "Acceso Denegado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtContrasena.Clear();
                txtContrasena.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error de Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void btnCrearCuenta_Click(object sender, EventArgs e)
        {
            FrmCrearCuenta crearCuenta = new FrmCrearCuenta();
            crearCuenta.Show();
            this.Hide();
        }
    }
}