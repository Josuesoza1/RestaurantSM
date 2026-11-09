namespace SistemaPedidoRestaurante.UICompartida
{
    partial class FrmPagoBase
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblTituloFactura = new System.Windows.Forms.Label();
            this.lblMetodoPago = new System.Windows.Forms.Label();
            this.cmbTipoPedido = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtNotas = new System.Windows.Forms.TextBox();
            this.lblNotas = new System.Windows.Forms.Label();
            this.txtMontoTotal = new System.Windows.Forms.TextBox();
            this.lblMontoTotal = new System.Windows.Forms.Label();
            this.lblNombreComprobante = new System.Windows.Forms.Label();
            this.txtNombreComprobante = new System.Windows.Forms.TextBox();
            this.btnRegistrarPago = new System.Windows.Forms.Button();
            this.btnVolverPagos = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.DarkSlateBlue;
            this.panel1.Controls.Add(this.lblTituloFactura);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(534, 82);
            this.panel1.TabIndex = 0;
            // 
            // lblTituloFactura
            // 
            this.lblTituloFactura.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblTituloFactura.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTituloFactura.Font = new System.Drawing.Font("Arial Black", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTituloFactura.ForeColor = System.Drawing.SystemColors.Control;
            this.lblTituloFactura.Location = new System.Drawing.Point(0, 0);
            this.lblTituloFactura.Name = "lblTituloFactura";
            this.lblTituloFactura.Size = new System.Drawing.Size(534, 82);
            this.lblTituloFactura.TabIndex = 1;
            this.lblTituloFactura.Text = "COBRO";
            this.lblTituloFactura.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblMetodoPago
            // 
            this.lblMetodoPago.AutoSize = true;
            this.lblMetodoPago.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMetodoPago.Location = new System.Drawing.Point(24, 110);
            this.lblMetodoPago.Name = "lblMetodoPago";
            this.lblMetodoPago.Size = new System.Drawing.Size(92, 15);
            this.lblMetodoPago.TabIndex = 14;
            this.lblMetodoPago.Text = "Forma de pago:";
            // 
            // cmbTipoPedido
            // 
            this.cmbTipoPedido.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTipoPedido.AccessibleName = "Forma de pago";
            this.cmbTipoPedido.FormattingEnabled = true;
            this.cmbTipoPedido.Items.AddRange(new object[] {
            "Efectivo",
            "Tarjeta",
            "Transferencia"});
            this.cmbTipoPedido.Location = new System.Drawing.Point(131, 108);
            this.cmbTipoPedido.Name = "cmbTipoPedido";
            this.cmbTipoPedido.Size = new System.Drawing.Size(362, 21);
            this.cmbTipoPedido.TabIndex = 15;
            this.cmbTipoPedido.SelectedIndexChanged += new System.EventHandler(this.cmbTipoPedido_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtNotas);
            this.groupBox1.Controls.Add(this.lblNotas);
            this.groupBox1.Controls.Add(this.txtMontoTotal);
            this.groupBox1.Controls.Add(this.lblMontoTotal);
            this.groupBox1.Controls.Add(this.lblNombreComprobante);
            this.groupBox1.Controls.Add(this.txtNombreComprobante);
            this.groupBox1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(27, 152);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(481, 190);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Información del cobro";
            // 
            // txtNotas
            // 
            this.txtNotas.Location = new System.Drawing.Point(86, 134);
            this.txtNotas.AccessibleName = "Notas del cobro";
            this.txtNotas.MaxLength = 200;
            this.txtNotas.Multiline = true;
            this.txtNotas.Name = "txtNotas";
            this.txtNotas.Size = new System.Drawing.Size(365, 44);
            this.txtNotas.TabIndex = 18;
            // 
            // lblNotas
            // 
            this.lblNotas.AutoSize = true;
            this.lblNotas.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNotas.Location = new System.Drawing.Point(12, 137);
            this.lblNotas.Name = "lblNotas";
            this.lblNotas.Size = new System.Drawing.Size(43, 15);
            this.lblNotas.TabIndex = 17;
            this.lblNotas.Text = "Notas:";
            // 
            // txtMontoTotal
            // 
            this.txtMontoTotal.Location = new System.Drawing.Point(106, 41);
            this.txtMontoTotal.Name = "txtMontoTotal";
            this.txtMontoTotal.ReadOnly = true;
            this.txtMontoTotal.AccessibleName = "Total de la venta";
            this.txtMontoTotal.TabStop = false;
            this.txtMontoTotal.Size = new System.Drawing.Size(221, 21);
            this.txtMontoTotal.TabIndex = 16;
            // 
            // lblMontoTotal
            // 
            this.lblMontoTotal.AutoSize = true;
            this.lblMontoTotal.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMontoTotal.Location = new System.Drawing.Point(12, 44);
            this.lblMontoTotal.Name = "lblMontoTotal";
            this.lblMontoTotal.Size = new System.Drawing.Size(75, 15);
            this.lblMontoTotal.TabIndex = 15;
            this.lblMontoTotal.Text = "Monto Total:";
            // 
            // lblNombreComprobante
            // 
            this.lblNombreComprobante.AutoSize = true;
            this.lblNombreComprobante.Location = new System.Drawing.Point(12, 78);
            this.lblNombreComprobante.Name = "lblNombreComprobante";
            this.lblNombreComprobante.Size = new System.Drawing.Size(232, 15);
            this.lblNombreComprobante.TabIndex = 19;
            this.lblNombreComprobante.Text = "Nombre para el comprobante (opcional)";
            // 
            // txtNombreComprobante
            // 
            this.txtNombreComprobante.AccessibleName = "Nombre para el comprobante (opcional)";
            this.txtNombreComprobante.Enabled = false;
            this.txtNombreComprobante.Location = new System.Drawing.Point(15, 98);
            this.txtNombreComprobante.MaxLength = 120;
            this.txtNombreComprobante.Name = "txtNombreComprobante";
            this.txtNombreComprobante.Size = new System.Drawing.Size(436, 21);
            this.txtNombreComprobante.TabIndex = 17;
            // 
            // btnRegistrarPago
            // 
            this.btnRegistrarPago.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnRegistrarPago.Enabled = false;
            this.btnRegistrarPago.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRegistrarPago.Font = new System.Drawing.Font("Arial Black", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRegistrarPago.ForeColor = System.Drawing.Color.Black;
            this.btnRegistrarPago.Location = new System.Drawing.Point(125, 356);
            this.btnRegistrarPago.Name = "btnRegistrarPago";
            this.btnRegistrarPago.Size = new System.Drawing.Size(158, 31);
            this.btnRegistrarPago.TabIndex = 17;
            this.btnRegistrarPago.Text = "Revisar pago";
            this.btnRegistrarPago.UseVisualStyleBackColor = false;
            this.btnRegistrarPago.Click += new System.EventHandler(this.btnRegistrarPago_Click);
            // 
            // btnVolverPagos
            // 
            this.btnVolverPagos.BackColor = System.Drawing.Color.Gainsboro;
            this.btnVolverPagos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVolverPagos.Font = new System.Drawing.Font("Arial Black", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVolverPagos.ForeColor = System.Drawing.Color.Black;
            this.btnVolverPagos.Location = new System.Drawing.Point(303, 355);
            this.btnVolverPagos.Name = "btnVolverPagos";
            this.btnVolverPagos.Size = new System.Drawing.Size(102, 32);
            this.btnVolverPagos.TabIndex = 18;
            this.btnVolverPagos.Text = "Volver";
            this.btnVolverPagos.UseVisualStyleBackColor = false;
            this.btnVolverPagos.Click += new System.EventHandler(this.btnVolver_Click);
            // 
            // FrmPagoBase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.ClientSize = new System.Drawing.Size(534, 399);
            this.CancelButton = this.btnVolverPagos;
            this.Controls.Add(this.btnVolverPagos);
            this.Controls.Add(this.btnRegistrarPago);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cmbTipoPedido);
            this.Controls.Add(this.lblMetodoPago);
            this.Controls.Add(this.panel1);
            this.MinimumSize = new System.Drawing.Size(550, 438);
            this.Name = "FrmPagoBase";
            this.Text = "GESTION DE PAGO";
            this.Load += new System.EventHandler(this.FrmPagoBase_Load);
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        protected System.Windows.Forms.Panel panel1;
        protected System.Windows.Forms.Label lblTituloFactura;
        protected System.Windows.Forms.Label lblMetodoPago;
        protected System.Windows.Forms.ComboBox cmbTipoPedido;
        protected System.Windows.Forms.GroupBox groupBox1;
        protected System.Windows.Forms.TextBox txtNotas;
        protected System.Windows.Forms.Label lblNotas;
        private System.Windows.Forms.Label lblNombreComprobante;
        private System.Windows.Forms.TextBox txtNombreComprobante;
        protected System.Windows.Forms.TextBox txtMontoTotal;
        protected System.Windows.Forms.Label lblMontoTotal;
        protected System.Windows.Forms.Button btnRegistrarPago;
        protected System.Windows.Forms.Button btnVolverPagos;
    }
}
