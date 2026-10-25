namespace SistemaPedidoRestaurante.UIEmpleado
{
    partial class FrmSeleccionCliente
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
            this.listado = new System.Windows.Forms.DataGridView();
            this.buscar = new System.Windows.Forms.TextBox();
            this.elegir = new System.Windows.Forms.Button();
            this.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listado)).BeginInit();
            this.listado.Name = "listado";
            this.buscar.Name = "buscar";
            this.elegir.Name = "elegir";
            this.buscar.Dock = System.Windows.Forms.DockStyle.Top;
            this.buscar.TextChanged += new System.EventHandler(this.buscar_TextChanged);
            this.listado.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listado.ReadOnly = true;
            this.listado.AllowUserToAddRows = false;
            this.listado.AllowUserToDeleteRows = false;
            this.listado.MultiSelect = false;
            this.listado.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.listado.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.elegir.Text = "Seleccionar";
            this.elegir.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.elegir.Height = 40;
            this.elegir.Click += new System.EventHandler(this.elegir_Click);
            this.Controls.Add(this.listado);
            this.Controls.Add(this.buscar);
            this.Controls.Add(this.elegir);
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 461);
            this.MinimumSize = new System.Drawing.Size(600, 350);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Name = "FrmSeleccionCliente";
            this.Text = "Seleccionar cliente registrado";
            this.Load += new System.EventHandler(this.cargar);
            ((System.ComponentModel.ISupportInitialize)(this.listado)).EndInit();
            this.ResumeLayout(false);
        }
        #endregion

        private System.Windows.Forms.DataGridView listado;
        private System.Windows.Forms.TextBox buscar;
        private System.Windows.Forms.Button elegir;
    }
}
