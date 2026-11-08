namespace SistemaPedidoRestaurante.UICompartida
{
    partial class FrmAnularVenta
    {
        private System.Windows.Forms.Label resumen;
        private System.Windows.Forms.Label etiquetaMotivo;
        private System.Windows.Forms.TextBox motivo;
        private System.Windows.Forms.CheckBox devolucionConfirmada;
        private System.Windows.Forms.CheckBox reponer;
        private System.Windows.Forms.Button registrar;
        private System.Windows.Forms.Button volver;

        private void InitializeComponent()
        {
            this.resumen = new System.Windows.Forms.Label();
            this.etiquetaMotivo = new System.Windows.Forms.Label();
            this.motivo = new System.Windows.Forms.TextBox();
            this.devolucionConfirmada = new System.Windows.Forms.CheckBox();
            this.reponer = new System.Windows.Forms.CheckBox();
            this.registrar = new System.Windows.Forms.Button();
            this.volver = new System.Windows.Forms.Button();
            this.SuspendLayout();
            this.resumen.Location = new System.Drawing.Point(15, 15);
            this.resumen.Name = "resumen";
            this.resumen.Size = new System.Drawing.Size(430, 24);
            this.resumen.Text = "Importe a devolver";
            this.etiquetaMotivo.AutoSize = true;
            this.etiquetaMotivo.Location = new System.Drawing.Point(15, 45);
            this.etiquetaMotivo.Name = "etiquetaMotivo";
            this.etiquetaMotivo.Text = "Motivo";
            this.motivo.Location = new System.Drawing.Point(15, 67);
            this.motivo.MaxLength = 200;
            this.motivo.Multiline = true;
            this.motivo.Name = "motivo";
            this.motivo.Size = new System.Drawing.Size(430, 53);
            this.motivo.TabIndex = 0;
            this.devolucionConfirmada.AutoSize = true;
            this.devolucionConfirmada.Location = new System.Drawing.Point(15, 128);
            this.devolucionConfirmada.Name = "devolucionConfirmada";
            this.devolucionConfirmada.TabIndex = 1;
            this.devolucionConfirmada.Text = "Ya devolví el importe por el medio correspondiente";
            this.reponer.AutoSize = true;
            this.reponer.Location = new System.Drawing.Point(15, 154);
            this.reponer.Name = "reponer";
            this.reponer.TabIndex = 2;
            this.reponer.Text = "Todos los productos regresaron al inventario";
            this.registrar.Location = new System.Drawing.Point(202, 190);
            this.registrar.Name = "registrar";
            this.registrar.Size = new System.Drawing.Size(140, 30);
            this.registrar.TabIndex = 3;
            this.registrar.Text = "Registrar anulación";
            this.registrar.Click += new System.EventHandler(this.Registrar_Click);
            this.volver.Location = new System.Drawing.Point(350, 190);
            this.volver.Name = "volver";
            this.volver.Size = new System.Drawing.Size(95, 30);
            this.volver.TabIndex = 4;
            this.volver.Text = "Volver";
            this.volver.Click += new System.EventHandler(this.Volver_Click);
            this.CancelButton = this.volver;
            this.ClientSize = new System.Drawing.Size(460, 234);
            this.Controls.AddRange(new System.Windows.Forms.Control[] { this.resumen, this.etiquetaMotivo, this.motivo,
                this.devolucionConfirmada, this.reponer, this.registrar, this.volver });
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmAnularVenta";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Anular venta";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
