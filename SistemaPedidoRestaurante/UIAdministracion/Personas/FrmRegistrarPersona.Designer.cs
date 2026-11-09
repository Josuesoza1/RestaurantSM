namespace SistemaPedidoRestaurante.UIAdministracion
{
    partial class FrmRegistrarPersona
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
            this.pnlTipo = new System.Windows.Forms.Panel();
            this.lblTipo = new System.Windows.Forms.Label();
            this.cmbTipo = new System.Windows.Forms.ComboBox();
            this.chkPersonaExistente = new System.Windows.Forms.CheckBox();
            this.cmbPersona = new System.Windows.Forms.ComboBox();
            this.lblIndicacion = new System.Windows.Forms.Label();
            this.tblContenido = new System.Windows.Forms.TableLayoutPanel();
            this.grpPersona = new System.Windows.Forms.GroupBox();
            this.tblPersona = new System.Windows.Forms.TableLayoutPanel();
            this.lblPrimerNombre = new System.Windows.Forms.Label();
            this.txtPrimerNombre = new System.Windows.Forms.TextBox();
            this.lblSegundoNombre = new System.Windows.Forms.Label();
            this.txtSegundoNombre = new System.Windows.Forms.TextBox();
            this.lblPrimerApellido = new System.Windows.Forms.Label();
            this.txtPrimerApellido = new System.Windows.Forms.TextBox();
            this.lblSegundoApellido = new System.Windows.Forms.Label();
            this.txtSegundoApellido = new System.Windows.Forms.TextBox();
            this.lblGenero = new System.Windows.Forms.Label();
            this.cmbGenero = new System.Windows.Forms.ComboBox();
            this.lblCedula = new System.Windows.Forms.Label();
            this.txtCedula = new System.Windows.Forms.TextBox();
            this.lblTelefono = new System.Windows.Forms.Label();
            this.txtTelefono = new System.Windows.Forms.TextBox();
            this.lblCargo = new System.Windows.Forms.Label();
            this.cmbCargo = new System.Windows.Forms.ComboBox();
            this.lblFormato = new System.Windows.Forms.Label();
            this.grpCuenta = new System.Windows.Forms.GroupBox();
            this.tblCuenta = new System.Windows.Forms.TableLayoutPanel();
            this.lblUsuario = new System.Windows.Forms.Label();
            this.txtUsuario = new System.Windows.Forms.TextBox();
            this.lblContrasena = new System.Windows.Forms.Label();
            this.txtContrasena = new System.Windows.Forms.TextBox();
            this.lblConfirmar = new System.Windows.Forms.Label();
            this.txtConfirmar = new System.Windows.Forms.TextBox();
            this.chkMostrar = new System.Windows.Forms.CheckBox();
            this.lblCuenta = new System.Windows.Forms.Label();
            this.pnlAcciones = new System.Windows.Forms.FlowLayoutPanel();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.pnlEncabezado.SuspendLayout();
            this.pnlTipo.SuspendLayout();
            this.tblContenido.SuspendLayout();
            this.grpPersona.SuspendLayout();
            this.tblPersona.SuspendLayout();
            this.grpCuenta.SuspendLayout();
            this.tblCuenta.SuspendLayout();
            this.pnlAcciones.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlEncabezado
            // 
            this.pnlEncabezado.BackColor = System.Drawing.Color.DarkSlateBlue;
            this.pnlEncabezado.Controls.Add(this.lblTitulo);
            this.pnlEncabezado.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlEncabezado.Location = new System.Drawing.Point(0, 0);
            this.pnlEncabezado.Name = "pnlEncabezado";
            this.pnlEncabezado.Size = new System.Drawing.Size(840, 72);
            this.pnlEncabezado.TabIndex = 4;
            // 
            // lblTitulo
            // 
            this.lblTitulo.BackColor = System.Drawing.Color.Red;
            this.lblTitulo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitulo.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Location = new System.Drawing.Point(0, 0);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(840, 72);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "ALTA DE PERSONAL";
            this.lblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlTipo
            // 
            this.pnlTipo.Controls.Add(this.lblTipo);
            this.pnlTipo.Controls.Add(this.cmbTipo);
            this.pnlTipo.Controls.Add(this.chkPersonaExistente);
            this.pnlTipo.Controls.Add(this.cmbPersona);
            this.pnlTipo.Controls.Add(this.lblIndicacion);
            this.pnlTipo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTipo.Location = new System.Drawing.Point(0, 72);
            this.pnlTipo.Name = "pnlTipo";
            this.pnlTipo.Size = new System.Drawing.Size(840, 124);
            this.pnlTipo.TabIndex = 3;
            // 
            // lblTipo
            // 
            this.lblTipo.AutoSize = true;
            this.lblTipo.Location = new System.Drawing.Point(16, 17);
            this.lblTipo.Name = "lblTipo";
            this.lblTipo.Size = new System.Drawing.Size(86, 13);
            this.lblTipo.TabIndex = 0;
            this.lblTipo.Text = "Rol de acceso *:";
            // 
            // cmbTipo
            // 
            this.cmbTipo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTipo.Items.AddRange(new object[] {
            "Empleado",
            "Administrador"});
            this.cmbTipo.Location = new System.Drawing.Point(156, 13);
            this.cmbTipo.Name = "cmbTipo";
            this.cmbTipo.Size = new System.Drawing.Size(240, 21);
            this.cmbTipo.TabIndex = 0;
            this.cmbTipo.SelectedIndexChanged += new System.EventHandler(this.cmbTipo_SelectedIndexChanged);
            // 
            // chkPersonaExistente
            // 
            this.chkPersonaExistente.AutoSize = true;
            this.chkPersonaExistente.Location = new System.Drawing.Point(16, 49);
            this.chkPersonaExistente.Name = "chkPersonaExistente";
            this.chkPersonaExistente.Size = new System.Drawing.Size(173, 17);
            this.chkPersonaExistente.TabIndex = 1;
            this.chkPersonaExistente.Text = "Usar una persona ya registrada";
            this.chkPersonaExistente.CheckedChanged += new System.EventHandler(this.chkPersonaExistente_CheckedChanged);
            // 
            // cmbPersona
            // 
            this.cmbPersona.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbPersona.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPersona.Enabled = false;
            this.cmbPersona.FormattingEnabled = true;
            this.cmbPersona.Location = new System.Drawing.Point(242, 46);
            this.cmbPersona.Name = "cmbPersona";
            this.cmbPersona.Size = new System.Drawing.Size(574, 21);
            this.cmbPersona.TabIndex = 2;
            this.cmbPersona.SelectedIndexChanged += new System.EventHandler(this.cmbPersona_SelectedIndexChanged);
            this.cmbPersona.Format += new System.Windows.Forms.ListControlConvertEventHandler(this.cmbPersona_Format);
            // 
            // lblIndicacion
            // 
            this.lblIndicacion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblIndicacion.Location = new System.Drawing.Point(16, 83);
            this.lblIndicacion.Name = "lblIndicacion";
            this.lblIndicacion.Size = new System.Drawing.Size(806, 36);
            this.lblIndicacion.TabIndex = 3;
            this.lblIndicacion.Text = "Los campos con * son obligatorios.";
            // 
            // tblContenido
            // 
            this.tblContenido.ColumnCount = 2;
            this.tblContenido.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 57F));
            this.tblContenido.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 43F));
            this.tblContenido.Controls.Add(this.grpPersona, 0, 0);
            this.tblContenido.Controls.Add(this.grpCuenta, 1, 0);
            this.tblContenido.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblContenido.Location = new System.Drawing.Point(0, 196);
            this.tblContenido.Name = "tblContenido";
            this.tblContenido.Padding = new System.Windows.Forms.Padding(12, 0, 12, 8);
            this.tblContenido.RowCount = 1;
            this.tblContenido.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblContenido.Size = new System.Drawing.Size(840, 385);
            this.tblContenido.TabIndex = 1;
            // 
            // grpPersona
            // 
            this.grpPersona.BackColor = System.Drawing.Color.DarkOrange;
            this.grpPersona.Controls.Add(this.tblPersona);
            this.grpPersona.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpPersona.Enabled = false;
            this.grpPersona.Location = new System.Drawing.Point(15, 3);
            this.grpPersona.Name = "grpPersona";
            this.grpPersona.Padding = new System.Windows.Forms.Padding(12, 22, 12, 8);
            this.grpPersona.Size = new System.Drawing.Size(459, 371);
            this.grpPersona.TabIndex = 0;
            this.grpPersona.TabStop = false;
            this.grpPersona.Text = "Datos personales";
            // 
            // tblPersona
            // 
            this.tblPersona.ColumnCount = 2;
            this.tblPersona.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 145F));
            this.tblPersona.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblPersona.Controls.Add(this.lblPrimerNombre, 0, 0);
            this.tblPersona.Controls.Add(this.txtPrimerNombre, 1, 0);
            this.tblPersona.Controls.Add(this.lblSegundoNombre, 0, 1);
            this.tblPersona.Controls.Add(this.txtSegundoNombre, 1, 1);
            this.tblPersona.Controls.Add(this.lblPrimerApellido, 0, 2);
            this.tblPersona.Controls.Add(this.txtPrimerApellido, 1, 2);
            this.tblPersona.Controls.Add(this.lblSegundoApellido, 0, 3);
            this.tblPersona.Controls.Add(this.txtSegundoApellido, 1, 3);
            this.tblPersona.Controls.Add(this.lblGenero, 0, 4);
            this.tblPersona.Controls.Add(this.cmbGenero, 1, 4);
            this.tblPersona.Controls.Add(this.lblCedula, 0, 5);
            this.tblPersona.Controls.Add(this.txtCedula, 1, 5);
            this.tblPersona.Controls.Add(this.lblTelefono, 0, 6);
            this.tblPersona.Controls.Add(this.txtTelefono, 1, 6);
            this.tblPersona.Controls.Add(this.lblCargo, 0, 7);
            this.tblPersona.Controls.Add(this.cmbCargo, 1, 7);
            this.tblPersona.Controls.Add(this.lblFormato, 0, 8);
            this.tblPersona.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblPersona.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.tblPersona.Location = new System.Drawing.Point(12, 35);
            this.tblPersona.Name = "tblPersona";
            this.tblPersona.RowCount = 9;
            this.tblPersona.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.tblPersona.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.tblPersona.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.tblPersona.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.tblPersona.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.tblPersona.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.tblPersona.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.tblPersona.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.tblPersona.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblPersona.Size = new System.Drawing.Size(435, 328);
            this.tblPersona.TabIndex = 0;
            // 
            // lblPrimerNombre
            // 
            this.lblPrimerNombre.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPrimerNombre.Location = new System.Drawing.Point(3, 0);
            this.lblPrimerNombre.Name = "lblPrimerNombre";
            this.lblPrimerNombre.Size = new System.Drawing.Size(139, 34);
            this.lblPrimerNombre.TabIndex = 0;
            this.lblPrimerNombre.Text = "Primer nombre *:";
            this.lblPrimerNombre.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtPrimerNombre
            // 
            this.txtPrimerNombre.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPrimerNombre.Location = new System.Drawing.Point(148, 5);
            this.txtPrimerNombre.MaxLength = 80;
            this.txtPrimerNombre.Name = "txtPrimerNombre";
            this.txtPrimerNombre.Size = new System.Drawing.Size(284, 23);
            this.txtPrimerNombre.TabIndex = 0;
            // 
            // lblSegundoNombre
            // 
            this.lblSegundoNombre.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSegundoNombre.Location = new System.Drawing.Point(3, 34);
            this.lblSegundoNombre.Name = "lblSegundoNombre";
            this.lblSegundoNombre.Size = new System.Drawing.Size(139, 34);
            this.lblSegundoNombre.TabIndex = 1;
            this.lblSegundoNombre.Text = "Segundo nombre:";
            this.lblSegundoNombre.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtSegundoNombre
            // 
            this.txtSegundoNombre.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSegundoNombre.Location = new System.Drawing.Point(148, 39);
            this.txtSegundoNombre.MaxLength = 80;
            this.txtSegundoNombre.Name = "txtSegundoNombre";
            this.txtSegundoNombre.Size = new System.Drawing.Size(284, 23);
            this.txtSegundoNombre.TabIndex = 1;
            // 
            // lblPrimerApellido
            // 
            this.lblPrimerApellido.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPrimerApellido.Location = new System.Drawing.Point(3, 68);
            this.lblPrimerApellido.Name = "lblPrimerApellido";
            this.lblPrimerApellido.Size = new System.Drawing.Size(139, 34);
            this.lblPrimerApellido.TabIndex = 2;
            this.lblPrimerApellido.Text = "Primer apellido *:";
            this.lblPrimerApellido.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtPrimerApellido
            // 
            this.txtPrimerApellido.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPrimerApellido.Location = new System.Drawing.Point(148, 73);
            this.txtPrimerApellido.MaxLength = 80;
            this.txtPrimerApellido.Name = "txtPrimerApellido";
            this.txtPrimerApellido.Size = new System.Drawing.Size(284, 23);
            this.txtPrimerApellido.TabIndex = 2;
            // 
            // lblSegundoApellido
            // 
            this.lblSegundoApellido.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSegundoApellido.Location = new System.Drawing.Point(3, 102);
            this.lblSegundoApellido.Name = "lblSegundoApellido";
            this.lblSegundoApellido.Size = new System.Drawing.Size(139, 34);
            this.lblSegundoApellido.TabIndex = 3;
            this.lblSegundoApellido.Text = "Segundo apellido:";
            this.lblSegundoApellido.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtSegundoApellido
            // 
            this.txtSegundoApellido.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSegundoApellido.Location = new System.Drawing.Point(148, 107);
            this.txtSegundoApellido.MaxLength = 80;
            this.txtSegundoApellido.Name = "txtSegundoApellido";
            this.txtSegundoApellido.Size = new System.Drawing.Size(284, 23);
            this.txtSegundoApellido.TabIndex = 3;
            // 
            // lblGenero
            // 
            this.lblGenero.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblGenero.Location = new System.Drawing.Point(3, 136);
            this.lblGenero.Name = "lblGenero";
            this.lblGenero.Size = new System.Drawing.Size(139, 34);
            this.lblGenero.TabIndex = 4;
            this.lblGenero.Text = "Género *:";
            this.lblGenero.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbGenero
            // 
            this.cmbGenero.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbGenero.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGenero.Items.AddRange(new object[] {
            "Masculino",
            "Femenino",
            "Otro"});
            this.cmbGenero.Location = new System.Drawing.Point(148, 142);
            this.cmbGenero.Name = "cmbGenero";
            this.cmbGenero.Size = new System.Drawing.Size(284, 23);
            this.cmbGenero.TabIndex = 4;
            // 
            // lblCedula
            // 
            this.lblCedula.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCedula.Location = new System.Drawing.Point(3, 170);
            this.lblCedula.Name = "lblCedula";
            this.lblCedula.Size = new System.Drawing.Size(139, 34);
            this.lblCedula.TabIndex = 5;
            this.lblCedula.Text = "Cédula *:";
            this.lblCedula.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtCedula
            // 
            this.txtCedula.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCedula.Location = new System.Drawing.Point(148, 175);
            this.txtCedula.MaxLength = 16;
            this.txtCedula.Name = "txtCedula";
            this.txtCedula.Size = new System.Drawing.Size(284, 23);
            this.txtCedula.TabIndex = 5;
            // 
            // lblTelefono
            // 
            this.lblTelefono.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTelefono.Location = new System.Drawing.Point(3, 204);
            this.lblTelefono.Name = "lblTelefono";
            this.lblTelefono.Size = new System.Drawing.Size(139, 34);
            this.lblTelefono.TabIndex = 6;
            this.lblTelefono.Text = "Teléfono *:";
            this.lblTelefono.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtTelefono
            // 
            this.txtTelefono.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTelefono.Location = new System.Drawing.Point(148, 209);
            this.txtTelefono.MaxLength = 20;
            this.txtTelefono.Name = "txtTelefono";
            this.txtTelefono.Size = new System.Drawing.Size(284, 23);
            this.txtTelefono.TabIndex = 6;
            // 
            // lblCargo
            // 
            this.lblCargo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCargo.Location = new System.Drawing.Point(3, 238);
            this.lblCargo.Name = "lblCargo";
            this.lblCargo.Size = new System.Drawing.Size(139, 34);
            this.lblCargo.TabIndex = 7;
            this.lblCargo.Text = "Cargo *:";
            this.lblCargo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbCargo
            // 
            this.cmbCargo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbCargo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCargo.Location = new System.Drawing.Point(148, 244);
            this.cmbCargo.Name = "cmbCargo";
            this.cmbCargo.Size = new System.Drawing.Size(284, 23);
            this.cmbCargo.TabIndex = 7;
            // 
            // lblFormato
            // 
            this.tblPersona.SetColumnSpan(this.lblFormato, 2);
            this.lblFormato.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblFormato.ForeColor = System.Drawing.Color.DimGray;
            this.lblFormato.Location = new System.Drawing.Point(3, 272);
            this.lblFormato.Name = "lblFormato";
            this.lblFormato.Size = new System.Drawing.Size(429, 56);
            this.lblFormato.TabIndex = 8;
            this.lblFormato.Text = "Cédula: 001-010100-0001A. Teléfono: 8 dígitos; admite prefijo internacional. Segu" +
    "ndo nombre y apellido son opcionales.";
            // 
            // grpCuenta
            // 
            this.grpCuenta.BackColor = System.Drawing.Color.DarkOrange;
            this.grpCuenta.Controls.Add(this.tblCuenta);
            this.grpCuenta.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpCuenta.Enabled = false;
            this.grpCuenta.Location = new System.Drawing.Point(480, 3);
            this.grpCuenta.Name = "grpCuenta";
            this.grpCuenta.Padding = new System.Windows.Forms.Padding(12, 22, 12, 8);
            this.grpCuenta.Size = new System.Drawing.Size(345, 371);
            this.grpCuenta.TabIndex = 1;
            this.grpCuenta.TabStop = false;
            this.grpCuenta.Text = "Cuenta de acceso";
            // 
            // tblCuenta
            // 
            this.tblCuenta.ColumnCount = 1;
            this.tblCuenta.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblCuenta.Controls.Add(this.lblUsuario, 0, 0);
            this.tblCuenta.Controls.Add(this.txtUsuario, 0, 1);
            this.tblCuenta.Controls.Add(this.lblContrasena, 0, 2);
            this.tblCuenta.Controls.Add(this.txtContrasena, 0, 3);
            this.tblCuenta.Controls.Add(this.lblConfirmar, 0, 4);
            this.tblCuenta.Controls.Add(this.txtConfirmar, 0, 5);
            this.tblCuenta.Controls.Add(this.chkMostrar, 0, 6);
            this.tblCuenta.Controls.Add(this.lblCuenta, 0, 7);
            this.tblCuenta.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblCuenta.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.tblCuenta.Location = new System.Drawing.Point(12, 35);
            this.tblCuenta.Name = "tblCuenta";
            this.tblCuenta.RowCount = 8;
            this.tblCuenta.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tblCuenta.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.tblCuenta.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tblCuenta.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.tblCuenta.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tblCuenta.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.tblCuenta.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.tblCuenta.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblCuenta.Size = new System.Drawing.Size(321, 328);
            this.tblCuenta.TabIndex = 0;
            // 
            // lblUsuario
            // 
            this.lblUsuario.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblUsuario.Location = new System.Drawing.Point(3, 0);
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.Size = new System.Drawing.Size(315, 24);
            this.lblUsuario.TabIndex = 0;
            this.lblUsuario.Text = "Usuario *:";
            // 
            // txtUsuario
            // 
            this.txtUsuario.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtUsuario.Location = new System.Drawing.Point(3, 29);
            this.txtUsuario.MaxLength = 50;
            this.txtUsuario.Name = "txtUsuario";
            this.txtUsuario.Size = new System.Drawing.Size(315, 23);
            this.txtUsuario.TabIndex = 0;
            // 
            // lblContrasena
            // 
            this.lblContrasena.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblContrasena.Location = new System.Drawing.Point(3, 58);
            this.lblContrasena.Name = "lblContrasena";
            this.lblContrasena.Size = new System.Drawing.Size(315, 24);
            this.lblContrasena.TabIndex = 1;
            this.lblContrasena.Text = "Contraseña *:";
            // 
            // txtContrasena
            // 
            this.txtContrasena.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtContrasena.Location = new System.Drawing.Point(3, 87);
            this.txtContrasena.MaxLength = 255;
            this.txtContrasena.Name = "txtContrasena";
            this.txtContrasena.Size = new System.Drawing.Size(315, 23);
            this.txtContrasena.TabIndex = 1;
            this.txtContrasena.UseSystemPasswordChar = true;
            // 
            // lblConfirmar
            // 
            this.lblConfirmar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblConfirmar.Location = new System.Drawing.Point(3, 116);
            this.lblConfirmar.Name = "lblConfirmar";
            this.lblConfirmar.Size = new System.Drawing.Size(315, 24);
            this.lblConfirmar.TabIndex = 2;
            this.lblConfirmar.Text = "Confirmar contraseña *:";
            // 
            // txtConfirmar
            // 
            this.txtConfirmar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtConfirmar.Location = new System.Drawing.Point(3, 145);
            this.txtConfirmar.MaxLength = 255;
            this.txtConfirmar.Name = "txtConfirmar";
            this.txtConfirmar.Size = new System.Drawing.Size(315, 23);
            this.txtConfirmar.TabIndex = 2;
            this.txtConfirmar.UseSystemPasswordChar = true;
            // 
            // chkMostrar
            // 
            this.chkMostrar.AutoSize = true;
            this.chkMostrar.Location = new System.Drawing.Point(3, 177);
            this.chkMostrar.Name = "chkMostrar";
            this.chkMostrar.Size = new System.Drawing.Size(133, 19);
            this.chkMostrar.TabIndex = 3;
            this.chkMostrar.Text = "Mostrar contraseñas";
            this.chkMostrar.CheckedChanged += new System.EventHandler(this.chkMostrar_CheckedChanged);
            // 
            // lblCuenta
            // 
            this.lblCuenta.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCuenta.ForeColor = System.Drawing.Color.DimGray;
            this.lblCuenta.Location = new System.Drawing.Point(3, 208);
            this.lblCuenta.Name = "lblCuenta";
            this.lblCuenta.Size = new System.Drawing.Size(315, 120);
            this.lblCuenta.TabIndex = 4;
            this.lblCuenta.Text = "Los datos personales y la cuenta de acceso se guardan juntos.";
            // 
            // pnlAcciones
            // 
            this.pnlAcciones.Controls.Add(this.btnCancelar);
            this.pnlAcciones.Controls.Add(this.btnLimpiar);
            this.pnlAcciones.Controls.Add(this.btnGuardar);
            this.pnlAcciones.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlAcciones.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.pnlAcciones.Location = new System.Drawing.Point(0, 581);
            this.pnlAcciones.Name = "pnlAcciones";
            this.pnlAcciones.Padding = new System.Windows.Forms.Padding(12, 10, 12, 10);
            this.pnlAcciones.Size = new System.Drawing.Size(840, 60);
            this.pnlAcciones.TabIndex = 2;
            this.pnlAcciones.WrapContents = false;
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.Color.Gainsboro;
            this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelar.Location = new System.Drawing.Point(693, 13);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(120, 36);
            this.btnCancelar.TabIndex = 2;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.BackColor = System.Drawing.Color.Aquamarine;
            this.btnLimpiar.Location = new System.Drawing.Point(567, 13);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(120, 36);
            this.btnLimpiar.TabIndex = 1;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.UseVisualStyleBackColor = false;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.btnGuardar.Enabled = false;
            this.btnGuardar.Location = new System.Drawing.Point(441, 13);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(120, 36);
            this.btnGuardar.TabIndex = 0;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // FrmRegistrarPersona
            // 
            this.AcceptButton = this.btnGuardar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.CancelButton = this.btnCancelar;
            this.ClientSize = new System.Drawing.Size(840, 641);
            this.Controls.Add(this.tblContenido);
            this.Controls.Add(this.pnlAcciones);
            this.Controls.Add(this.pnlTipo);
            this.Controls.Add(this.pnlEncabezado);
            this.MinimumSize = new System.Drawing.Size(820, 680);
            this.Name = "FrmRegistrarPersona";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Alta de personal";
            this.Load += new System.EventHandler(this.FrmRegistrarPersona_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmRegistrarPersona_FormClosing);
            this.pnlEncabezado.ResumeLayout(false);
            this.pnlTipo.ResumeLayout(false);
            this.pnlTipo.PerformLayout();
            this.tblContenido.ResumeLayout(false);
            this.grpPersona.ResumeLayout(false);
            this.tblPersona.ResumeLayout(false);
            this.tblPersona.PerformLayout();
            this.grpCuenta.ResumeLayout(false);
            this.tblCuenta.ResumeLayout(false);
            this.tblCuenta.PerformLayout();
            this.pnlAcciones.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Panel pnlEncabezado;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Panel pnlTipo;
        private System.Windows.Forms.Label lblTipo;
        private System.Windows.Forms.ComboBox cmbTipo;
        private System.Windows.Forms.CheckBox chkPersonaExistente;
        private System.Windows.Forms.ComboBox cmbPersona;
        private System.Windows.Forms.Label lblIndicacion;
        private System.Windows.Forms.TableLayoutPanel tblContenido;
        private System.Windows.Forms.GroupBox grpPersona;
        private System.Windows.Forms.TableLayoutPanel tblPersona;
        private System.Windows.Forms.GroupBox grpCuenta;
        private System.Windows.Forms.TableLayoutPanel tblCuenta;
        private System.Windows.Forms.Label lblPrimerNombre;
        private System.Windows.Forms.TextBox txtPrimerNombre;
        private System.Windows.Forms.Label lblSegundoNombre;
        private System.Windows.Forms.TextBox txtSegundoNombre;
        private System.Windows.Forms.Label lblPrimerApellido;
        private System.Windows.Forms.TextBox txtPrimerApellido;
        private System.Windows.Forms.Label lblSegundoApellido;
        private System.Windows.Forms.TextBox txtSegundoApellido;
        private System.Windows.Forms.Label lblGenero;
        private System.Windows.Forms.ComboBox cmbGenero;
        private System.Windows.Forms.Label lblCedula;
        private System.Windows.Forms.TextBox txtCedula;
        private System.Windows.Forms.Label lblTelefono;
        private System.Windows.Forms.TextBox txtTelefono;
        private System.Windows.Forms.Label lblCargo;
        private System.Windows.Forms.ComboBox cmbCargo;
        private System.Windows.Forms.Label lblFormato;
        private System.Windows.Forms.Label lblUsuario;
        private System.Windows.Forms.TextBox txtUsuario;
        private System.Windows.Forms.Label lblContrasena;
        private System.Windows.Forms.TextBox txtContrasena;
        private System.Windows.Forms.Label lblConfirmar;
        private System.Windows.Forms.TextBox txtConfirmar;
        private System.Windows.Forms.CheckBox chkMostrar;
        private System.Windows.Forms.Label lblCuenta;
        private System.Windows.Forms.FlowLayoutPanel pnlAcciones;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Button btnCancelar;
    }
}
