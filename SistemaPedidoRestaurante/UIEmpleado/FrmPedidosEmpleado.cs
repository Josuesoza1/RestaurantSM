using System;
using System.Windows.Forms;
using SistemaPedidoRestaurante.UICompartida;
using System.Data;
using System.Drawing;
using System.Globalization;
using SistemaPedidoRestaurante.BLL;
using SistemaPedidoRestaurante.Entities;
using SistemaPedidoRestaurante.UIPrincipal;

namespace SistemaPedidoRestaurante.UIEmpleado
{
    [System.ComponentModel.DesignerCategory("Form")]
    public partial class FrmPedidosEmpleado : Form
    {
        public FrmPedidosEmpleado() : this(false, "") { }
        public FrmPedidosEmpleado(bool seleccionar, string filtro = "")
        {
            rolPermitido = 2;
            this.tipo = "Pedidos";
            this.seleccionar = seleccionar;
            this.historial = false;

            filtroInicial = filtro;

            InitializeComponent();
            PrepararVista();
        }

        private readonly PedidoBLL pedidos = new PedidoBLL();
        private readonly ReservaBLL reservas = new ReservaBLL();
        private readonly FacturaBLL facturas = new FacturaBLL();
        private readonly InventarioBLL inventario = new InventarioBLL();
        private readonly MesaBLL mesas = new MesaBLL();
        private readonly int rolPermitido;
        private readonly string tipo;
        private readonly string filtroInicial;
        private readonly bool seleccionar;
        private readonly bool historial;
        private DataTable registros;
        private bool actualizandoListado;

        public int PedidoSeleccionado { get; private set; }

        private bool TieneHistorialVentas => historial && rolPermitido == RolesSistema.Administrador && tipo == "Pedidos" && !seleccionar;
        private DateTime MesDelResumen => chkFiltrarMes.Checked ? mesVenta.Value : DateTime.Today;

        private void PrepararVista()
        {
            VistaGestion.PermitirOrden(listado);
            VistaGestion.PermitirOrden(detalle);
            Text = seleccionar ? "Seleccione un registro" : "Bandeja de " + tipo;
            titulo.Text = Text.ToUpperInvariant();
            txtBuscar.Text = filtroInicial;
            ConfigurarOpciones();
            ActualizarAcciones();
        }

        private void ConfigurarOpciones()
        {
            if (seleccionar)
            {
                Text = "Seleccione un registro";
                titulo.Text = Text.ToUpperInvariant();
            }
            bool pedidos = !seleccionar && tipo == "Pedidos" && !historial;
            if (!seleccionar && tipo == "Pedidos")
            {
                Text = historial ? "Historial de ventas" : "Ventas abiertas";
                titulo.Text = Text.ToUpperInvariant();
            }
            filtroMes.Visible = TieneHistorialVentas;
            btnResumenMensual.Visible = TieneHistorialVentas;
            ActualizarMesDelResumen();
            panelResumenDiario.Visible = TieneHistorialVentas;
            btnSeleccionar.Visible = seleccionar;
            btnNuevo.Visible = pedidos;
            btnMesa.Visible = pedidos;
            btnAvanzar.Visible = pedidos;
            btnAvanzar.Text = "Despachar";
            btnCobrar.Visible = pedidos;
            btnCancelar.Visible = pedidos;
            btnPedidoPdf.Visible = TieneHistorialVentas;
            btnFacturaPdf.Visible = TieneHistorialVentas || (!seleccionar && tipo == "Facturas");
            paneles.Panel2Collapsed = tipo != "Pedidos";
            if (System.ComponentModel.LicenseManager.UsageMode != System.ComponentModel.LicenseUsageMode.Designtime)
            {
                btnNuevo.Visible = btnMesa.Visible = btnAvanzar.Visible = pedidos && Permisos.PuedeAtender;
                btnCancelar.Visible = pedidos && Permisos.PuedeAtender;
                btnCobrar.Visible = pedidos && Permisos.PuedeCobrar;
                btnUltimoComprobante.Visible = pedidos && Permisos.PuedeCobrar;
                btnResumenMensual.Visible = panelResumenDiario.Visible = TieneHistorialVentas && Permisos.PuedeVerHistorial;
                btnPedidoPdf.Visible = TieneHistorialVentas && Permisos.PuedeVerHistorial;
                btnFacturaPdf.Visible = (TieneHistorialVentas || (!seleccionar && tipo == "Facturas")) && Permisos.PuedeVerHistorial;
            }
        }

        private void FrmBandeja_Load(object sender, EventArgs e)
        {
            if (DesignMode || System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime)
                return;
            bool permitido = tipo == "Pedidos" ? Permisos.PuedeConsultarPedidos :
                tipo == "Mesas" ? Permisos.PuedeAtender : Permisos.PuedeVerHistorial;
            if (!AccesoFormulario.PermitirOperacion(this, rolPermitido, permitido)) return;
            ConfigurarOpciones();
            try
            {
                Recargar();
                ActiveControl = txtBuscar;
            }
            catch (Exception error)
            {
                if (tipo == "Pedidos" && !seleccionar && !historial)
                    lblGuia.Text = "No se pudieron cargar las ventas. Pulse Recargar para volver a intentar.";
                MostrarError(error);
            }
        }

        private void Recargar(int idPedido = 0)
        {
            if (tipo == "Pedidos")
                registros = TieneHistorialVentas
                    ? pedidos.ListarHistorial()
                    : pedidos.ListarPedidos(seleccionar, soloAbiertas: !historial);
            else if (tipo == "Reservas")
                registros = reservas.ListarReservas();
            else if (tipo == "Facturas")
                registros = facturas.ListarFacturas();
            else if (tipo == "Mesas")
                registros = mesas.ListarMesas();
            else
                registros = inventario.ListarMovimientos();
            Filtrar();
            if (idPedido > 0)
            {
                foreach (DataGridViewRow fila in listado.Rows)
                {
                    var venta = fila.DataBoundItem as DataRowView;
                    if (venta != null && Convert.ToInt32(venta["IdPedido"]) == idPedido)
                    {
                        listado.CurrentCell = fila.Cells["IdPedido"];
                        fila.Selected = true;
                        break;
                    }
                }
            }
        }

        private void Filtrar()
        {
            if (registros == null)
                return;
            actualizandoListado = true;
            try
            {
                listado.DataSource = TieneHistorialVentas && chkFiltrarMes.Checked
                    ? pedidos.FiltrarHistorialPorMes(registros, txtBuscar.Text, mesVenta.Value)
                    : BusquedaLINQ.Filtrar(registros, txtBuscar.Text);
                FormatearColumnas(listado);
                listado.ClearSelection();
                detalle.DataSource = null;
            }
            finally
            {
                actualizandoListado = false;
                ActualizarAcciones();
            }
        }

        private DataRow Seleccion()
        {
            DataRow fila = VistaGestion.Fila(listado);
            if (fila == null)
                throw new ArgumentException("Seleccione una fila del listado.");
            return fila;
        }

        private void filtro_Changed(object sender, EventArgs e)
        {
            ActualizarMesDelResumen();
            try
            {
                Filtrar();
            }
            catch (Exception error)
            {
                MostrarError(error);
            }
        }

        private void chkFiltrarMes_CheckedChanged(object sender, EventArgs e)
        {
            mesVenta.Enabled = chkFiltrarMes.Checked;
            filtro_Changed(sender, e);
        }

        private void ActualizarMesDelResumen()
        {
            if (TieneHistorialVentas)
                btnResumenMensual.Text = "Resumen de caja " + MesDelResumen.ToString("MM/yyyy", CultureInfo.InvariantCulture);
        }

        private void listado_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ActualizarAcciones();
        }

        private void listado_Enter(object sender, EventArgs e)
        {
            if (listado.CurrentRow != null && listado.SelectedRows.Count == 0)
                listado.CurrentRow.Selected = true;
        }

        private void listado_SelectionChanged(object sender, EventArgs e)
        {
            if (actualizandoListado)
                return;
            ActualizarAcciones();
            if (tipo != "Pedidos" || DesignMode ||
                System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime)
                return;
            DataRow fila = VistaGestion.Fila(listado);
            if (fila == null)
            {
                detalle.DataSource = null;
                return;
            }
            int idPedido = Convert.ToInt32(fila["IdPedido"]);
            try
            {
                CargarDetalle(idPedido);
            }
            catch (Exception error)
            {
                detalle.DataSource = null;
                System.Diagnostics.Trace.TraceError(error.ToString());
                MessageBox.Show(this, "No se pudo cargar el detalle de la venta " + idPedido +
                    ". Pulse Recargar y vuelva a seleccionar la venta.", Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void CargarDetalle(int idPedido)
        {
            detalle.DataSource = null;
            detalle.DataSource = pedidos.Detalle(idPedido);
            FormatearColumnas(detalle);
        }

        private void ActualizarAcciones()
        {
            DataRow fila = VistaGestion.Fila(listado);
            string estado = fila != null && fila.Table.Columns.Contains("Estado") ? Convert.ToString(fila["Estado"]) : "";
            bool puedeAvanzar = estado == "Pendiente" || estado == "En preparación" || estado == "Listo";
            bool puedeDeshacer = PuedeDeshacerDespacho(fila);
            bool cobroMostrador = PuedeCobrarEnMostrador(fila);
            btnSeleccionar.Enabled = fila != null;
            btnMesa.Enabled = fila != null && puedeAvanzar && (!fila.Table.Columns.Contains("IdTipoEntrega") || Convert.ToInt32(fila["IdTipoEntrega"]) <= 2);
            btnAvanzar.Text = puedeDeshacer ? "Deshacer despacho" : "Despachar";
            btnAvanzar.Enabled = fila != null && Convert.ToInt32(fila["IdTipoEntrega"]) == 1 && (puedeAvanzar || puedeDeshacer);
            btnCobrar.Enabled = Permisos.PuedeCobrar && (estado == "Entregado" || cobroMostrador);
            btnCancelar.Enabled = estado == "Pendiente";
            btnPedidoPdf.Enabled = fila != null;
            btnFacturaPdf.Enabled = fila != null && (tipo == "Facturas" || estado == "Pagado" ||
                tipo == "Pedidos" && Convert.ToString(fila["Pago"]) == "Anulado");
            bool ventaActiva = !seleccionar && !historial && tipo == "Pedidos" && Permisos.PuedeAtender;
            btnEditarVenta.Visible = ventaActiva;
            btnEditarVenta.Enabled = ventaActiva && estado == "Pendiente";
            btnAgregarConsumo.Visible = ventaActiva;
            btnAgregarConsumo.Enabled = ventaActiva && fila != null && Convert.ToInt32(fila["IdTipoEntrega"]) == 1 &&
                (estado == "Pendiente" || estado == "Entregado");
            btnMesa.Visible = btnAvanzar.Visible = btnCancelar.Visible = ventaActiva;
            btnCobrar.Visible = ventaActiva && Permisos.PuedeCobrar;
            lblGuia.Visible = ventaActiva || TieneHistorialVentas;
            if (TieneHistorialVentas)
                lblGuia.Text = "El resumen incluye todos los cobros y devoluciones del mes. La búsqueda solo filtra el listado.";
            else if (ventaActiva)
            {
                if (registros == null)
                    lblGuia.Text = "Cargando ventas abiertas…";
                else if (listado.Rows.Count == 0)
                    lblGuia.Text = txtBuscar.TextLength == 0
                        ? "No hay ventas abiertas. Pulse Nueva venta para comenzar."
                        : "No hay ventas que coincidan. Borre la búsqueda para ver todas.";
                else if (fila == null)
                    lblGuia.Text = "Seleccione una venta del listado para ver su detalle y continuar.";
                else
                    lblGuia.Text = "Venta " + fila["IdPedido"] + " seleccionada. Puede usar las acciones habilitadas.";
            }
            btnPedidoPdf.Visible = TieneHistorialVentas && Permisos.PuedeVerHistorial && btnPedidoPdf.Enabled;
            btnFacturaPdf.Visible = !seleccionar && (TieneHistorialVentas || tipo == "Facturas") &&
                Permisos.PuedeVerHistorial && btnFacturaPdf.Enabled;
        }

        private static bool PuedeCobrarEnMostrador(DataRow fila)
        {
            return fila != null && fila.Table.Columns.Contains("Entrega") &&
                Convert.ToString(fila["Entrega"]) == "Mostrador" &&
                Convert.ToString(fila["Estado"]) == "Pendiente" &&
                fila.IsNull("IdMesa");
        }

        private bool PuedeDeshacerDespacho(DataRow fila)
        {
            if (tipo != "Pedidos" || seleccionar || historial || !Permisos.PuedeAtender || fila == null)
                return false;

            int tipoEntrega = Convert.ToInt32(fila["IdTipoEntrega"]);
            return Convert.ToString(fila["Estado"]) == "Entregado"
                && Convert.ToString(fila["Origen"]) == "En el local"
                && (tipoEntrega == 1 || tipoEntrega == 2)
                && Convert.ToString(fila["Pago"]) == "Sin pagar";
        }

        private void btnSeleccionar_Click(object sender, EventArgs e)
        {
            try
            {
                PedidoSeleccionado = Convert.ToInt32(Seleccion()["IdPedido"]);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception error)
            {
                MostrarError(error);
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            try
            {
                using (SistemaPedidoRestaurante.UIEmpleado.FrmPedido formulario = new SistemaPedidoRestaurante.UIEmpleado.FrmPedido())
                {
                    if (formulario.ShowDialog(this) == DialogResult.OK)
                    {
                        chkFiltrarMes.Checked = false;
                        txtBuscar.Clear();
                        Recargar(formulario.PedidoRegistrado);
                    }
                }
            }
            catch (Exception error)
            {
                MostrarError(error);
            }
        }

        private void btnEditarVenta_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow fila = Seleccion();
                int id = Convert.ToInt32(fila["IdPedido"]);
                using (SistemaPedidoRestaurante.UIEmpleado.FrmPedido formulario = new SistemaPedidoRestaurante.UIEmpleado.FrmPedido())
                {
                    formulario.PrepararEdicion(id, Convert.ToString(fila["Cliente"]));
                    if (formulario.ShowDialog(this) == DialogResult.OK)
                        Recargar(id);
                }
            }
            catch (Exception error)
            {
                MostrarError(error);
            }
        }

        private void btnAgregarConsumo_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow fila = Seleccion();
                int id = Convert.ToInt32(fila["IdPedido"]);
                using (var formulario = new FrmPedido())
                {
                    formulario.PrepararConsumo(id, Convert.ToString(fila["Cliente"]));
                    if (formulario.ShowDialog(this) == DialogResult.OK)
                        Recargar(id);
                }
            }
            catch (Exception error)
            {
                MostrarError(error);
            }
        }

        private void btnAvanzar_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow fila = Seleccion();
                string estado = Convert.ToString(fila["Estado"]);
                bool deshacer = PuedeDeshacerDespacho(fila);
                int idPedido = Convert.ToInt32(fila["IdPedido"]);
                if (deshacer)
                {
                    if (MessageBox.Show(this, "¿Deshacer el despacho de la venta " + idPedido + "? Volverá a quedar pendiente.",
                        Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
                        return;
                    pedidos.DeshacerDespacho(idPedido);
                }
                else
                {
                    if (!VistaGestion.Confirmar(this, "¿Confirmar el despacho de la venta " + idPedido + "?"))
                        return;
                    if (fila.IsNull("IdMesa"))
                    {
                        using (var elegir = new FrmElegirMesa(idPedido))
                            if (elegir.ShowDialog(this) != DialogResult.OK)
                                return;
                    }
                    pedidos.DespacharPedido(idPedido, estado);
                }
                txtBuscar.Clear();
                Recargar(idPedido);
            }
            catch (Exception error)
            {
                MostrarError(error);
            }
        }

        private void btnCobrar_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow fila = Seleccion();
                int pedido = Convert.ToInt32(fila["IdPedido"]);
                using (Form formulario = PantallasPersonal.CrearCobro(pedido, PuedeCobrarEnMostrador(fila)))
                    if (formulario.ShowDialog(this) == DialogResult.OK)
                    {
                        Recargar();
                        using (var siguiente = new FrmPedido())
                            siguiente.ShowDialog(this);
                        Recargar();
                    }
            }
            catch (Exception error)
            {
                MostrarError(error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (tipo != "Pedidos" || seleccionar || historial)
                return;
            try
            {
                DataRow fila = Seleccion();
                if (!VistaGestion.Confirmar(this, "¿Cancelar la venta " + fila["IdPedido"] +
                    "? Los productos volverán al inventario."))
                    return;
                pedidos.CancelarPedido(Convert.ToInt32(fila["IdPedido"]));
                Recargar();
            }
            catch (Exception error)
            {
                MostrarError(error);
            }
        }

        private void btnUltimoComprobante_Click(object sender, EventArgs e)
        {
            try
            {
                DocumentoVenta documento = facturas.ObtenerUltimoComprobantePropio();
                if (documento == null)
                {
                    MessageBox.Show(this, "Esta cuenta no tiene comprobantes vigentes de ventas cobradas.", Text);
                    return;
                }

                DocumentoPdf.MostrarVistaPrevia(this, documento);
            }
            catch (Exception error)
            {
                MostrarError(error);
            }
        }

        private void btnPedidoPdf_Click(object sender, EventArgs e)
        {
            Exportar(false);
        }

        private void btnFacturaPdf_Click(object sender, EventArgs e)
        {
            try
            {
                int pedido = Convert.ToInt32(Seleccion()["IdPedido"]);
                using (var formulario = new UIAdministracion.FrmFacturasAdministracion(pedido))
                    formulario.ShowDialog(this);
                Recargar(pedido);
            }
            catch (Exception error)
            {
                MostrarError(error);
            }
        }

        private void Exportar(bool factura)
        {
            try
            {
                int pedido = Convert.ToInt32(Seleccion()["IdPedido"]);
                DocumentoPdf.MostrarVistaPrevia(this, facturas.ObtenerDocumento(pedido, factura));
            }
            catch (Exception error)
            {
                MostrarError(error);
            }
        }

        private void btnResumenMensual_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime mes = MesDelResumen;
                MostrarResumen(new ResumenVentasDocumento(mes, facturas.ConsultarResumenMensual(mes), diario: false));
            }
            catch (Exception error)
            {
                MostrarError(error);
            }
        }

        private void btnResumenDiario_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime dia = diaCaja.Value.Date;
                MostrarResumen(new ResumenVentasDocumento(dia, facturas.ConsultarResumenDiario(dia), diario: true));
            }
            catch (Exception error)
            {
                MostrarError(error);
            }
        }

        private void MostrarResumen(ResumenVentasDocumento reporte)
        {
            using (var vista = new FrmVistaPrevia(reporte.CrearPdf(), reporte.NombreArchivo, reporte.GuardarExcel,
                reporte.TituloVista, reporte.DetalleVista(), ResumenVentasDocumento.Notas))
                vista.ShowDialog(this);
        }

        private void btnRecargar_Click(object sender, EventArgs e)
        {
            RecargarListado();
        }

        private void RecargarListado()
        {
            try
            {
                Recargar();
            }
            catch (Exception error)
            {
                MostrarError(error);
            }
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void MostrarError(Exception error)
        {
            MessageBox.Show(this, VistaGestion.MensajeParaUsuario(error,
                "No se pudo completar la operación. Pulse Recargar y revise el listado antes de volver a intentar."),
                Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void btnMesa_Click(object sender, EventArgs e)
        {
            try
            {
                int idPedido = Convert.ToInt32(Seleccion()["IdPedido"]);
                using (FrmElegirMesa formulario = new FrmElegirMesa(idPedido))
                    if (formulario.ShowDialog(this) == DialogResult.OK)
                        Recargar(idPedido);
            }
            catch (Exception error)
            {
                MostrarError(error);
            }
        }

        private void FormatearColumnas(DataGridView grid)
        {
            foreach (DataGridViewColumn columna in grid.Columns)
            {
                bool datosDeHistorial = columna.Name == "Origen" || columna.Name == "RegistradoPor" ||
                    columna.Name == "AtendidoPor" || columna.Name == "Pago";
                if (columna.Name == "IdMesa" || columna.Name == "IdCategoria" || columna.Name == "IdTipoEntrega" ||
                    columna.Name == "Envio" || columna.Name == "Direccion" || columna.Name == "Contacto" ||
                    tipo == "Pedidos" && !historial && datosDeHistorial)
                    columna.Visible = false;
                if (tipo == "Mesas" && columna.Name == "Nombre")
                    columna.Visible = false;
                if (columna.ValueType == typeof(decimal))
                    columna.DefaultCellStyle.Format = "N2";
                if (columna.ValueType == typeof(DateTime))
                    columna.DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
                if (columna.Name == "IdPedido")
                    columna.HeaderText = "Venta";
                if (TieneHistorialVentas && grid == listado && columna.Name == "Fecha")
                    columna.HeaderText = "Fecha de registro";
                if (columna.Name == "RegistradoPor")
                    columna.HeaderText = "Registrado por";
                if (columna.Name == "AtendidoPor")
                    columna.HeaderText = "Atendido por";
                if (columna.Name == "IdProducto")
                    columna.HeaderText = "ID de producto";
                if (columna.Name == "PrecioUnitario")
                    columna.HeaderText = "Precio unitario";
                columna.MinimumWidth = columna.ValueType == typeof(DateTime) ? 132 : 70;
            }
        }
    }
}
