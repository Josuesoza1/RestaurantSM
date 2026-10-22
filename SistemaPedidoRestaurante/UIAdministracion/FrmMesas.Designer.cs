namespace SistemaPedidoRestaurante.UIAdministracion
{
    partial class FrmMesas
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.agregarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eliminarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.desactivarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.volverToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmbEstadoMesas = new System.Windows.Forms.ComboBox();
            this.txtCapacidadMesas = new System.Windows.Forms.TextBox();
            this.txtNumeroMesas = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnGuardarMesas = new System.Windows.Forms.Button();
            this.btnCancelarMesas = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgvMesas = new System.Windows.Forms.DataGridView();
            this.ColNumMesas = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColCapacidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColEstado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtBuscarMesas = new System.Windows.Forms.TextBox();
            this.btnBuscarMesas = new System.Windows.Forms.Button();
            this.lblBuscarMesas = new System.Windows.Forms.Label();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.ayudaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMesas)).BeginInit();
            this.chkActiva = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            //
            // menuStrip1
            //
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.agregarToolStripMenuItem,
            this.eliminarToolStripMenuItem,
            this.desactivarToolStripMenuItem,
            this.editarToolStripMenuItem,
            this.volverToolStripMenuItem1,
            this.ayudaToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(751, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            //
            // agregarToolStripMenuItem
            //
            this.agregarToolStripMenuItem.Name = "agregarToolStripMenuItem";
            this.agregarToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.agregarToolStripMenuItem.Text = "Agregar";
            //
            // eliminarToolStripMenuItem
            //
            this.desactivarToolStripMenuItem.Name = "desactivarToolStripMenuItem";
            this.desactivarToolStripMenuItem.Text = "Desactivar";
            this.desactivarToolStripMenuItem.Click += new System.EventHandler(this.desactivarToolStripMenuItem_Click);
            this.eliminarToolStripMenuItem.Name = "eliminarToolStripMenuItem";
            this.eliminarToolStripMenuItem.Click += new System.EventHandler(this.eliminarToolStripMenuItem_Click);
            this.eliminarToolStripMenuItem.Size = new System.Drawing.Size(62, 20);
            this.eliminarToolStripMenuItem.Text = "Eliminar";
            //
            // editarToolStripMenuItem
            //
            this.editarToolStripMenuItem.Name = "editarToolStripMenuItem";
            this.editarToolStripMenuItem.Size = new System.Drawing.Size(49, 20);
            this.editarToolStripMenuItem.Text = "Editar";
            //
            // volverToolStripMenuItem1
            //
            this.volverToolStripMenuItem1.Name = "volverToolStripMenuItem1";
            this.volverToolStripMenuItem1.Size = new System.Drawing.Size(51, 20);
            this.volverToolStripMenuItem1.Text = "Volver";
            //
            // panel1
            //
            this.panel1.BackColor = System.Drawing.Color.DarkSlateBlue;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(751, 100);
            this.panel1.TabIndex = 2;
            //
            // label1
            //
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial Black", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.Control;
            this.label1.Location = new System.Drawing.Point(31, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(280, 33);
            this.label1.TabIndex = 0;
            this.label1.Text = "GESTION DE MESAS";
            //
            // groupBox1
            //
            this.groupBox1.Controls.Add(this.cmbEstadoMesas);
            this.groupBox1.Controls.Add(this.txtCapacidadMesas);
            this.groupBox1.Controls.Add(this.txtNumeroMesas);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Font = new System.Drawing.Font("Arial Black", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(26, 142);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(279, 215);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Datos de mesa";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            //
            // cmbEstadoMesas
            //
            this.cmbEstadoMesas.FormattingEnabled = true;
            this.cmbEstadoMesas.Location = new System.Drawing.Point(138, 144);
            this.cmbEstadoMesas.Name = "cmbEstadoMesas";
            this.cmbEstadoMesas.Size = new System.Drawing.Size(134, 25);
            this.cmbEstadoMesas.TabIndex = 16;
            //
            // txtCapacidadMesas
            //
            this.txtCapacidadMesas.Location = new System.Drawing.Point(139, 93);
            this.txtCapacidadMesas.Name = "txtCapacidadMesas";
            this.txtCapacidadMesas.Size = new System.Drawing.Size(133, 24);
            this.txtCapacidadMesas.TabIndex = 15;
            //
            // txtNumeroMesas
            //
            this.txtNumeroMesas.Location = new System.Drawing.Point(139, 45);
            this.txtNumeroMesas.Name = "txtNumeroMesas";
            this.txtNumeroMesas.Size = new System.Drawing.Size(133, 24);
            this.txtNumeroMesas.TabIndex = 14;
            //
            // label4
            //
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(21, 149);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 15);
            this.label4.TabIndex = 13;
            this.label4.Text = "Estado:";
            //
            // label2
            //
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(21, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(113, 15);
            this.label2.TabIndex = 12;
            this.label2.Text = "Número de Mesas:";
            //
            // label3
            //
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(21, 97);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 15);
            this.label3.TabIndex = 11;
            this.label3.Text = "Capacidad:";
            //
            // btnGuardarMesas
            //
            this.btnGuardarMesas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnGuardarMesas.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardarMesas.Font = new System.Drawing.Font("Arial Black", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardarMesas.ForeColor = System.Drawing.Color.Black;
            this.btnGuardarMesas.Location = new System.Drawing.Point(50, 373);
            this.btnGuardarMesas.Name = "btnGuardarMesas";
            this.btnGuardarMesas.Click += new System.EventHandler(this.btnGuardarMesas_Click);
            this.btnGuardarMesas.Size = new System.Drawing.Size(81, 31);
            this.btnGuardarMesas.TabIndex = 4;
            this.btnGuardarMesas.Text = "Guardar";
            this.btnGuardarMesas.UseVisualStyleBackColor = false;
            //
            // btnCancelarMesas
            //
            this.btnCancelarMesas.BackColor = System.Drawing.Color.LightCoral;
            this.btnCancelarMesas.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelarMesas.Font = new System.Drawing.Font("Arial Black", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelarMesas.ForeColor = System.Drawing.Color.Black;
            this.btnCancelarMesas.Location = new System.Drawing.Point(202, 373);
            this.btnCancelarMesas.Name = "btnCancelarMesas";
            this.btnCancelarMesas.Click += new System.EventHandler(this.btnCancelarMesas_Click);
            this.btnCancelarMesas.Size = new System.Drawing.Size(81, 31);
            this.btnCancelarMesas.TabIndex = 8;
            this.btnCancelarMesas.Text = "Cancelar";
            this.btnCancelarMesas.UseVisualStyleBackColor = false;
            //
            // groupBox2
            //
            this.groupBox2.Controls.Add(this.dgvMesas);
            this.groupBox2.Location = new System.Drawing.Point(311, 168);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(431, 100);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Listado de Mesas";
            //
            // dgvMesas
            //
            this.dgvMesas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMesas.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColNumMesas,
            this.ColCapacidad,
            this.ColEstado});
            this.dgvMesas.Location = new System.Drawing.Point(6, 19);
            this.dgvMesas.Name = "dgvMesas";
            this.dgvMesas.Size = new System.Drawing.Size(408, 75);
            this.dgvMesas.TabIndex = 0;
            //
            // ColNumMesas
            //
            this.ColNumMesas.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColNumMesas.HeaderText = "Número de Mesas";
            this.ColNumMesas.Name = "ColNumMesas";
            //
            // ColCapacidad
            //
            this.ColCapacidad.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColCapacidad.HeaderText = "Capacidad";
            this.ColCapacidad.Name = "ColCapacidad";
            //
            // ColEstado
            //
            this.ColEstado.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColEstado.HeaderText = "Estado";
            this.ColEstado.Name = "ColEstado";
            //
            // txtBuscarMesas
            //
            this.txtBuscarMesas.Location = new System.Drawing.Point(375, 327);
            this.txtBuscarMesas.Name = "txtBuscarMesas";
            this.txtBuscarMesas.Size = new System.Drawing.Size(266, 20);
            this.txtBuscarMesas.TabIndex = 10;
            //
            // btnBuscarMesas
            //
            this.btnBuscarMesas.BackColor = System.Drawing.Color.DarkTurquoise;
            this.btnBuscarMesas.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscarMesas.ForeColor = System.Drawing.Color.Blue;
            this.btnBuscarMesas.Location = new System.Drawing.Point(461, 375);
            this.btnBuscarMesas.Name = "btnBuscarMesas";
            this.btnBuscarMesas.Click += new System.EventHandler(this.btnBuscarMesas_Click);
            this.btnBuscarMesas.Size = new System.Drawing.Size(89, 31);
            this.btnBuscarMesas.TabIndex = 11;
            this.btnBuscarMesas.Text = "Buscar";
            this.btnBuscarMesas.UseVisualStyleBackColor = false;
            //
            // lblBuscarMesas
            //
            this.lblBuscarMesas.AutoSize = true;
            this.lblBuscarMesas.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBuscarMesas.ForeColor = System.Drawing.Color.Black;
            this.lblBuscarMesas.Location = new System.Drawing.Point(336, 291);
            this.lblBuscarMesas.Name = "lblBuscarMesas";
            this.lblBuscarMesas.Size = new System.Drawing.Size(51, 15);
            this.lblBuscarMesas.TabIndex = 12;
            this.lblBuscarMesas.Text = "Buscar:";
            //
            // btnLimpiar
            //
            this.btnLimpiar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnLimpiar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLimpiar.Font = new System.Drawing.Font("Arial Black", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLimpiar.ForeColor = System.Drawing.Color.Black;
            this.btnLimpiar.Location = new System.Drawing.Point(124, 373);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            this.btnLimpiar.Size = new System.Drawing.Size(81, 31);
            this.btnLimpiar.TabIndex = 13;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.UseVisualStyleBackColor = false;
            //
            // ayudaToolStripMenuItem
            //
            this.ayudaToolStripMenuItem.Name = "ayudaToolStripMenuItem";
            this.ayudaToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
            this.ayudaToolStripMenuItem.Text = "Ayuda";
            //
            // FrmMesas
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(751, 450);
            this.Controls.Add(this.btnLimpiar);
            this.Controls.Add(this.lblBuscarMesas);
            this.Controls.Add(this.btnBuscarMesas);
            this.Controls.Add(this.txtBuscarMesas);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnCancelarMesas);
            this.Controls.Add(this.btnGuardarMesas);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.Name = "FrmMesas";
            this.Text = "GESTION DE MESAS";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMesas)).EndInit();
            this.chkActiva.Name = "chkActiva";
            this.chkActiva.Text = "Activa";
            this.chkActiva.Checked = true;
            this.chkActiva.Location = new System.Drawing.Point(138, 181);
            this.chkActiva.Size = new System.Drawing.Size(120, 22);
            this.groupBox1.Controls.Add(this.chkActiva);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkActiva;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem agregarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem eliminarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem desactivarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editarToolStripMenuItem;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnGuardarMesas;
        private System.Windows.Forms.Button btnCancelarMesas;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgvMesas;
        private System.Windows.Forms.ToolStripMenuItem volverToolStripMenuItem1;
        private System.Windows.Forms.TextBox txtBuscarMesas;
        private System.Windows.Forms.Button btnBuscarMesas;
        private System.Windows.Forms.Label lblBuscarMesas;
        private System.Windows.Forms.TextBox txtCapacidadMesas;
        private System.Windows.Forms.TextBox txtNumeroMesas;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbEstadoMesas;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColNumMesas;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColCapacidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColEstado;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.ToolStripMenuItem ayudaToolStripMenuItem;
    }
}