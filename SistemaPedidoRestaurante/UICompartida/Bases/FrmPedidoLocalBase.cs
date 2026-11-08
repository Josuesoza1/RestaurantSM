using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using SistemaPedidoRestaurante.BLL;
using SistemaPedidoRestaurante.Entities;
using SistemaPedidoRestaurante.UIPrincipal;

namespace SistemaPedidoRestaurante.UICompartida
{
    public partial class FrmPedidoLocalBase : Form
    {
        private readonly PedidoBorrador borrador = new PedidoBorrador();
        private readonly PedidoBLL pedidos = new PedidoBLL();
        private List<LineaPedidoBorrador> detalleOriginal = new List<LineaPedidoBorrador>();
        private Productos catalogo = new Productos();
        private bool perfilValido;
        private int pedidoEnEdicion;
        private bool detalleModificado;
        private readonly int rolPermitido;
        private readonly Guid solicitud = Guid.NewGuid();

        public int PedidoRegistrado { get; private set; }

        public FrmPedidoLocalBase() : this(0)
        {
        }

        protected FrmPedidoLocalBase(int rol)
        {
            rolPermitido = rol;
            InitializeComponent();
            VistaGestion.PermitirOrden(dgvDetalle);
            Text = "Nueva venta local";
            cmbProducto.DropDownStyle = cmbCategoria.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbPrecio.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbPrecio.Enabled = false;
            btnAceptar.Text = "Agregar al pedido";
            btnAceptar.Enabled = false;
            btnCancelar.Text = "Volver";
            ActualizarDetalle();
            UIPrincipal.TemaUI.Aplicar(this);
        }

        internal void PrepararEdicion(int pedido, string cliente)
        {
            pedidoEnEdicion = pedido;
            Text = "Editar venta " + pedido;
            lblTitulo.Text = "EDITAR VENTA " + pedido;
            txtCliente.Text = cliente;
            lblNombreOpcional.Visible = false;
            btnEnviar.Visible = btnCobrarAhora.Visible = btnVaciar.Visible = false;
            btnGuardarCambios.Visible = btnCambiarCantidad.Visible = nudCantidadDetalle.Visible = true;
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
                if (pedidoEnEdicion > 0)
                {
                    detalleOriginal = pedidos.ObtenerDetallePendiente(pedidoEnEdicion);
                    borrador.CargarDetalle(detalleOriginal);
                }
            }
            catch (Exception error)
            {
                estadoPerfil.Text = error.Message;
                return;
            }

            perfilValido = true;
            btnRecargarCatalogo.Enabled = true;
            ActualizarDetalle();
            RecargarCatalogo(inicial: true);
        }

        private void btnRecargarCatalogo_Click(object sender, EventArgs e)
        {
            RecargarCatalogo(inicial: false);
        }

        private void RecargarCatalogo(bool inicial)
        {
            Productos productos;
            try
            {
                productos = new ProductoBLL().ObtenerDisponiblesParaPedido();
            }
            catch (Exception)
            {
                lblEstadoCatalogo.ForeColor = Color.DarkRed;
                lblEstadoCatalogo.Text = "No se pudo recargar el catálogo. Intente de nuevo.";
                return;
            }

            bool preciosActualizados = borrador.ActualizarPrecios(productos, detalleOriginal);
            CargarCatalogo(productos);
            string aviso = inicial ? "" : "Catálogo actualizado.";
            if (preciosActualizados)
                aviso = "Se actualizaron los precios del catálogo y el total estimado.";
            ActualizarDetalle(aviso, actualizarListado: preciosActualizados);
        }

        private void CargarCatalogo(Productos productos)
        {
            int categoria = cmbCategoria.SelectedValue is int ? (int)cmbCategoria.SelectedValue : 0;
            int producto = cmbProducto.SelectedValue is int ? (int)cmbProducto.SelectedValue : 0;
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
            cmbCategoria.SelectedValue = categorias.Any(c => c.IdCategoria == categoria) ? categoria : 0;
            AplicarFiltro(producto);
        }

        private void AplicarFiltro(int productoPreferido = 0)
        {
            int id = cmbCategoria.SelectedValue is int ? (int)cmbCategoria.SelectedValue : 0;
            var productos = catalogo.Where(p => id == 0 || p.IdCategoria == id).OrderBy(p => p.Nombre).ToList();
            cmbProducto.DisplayMember = "Nombre";
            cmbProducto.ValueMember = "IdProducto";
            cmbProducto.DataSource = productos;
            if (productos.Any(p => p.IdProducto == productoPreferido))
                cmbProducto.SelectedValue = productoPreferido;
            MostrarPrecio();
        }

        private void MostrarPrecio()
        {
            var producto = cmbProducto.SelectedItem as Producto;
            cmbPrecio.Items.Clear();
            if (producto != null)
            {
                var original = BuscarLineaOriginal(producto.IdProducto);
                cmbPrecio.Items.Add((original?.PrecioUnitario ?? producto.Precio).ToString("N2"));
                cmbPrecio.SelectedIndex = 0;
            }

            ActualizarAvisoStock();
        }

        private long CantidadDisponible(Producto producto)
        {
            var linea = borrador.Lineas.FirstOrDefault(item => item.IdProducto == producto.IdProducto);
            return MaximoDisponible(producto.IdProducto, producto.StockActual) - (linea?.Cantidad ?? 0);
        }

        private LineaPedidoBorrador BuscarLineaOriginal(int producto)
        {
            return detalleOriginal.FirstOrDefault(linea => linea.IdProducto == producto);
        }

        private long MaximoDisponible(int producto, int stockActual)
        {
            var original = BuscarLineaOriginal(producto);
            return (long)stockActual + (original?.Cantidad ?? 0);
        }

        private void ActualizarAvisoStock()
        {
            var producto = cmbProducto.SelectedItem as Producto;
            if (!perfilValido || producto == null)
            {
                btnAceptar.Enabled = false;
                if (perfilValido)
                {
                    estadoPerfil.ForeColor = SystemColors.ControlText;
                    estadoPerfil.Text = "No hay productos con existencias en esta categoría.";
                }
                return;
            }

            long disponible = CantidadDisponible(producto);
            long restante = disponible - (int)nudCantidad.Value;
            btnAceptar.Enabled = restante >= 0;
            estadoPerfil.ForeColor = restante <= producto.StockMinimo ? Color.DarkRed : SystemColors.ControlText;
            if (restante < 0)
                estadoPerfil.Text = "Existencias insuficientes. Disponible para agregar: " + Math.Max(0, disponible) + ".";
            else if (restante <= producto.StockMinimo)
                estadoPerfil.Text = "Stock bajo. Con esta cantidad quedarían " + restante + " unidades. Mínimo: " + producto.StockMinimo + ".";
            else
                estadoPerfil.Text = "Disponible para agregar: " + disponible + " unidades.";
        }

        private void nudCantidad_ValueChanged(object sender, EventArgs e)
        {
            ActualizarAvisoStock();
        }

        private void AgregarAlPedido()
        {
            try
            {
                if (!TieneAcceso() || !perfilValido)
                {
                    throw new ArgumentException("Inicie sesión con una cuenta del personal.");
                }

                var producto = cmbProducto.SelectedItem as Producto;
                int cantidad = (int)nudCantidad.Value;
                if (producto != null && cantidad > CantidadDisponible(producto))
                    throw new ArgumentException("La cantidad supera las existencias disponibles.");
                decimal? precioOriginal = producto == null ? null : BuscarLineaOriginal(producto.IdProducto)?.PrecioUnitario;
                borrador.Agregar(producto, cantidad, precioOriginal);
                detalleModificado = true;
                ActualizarDetalle();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Mi pedido", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void QuitarDelPedido()
        {
            if (dgvDetalle.SelectedRows.Count == 0)
                return;

            var linea = (LineaPedidoBorrador)dgvDetalle.SelectedRows[0].DataBoundItem;
            borrador.Quitar(linea.IdProducto);
            detalleModificado = true;
            ActualizarDetalle();
        }

        private void ActualizarDetalle(string aviso = "", bool actualizarListado = true)
        {
            if (actualizarListado)
            {
                VistaGestion.Vincular(dgvDetalle, borrador.Lineas);
                ActualizarBotonQuitar();
            }

            lblTotal.Text = "Total estimado: " + borrador.Total.ToString("N2");
            btnVaciar.Enabled = borrador.Lineas.Count > 0;
            string problema = BuscarProblemaDetalle();
            lblEstadoCatalogo.ForeColor = problema == "" ? SystemColors.ControlText : Color.DarkRed;
            lblEstadoCatalogo.Text = aviso == "" || problema == "" ? aviso + problema : aviso + Environment.NewLine + problema;
            bool puedeGuardar = perfilValido && borrador.Lineas.Count > 0 && problema == "";
            btnEnviar.Enabled = btnCobrarAhora.Enabled = puedeGuardar;
            btnGuardarCambios.Enabled = puedeGuardar && detalleModificado;
            ActualizarAvisoStock();
        }

        private string BuscarProblemaDetalle()
        {
            foreach (LineaPedidoBorrador linea in borrador.Lineas)
            {
                var original = BuscarLineaOriginal(linea.IdProducto);
                int cantidadNueva = linea.Cantidad - (original?.Cantidad ?? 0);
                if (cantidadNueva <= 0)
                    continue;

                var producto = catalogo.FirstOrDefault(item => item.IdProducto == linea.IdProducto);
                if (producto == null)
                    return linea.Producto + " ya no está disponible para agregar. Revise la línea.";
                if (producto.StockActual < cantidadNueva)
                    return linea.Producto + " tiene existencias insuficientes. Disponible: " + producto.StockActual + ".";
                if (original == null && linea.PrecioUnitario != producto.Precio)
                    return "Cambió el precio de " + linea.Producto + ". Quite la línea o recargue el catálogo.";
            }

            return "";
        }

        private void RegistrarVenta(bool cobrarAhora)
        {
            btnEnviar.Enabled = btnCobrarAhora.Enabled = false;
            try
            {
                PedidoRegistrado = pedidos.GuardarPedido(0, borrador, solicitud);
            }
            catch (Exception error)
            {
                MessageBox.Show(this, error.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                ActualizarDetalle();
                return;
            }

            if (cobrarAhora)
                AbrirCobro();
            else
                MessageBox.Show(this, "Venta " + PedidoRegistrado + " guardada como pendiente.", "Venta local");

            DialogResult = DialogResult.OK;
            Close();
        }

        private void AbrirCobro()
        {
            try
            {
                using (Form formulario = PantallasPersonal.CrearCobro(PedidoRegistrado, true))
                {
                    if (formulario.ShowDialog(this) == DialogResult.OK)
                        return;
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(this, error.Message, "Cobro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            MessageBox.Show(this, "La venta " + PedidoRegistrado +
                " quedó registrada. Si el cobro sigue pendiente, puedes retomarlo en Ventas abiertas.", "Venta local");
        }

        private void dgvDetalle_SelectionChanged(object sender, EventArgs e)
        {
            ActualizarBotonQuitar();
        }

        private void ActualizarBotonQuitar()
        {
            btnQuitar.Enabled = perfilValido && dgvDetalle.SelectedRows.Count > 0;
            if (pedidoEnEdicion == 0)
                return;

            nudCantidadDetalle.Enabled = btnCambiarCantidad.Enabled = btnQuitar.Enabled;
            if (nudCantidadDetalle.Enabled)
            {
                var linea = (LineaPedidoBorrador)dgvDetalle.SelectedRows[0].DataBoundItem;
                nudCantidadDetalle.Value = Math.Min(linea.Cantidad, nudCantidadDetalle.Maximum);
            }
        }

        private void btnCambiarCantidad_Click(object sender, EventArgs e)
        {
            var linea = (LineaPedidoBorrador)dgvDetalle.SelectedRows[0].DataBoundItem;
            int cantidad = (int)nudCantidadDetalle.Value;
            if (cantidad == linea.Cantidad)
                return;

            try
            {
                var producto = catalogo.FirstOrDefault(p => p.IdProducto == linea.IdProducto);
                if (cantidad > linea.Cantidad && cantidad > MaximoDisponible(linea.IdProducto, producto?.StockActual ?? 0))
                    throw new ArgumentException("La cantidad supera las existencias disponibles.");

                borrador.CambiarCantidad(linea, cantidad);
                detalleModificado = true;
                ActualizarDetalle();
            }
            catch (ArgumentException error)
            {
                MessageBox.Show(this, error.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnGuardarCambios_Click(object sender, EventArgs e)
        {
            try
            {
                pedidos.EditarPendiente(pedidoEnEdicion, detalleOriginal, borrador);
                PedidoRegistrado = pedidoEnEdicion;
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception error)
            {
                MessageBox.Show(this, error.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            AgregarAlPedido();
        }

        private void btnEnviar_Click(object sender, EventArgs e)
        {
            RegistrarVenta(false);
        }

        private void btnCobrarAhora_Click(object sender, EventArgs e)
        {
            RegistrarVenta(true);
        }

        private void btnQuitar_Click(object sender, EventArgs e)
        {
            QuitarDelPedido();
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
            bool hayCambios = pedidoEnEdicion > 0 ? detalleModificado : borrador.Lineas.Count > 0;
            if (DialogResult != DialogResult.OK && hayCambios)
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
