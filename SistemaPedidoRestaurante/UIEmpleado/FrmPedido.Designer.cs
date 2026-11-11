namespace SistemaPedidoRestaurante.UIEmpleado
{
    partial class FrmPedido
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPedido));
            this.pnlEncabezado = new System.Windows.Forms.Panel();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.grpDatosCliente = new System.Windows.Forms.GroupBox();
            this.txtCliente = new System.Windows.Forms.TextBox();
            this.lblNombreOpcional = new System.Windows.Forms.Label();
            this.lblResumenImportes = new System.Windows.Forms.Label();
            this.lblCiente = new System.Windows.Forms.Label();
            this.grpProducto = new System.Windows.Forms.GroupBox();
            this.lblBuscarProducto = new System.Windows.Forms.Label();
            this.txtBuscarProducto = new System.Windows.Forms.TextBox();
            this.cmbCategoria = new System.Windows.Forms.ComboBox();
            this.cmbPrecio = new System.Windows.Forms.ComboBox();
            this.cmbProducto = new System.Windows.Forms.ComboBox();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.lblCategoria = new System.Windows.Forms.Label();
            this.lblCantidadPlato = new System.Windows.Forms.Label();
            this.lblPrecioPlato = new System.Windows.Forms.Label();
            this.lblProducto = new System.Windows.Forms.Label();
            this.nudCantidad = new System.Windows.Forms.NumericUpDown();
            this.dgvDetalle = new System.Windows.Forms.DataGridView();
            this.estadoPerfil = new System.Windows.Forms.Label();
            this.lblEstadoCatalogo = new System.Windows.Forms.Label();
            this.btnRecargarCatalogo = new System.Windows.Forms.Button();
            this.btnQuitar = new System.Windows.Forms.Button();
            this.btnVaciar = new System.Windows.Forms.Button();
            this.btnEnviar = new System.Windows.Forms.Button();
            this.btnCobrarAhora = new System.Windows.Forms.Button();
            this.nudCantidadDetalle = new System.Windows.Forms.NumericUpDown();
            this.btnCambiarCantidad = new System.Windows.Forms.Button();
            this.btnGuardarCambios = new System.Windows.Forms.Button();
            this.lblTotal = new System.Windows.Forms.Label();
            this.accionesDetalle = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlEncabezado.SuspendLayout();
            this.grpDatosCliente.SuspendLayout();
            this.grpProducto.SuspendLayout();
            this.accionesDetalle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCantidad)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCantidadDetalle)).BeginInit();
            this.SuspendLayout();
            this.pnlEncabezado.BackColor = System.Drawing.Color.Red;
            this.pnlEncabezado.Controls.Add(this.lblTitulo);
            this.pnlEncabezado.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlEncabezado.Location = new System.Drawing.Point(0, 0);
            this.pnlEncabezado.Name = "pnlEncabezado";
            this.pnlEncabezado.Size = new System.Drawing.Size(784, 70);
            this.pnlEncabezado.TabIndex = 1;
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Location = new System.Drawing.Point(48, 20);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(179, 29);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "NUEVA VENTA";
            this.grpDatosCliente.Controls.Add(this.lblResumenImportes);
            this.grpDatosCliente.Controls.Add(this.txtCliente);
            this.grpDatosCliente.Controls.Add(this.lblNombreOpcional);
            this.grpDatosCliente.Controls.Add(this.lblCiente);
            this.grpDatosCliente.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpDatosCliente.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.grpDatosCliente.Location = new System.Drawing.Point(12, 109);
            this.grpDatosCliente.Name = "grpDatosCliente";
            this.grpDatosCliente.Size = new System.Drawing.Size(310, 175);
            this.grpDatosCliente.TabIndex = 2;
            this.grpDatosCliente.TabStop = false;
            this.grpDatosCliente.Text = "Cliente de la venta";
            this.txtCliente.Location = new System.Drawing.Point(76, 19);
            this.txtCliente.Name = "txtCliente";
            this.txtCliente.ReadOnly = true;
            this.txtCliente.Size = new System.Drawing.Size(201, 20);
            this.txtCliente.TabIndex = 7;
            this.txtCliente.TabStop = false;
            this.txtCliente.Text = "Consumidor final";
            this.lblNombreOpcional.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblNombreOpcional.Location = new System.Drawing.Point(21, 52);
            this.lblNombreOpcional.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblNombreOpcional.Name = "lblNombreOpcional";
            this.lblNombreOpcional.Size = new System.Drawing.Size(255, 39);
            this.lblNombreOpcional.TabIndex = 8;
            this.lblNombreOpcional.Text = "Al cobrar, puede indicar un nombre para el comprobante.";
            this.lblResumenImportes.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblResumenImportes.Location = new System.Drawing.Point(21, 98);
            this.lblResumenImportes.Name = "lblResumenImportes";
            this.lblResumenImportes.Size = new System.Drawing.Size(275, 70);
            this.lblResumenImportes.Text = "Subtotal: C$ 0.00";
            this.lblCiente.AutoSize = true;
            this.lblCiente.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblCiente.Location = new System.Drawing.Point(21, 22);
            this.lblCiente.Name = "lblCiente";
            this.lblCiente.Size = new System.Drawing.Size(49, 14);
            this.lblCiente.TabIndex = 0;
            this.lblCiente.Text = "Cliente:";
            this.grpProducto.Controls.Add(this.cmbCategoria);
            this.grpProducto.Controls.Add(this.lblBuscarProducto);
            this.grpProducto.Controls.Add(this.txtBuscarProducto);
            this.grpProducto.Controls.Add(this.cmbPrecio);
            this.grpProducto.Controls.Add(this.cmbProducto);
            this.grpProducto.Controls.Add(this.btnAceptar);
            this.grpProducto.Controls.Add(this.lblCategoria);
            this.grpProducto.Controls.Add(this.lblCantidadPlato);
            this.grpProducto.Controls.Add(this.lblPrecioPlato);
            this.grpProducto.Controls.Add(this.lblProducto);
            this.grpProducto.Controls.Add(this.nudCantidad);
            this.grpProducto.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpProducto.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.grpProducto.Location = new System.Drawing.Point(342, 109);
            this.grpProducto.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.grpProducto.Name = "grpProducto";
            this.grpProducto.Size = new System.Drawing.Size(435, 175);
            this.grpProducto.TabIndex = 0;
            this.grpProducto.TabStop = false;
            this.grpProducto.Text = "Producto";
            this.cmbCategoria.AccessibleName = "Categoría";
            this.cmbCategoria.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCategoria.FormattingEnabled = true;
            this.cmbCategoria.Location = new System.Drawing.Point(95, 20);
            this.cmbCategoria.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbCategoria.Name = "cmbCategoria";
            this.cmbCategoria.Size = new System.Drawing.Size(320, 22);
            this.cmbCategoria.TabIndex = 0;
            this.cmbCategoria.SelectedValueChanged += new System.EventHandler(this.cmbCategoria_SelectedValueChanged);
            this.cmbPrecio.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPrecio.Enabled = false;
            this.cmbPrecio.FormattingEnabled = true;
            this.cmbPrecio.Location = new System.Drawing.Point(255, 109);
            this.cmbPrecio.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbPrecio.Name = "cmbPrecio";
            this.cmbPrecio.Size = new System.Drawing.Size(160, 22);
            this.cmbPrecio.TabIndex = 1;
            this.cmbPrecio.TabStop = false;
            this.cmbProducto.AccessibleName = "Producto";
            this.cmbProducto.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProducto.FormattingEnabled = true;
            this.cmbProducto.Location = new System.Drawing.Point(95, 80);
            this.cmbProducto.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbProducto.Name = "cmbProducto";
            this.cmbProducto.Size = new System.Drawing.Size(320, 22);
            this.cmbProducto.TabIndex = 2;
            this.cmbProducto.SelectedValueChanged += new System.EventHandler(this.cmbProducto_SelectedValueChanged);
            this.lblBuscarProducto.AutoSize = true;
            this.lblBuscarProducto.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblBuscarProducto.Location = new System.Drawing.Point(12, 54);
            this.lblBuscarProducto.Name = "lblBuscarProducto";
            this.lblBuscarProducto.Text = "Nombre/código:";
            this.txtBuscarProducto.AccessibleName = "Buscar producto por nombre o código";
            this.txtBuscarProducto.Location = new System.Drawing.Point(120, 49);
            this.txtBuscarProducto.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBuscarProducto.Name = "txtBuscarProducto";
            this.txtBuscarProducto.Size = new System.Drawing.Size(295, 20);
            this.txtBuscarProducto.TabIndex = 1;
            this.txtBuscarProducto.TextChanged += new System.EventHandler(this.txtBuscarProducto_TextChanged);
            this.btnCancelar.BackColor = System.Drawing.Color.Red;
            this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Font = new System.Drawing.Font("Arial Black", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.ForeColor = System.Drawing.Color.Black;
            this.btnCancelar.Location = new System.Drawing.Point(12, 560);
            this.btnCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(120, 34);
            this.btnCancelar.TabIndex = 12;
            this.btnCancelar.Text = "Volver";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            this.btnAceptar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnAceptar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAceptar.Font = new System.Drawing.Font("Arial Black", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAceptar.ForeColor = System.Drawing.Color.Black;
            this.btnAceptar.Location = new System.Drawing.Point(195, 137);
            this.btnAceptar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(220, 31);
            this.btnAceptar.TabIndex = 4;
            this.btnAceptar.Text = "Agregar a la venta";
            this.btnAceptar.UseVisualStyleBackColor = false;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            this.lblCategoria.AutoSize = true;
            this.lblCategoria.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblCategoria.Location = new System.Drawing.Point(12, 25);
            this.lblCategoria.Name = "lblCategoria";
            this.lblCategoria.Size = new System.Drawing.Size(63, 14);
            this.lblCategoria.TabIndex = 15;
            this.lblCategoria.Text = "Categoría:";
            this.lblCantidadPlato.AutoSize = true;
            this.lblCantidadPlato.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblCantidadPlato.Location = new System.Drawing.Point(12, 114);
            this.lblCantidadPlato.Name = "lblCantidadPlato";
            this.lblCantidadPlato.Size = new System.Drawing.Size(58, 14);
            this.lblCantidadPlato.TabIndex = 14;
            this.lblCantidadPlato.Text = "Cantidad:";
            this.lblPrecioPlato.AutoSize = true;
            this.lblPrecioPlato.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblPrecioPlato.Location = new System.Drawing.Point(195, 114);
            this.lblPrecioPlato.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPrecioPlato.Name = "lblPrecioPlato";
            this.lblPrecioPlato.Size = new System.Drawing.Size(45, 14);
            this.lblPrecioPlato.TabIndex = 5;
            this.lblPrecioPlato.Text = "Precio:";
            this.lblProducto.AutoSize = true;
            this.lblProducto.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblProducto.Location = new System.Drawing.Point(12, 85);
            this.lblProducto.Name = "lblProducto";
            this.lblProducto.Size = new System.Drawing.Size(60, 14);
            this.lblProducto.TabIndex = 0;
            this.lblProducto.Text = "Producto:";
            this.nudCantidad.AccessibleName = "Cantidad para agregar";
            this.nudCantidad.Location = new System.Drawing.Point(95, 109);
            this.nudCantidad.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.nudCantidad.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudCantidad.Name = "nudCantidad";
            this.nudCantidad.Size = new System.Drawing.Size(80, 20);
            this.nudCantidad.TabIndex = 3;
            this.nudCantidad.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudCantidad.ValueChanged += new System.EventHandler(this.nudCantidad_ValueChanged);
            this.dgvDetalle.AccessibleName = "Productos agregados a la venta";
            this.dgvDetalle.AllowUserToAddRows = false;
            this.dgvDetalle.AllowUserToDeleteRows = false;
            this.dgvDetalle.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvDetalle.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDetalle.ColumnHeadersHeight = 29;
            this.dgvDetalle.Location = new System.Drawing.Point(12, 330);
            this.dgvDetalle.MultiSelect = false;
            this.dgvDetalle.Name = "dgvDetalle";
            this.dgvDetalle.ReadOnly = true;
            this.dgvDetalle.RowHeadersWidth = 51;
            this.dgvDetalle.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDetalle.Size = new System.Drawing.Size(760, 170);
            this.dgvDetalle.StandardTab = true;
            this.dgvDetalle.TabIndex = 2;
            this.dgvDetalle.SelectionChanged += new System.EventHandler(this.dgvDetalle_SelectionChanged);
            this.estadoPerfil.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.estadoPerfil.BackColor = System.Drawing.SystemColors.Window;
            this.estadoPerfil.Location = new System.Drawing.Point(12, 297);
            this.estadoPerfil.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.estadoPerfil.Name = "estadoPerfil";
            this.estadoPerfil.Size = new System.Drawing.Size(760, 26);
            this.estadoPerfil.TabIndex = 0;
            this.estadoPerfil.Text = "Elija un producto y una cantidad para agregarlos a la venta.";
            this.lblEstadoCatalogo.AutoEllipsis = true;
            this.lblEstadoCatalogo.BackColor = System.Drawing.SystemColors.Window;
            this.lblEstadoCatalogo.Location = new System.Drawing.Point(12, 75);
            this.lblEstadoCatalogo.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblEstadoCatalogo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.lblEstadoCatalogo.Name = "lblEstadoCatalogo";
            this.lblEstadoCatalogo.Size = new System.Drawing.Size(604, 28);
            this.lblEstadoCatalogo.TabIndex = 1;
            this.lblEstadoCatalogo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRecargarCatalogo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnRecargarCatalogo.Enabled = false;
            this.btnRecargarCatalogo.Location = new System.Drawing.Point(627, 75);
            this.btnRecargarCatalogo.Margin = new System.Windows.Forms.Padding(2);
            this.btnRecargarCatalogo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRecargarCatalogo.Name = "btnRecargarCatalogo";
            this.btnRecargarCatalogo.Size = new System.Drawing.Size(150, 28);
            this.btnRecargarCatalogo.TabIndex = 1;
            this.btnRecargarCatalogo.Text = "Recargar catálogo";
            this.btnRecargarCatalogo.UseVisualStyleBackColor = false;
            this.btnRecargarCatalogo.Click += new System.EventHandler(this.btnRecargarCatalogo_Click);
            this.btnQuitar.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnQuitar.BackColor = System.Drawing.Color.LightPink;
            this.btnQuitar.Location = new System.Drawing.Point(208, 4);
            this.btnQuitar.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnQuitar.Name = "btnQuitar";
            this.btnQuitar.Size = new System.Drawing.Size(148, 34);
            this.btnQuitar.TabIndex = 5;
            this.btnQuitar.Text = "Quitar seleccionado";
            this.btnQuitar.UseVisualStyleBackColor = false;
            this.btnQuitar.Click += new System.EventHandler(this.btnQuitar_Click);
            this.btnVaciar.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnVaciar.BackColor = System.Drawing.Color.PaleTurquoise;
            this.btnVaciar.Location = new System.Drawing.Point(364, 4);
            this.btnVaciar.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnVaciar.Name = "btnVaciar";
            this.btnVaciar.Size = new System.Drawing.Size(120, 34);
            this.btnVaciar.TabIndex = 6;
            this.btnVaciar.Text = "Vaciar venta";
            this.btnVaciar.UseVisualStyleBackColor = false;
            this.btnVaciar.Click += new System.EventHandler(this.btnVaciar_Click);
            this.btnEnviar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEnviar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnEnviar.Enabled = false;
            this.btnEnviar.Location = new System.Drawing.Point(502, 560);
            this.btnEnviar.Name = "btnEnviar";
            this.btnEnviar.Size = new System.Drawing.Size(132, 34);
            this.btnEnviar.TabIndex = 7;
            this.btnEnviar.Text = "Guardar pendiente";
            this.btnEnviar.UseVisualStyleBackColor = false;
            this.btnEnviar.Click += new System.EventHandler(this.btnEnviar_Click);
            this.btnCobrarAhora.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCobrarAhora.BackColor = System.Drawing.Color.Olive;
            this.btnCobrarAhora.Enabled = false;
            this.btnCobrarAhora.Location = new System.Drawing.Point(640, 560);
            this.btnCobrarAhora.Name = "btnCobrarAhora";
            this.btnCobrarAhora.Size = new System.Drawing.Size(132, 34);
            this.btnCobrarAhora.TabIndex = 8;
            this.btnCobrarAhora.Text = "Guardar y cobrar";
            this.btnCobrarAhora.UseVisualStyleBackColor = false;
            this.btnCobrarAhora.Click += new System.EventHandler(this.btnCobrarAhora_Click);
            this.nudCantidadDetalle.AccessibleName = "Cantidad de la línea seleccionada";
            this.nudCantidadDetalle.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.nudCantidadDetalle.Enabled = false;
            this.nudCantidadDetalle.Location = new System.Drawing.Point(4, 4);
            this.nudCantidadDetalle.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.nudCantidadDetalle.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.nudCantidadDetalle.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudCantidadDetalle.Name = "nudCantidadDetalle";
            this.nudCantidadDetalle.Size = new System.Drawing.Size(52, 28);
            this.nudCantidadDetalle.TabIndex = 3;
            this.nudCantidadDetalle.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.btnCambiarCantidad.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnCambiarCantidad.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnCambiarCantidad.Enabled = false;
            this.btnCambiarCantidad.Location = new System.Drawing.Point(64, 4);
            this.btnCambiarCantidad.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnCambiarCantidad.Name = "btnCambiarCantidad";
            this.btnCambiarCantidad.Size = new System.Drawing.Size(136, 34);
            this.btnCambiarCantidad.TabIndex = 4;
            this.btnCambiarCantidad.Text = "Cambiar cantidad";
            this.btnCambiarCantidad.UseVisualStyleBackColor = false;
            this.btnCambiarCantidad.Click += new System.EventHandler(this.btnCambiarCantidad_Click);
            this.btnGuardarCambios.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGuardarCambios.BackColor = System.Drawing.Color.Lime;
            this.btnGuardarCambios.Enabled = false;
            this.btnGuardarCambios.Location = new System.Drawing.Point(620, 560);
            this.btnGuardarCambios.Margin = new System.Windows.Forms.Padding(2);
            this.btnGuardarCambios.Name = "btnGuardarCambios";
            this.btnGuardarCambios.Size = new System.Drawing.Size(152, 34);
            this.btnGuardarCambios.TabIndex = 9;
            this.btnGuardarCambios.Text = "Guardar cambios";
            this.btnGuardarCambios.UseVisualStyleBackColor = false;
            this.btnGuardarCambios.Visible = false;
            this.btnGuardarCambios.Click += new System.EventHandler(this.btnGuardarCambios_Click);
            this.lblTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTotal.Location = new System.Drawing.Point(502, 506);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(270, 40);
            this.lblTotal.TabIndex = 5;
            this.lblTotal.Text = "Total estimado: 0.00";
            this.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.accionesDetalle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.accionesDetalle.Controls.Add(this.nudCantidadDetalle);
            this.accionesDetalle.Controls.Add(this.btnCambiarCantidad);
            this.accionesDetalle.Controls.Add(this.btnQuitar);
            this.accionesDetalle.Controls.Add(this.btnVaciar);
            this.accionesDetalle.Location = new System.Drawing.Point(12, 506);
            this.accionesDetalle.Name = "accionesDetalle";
            this.accionesDetalle.Size = new System.Drawing.Size(488, 40);
            this.accionesDetalle.TabIndex = 4;
            this.accionesDetalle.WrapContents = false;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.IndianRed;
            this.CancelButton = this.btnCancelar;
            this.ClientSize = new System.Drawing.Size(784, 610);
            this.Controls.Add(this.accionesDetalle);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.estadoPerfil);
            this.Controls.Add(this.lblEstadoCatalogo);
            this.Controls.Add(this.btnRecargarCatalogo);
            this.Controls.Add(this.dgvDetalle);
            this.Controls.Add(this.btnEnviar);
            this.Controls.Add(this.btnCobrarAhora);
            this.Controls.Add(this.btnGuardarCambios);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.grpProducto);
            this.Controls.Add(this.grpDatosCliente);
            this.Controls.Add(this.pnlEncabezado);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(800, 649);
            this.Name = "FrmPedido";
            this.Text = "Nueva venta local";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmPedido_FormClosing);
            this.Load += new System.EventHandler(this.CargarPantalla);
            this.pnlEncabezado.ResumeLayout(false);
            this.pnlEncabezado.PerformLayout();
            this.grpDatosCliente.ResumeLayout(false);
            this.grpDatosCliente.PerformLayout();
            this.accionesDetalle.ResumeLayout(false);
            this.grpProducto.ResumeLayout(false);
            this.grpProducto.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCantidad)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCantidadDetalle)).EndInit();
            this.btnQuitar.FlatAppearance.BorderColor = System.Drawing.Color.DarkGray;
            this.btnVaciar.FlatAppearance.BorderColor = System.Drawing.Color.DarkGray;
            this.btnEnviar.FlatAppearance.BorderColor = System.Drawing.Color.DarkGray;
            this.btnCobrarAhora.FlatAppearance.BorderColor = System.Drawing.Color.DarkGray;
            this.pnlEncabezado.BackColor = System.Drawing.Color.Maroon;
            this.btnAceptar.FlatAppearance.BorderColor = System.Drawing.Color.DarkGray;
            this.btnCancelar.FlatAppearance.BorderColor = System.Drawing.Color.DarkGray;
            this.cmbPrecio.Size = new System.Drawing.Size(160, 23);
            this.cmbProducto.Size = new System.Drawing.Size(320, 23);
            this.cmbCategoria.Size = new System.Drawing.Size(320, 23);
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.BackColor = System.Drawing.Color.Khaki;
            this.ClientSize = new System.Drawing.Size(784, 610);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmPedido";
            this.ResumeLayout(false);

        }

        #endregion




        private System.Windows.Forms.FlowLayoutPanel accionesDetalle;
        private System.Windows.Forms.NumericUpDown nudCantidad;
        private System.Windows.Forms.Label estadoPerfil;
        private System.Windows.Forms.Label lblEstadoCatalogo;
        private System.Windows.Forms.Button btnRecargarCatalogo;
        private System.Windows.Forms.DataGridView dgvDetalle;
        private System.Windows.Forms.Button btnQuitar;
        private System.Windows.Forms.Button btnVaciar;
        private System.Windows.Forms.Button btnEnviar;
        private System.Windows.Forms.Button btnCobrarAhora;
        private System.Windows.Forms.NumericUpDown nudCantidadDetalle;
        private System.Windows.Forms.Button btnCambiarCantidad;
        private System.Windows.Forms.Button btnGuardarCambios;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label lblBuscarProducto;
        private System.Windows.Forms.TextBox txtBuscarProducto;

        private System.Windows.Forms.Panel pnlEncabezado;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.GroupBox grpDatosCliente;
        private System.Windows.Forms.Label lblNombreOpcional;
        private System.Windows.Forms.Label lblResumenImportes;
        private System.Windows.Forms.TextBox txtCliente;
        private System.Windows.Forms.Label lblCiente;
        private System.Windows.Forms.GroupBox grpProducto;
        private System.Windows.Forms.Label lblCantidadPlato;
        private System.Windows.Forms.Label lblPrecioPlato;
        private System.Windows.Forms.Label lblProducto;


        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Label lblCategoria;


        private System.Windows.Forms.ComboBox cmbPrecio;

        private System.Windows.Forms.ComboBox cmbProducto;

        private System.Windows.Forms.ComboBox cmbCategoria;
    }
}
