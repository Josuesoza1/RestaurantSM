namespace SistemaPedidoRestaurante.UIEmpleado
{
    partial class FrmMenuEmpleado
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pnlEncabezado = new System.Windows.Forms.Panel();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.pedidosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cobrarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.salirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lblEstado = new System.Windows.Forms.Label();
            this.pnlEncabezado.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // pnlEncabezado
            this.pnlEncabezado.BackColor = System.Drawing.Color.MidnightBlue;
            this.pnlEncabezado.Controls.Add(this.lblTitulo);
            this.pnlEncabezado.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlEncabezado.Name = "pnlEncabezado";
            this.pnlEncabezado.Size = new System.Drawing.Size(672, 86);
            // lblTitulo
            this.lblTitulo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitulo.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Text = "EMPLEADOS";
            this.lblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // menuStrip1
            this.menuStrip1.BackColor = System.Drawing.Color.LightSteelBlue;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.pedidosToolStripMenuItem,
                this.cobrarToolStripMenuItem,
                this.salirToolStripMenuItem });
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.TabIndex = 0;
            // pedidosToolStripMenuItem
            this.pedidosToolStripMenuItem.Enabled = false;
            this.pedidosToolStripMenuItem.Name = "pedidosToolStripMenuItem";
            this.pedidosToolStripMenuItem.Text = "Pedidos";
            // cobrarToolStripMenuItem
            this.cobrarToolStripMenuItem.Enabled = false;
            this.cobrarToolStripMenuItem.Name = "cobrarToolStripMenuItem";
            this.cobrarToolStripMenuItem.Text = "Cobrar venta";
            // salirToolStripMenuItem
            this.salirToolStripMenuItem.Name = "salirToolStripMenuItem";
            this.salirToolStripMenuItem.Text = "Salir";
            this.salirToolStripMenuItem.Click += new System.EventHandler(this.salirToolStripMenuItem_Click);
            // lblEstado
            this.lblEstado.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblEstado.Font = new System.Drawing.Font("Arial", 11F);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Padding = new System.Windows.Forms.Padding(24);
            this.lblEstado.Text = "Bienvenido al menú del empleado.\r\n\r\nLas opciones de pedidos y cobro se habilitarán al completar esos módulos.";
            this.lblEstado.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // FrmMenuEmpleado
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.ClientSize = new System.Drawing.Size(672, 421);
            this.Controls.Add(this.lblEstado);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.pnlEncabezado);
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(600, 350);
            this.Name = "FrmMenuEmpleado";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Menú para Empleados";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmMenuEmpleado_FormClosed);
            this.pnlEncabezado.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Panel pnlEncabezado;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem pedidosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cobrarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem salirToolStripMenuItem;
        private System.Windows.Forms.Label lblEstado;
    }
}
