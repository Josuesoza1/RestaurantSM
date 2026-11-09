namespace SistemaPedidoRestaurante.UICompartida
{
    partial class FrmConfirmarCobro
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        #region Codigo generado por el disenador de Windows Forms
        private void InitializeComponent()
        {
            this.panel = new System.Windows.Forms.TableLayoutPanel();
            this.resumen = new System.Windows.Forms.Label();
            this.lblRecibido = new System.Windows.Forms.Label();
            this.monto = new System.Windows.Forms.TextBox();
            this.cambio = new System.Windows.Forms.Label();
            this.botones = new System.Windows.Forms.FlowLayoutPanel();
            this.pagoExacto = new System.Windows.Forms.Button();
            this.confirmar = new System.Windows.Forms.Button();
            this.cancelar = new System.Windows.Forms.Button();
            this.panel.SuspendLayout();
            this.botones.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel
            // 
            this.panel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.panel.ColumnCount = 1;
            this.panel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.panel.Controls.Add(this.resumen, 0, 0);
            this.panel.Controls.Add(this.lblRecibido, 0, 1);
            this.panel.Controls.Add(this.monto, 0, 2);
            this.panel.Controls.Add(this.cambio, 0, 3);
            this.panel.Controls.Add(this.botones, 0, 4);
            this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel.Location = new System.Drawing.Point(0, 0);
            this.panel.Name = "panel";
            this.panel.Padding = new System.Windows.Forms.Padding(20);
            this.panel.RowCount = 5;
            this.panel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.panel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.panel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.panel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.panel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.panel.Size = new System.Drawing.Size(420, 250);
            this.panel.TabIndex = 0;
            // 
            // resumen
            // 
            this.resumen.AutoSize = true;
            this.resumen.Location = new System.Drawing.Point(23, 20);
            this.resumen.Name = "resumen";
            this.resumen.Size = new System.Drawing.Size(125, 13);
            this.resumen.TabIndex = 0;
            this.resumen.Text = "Efectivo — Total: C$ 0.00";
            // 
            // lblRecibido
            // 
            this.lblRecibido.AutoSize = true;
            this.lblRecibido.Location = new System.Drawing.Point(23, 40);
            this.lblRecibido.Name = "lblRecibido";
            this.lblRecibido.Size = new System.Drawing.Size(77, 13);
            this.lblRecibido.TabIndex = 1;
            this.lblRecibido.Text = "Monto recibido";
            // 
            // monto
            // 
            this.monto.Dock = System.Windows.Forms.DockStyle.Top;
            this.monto.Location = new System.Drawing.Point(23, 63);
            this.monto.Name = "monto";
            this.monto.Size = new System.Drawing.Size(374, 20);
            this.monto.TabIndex = 0;
            this.monto.TextChanged += new System.EventHandler(this.monto_TextChanged);
            // 
            // cambio
            // 
            this.cambio.AutoSize = true;
            this.cambio.Location = new System.Drawing.Point(23, 80);
            this.cambio.Name = "cambio";
            this.cambio.Size = new System.Drawing.Size(221, 13);
            this.cambio.TabIndex = 2;
            this.cambio.Text = "Ingrese el monto recibido o elija Pago exacto.";
            // 
            // botones
            // 
            this.botones.AutoSize = true;
            this.botones.Controls.Add(this.pagoExacto);
            this.botones.Controls.Add(this.confirmar);
            this.botones.Controls.Add(this.cancelar);
            this.botones.Location = new System.Drawing.Point(23, 103);
            this.botones.Name = "botones";
            this.botones.Size = new System.Drawing.Size(261, 29);
            this.botones.TabIndex = 1;
            // 
            // pagoExacto
            // 
            this.pagoExacto.AutoSize = true;
            this.pagoExacto.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.pagoExacto.Location = new System.Drawing.Point(3, 3);
            this.pagoExacto.Name = "pagoExacto";
            this.pagoExacto.Size = new System.Drawing.Size(77, 23);
            this.pagoExacto.TabIndex = 0;
            this.pagoExacto.Text = "Pago exacto";
            this.pagoExacto.UseVisualStyleBackColor = false;
            this.pagoExacto.Click += new System.EventHandler(this.pagoExacto_Click);
            // 
            // confirmar
            // 
            this.confirmar.AutoSize = true;
            this.confirmar.BackColor = System.Drawing.Color.Lime;
            this.confirmar.Enabled = false;
            this.confirmar.Location = new System.Drawing.Point(86, 3);
            this.confirmar.Name = "confirmar";
            this.confirmar.Size = new System.Drawing.Size(91, 23);
            this.confirmar.TabIndex = 1;
            this.confirmar.Text = "Confirmar cobro";
            this.confirmar.UseVisualStyleBackColor = false;
            this.confirmar.Click += new System.EventHandler(this.confirmar_Click);
            // 
            // cancelar
            // 
            this.cancelar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.cancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelar.Location = new System.Drawing.Point(183, 3);
            this.cancelar.Name = "cancelar";
            this.cancelar.Size = new System.Drawing.Size(75, 23);
            this.cancelar.TabIndex = 2;
            this.cancelar.Text = "Volver";
            this.cancelar.UseVisualStyleBackColor = false;
            this.cancelar.Click += new System.EventHandler(this.cancelar_Click);
            // 
            // FrmConfirmarCobro
            // 
            this.AcceptButton = this.confirmar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelar;
            this.ClientSize = new System.Drawing.Size(420, 250);
            this.Controls.Add(this.panel);
            this.MinimumSize = new System.Drawing.Size(436, 289);
            this.Name = "FrmConfirmarCobro";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Confirmar cobro";
            this.panel.ResumeLayout(false);
            this.panel.PerformLayout();
            this.botones.ResumeLayout(false);
            this.botones.PerformLayout();
            this.ResumeLayout(false);

        }
        #endregion

        private System.Windows.Forms.TableLayoutPanel panel;
        private System.Windows.Forms.Label resumen;
        private System.Windows.Forms.Label lblRecibido;
        private System.Windows.Forms.TextBox monto;
        private System.Windows.Forms.Label cambio;
        private System.Windows.Forms.FlowLayoutPanel botones;
        private System.Windows.Forms.Button confirmar;
        private System.Windows.Forms.Button pagoExacto;
        private System.Windows.Forms.Button cancelar;
    }
}
