using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using SistemaPedidoRestaurante.BLL;
using SistemaPedidoRestaurante.Entities;

namespace SistemaPedidoRestaurante.UICompartida
{
    public partial class FrmPedidoLocalBase : Form
    {
        private readonly PedidoBorrador borrador = new PedidoBorrador();
        private Productos catalogo = new Productos();
        private bool perfilValido;
        private int clienteDelLocal;
        private readonly int rolPermitido;
        private readonly Guid solicitud = Guid.NewGuid();
        public FrmPedidoLocalBase() : this(0, 0)
        {
        }

        protected FrmPedidoLocalBase(int rol, int clienteDelLocal)
        {
            this.clienteDelLocal = clienteDelLocal;
            rolPermitido = rol;
            InitializeComponent();
            Text = "Nuevo pedido en el local";
            grpDatosCliente.Text = "Cliente del pedido";
            txtCliente.ReadOnly = txtCedula.ReadOnly = txtTelefono.ReadOnly = txtEmail.ReadOnly = true;
            cmbProducto.DropDownStyle = cmbCategoria.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbPrecio.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbPrecio.Enabled = false;
            btnAceptar.Text = "Agregar al pedido";
            btnAceptar.Enabled = false;
            btnCancelar.Text = "Volver";
            cmbCategoria.SelectedValueChanged += cmbCategoria_SelectedValueChanged;
            cmbProducto.SelectedValueChanged += cmbProducto_SelectedValueChanged;
            Load += CargarPantalla;
            FormClosing += FrmPedidoLocalBase_FormClosing;
            ActualizarDetalle();
            UIPrincipal.TemaUI.Aplicar(this);
        }

        private void CargarPantalla(object sender, EventArgs e)
        {
            if (DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime)
            {
                return;
            }

            if (!TieneAcceso())
            {
                Close();
                return;
            }

            try
            {
                if (clienteDelLocal <= 0)
                {
                    using (FrmSeleccionCliente elegir = new FrmSeleccionCliente())
                    {
                        if (elegir.ShowDialog(this) != DialogResult.OK) { Close(); return; }
                        clienteDelLocal = Convert.ToInt32(elegir.Cliente["IdCliente"]);
                    }
                }
                Cliente cliente;
                if (clienteDelLocal > 0)
                {
                    System.Data.DataRow[] filas = new ClienteBLL().ListarClientes().Select("IdCliente=" + clienteDelLocal);
                    if (filas.Length != 1) throw new ArgumentException("Seleccione un cliente activo.");
                    System.Data.DataRow fila = filas[0];
                    cliente = new Cliente();
                    cliente.PrimerNombre = Convert.ToString(fila["Nombre"]);
                    cliente.Cedula = Convert.ToString(fila["Cedula"]);
                    cliente.Telefono = Convert.ToString(fila["Telefono"]);
                    cliente.Email = Convert.ToString(fila["Email"]);
                }
                else throw new ArgumentException("Seleccione un cliente activo.");
                perfilValido = cliente != null;
                if (cliente != null)
                {
                    txtCliente.Text = string.Join(" ", new[]
                    {
                        cliente.PrimerNombre,
                        cliente.SegundoNombre,
                        cliente.PrimerApellido,
                        cliente.SegundoApellido
                    }.Where(n => !string.IsNullOrWhiteSpace(n)));
                    txtCedula.Text = cliente.Cedula;
                    txtTelefono.Text = cliente.Telefono;
                    txtEmail.Text = cliente.Email;
                }

                CargarCatalogo(new ProductoBLL().ObtenerDisponiblesParaPedido());


                ActualizarDetalle();
                if (!perfilValido)
                {
                    estadoPerfil.Text = "Seleccione un cliente activo antes de registrar el pedido.";
                }
            }
            catch (Exception)
            {
                perfilValido = false;
                btnAceptar.Enabled = btnEnviar.Enabled = false;
                estadoPerfil.Text = "No se pudieron cargar el cliente o el catálogo. Cierre la pantalla y vuelva a seleccionar un cliente activo.";
            }
        }

        private void CargarCatalogo(Productos productos)
        {
            catalogo = productos;
            var categorias = catalogo.GroupBy(p => p.IdCategoria).Select(g => new Categoria
            {
                IdCategoria = g.Key,
                Nombre = g.First().CategoriaNombre
            }).OrderBy(c => c.Nombre).ToList();
            categorias.Insert(0, new Categoria
            {
                IdCategoria = 0,
                Nombre = "Todas"
            });
            cmbCategoria.DisplayMember = "Nombre";
            cmbCategoria.ValueMember = "IdCategoria";
            cmbCategoria.DataSource = categorias;
            cmbCategoria.SelectedValue = 0;
            AplicarFiltro();
            if (catalogo.Count == 0)
            {
                estadoPerfil.Text = "No hay productos disponibles en este momento.";
            }
        }

        private void AplicarFiltro()
        {
            int id = cmbCategoria.SelectedValue is int ? (int)cmbCategoria.SelectedValue : 0;
            cmbProducto.DisplayMember = "Nombre";
            cmbProducto.ValueMember = "IdProducto";
            cmbProducto.DataSource = catalogo.Where(p => id == 0 || p.IdCategoria == id).OrderBy(p => p.Nombre).ToList();
            MostrarPrecio();
        }

        private void MostrarPrecio()
        {
            var producto = cmbProducto.SelectedItem as Producto;
            cmbPrecio.Items.Clear();
            if (producto != null)
            {
                cmbPrecio.Items.Add(producto.Precio.ToString("N2"));
                cmbPrecio.SelectedIndex = 0;
            }

            btnAceptar.Enabled = perfilValido && producto != null;
        }

        private void AgregarAlPedido(object sender, EventArgs e)
        {
            try
            {
                if (!TieneAcceso() || !perfilValido)
                {
                    throw new ArgumentException("Inicie sesión con una cuenta del personal.");
                }

                borrador.Agregar(cmbProducto.SelectedItem as Producto, (int)nudCantidad.Value);
                ActualizarDetalle();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Mi pedido", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void QuitarDelPedido(object sender, EventArgs e)
        {
            var linea = dgvDetalle.CurrentRow?.DataBoundItem as LineaPedidoBorrador;
            if (linea == null)
            {
                return;
            }

            borrador.Quitar(linea.IdProducto);
            ActualizarDetalle();
        }

        private void ActualizarDetalle()
        {
            dgvDetalle.DataSource = borrador.Lineas.ToList();
            lblTotal.Text = "Total estimado: " + borrador.Total.ToString("N2");
            btnQuitar.Enabled = btnVaciar.Enabled = borrador.Lineas.Count > 0;
            btnEnviar.Enabled = perfilValido && borrador.Lineas.Count > 0;
        }

        private void Enviar()
        {
            try
            {
                btnEnviar.Enabled = false;
                try
                {
                    int numero = new PedidoBLL().GuardarPedido(clienteDelLocal, borrador, solicitud);
                    MessageBox.Show(this, "Pedido " + numero + " guardado. Puede consultarlo en la lista de pedidos.", "Mi pedido");
                    DialogResult = DialogResult.OK;
                    Close();
                }
                finally
                {
                    if (!IsDisposed)
                    {
                        ActualizarDetalle();
                    }
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(this, error.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void lblPnombre_Click(object sender, EventArgs e)
        {
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            AgregarAlPedido(sender, e);
        }

        private void btnEnviar_Click(object sender, EventArgs e)
        {
            Enviar();
        }

        private void btnQuitar_Click(object sender, EventArgs e)
        {
            QuitarDelPedido(sender, e);
        }

        private void btnVaciar_Click(object sender, EventArgs e)
        {
            borrador.Vaciar();
            ActualizarDetalle();
        }

        private void cmbCategoria_SelectedValueChanged(object sender, EventArgs e)
        {
            AplicarFiltro();
        }

        private void cmbProducto_SelectedValueChanged(object sender, EventArgs e)
        {
            MostrarPrecio();
        }
        private void FrmPedidoLocalBase_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult != DialogResult.OK && borrador.Lineas.Count > 0)
            {
                e.Cancel = !UIPrincipal.VistaGestion.Confirmar(this, "¿Salir y descartar los cambios del pedido?");
            }
        }
        private bool TieneAcceso()
        {
            return rolPermitido > 0 && SesionActual.TieneRol(rolPermitido) && Permisos.PuedeAtender;
        }
    }
}
