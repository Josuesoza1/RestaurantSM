namespace SistemaPedidoRestaurante.UICompartida
{
    partial class FrmBandejaOperaciones
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
            this.btnCobrar = new System.Windows.Forms.Button();
            this.btnEditarVenta = new System.Windows.Forms.Button();
            this.btnMesa = new System.Windows.Forms.Button();
            this.btnAvanzar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnPedidoPdf = new System.Windows.Forms.Button();
            this.btnFacturaPdf = new System.Windows.Forms.Button();
            this.btnLimpiarMesa = new System.Windows.Forms.Button();
            this.panelResumenDiario = new System.Windows.Forms.Panel();
            this.diaCaja = new System.Windows.Forms.DateTimePicker();
            this.btnResumenDiario = new System.Windows.Forms.Button();
            this.btnRecargar = new System.Windows.Forms.Button();
            this.btnVolver = new System.Windows.Forms.Button();
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
            this.titulo.BackColor = System.Drawing.Color.DarkSlateBlue;
            this.titulo.Dock = System.Windows.Forms.DockStyle.Top;
            this.titulo.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.titulo.ForeColor = System.Drawing.Color.White;
            this.titulo.Location = new System.Drawing.Point(0, 0);
            this.titulo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.titulo.Name = "titulo";
            this.titulo.Size = new System.Drawing.Size(1445, 80);
            this.titulo.TabIndex = 3;
            this.titulo.Text = "BANDEJA DE PEDIDOS";
            this.titulo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // buscar
            // 
            this.buscar.Controls.Add(this.txtBuscar);
            this.buscar.Controls.Add(this.btnUltimoComprobante);
            this.buscar.Controls.Add(this.filtroMes);
            this.buscar.Controls.Add(this.btnResumenMensual);
            this.buscar.Controls.Add(this.etiquetaBuscar);
            this.buscar.Controls.Add(this.btnNuevo);
            this.buscar.Dock = System.Windows.Forms.DockStyle.Top;
            this.buscar.Location = new System.Drawing.Point(0, 80);
            this.buscar.Margin = new System.Windows.Forms.Padding(4);
            this.buscar.Name = "buscar";
            this.buscar.Padding = new System.Windows.Forms.Padding(16, 10, 16, 10);
            this.buscar.Size = new System.Drawing.Size(1445, 55);
            this.buscar.TabIndex = 2;
            // 
            // txtBuscar
            // 
            this.txtBuscar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtBuscar.Location = new System.Drawing.Point(254, 10);
            this.txtBuscar.Margin = new System.Windows.Forms.Padding(4);
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(498, 22);
            this.txtBuscar.TabIndex = 0;
            this.txtBuscar.TextChanged += new System.EventHandler(this.filtro_Changed);
            // 
            // btnUltimoComprobante
            // 
            this.btnUltimoComprobante.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnUltimoComprobante.Location = new System.Drawing.Point(752, 10);
            this.btnUltimoComprobante.Name = "btnUltimoComprobante";
            this.btnUltimoComprobante.Size = new System.Drawing.Size(183, 35);
            this.btnUltimoComprobante.TabIndex = 3;
            this.btnUltimoComprobante.Text = "Último comprobante";
            this.btnUltimoComprobante.Visible = false;
            this.btnUltimoComprobante.Click += new System.EventHandler(this.btnUltimoComprobante_Click);
            // 
            // filtroMes
            // 
            this.filtroMes.Controls.Add(this.mesVenta);
            this.filtroMes.Controls.Add(this.chkFiltrarMes);
            this.filtroMes.Dock = System.Windows.Forms.DockStyle.Right;
            this.filtroMes.Location = new System.Drawing.Point(935, 10);
            this.filtroMes.Name = "filtroMes";
            this.filtroMes.Padding = new System.Windows.Forms.Padding(16, 0, 0, 0);
            this.filtroMes.Size = new System.Drawing.Size(328, 35);
            this.filtroMes.TabIndex = 2;
            // 
            // mesVenta
            // 
            this.mesVenta.AccessibleName = "Mes y año de registro de la venta";
            this.mesVenta.CustomFormat = "MMMM yyyy";
            this.mesVenta.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mesVenta.Enabled = false;
            this.mesVenta.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.mesVenta.Location = new System.Drawing.Point(149, 0);
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
            this.btnResumenMensual.Location = new System.Drawing.Point(1263, 10);
            this.btnResumenMensual.Margin = new System.Windows.Forms.Padding(4);
            this.btnResumenMensual.Name = "btnResumenMensual";
            this.btnResumenMensual.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.btnResumenMensual.Size = new System.Drawing.Size(166, 35);
            this.btnResumenMensual.TabIndex = 12;
            this.btnResumenMensual.Text = "Resumen mensual";
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
            this.btnNuevo.Margin = new System.Windows.Forms.Padding(4);
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.btnNuevo.Size = new System.Drawing.Size(151, 35);
            this.btnNuevo.TabIndex = 1;
            this.btnNuevo.Text = "Nueva venta";
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // acciones
            // 
            this.acciones.AutoSize = true;
            this.acciones.Controls.Add(this.btnSeleccionar);
            this.acciones.Controls.Add(this.btnCobrar);
            this.acciones.Controls.Add(this.btnEditarVenta);
            this.acciones.Controls.Add(this.btnMesa);
            this.acciones.Controls.Add(this.btnAvanzar);
            this.acciones.Controls.Add(this.btnCancelar);
            this.acciones.Controls.Add(this.btnPedidoPdf);
            this.acciones.Controls.Add(this.btnFacturaPdf);
            this.acciones.Controls.Add(this.btnLimpiarMesa);
            this.acciones.Controls.Add(this.panelResumenDiario);
            this.acciones.Controls.Add(this.btnRecargar);
            this.acciones.Controls.Add(this.btnVolver);
            this.acciones.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.acciones.Location = new System.Drawing.Point(0, 690);
            this.acciones.Margin = new System.Windows.Forms.Padding(4);
            this.acciones.Name = "acciones";
            this.acciones.Padding = new System.Windows.Forms.Padding(11, 10, 11, 10);
            this.acciones.Size = new System.Drawing.Size(1445, 124);
            this.acciones.TabIndex = 1;
            // 
            // btnSeleccionar
            // 
            this.btnSeleccionar.AutoSize = true;
            this.btnSeleccionar.Location = new System.Drawing.Point(15, 14);
            this.btnSeleccionar.Margin = new System.Windows.Forms.Padding(4);
            this.btnSeleccionar.Name = "btnSeleccionar";
            this.btnSeleccionar.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.btnSeleccionar.Size = new System.Drawing.Size(132, 44);
            this.btnSeleccionar.TabIndex = 0;
            this.btnSeleccionar.Text = "Seleccionar";
            this.btnSeleccionar.Visible = false;
            this.btnSeleccionar.Click += new System.EventHandler(this.btnSeleccionar_Click);
            // 
            // btnCobrar
            // 
            this.btnCobrar.AutoSize = true;
            this.btnCobrar.Location = new System.Drawing.Point(155, 14);
            this.btnCobrar.Margin = new System.Windows.Forms.Padding(4);
            this.btnCobrar.Name = "btnCobrar";
            this.btnCobrar.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.btnCobrar.Size = new System.Drawing.Size(100, 44);
            this.btnCobrar.TabIndex = 1;
            this.btnCobrar.Text = "Cobrar";
            this.btnCobrar.Click += new System.EventHandler(this.btnCobrar_Click);
            // 
            // btnEditarVenta
            // 
            this.btnEditarVenta.AutoSize = true;
            this.btnEditarVenta.Enabled = false;
            this.btnEditarVenta.Location = new System.Drawing.Point(263, 14);
            this.btnEditarVenta.Margin = new System.Windows.Forms.Padding(4);
            this.btnEditarVenta.Name = "btnEditarVenta";
            this.btnEditarVenta.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.btnEditarVenta.Size = new System.Drawing.Size(118, 44);
            this.btnEditarVenta.TabIndex = 2;
            this.btnEditarVenta.Text = "Editar venta";
            this.btnEditarVenta.Visible = false;
            this.btnEditarVenta.Click += new System.EventHandler(this.btnEditarVenta_Click);
            // 
            // btnMesa
            // 
            this.btnMesa.AutoSize = true;
            this.btnMesa.Location = new System.Drawing.Point(389, 14);
            this.btnMesa.Margin = new System.Windows.Forms.Padding(4);
            this.btnMesa.Name = "btnMesa";
            this.btnMesa.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.btnMesa.Size = new System.Drawing.Size(147, 44);
            this.btnMesa.TabIndex = 3;
            this.btnMesa.Text = "Asignar mesa";
            this.btnMesa.Click += new System.EventHandler(this.btnMesa_Click);
            // 
            // btnAvanzar
            // 
            this.btnAvanzar.AutoSize = true;
            this.btnAvanzar.Location = new System.Drawing.Point(544, 14);
            this.btnAvanzar.Margin = new System.Windows.Forms.Padding(4);
            this.btnAvanzar.Name = "btnAvanzar";
            this.btnAvanzar.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.btnAvanzar.Size = new System.Drawing.Size(161, 44);
            this.btnAvanzar.TabIndex = 4;
            this.btnAvanzar.Text = "Avanzar estado";
            this.btnAvanzar.Click += new System.EventHandler(this.btnAvanzar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.AutoSize = true;
            this.btnCancelar.Location = new System.Drawing.Point(713, 14);
            this.btnCancelar.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.btnCancelar.Size = new System.Drawing.Size(192, 44);
            this.btnCancelar.TabIndex = 8;
            this.btnCancelar.Text = "Cancelar pendiente";
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnPedidoPdf
            // 
            this.btnPedidoPdf.AutoSize = true;
            this.btnPedidoPdf.Location = new System.Drawing.Point(913, 14);
            this.btnPedidoPdf.Margin = new System.Windows.Forms.Padding(4);
            this.btnPedidoPdf.Name = "btnPedidoPdf";
            this.btnPedidoPdf.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.btnPedidoPdf.Size = new System.Drawing.Size(163, 44);
            this.btnPedidoPdf.TabIndex = 9;
            this.btnPedidoPdf.Text = "Ver comprobante";
            this.btnPedidoPdf.Click += new System.EventHandler(this.btnPedidoPdf_Click);
            // 
            // btnFacturaPdf
            // 
            this.btnFacturaPdf.AutoSize = true;
            this.btnFacturaPdf.Location = new System.Drawing.Point(1084, 14);
            this.btnFacturaPdf.Margin = new System.Windows.Forms.Padding(4);
            this.btnFacturaPdf.Name = "btnFacturaPdf";
            this.btnFacturaPdf.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.btnFacturaPdf.Size = new System.Drawing.Size(155, 44);
            this.btnFacturaPdf.TabIndex = 10;
            this.btnFacturaPdf.Text = "Ver factura";
            this.btnFacturaPdf.Click += new System.EventHandler(this.btnFacturaPdf_Click);
            // 
            // btnLimpiarMesa
            // 
            this.btnLimpiarMesa.AutoSize = true;
            this.btnLimpiarMesa.Location = new System.Drawing.Point(1247, 14);
            this.btnLimpiarMesa.Margin = new System.Windows.Forms.Padding(4);
            this.btnLimpiarMesa.Name = "btnLimpiarMesa";
            this.btnLimpiarMesa.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.btnLimpiarMesa.Size = new System.Drawing.Size(172, 44);
            this.btnLimpiarMesa.TabIndex = 11;
            this.btnLimpiarMesa.Text = "Avanzar limpieza";
            this.btnLimpiarMesa.Visible = false;
            this.btnLimpiarMesa.Click += new System.EventHandler(this.btnLimpiarMesa_Click);
            // 
            // panelResumenDiario
            // 
            this.panelResumenDiario.Controls.Add(this.diaCaja);
            this.panelResumenDiario.Controls.Add(this.btnResumenDiario);
            this.panelResumenDiario.Location = new System.Drawing.Point(15, 66);
            this.panelResumenDiario.Margin = new System.Windows.Forms.Padding(4);
            this.panelResumenDiario.Name = "panelResumenDiario";
            this.panelResumenDiario.Size = new System.Drawing.Size(289, 44);
            this.panelResumenDiario.TabIndex = 12;
            // 
            // diaCaja
            // 
            this.diaCaja.AccessibleName = "Fecha del resumen diario";
            this.diaCaja.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.diaCaja.Location = new System.Drawing.Point(0, 10);
            this.diaCaja.Name = "diaCaja";
            this.diaCaja.Size = new System.Drawing.Size(117, 22);
            this.diaCaja.TabIndex = 0;
            // 
            // btnResumenDiario
            // 
            this.btnResumenDiario.Location = new System.Drawing.Point(125, 0);
            this.btnResumenDiario.Name = "btnResumenDiario";
            this.btnResumenDiario.Size = new System.Drawing.Size(164, 44);
            this.btnResumenDiario.TabIndex = 1;
            this.btnResumenDiario.Text = "Resumen diario";
            this.btnResumenDiario.Click += new System.EventHandler(this.btnResumenDiario_Click);
            // 
            // btnRecargar
            // 
            this.btnRecargar.AutoSize = true;
            this.btnRecargar.Location = new System.Drawing.Point(312, 66);
            this.btnRecargar.Margin = new System.Windows.Forms.Padding(4);
            this.btnRecargar.Name = "btnRecargar";
            this.btnRecargar.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.btnRecargar.Size = new System.Drawing.Size(112, 44);
            this.btnRecargar.TabIndex = 13;
            this.btnRecargar.Text = "Recargar";
            this.btnRecargar.Click += new System.EventHandler(this.btnRecargar_Click);
            // 
            // btnVolver
            // 
            this.btnVolver.AutoSize = true;
            this.btnVolver.Location = new System.Drawing.Point(432, 66);
            this.btnVolver.Margin = new System.Windows.Forms.Padding(4);
            this.btnVolver.Name = "btnVolver";
            this.btnVolver.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.btnVolver.Size = new System.Drawing.Size(100, 44);
            this.btnVolver.TabIndex = 14;
            this.btnVolver.Text = "Volver";
            this.btnVolver.Click += new System.EventHandler(this.btnVolver_Click);
            // 
            // paneles
            // 
            this.paneles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.paneles.Location = new System.Drawing.Point(0, 135);
            this.paneles.Margin = new System.Windows.Forms.Padding(4);
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
            this.paneles.Size = new System.Drawing.Size(1445, 555);
            this.paneles.SplitterDistance = 333;
            this.paneles.SplitterWidth = 5;
            this.paneles.TabIndex = 0;
            // 
            // listado
            // 
            this.listado.AllowUserToAddRows = false;
            this.listado.AllowUserToDeleteRows = false;
            this.listado.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.listado.BackgroundColor = System.Drawing.Color.White;
            this.listado.ColumnHeadersHeight = 29;
            this.listado.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listado.Location = new System.Drawing.Point(0, 0);
            this.listado.Margin = new System.Windows.Forms.Padding(4);
            this.listado.MultiSelect = false;
            this.listado.Name = "listado";
            this.listado.ReadOnly = true;
            this.listado.RowHeadersWidth = 51;
            this.listado.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.listado.Size = new System.Drawing.Size(1445, 333);
            this.listado.TabIndex = 0;
            this.listado.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.listado_CellClick);
            // 
            // detalle
            // 
            this.detalle.AllowUserToAddRows = false;
            this.detalle.AllowUserToDeleteRows = false;
            this.detalle.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.detalle.BackgroundColor = System.Drawing.Color.White;
            this.detalle.ColumnHeadersHeight = 29;
            this.detalle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.detalle.Location = new System.Drawing.Point(0, 0);
            this.detalle.Margin = new System.Windows.Forms.Padding(4);
            this.detalle.MultiSelect = false;
            this.detalle.Name = "detalle";
            this.detalle.ReadOnly = true;
            this.detalle.RowHeadersWidth = 51;
            this.detalle.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.detalle.Size = new System.Drawing.Size(1445, 217);
            this.detalle.TabIndex = 0;
            // 
            // FrmBandejaOperaciones
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(1445, 814);
            this.Controls.Add(this.paneles);
            this.Controls.Add(this.acciones);
            this.Controls.Add(this.buscar);
            this.Controls.Add(this.titulo);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(927, 543);
            this.Name = "FrmBandejaOperaciones";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Bandeja de Pedidos";
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

        protected System.Windows.Forms.Label titulo;
        protected System.Windows.Forms.Panel buscar;
        protected System.Windows.Forms.TextBox txtBuscar;
        protected System.Windows.Forms.Label etiquetaBuscar;
        private System.Windows.Forms.Panel filtroMes;
        private System.Windows.Forms.CheckBox chkFiltrarMes;
        private System.Windows.Forms.DateTimePicker mesVenta;
        protected System.Windows.Forms.FlowLayoutPanel acciones;
        protected System.Windows.Forms.SplitContainer paneles;
        protected System.Windows.Forms.DataGridView listado;
        protected System.Windows.Forms.DataGridView detalle;
        protected System.Windows.Forms.Button btnSeleccionar;
        protected System.Windows.Forms.Button btnNuevo;
        private System.Windows.Forms.Button btnUltimoComprobante;
        protected System.Windows.Forms.Button btnMesa;
        protected System.Windows.Forms.Button btnAvanzar;
        protected System.Windows.Forms.Button btnCobrar;
        private System.Windows.Forms.Button btnEditarVenta;
        protected System.Windows.Forms.Button btnCancelar;
        protected System.Windows.Forms.Button btnPedidoPdf;
        protected System.Windows.Forms.Button btnFacturaPdf;
        protected System.Windows.Forms.Button btnLimpiarMesa;
        private System.Windows.Forms.Button btnResumenMensual;
        private System.Windows.Forms.Panel panelResumenDiario;
        private System.Windows.Forms.DateTimePicker diaCaja;
        private System.Windows.Forms.Button btnResumenDiario;
        protected System.Windows.Forms.Button btnRecargar;
        protected System.Windows.Forms.Button btnVolver;
    }
}
