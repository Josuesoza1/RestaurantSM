namespace SistemaPedidoRestaurante.UIEmpleado
{
    partial class FrmMesasEmpleado
    {
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMesasEmpleado));
            this.titulo = new System.Windows.Forms.Label();
            this.buscar = new System.Windows.Forms.Panel();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.etiquetaBuscar = new System.Windows.Forms.Label();
            this.listado = new System.Windows.Forms.DataGridView();
            this.acciones = new System.Windows.Forms.FlowLayoutPanel();
            this.btnRecargar = new System.Windows.Forms.Button();
            this.btnVolver = new System.Windows.Forms.Button();
            this.buscar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listado)).BeginInit();
            this.acciones.SuspendLayout();
            this.SuspendLayout();
            //
            // titulo
            //
            this.titulo.BackColor = System.Drawing.Color.Maroon;
            this.titulo.Dock = System.Windows.Forms.DockStyle.Top;
            this.titulo.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.titulo.ForeColor = System.Drawing.Color.White;
            this.titulo.Location = new System.Drawing.Point(0, 0);
            this.titulo.Name = "titulo";
            this.titulo.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.titulo.Size = new System.Drawing.Size(1371, 98);
            this.titulo.TabIndex = 3;
            this.titulo.Text = "MESAS — EMPLEADO";
            this.titulo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // buscar
            //
            this.buscar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.buscar.Controls.Add(this.txtBuscar);
            this.buscar.Controls.Add(this.etiquetaBuscar);
            this.buscar.Dock = System.Windows.Forms.DockStyle.Top;
            this.buscar.Location = new System.Drawing.Point(0, 98);
            this.buscar.Name = "buscar";
            this.buscar.Padding = new System.Windows.Forms.Padding(12, 8, 12, 8);
            this.buscar.Size = new System.Drawing.Size(1371, 49);
            this.buscar.TabIndex = 0;
            //
            // txtBuscar
            //
            this.txtBuscar.AccessibleName = "Buscar mesa";
            this.txtBuscar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtBuscar.Location = new System.Drawing.Point(77, 8);
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(1282, 22);
            this.txtBuscar.TabIndex = 0;
            this.txtBuscar.TextChanged += new System.EventHandler(this.txtBuscar_TextChanged);
            //
            // etiquetaBuscar
            //
            this.etiquetaBuscar.Dock = System.Windows.Forms.DockStyle.Left;
            this.etiquetaBuscar.Location = new System.Drawing.Point(12, 8);
            this.etiquetaBuscar.Name = "etiquetaBuscar";
            this.etiquetaBuscar.Size = new System.Drawing.Size(65, 33);
            this.etiquetaBuscar.TabIndex = 1;
            this.etiquetaBuscar.Text = "Buscar:";
            this.etiquetaBuscar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // listado
            //
            this.listado.AccessibleName = "Listado de mesas";
            this.listado.AllowUserToAddRows = false;
            this.listado.AllowUserToDeleteRows = false;
            this.listado.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.listado.BackgroundColor = System.Drawing.Color.White;
            this.listado.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.listado.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listado.Location = new System.Drawing.Point(0, 147);
            this.listado.MultiSelect = false;
            this.listado.Name = "listado";
            this.listado.ReadOnly = true;
            this.listado.RowHeadersWidth = 51;
            this.listado.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.listado.Size = new System.Drawing.Size(1371, 545);
            this.listado.StandardTab = true;
            this.listado.TabIndex = 1;
            //
            // acciones
            //
            this.acciones.AutoSize = true;
            this.acciones.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.acciones.Controls.Add(this.btnRecargar);
            this.acciones.Controls.Add(this.btnVolver);
            this.acciones.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.acciones.Location = new System.Drawing.Point(0, 692);
            this.acciones.Name = "acciones";
            this.acciones.Padding = new System.Windows.Forms.Padding(8);
            this.acciones.Size = new System.Drawing.Size(1371, 58);
            this.acciones.TabIndex = 2;
            //
            // btnRecargar
            //
            this.btnRecargar.AutoSize = true;
            this.btnRecargar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnRecargar.Location = new System.Drawing.Point(11, 11);
            this.btnRecargar.Name = "btnRecargar";
            this.btnRecargar.Padding = new System.Windows.Forms.Padding(5);
            this.btnRecargar.Size = new System.Drawing.Size(84, 36);
            this.btnRecargar.TabIndex = 0;
            this.btnRecargar.Text = "Recargar";
            this.btnRecargar.UseVisualStyleBackColor = false;
            this.btnRecargar.Click += new System.EventHandler(this.btnRecargar_Click);
            //
            // btnVolver
            //
            this.btnVolver.AutoSize = true;
            this.btnVolver.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnVolver.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnVolver.Location = new System.Drawing.Point(101, 11);
            this.btnVolver.Name = "btnVolver";
            this.btnVolver.Padding = new System.Windows.Forms.Padding(5);
            this.btnVolver.Size = new System.Drawing.Size(75, 36);
            this.btnVolver.TabIndex = 1;
            this.btnVolver.Text = "Volver";
            this.btnVolver.UseVisualStyleBackColor = false;
            this.btnVolver.Click += new System.EventHandler(this.btnVolver_Click);
            //
            // FrmMesasEmpleado
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnVolver;
            this.ClientSize = new System.Drawing.Size(1371, 750);
            this.Controls.Add(this.listado);
            this.Controls.Add(this.acciones);
            this.Controls.Add(this.buscar);
            this.Controls.Add(this.titulo);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(1229, 653);
            this.Name = "FrmMesasEmpleado";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Mesas — Empleado";
            this.Load += new System.EventHandler(this.FrmMesasEmpleado_Load);
            this.buscar.ResumeLayout(false);
            this.buscar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listado)).EndInit();
            this.acciones.ResumeLayout(false);
            this.acciones.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Label titulo;
        private System.Windows.Forms.Panel buscar;
        private System.Windows.Forms.Label etiquetaBuscar;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.DataGridView listado;
        private System.Windows.Forms.FlowLayoutPanel acciones;
        private System.Windows.Forms.Button btnRecargar;
        private System.Windows.Forms.Button btnVolver;
    }
}
