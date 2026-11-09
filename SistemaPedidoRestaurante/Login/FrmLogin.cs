using System;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Windows.Forms;
using SistemaPedidoRestaurante.BLL;
using SistemaPedidoRestaurante.Entities;
using SistemaPedidoRestaurante.UIAdministracion;
using SistemaPedidoRestaurante.UIEmpleado;

namespace SistemaPedidoRestaurante.Loggin
{
    public partial class FrmLogin : Form
    {
        private readonly UsuarioBLL _usuarioBLL = new UsuarioBLL();
        public FrmLogin()
        {
            InitializeComponent();
            ActiveControl = txtUsuario;
        }

        private void btnIniciarSesion_Click(object sender, EventArgs e)
        {
            try
            {
                string usuario = txtUsuario.Text.Trim();
                string contrasena = txtContrasena.Text;
                Usuario usuarioLogueado = _usuarioBLL.IniciarSesion(usuario, contrasena);
                AbrirSesion(usuarioLogueado);
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(this, ex.Message, "Revise los datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (!string.IsNullOrWhiteSpace(txtUsuario.Text) && string.IsNullOrWhiteSpace(txtContrasena.Text))
                    txtContrasena.Focus();
                else
                    txtUsuario.Focus();
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show(this, "Revise el usuario y la contraseña. Si sigue sin poder entrar, avise al encargado.",
                    "No se pudo iniciar sesión", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtContrasena.Clear();
                txtContrasena.Focus();
            }
            catch (SqlException ex)
            {
                Trace.TraceError(ex.ToString());
                MessageBox.Show(this, "No se pudo acceder a los datos del restaurante. Intente de nuevo. Si el problema continúa, avise al encargado.",
                    "No se pudo iniciar sesión", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
                MessageBox.Show(this, "No se pudo abrir el sistema. Cierre el programa y vuelva a abrirlo. Si el problema continúa, avise al encargado.",
                    "No se pudo iniciar sesión", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AbrirSesion(Usuario usuario)
        {
            bool salir = false;
            try
            {
                SesionActual.Iniciar(usuario);
                using (Form menu = CrearMenuSegunRol(usuario.IdRol))
                {
                    Hide();
                    salir = menu.ShowDialog(this) == DialogResult.Abort;
                }
            }
            finally
            {
                SesionActual.Cerrar();
                if (!IsDisposed && !Disposing)
                {
                    txtContrasena.Clear();
                    if (salir) Close();
                    else { Show(); txtUsuario.Focus(); }
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
                default:
                    throw new UnauthorizedAccessException("El usuario no tiene un rol de acceso válido.");
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
