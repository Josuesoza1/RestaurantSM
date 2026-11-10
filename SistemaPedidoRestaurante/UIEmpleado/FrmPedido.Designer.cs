namespace SistemaPedidoRestaurante.UIEmpleado
{
    partial class FrmPedido
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPedido));
            ((System.ComponentModel.ISupportInitialize)(this.nudCantidad)).BeginInit();
            this.pnlEncabezado.SuspendLayout();
            this.grpDatosCliente.SuspendLayout();
            this.grpProducto.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnQuitar
            // 
            this.btnQuitar.FlatAppearance.BorderColor = System.Drawing.Color.DarkGray;
            // 
            // btnVaciar
            // 
            this.btnVaciar.FlatAppearance.BorderColor = System.Drawing.Color.DarkGray;
            // 
            // btnEnviar
            // 
            this.btnEnviar.FlatAppearance.BorderColor = System.Drawing.Color.DarkGray;
            // 
            // btnCobrarAhora
            // 
            this.btnCobrarAhora.FlatAppearance.BorderColor = System.Drawing.Color.DarkGray;
            // 
            // pnlEncabezado
            // 
            this.pnlEncabezado.BackColor = System.Drawing.Color.Maroon;
            // 
            // btnAceptar
            // 
            this.btnAceptar.FlatAppearance.BorderColor = System.Drawing.Color.DarkGray;
            // 
            // btnCancelar
            // 
            this.btnCancelar.FlatAppearance.BorderColor = System.Drawing.Color.DarkGray;
            // 
            // cmbPrecio
            // 
            this.cmbPrecio.Size = new System.Drawing.Size(160, 23);
            // 
            // cmbProducto
            // 
            this.cmbProducto.Size = new System.Drawing.Size(320, 23);
            // 
            // cmbCategoria
            // 
            this.cmbCategoria.Size = new System.Drawing.Size(320, 23);
            // 
            // FrmPedido
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.BackColor = System.Drawing.Color.Khaki;
            this.ClientSize = new System.Drawing.Size(784, 565);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmPedido";
            ((System.ComponentModel.ISupportInitialize)(this.nudCantidad)).EndInit();
            this.pnlEncabezado.ResumeLayout(false);
            this.pnlEncabezado.PerformLayout();
            this.grpDatosCliente.ResumeLayout(false);
            this.grpDatosCliente.PerformLayout();
            this.grpProducto.ResumeLayout(false);
            this.grpProducto.PerformLayout();
            this.ResumeLayout(false);

        }
    }
}
