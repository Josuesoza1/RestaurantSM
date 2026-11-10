namespace SistemaPedidoRestaurante.UIAdministracion
{
    partial class FrmClientes
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmClientes));
            this.pnlEncabezado = new System.Windows.Forms.Panel();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.pnlOperacion = new System.Windows.Forms.Panel();
            this.lblOperacion = new System.Windows.Forms.Label();
            this.grpDatosClientes = new System.Windows.Forms.GroupBox();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.txtSnombre = new System.Windows.Forms.TextBox();
            this.txtCedula = new System.Windows.Forms.TextBox();
            this.txtSapellido = new System.Windows.Forms.TextBox();
            this.txtPapellido = new System.Windows.Forms.TextBox();
            this.txtTelefono = new System.Windows.Forms.TextBox();
            this.txtPnombre = new System.Windows.Forms.TextBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.lblTelefono = new System.Windows.Forms.Label();
            this.lblCedula = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblSnombre = new System.Windows.Forms.Label();
            this.lblPnombre = new System.Windows.Forms.Label();
            this.cmbGenero = new System.Windows.Forms.ComboBox();
            this.lblGenero = new System.Windows.Forms.Label();
            this.chkActivo = new System.Windows.Forms.CheckBox();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.grpListadoCliente = new System.Windows.Forms.GroupBox();
            this.dgvClientes = new System.Windows.Forms.DataGridView();
            this.ColID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColPnombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColSnombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColPapellido = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColSapellido = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Colcedula = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColTelefono = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColEmail = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtTextoBuscar = new System.Windows.Forms.TextBox();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.lblBuscar = new System.Windows.Forms.Label();
            this.grpAccionesClientes = new System.Windows.Forms.GroupBox();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.agregarMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.editarMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.desactivarMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.eliminarMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.volverMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlEncabezado.SuspendLayout();
            this.pnlOperacion.SuspendLayout();
            this.grpDatosClientes.SuspendLayout();
            this.grpListadoCliente.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvClientes)).BeginInit();
            this.grpAccionesClientes.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlEncabezado
            // 
            this.pnlEncabezado.BackColor = System.Drawing.Color.Red;
            this.pnlEncabezado.Controls.Add(this.lblTitulo);
            this.pnlEncabezado.Controls.Add(this.pnlOperacion);
            this.pnlEncabezado.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlEncabezado.Location = new System.Drawing.Point(0, 24);
            this.pnlEncabezado.Name = "pnlEncabezado";
            this.pnlEncabezado.Size = new System.Drawing.Size(704, 96);
            this.pnlEncabezado.TabIndex = 0;
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Location = new System.Drawing.Point(48, 20);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(287, 29);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "GESTION DE CLIENTES";
            // 
            // pnlOperacion
            // 
            this.pnlOperacion.BackColor = System.Drawing.Color.Gainsboro;
            this.pnlOperacion.Controls.Add(this.lblOperacion);
            this.pnlOperacion.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlOperacion.Location = new System.Drawing.Point(0, 70);
            this.pnlOperacion.Name = "pnlOperacion";
            this.pnlOperacion.Size = new System.Drawing.Size(704, 26);
            this.pnlOperacion.TabIndex = 1;
            // 
            // lblOperacion
            // 
            this.lblOperacion.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.lblOperacion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblOperacion.Location = new System.Drawing.Point(0, 0);
            this.lblOperacion.Name = "lblOperacion";
            this.lblOperacion.Size = new System.Drawing.Size(704, 26);
            this.lblOperacion.TabIndex = 0;
            this.lblOperacion.Text = "Operación actual: Seleccione una operación";
            this.lblOperacion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // grpDatosClientes
            // 
            this.grpDatosClientes.Controls.Add(this.txtEmail);
            this.grpDatosClientes.Controls.Add(this.txtSnombre);
            this.grpDatosClientes.Controls.Add(this.txtCedula);
            this.grpDatosClientes.Controls.Add(this.txtSapellido);
            this.grpDatosClientes.Controls.Add(this.txtPapellido);
            this.grpDatosClientes.Controls.Add(this.txtTelefono);
            this.grpDatosClientes.Controls.Add(this.txtPnombre);
            this.grpDatosClientes.Controls.Add(this.lblEmail);
            this.grpDatosClientes.Controls.Add(this.lblTelefono);
            this.grpDatosClientes.Controls.Add(this.lblCedula);
            this.grpDatosClientes.Controls.Add(this.label4);
            this.grpDatosClientes.Controls.Add(this.label3);
            this.grpDatosClientes.Controls.Add(this.lblSnombre);
            this.grpDatosClientes.Controls.Add(this.lblPnombre);
            this.grpDatosClientes.Controls.Add(this.cmbGenero);
            this.grpDatosClientes.Controls.Add(this.lblGenero);
            this.grpDatosClientes.Controls.Add(this.chkActivo);
            this.grpDatosClientes.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpDatosClientes.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.grpDatosClientes.Location = new System.Drawing.Point(22, 126);
            this.grpDatosClientes.Name = "grpDatosClientes";
            this.grpDatosClientes.Size = new System.Drawing.Size(333, 296);
            this.grpDatosClientes.TabIndex = 1;
            this.grpDatosClientes.TabStop = false;
            this.grpDatosClientes.Text = "Datos del Cliente";
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(131, 206);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(149, 20);
            this.txtEmail.TabIndex = 13;
            // 
            // txtSnombre
            // 
            this.txtSnombre.Location = new System.Drawing.Point(131, 54);
            this.txtSnombre.Name = "txtSnombre";
            this.txtSnombre.Size = new System.Drawing.Size(149, 20);
            this.txtSnombre.TabIndex = 12;
            // 
            // txtCedula
            // 
            this.txtCedula.Location = new System.Drawing.Point(131, 145);
            this.txtCedula.Name = "txtCedula";
            this.txtCedula.Size = new System.Drawing.Size(149, 20);
            this.txtCedula.TabIndex = 11;
            // 
            // txtSapellido
            // 
            this.txtSapellido.Location = new System.Drawing.Point(131, 112);
            this.txtSapellido.Name = "txtSapellido";
            this.txtSapellido.Size = new System.Drawing.Size(149, 20);
            this.txtSapellido.TabIndex = 10;
            // 
            // txtPapellido
            // 
            this.txtPapellido.Location = new System.Drawing.Point(131, 82);
            this.txtPapellido.Name = "txtPapellido";
            this.txtPapellido.Size = new System.Drawing.Size(149, 20);
            this.txtPapellido.TabIndex = 9;
            // 
            // txtTelefono
            // 
            this.txtTelefono.Location = new System.Drawing.Point(131, 173);
            this.txtTelefono.Name = "txtTelefono";
            this.txtTelefono.Size = new System.Drawing.Size(149, 20);
            this.txtTelefono.TabIndex = 8;
            // 
            // txtPnombre
            // 
            this.txtPnombre.Location = new System.Drawing.Point(131, 24);
            this.txtPnombre.Name = "txtPnombre";
            this.txtPnombre.Size = new System.Drawing.Size(149, 20);
            this.txtPnombre.TabIndex = 7;
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblEmail.Location = new System.Drawing.Point(21, 209);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(39, 14);
            this.lblEmail.TabIndex = 6;
            this.lblEmail.Text = "Email:";
            // 
            // lblTelefono
            // 
            this.lblTelefono.AutoSize = true;
            this.lblTelefono.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblTelefono.Location = new System.Drawing.Point(21, 176);
            this.lblTelefono.Name = "lblTelefono";
            this.lblTelefono.Size = new System.Drawing.Size(58, 14);
            this.lblTelefono.TabIndex = 5;
            this.lblTelefono.Text = "Telefono:";
            // 
            // lblCedula
            // 
            this.lblCedula.AutoSize = true;
            this.lblCedula.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblCedula.Location = new System.Drawing.Point(21, 148);
            this.lblCedula.Name = "lblCedula";
            this.lblCedula.Size = new System.Drawing.Size(48, 14);
            this.lblCedula.TabIndex = 4;
            this.lblCedula.Text = "Cédula:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label4.Location = new System.Drawing.Point(21, 115);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(104, 14);
            this.label4.TabIndex = 3;
            this.label4.Text = "Segundo Apelido:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label3.Location = new System.Drawing.Point(19, 85);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 14);
            this.label3.TabIndex = 2;
            this.label3.Text = "Primer Apellido:";
            // 
            // lblSnombre
            // 
            this.lblSnombre.AutoSize = true;
            this.lblSnombre.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblSnombre.Location = new System.Drawing.Point(19, 57);
            this.lblSnombre.Name = "lblSnombre";
            this.lblSnombre.Size = new System.Drawing.Size(106, 14);
            this.lblSnombre.TabIndex = 1;
            this.lblSnombre.Text = "Segundo Nombre:";
            // 
            // lblPnombre
            // 
            this.lblPnombre.AutoSize = true;
            this.lblPnombre.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblPnombre.Location = new System.Drawing.Point(19, 27);
            this.lblPnombre.Name = "lblPnombre";
            this.lblPnombre.Size = new System.Drawing.Size(95, 14);
            this.lblPnombre.TabIndex = 0;
            this.lblPnombre.Text = "Primer Nombre:";
            // 
            // cmbGenero
            // 
            this.cmbGenero.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGenero.Items.AddRange(new object[] {
            "M",
            "F",
            "O"});
            this.cmbGenero.Location = new System.Drawing.Point(131, 235);
            this.cmbGenero.Name = "cmbGenero";
            this.cmbGenero.Size = new System.Drawing.Size(149, 22);
            this.cmbGenero.TabIndex = 14;
            // 
            // lblGenero
            // 
            this.lblGenero.AutoSize = true;
            this.lblGenero.Location = new System.Drawing.Point(19, 240);
            this.lblGenero.Name = "lblGenero";
            this.lblGenero.Size = new System.Drawing.Size(51, 14);
            this.lblGenero.TabIndex = 15;
            this.lblGenero.Text = "Género:";
            // 
            // chkActivo
            // 
            this.chkActivo.Checked = true;
            this.chkActivo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkActivo.Location = new System.Drawing.Point(131, 265);
            this.chkActivo.Name = "chkActivo";
            this.chkActivo.Size = new System.Drawing.Size(140, 22);
            this.chkActivo.TabIndex = 16;
            this.chkActivo.Text = "Activo";
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnLimpiar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLimpiar.Font = new System.Drawing.Font("Arial Black", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLimpiar.ForeColor = System.Drawing.Color.Black;
            this.btnLimpiar.Location = new System.Drawing.Point(112, 156);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(84, 33);
            this.btnLimpiar.TabIndex = 4;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.UseVisualStyleBackColor = false;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.BackColor = System.Drawing.Color.Lime;
            this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardar.Font = new System.Drawing.Font("Arial Black", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardar.ForeColor = System.Drawing.Color.Black;
            this.btnGuardar.Location = new System.Drawing.Point(16, 156);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(90, 33);
            this.btnGuardar.TabIndex = 1;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // grpListadoCliente
            // 
            this.grpListadoCliente.Controls.Add(this.dgvClientes);
            this.grpListadoCliente.ForeColor = System.Drawing.Color.DarkOrchid;
            this.grpListadoCliente.Location = new System.Drawing.Point(12, 428);
            this.grpListadoCliente.Name = "grpListadoCliente";
            this.grpListadoCliente.Size = new System.Drawing.Size(669, 122);
            this.grpListadoCliente.TabIndex = 3;
            this.grpListadoCliente.TabStop = false;
            this.grpListadoCliente.Text = "Listado de Clientes";
            // 
            // dgvClientes
            // 
            this.dgvClientes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvClientes.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColID,
            this.ColPnombre,
            this.ColSnombre,
            this.ColPapellido,
            this.ColSapellido,
            this.Colcedula,
            this.ColTelefono,
            this.ColEmail});
            this.dgvClientes.GridColor = System.Drawing.SystemColors.AppWorkspace;
            this.dgvClientes.Location = new System.Drawing.Point(10, 19);
            this.dgvClientes.Name = "dgvClientes";
            this.dgvClientes.ReadOnly = true;
            this.dgvClientes.RowHeadersWidth = 51;
            this.dgvClientes.Size = new System.Drawing.Size(642, 97);
            this.dgvClientes.TabIndex = 0;
            this.dgvClientes.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvClientes_CellClick);
            // 
            // ColID
            // 
            this.ColID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColID.HeaderText = "ID";
            this.ColID.MinimumWidth = 6;
            this.ColID.Name = "ColID";
            this.ColID.ReadOnly = true;
            // 
            // ColPnombre
            // 
            this.ColPnombre.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColPnombre.HeaderText = "Primer Nombre";
            this.ColPnombre.MinimumWidth = 6;
            this.ColPnombre.Name = "ColPnombre";
            this.ColPnombre.ReadOnly = true;
            // 
            // ColSnombre
            // 
            this.ColSnombre.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColSnombre.HeaderText = "Segundo Nombre";
            this.ColSnombre.MinimumWidth = 6;
            this.ColSnombre.Name = "ColSnombre";
            this.ColSnombre.ReadOnly = true;
            // 
            // ColPapellido
            // 
            this.ColPapellido.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColPapellido.HeaderText = "Primer Apellido";
            this.ColPapellido.MinimumWidth = 6;
            this.ColPapellido.Name = "ColPapellido";
            this.ColPapellido.ReadOnly = true;
            // 
            // ColSapellido
            // 
            this.ColSapellido.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColSapellido.HeaderText = "Segundo Apellido";
            this.ColSapellido.MinimumWidth = 6;
            this.ColSapellido.Name = "ColSapellido";
            this.ColSapellido.ReadOnly = true;
            // 
            // Colcedula
            // 
            this.Colcedula.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Colcedula.HeaderText = "Cedula";
            this.Colcedula.MinimumWidth = 6;
            this.Colcedula.Name = "Colcedula";
            this.Colcedula.ReadOnly = true;
            // 
            // ColTelefono
            // 
            this.ColTelefono.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColTelefono.HeaderText = "Telefono";
            this.ColTelefono.MinimumWidth = 6;
            this.ColTelefono.Name = "ColTelefono";
            this.ColTelefono.ReadOnly = true;
            // 
            // ColEmail
            // 
            this.ColEmail.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColEmail.HeaderText = "Email";
            this.ColEmail.MinimumWidth = 6;
            this.ColEmail.Name = "ColEmail";
            this.ColEmail.ReadOnly = true;
            // 
            // txtTextoBuscar
            // 
            this.txtTextoBuscar.Location = new System.Drawing.Point(22, 51);
            this.txtTextoBuscar.Name = "txtTextoBuscar";
            this.txtTextoBuscar.Size = new System.Drawing.Size(266, 20);
            this.txtTextoBuscar.TabIndex = 9;
            // 
            // btnBuscar
            // 
            this.btnBuscar.BackColor = System.Drawing.Color.DarkTurquoise;
            this.btnBuscar.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscar.ForeColor = System.Drawing.Color.Blue;
            this.btnBuscar.Location = new System.Drawing.Point(107, 85);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(89, 31);
            this.btnBuscar.TabIndex = 8;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = false;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // lblBuscar
            // 
            this.lblBuscar.AutoSize = true;
            this.lblBuscar.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBuscar.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblBuscar.Location = new System.Drawing.Point(19, 24);
            this.lblBuscar.Name = "lblBuscar";
            this.lblBuscar.Size = new System.Drawing.Size(48, 14);
            this.lblBuscar.TabIndex = 5;
            this.lblBuscar.Text = "Buscar:";
            // 
            // grpAccionesClientes
            // 
            this.grpAccionesClientes.Controls.Add(this.btnGuardar);
            this.grpAccionesClientes.Controls.Add(this.btnBuscar);
            this.grpAccionesClientes.Controls.Add(this.txtTextoBuscar);
            this.grpAccionesClientes.Controls.Add(this.lblBuscar);
            this.grpAccionesClientes.Controls.Add(this.btnLimpiar);
            this.grpAccionesClientes.Controls.Add(this.btnCancelar);
            this.grpAccionesClientes.Location = new System.Drawing.Point(361, 126);
            this.grpAccionesClientes.Name = "grpAccionesClientes";
            this.grpAccionesClientes.Size = new System.Drawing.Size(303, 236);
            this.grpAccionesClientes.TabIndex = 10;
            this.grpAccionesClientes.TabStop = false;
            this.grpAccionesClientes.Text = "Acciones";
            this.grpAccionesClientes.Enter += new System.EventHandler(this.grpAccionesClientes_Enter);
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnCancelar.Enabled = false;
            this.btnCancelar.Location = new System.Drawing.Point(202, 154);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(86, 37);
            this.btnCancelar.TabIndex = 5;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.agregarMenu,
            this.editarMenu,
            this.desactivarMenu,
            this.eliminarMenu,
            this.volverMenu});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(704, 24);
            this.menuStrip1.TabIndex = 11;
            // 
            // agregarMenu
            // 
            this.agregarMenu.Image = global::SistemaPedidoRestaurante.Properties.Resources.Agregar;
            this.agregarMenu.Name = "agregarMenu";
            this.agregarMenu.Size = new System.Drawing.Size(77, 20);
            this.agregarMenu.Text = "Agregar";
            this.agregarMenu.Click += new System.EventHandler(this.agregarMenu_Click);
            // 
            // editarMenu
            // 
            this.editarMenu.Image = ((System.Drawing.Image)(resources.GetObject("editarMenu.Image")));
            this.editarMenu.Name = "editarMenu";
            this.editarMenu.Size = new System.Drawing.Size(65, 20);
            this.editarMenu.Text = "Editar";
            this.editarMenu.Click += new System.EventHandler(this.editarMenu_Click);
            // 
            // desactivarMenu
            // 
            this.desactivarMenu.Image = ((System.Drawing.Image)(resources.GetObject("desactivarMenu.Image")));
            this.desactivarMenu.Name = "desactivarMenu";
            this.desactivarMenu.Size = new System.Drawing.Size(89, 20);
            this.desactivarMenu.Text = "Desactivar";
            this.desactivarMenu.Click += new System.EventHandler(this.desactivarMenu_Click);
            // 
            // eliminarMenu
            // 
            this.eliminarMenu.Image = ((System.Drawing.Image)(resources.GetObject("eliminarMenu.Image")));
            this.eliminarMenu.Name = "eliminarMenu";
            this.eliminarMenu.Size = new System.Drawing.Size(78, 20);
            this.eliminarMenu.Text = "Eliminar";
            this.eliminarMenu.Click += new System.EventHandler(this.eliminarMenu_Click);
            // 
            // volverMenu
            // 
            this.volverMenu.Image = ((System.Drawing.Image)(resources.GetObject("volverMenu.Image")));
            this.volverMenu.Name = "volverMenu";
            this.volverMenu.Size = new System.Drawing.Size(67, 20);
            this.volverMenu.Text = "Volver";
            this.volverMenu.Click += new System.EventHandler(this.volverMenu_Click);
            // 
            // FrmClientes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.ClientSize = new System.Drawing.Size(704, 557);
            this.Controls.Add(this.grpAccionesClientes);
            this.Controls.Add(this.grpListadoCliente);
            this.Controls.Add(this.grpDatosClientes);
            this.Controls.Add(this.pnlEncabezado);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(720, 540);
            this.Name = "FrmClientes";
            this.Text = "Gestion de clientes";
            this.Load += new System.EventHandler(this.FrmClientes_CargarDatos);
            this.pnlEncabezado.ResumeLayout(false);
            this.pnlEncabezado.PerformLayout();
            this.pnlOperacion.ResumeLayout(false);
            this.grpDatosClientes.ResumeLayout(false);
            this.grpDatosClientes.PerformLayout();
            this.grpListadoCliente.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvClientes)).EndInit();
            this.grpAccionesClientes.ResumeLayout(false);
            this.grpAccionesClientes.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbGenero;
        private System.Windows.Forms.CheckBox chkActivo;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Label lblGenero;
        private System.Windows.Forms.Panel pnlEncabezado;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.GroupBox grpDatosClientes;
        private System.Windows.Forms.GroupBox grpListadoCliente;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.Label lblTelefono;
        private System.Windows.Forms.Label lblCedula;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblSnombre;
        private System.Windows.Forms.Label lblPnombre;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.TextBox txtSnombre;
        private System.Windows.Forms.TextBox txtCedula;
        private System.Windows.Forms.TextBox txtSapellido;
        private System.Windows.Forms.TextBox txtPapellido;
        private System.Windows.Forms.TextBox txtTelefono;
        private System.Windows.Forms.TextBox txtPnombre;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Label lblBuscar;
        private System.Windows.Forms.DataGridView dgvClientes;
        private System.Windows.Forms.TextBox txtTextoBuscar;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.GroupBox grpAccionesClientes;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColPnombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColSnombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColPapellido;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColSapellido;
        private System.Windows.Forms.DataGridViewTextBoxColumn Colcedula;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColTelefono;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColEmail;

        private System.Windows.Forms.Panel pnlOperacion;
        private System.Windows.Forms.Label lblOperacion;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem editarMenu;
        private System.Windows.Forms.ToolStripMenuItem eliminarMenu;
        private System.Windows.Forms.ToolStripMenuItem desactivarMenu;
        private System.Windows.Forms.ToolStripMenuItem agregarMenu;
        private System.Windows.Forms.ToolStripMenuItem volverMenu;
    }
}

