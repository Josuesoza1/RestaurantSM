using System;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using SistemaPedidoRestaurante.BLL;
using SistemaPedidoRestaurante.Entities;
using SistemaPedidoRestaurante.UIPrincipal;

namespace SistemaPedidoRestaurante.UICompartida
{
    public class FrmBandejaOperaciones : Form
    {
        protected virtual Label Titulo => null;
        protected virtual TextBox TxtBuscar => null;
        protected virtual Panel FiltroMes => null;
        protected virtual CheckBox ChkFiltrarMes => null;
        protected virtual DateTimePicker MesVenta => null;
        protected virtual FlowLayoutPanel Acciones => null;
        protected virtual SplitContainer Paneles => null;
        protected virtual DataGridView Listado => null;
        protected virtual DataGridView Detalle => null;
        protected virtual Button BtnSeleccionar => null;
        protected virtual Button BtnNuevo => null;
        protected virtual Button BtnUltimoComprobante => null;
        protected virtual Button BtnMesa => null;
        protected virtual Button BtnAvanzar => null;
        protected virtual Button BtnCobrar => null;
        protected virtual Button BtnEditarVenta => null;
        protected virtual Button BtnCancelar => null;
        protected virtual Button BtnPedidoPdf => null;
        protected virtual Button BtnFacturaPdf => null;
        protected virtual Button BtnResumenMensual => null;
        protected virtual Panel PanelResumenDiario => null;
        protected virtual DateTimePicker DiaCaja => null;
        protected virtual Label LblGuia => null;

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
        private DateTime MesDelResumen => ChkFiltrarMes.Checked ? MesVenta.Value : DateTime.Today;

        public FrmBandejaOperaciones() : this(0, "Pedidos")
        {
        }

        protected FrmBandejaOperaciones(int rol, string tipo, bool seleccionar = false, string filtro = "", bool historial = false)
        {
            rolPermitido = rol;
            this.tipo = tipo;
            this.seleccionar = seleccionar;
            this.historial = historial;

            filtroInicial = filtro;
        }

        protected void PrepararVista()
        {
            VistaGestion.PermitirOrden(Listado);
            VistaGestion.PermitirOrden(Detalle);
            Text = seleccionar ? "Seleccione un registro" : "Bandeja de " + tipo;
            Titulo.Text = Text.ToUpperInvariant();
            TxtBuscar.Text = filtroInicial;
            ConfigurarOpciones();
            ActualizarAcciones();
        }

        protected void ConfigurarOpciones()
        {
            if (seleccionar)
            {
                Text = "Seleccione un registro";
                Titulo.Text = Text.ToUpperInvariant();
            }
            bool pedidos = !seleccionar && tipo == "Pedidos" && !historial;
            if (!seleccionar && tipo == "Pedidos")
            {
                Text = historial ? "Historial de ventas" : "Ventas abiertas";
                Titulo.Text = Text.ToUpperInvariant();
            }
            FiltroMes.Visible = TieneHistorialVentas;
            BtnResumenMensual.Visible = TieneHistorialVentas;
            ActualizarMesDelResumen();
            PanelResumenDiario.Visible = TieneHistorialVentas;
            BtnSeleccionar.Visible = seleccionar;
            BtnNuevo.Visible = pedidos;
            BtnMesa.Visible = pedidos;
            BtnAvanzar.Visible = pedidos;
            BtnAvanzar.Text = "Despachar";
            BtnCobrar.Visible = pedidos;
            BtnCancelar.Visible = pedidos;
            BtnPedidoPdf.Visible = TieneHistorialVentas;
            BtnFacturaPdf.Visible = TieneHistorialVentas || (!seleccionar && tipo == "Facturas");
            Paneles.Panel2Collapsed = tipo != "Pedidos";
            if (System.ComponentModel.LicenseManager.UsageMode != System.ComponentModel.LicenseUsageMode.Designtime)
            {
                BtnNuevo.Visible = BtnMesa.Visible = BtnAvanzar.Visible = pedidos && Permisos.PuedeAtender;
                BtnCancelar.Visible = pedidos && Permisos.PuedeAtender;
                BtnCobrar.Visible = pedidos && Permisos.PuedeCobrar;
                BtnUltimoComprobante.Visible = pedidos && Permisos.PuedeCobrar;
                BtnResumenMensual.Visible = PanelResumenDiario.Visible = TieneHistorialVentas && Permisos.PuedeVerHistorial;
                BtnPedidoPdf.Visible = TieneHistorialVentas && Permisos.PuedeVerHistorial;
                BtnFacturaPdf.Visible = (TieneHistorialVentas || (!seleccionar && tipo == "Facturas")) && Permisos.PuedeVerHistorial;
            }
        }

        protected void Procesar_FrmBandeja_Load(object sender, EventArgs e)
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
                ActiveControl = TxtBuscar;
            }
            catch (Exception error)
            {
                if (tipo == "Pedidos" && !seleccionar && !historial)
                    LblGuia.Text = "No se pudieron cargar las ventas. Pulse Recargar para volver a intentar.";
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
                foreach (DataGridViewRow fila in Listado.Rows)
                {
                    var venta = fila.DataBoundItem as DataRowView;
                    if (venta != null && Convert.ToInt32(venta["IdPedido"]) == idPedido)
                    {
                        Listado.CurrentCell = fila.Cells["IdPedido"];
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
                Listado.DataSource = TieneHistorialVentas && ChkFiltrarMes.Checked
                    ? pedidos.FiltrarHistorialPorMes(registros, TxtBuscar.Text, MesVenta.Value)
                    : BusquedaLINQ.Filtrar(registros, TxtBuscar.Text);
                FormatearColumnas(Listado);
                Listado.ClearSelection();
                Detalle.DataSource = null;
            }
            finally
            {
                actualizandoListado = false;
                ActualizarAcciones();
            }
        }

        private DataRow Seleccion()
        {
            DataRow fila = VistaGestion.Fila(Listado);
            if (fila == null)
                throw new ArgumentException("Seleccione una fila del Listado.");
            return fila;
        }

        protected void Procesar_filtro_Changed(object sender, EventArgs e)
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

        protected void Procesar_chkFiltrarMes_CheckedChanged(object sender, EventArgs e)
        {
            MesVenta.Enabled = ChkFiltrarMes.Checked;
            Procesar_filtro_Changed(sender, e);
        }

        private void ActualizarMesDelResumen()
        {
            if (TieneHistorialVentas)
                BtnResumenMensual.Text = "Resumen de caja " + MesDelResumen.ToString("MM/yyyy", CultureInfo.InvariantCulture);
        }

        protected void Procesar_listado_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ActualizarAcciones();
        }

        protected void Procesar_listado_Enter(object sender, EventArgs e)
        {
            if (Listado.CurrentRow != null && Listado.SelectedRows.Count == 0)
                Listado.CurrentRow.Selected = true;
        }

        protected void Procesar_listado_SelectionChanged(object sender, EventArgs e)
        {
            if (actualizandoListado)
                return;
            ActualizarAcciones();
            if (tipo != "Pedidos" || DesignMode ||
                System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime)
                return;
            DataRow fila = VistaGestion.Fila(Listado);
            if (fila == null)
            {
                Detalle.DataSource = null;
                return;
            }
            int idPedido = Convert.ToInt32(fila["IdPedido"]);
            try
            {
                CargarDetalle(idPedido);
            }
            catch (Exception error)
            {
                Detalle.DataSource = null;
                System.Diagnostics.Trace.TraceError(error.ToString());
                MessageBox.Show(this, "No se pudo cargar el Detalle de la venta " + idPedido +
                    ". Pulse Recargar y vuelva a seleccionar la venta.", Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void CargarDetalle(int idPedido)
        {
            Detalle.DataSource = null;
            Detalle.DataSource = pedidos.Detalle(idPedido);
            FormatearColumnas(Detalle);
        }

        private void ActualizarAcciones()
        {
            DataRow fila = VistaGestion.Fila(Listado);
            string estado = fila != null && fila.Table.Columns.Contains("Estado") ? Convert.ToString(fila["Estado"]) : "";
            bool puedeAvanzar = estado == "Pendiente" || estado == "En preparación" || estado == "Listo";
            bool puedeDeshacer = PuedeDeshacerDespacho(fila);
            bool cobroMostrador = PuedeCobrarEnMostrador(fila);
            BtnSeleccionar.Enabled = fila != null;
            BtnMesa.Enabled = fila != null && puedeAvanzar && (!fila.Table.Columns.Contains("IdTipoEntrega") || Convert.ToInt32(fila["IdTipoEntrega"]) <= 2);
            BtnAvanzar.Text = puedeDeshacer ? "Deshacer despacho" : "Despachar";
            BtnAvanzar.Enabled = puedeAvanzar || puedeDeshacer;
            BtnCobrar.Enabled = Permisos.PuedeCobrar && (estado == "Entregado" || cobroMostrador);
            BtnCancelar.Enabled = estado == "Pendiente";
            BtnPedidoPdf.Enabled = fila != null;
            BtnFacturaPdf.Enabled = fila != null && (tipo == "Facturas" || estado == "Pagado" ||
                tipo == "Pedidos" && Convert.ToString(fila["Pago"]) == "Anulado");
            bool ventaActiva = !seleccionar && !historial && tipo == "Pedidos" && Permisos.PuedeAtender;
            BtnEditarVenta.Visible = ventaActiva;
            BtnEditarVenta.Enabled = ventaActiva && estado == "Pendiente";
            BtnMesa.Visible = BtnAvanzar.Visible = BtnCancelar.Visible = ventaActiva;
            BtnCobrar.Visible = ventaActiva && Permisos.PuedeCobrar;
            LblGuia.Visible = ventaActiva || TieneHistorialVentas;
            if (TieneHistorialVentas)
                LblGuia.Text = "El resumen incluye todos los cobros y devoluciones del mes. La búsqueda solo filtra el Listado.";
            else if (ventaActiva)
            {
                if (registros == null)
                    LblGuia.Text = "Cargando ventas abiertas…";
                else if (Listado.Rows.Count == 0)
                    LblGuia.Text = TxtBuscar.TextLength == 0
                        ? "No hay ventas abiertas. Pulse Nueva venta para comenzar."
                        : "No hay ventas que coincidan. Borre la búsqueda para ver todas.";
                else if (fila == null)
                    LblGuia.Text = "Seleccione una venta del Listado para ver su Detalle y continuar.";
                else
                    LblGuia.Text = "Venta " + fila["IdPedido"] + " seleccionada. Puede usar las Acciones habilitadas.";
            }
            BtnPedidoPdf.Visible = TieneHistorialVentas && Permisos.PuedeVerHistorial && BtnPedidoPdf.Enabled;
            BtnFacturaPdf.Visible = !seleccionar && (TieneHistorialVentas || tipo == "Facturas") &&
                Permisos.PuedeVerHistorial && BtnFacturaPdf.Enabled;
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

        protected void Procesar_btnSeleccionar_Click(object sender, EventArgs e)
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

        protected void Procesar_btnNuevo_Click(object sender, EventArgs e)
        {
            try
            {
                using (FrmPedidoLocalBase formulario = PantallasPersonal.CrearPedido())
                {
                    if (formulario.ShowDialog(this) == DialogResult.OK)
                    {
                        ChkFiltrarMes.Checked = false;
                        TxtBuscar.Clear();
                        Recargar(formulario.PedidoRegistrado);
                    }
                }
            }
            catch (Exception error)
            {
                MostrarError(error);
            }
        }

        protected void Procesar_btnEditarVenta_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow fila = Seleccion();
                int id = Convert.ToInt32(fila["IdPedido"]);
                using (FrmPedidoLocalBase formulario = PantallasPersonal.CrearPedido())
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

        protected void Procesar_btnAvanzar_Click(object sender, EventArgs e)
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
                    pedidos.DespacharPedido(idPedido, estado);
                }
                TxtBuscar.Clear();
                Recargar(idPedido);
            }
            catch (Exception error)
            {
                MostrarError(error);
            }
        }

        protected void Procesar_btnCobrar_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow fila = Seleccion();
                int pedido = Convert.ToInt32(fila["IdPedido"]);
                using (Form formulario = PantallasPersonal.CrearCobro(pedido, PuedeCobrarEnMostrador(fila)))
                    if (formulario.ShowDialog(this) == DialogResult.OK)
                        Recargar();
            }
            catch (Exception error)
            {
                MostrarError(error);
            }
        }

        protected void Procesar_btnCancelar_Click(object sender, EventArgs e)
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

        protected void Procesar_btnUltimoComprobante_Click(object sender, EventArgs e)
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

        protected void Procesar_btnPedidoPdf_Click(object sender, EventArgs e)
        {
            Exportar(false);
        }

        protected void Procesar_btnFacturaPdf_Click(object sender, EventArgs e)
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

        protected void Procesar_btnResumenMensual_Click(object sender, EventArgs e)
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

        protected void Procesar_btnResumenDiario_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime dia = DiaCaja.Value.Date;
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

        protected void Procesar_btnRecargar_Click(object sender, EventArgs e)
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

        protected void Procesar_btnVolver_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void MostrarError(Exception error)
        {
            MessageBox.Show(this, VistaGestion.MensajeParaUsuario(error,
                "No se pudo completar la operación. Pulse Recargar y revise el Listado antes de volver a intentar."),
                Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        protected void Procesar_btnMesa_Click(object sender, EventArgs e)
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
                if (TieneHistorialVentas && grid == Listado && columna.Name == "Fecha")
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
