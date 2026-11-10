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
    public class FrmPedidoLocalBase : Form
    {
        protected virtual NumericUpDown NudCantidad => null;
        protected virtual Label EstadoPerfil => null;
        protected virtual Label LblEstadoCatalogo => null;
        protected virtual Button BtnRecargarCatalogo => null;
        protected virtual DataGridView DgvDetalle => null;
        protected virtual Button BtnQuitar => null;
        protected virtual Button BtnVaciar => null;
        protected virtual Button BtnEnviar => null;
        protected virtual Button BtnCobrarAhora => null;
        protected virtual NumericUpDown NudCantidadDetalle => null;
        protected virtual Button BtnCambiarCantidad => null;
        protected virtual Button BtnGuardarCambios => null;
        protected virtual Label LblTotal => null;
        protected virtual TextBox TxtBuscarProducto => null;
        protected virtual Label LblTitulo => null;
        protected virtual Label LblNombreOpcional => null;
        protected virtual TextBox TxtCliente => null;
        protected virtual Button BtnAceptar => null;
        protected virtual ComboBox CmbPrecio => null;
        protected virtual ComboBox CmbProducto => null;
        protected virtual ComboBox CmbCategoria => null;

        private const int CantidadVentaGrande = 50;
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

        }

        protected void PrepararVista()
        {
            VistaGestion.PermitirOrden(DgvDetalle);
            CmbProducto.DropDownStyle = CmbCategoria.DropDownStyle = ComboBoxStyle.DropDownList;
            CmbPrecio.DropDownStyle = ComboBoxStyle.DropDownList;
            CmbPrecio.Enabled = false;
            BtnAceptar.Enabled = false;
            ActualizarDetalle();
            UIPrincipal.TemaUI.Aplicar(this);
        }

        internal void PrepararEdicion(int pedido, string cliente)
        {
            pedidoEnEdicion = pedido;
            Text = "Editar venta " + pedido;
            LblTitulo.Text = "EDITAR VENTA " + pedido;
            TxtCliente.Text = cliente;
            LblNombreOpcional.Visible = false;
            BtnEnviar.Visible = BtnCobrarAhora.Visible = BtnVaciar.Visible = false;
            BtnGuardarCambios.Visible = true;
        }

        protected void Procesar_CargarPantalla(object sender, EventArgs e)
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
                EstadoPerfil.Text = VistaGestion.MensajeParaUsuario(error,
                    "No se pudo abrir la venta. Vuelva a Ventas abiertas y pulse Recargar.");
                return;
            }

            perfilValido = true;
            BtnRecargarCatalogo.Enabled = true;
            ActualizarDetalle();
            RecargarCatalogo(inicial: true);
            ActiveControl = pedidoEnEdicion > 0 ? (Control)DgvDetalle : CmbCategoria;
        }

        protected void Procesar_btnRecargarCatalogo_Click(object sender, EventArgs e)
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
            catch (Exception error)
            {
                LblEstadoCatalogo.ForeColor = Color.DarkRed;
                LblEstadoCatalogo.Text = VistaGestion.MensajeParaUsuario(error,
                    "No se pudo cargar el catálogo. Pulse Recargar catálogo para volver a intentar.");
                LblEstadoCatalogo.Visible = true;
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
            int categoria = CmbCategoria.SelectedValue is int ? (int)CmbCategoria.SelectedValue : 0;
            int producto = CmbProducto.SelectedValue is int ? (int)CmbProducto.SelectedValue : 0;
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
            CmbCategoria.DisplayMember = "Nombre";
            CmbCategoria.ValueMember = "IdCategoria";
            CmbCategoria.DataSource = categorias;
            CmbCategoria.SelectedValue = categorias.Any(c => c.IdCategoria == categoria) ? categoria : 0;
            AplicarFiltro(producto);
        }

        private void AplicarFiltro(int productoPreferido = 0)
        {
            int id = CmbCategoria.SelectedValue is int ? (int)CmbCategoria.SelectedValue : 0;
            string busqueda = TxtBuscarProducto.Text.Trim();
            var productos = catalogo.Where(p =>
                (id == 0 || p.IdCategoria == id) &&
                (busqueda.Length == 0 || BusquedaLINQ.Contiene(p.Nombre, busqueda) ||
                    BusquedaLINQ.Contiene(p.Codigo, busqueda)))
                .OrderBy(p => p.Nombre).ToList();
            CmbProducto.DisplayMember = "Nombre";
            CmbProducto.ValueMember = "IdProducto";
            CmbProducto.DataSource = productos;
            if (productos.Any(p => p.IdProducto == productoPreferido))
                CmbProducto.SelectedValue = productoPreferido;
            MostrarPrecio();
        }

        private void MostrarPrecio()
        {
            var producto = CmbProducto.SelectedItem as Producto;
            CmbPrecio.Items.Clear();
            if (producto != null)
            {
                var original = BuscarLineaOriginal(producto.IdProducto);
                CmbPrecio.Items.Add((original?.PrecioUnitario ?? producto.Precio).ToString("N2"));
                CmbPrecio.SelectedIndex = 0;
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
            var producto = CmbProducto.SelectedItem as Producto;
            if (!perfilValido || producto == null)
            {
                BtnAceptar.Enabled = false;
                if (perfilValido)
                {
                    EstadoPerfil.ForeColor = SystemColors.ControlText;
                    EstadoPerfil.Text = TxtBuscarProducto.Text.Trim().Length == 0
                        ? "No hay productos con existencias en esta categoría."
                        : "No hay coincidencias. Revise el nombre, el código o la categoría.";
                }
                return;
            }

            long disponible = CantidadDisponible(producto);
            long restante = disponible - (int)NudCantidad.Value;
            BtnAceptar.Enabled = restante >= 0;
            EstadoPerfil.ForeColor = restante <= producto.StockMinimo ? Color.DarkRed : SystemColors.ControlText;
            if (restante < 0)
                EstadoPerfil.Text = "Existencias insuficientes. Disponible para agregar: " + Math.Max(0, disponible) + ".";
            else if (restante <= producto.StockMinimo)
                EstadoPerfil.Text = "Stock bajo. Con esta cantidad quedarían " + restante + " unidades. Mínimo: " + producto.StockMinimo + ".";
            else
                EstadoPerfil.Text = "Disponible para agregar: " + disponible + " unidades.";
        }

        protected void Procesar_nudCantidad_ValueChanged(object sender, EventArgs e)
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

                var producto = CmbProducto.SelectedItem as Producto;
                int cantidad = (int)NudCantidad.Value;
                if (producto != null && cantidad > CantidadDisponible(producto))
                    throw new ArgumentException("Disponible para agregar: " + Math.Max(0, CantidadDisponible(producto)) + " unidades.");
                decimal? precioOriginal = producto == null ? null : BuscarLineaOriginal(producto.IdProducto)?.PrecioUnitario;
                borrador.Agregar(producto, cantidad, precioOriginal);
                detalleModificado = true;
                ActualizarDetalle();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(this, ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void QuitarDelPedido()
        {
            var linea = VistaGestion.RegistroSeleccionado<LineaPedidoBorrador>(DgvDetalle);
            if (linea == null)
                return;

            borrador.Quitar(linea.IdProducto);
            detalleModificado = true;
            ActualizarDetalle();
        }

        private void ActualizarDetalle(string aviso = "", bool actualizarListado = true)
        {
            if (actualizarListado)
            {
                VistaGestion.Vincular(DgvDetalle, borrador.Lineas);
                ActualizarAccionesDetalle();
            }

            LblTotal.Text = "Total estimado: " + borrador.Total.ToString("N2");
            BtnVaciar.Enabled = borrador.Lineas.Count > 0;
            string problema = BuscarProblemaDetalle();
            LblEstadoCatalogo.ForeColor = problema == "" ? SystemColors.ControlText : Color.DarkRed;
            LblEstadoCatalogo.Text = aviso == "" || problema == "" ? aviso + problema : aviso + Environment.NewLine + problema;
            LblEstadoCatalogo.Visible = LblEstadoCatalogo.Text.Length > 0;
            bool puedeGuardar = perfilValido && borrador.Lineas.Count > 0 && problema == "";
            BtnEnviar.Enabled = BtnCobrarAhora.Enabled = puedeGuardar;
            BtnGuardarCambios.Enabled = puedeGuardar && detalleModificado;
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
            BtnEnviar.Enabled = BtnCobrarAhora.Enabled = false;
            try
            {
                PedidoRegistrado = pedidos.GuardarPedido(0, borrador, solicitud);
            }
            catch (Exception error)
            {
                MessageBox.Show(this, VistaGestion.MensajeParaUsuario(error,
                    "No se pudo confirmar que la venta se guardó. Revise Ventas abiertas antes de volver a intentar."),
                    Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ActualizarDetalle();
                return;
            }

            if (cobrarAhora)
                AbrirCobro();
            else
                MessageBox.Show(this, "Venta " + PedidoRegistrado +
                    " guardada. Puede cobrarla desde Ventas abiertas.", "Venta guardada");

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
                MessageBox.Show(this, VistaGestion.MensajeParaUsuario(error,
                    "No se pudo abrir el cobro. Busque la venta en Ventas abiertas."),
                    "Cobro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            MessageBox.Show(this, "La venta " + PedidoRegistrado +
                " quedó guardada. Si sigue pendiente de pago, selecciónela en Ventas abiertas y pulse Cobrar.",
                "Venta guardada");
        }

        protected void Procesar_dgvDetalle_SelectionChanged(object sender, EventArgs e)
        {
            ActualizarAccionesDetalle();
        }

        private void ActualizarAccionesDetalle()
        {
            var linea = VistaGestion.RegistroSeleccionado<LineaPedidoBorrador>(DgvDetalle);
            bool hayLinea = perfilValido && linea != null;
            BtnQuitar.Enabled = NudCantidadDetalle.Enabled = BtnCambiarCantidad.Enabled = hayLinea;
            NudCantidadDetalle.Value = hayLinea
                ? Math.Min(linea.Cantidad, NudCantidadDetalle.Maximum)
                : NudCantidadDetalle.Minimum;
        }

        protected void Procesar_btnCambiarCantidad_Click(object sender, EventArgs e)
        {
            var linea = VistaGestion.RegistroSeleccionado<LineaPedidoBorrador>(DgvDetalle);
            int cantidad = (int)NudCantidadDetalle.Value;
            if (!perfilValido || linea == null || cantidad == linea.Cantidad)
                return;

            try
            {
                var producto = catalogo.FirstOrDefault(p => p.IdProducto == linea.IdProducto);
                long maximo = MaximoDisponible(linea.IdProducto, producto?.StockActual ?? 0);
                if (cantidad > linea.Cantidad && cantidad > maximo)
                    throw new ArgumentException("Puede dejar hasta " + Math.Max(linea.Cantidad, maximo) +
                        " unidades en esta línea con el stock actual.");

                var lineasMostradas = (IList<LineaPedidoBorrador>)DgvDetalle.DataSource;
                int indice = lineasMostradas.IndexOf(linea);
                lineasMostradas[indice] = borrador.CambiarCantidad(linea, cantidad);
                var columnaOrdenada = DgvDetalle.SortedColumn;
                if (columnaOrdenada != null &&
                    (columnaOrdenada.DataPropertyName == nameof(LineaPedidoBorrador.Cantidad) ||
                     columnaOrdenada.DataPropertyName == nameof(LineaPedidoBorrador.Subtotal)))
                {
                    VistaGestion.Ordenar(DgvDetalle, columnaOrdenada, DgvDetalle.SortOrder == SortOrder.Descending);
                }
                detalleModificado = true;
                ActualizarDetalle(actualizarListado: false);
            }
            catch (ArgumentException error)
            {
                ActualizarAccionesDetalle();
                MessageBox.Show(this, VistaGestion.MensajeParaUsuario(error, "No se pudo cambiar la cantidad."),
                    Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        protected void Procesar_btnGuardarCambios_Click(object sender, EventArgs e)
        {
            string avisoCantidad = AvisoVentaGrande();
            if (avisoCantidad.Length > 0 && !VistaGestion.Confirmar(this, avisoCantidad + "¿Guardar los cambios?"))
                return;

            try
            {
                pedidos.EditarPendiente(pedidoEnEdicion, detalleOriginal, borrador);
                PedidoRegistrado = pedidoEnEdicion;
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception error)
            {
                MessageBox.Show(this, VistaGestion.MensajeParaUsuario(error,
                    "No se pudo confirmar que se guardaron los cambios. Revise la venta antes de volver a intentar."),
                    Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        protected void Procesar_btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        protected void Procesar_btnAceptar_Click(object sender, EventArgs e)
        {
            AgregarAlPedido();
        }

        protected void Procesar_btnEnviar_Click(object sender, EventArgs e)
        {
            string avisoCantidad = AvisoVentaGrande();
            if (avisoCantidad.Length == 0 || VistaGestion.Confirmar(this, avisoCantidad + "¿Guardar la venta?"))
                RegistrarVenta(false);
        }

        protected void Procesar_btnCobrarAhora_Click(object sender, EventArgs e)
        {
            if (UIPrincipal.VistaGestion.Confirmar(this,
                AvisoVentaGrande() + "¿Guardar la venta y continuar al cobro? Al guardar se reservan los productos del inventario." +
                Environment.NewLine + Environment.NewLine +
                "Si vuelve del cobro sin confirmar el pago, la venta quedará pendiente en Ventas abiertas."))
                RegistrarVenta(true);
        }

        private string AvisoVentaGrande()
        {
            int unidades = borrador.Lineas.Sum(linea => linea.Cantidad);
            return unidades >= CantidadVentaGrande
                ? "La venta incluye " + unidades + " unidades. Revise las cantidades antes de confirmar." +
                    Environment.NewLine + Environment.NewLine
                : "";
        }

        protected void Procesar_btnQuitar_Click(object sender, EventArgs e)
        {
            QuitarDelPedido();
        }

        protected void Procesar_btnVaciar_Click(object sender, EventArgs e)
        {
            if (borrador.Lineas.Count == 0 || !UIPrincipal.VistaGestion.Confirmar(this,
                "¿Quitar todos los productos de esta venta? Tendrá que agregarlos de nuevo."))
                return;
            borrador.Vaciar();
            ActualizarDetalle();
        }

        protected void Procesar_cmbCategoria_SelectedValueChanged(object sender, EventArgs e)
        {
            AplicarFiltro();
        }

        protected void Procesar_txtBuscarProducto_TextChanged(object sender, EventArgs e)
        {
            int producto = CmbProducto.SelectedValue is int ? (int)CmbProducto.SelectedValue : 0;
            AplicarFiltro(producto);
        }

        protected void Procesar_cmbProducto_SelectedValueChanged(object sender, EventArgs e)
        {
            MostrarPrecio();
        }
        protected void Procesar_FrmPedidoLocalBase_FormClosing(object sender, FormClosingEventArgs e)
        {
            bool hayCambios = pedidoEnEdicion > 0 ? detalleModificado : borrador.Lineas.Count > 0;
            if (DialogResult != DialogResult.OK && hayCambios)
            {
                e.Cancel = !VistaGestion.Confirmar(this, "¿Volver y descartar los cambios de esta venta?");
            }
        }
        private bool TieneAcceso()
        {
            return rolPermitido > 0 && SesionActual.TieneRol(rolPermitido) && Permisos.PuedeAtender;
        }
    }
}
