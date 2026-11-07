namespace SistemaPedidoRestaurante.UIPrincipal
{
    partial class FrmVistaPrevia
    {
        private System.Windows.Forms.Panel panelPagina;
        private System.Windows.Forms.PictureBox foto;
        private System.Windows.Forms.FlowLayoutPanel acciones;
        private System.Windows.Forms.Label lblPagina;
        private System.Windows.Forms.Button btnAnterior;
        private System.Windows.Forms.Button btnSiguiente;
        private System.Windows.Forms.CheckBox chkAmpliar;
        private System.Windows.Forms.Button btnPdf;
        private System.Windows.Forms.Button btnExcel;
        private System.Windows.Forms.Button btnImprimir;
        private System.Windows.Forms.Button btnCerrar;

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                foto?.Image?.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.panelPagina = new System.Windows.Forms.Panel();
            this.foto = new System.Windows.Forms.PictureBox();
            this.acciones = new System.Windows.Forms.FlowLayoutPanel();
            this.lblPagina = new System.Windows.Forms.Label();
            this.btnAnterior = new System.Windows.Forms.Button();
            this.btnSiguiente = new System.Windows.Forms.Button();
            this.chkAmpliar = new System.Windows.Forms.CheckBox();
            this.btnPdf = new System.Windows.Forms.Button();
            this.btnExcel = new System.Windows.Forms.Button();
            this.btnImprimir = new System.Windows.Forms.Button();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            this.panelPagina.AutoScroll = true;
            this.panelPagina.BackColor = System.Drawing.Color.Gainsboro;
            this.panelPagina.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelPagina.Name = "panelPagina";
            this.panelPagina.Controls.Add(this.foto);
            this.foto.Dock = System.Windows.Forms.DockStyle.Fill;
            this.foto.Name = "foto";
            this.foto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.acciones.AutoSize = true;
            this.acciones.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.acciones.Name = "acciones";
            this.acciones.Padding = new System.Windows.Forms.Padding(8);
            this.acciones.Controls.AddRange(new System.Windows.Forms.Control[] { this.btnAnterior, this.lblPagina,
                this.btnSiguiente, this.chkAmpliar, this.btnPdf, this.btnExcel, this.btnImprimir, this.btnCerrar });
            this.lblPagina.AutoSize = true;
            this.lblPagina.Margin = new System.Windows.Forms.Padding(6, 8, 6, 0);
            this.lblPagina.Name = "lblPagina";
            this.lblPagina.Text = "Página 1 de 1";
            this.btnAnterior.AutoSize = true;
            this.btnAnterior.Name = "btnAnterior";
            this.btnAnterior.TabIndex = 0;
            this.btnAnterior.Text = "Anterior";
            this.btnAnterior.Click += this.btnAnterior_Click;
            this.btnSiguiente.AutoSize = true;
            this.btnSiguiente.Name = "btnSiguiente";
            this.btnSiguiente.TabIndex = 1;
            this.btnSiguiente.Text = "Siguiente";
            this.btnSiguiente.Click += this.btnSiguiente_Click;
            this.chkAmpliar.AutoSize = true;
            this.chkAmpliar.Margin = new System.Windows.Forms.Padding(8, 7, 8, 0);
            this.chkAmpliar.Name = "chkAmpliar";
            this.chkAmpliar.TabIndex = 2;
            this.chkAmpliar.Text = "Ampliar";
            this.chkAmpliar.CheckedChanged += this.chkAmpliar_CheckedChanged;
            this.btnPdf.AutoSize = true;
            this.btnPdf.Name = "btnPdf";
            this.btnPdf.TabIndex = 3;
            this.btnPdf.Text = "Guardar PDF";
            this.btnPdf.Click += this.btnPdf_Click;
            this.btnExcel.AutoSize = true;
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.TabIndex = 4;
            this.btnExcel.Text = "Guardar Excel";
            this.btnExcel.Click += this.btnExcel_Click;
            this.btnImprimir.AutoSize = true;
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.TabIndex = 5;
            this.btnImprimir.Text = "Confirmar impresión…";
            this.btnImprimir.Click += this.btnImprimir_Click;
            this.btnCerrar.AutoSize = true;
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.TabIndex = 6;
            this.btnCerrar.Text = "Cerrar";
            this.btnCerrar.Click += this.btnCerrar_Click;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCerrar;
            this.ClientSize = new System.Drawing.Size(820, 690);
            this.Controls.Add(this.panelPagina);
            this.Controls.Add(this.acciones);
            this.MinimumSize = new System.Drawing.Size(620, 470);
            this.Name = "FrmVistaPrevia";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Vista previa";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
