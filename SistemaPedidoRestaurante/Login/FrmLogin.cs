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
            txtUsuario.MaxLength = 50;
            txtContrasena.MaxLength = 255;
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
            bool salir = false;
            try
            {
                int perfil = usuario.AccesoPersonal ? usuario.IdRol : RolesSistema.Cliente;
                SesionActual.Iniciar(usuario, perfil);
                if (usuario.IdRol != RolesSistema.Cliente)
                {
                    using (FrmElegirPerfil elegir = new FrmElegirPerfil(usuario))
                    {
                        if (elegir.ShowDialog(this) != DialogResult.OK) return;
                        usuario = elegir.Cuenta;
                        perfil = elegir.RolElegido;
                    }
                }
                SesionActual.Iniciar(usuario, perfil);
                using (Form menu = CrearMenuSegunRol(perfil))
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
                using (FrmCrearCuenta crearCuenta = new FrmCrearCuenta())
                {
                    crearCuenta.ShowDialog(this);
                }
            }
            finally
            {
                if (!this.IsDisposed && !this.Disposing)
                {
                    this.Show();
                }
            }
        }
    }
}
