namespace SistemaPedidoRestaurante.UIEmpleado
{
    partial class FrmPago
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblTituloFactura = new System.Windows.Forms.Label();
            this.lblMetodoPago = new System.Windows.Forms.Label();
            this.cmbTipoPedido = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblMontoTotal = new System.Windows.Forms.Label();
            this.txtMontoTotal = new System.Windows.Forms.TextBox();
            this.lblNotas = new System.Windows.Forms.Label();
            this.txtNotas = new System.Windows.Forms.TextBox();
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
            this.lblTituloFactura.AutoSize = true;
            this.lblTituloFactura.Font = new System.Drawing.Font("Arial Black", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTituloFactura.ForeColor = System.Drawing.SystemColors.Control;
            this.lblTituloFactura.Location = new System.Drawing.Point(127, 28);
            this.lblTituloFactura.Name = "lblTituloFactura";
            this.lblTituloFactura.Size = new System.Drawing.Size(278, 33);
            this.lblTituloFactura.TabIndex = 1;
            this.lblTituloFactura.Text = "GESTION DE PAGOS";
            // 
            // lblMetodoPago
            // 
            this.lblMetodoPago.AutoSize = true;
            this.lblMetodoPago.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMetodoPago.Location = new System.Drawing.Point(24, 110);
            this.lblMetodoPago.Name = "lblMetodoPago";
            this.lblMetodoPago.Size = new System.Drawing.Size(101, 15);
            this.lblMetodoPago.TabIndex = 14;
            this.lblMetodoPago.Text = "Metodo de Pago:";
            // 
            // cmbTipoPedido
            // 
            this.cmbTipoPedido.FormattingEnabled = true;
            this.cmbTipoPedido.Location = new System.Drawing.Point(131, 108);
            this.cmbTipoPedido.Name = "cmbTipoPedido";
            this.cmbTipoPedido.Size = new System.Drawing.Size(362, 21);
            this.cmbTipoPedido.TabIndex = 15;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtNotas);
            this.groupBox1.Controls.Add(this.lblNotas);
            this.groupBox1.Controls.Add(this.txtMontoTotal);
            this.groupBox1.Controls.Add(this.lblMontoTotal);
            this.groupBox1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(27, 152);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(481, 190);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "INFROMACION DE PAGO";
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
            // txtMontoTotal
            // 
            this.txtMontoTotal.Location = new System.Drawing.Point(106, 41);
            this.txtMontoTotal.Name = "txtMontoTotal";
            this.txtMontoTotal.Size = new System.Drawing.Size(221, 21);
            this.txtMontoTotal.TabIndex = 16;
            // 
            // lblNotas
            // 
            this.lblNotas.AutoSize = true;
            this.lblNotas.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNotas.Location = new System.Drawing.Point(12, 96);
            this.lblNotas.Name = "lblNotas";
            this.lblNotas.Size = new System.Drawing.Size(43, 15);
            this.lblNotas.TabIndex = 17;
            this.lblNotas.Text = "Notas:";
            // 
            // txtNotas
            // 
            this.txtNotas.Location = new System.Drawing.Point(86, 96);
            this.txtNotas.Multiline = true;
            this.txtNotas.Name = "txtNotas";
            this.txtNotas.Size = new System.Drawing.Size(315, 72);
            this.txtNotas.TabIndex = 18;
            // 
            // btnRegistrarPago
            // 
            this.btnRegistrarPago.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnRegistrarPago.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRegistrarPago.Font = new System.Drawing.Font("Arial Black", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRegistrarPago.ForeColor = System.Drawing.Color.Black;
            this.btnRegistrarPago.Location = new System.Drawing.Point(142, 356);
            this.btnRegistrarPago.Name = "btnRegistrarPago";
            this.btnRegistrarPago.Size = new System.Drawing.Size(133, 31);
            this.btnRegistrarPago.TabIndex = 17;
            this.btnRegistrarPago.Text = "Registrar Pago";
            this.btnRegistrarPago.UseVisualStyleBackColor = false;
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
            // 
            // FrmPago
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(534, 399);
            this.Controls.Add(this.btnVolverPagos);
            this.Controls.Add(this.btnRegistrarPago);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cmbTipoPedido);
            this.Controls.Add(this.lblMetodoPago);
            this.Controls.Add(this.panel1);
            this.Name = "FrmPago";
            this.Text = "GESTION DE PAGO";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblTituloFactura;
        private System.Windows.Forms.Label lblMetodoPago;
        private System.Windows.Forms.ComboBox cmbTipoPedido;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtNotas;
        private System.Windows.Forms.Label lblNotas;
        private System.Windows.Forms.TextBox txtMontoTotal;
        private System.Windows.Forms.Label lblMontoTotal;
        private System.Windows.Forms.Button btnRegistrarPago;
        private System.Windows.Forms.Button btnVolverPagos;
    }
}