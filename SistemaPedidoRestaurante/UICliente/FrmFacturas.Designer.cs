namespace SistemaPedidoRestaurante.UICliente
{
    partial class FrmFacturas
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.agregarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eliminarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.volverToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlEncabezado = new System.Windows.Forms.Panel();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.grpDatosFactura = new System.Windows.Forms.GroupBox();
            this.lblEmpleadoFactura = new System.Windows.Forms.Label();
            this.lblMesaFactura = new System.Windows.Forms.Label();
            this.lblClienteFactura = new System.Windows.Forms.Label();
            this.lblTipoPedido = new System.Windows.Forms.Label();
            this.lblFechaFactura = new System.Windows.Forms.Label();
            this.lblNumeroFactura = new System.Windows.Forms.Label();
            this.btnGuardarProducto = new System.Windows.Forms.Button();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.grpDetalleFactura = new System.Windows.Forms.GroupBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.ColProducto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColCantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColPrecioUnitario = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColSubtotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grpInformacionAdicional = new System.Windows.Forms.GroupBox();
            this.lblSubtotal = new System.Windows.Forms.Label();
            this.lblImpuestos = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtNumeroFactura = new System.Windows.Forms.TextBox();
            this.txtDescuentoFactura = new System.Windows.Forms.TextBox();
            this.txtImpuestoFactura = new System.Windows.Forms.TextBox();
            this.txtMesaFactura = new System.Windows.Forms.TextBox();
            this.txtEmpleadoFactura = new System.Windows.Forms.TextBox();
            this.txtClienteFactura = new System.Windows.Forms.TextBox();
            this.txtSubtotalFactura = new System.Windows.Forms.TextBox();
            this.txtTotalFactura = new System.Windows.Forms.TextBox();
            this.txtMetodoPago = new System.Windows.Forms.TextBox();
            this.lblMetodoPagoFactura = new System.Windows.Forms.Label();
            this.txtBuscarFactura = new System.Windows.Forms.TextBox();
            this.lblBuscarFactura = new System.Windows.Forms.Label();
            this.btnBuscarFactura = new System.Windows.Forms.Button();
            this.dtpFechaFactura = new System.Windows.Forms.DateTimePicker();
            this.cmbTipoPedido = new System.Windows.Forms.ComboBox();
            this.menuStrip1.SuspendLayout();
            this.pnlEncabezado.SuspendLayout();
            this.grpDatosFactura.SuspendLayout();
            this.grpDetalleFactura.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.grpInformacionAdicional.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.agregarToolStripMenuItem,
            this.eliminarToolStripMenuItem,
            this.editarToolStripMenuItem,
            this.volverToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // agregarToolStripMenuItem
            // 
            this.agregarToolStripMenuItem.Name = "agregarToolStripMenuItem";
            this.agregarToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.agregarToolStripMenuItem.Text = "Agregar";
            // 
            // eliminarToolStripMenuItem
            // 
            this.eliminarToolStripMenuItem.Name = "eliminarToolStripMenuItem";
            this.eliminarToolStripMenuItem.Size = new System.Drawing.Size(62, 20);
            this.eliminarToolStripMenuItem.Text = "Eliminar";
            // 
            // editarToolStripMenuItem
            // 
            this.editarToolStripMenuItem.Name = "editarToolStripMenuItem";
            this.editarToolStripMenuItem.Size = new System.Drawing.Size(49, 20);
            this.editarToolStripMenuItem.Text = "Editar";
            // 
            // volverToolStripMenuItem
            // 
            this.volverToolStripMenuItem.Name = "volverToolStripMenuItem";
            this.volverToolStripMenuItem.Size = new System.Drawing.Size(51, 20);
            this.volverToolStripMenuItem.Text = "Volver";
            // 
            // pnlEncabezado
            // 
            this.pnlEncabezado.BackColor = System.Drawing.Color.DarkSlateBlue;
            this.pnlEncabezado.Controls.Add(this.lblTitulo);
            this.pnlEncabezado.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlEncabezado.Location = new System.Drawing.Point(0, 24);
            this.pnlEncabezado.Name = "pnlEncabezado";
            this.pnlEncabezado.Size = new System.Drawing.Size(800, 70);
            this.pnlEncabezado.TabIndex = 2;
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Location = new System.Drawing.Point(48, 20);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(298, 29);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "GESTION DE FACTURAS\r\n";
            // 
            // grpDatosFactura
            // 
            this.grpDatosFactura.Controls.Add(this.cmbTipoPedido);
            this.grpDatosFactura.Controls.Add(this.dtpFechaFactura);
            this.grpDatosFactura.Controls.Add(this.txtClienteFactura);
            this.grpDatosFactura.Controls.Add(this.txtEmpleadoFactura);
            this.grpDatosFactura.Controls.Add(this.txtMesaFactura);
            this.grpDatosFactura.Controls.Add(this.txtNumeroFactura);
            this.grpDatosFactura.Controls.Add(this.lblEmpleadoFactura);
            this.grpDatosFactura.Controls.Add(this.lblMesaFactura);
            this.grpDatosFactura.Controls.Add(this.lblClienteFactura);
            this.grpDatosFactura.Controls.Add(this.lblTipoPedido);
            this.grpDatosFactura.Controls.Add(this.lblFechaFactura);
            this.grpDatosFactura.Controls.Add(this.lblNumeroFactura);
            this.grpDatosFactura.Font = new System.Drawing.Font("Arial Black", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpDatosFactura.Location = new System.Drawing.Point(12, 100);
            this.grpDatosFactura.Name = "grpDatosFactura";
            this.grpDatosFactura.Size = new System.Drawing.Size(267, 273);
            this.grpDatosFactura.TabIndex = 3;
            this.grpDatosFactura.TabStop = false;
            this.grpDatosFactura.Text = "DATOS DE LA FACTURA";
            // 
            // lblEmpleadoFactura
            // 
            this.lblEmpleadoFactura.AutoSize = true;
            this.lblEmpleadoFactura.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmpleadoFactura.Location = new System.Drawing.Point(20, 231);
            this.lblEmpleadoFactura.Name = "lblEmpleadoFactura";
            this.lblEmpleadoFactura.Size = new System.Drawing.Size(66, 15);
            this.lblEmpleadoFactura.TabIndex = 5;
            this.lblEmpleadoFactura.Text = "Empleado:";
            // 
            // lblMesaFactura
            // 
            this.lblMesaFactura.AutoSize = true;
            this.lblMesaFactura.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMesaFactura.Location = new System.Drawing.Point(20, 187);
            this.lblMesaFactura.Name = "lblMesaFactura";
            this.lblMesaFactura.Size = new System.Drawing.Size(41, 15);
            this.lblMesaFactura.TabIndex = 4;
            this.lblMesaFactura.Text = "Mesa:";
            // 
            // lblClienteFactura
            // 
            this.lblClienteFactura.AutoSize = true;
            this.lblClienteFactura.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblClienteFactura.Location = new System.Drawing.Point(20, 144);
            this.lblClienteFactura.Name = "lblClienteFactura";
            this.lblClienteFactura.Size = new System.Drawing.Size(49, 15);
            this.lblClienteFactura.TabIndex = 3;
            this.lblClienteFactura.Text = "Cliente:";
            // 
            // lblTipoPedido
            // 
            this.lblTipoPedido.AutoSize = true;
            this.lblTipoPedido.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTipoPedido.Location = new System.Drawing.Point(20, 107);
            this.lblTipoPedido.Name = "lblTipoPedido";
            this.lblTipoPedido.Size = new System.Drawing.Size(76, 15);
            this.lblTipoPedido.TabIndex = 2;
            this.lblTipoPedido.Text = "Tipo Pedido:";
            // 
            // lblFechaFactura
            // 
            this.lblFechaFactura.AutoSize = true;
            this.lblFechaFactura.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFechaFactura.Location = new System.Drawing.Point(20, 69);
            this.lblFechaFactura.Name = "lblFechaFactura";
            this.lblFechaFactura.Size = new System.Drawing.Size(47, 15);
            this.lblFechaFactura.TabIndex = 1;
            this.lblFechaFactura.Text = " Fecha:";
            // 
            // lblNumeroFactura
            // 
            this.lblNumeroFactura.AutoSize = true;
            this.lblNumeroFactura.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNumeroFactura.Location = new System.Drawing.Point(20, 33);
            this.lblNumeroFactura.Name = "lblNumeroFactura";
            this.lblNumeroFactura.Size = new System.Drawing.Size(68, 15);
            this.lblNumeroFactura.TabIndex = 0;
            this.lblNumeroFactura.Text = "Nº Factura:";
            // 
            // btnGuardarProducto
            // 
            this.btnGuardarProducto.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnGuardarProducto.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardarProducto.Font = new System.Drawing.Font("Arial Black", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardarProducto.ForeColor = System.Drawing.Color.Black;
            this.btnGuardarProducto.Location = new System.Drawing.Point(19, 379);
            this.btnGuardarProducto.Name = "btnGuardarProducto";
            this.btnGuardarProducto.Size = new System.Drawing.Size(81, 31);
            this.btnGuardarProducto.TabIndex = 4;
            this.btnGuardarProducto.Text = "Guardar";
            this.btnGuardarProducto.UseVisualStyleBackColor = false;
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnLimpiar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLimpiar.Font = new System.Drawing.Font("Arial Black", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLimpiar.ForeColor = System.Drawing.Color.Black;
            this.btnLimpiar.Location = new System.Drawing.Point(95, 379);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(81, 31);
            this.btnLimpiar.TabIndex = 5;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.UseVisualStyleBackColor = false;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.Color.Red;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Font = new System.Drawing.Font("Arial Black", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.ForeColor = System.Drawing.Color.Black;
            this.btnCancelar.Location = new System.Drawing.Point(171, 379);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(81, 31);
            this.btnCancelar.TabIndex = 6;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            // 
            // grpDetalleFactura
            // 
            this.grpDetalleFactura.Controls.Add(this.dataGridView1);
            this.grpDetalleFactura.Location = new System.Drawing.Point(285, 312);
            this.grpDetalleFactura.Name = "grpDetalleFactura";
            this.grpDetalleFactura.Size = new System.Drawing.Size(503, 100);
            this.grpDetalleFactura.TabIndex = 7;
            this.grpDetalleFactura.TabStop = false;
            this.grpDetalleFactura.Text = "Detalle de la Factura";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColProducto,
            this.ColCantidad,
            this.ColPrecioUnitario,
            this.ColSubtotal});
            this.dataGridView1.Location = new System.Drawing.Point(6, 19);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(491, 75);
            this.dataGridView1.TabIndex = 0;
            // 
            // ColProducto
            // 
            this.ColProducto.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColProducto.HeaderText = "Producto";
            this.ColProducto.Name = "ColProducto";
            // 
            // ColCantidad
            // 
            this.ColCantidad.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColCantidad.HeaderText = "Cantidad";
            this.ColCantidad.Name = "ColCantidad";
            // 
            // ColPrecioUnitario
            // 
            this.ColPrecioUnitario.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColPrecioUnitario.HeaderText = "Precio Unitario";
            this.ColPrecioUnitario.Name = "ColPrecioUnitario";
            // 
            // ColSubtotal
            // 
            this.ColSubtotal.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColSubtotal.HeaderText = "Subtotal";
            this.ColSubtotal.Name = "ColSubtotal";
            // 
            // grpInformacionAdicional
            // 
            this.grpInformacionAdicional.Controls.Add(this.lblMetodoPagoFactura);
            this.grpInformacionAdicional.Controls.Add(this.txtMetodoPago);
            this.grpInformacionAdicional.Controls.Add(this.txtTotalFactura);
            this.grpInformacionAdicional.Controls.Add(this.txtSubtotalFactura);
            this.grpInformacionAdicional.Controls.Add(this.label4);
            this.grpInformacionAdicional.Controls.Add(this.label3);
            this.grpInformacionAdicional.Controls.Add(this.txtDescuentoFactura);
            this.grpInformacionAdicional.Controls.Add(this.txtImpuestoFactura);
            this.grpInformacionAdicional.Controls.Add(this.lblImpuestos);
            this.grpInformacionAdicional.Controls.Add(this.lblSubtotal);
            this.grpInformacionAdicional.Font = new System.Drawing.Font("Arial Black", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpInformacionAdicional.Location = new System.Drawing.Point(305, 100);
            this.grpInformacionAdicional.Name = "grpInformacionAdicional";
            this.grpInformacionAdicional.Size = new System.Drawing.Size(248, 202);
            this.grpInformacionAdicional.TabIndex = 8;
            this.grpInformacionAdicional.TabStop = false;
            this.grpInformacionAdicional.Text = "Información Adicional";
            // 
            // lblSubtotal
            // 
            this.lblSubtotal.AutoSize = true;
            this.lblSubtotal.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubtotal.Location = new System.Drawing.Point(12, 33);
            this.lblSubtotal.Name = "lblSubtotal";
            this.lblSubtotal.Size = new System.Drawing.Size(57, 15);
            this.lblSubtotal.TabIndex = 6;
            this.lblSubtotal.Text = "Subtotal:";
            // 
            // lblImpuestos
            // 
            this.lblImpuestos.AutoSize = true;
            this.lblImpuestos.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblImpuestos.Location = new System.Drawing.Point(12, 73);
            this.lblImpuestos.Name = "lblImpuestos";
            this.lblImpuestos.Size = new System.Drawing.Size(101, 15);
            this.lblImpuestos.TabIndex = 7;
            this.lblImpuestos.Text = "Impuestos(15%):";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 107);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 15);
            this.label3.TabIndex = 8;
            this.label3.Text = "Descuento:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(12, 173);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 15);
            this.label4.TabIndex = 9;
            this.label4.Text = "TOTAL:";
            // 
            // txtNumeroFactura
            // 
            this.txtNumeroFactura.Location = new System.Drawing.Point(99, 29);
            this.txtNumeroFactura.Name = "txtNumeroFactura";
            this.txtNumeroFactura.Size = new System.Drawing.Size(100, 24);
            this.txtNumeroFactura.TabIndex = 6;
            // 
            // txtDescuentoFactura
            // 
            this.txtDescuentoFactura.Location = new System.Drawing.Point(119, 103);
            this.txtDescuentoFactura.Name = "txtDescuentoFactura";
            this.txtDescuentoFactura.Size = new System.Drawing.Size(100, 24);
            this.txtDescuentoFactura.TabIndex = 7;
            // 
            // txtImpuestoFactura
            // 
            this.txtImpuestoFactura.Location = new System.Drawing.Point(119, 65);
            this.txtImpuestoFactura.Name = "txtImpuestoFactura";
            this.txtImpuestoFactura.Size = new System.Drawing.Size(100, 24);
            this.txtImpuestoFactura.TabIndex = 8;
            // 
            // txtMesaFactura
            // 
            this.txtMesaFactura.Location = new System.Drawing.Point(99, 183);
            this.txtMesaFactura.Name = "txtMesaFactura";
            this.txtMesaFactura.Size = new System.Drawing.Size(100, 24);
            this.txtMesaFactura.TabIndex = 9;
            // 
            // txtEmpleadoFactura
            // 
            this.txtEmpleadoFactura.Location = new System.Drawing.Point(99, 222);
            this.txtEmpleadoFactura.Name = "txtEmpleadoFactura";
            this.txtEmpleadoFactura.Size = new System.Drawing.Size(100, 24);
            this.txtEmpleadoFactura.TabIndex = 10;
            // 
            // txtClienteFactura
            // 
            this.txtClienteFactura.Location = new System.Drawing.Point(99, 140);
            this.txtClienteFactura.Name = "txtClienteFactura";
            this.txtClienteFactura.Size = new System.Drawing.Size(100, 24);
            this.txtClienteFactura.TabIndex = 11;
            // 
            // txtSubtotalFactura
            // 
            this.txtSubtotalFactura.Location = new System.Drawing.Point(119, 29);
            this.txtSubtotalFactura.Name = "txtSubtotalFactura";
            this.txtSubtotalFactura.Size = new System.Drawing.Size(100, 24);
            this.txtSubtotalFactura.TabIndex = 10;
            // 
            // txtTotalFactura
            // 
            this.txtTotalFactura.Location = new System.Drawing.Point(119, 169);
            this.txtTotalFactura.Name = "txtTotalFactura";
            this.txtTotalFactura.Size = new System.Drawing.Size(100, 24);
            this.txtTotalFactura.TabIndex = 11;
            // 
            // txtMetodoPago
            // 
            this.txtMetodoPago.Location = new System.Drawing.Point(119, 136);
            this.txtMetodoPago.Name = "txtMetodoPago";
            this.txtMetodoPago.Size = new System.Drawing.Size(100, 24);
            this.txtMetodoPago.TabIndex = 12;
            // 
            // lblMetodoPagoFactura
            // 
            this.lblMetodoPagoFactura.AutoSize = true;
            this.lblMetodoPagoFactura.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMetodoPagoFactura.Location = new System.Drawing.Point(12, 140);
            this.lblMetodoPagoFactura.Name = "lblMetodoPagoFactura";
            this.lblMetodoPagoFactura.Size = new System.Drawing.Size(101, 15);
            this.lblMetodoPagoFactura.TabIndex = 13;
            this.lblMetodoPagoFactura.Text = "Metodo de Pago:";
            // 
            // txtBuscarFactura
            // 
            this.txtBuscarFactura.Location = new System.Drawing.Point(559, 147);
            this.txtBuscarFactura.Name = "txtBuscarFactura";
            this.txtBuscarFactura.Size = new System.Drawing.Size(231, 20);
            this.txtBuscarFactura.TabIndex = 11;
            // 
            // lblBuscarFactura
            // 
            this.lblBuscarFactura.AutoSize = true;
            this.lblBuscarFactura.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBuscarFactura.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblBuscarFactura.Location = new System.Drawing.Point(559, 115);
            this.lblBuscarFactura.Name = "lblBuscarFactura";
            this.lblBuscarFactura.Size = new System.Drawing.Size(48, 14);
            this.lblBuscarFactura.TabIndex = 12;
            this.lblBuscarFactura.Text = "Buscar:";
            // 
            // btnBuscarFactura
            // 
            this.btnBuscarFactura.BackColor = System.Drawing.Color.Cyan;
            this.btnBuscarFactura.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscarFactura.Font = new System.Drawing.Font("Arial Black", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscarFactura.ForeColor = System.Drawing.Color.Black;
            this.btnBuscarFactura.Location = new System.Drawing.Point(637, 193);
            this.btnBuscarFactura.Name = "btnBuscarFactura";
            this.btnBuscarFactura.Size = new System.Drawing.Size(67, 29);
            this.btnBuscarFactura.TabIndex = 13;
            this.btnBuscarFactura.Text = "Buscar";
            this.btnBuscarFactura.UseVisualStyleBackColor = false;
            // 
            // dtpFechaFactura
            // 
            this.dtpFechaFactura.Location = new System.Drawing.Point(99, 63);
            this.dtpFechaFactura.Name = "dtpFechaFactura";
            this.dtpFechaFactura.Size = new System.Drawing.Size(100, 24);
            this.dtpFechaFactura.TabIndex = 12;
            // 
            // cmbTipoPedido
            // 
            this.cmbTipoPedido.FormattingEnabled = true;
            this.cmbTipoPedido.Location = new System.Drawing.Point(99, 102);
            this.cmbTipoPedido.Name = "cmbTipoPedido";
            this.cmbTipoPedido.Size = new System.Drawing.Size(96, 25);
            this.cmbTipoPedido.TabIndex = 13;
            // 
            // FrmFacturas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnBuscarFactura);
            this.Controls.Add(this.lblBuscarFactura);
            this.Controls.Add(this.txtBuscarFactura);
            this.Controls.Add(this.grpInformacionAdicional);
            this.Controls.Add(this.grpDetalleFactura);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnLimpiar);
            this.Controls.Add(this.btnGuardarProducto);
            this.Controls.Add(this.grpDatosFactura);
            this.Controls.Add(this.pnlEncabezado);
            this.Controls.Add(this.menuStrip1);
            this.Name = "FrmFacturas";
            this.Text = "GESTION DE FACTURAS";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.pnlEncabezado.ResumeLayout(false);
            this.pnlEncabezado.PerformLayout();
            this.grpDatosFactura.ResumeLayout(false);
            this.grpDatosFactura.PerformLayout();
            this.grpDetalleFactura.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.grpInformacionAdicional.ResumeLayout(false);
            this.grpInformacionAdicional.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem agregarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem eliminarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem volverToolStripMenuItem;
        private System.Windows.Forms.Panel pnlEncabezado;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.GroupBox grpDatosFactura;
        private System.Windows.Forms.Button btnGuardarProducto;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.GroupBox grpDetalleFactura;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColProducto;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColCantidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColPrecioUnitario;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColSubtotal;
        private System.Windows.Forms.Label lblNumeroFactura;
        private System.Windows.Forms.Label lblClienteFactura;
        private System.Windows.Forms.Label lblTipoPedido;
        private System.Windows.Forms.Label lblFechaFactura;
        private System.Windows.Forms.Label lblEmpleadoFactura;
        private System.Windows.Forms.Label lblMesaFactura;
        private System.Windows.Forms.GroupBox grpInformacionAdicional;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblImpuestos;
        private System.Windows.Forms.Label lblSubtotal;
        private System.Windows.Forms.TextBox txtClienteFactura;
        private System.Windows.Forms.TextBox txtEmpleadoFactura;
        private System.Windows.Forms.TextBox txtMesaFactura;
        private System.Windows.Forms.TextBox txtImpuestoFactura;
        private System.Windows.Forms.TextBox txtDescuentoFactura;
        private System.Windows.Forms.TextBox txtNumeroFactura;
        private System.Windows.Forms.Label lblMetodoPagoFactura;
        private System.Windows.Forms.TextBox txtMetodoPago;
        private System.Windows.Forms.TextBox txtTotalFactura;
        private System.Windows.Forms.TextBox txtSubtotalFactura;
        private System.Windows.Forms.TextBox txtBuscarFactura;
        private System.Windows.Forms.Label lblBuscarFactura;
        private System.Windows.Forms.Button btnBuscarFactura;
        private System.Windows.Forms.ComboBox cmbTipoPedido;
        private System.Windows.Forms.DateTimePicker dtpFechaFactura;
    }
}