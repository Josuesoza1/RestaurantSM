namespace SistemaPedidoRestaurante.UICompartida
{
    partial class FrmPedidoLocalBase
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
            this.pnlEncabezado = new System.Windows.Forms.Panel();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.grpDatosCliente = new System.Windows.Forms.GroupBox();
            this.lblNombreOpcional = new System.Windows.Forms.Label();
            this.txtCliente = new System.Windows.Forms.TextBox();
            this.lblCiente = new System.Windows.Forms.Label();
            this.grpProducto = new System.Windows.Forms.GroupBox();
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
            this.colProducto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPrecio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSubtotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.estadoPerfil = new System.Windows.Forms.Label();
            this.lblEstadoCatalogo = new System.Windows.Forms.Label();
            this.btnRecargarCatalogo = new System.Windows.Forms.Button();
            System.Windows.Forms.DataGridViewCellStyle estiloPrecio = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle estiloSubtotal = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnQuitar = new System.Windows.Forms.Button();
            this.btnVaciar = new System.Windows.Forms.Button();
            this.btnEnviar = new System.Windows.Forms.Button();
            this.btnCobrarAhora = new System.Windows.Forms.Button();
            this.nudCantidadDetalle = new System.Windows.Forms.NumericUpDown();
            this.btnCambiarCantidad = new System.Windows.Forms.Button();
            this.btnGuardarCambios = new System.Windows.Forms.Button();
            this.lblTotal = new System.Windows.Forms.Label();
            this.pnlEncabezado.SuspendLayout();
            this.grpDatosCliente.SuspendLayout();
            this.grpProducto.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCantidad)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCantidadDetalle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalle)).BeginInit();
            this.SuspendLayout();
            this.pnlEncabezado.BackColor = System.Drawing.Color.DarkSlateBlue;
            this.pnlEncabezado.Controls.Add(this.lblTitulo);
            this.pnlEncabezado.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlEncabezado.Location = new System.Drawing.Point(0, 0);
            this.pnlEncabezado.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnlEncabezado.Name = "pnlEncabezado";
            this.pnlEncabezado.Size = new System.Drawing.Size(1045, 86);
            this.pnlEncabezado.TabIndex = 1;
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Location = new System.Drawing.Point(64, 25);
            this.lblTitulo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(171, 35);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "PEDIDO EN EL LOCAL";
            this.grpDatosCliente.Controls.Add(this.txtCliente);
            this.grpDatosCliente.Controls.Add(this.lblNombreOpcional);
            this.grpDatosCliente.Controls.Add(this.lblCiente);
            this.grpDatosCliente.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpDatosCliente.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.grpDatosCliente.Location = new System.Drawing.Point(16, 134);
            this.grpDatosCliente.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grpDatosCliente.Name = "grpDatosCliente";
            this.grpDatosCliente.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grpDatosCliente.Size = new System.Drawing.Size(413, 215);
            this.grpDatosCliente.TabIndex = 2;
            this.grpDatosCliente.TabStop = false;
            this.grpDatosCliente.Text = "Cliente de la venta";
            this.lblNombreOpcional.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblNombreOpcional.Location = new System.Drawing.Point(28, 64);
            this.lblNombreOpcional.Name = "lblNombreOpcional";
            this.lblNombreOpcional.Size = new System.Drawing.Size(340, 48);
            this.lblNombreOpcional.Text = "Al cobrar, puede indicar un nombre para el comprobante.";
            this.txtCliente.Location = new System.Drawing.Point(101, 23);
            this.txtCliente.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtCliente.Name = "txtCliente";
            this.txtCliente.ReadOnly = true;
            this.txtCliente.TabStop = false;
            this.txtCliente.Text = "Consumidor final";
            this.txtCliente.Size = new System.Drawing.Size(267, 23);
            this.txtCliente.TabIndex = 7;
            this.lblCiente.AutoSize = true;
            this.lblCiente.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblCiente.Location = new System.Drawing.Point(28, 27);
            this.lblCiente.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCiente.Name = "lblCiente";
            this.lblCiente.Size = new System.Drawing.Size(61, 16);
            this.lblCiente.TabIndex = 0;
            this.lblCiente.Text = "Cliente:";
            this.grpProducto.Controls.Add(this.cmbCategoria);
            this.grpProducto.Controls.Add(this.cmbPrecio);
            this.grpProducto.Controls.Add(this.cmbProducto);
            this.grpProducto.Controls.Add(this.btnCancelar);
            this.grpProducto.Controls.Add(this.btnAceptar);
            this.grpProducto.Controls.Add(this.lblCategoria);
            this.grpProducto.Controls.Add(this.lblCantidadPlato);
            this.grpProducto.Controls.Add(this.lblPrecioPlato);
            this.grpProducto.Controls.Add(this.lblProducto);
            this.grpProducto.Controls.Add(this.nudCantidad);
            this.grpProducto.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpProducto.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.grpProducto.Location = new System.Drawing.Point(456, 134);
            this.grpProducto.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grpProducto.Name = "grpProducto";
            this.grpProducto.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grpProducto.Size = new System.Drawing.Size(580, 215);
            this.grpProducto.TabIndex = 3;
            this.grpProducto.TabStop = false;
            this.grpProducto.Text = "Producto";
            this.cmbCategoria.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCategoria.FormattingEnabled = true;
            this.cmbCategoria.Location = new System.Drawing.Point(127, 25);
            this.cmbCategoria.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbCategoria.Name = "cmbCategoria";
            this.cmbCategoria.Size = new System.Drawing.Size(425, 24);
            this.cmbCategoria.TabIndex = 23;
            this.cmbCategoria.SelectedValueChanged += new System.EventHandler(this.cmbCategoria_SelectedValueChanged);
            this.cmbPrecio.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPrecio.Enabled = false;
            this.cmbPrecio.FormattingEnabled = true;
            this.cmbPrecio.Location = new System.Drawing.Point(340, 111);
            this.cmbPrecio.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbPrecio.Name = "cmbPrecio";
            this.cmbPrecio.Size = new System.Drawing.Size(212, 24);
            this.cmbPrecio.TabIndex = 22;
            this.cmbProducto.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProducto.FormattingEnabled = true;
            this.cmbProducto.Location = new System.Drawing.Point(127, 68);
            this.cmbProducto.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbProducto.Name = "cmbProducto";
            this.cmbProducto.Size = new System.Drawing.Size(425, 24);
            this.cmbProducto.TabIndex = 20;
            this.cmbProducto.SelectedValueChanged += new System.EventHandler(this.cmbProducto_SelectedValueChanged);
            this.btnCancelar.BackColor = System.Drawing.Color.Red;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Font = new System.Drawing.Font("Arial Black", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.ForeColor = System.Drawing.Color.Black;
            this.btnCancelar.Location = new System.Drawing.Point(353, 160);
            this.btnCancelar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            this.btnCancelar.Size = new System.Drawing.Size(200, 38);
            this.btnCancelar.TabIndex = 19;
            this.btnCancelar.Text = "Volver";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnAceptar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnAceptar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAceptar.Font = new System.Drawing.Font("Arial Black", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAceptar.ForeColor = System.Drawing.Color.Black;
            this.btnAceptar.Location = new System.Drawing.Point(127, 160);
            this.btnAceptar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            this.btnAceptar.Size = new System.Drawing.Size(207, 38);
            this.btnAceptar.TabIndex = 18;
            this.btnAceptar.Text = "Agregar al pedido";
            this.btnAceptar.UseVisualStyleBackColor = false;
            this.lblCategoria.AutoSize = true;
            this.lblCategoria.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblCategoria.Location = new System.Drawing.Point(16, 31);
            this.lblCategoria.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCategoria.Name = "lblCategoria";
            this.lblCategoria.Size = new System.Drawing.Size(79, 16);
            this.lblCategoria.TabIndex = 15;
            this.lblCategoria.Text = "Categoría:";
            this.lblCantidadPlato.AutoSize = true;
            this.lblCantidadPlato.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblCantidadPlato.Location = new System.Drawing.Point(16, 117);
            this.lblCantidadPlato.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCantidadPlato.Name = "lblCantidadPlato";
            this.lblCantidadPlato.Size = new System.Drawing.Size(73, 16);
            this.lblCantidadPlato.TabIndex = 14;
            this.lblCantidadPlato.Text = "Cantidad:";
            this.lblPrecioPlato.AutoSize = true;
            this.lblPrecioPlato.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblPrecioPlato.Location = new System.Drawing.Point(260, 117);
            this.lblPrecioPlato.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPrecioPlato.Name = "lblPrecioPlato";
            this.lblPrecioPlato.Size = new System.Drawing.Size(56, 16);
            this.lblPrecioPlato.TabIndex = 5;
            this.lblPrecioPlato.Text = "Precio:";
            this.lblProducto.AutoSize = true;
            this.lblProducto.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblProducto.Location = new System.Drawing.Point(16, 74);
            this.lblProducto.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblProducto.Name = "lblProducto";
            this.lblProducto.Size = new System.Drawing.Size(75, 16);
            this.lblProducto.TabIndex = 0;
            this.lblProducto.Text = "Producto:";
            this.nudCantidad.Location = new System.Drawing.Point(127, 111);
            this.nudCantidad.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
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
            this.nudCantidad.Size = new System.Drawing.Size(107, 23);
            this.nudCantidad.TabIndex = 26;
            this.nudCantidad.ValueChanged += new System.EventHandler(this.nudCantidad_ValueChanged);
            this.nudCantidad.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.dgvDetalle.AllowUserToAddRows = false;
            this.dgvDetalle.AllowUserToDeleteRows = false;
            this.dgvDetalle.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvDetalle.AutoGenerateColumns = false;
            this.dgvDetalle.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                this.colProducto, this.colCantidad, this.colPrecio, this.colSubtotal });
            this.dgvDetalle.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDetalle.ColumnHeadersHeight = 29;
            this.dgvDetalle.Location = new System.Drawing.Point(16, 406);
            this.dgvDetalle.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dgvDetalle.MultiSelect = false;
            this.dgvDetalle.Name = "dgvDetalle";
            this.dgvDetalle.ReadOnly = true;
            this.dgvDetalle.RowHeadersWidth = 51;
            this.dgvDetalle.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDetalle.Size = new System.Drawing.Size(1013, 197);
            this.dgvDetalle.TabIndex = 0;
            this.dgvDetalle.SelectionChanged += new System.EventHandler(this.dgvDetalle_SelectionChanged);
            this.btnQuitar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnQuitar.Location = new System.Drawing.Point(16, 628);
            this.btnQuitar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnQuitar.Name = "btnQuitar";
            this.btnQuitar.Click += new System.EventHandler(this.btnQuitar_Click);
            this.btnQuitar.Size = new System.Drawing.Size(193, 39);
            this.btnQuitar.TabIndex = 1;
            this.btnQuitar.Text = "Quitar seleccionado";
            this.btnVaciar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnVaciar.Location = new System.Drawing.Point(220, 628);
            this.btnVaciar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnVaciar.Name = "btnVaciar";
            this.btnVaciar.Click += new System.EventHandler(this.btnVaciar_Click);
            this.btnVaciar.Size = new System.Drawing.Size(180, 39);
            this.btnVaciar.TabIndex = 2;
            this.btnVaciar.Text = "Vaciar pedido";
            this.btnEnviar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnEnviar.Enabled = false;
            this.btnEnviar.Location = new System.Drawing.Point(413, 628);
            this.btnEnviar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnEnviar.Name = "btnEnviar";
            this.btnEnviar.Click += new System.EventHandler(this.btnEnviar_Click);
            this.btnEnviar.Size = new System.Drawing.Size(180, 39);
            this.btnEnviar.TabIndex = 3;
            this.btnEnviar.Text = "Guardar pendiente";
            this.btnCobrarAhora.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCobrarAhora.Enabled = false;
            this.btnCobrarAhora.Location = new System.Drawing.Point(606, 628);
            this.btnCobrarAhora.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCobrarAhora.Name = "btnCobrarAhora";
            this.btnCobrarAhora.Size = new System.Drawing.Size(180, 39);
            this.btnCobrarAhora.TabIndex = 4;
            this.btnCobrarAhora.Text = "Cobrar ahora";
            this.btnCobrarAhora.Click += new System.EventHandler(this.btnCobrarAhora_Click);
            this.nudCantidadDetalle.AccessibleName = "Cantidad de la línea seleccionada";
            this.nudCantidadDetalle.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            this.nudCantidadDetalle.Location = new System.Drawing.Point(220, 636);
            this.nudCantidadDetalle.Maximum = 999;
            this.nudCantidadDetalle.Minimum = 1;
            this.nudCantidadDetalle.Name = "nudCantidadDetalle";
            this.nudCantidadDetalle.Size = new System.Drawing.Size(64, 23);
            this.nudCantidadDetalle.TabIndex = 6;
            this.nudCantidadDetalle.Value = 1;
            this.nudCantidadDetalle.Visible = false;
            this.btnCambiarCantidad.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            this.btnCambiarCantidad.Location = new System.Drawing.Point(293, 628);
            this.btnCambiarCantidad.Name = "btnCambiarCantidad";
            this.btnCambiarCantidad.Size = new System.Drawing.Size(171, 39);
            this.btnCambiarCantidad.TabIndex = 7;
            this.btnCambiarCantidad.Text = "Cambiar cantidad";
            this.btnCambiarCantidad.Visible = false;
            this.btnCambiarCantidad.Click += new System.EventHandler(this.btnCambiarCantidad_Click);
            this.btnGuardarCambios.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            this.btnGuardarCambios.Enabled = false;
            this.btnGuardarCambios.Location = new System.Drawing.Point(477, 628);
            this.btnGuardarCambios.Name = "btnGuardarCambios";
            this.btnGuardarCambios.Size = new System.Drawing.Size(180, 39);
            this.btnGuardarCambios.TabIndex = 8;
            this.btnGuardarCambios.Text = "Guardar cambios";
            this.btnGuardarCambios.Visible = false;
            this.btnGuardarCambios.Click += new System.EventHandler(this.btnGuardarCambios_Click);
            this.lblTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTotal.Location = new System.Drawing.Point(800, 628);
            this.lblTotal.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(226, 39);
            this.lblTotal.TabIndex = 5;
            this.lblTotal.Text = "Total estimado: 0.00";
            this.colProducto.Name = "colProducto";
            this.colProducto.DataPropertyName = "Producto";
            this.colProducto.HeaderText = "Producto";
            this.colProducto.FillWeight = 180F;
            this.colProducto.ReadOnly = true;
            this.colCantidad.Name = "colCantidad";
            this.colCantidad.DataPropertyName = "Cantidad";
            this.colCantidad.HeaderText = "Cantidad";
            this.colCantidad.FillWeight = 70F;
            this.colCantidad.ReadOnly = true;
            this.colPrecio.Name = "colPrecio";
            this.colPrecio.DataPropertyName = "PrecioUnitario";
            this.colPrecio.HeaderText = "Precio unitario";
            this.colPrecio.FillWeight = 100F;
            this.colPrecio.ReadOnly = true;
            this.colSubtotal.Name = "colSubtotal";
            this.colSubtotal.DataPropertyName = "Subtotal";
            this.colSubtotal.HeaderText = "Subtotal";
            this.colSubtotal.FillWeight = 100F;
            this.colSubtotal.ReadOnly = true;
            estiloPrecio.Format = "N2";
            estiloSubtotal.Format = "N2";
            this.colPrecio.DefaultCellStyle = estiloPrecio;
            this.colSubtotal.DefaultCellStyle = estiloSubtotal;
            this.estadoPerfil.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.estadoPerfil.Location = new System.Drawing.Point(16, 365);
            this.estadoPerfil.Name = "estadoPerfil";
            this.estadoPerfil.Size = new System.Drawing.Size(1013, 32);
            this.estadoPerfil.Text = "Selecciona productos para preparar tu pedido. Todavía no se ha enviado.";
            this.lblEstadoCatalogo.AutoEllipsis = true;
            this.lblEstadoCatalogo.Location = new System.Drawing.Point(16, 92);
            this.lblEstadoCatalogo.Name = "lblEstadoCatalogo";
            this.lblEstadoCatalogo.Size = new System.Drawing.Size(806, 35);
            this.lblEstadoCatalogo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRecargarCatalogo.Enabled = false;
            this.btnRecargarCatalogo.Location = new System.Drawing.Point(836, 92);
            this.btnRecargarCatalogo.Name = "btnRecargarCatalogo";
            this.btnRecargarCatalogo.Size = new System.Drawing.Size(200, 35);
            this.btnRecargarCatalogo.TabIndex = 9;
            this.btnRecargarCatalogo.Text = "Recargar catálogo";
            this.btnRecargarCatalogo.Click += new System.EventHandler(this.btnRecargarCatalogo_Click);
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGray;
            this.ClientSize = new System.Drawing.Size(1045, 695);
            this.Controls.Add(this.estadoPerfil);
            this.Controls.Add(this.lblEstadoCatalogo);
            this.Controls.Add(this.btnRecargarCatalogo);
            this.Controls.Add(this.dgvDetalle);
            this.Controls.Add(this.btnQuitar);
            this.Controls.Add(this.btnVaciar);
            this.Controls.Add(this.btnEnviar);
            this.Controls.Add(this.btnCobrarAhora);
            this.Controls.Add(this.nudCantidadDetalle);
            this.Controls.Add(this.btnCambiarCantidad);
            this.Controls.Add(this.btnGuardarCambios);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.grpProducto);
            this.Controls.Add(this.grpDatosCliente);
            this.Controls.Add(this.pnlEncabezado);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MinimumSize = new System.Drawing.Size(1061, 733);
            this.Name = "FrmPedidoLocalBase";
            this.Text = "MI PEDIDO";
            this.Load += new System.EventHandler(this.CargarPantalla);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmPedidoLocalBase_FormClosing);
            this.pnlEncabezado.ResumeLayout(false);
            this.pnlEncabezado.PerformLayout();
            this.grpDatosCliente.ResumeLayout(false);
            this.grpDatosCliente.PerformLayout();
            this.grpProducto.ResumeLayout(false);
            this.grpProducto.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCantidad)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCantidadDetalle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalle)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        protected System.Windows.Forms.DataGridViewTextBoxColumn colProducto;
        protected System.Windows.Forms.DataGridViewTextBoxColumn colCantidad;
        protected System.Windows.Forms.DataGridViewTextBoxColumn colPrecio;
        protected System.Windows.Forms.DataGridViewTextBoxColumn colSubtotal;
        protected System.Windows.Forms.NumericUpDown nudCantidad;
        protected System.Windows.Forms.Label estadoPerfil;
        private System.Windows.Forms.Label lblEstadoCatalogo;
        private System.Windows.Forms.Button btnRecargarCatalogo;
        protected System.Windows.Forms.DataGridView dgvDetalle;
        protected System.Windows.Forms.Button btnQuitar;
        protected System.Windows.Forms.Button btnVaciar;
        protected System.Windows.Forms.Button btnEnviar;
        protected System.Windows.Forms.Button btnCobrarAhora;
        private System.Windows.Forms.NumericUpDown nudCantidadDetalle;
        private System.Windows.Forms.Button btnCambiarCantidad;
        private System.Windows.Forms.Button btnGuardarCambios;
        protected System.Windows.Forms.Label lblTotal;

        protected System.Windows.Forms.Panel pnlEncabezado;
        protected System.Windows.Forms.Label lblTitulo;
        protected System.Windows.Forms.GroupBox grpDatosCliente;
        private System.Windows.Forms.Label lblNombreOpcional;
        protected System.Windows.Forms.TextBox txtCliente;
        protected System.Windows.Forms.Label lblCiente;
        protected System.Windows.Forms.GroupBox grpProducto;
        protected System.Windows.Forms.Label lblCantidadPlato;
        protected System.Windows.Forms.Label lblPrecioPlato;
        protected System.Windows.Forms.Label lblProducto;


        protected System.Windows.Forms.Button btnAceptar;
        protected System.Windows.Forms.Button btnCancelar;
        protected System.Windows.Forms.Label lblCategoria;


        protected System.Windows.Forms.ComboBox cmbPrecio;

        protected System.Windows.Forms.ComboBox cmbProducto;

        protected System.Windows.Forms.ComboBox cmbCategoria;
    }
}
