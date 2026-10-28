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
            this.components = new System.ComponentModel.Container();
            this.panel = new System.Windows.Forms.TableLayoutPanel();
            this.resumen = new System.Windows.Forms.Label();
            this.lblRecibido = new System.Windows.Forms.Label();
            this.monto = new System.Windows.Forms.NumericUpDown();
            this.cambio = new System.Windows.Forms.Label();
            this.botones = new System.Windows.Forms.FlowLayoutPanel();
            this.confirmar = new System.Windows.Forms.Button();
            this.cancelar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            this.panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.monto)).BeginInit();
            this.botones.SuspendLayout();
            this.panel.Name = "panel";
            this.resumen.Name = "resumen";
            this.lblRecibido.Name = "lblRecibido";
            this.monto.Name = "monto";
            this.cambio.Name = "cambio";
            this.botones.Name = "botones";
            this.confirmar.Name = "confirmar";
            this.cancelar.Name = "cancelar";
            this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel.Padding = new System.Windows.Forms.Padding(20);
            this.panel.ColumnCount = 1;
            this.panel.RowCount = 5;
            this.resumen.Text = "Efectivo — Total: C$ 0.00";
            this.resumen.AutoSize = true;
            this.lblRecibido.Text = "Monto recibido";
            this.lblRecibido.AutoSize = true;
            this.monto.DecimalPlaces = 2;
            this.monto.Maximum = new decimal(new int[] { 1410065407, 2, 0, 131072 });
            this.monto.Dock = System.Windows.Forms.DockStyle.Top;
            this.monto.ValueChanged += new System.EventHandler(this.monto_ValueChanged);
            this.cambio.AutoSize = true;
            this.cambio.Text = "Cambio: C$ 0.00";
            this.confirmar.Text = "Confirmar cobro";
            this.confirmar.AutoSize = true;
            this.confirmar.Click += new System.EventHandler(this.confirmar_Click);
            this.cancelar.Text = "Volver";
            this.cancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelar.Click += new System.EventHandler(this.cancelar_Click);
            this.botones.AutoSize = true;
            this.botones.Controls.Add(this.confirmar);
            this.botones.Controls.Add(this.cancelar);
            this.panel.Controls.Add(this.resumen, 0, 0);
            this.panel.Controls.Add(this.lblRecibido, 0, 1);
            this.panel.Controls.Add(this.monto, 0, 2);
            this.panel.Controls.Add(this.cambio, 0, 3);
            this.panel.Controls.Add(this.botones, 0, 4);
            this.Controls.Add(this.panel);
            this.AcceptButton = this.confirmar;
            this.CancelButton = this.cancelar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(420, 250);
            this.MinimumSize = new System.Drawing.Size(436, 289);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Name = "FrmConfirmarCobro";
            this.Text = "Confirmar cobro";
            this.panel.ResumeLayout(false);
            this.panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.monto)).EndInit();
            this.botones.ResumeLayout(false);
            this.botones.PerformLayout();
            this.ResumeLayout(false);
        }
        #endregion

        private System.Windows.Forms.TableLayoutPanel panel;
        private System.Windows.Forms.Label resumen;
        private System.Windows.Forms.Label lblRecibido;
        private System.Windows.Forms.NumericUpDown monto;
        private System.Windows.Forms.Label cambio;
        private System.Windows.Forms.FlowLayoutPanel botones;
        private System.Windows.Forms.Button confirmar;
        private System.Windows.Forms.Button cancelar;
    }
}
