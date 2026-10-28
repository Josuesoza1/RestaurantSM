namespace SistemaPedidoRestaurante.UIAdministracion
{
    partial class FrmUsuarios
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pnlEncabezado = new System.Windows.Forms.Panel();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.pnlBusqueda = new System.Windows.Forms.Panel();
            this.lblBuscar = new System.Windows.Forms.Label();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.grpListado = new System.Windows.Forms.GroupBox();
            this.dgvUsuarios = new System.Windows.Forms.DataGridView();
            this.colIdUsuario = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNombreUsuario = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNombreRol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlAcciones = new System.Windows.Forms.Panel();
            this.lblCantidad = new System.Windows.Forms.Label();
            this.btnRecargar = new System.Windows.Forms.Button();
            this.btnRegistrarPersona = new System.Windows.Forms.Button();
            this.btnVolver = new System.Windows.Forms.Button();
            this.pnlEncabezado.SuspendLayout();
            this.pnlBusqueda.SuspendLayout();
            this.grpListado.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsuarios)).BeginInit();
            this.pnlAcciones.SuspendLayout();
            this.SuspendLayout();
            // Encabezado
            this.pnlEncabezado.BackColor = System.Drawing.Color.DarkSlateBlue;
            this.pnlEncabezado.Controls.Add(this.lblTitulo);
            this.pnlEncabezado.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlEncabezado.Height = 86;
            this.pnlEncabezado.Name = "pnlEncabezado";
            this.lblTitulo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitulo.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Text = "USUARIOS REGISTRADOS";
            this.lblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // Buscador
            this.pnlBusqueda.Size = new System.Drawing.Size(900, 64);
            this.pnlBusqueda.Controls.Add(this.lblBuscar);
            this.pnlBusqueda.Controls.Add(this.txtBuscar);
            this.pnlBusqueda.Controls.Add(this.btnBuscar);
            this.pnlBusqueda.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblBuscar.AutoSize = true;
            this.lblBuscar.Location = new System.Drawing.Point(18, 24);
            this.lblBuscar.Text = "Buscar:";
            this.txtBuscar.Location = new System.Drawing.Point(80, 21);
            this.txtBuscar.Size = new System.Drawing.Size(680, 22);
            this.txtBuscar.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.txtBuscar.MaxLength = 50;
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.TabIndex = 0;
            this.btnBuscar.Location = new System.Drawing.Point(780, 14);
            this.btnBuscar.Size = new System.Drawing.Size(100, 36);
            this.btnBuscar.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.btnBuscar.BackColor = System.Drawing.Color.Turquoise;
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.TabIndex = 1;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = false;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // Listado
            this.grpListado.Controls.Add(this.dgvUsuarios);
            this.grpListado.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpListado.Name = "grpListado";
            this.grpListado.Padding = new System.Windows.Forms.Padding(12);
            this.grpListado.Text = "Cuentas registradas y sus roles";
            this.dgvUsuarios.AllowUserToAddRows = false;
            this.dgvUsuarios.AllowUserToDeleteRows = false;
            this.dgvUsuarios.AutoGenerateColumns = false;
            this.dgvUsuarios.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvUsuarios.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUsuarios.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                this.colIdUsuario, this.colNombreUsuario, this.colNombreRol });
            this.dgvUsuarios.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvUsuarios.Name = "dgvUsuarios";
            this.dgvUsuarios.ReadOnly = true;
            this.dgvUsuarios.TabIndex = 2;
            this.colIdUsuario.DataPropertyName = "IdUsuario";
            this.colIdUsuario.FillWeight = 25F;
            this.colIdUsuario.HeaderText = "ID";
            this.colIdUsuario.Name = "colIdUsuario";
            this.colNombreUsuario.DataPropertyName = "NombreUsuario";
            this.colNombreUsuario.HeaderText = "Usuario";
            this.colNombreUsuario.Name = "colNombreUsuario";
            this.colNombreRol.DataPropertyName = "NombreRol";
            this.colNombreRol.FillWeight = 70F;
            this.colNombreRol.HeaderText = "Rol";
            this.colNombreRol.Name = "colNombreRol";
            // Acciones
            this.pnlAcciones.Size = new System.Drawing.Size(900, 58);
            this.pnlAcciones.Controls.Add(this.lblCantidad);
            this.pnlAcciones.Controls.Add(this.btnRecargar);
            this.pnlAcciones.Controls.Add(this.btnRegistrarPersona);
            this.pnlAcciones.Controls.Add(this.btnVolver);
            this.pnlAcciones.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblCantidad.AutoSize = true;
            this.lblCantidad.Location = new System.Drawing.Point(18, 22);
            this.lblCantidad.Name = "lblCantidad";
            this.lblCantidad.Text = "Usuarios mostrados: 0";
            this.btnRecargar.Location = new System.Drawing.Point(640, 12);
            this.btnRecargar.Size = new System.Drawing.Size(110, 36);
            this.btnRecargar.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.btnRecargar.Name = "btnRecargar";
            this.btnRecargar.TabIndex = 4;
            this.btnRecargar.Text = "Recargar";
            this.btnRecargar.Click += new System.EventHandler(this.btnRecargar_Click);
            this.btnRegistrarPersona.Location = new System.Drawing.Point(470, 12);
            this.btnRegistrarPersona.Size = new System.Drawing.Size(155, 36);
            this.btnRegistrarPersona.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.btnRegistrarPersona.Name = "btnRegistrarPersona";
            this.btnRegistrarPersona.TabIndex = 3;
            this.btnRegistrarPersona.Text = "Registrar persona";
            this.btnRegistrarPersona.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.btnRegistrarPersona.Click += new System.EventHandler(this.btnRegistrarPersona_Click);
            this.btnVolver.Location = new System.Drawing.Point(770, 12);
            this.btnVolver.Size = new System.Drawing.Size(110, 36);
            this.btnVolver.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.btnVolver.Name = "btnVolver";
            this.btnVolver.TabIndex = 5;
            this.btnVolver.Text = "Volver";
            this.btnVolver.Click += new System.EventHandler(this.btnVolver_Click);
            // Formulario
            this.AcceptButton = this.btnBuscar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(900, 560);
            this.Controls.Add(this.grpListado);
            this.Controls.Add(this.pnlAcciones);
            this.Controls.Add(this.pnlBusqueda);
            this.Controls.Add(this.pnlEncabezado);
            this.MinimumSize = new System.Drawing.Size(800, 420);
            this.Name = "FrmUsuarios";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Administrar usuarios";
            this.pnlEncabezado.ResumeLayout(false);
            this.pnlBusqueda.ResumeLayout(false);
            this.pnlBusqueda.PerformLayout();
            this.grpListado.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsuarios)).EndInit();
            this.pnlAcciones.ResumeLayout(false);
            this.pnlAcciones.PerformLayout();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel pnlEncabezado;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Panel pnlBusqueda;
        private System.Windows.Forms.Label lblBuscar;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.GroupBox grpListado;
        private System.Windows.Forms.DataGridView dgvUsuarios;
        private System.Windows.Forms.DataGridViewTextBoxColumn colIdUsuario;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNombreUsuario;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNombreRol;
        private System.Windows.Forms.Panel pnlAcciones;
        private System.Windows.Forms.Label lblCantidad;
        private System.Windows.Forms.Button btnRecargar;
        private System.Windows.Forms.Button btnRegistrarPersona;
        private System.Windows.Forms.Button btnVolver;
    }
}
