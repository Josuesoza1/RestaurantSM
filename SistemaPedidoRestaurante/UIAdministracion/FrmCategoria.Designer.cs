namespace SistemaPedidoRestaurante.UIAdministracion
{
    partial class FrmCategoria
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlEncabezado = new System.Windows.Forms.Panel();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.btnAccion = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtTextoBuscar = new System.Windows.Forms.TextBox();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.lblBuscar = new System.Windows.Forms.Label();
            this.grpListadoCliente = new System.Windows.Forms.GroupBox();
            this.dgvCategorias = new System.Windows.Forms.DataGridView();
            this.ColID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColPnombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColSnombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.editarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eliminarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nuevoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.volverToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lblOperacion = new System.Windows.Forms.Label();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lblPnombre = new System.Windows.Forms.Label();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.grpAcciones = new System.Windows.Forms.GroupBox();
            this.grpDatosClientes = new System.Windows.Forms.GroupBox();
            this.pnlEncabezado.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.grpListadoCliente.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCategorias)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.grpAcciones.SuspendLayout();
            this.grpDatosClientes.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlEncabezado
            // 
            this.pnlEncabezado.BackColor = System.Drawing.Color.DarkSlateBlue;
            this.pnlEncabezado.Controls.Add(this.lblTitulo);
            this.pnlEncabezado.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlEncabezado.Location = new System.Drawing.Point(0, 33);
            this.pnlEncabezado.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnlEncabezado.Name = "pnlEncabezado";
            this.pnlEncabezado.Size = new System.Drawing.Size(908, 120);
            this.pnlEncabezado.TabIndex = 2;
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Location = new System.Drawing.Point(195, 36);
            this.lblTitulo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(487, 43);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "GESTIÓN DE CATEGORÍAS";
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.BackColor = System.Drawing.Color.MediumSpringGreen;
            this.btnLimpiar.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLimpiar.ForeColor = System.Drawing.Color.Black;
            this.btnLimpiar.Location = new System.Drawing.Point(147, 34);
            this.btnLimpiar.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(111, 58);
            this.btnLimpiar.TabIndex = 4;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.UseVisualStyleBackColor = false;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // btnAccion
            // 
            this.btnAccion.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.btnAccion.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAccion.ForeColor = System.Drawing.Color.Black;
            this.btnAccion.Location = new System.Drawing.Point(28, 32);
            this.btnAccion.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnAccion.Name = "btnAccion";
            this.btnAccion.Size = new System.Drawing.Size(111, 60);
            this.btnAccion.TabIndex = 1;
            this.btnAccion.Text = "Guardar";
            this.btnAccion.UseVisualStyleBackColor = false;
            this.btnAccion.Click += new System.EventHandler(this.btnAccion_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.LightGray;
            this.groupBox2.Controls.Add(this.txtTextoBuscar);
            this.groupBox2.Controls.Add(this.btnBuscar);
            this.groupBox2.Controls.Add(this.lblBuscar);
            this.groupBox2.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.groupBox2.Location = new System.Drawing.Point(453, 387);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox2.Size = new System.Drawing.Size(411, 71);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            // 
            // txtTextoBuscar
            // 
            this.txtTextoBuscar.Location = new System.Drawing.Point(87, 23);
            this.txtTextoBuscar.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtTextoBuscar.Name = "txtTextoBuscar";
            this.txtTextoBuscar.Size = new System.Drawing.Size(228, 26);
            this.txtTextoBuscar.TabIndex = 9;
            // 
            // btnBuscar
            // 
            this.btnBuscar.BackColor = System.Drawing.Color.SlateGray;
            this.btnBuscar.Font = new System.Drawing.Font("Arial", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscar.ForeColor = System.Drawing.Color.Black;
            this.btnBuscar.Location = new System.Drawing.Point(323, 13);
            this.btnBuscar.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(80, 48);
            this.btnBuscar.TabIndex = 8;
            this.btnBuscar.Text = "⌕";
            this.btnBuscar.UseVisualStyleBackColor = false;
            // 
            // lblBuscar
            // 
            this.lblBuscar.AutoSize = true;
            this.lblBuscar.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBuscar.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblBuscar.Location = new System.Drawing.Point(8, 27);
            this.lblBuscar.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBuscar.Name = "lblBuscar";
            this.lblBuscar.Size = new System.Drawing.Size(71, 19);
            this.lblBuscar.TabIndex = 5;
            this.lblBuscar.Text = "Buscar:";
            // 
            // grpListadoCliente
            // 
            this.grpListadoCliente.Controls.Add(this.dgvCategorias);
            this.grpListadoCliente.ForeColor = System.Drawing.Color.DarkOrchid;
            this.grpListadoCliente.Location = new System.Drawing.Point(27, 530);
            this.grpListadoCliente.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grpListadoCliente.Name = "grpListadoCliente";
            this.grpListadoCliente.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grpListadoCliente.Size = new System.Drawing.Size(848, 188);
            this.grpListadoCliente.TabIndex = 13;
            this.grpListadoCliente.TabStop = false;
            this.grpListadoCliente.Text = "Listado de Categorías";
            // 
            // dgvCategorias
            // 
            this.dgvCategorias.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCategorias.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvCategorias.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCategorias.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColID,
            this.ColPnombre,
            this.ColSnombre});
            this.dgvCategorias.GridColor = System.Drawing.SystemColors.AppWorkspace;
            this.dgvCategorias.Location = new System.Drawing.Point(33, 29);
            this.dgvCategorias.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dgvCategorias.Name = "dgvCategorias";
            this.dgvCategorias.ReadOnly = true;
            this.dgvCategorias.RowHeadersWidth = 62;
            this.dgvCategorias.Size = new System.Drawing.Size(781, 149);
            this.dgvCategorias.TabIndex = 0;
            // 
            // ColID
            // 
            this.ColID.HeaderText = "ID";
            this.ColID.MinimumWidth = 8;
            this.ColID.Name = "ColID";
            this.ColID.ReadOnly = true;
            // 
            // ColPnombre
            // 
            this.ColPnombre.HeaderText = "Nombre";
            this.ColPnombre.MinimumWidth = 8;
            this.ColPnombre.Name = "ColPnombre";
            this.ColPnombre.ReadOnly = true;
            // 
            // ColSnombre
            // 
            this.ColSnombre.HeaderText = "Descripción";
            this.ColSnombre.MinimumWidth = 8;
            this.ColSnombre.Name = "ColSnombre";
            this.ColSnombre.ReadOnly = true;
            // 
            // menuStrip1
            // 
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editarToolStripMenuItem,
            this.eliminarToolStripMenuItem,
            this.nuevoToolStripMenuItem,
            this.volverToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(908, 33);
            this.menuStrip1.TabIndex = 14;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // editarToolStripMenuItem
            // 
            this.editarToolStripMenuItem.BackColor = System.Drawing.Color.Silver;
            this.editarToolStripMenuItem.ForeColor = System.Drawing.Color.DarkSlateBlue;
            this.editarToolStripMenuItem.Name = "editarToolStripMenuItem";
            this.editarToolStripMenuItem.Size = new System.Drawing.Size(73, 29);
            this.editarToolStripMenuItem.Text = "Editar";
            this.editarToolStripMenuItem.Click += new System.EventHandler(this.editarToolStripMenuItem_Click);
            // 
            // eliminarToolStripMenuItem
            // 
            this.eliminarToolStripMenuItem.ForeColor = System.Drawing.Color.Crimson;
            this.eliminarToolStripMenuItem.Name = "eliminarToolStripMenuItem";
            this.eliminarToolStripMenuItem.Size = new System.Drawing.Size(90, 29);
            this.eliminarToolStripMenuItem.Text = "Eliminar";
            this.eliminarToolStripMenuItem.Click += new System.EventHandler(this.eliminarToolStripMenuItem_Click);
            // 
            // nuevoToolStripMenuItem
            // 
            this.nuevoToolStripMenuItem.ForeColor = System.Drawing.Color.SeaGreen;
            this.nuevoToolStripMenuItem.Name = "nuevoToolStripMenuItem";
            this.nuevoToolStripMenuItem.Size = new System.Drawing.Size(92, 29);
            this.nuevoToolStripMenuItem.Text = "Agregar";
            this.nuevoToolStripMenuItem.Click += new System.EventHandler(this.nuevoToolStripMenuItem_Click);
            // 
            // volverToolStripMenuItem
            // 
            this.volverToolStripMenuItem.Name = "volverToolStripMenuItem";
            this.volverToolStripMenuItem.Size = new System.Drawing.Size(216, 29);
            this.volverToolStripMenuItem.Text = "Volver al Menú Príncipal";
            this.volverToolStripMenuItem.Click += new System.EventHandler(this.volverToolStripMenuItem_Click);
            // 
            // lblOperacion
            // 
            this.lblOperacion.AutoSize = true;
            this.lblOperacion.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOperacion.Location = new System.Drawing.Point(16, 21);
            this.lblOperacion.Name = "lblOperacion";
            this.lblOperacion.Size = new System.Drawing.Size(395, 21);
            this.lblOperacion.TabIndex = 15;
            this.lblOperacion.Text = "Operación actual: Seleccione una operación";
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.Color.Crimson;
            this.btnCancelar.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.ForeColor = System.Drawing.Color.Black;
            this.btnCancelar.Location = new System.Drawing.Point(266, 34);
            this.btnCancelar.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(111, 60);
            this.btnCancelar.TabIndex = 5;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Silver;
            this.panel1.Controls.Add(this.lblOperacion);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 153);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(908, 64);
            this.panel1.TabIndex = 16;
            // 
            // txtNombre
            // 
            this.txtNombre.Location = new System.Drawing.Point(134, 51);
            this.txtNombre.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(223, 26);
            this.txtNombre.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label3.Location = new System.Drawing.Point(48, 105);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(108, 19);
            this.label3.TabIndex = 2;
            this.label3.Text = "Descripción:";
            // 
            // lblPnombre
            // 
            this.lblPnombre.AutoSize = true;
            this.lblPnombre.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblPnombre.Location = new System.Drawing.Point(48, 54);
            this.lblPnombre.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPnombre.Name = "lblPnombre";
            this.lblPnombre.Size = new System.Drawing.Size(77, 19);
            this.lblPnombre.TabIndex = 0;
            this.lblPnombre.Text = "Nombre:";
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.Location = new System.Drawing.Point(51, 146);
            this.txtDescripcion.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtDescripcion.MaxLength = 150;
            this.txtDescripcion.Multiline = true;
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Size = new System.Drawing.Size(302, 93);
            this.txtDescripcion.TabIndex = 9;
            // 
            // grpAcciones
            // 
            this.grpAcciones.Controls.Add(this.btnCancelar);
            this.grpAcciones.Controls.Add(this.btnLimpiar);
            this.grpAcciones.Controls.Add(this.btnAccion);
            this.grpAcciones.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.grpAcciones.ForeColor = System.Drawing.Color.Green;
            this.grpAcciones.Location = new System.Drawing.Point(453, 241);
            this.grpAcciones.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grpAcciones.Name = "grpAcciones";
            this.grpAcciones.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grpAcciones.Size = new System.Drawing.Size(411, 124);
            this.grpAcciones.TabIndex = 11;
            this.grpAcciones.TabStop = false;
            this.grpAcciones.Text = "Acciones";
            // 
            // grpDatosClientes
            // 
            this.grpDatosClientes.Controls.Add(this.txtDescripcion);
            this.grpDatosClientes.Controls.Add(this.txtNombre);
            this.grpDatosClientes.Controls.Add(this.label3);
            this.grpDatosClientes.Controls.Add(this.lblPnombre);
            this.grpDatosClientes.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpDatosClientes.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.grpDatosClientes.Location = new System.Drawing.Point(20, 241);
            this.grpDatosClientes.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grpDatosClientes.Name = "grpDatosClientes";
            this.grpDatosClientes.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grpDatosClientes.Size = new System.Drawing.Size(411, 263);
            this.grpDatosClientes.TabIndex = 3;
            this.grpDatosClientes.TabStop = false;
            this.grpDatosClientes.Text = "Datos de la Categoría";
            // 
            // FrmCategoria
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(908, 740);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.grpListadoCliente);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.grpAcciones);
            this.Controls.Add(this.grpDatosClientes);
            this.Controls.Add(this.pnlEncabezado);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FrmCategoria";
            this.Text = "Gestión de Categoría";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmCategoria_FormClosed);
            this.Load += new System.EventHandler(this.FrmCategoria_Load);
            this.pnlEncabezado.ResumeLayout(false);
            this.pnlEncabezado.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.grpListadoCliente.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCategorias)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.grpAcciones.ResumeLayout(false);
            this.grpDatosClientes.ResumeLayout(false);
            this.grpDatosClientes.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlEncabezado;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Button btnAccion;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtTextoBuscar;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.Label lblBuscar;
        private System.Windows.Forms.GroupBox grpListadoCliente;
        private System.Windows.Forms.DataGridView dgvCategorias;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColPnombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColSnombre;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem nuevoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem eliminarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem volverToolStripMenuItem;
        private System.Windows.Forms.Label lblOperacion;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.Label lblPnombre;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.GroupBox grpAcciones;
        private System.Windows.Forms.GroupBox grpDatosClientes;
    }
}