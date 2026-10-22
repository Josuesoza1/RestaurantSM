using System;
using System.Windows.Forms;
using SistemaPedidoRestaurante.BLL;
using SistemaPedidoRestaurante.Entities;
using SistemaPedidoRestaurante.UIAdministracion;
using SistemaPedidoRestaurante.UICliente;
using SistemaPedidoRestaurante.UIEmpleado;
namespace SistemaPedidoRestaurante.Loggin
{
    public partial class FrmLogin : Form
    {
        private readonly UsuarioBLL _usuarioBLL = new UsuarioBLL();

        public FrmLogin()
        {
            InitializeComponent();
            txtUsuario.MaxLength=50; txtContrasena.MaxLength=255;
        }

        private void btnIniciarSesion_Click(object sender, EventArgs e)
        {
            try
            {
                string usuario = txtUsuario.Text.Trim();
                string contrasena = txtContrasena.Text.Trim();

                Usuario usuarioLogueado = _usuarioBLL.IniciarSesion(usuario, contrasena);    

                AbrirSesion(usuarioLogueado);
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

        private void AbrirSesion(Usuario usuario)
        {
            using (Form menu = CrearMenuSegunRol(usuario.IdRol))
            {
                SesionActual.Iniciar(usuario);
                this.Hide();
                bool salir = false;
                try
                {
                    salir = menu.ShowDialog(this) == DialogResult.Abort;
                }
                finally
                {
                    SesionActual.Cerrar();
                    if (!this.IsDisposed && !this.Disposing)
                    {
                        txtContrasena.Clear();
                        if (salir)
                            this.Close();
                        else
                        {
                            this.Show();
                            txtUsuario.Focus();
                        }
                    }
                }
            }
        }

        private Form CrearMenuSegunRol(int idRol)
        {
            switch (idRol)
            {
                case 1:
                    return new FrmMenuAdministracion();
                case 2:
                    return new FrmMenuEmpleado();
                case 3:
                    return new FrmMenuCliente();
                default:
                    throw new UnauthorizedAccessException("El usuario no tiene un rol de acceso válido.");
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnCrearCuenta_Click(object sender, EventArgs e)
        {
            this.Hide();
            try
            {
                using (var crearCuenta = new FrmCrearCuenta())
                    crearCuenta.ShowDialog(this);
            }
            finally
            {
                if (!this.IsDisposed && !this.Disposing)
                    this.Show();
            }
        }
    }
}
