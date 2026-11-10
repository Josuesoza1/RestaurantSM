using System;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using SistemaPedidoRestaurante.BLL;
using SistemaPedidoRestaurante.Entities;
using SistemaPedidoRestaurante.UIPrincipal;

namespace SistemaPedidoRestaurante.UIAdministracion
{
    public partial class FrmInventario : Form
    {
        private readonly InventarioBLL servicio = new InventarioBLL();
        private InventarioProductos inventario;
        private int revision;
        private int seleccionado;
        private ModoFormulario modoActual;
        private (string Stock, string Minimo, bool ControlActivo) datosIniciales;
        private readonly int productoInicial;

        public FrmInventario() : this(0)
        {
        }

        internal FrmInventario(int productoInicial)
        {
            this.productoInicial = productoInicial;
            InitializeComponent();
            ValidacionEntradaUI.Enteros(txtStockActual, textBox1);
            VistaGestion.Listado(dgvProductoInventario);
            string[] campos =

            {
                "Producto",
                "Categoria",
                "Unidad",
                "PrecioVenta",
                "StockActual",
                "StockMinimo"
            };
            for (int i = 0; i < campos.Length; i++)
            {
                dgvProductoInventario.Columns[i].DataPropertyName = campos[i];
            }

            dgvProductoInventario.Columns[3].DefaultCellStyle.Format = "N2";
            dgvProductoInventario.CellFormatting += dgvProductoInventario_CellFormatting;
            txtPrecioVenta.ReadOnly = txtUnidad.ReadOnly = true;
            cmbCategoriaInventario.Enabled = false;
            cmbProductoInventario.DisplayMember = "Producto";
            cmbProductoInventario.ValueMember = "IdProducto";
            CambiarModo(ModoFormulario.Neutral);
            TemaUI.Aplicar(this);
        }

        private void Recargar(int productoGuardado = 0)
        {
            inventario = servicio.Listar(incluirSinControl: true);
            cmbProductoInventario.DataSource = inventario;
            int cantidadBajos = inventario.Count(item => item.StockBajo);
            bajos.Text = "Stock bajo (" + cantidadBajos + ")";
            Text = cantidadBajos > 0 ? "Inventario — " + cantidadBajos + " con stock bajo" : "Inventario";
            Filtrar();
            if (productoGuardado == 0)
                return;

            foreach (DataGridViewRow fila in dgvProductoInventario.Rows)
            {
                if (fila.DataBoundItem is InventarioProducto producto && producto.IdProducto == productoGuardado)
                {
                    dgvProductoInventario.CurrentCell = fila.Cells[0];
                    fila.Selected = true;
                    break;
                }
            }
        }

        private void dgvProductoInventario_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            var item = dgvProductoInventario.Rows[e.RowIndex].DataBoundItem as InventarioProducto;
            if (item == null)
                return;

            string campo = dgvProductoInventario.Columns[e.ColumnIndex].DataPropertyName;
            if (!item.TieneControl && (campo == nameof(InventarioProducto.StockActual) ||
                                       campo == nameof(InventarioProducto.StockMinimo)))
            {
                e.Value = "—";
                e.FormattingApplied = true;
            }
            if (item.StockBajo)
            {
                e.CellStyle.BackColor = Color.MistyRose;
                e.CellStyle.ForeColor = Color.DarkRed;
            }
        }

        private void Filtrar()
        {
            try
            {
                VistaGestion.VincularGestion(dgvProductoInventario, servicio.Buscar(inventario, textBox2.Text, bajos.Checked));
                if (modoActual == ModoFormulario.Neutral)
                    MostrarSeleccion();
            }
            catch (Exception error)
            {
                MessageBox.Show(this, error.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void MostrarProducto(InventarioProducto producto)
        {
            if (producto == null)
                cmbProductoInventario.SelectedIndex = -1;
            else
                cmbProductoInventario.SelectedValue = producto.IdProducto;

            seleccionado = producto?.IdProducto ?? 0;
            revision = producto?.Revision ?? 0;
            bool tieneInventario = producto != null && producto.TieneControl;
            txtStockActual.Text = tieneInventario ? producto.StockActual.ToString() : "";
            textBox1.Text = tieneInventario ? producto.StockMinimo.ToString() : "";
            chkControlActivo.Checked = tieneInventario && producto.Activo;
            txtUnidad.Text = producto?.Unidad ?? "";
            txtPrecioVenta.Text = producto == null ? "" : producto.PrecioVenta.ToString("N2");
            cmbCategoriaInventario.Items.Clear();
            if (producto != null)
            {
                cmbCategoriaInventario.Items.Add(producto.Categoria);
                cmbCategoriaInventario.SelectedIndex = 0;
            }
        }

        private void MostrarSeleccion()
        {
            var producto = VistaGestion.RegistroSeleccionado<InventarioProducto>(dgvProductoInventario);
            MostrarProducto(producto);
            bool tieneInventario = producto != null && producto.TieneControl;
            registro.Enabled = eliminarToolStripMenuItem.Enabled = tieneInventario;
            desactivarToolStripMenuItem.Enabled = tieneInventario && producto.Activo;

            if (producto == null)
                lblOperacion.Text = "Seleccione un producto para registrar existencias, corregir un conteo o anotar un movimiento.";
            else if (!tieneInventario)
                lblOperacion.Text = "Sin inventario. Use «Registrar o editar stock» para ingresar las cantidades iniciales.";
            else if (!producto.Activo)
                lblOperacion.Text = "Inventario inactivo. Puede reactivarlo al editar sus existencias.";
            else
                lblOperacion.Text = "Para sumar o retirar unidades, abra Movimientos → Entrada o salida.";
        }

        private void Guardar(bool activo)
        {
            int stock = SistemaPedidoRestaurante.Validaciones.CamposValidacion.Entero(txtStockActual.Text, "stock actual", 0);
            int minimo = SistemaPedidoRestaurante.Validaciones.CamposValidacion.Entero(textBox1.Text, "stock mínimo", 0);
            servicio.Guardar(new InventarioProducto
            {
                IdProducto = seleccionado,
                StockActual = stock,
                StockMinimo = minimo,
                Activo = activo,
                Revision = revision
            });
        }

        private void btnGuardarProducto_Click(object s, EventArgs e)
        {
            if (modoActual == ModoFormulario.Neutral) return;
            try
            {
                int productoGuardado = seleccionado;
                if (modoActual == ModoFormulario.Eliminar)
                {
                    if (!VistaGestion.Confirmar(this, "¿Eliminar este registro de inventario? Requiere stock cero y no haber sido usado en pedidos.")) return;
                    servicio.Eliminar(seleccionado, revision);
                }
                else Guardar(modoActual == ModoFormulario.Nuevo || chkControlActivo.Checked);
                CambiarModo(ModoFormulario.Neutral);
                Recargar(productoGuardado);
            }
            catch (Exception error) { MessageBox.Show(this, error.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information); }
        }

        private void btnLimpiar_Click(object s, EventArgs e)
        {
            if (!VistaGestion.ConfirmarDescarte(this, HayCambios()))
                return;

            txtStockActual.Clear(); textBox1.Clear();
        }

        private void btnCancelar_Click(object s, EventArgs e)
        {
            if (!VistaGestion.ConfirmarDescarte(this, HayCambios()))
                return;

            CambiarModo(ModoFormulario.Neutral);
        }

        private void btnBuscar_Click(object s, EventArgs e)
        {
            Filtrar();
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IniciarSeleccion(ModoFormulario.Eliminar);
        }

        private void desactivarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!SeleccionarConControl())
                    return;
                if (VistaGestion.Confirmar(this, "¿Retirar el control de existencias conservando el registro? Requiere stock cero y ningún pedido pendiente."))
                {
                    Guardar(false);
                    Recargar(seleccionado);
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(this, error.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void editarToolStripMenuItem_Click(object s, EventArgs e)
        {
            EditarExistenciasSeleccionadas();
        }

        private void EditarExistenciasSeleccionadas()
        {
            var producto = VistaGestion.RegistroSeleccionado<InventarioProducto>(dgvProductoInventario);
            if (producto == null)
            {
                MostrarSeleccion();
                return;
            }

            MostrarProducto(producto);
            CambiarModo(producto.TieneControl ? ModoFormulario.Editar : ModoFormulario.Nuevo);
            txtStockActual.Focus();
        }

        private void volverToolStripMenuItem_Click(object s, EventArgs e)
        {
            Close();
        }

        private void bajos_CheckedChanged(object s, EventArgs e)
        {
            Filtrar();
        }

        private void actualizar_Click(object s, EventArgs e)
        {
            try
            {
                Recargar();
            }
            catch (Exception error)
            {
                MessageBox.Show(this, error.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void ayudaToolStripMenuItem_Click(object s, EventArgs e)
        {
            MessageBox.Show(this,
                "Registrar o editar stock sirve para ingresar el conteo inicial, corregirlo o cambiar el mínimo." +
                Environment.NewLine + Environment.NewLine +
                "Movimientos → Entrada o salida sirve para sumar unidades recibidas o retirar unidades por pérdida, devolución u otro motivo." +
                Environment.NewLine + Environment.NewLine +
                "La columna En caja indica si el producto se puede vender.", "Inventario");
        }

        private void dgvProductoInventario_SelectionChanged(object s, EventArgs e)
        {
            if (modoActual == ModoFormulario.Neutral)
            {
                MostrarSeleccion();
            }
        }

        private void FrmInventario_CargarDatos(object sender, EventArgs e)
        {
            if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime || this.Site?.DesignMode == true)
            {
                return;
            }

            if (!SesionActual.TieneRol(1))
            {
                Close();
                return;
            }

            ActiveControl = textBox2;

            try
            {
                Recargar(productoInicial);
                if (productoInicial > 0)
                    EditarExistenciasSeleccionadas();
            }
            catch (Exception error)
            {
                MessageBox.Show(this, error.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void entradaSalida_Click(object sender, EventArgs e)
        {
            var producto = VistaGestion.RegistroSeleccionado<InventarioProducto>(dgvProductoInventario);
            try
            {
                using (var formulario = new FrmAjusteInventario(producto?.IdProducto ?? 0))
                    if (formulario.ShowDialog(this) == DialogResult.OK)
                        Recargar(formulario.ProductoAjustado);
            }
            catch (Exception error)
            {
                MessageBox.Show(this, VistaGestion.MensajeParaUsuario(error,
                    "No se pudo actualizar el listado. Pulse Recargar para revisar las existencias."),
                    Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void historial_Click(object sender, EventArgs e)
        {
            using (FrmMovimientosAdministracion formulario = new FrmMovimientosAdministracion()) formulario.ShowDialog(this);
        }

        private void IniciarSeleccion(ModoFormulario modo)
        {
            if (SeleccionarConControl())
                CambiarModo(modo);
        }

        private bool SeleccionarConControl()
        {
            var producto = VistaGestion.RegistroSeleccionado<InventarioProducto>(dgvProductoInventario);
            if (producto == null || !producto.TieneControl)
            {
                MostrarSeleccion();
                return false;
            }
            MostrarProducto(producto);
            return true;
        }
        private (string Stock, string Minimo, bool ControlActivo) DatosIngresados()
        {
            return (txtStockActual.Text, textBox1.Text, chkControlActivo.Checked);
        }

        private bool HayCambios()
        {
            return (modoActual == ModoFormulario.Nuevo || modoActual == ModoFormulario.Editar) && !datosIniciales.Equals(DatosIngresados());
        }

        private void FrmInventario_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!VistaGestion.ConfirmarDescarte(this, HayCambios()))
                e.Cancel = true;
        }

        private void CambiarModo(ModoFormulario modo)
        {
            modoActual = modo;
            bool edicion = modo == ModoFormulario.Nuevo || modo == ModoFormulario.Editar;
            bool operacion = modo != ModoFormulario.Neutral;
            string inventario = modo == ModoFormulario.Editar || modo == ModoFormulario.Eliminar
                ? "inventario de «" + cmbProductoInventario.Text + "»" : "inventario";
            VistaGestion.Modo(lblOperacion, pnlOperacion, btnGuardarProducto, modo, inventario);
            if (modo == ModoFormulario.Nuevo)
            {
                lblOperacion.Text = "Registrar existencias de «" + cmbProductoInventario.Text + "». El inventario se activa al guardar.";
                textBox1.Text = "0";
            }
            if (modo == ModoFormulario.Editar)
                btnGuardarProducto.Text = "Guardar";
            grpAgregarProducto.Enabled = edicion;
            chkControlActivo.Enabled = modo == ModoFormulario.Editar;
            btnGuardarProducto.Enabled = btnCancelar.Enabled = operacion;
            btnLimpiar.Enabled = edicion;
            if (edicion)
                datosIniciales = DatosIngresados();
            editarToolStripMenuItem.Enabled = movimientos.Enabled = actualizar.Enabled = !operacion;
            registro.Enabled = !operacion;
            if (!operacion)
                MostrarSeleccion();
        }
    }
}
