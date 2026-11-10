namespace SistemaPedidoRestaurante.UIEmpleado
{
    partial class FrmMesasEmpleado
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMesasEmpleado));
            this.buscar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.paneles)).BeginInit();
            this.paneles.SuspendLayout();
            this.SuspendLayout();
            // 
            // titulo
            // 
            this.titulo.BackColor = System.Drawing.Color.Maroon;
            this.titulo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.titulo.Size = new System.Drawing.Size(1028, 80);
            this.titulo.Text = "MESAS — EMPLEADO";
            // 
            // buscar
            // 
            this.buscar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.buscar.Location = new System.Drawing.Point(0, 80);
            this.buscar.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.buscar.Padding = new System.Windows.Forms.Padding(9, 6, 9, 6);
            // 
            // txtBuscar
            // 
            this.txtBuscar.Location = new System.Drawing.Point(187, 6);
            this.txtBuscar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtBuscar.Size = new System.Drawing.Size(325, 20);
            // 
            // etiquetaBuscar
            // 
            this.etiquetaBuscar.Location = new System.Drawing.Point(122, 6);
            this.etiquetaBuscar.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.etiquetaBuscar.Size = new System.Drawing.Size(65, 33);
            // 
            // paneles
            // 
            this.paneles.Location = new System.Drawing.Point(0, 125);
            this.paneles.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.paneles.Panel2Collapsed = true;
            this.paneles.Size = new System.Drawing.Size(1028, 384);
            this.paneles.SplitterDistance = 239;
            this.paneles.SplitterWidth = 5;
            // 
            // btnNuevo
            // 
            this.btnNuevo.Location = new System.Drawing.Point(9, 6);
            this.btnNuevo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnNuevo.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnNuevo.Size = new System.Drawing.Size(113, 33);
            this.btnNuevo.Visible = false;
            // 
            // FrmMesasEmpleado
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(1028, 609);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MinimumSize = new System.Drawing.Size(926, 539);
            this.Name = "FrmMesasEmpleado";
            this.Text = "Mesas — Empleado";
            this.buscar.ResumeLayout(false);
            this.buscar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.paneles)).EndInit();
            this.paneles.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
