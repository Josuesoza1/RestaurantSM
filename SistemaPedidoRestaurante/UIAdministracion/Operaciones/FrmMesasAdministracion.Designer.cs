namespace SistemaPedidoRestaurante.UIAdministracion
{
    partial class FrmMesasAdministracion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMesasAdministracion));
            this.titulo = new System.Windows.Forms.Label();
            this.buscar = new System.Windows.Forms.Panel();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.btnUltimoComprobante = new System.Windows.Forms.Button();
            this.filtroMes = new System.Windows.Forms.Panel();
            this.mesVenta = new System.Windows.Forms.DateTimePicker();
            this.chkFiltrarMes = new System.Windows.Forms.CheckBox();
            this.btnResumenMensual = new System.Windows.Forms.Button();
            this.etiquetaBuscar = new System.Windows.Forms.Label();
            this.btnNuevo = new System.Windows.Forms.Button();
            this.acciones = new System.Windows.Forms.FlowLayoutPanel();
            this.btnSeleccionar = new System.Windows.Forms.Button();
            this.btnEditarVenta = new System.Windows.Forms.Button();
            this.btnMesa = new System.Windows.Forms.Button();
            this.btnAvanzar = new System.Windows.Forms.Button();
            this.btnCobrar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnPedidoPdf = new System.Windows.Forms.Button();
            this.btnFacturaPdf = new System.Windows.Forms.Button();
            this.panelResumenDiario = new System.Windows.Forms.Panel();
            this.diaCaja = new System.Windows.Forms.DateTimePicker();
            this.btnResumenDiario = new System.Windows.Forms.Button();
            this.btnRecargar = new System.Windows.Forms.Button();
            this.btnVolver = new System.Windows.Forms.Button();
            this.lblGuia = new System.Windows.Forms.Label();
            this.paneles = new System.Windows.Forms.SplitContainer();
            this.listado = new System.Windows.Forms.DataGridView();
            this.detalle = new System.Windows.Forms.DataGridView();
            this.buscar.SuspendLayout();
            this.filtroMes.SuspendLayout();
            this.acciones.SuspendLayout();
            this.panelResumenDiario.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.paneles)).BeginInit();
            this.paneles.Panel1.SuspendLayout();
            this.paneles.Panel2.SuspendLayout();
            this.paneles.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listado)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.detalle)).BeginInit();
            this.SuspendLayout();
            // 
            // titulo
            // 
            this.titulo.BackColor = System.Drawing.Color.Red;
            this.titulo.Dock = System.Windows.Forms.DockStyle.Top;
            this.titulo.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.titulo.ForeColor = System.Drawing.Color.White;
            this.titulo.Location = new System.Drawing.Point(0, 0);
            this.titulo.Name = "titulo";
            this.titulo.Size = new System.Drawing.Size(1371, 65);
            this.titulo.TabIndex = 3;
            this.titulo.Text = "MESAS — ADMINISTRACIÓN";
            this.titulo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // buscar
            // 
            this.buscar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.buscar.Controls.Add(this.txtBuscar);
            this.buscar.Controls.Add(this.btnUltimoComprobante);
            this.buscar.Controls.Add(this.filtroMes);
            this.buscar.Controls.Add(this.btnResumenMensual);
            this.buscar.Controls.Add(this.etiquetaBuscar);
            this.buscar.Controls.Add(this.btnNuevo);
            this.buscar.Dock = System.Windows.Forms.DockStyle.Top;
            this.buscar.Location = new System.Drawing.Point(0, 65);
            this.buscar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buscar.Name = "buscar";
            this.buscar.Padding = new System.Windows.Forms.Padding(16, 10, 16, 10);
            this.buscar.Size = new System.Drawing.Size(1371, 55);
            this.buscar.TabIndex = 0;
            // 
            // txtBuscar
            // 
            this.txtBuscar.AccessibleName = "Buscar en el listado";
            this.txtBuscar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtBuscar.Location = new System.Drawing.Point(254, 10);
            this.txtBuscar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(296, 22);
            this.txtBuscar.TabIndex = 1;
            this.txtBuscar.TextChanged += new System.EventHandler(this.filtro_Changed);
            // 
            // btnUltimoComprobante
            // 
            this.btnUltimoComprobante.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnUltimoComprobante.Location = new System.Drawing.Point(550, 10);
            this.btnUltimoComprobante.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnUltimoComprobante.Name = "btnUltimoComprobante";
            this.btnUltimoComprobante.Size = new System.Drawing.Size(245, 35);
            this.btnUltimoComprobante.TabIndex = 3;
            this.btnUltimoComprobante.Text = "Último comprobante vigente";
            this.btnUltimoComprobante.Visible = false;
            this.btnUltimoComprobante.Click += new System.EventHandler(this.btnUltimoComprobante_Click);
            // 
            // filtroMes
            // 
            this.filtroMes.Controls.Add(this.mesVenta);
            this.filtroMes.Controls.Add(this.chkFiltrarMes);
            this.filtroMes.Dock = System.Windows.Forms.DockStyle.Right;
            this.filtroMes.Location = new System.Drawing.Point(795, 10);
            this.filtroMes.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.filtroMes.Name = "filtroMes";
            this.filtroMes.Padding = new System.Windows.Forms.Padding(16, 0, 0, 0);
            this.filtroMes.Size = new System.Drawing.Size(328, 35);
            this.filtroMes.TabIndex = 2;
            this.filtroMes.Visible = false;
            // 
            // mesVenta
            // 
            this.mesVenta.AccessibleName = "Mes y año de registro de la venta";
            this.mesVenta.CustomFormat = "MMMM yyyy";
            this.mesVenta.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mesVenta.Enabled = false;
            this.mesVenta.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.mesVenta.Location = new System.Drawing.Point(149, 0);
            this.mesVenta.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.mesVenta.Name = "mesVenta";
            this.mesVenta.ShowUpDown = true;
            this.mesVenta.Size = new System.Drawing.Size(179, 22);
            this.mesVenta.TabIndex = 1;
            this.mesVenta.ValueChanged += new System.EventHandler(this.filtro_Changed);
            // 
            // chkFiltrarMes
            // 
            this.chkFiltrarMes.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.chkFiltrarMes.Dock = System.Windows.Forms.DockStyle.Left;
            this.chkFiltrarMes.Location = new System.Drawing.Point(16, 0);
            this.chkFiltrarMes.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chkFiltrarMes.Name = "chkFiltrarMes";
            this.chkFiltrarMes.Size = new System.Drawing.Size(133, 35);
            this.chkFiltrarMes.TabIndex = 0;
            this.chkFiltrarMes.Text = "Mes de registro";
            this.chkFiltrarMes.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.chkFiltrarMes.CheckedChanged += new System.EventHandler(this.chkFiltrarMes_CheckedChanged);
            // 
            // btnResumenMensual
            // 
            this.btnResumenMensual.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnResumenMensual.Location = new System.Drawing.Point(1123, 10);
            this.btnResumenMensual.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnResumenMensual.Name = "btnResumenMensual";
            this.btnResumenMensual.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.btnResumenMensual.Size = new System.Drawing.Size(232, 35);
            this.btnResumenMensual.TabIndex = 12;
            this.btnResumenMensual.Text = "Resumen de caja del mes";
            this.btnResumenMensual.Visible = false;
            this.btnResumenMensual.Click += new System.EventHandler(this.btnResumenMensual_Click);
            // 
            // etiquetaBuscar
            // 
            this.etiquetaBuscar.Dock = System.Windows.Forms.DockStyle.Left;
            this.etiquetaBuscar.Location = new System.Drawing.Point(167, 10);
            this.etiquetaBuscar.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.etiquetaBuscar.Name = "etiquetaBuscar";
            this.etiquetaBuscar.Size = new System.Drawing.Size(87, 35);
            this.etiquetaBuscar.TabIndex = 1;
            this.etiquetaBuscar.Text = "Buscar:";
            this.etiquetaBuscar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnNuevo
            // 
            this.btnNuevo.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnNuevo.Location = new System.Drawing.Point(16, 10);
            this.btnNuevo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnNuevo.Size = new System.Drawing.Size(151, 35);
            this.btnNuevo.TabIndex = 0;
            this.btnNuevo.Text = "Nueva venta";
            this.btnNuevo.Visible = false;
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // acciones
            // 
            this.acciones.AutoSize = true;
            this.acciones.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.acciones.Controls.Add(this.btnSeleccionar);
            this.acciones.Controls.Add(this.btnEditarVenta);
            this.acciones.Controls.Add(this.btnMesa);
            this.acciones.Controls.Add(this.btnAvanzar);
            this.acciones.Controls.Add(this.btnCobrar);
            this.acciones.Controls.Add(this.btnCancelar);
            this.acciones.Controls.Add(this.btnPedidoPdf);
            this.acciones.Controls.Add(this.btnFacturaPdf);
            this.acciones.Controls.Add(this.panelResumenDiario);
            this.acciones.Controls.Add(this.btnRecargar);
            this.acciones.Controls.Add(this.btnVolver);
            this.acciones.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.acciones.Location = new System.Drawing.Point(0, 626);
            this.acciones.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.acciones.Name = "acciones";
            this.acciones.Padding = new System.Windows.Forms.Padding(11, 10, 11, 10);
            this.acciones.Size = new System.Drawing.Size(1371, 124);
            this.acciones.TabIndex = 2;
            // 
            // btnSeleccionar
            // 
            this.btnSeleccionar.AutoSize = true;
            this.btnSeleccionar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnSeleccionar.Location = new System.Drawing.Point(15, 14);
            this.btnSeleccionar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSeleccionar.Name = "btnSeleccionar";
            this.btnSeleccionar.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.btnSeleccionar.Size = new System.Drawing.Size(132, 44);
            this.btnSeleccionar.TabIndex = 0;
            this.btnSeleccionar.Text = "Seleccionar";
            this.btnSeleccionar.UseVisualStyleBackColor = false;
            this.btnSeleccionar.Visible = false;
            this.btnSeleccionar.Click += new System.EventHandler(this.btnSeleccionar_Click);
            // 
            // btnEditarVenta
            // 
            this.btnEditarVenta.AutoSize = true;
            this.btnEditarVenta.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnEditarVenta.Enabled = false;
            this.btnEditarVenta.Location = new System.Drawing.Point(155, 14);
            this.btnEditarVenta.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnEditarVenta.Name = "btnEditarVenta";
            this.btnEditarVenta.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.btnEditarVenta.Size = new System.Drawing.Size(131, 44);
            this.btnEditarVenta.TabIndex = 1;
            this.btnEditarVenta.Text = "Editar venta";
            this.btnEditarVenta.UseVisualStyleBackColor = false;
            this.btnEditarVenta.Visible = false;
            this.btnEditarVenta.Click += new System.EventHandler(this.btnEditarVenta_Click);
            // 
            // btnMesa
            // 
            this.btnMesa.AutoSize = true;
            this.btnMesa.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnMesa.Location = new System.Drawing.Point(294, 14);
            this.btnMesa.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnMesa.Name = "btnMesa";
            this.btnMesa.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.btnMesa.Size = new System.Drawing.Size(147, 44);
            this.btnMesa.TabIndex = 2;
            this.btnMesa.Text = "Asignar mesa";
            this.btnMesa.UseVisualStyleBackColor = false;
            this.btnMesa.Visible = false;
            this.btnMesa.Click += new System.EventHandler(this.btnMesa_Click);
            // 
            // btnAvanzar
            // 
            this.btnAvanzar.AutoSize = true;
            this.btnAvanzar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnAvanzar.Location = new System.Drawing.Point(449, 14);
            this.btnAvanzar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnAvanzar.MinimumSize = new System.Drawing.Size(200, 0);
            this.btnAvanzar.Name = "btnAvanzar";
            this.btnAvanzar.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.btnAvanzar.Size = new System.Drawing.Size(200, 44);
            this.btnAvanzar.TabIndex = 3;
            this.btnAvanzar.Text = "Avanzar estado";
            this.btnAvanzar.UseVisualStyleBackColor = false;
            this.btnAvanzar.Visible = false;
            this.btnAvanzar.Click += new System.EventHandler(this.btnAvanzar_Click);
            // 
            // btnCobrar
            // 
            this.btnCobrar.AutoSize = true;
            this.btnCobrar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnCobrar.Location = new System.Drawing.Point(657, 14);
            this.btnCobrar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCobrar.MinimumSize = new System.Drawing.Size(133, 0);
            this.btnCobrar.Name = "btnCobrar";
            this.btnCobrar.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.btnCobrar.Size = new System.Drawing.Size(133, 44);
            this.btnCobrar.TabIndex = 4;
            this.btnCobrar.Text = "Cobrar";
            this.btnCobrar.UseVisualStyleBackColor = false;
            this.btnCobrar.Visible = false;
            this.btnCobrar.Click += new System.EventHandler(this.btnCobrar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.AutoSize = true;
            this.btnCancelar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnCancelar.Location = new System.Drawing.Point(798, 14);
            this.btnCancelar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.btnCancelar.Size = new System.Drawing.Size(192, 44);
            this.btnCancelar.TabIndex = 8;
            this.btnCancelar.Text = "Cancelar venta";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Visible = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnPedidoPdf
            // 
            this.btnPedidoPdf.AutoSize = true;
            this.btnPedidoPdf.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnPedidoPdf.Location = new System.Drawing.Point(998, 14);
            this.btnPedidoPdf.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnPedidoPdf.Name = "btnPedidoPdf";
            this.btnPedidoPdf.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.btnPedidoPdf.Size = new System.Drawing.Size(175, 44);
            this.btnPedidoPdf.TabIndex = 9;
            this.btnPedidoPdf.Text = "Ver comprobante";
            this.btnPedidoPdf.UseVisualStyleBackColor = false;
            this.btnPedidoPdf.Visible = false;
            this.btnPedidoPdf.Click += new System.EventHandler(this.btnPedidoPdf_Click);
            // 
            // btnFacturaPdf
            // 
            this.btnFacturaPdf.AutoSize = true;
            this.btnFacturaPdf.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnFacturaPdf.Location = new System.Drawing.Point(1181, 14);
            this.btnFacturaPdf.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnFacturaPdf.Name = "btnFacturaPdf";
            this.btnFacturaPdf.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.btnFacturaPdf.Size = new System.Drawing.Size(155, 44);
            this.btnFacturaPdf.TabIndex = 10;
            this.btnFacturaPdf.Text = "Ver factura";
            this.btnFacturaPdf.UseVisualStyleBackColor = false;
            this.btnFacturaPdf.Visible = false;
            this.btnFacturaPdf.Click += new System.EventHandler(this.btnFacturaPdf_Click);
            // 
            // panelResumenDiario
            // 
            this.panelResumenDiario.Controls.Add(this.diaCaja);
            this.panelResumenDiario.Controls.Add(this.btnResumenDiario);
            this.panelResumenDiario.Location = new System.Drawing.Point(15, 66);
            this.panelResumenDiario.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelResumenDiario.Name = "panelResumenDiario";
            this.panelResumenDiario.Size = new System.Drawing.Size(289, 44);
            this.panelResumenDiario.TabIndex = 12;
            this.panelResumenDiario.Visible = false;
            // 
            // diaCaja
            // 
            this.diaCaja.AccessibleName = "Fecha del resumen diario";
            this.diaCaja.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.diaCaja.Location = new System.Drawing.Point(0, 10);
            this.diaCaja.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.diaCaja.Name = "diaCaja";
            this.diaCaja.Size = new System.Drawing.Size(117, 22);
            this.diaCaja.TabIndex = 0;
            // 
            // btnResumenDiario
            // 
            this.btnResumenDiario.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnResumenDiario.Location = new System.Drawing.Point(125, 0);
            this.btnResumenDiario.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnResumenDiario.Name = "btnResumenDiario";
            this.btnResumenDiario.Size = new System.Drawing.Size(164, 44);
            this.btnResumenDiario.TabIndex = 1;
            this.btnResumenDiario.Text = "Resumen diario";
            this.btnResumenDiario.UseVisualStyleBackColor = false;
            this.btnResumenDiario.Click += new System.EventHandler(this.btnResumenDiario_Click);
            // 
            // btnRecargar
            // 
            this.btnRecargar.AutoSize = true;
            this.btnRecargar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnRecargar.Location = new System.Drawing.Point(312, 66);
            this.btnRecargar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnRecargar.Name = "btnRecargar";
            this.btnRecargar.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.btnRecargar.Size = new System.Drawing.Size(112, 44);
            this.btnRecargar.TabIndex = 13;
            this.btnRecargar.Text = "Recargar";
            this.btnRecargar.UseVisualStyleBackColor = false;
            this.btnRecargar.Click += new System.EventHandler(this.btnRecargar_Click);
            // 
            // btnVolver
            // 
            this.btnVolver.AutoSize = true;
            this.btnVolver.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnVolver.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnVolver.Location = new System.Drawing.Point(432, 66);
            this.btnVolver.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnVolver.Name = "btnVolver";
            this.btnVolver.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.btnVolver.Size = new System.Drawing.Size(100, 44);
            this.btnVolver.TabIndex = 14;
            this.btnVolver.Text = "Volver";
            this.btnVolver.UseVisualStyleBackColor = false;
            this.btnVolver.Click += new System.EventHandler(this.btnVolver_Click);
            // 
            // lblGuia
            // 
            this.lblGuia.BackColor = System.Drawing.SystemColors.Control;
            this.lblGuia.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblGuia.Location = new System.Drawing.Point(0, 120);
            this.lblGuia.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblGuia.Name = "lblGuia";
            this.lblGuia.Padding = new System.Windows.Forms.Padding(13, 0, 0, 0);
            this.lblGuia.Size = new System.Drawing.Size(1371, 34);
            this.lblGuia.TabIndex = 2;
            this.lblGuia.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblGuia.Visible = false;
            // 
            // paneles
            // 
            this.paneles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.paneles.Location = new System.Drawing.Point(0, 154);
            this.paneles.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.paneles.Name = "paneles";
            this.paneles.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // paneles.Panel1
            // 
            this.paneles.Panel1.Controls.Add(this.listado);
            // 
            // paneles.Panel2
            // 
            this.paneles.Panel2.Controls.Add(this.detalle);
            this.paneles.Panel2Collapsed = true;
            this.paneles.Size = new System.Drawing.Size(1371, 472);
            this.paneles.SplitterDistance = 239;
            this.paneles.SplitterWidth = 5;
            this.paneles.TabIndex = 1;
            this.paneles.TabStop = false;
            // 
            // listado
            // 
            this.listado.AccessibleName = "Listado de registros";
            this.listado.AllowUserToAddRows = false;
            this.listado.AllowUserToDeleteRows = false;
            this.listado.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.listado.BackgroundColor = System.Drawing.Color.White;
            this.listado.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.listado.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listado.Location = new System.Drawing.Point(0, 0);
            this.listado.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.listado.MultiSelect = false;
            this.listado.Name = "listado";
            this.listado.ReadOnly = true;
            this.listado.RowHeadersWidth = 51;
            this.listado.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.listado.Size = new System.Drawing.Size(1371, 472);
            this.listado.StandardTab = true;
            this.listado.TabIndex = 0;
            this.listado.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.listado_CellClick);
            this.listado.SelectionChanged += new System.EventHandler(this.listado_SelectionChanged);
            this.listado.Enter += new System.EventHandler(this.listado_Enter);
            // 
            // detalle
            // 
            this.detalle.AccessibleName = "Productos de la venta seleccionada";
            this.detalle.AllowUserToAddRows = false;
            this.detalle.AllowUserToDeleteRows = false;
            this.detalle.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.detalle.BackgroundColor = System.Drawing.Color.White;
            this.detalle.ColumnHeadersHeight = 29;
            this.detalle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.detalle.Location = new System.Drawing.Point(0, 0);
            this.detalle.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.detalle.MultiSelect = false;
            this.detalle.Name = "detalle";
            this.detalle.ReadOnly = true;
            this.detalle.RowHeadersWidth = 51;
            this.detalle.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.detalle.Size = new System.Drawing.Size(1371, 207);
            this.detalle.StandardTab = true;
            this.detalle.TabIndex = 0;
            // 
            // FrmMesasAdministracion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.IndianRed;
            this.CancelButton = this.btnVolver;
            this.ClientSize = new System.Drawing.Size(1371, 750);
            this.Controls.Add(this.paneles);
            this.Controls.Add(this.lblGuia);
            this.Controls.Add(this.acciones);
            this.Controls.Add(this.buscar);
            this.Controls.Add(this.titulo);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MinimumSize = new System.Drawing.Size(698, 447);
            this.Name = "FrmMesasAdministracion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Mesas — Administración";
            this.Load += new System.EventHandler(this.FrmBandeja_Load);
            this.buscar.ResumeLayout(false);
            this.buscar.PerformLayout();
            this.filtroMes.ResumeLayout(false);
            this.acciones.ResumeLayout(false);
            this.acciones.PerformLayout();
            this.panelResumenDiario.ResumeLayout(false);
            this.paneles.Panel1.ResumeLayout(false);
            this.paneles.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.paneles)).EndInit();
            this.paneles.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.listado)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.detalle)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private System.Windows.Forms.Label titulo;
        private System.Windows.Forms.Panel buscar;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.Label etiquetaBuscar;
        private System.Windows.Forms.Panel filtroMes;
        private System.Windows.Forms.CheckBox chkFiltrarMes;
        private System.Windows.Forms.DateTimePicker mesVenta;
        private System.Windows.Forms.FlowLayoutPanel acciones;
        private System.Windows.Forms.SplitContainer paneles;
        private System.Windows.Forms.DataGridView listado;
        private System.Windows.Forms.DataGridView detalle;
        private System.Windows.Forms.Button btnSeleccionar;
        private System.Windows.Forms.Button btnNuevo;
        private System.Windows.Forms.Button btnUltimoComprobante;
        private System.Windows.Forms.Button btnMesa;
        private System.Windows.Forms.Button btnAvanzar;
        private System.Windows.Forms.Button btnCobrar;
        private System.Windows.Forms.Button btnEditarVenta;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnPedidoPdf;
        private System.Windows.Forms.Button btnFacturaPdf;
        private System.Windows.Forms.Button btnResumenMensual;
        private System.Windows.Forms.Panel panelResumenDiario;
        private System.Windows.Forms.DateTimePicker diaCaja;
        private System.Windows.Forms.Button btnResumenDiario;
        private System.Windows.Forms.Button btnRecargar;
        private System.Windows.Forms.Button btnVolver;
        private System.Windows.Forms.Label lblGuia;
    }
}
