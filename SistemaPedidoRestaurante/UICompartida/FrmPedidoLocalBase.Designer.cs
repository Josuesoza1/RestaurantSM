namespace SistemaPedidoRestaurante.UICompartida
{
    partial class FrmPedidoLocalBase
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlEncabezado = new System.Windows.Forms.Panel();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.grpDatosCliente = new System.Windows.Forms.GroupBox();
            this.lblCedulaCliente = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.txtCedula = new System.Windows.Forms.TextBox();
            this.txtTelefono = new System.Windows.Forms.TextBox();
            this.txtCliente = new System.Windows.Forms.TextBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.lblTelefono = new System.Windows.Forms.Label();
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
            System.Windows.Forms.DataGridViewCellStyle estiloPrecio = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle estiloSubtotal = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnQuitar = new System.Windows.Forms.Button();
            this.btnVaciar = new System.Windows.Forms.Button();
            this.btnEnviar = new System.Windows.Forms.Button();
            this.lblTotal = new System.Windows.Forms.Label();
            this.pnlEncabezado.SuspendLayout();
            this.grpDatosCliente.SuspendLayout();
            this.grpProducto.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCantidad)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalle)).BeginInit();
            this.SuspendLayout();
            //
            // pnlEncabezado
            //
            this.pnlEncabezado.BackColor = System.Drawing.Color.DarkSlateBlue;
            this.pnlEncabezado.Controls.Add(this.lblTitulo);
            this.pnlEncabezado.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlEncabezado.Location = new System.Drawing.Point(0, 0);
            this.pnlEncabezado.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnlEncabezado.Name = "pnlEncabezado";
            this.pnlEncabezado.Size = new System.Drawing.Size(1045, 86);
            this.pnlEncabezado.TabIndex = 1;
            //
            // lblTitulo
            //
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Location = new System.Drawing.Point(64, 25);
            this.lblTitulo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(171, 35);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "PEDIDO EN EL LOCAL";
            //
            // grpDatosCliente
            //
            this.grpDatosCliente.Controls.Add(this.lblCedulaCliente);
            this.grpDatosCliente.Controls.Add(this.txtEmail);
            this.grpDatosCliente.Controls.Add(this.txtCedula);
            this.grpDatosCliente.Controls.Add(this.txtTelefono);
            this.grpDatosCliente.Controls.Add(this.txtCliente);
            this.grpDatosCliente.Controls.Add(this.lblEmail);
            this.grpDatosCliente.Controls.Add(this.lblTelefono);
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
            this.grpDatosCliente.Text = "Mis datos";
            //
            // lblCedulaCliente
            //
            this.lblCedulaCliente.AutoSize = true;
            this.lblCedulaCliente.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblCedulaCliente.Location = new System.Drawing.Point(28, 66);
            this.lblCedulaCliente.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCedulaCliente.Name = "lblCedulaCliente";
            this.lblCedulaCliente.Size = new System.Drawing.Size(59, 16);
            this.lblCedulaCliente.TabIndex = 14;
            this.lblCedulaCliente.Text = "Cédula:";
            //
            // txtEmail
            //
            this.txtEmail.Location = new System.Drawing.Point(101, 132);
            this.txtEmail.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.ReadOnly = true;
            this.txtEmail.Size = new System.Drawing.Size(267, 23);
            this.txtEmail.TabIndex = 13;
            //
            // txtCedula
            //
            this.txtCedula.Location = new System.Drawing.Point(101, 63);
            this.txtCedula.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtCedula.Name = "txtCedula";
            this.txtCedula.ReadOnly = true;
            this.txtCedula.Size = new System.Drawing.Size(267, 23);
            this.txtCedula.TabIndex = 12;
            //
            // txtTelefono
            //
            this.txtTelefono.Location = new System.Drawing.Point(101, 100);
            this.txtTelefono.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtTelefono.Name = "txtTelefono";
            this.txtTelefono.ReadOnly = true;
            this.txtTelefono.Size = new System.Drawing.Size(267, 23);
            this.txtTelefono.TabIndex = 8;
            //
            // txtCliente
            //
            this.txtCliente.Location = new System.Drawing.Point(101, 23);
            this.txtCliente.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtCliente.Name = "txtCliente";
            this.txtCliente.ReadOnly = true;
            this.txtCliente.Size = new System.Drawing.Size(267, 23);
            this.txtCliente.TabIndex = 7;
            //
            // lblEmail
            //
            this.lblEmail.AutoSize = true;
            this.lblEmail.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblEmail.Location = new System.Drawing.Point(40, 135);
            this.lblEmail.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(48, 16);
            this.lblEmail.TabIndex = 6;
            this.lblEmail.Text = "Email:";
            //
            // lblTelefono
            //
            this.lblTelefono.AutoSize = true;
            this.lblTelefono.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblTelefono.Location = new System.Drawing.Point(16, 103);
            this.lblTelefono.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTelefono.Name = "lblTelefono";
            this.lblTelefono.Size = new System.Drawing.Size(72, 16);
            this.lblTelefono.TabIndex = 5;
            this.lblTelefono.Text = "Telefono:";
            //
            // lblCiente
            //
            this.lblCiente.AutoSize = true;
            this.lblCiente.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblCiente.Location = new System.Drawing.Point(28, 27);
            this.lblCiente.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCiente.Name = "lblCiente";
            this.lblCiente.Size = new System.Drawing.Size(61, 16);
            this.lblCiente.TabIndex = 0;
            this.lblCiente.Text = "Cliente:";
            this.lblCiente.Click += new System.EventHandler(this.lblPnombre_Click);
            //
            // grpProducto
            //
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
            //
            // cmbCategoria
            //
            this.cmbCategoria.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCategoria.FormattingEnabled = true;
            this.cmbCategoria.Location = new System.Drawing.Point(127, 25);
            this.cmbCategoria.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbCategoria.Name = "cmbCategoria";
            this.cmbCategoria.Size = new System.Drawing.Size(425, 24);
            this.cmbCategoria.TabIndex = 23;
            //
            // cmbPrecio
            //
            this.cmbPrecio.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPrecio.Enabled = false;
            this.cmbPrecio.FormattingEnabled = true;
            this.cmbPrecio.Location = new System.Drawing.Point(340, 111);
            this.cmbPrecio.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbPrecio.Name = "cmbPrecio";
            this.cmbPrecio.Size = new System.Drawing.Size(212, 24);
            this.cmbPrecio.TabIndex = 22;
            //
            // cmbProducto
            //
            this.cmbProducto.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProducto.FormattingEnabled = true;
            this.cmbProducto.Location = new System.Drawing.Point(127, 68);
            this.cmbProducto.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbProducto.Name = "cmbProducto";
            this.cmbProducto.Size = new System.Drawing.Size(425, 24);
            this.cmbProducto.TabIndex = 20;
            //
            // btnCancelar
            //
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
            //
            // btnAceptar
            //
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
            //
            // lblCategoria
            //
            this.lblCategoria.AutoSize = true;
            this.lblCategoria.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblCategoria.Location = new System.Drawing.Point(16, 31);
            this.lblCategoria.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCategoria.Name = "lblCategoria";
            this.lblCategoria.Size = new System.Drawing.Size(79, 16);
            this.lblCategoria.TabIndex = 15;
            this.lblCategoria.Text = "Categoría:";
            //
            // lblCantidadPlato
            //
            this.lblCantidadPlato.AutoSize = true;
            this.lblCantidadPlato.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblCantidadPlato.Location = new System.Drawing.Point(16, 117);
            this.lblCantidadPlato.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCantidadPlato.Name = "lblCantidadPlato";
            this.lblCantidadPlato.Size = new System.Drawing.Size(73, 16);
            this.lblCantidadPlato.TabIndex = 14;
            this.lblCantidadPlato.Text = "Cantidad:";
            //
            // lblPrecioPlato
            //
            this.lblPrecioPlato.AutoSize = true;
            this.lblPrecioPlato.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblPrecioPlato.Location = new System.Drawing.Point(260, 117);
            this.lblPrecioPlato.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPrecioPlato.Name = "lblPrecioPlato";
            this.lblPrecioPlato.Size = new System.Drawing.Size(56, 16);
            this.lblPrecioPlato.TabIndex = 5;
            this.lblPrecioPlato.Text = "Precio:";
            //
            // lblProducto
            //
            this.lblProducto.AutoSize = true;
            this.lblProducto.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblProducto.Location = new System.Drawing.Point(16, 74);
            this.lblProducto.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblProducto.Name = "lblProducto";
            this.lblProducto.Size = new System.Drawing.Size(75, 16);
            this.lblProducto.TabIndex = 0;
            this.lblProducto.Text = "Producto:";
            //
            // nudCantidad
            //
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
            this.nudCantidad.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            //
            // dgvDetalle
            //
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
            //
            // btnQuitar
            //
            this.btnQuitar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnQuitar.Location = new System.Drawing.Point(16, 628);
            this.btnQuitar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnQuitar.Name = "btnQuitar";
            this.btnQuitar.Click += new System.EventHandler(this.btnQuitar_Click);
            this.btnQuitar.Size = new System.Drawing.Size(193, 39);
            this.btnQuitar.TabIndex = 1;
            this.btnQuitar.Text = "Quitar seleccionado";
            //
            // btnVaciar
            //
            this.btnVaciar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnVaciar.Location = new System.Drawing.Point(220, 628);
            this.btnVaciar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnVaciar.Name = "btnVaciar";
            this.btnVaciar.Click += new System.EventHandler(this.btnVaciar_Click);
            this.btnVaciar.Size = new System.Drawing.Size(180, 39);
            this.btnVaciar.TabIndex = 2;
            this.btnVaciar.Text = "Vaciar pedido";
            //
            // btnEnviar
            //
            this.btnEnviar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnEnviar.Enabled = false;
            this.btnEnviar.Location = new System.Drawing.Point(413, 628);
            this.btnEnviar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnEnviar.Name = "btnEnviar";
            this.btnEnviar.Click += new System.EventHandler(this.btnEnviar_Click);
            this.btnEnviar.Size = new System.Drawing.Size(180, 39);
            this.btnEnviar.TabIndex = 3;
            this.btnEnviar.Text = "Enviar pedido";
            //
            // lblTotal
            //
            this.lblTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTotal.Location = new System.Drawing.Point(653, 628);
            this.lblTotal.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(373, 39);
            this.lblTotal.TabIndex = 4;
            this.lblTotal.Text = "Total estimado: 0.00";
            //
            // colProducto
            //
            this.colProducto.Name = "colProducto";
            this.colProducto.DataPropertyName = "Producto";
            this.colProducto.HeaderText = "Producto";
            this.colProducto.FillWeight = 180F;
            this.colProducto.ReadOnly = true;
            //
            // colCantidad
            //
            this.colCantidad.Name = "colCantidad";
            this.colCantidad.DataPropertyName = "Cantidad";
            this.colCantidad.HeaderText = "Cantidad";
            this.colCantidad.FillWeight = 70F;
            this.colCantidad.ReadOnly = true;
            //
            // colPrecio
            //
            this.colPrecio.Name = "colPrecio";
            this.colPrecio.DataPropertyName = "PrecioUnitario";
            this.colPrecio.HeaderText = "Precio unitario";
            this.colPrecio.FillWeight = 100F;
            this.colPrecio.ReadOnly = true;
            //
            // colSubtotal
            //
            this.colSubtotal.Name = "colSubtotal";
            this.colSubtotal.DataPropertyName = "Subtotal";
            this.colSubtotal.HeaderText = "Subtotal";
            this.colSubtotal.FillWeight = 100F;
            this.colSubtotal.ReadOnly = true;
            estiloPrecio.Format = "N2";
            estiloSubtotal.Format = "N2";
            this.colPrecio.DefaultCellStyle = estiloPrecio;
            this.colSubtotal.DefaultCellStyle = estiloSubtotal;
            //
            // estadoPerfil
            //
            this.estadoPerfil.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.estadoPerfil.Location = new System.Drawing.Point(16, 365);
            this.estadoPerfil.Name = "estadoPerfil";
            this.estadoPerfil.Size = new System.Drawing.Size(1013, 32);
            this.estadoPerfil.Text = "Selecciona productos para preparar tu pedido. Todavía no se ha enviado.";
            //
            // FrmPedidoLocalBase
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGray;
            this.ClientSize = new System.Drawing.Size(1045, 695);
            this.Controls.Add(this.estadoPerfil);
            this.Controls.Add(this.dgvDetalle);
            this.Controls.Add(this.btnQuitar);
            this.Controls.Add(this.btnVaciar);
            this.Controls.Add(this.btnEnviar);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.grpProducto);
            this.Controls.Add(this.grpDatosCliente);
            this.Controls.Add(this.pnlEncabezado);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MinimumSize = new System.Drawing.Size(1061, 733);
            this.Name = "FrmPedidoLocalBase";
            this.Text = "MI PEDIDO";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.pnlEncabezado.ResumeLayout(false);
            this.pnlEncabezado.PerformLayout();
            this.grpDatosCliente.ResumeLayout(false);
            this.grpDatosCliente.PerformLayout();
            this.grpProducto.ResumeLayout(false);
            this.grpProducto.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCantidad)).EndInit();
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
        protected System.Windows.Forms.DataGridView dgvDetalle;
        protected System.Windows.Forms.Button btnQuitar;
        protected System.Windows.Forms.Button btnVaciar;
        protected System.Windows.Forms.Button btnEnviar;
        protected System.Windows.Forms.Label lblTotal;

        protected System.Windows.Forms.Panel pnlEncabezado;
        protected System.Windows.Forms.Label lblTitulo;
        protected System.Windows.Forms.GroupBox grpDatosCliente;
        protected System.Windows.Forms.TextBox txtEmail;
        protected System.Windows.Forms.TextBox txtCedula;
        protected System.Windows.Forms.TextBox txtTelefono;
        protected System.Windows.Forms.TextBox txtCliente;
        protected System.Windows.Forms.Label lblEmail;
        protected System.Windows.Forms.Label lblTelefono;
        protected System.Windows.Forms.Label lblCiente;
        protected System.Windows.Forms.Label lblCedulaCliente;
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
