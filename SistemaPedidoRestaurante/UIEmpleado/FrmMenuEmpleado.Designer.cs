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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMenuEmpleado));
            this.pnlEncabezado = new System.Windows.Forms.Panel();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.nuevaVentaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.operacionesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pedidosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mesasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cerrarSesionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.salirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lblEstado = new System.Windows.Forms.Label();
            this.pnlEncabezado.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            this.pnlEncabezado.BackColor = System.Drawing.Color.MidnightBlue;
            this.pnlEncabezado.Controls.Add(this.lblTitulo);
            this.pnlEncabezado.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlEncabezado.Location = new System.Drawing.Point(0, 0);
            this.pnlEncabezado.Margin = new System.Windows.Forms.Padding(2);
            this.pnlEncabezado.Name = "pnlEncabezado";
            this.pnlEncabezado.Size = new System.Drawing.Size(504, 70);
            this.pnlEncabezado.TabIndex = 1;
            this.lblTitulo.BackColor = System.Drawing.Color.DarkRed;
            this.lblTitulo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitulo.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Location = new System.Drawing.Point(0, 0);
            this.lblTitulo.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(504, 70);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "EMPLEADOS";
            this.lblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.menuStrip1.BackColor = System.Drawing.Color.Tomato;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nuevaVentaToolStripMenuItem,
            this.operacionesToolStripMenuItem,
            this.cerrarSesionToolStripMenuItem,
            this.salirToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 70);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(504, 24);
            this.menuStrip1.TabIndex = 0;
            this.nuevaVentaToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("nuevaVentaToolStripMenuItem.Image")));
            this.nuevaVentaToolStripMenuItem.Name = "nuevaVentaToolStripMenuItem";
            this.nuevaVentaToolStripMenuItem.Size = new System.Drawing.Size(101, 20);
            this.nuevaVentaToolStripMenuItem.Text = "Nueva venta";
            this.nuevaVentaToolStripMenuItem.Click += new System.EventHandler(this.nuevaVentaToolStripMenuItem_Click);
            this.operacionesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pedidosToolStripMenuItem,
            this.mesasToolStripMenuItem});
            this.operacionesToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("operacionesToolStripMenuItem.Image")));
            this.operacionesToolStripMenuItem.Name = "operacionesToolStripMenuItem";
            this.operacionesToolStripMenuItem.Size = new System.Drawing.Size(86, 20);
            this.operacionesToolStripMenuItem.Text = "Consultar";
            this.pedidosToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("pedidosToolStripMenuItem.Image")));
            this.pedidosToolStripMenuItem.Name = "pedidosToolStripMenuItem";
            this.pedidosToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.pedidosToolStripMenuItem.Text = "Ventas abiertas";
            this.pedidosToolStripMenuItem.Click += new System.EventHandler(this.pedidosToolStripMenuItem_Click);
            this.mesasToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("mesasToolStripMenuItem.Image")));
            this.mesasToolStripMenuItem.Name = "mesasToolStripMenuItem";
            this.mesasToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.mesasToolStripMenuItem.Text = "Estado de mesas";
            this.mesasToolStripMenuItem.Click += new System.EventHandler(this.mesas_Click);
            this.cerrarSesionToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("cerrarSesionToolStripMenuItem.Image")));
            this.cerrarSesionToolStripMenuItem.Name = "cerrarSesionToolStripMenuItem";
            this.cerrarSesionToolStripMenuItem.Size = new System.Drawing.Size(103, 20);
            this.cerrarSesionToolStripMenuItem.Text = "Cerrar sesión";
            this.cerrarSesionToolStripMenuItem.Click += new System.EventHandler(this.cerrarSesionToolStripMenuItem_Click);
            this.salirToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("salirToolStripMenuItem.Image")));
            this.salirToolStripMenuItem.Name = "salirToolStripMenuItem";
            this.salirToolStripMenuItem.Size = new System.Drawing.Size(122, 20);
            this.salirToolStripMenuItem.Text = "Cerrar programa";
            this.salirToolStripMenuItem.Click += new System.EventHandler(this.salirToolStripMenuItem_Click);
            this.lblEstado.BackColor = System.Drawing.Color.DarkSalmon;
            this.lblEstado.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblEstado.Font = new System.Drawing.Font("Arial", 11F);
            this.lblEstado.Location = new System.Drawing.Point(0, 94);
            this.lblEstado.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Padding = new System.Windows.Forms.Padding(18, 20, 18, 20);
            this.lblEstado.Size = new System.Drawing.Size(504, 248);
            this.lblEstado.TabIndex = 0;
            this.lblEstado.Text = "Consulte pedidos, avance su preparacion y registre cobros desde Operaciones.";
            this.lblEstado.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.ClientSize = new System.Drawing.Size(504, 342);
            this.Controls.Add(this.lblEstado);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.pnlEncabezado);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MinimumSize = new System.Drawing.Size(454, 292);
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
        private System.Windows.Forms.ToolStripMenuItem nuevaVentaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pedidosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem operacionesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem salirToolStripMenuItem;
        private System.Windows.Forms.Label lblEstado;

        private System.Windows.Forms.ToolStripMenuItem cerrarSesionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mesasToolStripMenuItem;
    }
}
