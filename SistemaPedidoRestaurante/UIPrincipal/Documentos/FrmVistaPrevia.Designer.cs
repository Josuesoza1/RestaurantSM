namespace SistemaPedidoRestaurante.UIPrincipal
{
    partial class FrmVistaPrevia
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Panel panelDatos;
        private System.Windows.Forms.DataGridView tablaDetalle;
        private System.Windows.Forms.Panel acciones;
        private System.Windows.Forms.Button btnPdf;
        private System.Windows.Forms.Button btnExcel;
        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.SaveFileDialog dialogoPdf;
        private System.Windows.Forms.SaveFileDialog dialogoExcel;
        private System.Windows.Forms.Label lblNumero;
        private System.Windows.Forms.TextBox txtNumero;
        private System.Windows.Forms.Label lblFecha;
        private System.Windows.Forms.TextBox txtFecha;
        private System.Windows.Forms.Label lblCliente;
        private System.Windows.Forms.TextBox txtCliente;
        private System.Windows.Forms.Label lblMesa;
        private System.Windows.Forms.TextBox txtMesa;
        private System.Windows.Forms.Label lblAtendido;
        private System.Windows.Forms.TextBox txtAtendido;
        private System.Windows.Forms.Label lblEstado;
        private System.Windows.Forms.TextBox txtEstado;
        private System.Windows.Forms.Label lblSubtotal;
        private System.Windows.Forms.TextBox txtSubtotal;
        private System.Windows.Forms.Label lblDescuento;
        private System.Windows.Forms.TextBox txtDescuento;
        private System.Windows.Forms.Label lblBaseImpuesto;
        private System.Windows.Forms.TextBox txtBaseImpuesto;
        private System.Windows.Forms.Label lblImpuesto;
        private System.Windows.Forms.TextBox txtImpuesto;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.TextBox txtTotal;
        private System.Windows.Forms.Label lblRecibido;
        private System.Windows.Forms.TextBox txtRecibido;
        private System.Windows.Forms.Label lblVuelto;
        private System.Windows.Forms.TextBox txtVuelto;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProducto;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCantidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPrecio;
        private System.Windows.Forms.DataGridViewTextBoxColumn colImporte;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmVistaPrevia));
            this.dialogoPdf = new System.Windows.Forms.SaveFileDialog();
            this.dialogoExcel = new System.Windows.Forms.SaveFileDialog();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.panelDatos = new System.Windows.Forms.Panel();
            this.lblNumero = new System.Windows.Forms.Label();
            this.txtNumero = new System.Windows.Forms.TextBox();
            this.lblFecha = new System.Windows.Forms.Label();
            this.txtFecha = new System.Windows.Forms.TextBox();
            this.lblCliente = new System.Windows.Forms.Label();
            this.txtCliente = new System.Windows.Forms.TextBox();
            this.lblMesa = new System.Windows.Forms.Label();
            this.txtMesa = new System.Windows.Forms.TextBox();
            this.lblAtendido = new System.Windows.Forms.Label();
            this.txtAtendido = new System.Windows.Forms.TextBox();
            this.lblEstado = new System.Windows.Forms.Label();
            this.txtEstado = new System.Windows.Forms.TextBox();
            this.lblSubtotal = new System.Windows.Forms.Label();
            this.txtSubtotal = new System.Windows.Forms.TextBox();
            this.lblDescuento = new System.Windows.Forms.Label();
            this.txtDescuento = new System.Windows.Forms.TextBox();
            this.lblBaseImpuesto = new System.Windows.Forms.Label();
            this.txtBaseImpuesto = new System.Windows.Forms.TextBox();
            this.lblImpuesto = new System.Windows.Forms.Label();
            this.txtImpuesto = new System.Windows.Forms.TextBox();
            this.lblTotal = new System.Windows.Forms.Label();
            this.txtTotal = new System.Windows.Forms.TextBox();
            this.lblRecibido = new System.Windows.Forms.Label();
            this.txtRecibido = new System.Windows.Forms.TextBox();
            this.lblVuelto = new System.Windows.Forms.Label();
            this.txtVuelto = new System.Windows.Forms.TextBox();
            this.tablaDetalle = new System.Windows.Forms.DataGridView();
            this.acciones = new System.Windows.Forms.Panel();
            this.btnPdf = new System.Windows.Forms.Button();
            this.btnExcel = new System.Windows.Forms.Button();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.lblMetodo = new System.Windows.Forms.Label();
            this.txtMetodo = new System.Windows.Forms.TextBox();
            this.lblRegistrado = new System.Windows.Forms.Label();
            this.txtRegistrado = new System.Windows.Forms.TextBox();
            this.lblRestaurante = new System.Windows.Forms.Label();
            this.txtRestaurante = new System.Windows.Forms.TextBox();
            this.panelDatos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tablaDetalle)).BeginInit();
            this.acciones.SuspendLayout();
            this.SuspendLayout();
            // 
            // dialogoPdf
            // 
            this.dialogoPdf.DefaultExt = "pdf";
            this.dialogoPdf.Filter = "Documento PDF (*.pdf)|*.pdf";
            // 
            // dialogoExcel
            // 
            this.dialogoExcel.DefaultExt = "xlsx";
            this.dialogoExcel.Filter = "Libro de Excel (*.xlsx)|*.xlsx";
            // 
            // lblTitulo
            // 
            this.lblTitulo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTitulo.BackColor = System.Drawing.Color.Red;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Location = new System.Drawing.Point(0, 0);
            this.lblTitulo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Padding = new System.Windows.Forms.Padding(22, 0, 0, 0);
            this.lblTitulo.Size = new System.Drawing.Size(1400, 80);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Vista previa de la factura";
            this.lblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panelDatos
            // 
            this.panelDatos.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelDatos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.panelDatos.Controls.Add(this.lblNumero);
            this.panelDatos.Controls.Add(this.txtNumero);
            this.panelDatos.Controls.Add(this.lblFecha);
            this.panelDatos.Controls.Add(this.txtFecha);
            this.panelDatos.Controls.Add(this.lblCliente);
            this.panelDatos.Controls.Add(this.txtCliente);
            this.panelDatos.Controls.Add(this.lblMetodo);
            this.panelDatos.Controls.Add(this.txtMetodo);
            this.panelDatos.Controls.Add(this.lblMesa);
            this.panelDatos.Controls.Add(this.txtMesa);
            this.panelDatos.Controls.Add(this.lblAtendido);
            this.panelDatos.Controls.Add(this.txtAtendido);
            this.panelDatos.Controls.Add(this.lblEstado);
            this.panelDatos.Controls.Add(this.txtEstado);
            this.panelDatos.Controls.Add(this.lblRestaurante);
            this.panelDatos.Controls.Add(this.txtRestaurante);
            this.panelDatos.Controls.Add(this.lblSubtotal);
            this.panelDatos.Controls.Add(this.txtSubtotal);
            this.panelDatos.Controls.Add(this.lblDescuento);
            this.panelDatos.Controls.Add(this.txtDescuento);
            this.panelDatos.Controls.Add(this.lblBaseImpuesto);
            this.panelDatos.Controls.Add(this.txtBaseImpuesto);
            this.panelDatos.Controls.Add(this.lblImpuesto);
            this.panelDatos.Controls.Add(this.txtImpuesto);
            this.panelDatos.Controls.Add(this.lblTotal);
            this.panelDatos.Controls.Add(this.txtTotal);
            this.panelDatos.Controls.Add(this.lblRecibido);
            this.panelDatos.Controls.Add(this.txtRecibido);
            this.panelDatos.Controls.Add(this.lblVuelto);
            this.panelDatos.Controls.Add(this.txtVuelto);
            this.panelDatos.Controls.Add(this.lblRegistrado);
            this.panelDatos.Controls.Add(this.txtRegistrado);
            this.panelDatos.Location = new System.Drawing.Point(18, 100);
            this.panelDatos.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelDatos.Name = "panelDatos";
            this.panelDatos.Size = new System.Drawing.Size(1365, 450);
            this.panelDatos.TabIndex = 1;
            // 
            // lblNumero
            // 
            this.lblNumero.Location = new System.Drawing.Point(20, 18);
            this.lblNumero.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNumero.Name = "lblNumero";
            this.lblNumero.Size = new System.Drawing.Size(138, 38);
            this.lblNumero.TabIndex = 0;
            this.lblNumero.Text = "Número";
            this.lblNumero.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtNumero
            // 
            this.txtNumero.AccessibleName = "Número";
            this.txtNumero.BackColor = System.Drawing.Color.White;
            this.txtNumero.Location = new System.Drawing.Point(162, 18);
            this.txtNumero.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtNumero.Name = "txtNumero";
            this.txtNumero.ReadOnly = true;
            this.txtNumero.Size = new System.Drawing.Size(362, 30);
            this.txtNumero.TabIndex = 1;
            this.txtNumero.TabStop = false;
            this.txtNumero.Text = "—";
            // 
            // lblFecha
            // 
            this.lblFecha.Location = new System.Drawing.Point(20, 70);
            this.lblFecha.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFecha.Name = "lblFecha";
            this.lblFecha.Size = new System.Drawing.Size(138, 38);
            this.lblFecha.TabIndex = 2;
            this.lblFecha.Text = "Fecha";
            this.lblFecha.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtFecha
            // 
            this.txtFecha.AccessibleName = "Fecha";
            this.txtFecha.BackColor = System.Drawing.Color.White;
            this.txtFecha.Location = new System.Drawing.Point(162, 70);
            this.txtFecha.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtFecha.Name = "txtFecha";
            this.txtFecha.ReadOnly = true;
            this.txtFecha.Size = new System.Drawing.Size(362, 30);
            this.txtFecha.TabIndex = 3;
            this.txtFecha.TabStop = false;
            this.txtFecha.Text = "—";
            // 
            // lblCliente
            // 
            this.lblCliente.Location = new System.Drawing.Point(20, 122);
            this.lblCliente.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCliente.Name = "lblCliente";
            this.lblCliente.Size = new System.Drawing.Size(138, 38);
            this.lblCliente.TabIndex = 4;
            this.lblCliente.Text = "Cliente";
            this.lblCliente.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtCliente
            // 
            this.txtCliente.AccessibleName = "Cliente";
            this.txtCliente.BackColor = System.Drawing.Color.White;
            this.txtCliente.Location = new System.Drawing.Point(162, 122);
            this.txtCliente.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtCliente.Name = "txtCliente";
            this.txtCliente.ReadOnly = true;
            this.txtCliente.Size = new System.Drawing.Size(362, 30);
            this.txtCliente.TabIndex = 5;
            this.txtCliente.TabStop = false;
            this.txtCliente.Text = "—";
            // 
            // lblMesa
            // 
            this.lblMesa.Location = new System.Drawing.Point(20, 228);
            this.lblMesa.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMesa.Name = "lblMesa";
            this.lblMesa.Size = new System.Drawing.Size(138, 38);
            this.lblMesa.TabIndex = 8;
            this.lblMesa.Text = "Mesa";
            this.lblMesa.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtMesa
            // 
            this.txtMesa.AccessibleName = "Mesa";
            this.txtMesa.BackColor = System.Drawing.Color.White;
            this.txtMesa.Location = new System.Drawing.Point(162, 228);
            this.txtMesa.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtMesa.Name = "txtMesa";
            this.txtMesa.ReadOnly = true;
            this.txtMesa.Size = new System.Drawing.Size(362, 30);
            this.txtMesa.TabIndex = 9;
            this.txtMesa.TabStop = false;
            this.txtMesa.Text = "—";
            // 
            // lblAtendido
            // 
            this.lblAtendido.Location = new System.Drawing.Point(20, 280);
            this.lblAtendido.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAtendido.Name = "lblAtendido";
            this.lblAtendido.Size = new System.Drawing.Size(138, 38);
            this.lblAtendido.TabIndex = 10;
            this.lblAtendido.Text = "Atendido por";
            this.lblAtendido.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtAtendido
            // 
            this.txtAtendido.AccessibleName = "Atendido por";
            this.txtAtendido.BackColor = System.Drawing.Color.White;
            this.txtAtendido.Location = new System.Drawing.Point(162, 280);
            this.txtAtendido.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtAtendido.Name = "txtAtendido";
            this.txtAtendido.ReadOnly = true;
            this.txtAtendido.Size = new System.Drawing.Size(362, 30);
            this.txtAtendido.TabIndex = 11;
            this.txtAtendido.TabStop = false;
            this.txtAtendido.Text = "—";
            // 
            // lblEstado
            // 
            this.lblEstado.Location = new System.Drawing.Point(20, 332);
            this.lblEstado.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Size = new System.Drawing.Size(138, 38);
            this.lblEstado.TabIndex = 12;
            this.lblEstado.Text = "Estado";
            this.lblEstado.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtEstado
            // 
            this.txtEstado.AccessibleName = "Estado";
            this.txtEstado.BackColor = System.Drawing.Color.White;
            this.txtEstado.Location = new System.Drawing.Point(162, 332);
            this.txtEstado.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtEstado.Name = "txtEstado";
            this.txtEstado.ReadOnly = true;
            this.txtEstado.Size = new System.Drawing.Size(362, 30);
            this.txtEstado.TabIndex = 13;
            this.txtEstado.TabStop = false;
            this.txtEstado.Text = "—";
            // 
            // lblSubtotal
            // 
            this.lblSubtotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSubtotal.Location = new System.Drawing.Point(688, 18);
            this.lblSubtotal.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSubtotal.Name = "lblSubtotal";
            this.lblSubtotal.Size = new System.Drawing.Size(188, 38);
            this.lblSubtotal.TabIndex = 16;
            this.lblSubtotal.Text = "Subtotal";
            this.lblSubtotal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtSubtotal
            // 
            this.txtSubtotal.AccessibleName = "Subtotal";
            this.txtSubtotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSubtotal.BackColor = System.Drawing.Color.White;
            this.txtSubtotal.Location = new System.Drawing.Point(888, 18);
            this.txtSubtotal.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtSubtotal.Name = "txtSubtotal";
            this.txtSubtotal.ReadOnly = true;
            this.txtSubtotal.Size = new System.Drawing.Size(436, 30);
            this.txtSubtotal.TabIndex = 17;
            this.txtSubtotal.TabStop = false;
            this.txtSubtotal.Text = "—";
            // 
            // lblDescuento
            // 
            this.lblDescuento.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDescuento.Location = new System.Drawing.Point(688, 70);
            this.lblDescuento.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDescuento.Name = "lblDescuento";
            this.lblDescuento.Size = new System.Drawing.Size(188, 38);
            this.lblDescuento.TabIndex = 18;
            this.lblDescuento.Text = "Descuento";
            this.lblDescuento.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtDescuento
            // 
            this.txtDescuento.AccessibleName = "Descuento";
            this.txtDescuento.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDescuento.BackColor = System.Drawing.Color.White;
            this.txtDescuento.Location = new System.Drawing.Point(888, 70);
            this.txtDescuento.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtDescuento.Name = "txtDescuento";
            this.txtDescuento.ReadOnly = true;
            this.txtDescuento.Size = new System.Drawing.Size(436, 30);
            this.txtDescuento.TabIndex = 19;
            this.txtDescuento.TabStop = false;
            this.txtDescuento.Text = "—";
            // 
            // lblBaseImpuesto
            // 
            this.lblBaseImpuesto.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblBaseImpuesto.Location = new System.Drawing.Point(688, 122);
            this.lblBaseImpuesto.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBaseImpuesto.Name = "lblBaseImpuesto";
            this.lblBaseImpuesto.Size = new System.Drawing.Size(188, 38);
            this.lblBaseImpuesto.TabIndex = 20;
            this.lblBaseImpuesto.Text = "Base para el impuesto";
            this.lblBaseImpuesto.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtBaseImpuesto
            // 
            this.txtBaseImpuesto.AccessibleName = "Base para el impuesto";
            this.txtBaseImpuesto.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBaseImpuesto.BackColor = System.Drawing.Color.White;
            this.txtBaseImpuesto.Location = new System.Drawing.Point(888, 122);
            this.txtBaseImpuesto.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtBaseImpuesto.Name = "txtBaseImpuesto";
            this.txtBaseImpuesto.ReadOnly = true;
            this.txtBaseImpuesto.Size = new System.Drawing.Size(436, 30);
            this.txtBaseImpuesto.TabIndex = 21;
            this.txtBaseImpuesto.TabStop = false;
            this.txtBaseImpuesto.Text = "—";
            // 
            // lblImpuesto
            // 
            this.lblImpuesto.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblImpuesto.Location = new System.Drawing.Point(688, 175);
            this.lblImpuesto.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblImpuesto.Name = "lblImpuesto";
            this.lblImpuesto.Size = new System.Drawing.Size(188, 38);
            this.lblImpuesto.TabIndex = 22;
            this.lblImpuesto.Text = "Impuesto";
            this.lblImpuesto.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtImpuesto
            // 
            this.txtImpuesto.AccessibleName = "Impuesto";
            this.txtImpuesto.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtImpuesto.BackColor = System.Drawing.Color.White;
            this.txtImpuesto.Location = new System.Drawing.Point(888, 175);
            this.txtImpuesto.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtImpuesto.Name = "txtImpuesto";
            this.txtImpuesto.ReadOnly = true;
            this.txtImpuesto.Size = new System.Drawing.Size(436, 30);
            this.txtImpuesto.TabIndex = 23;
            this.txtImpuesto.TabStop = false;
            this.txtImpuesto.Text = "—";
            // 
            // lblTotal
            // 
            this.lblTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTotal.Location = new System.Drawing.Point(688, 228);
            this.lblTotal.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(188, 38);
            this.lblTotal.TabIndex = 24;
            this.lblTotal.Text = "Total";
            this.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtTotal
            // 
            this.txtTotal.AccessibleName = "Total";
            this.txtTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTotal.BackColor = System.Drawing.Color.White;
            this.txtTotal.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.txtTotal.Location = new System.Drawing.Point(888, 228);
            this.txtTotal.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.ReadOnly = true;
            this.txtTotal.Size = new System.Drawing.Size(436, 34);
            this.txtTotal.TabIndex = 25;
            this.txtTotal.TabStop = false;
            this.txtTotal.Text = "—";
            // 
            // lblRecibido
            // 
            this.lblRecibido.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblRecibido.Location = new System.Drawing.Point(688, 280);
            this.lblRecibido.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRecibido.Name = "lblRecibido";
            this.lblRecibido.Size = new System.Drawing.Size(188, 38);
            this.lblRecibido.TabIndex = 26;
            this.lblRecibido.Text = "Recibido";
            this.lblRecibido.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtRecibido
            // 
            this.txtRecibido.AccessibleName = "Recibido";
            this.txtRecibido.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRecibido.BackColor = System.Drawing.Color.White;
            this.txtRecibido.Location = new System.Drawing.Point(888, 280);
            this.txtRecibido.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtRecibido.Name = "txtRecibido";
            this.txtRecibido.ReadOnly = true;
            this.txtRecibido.Size = new System.Drawing.Size(436, 30);
            this.txtRecibido.TabIndex = 27;
            this.txtRecibido.TabStop = false;
            this.txtRecibido.Text = "—";
            // 
            // lblVuelto
            // 
            this.lblVuelto.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblVuelto.Location = new System.Drawing.Point(688, 332);
            this.lblVuelto.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblVuelto.Name = "lblVuelto";
            this.lblVuelto.Size = new System.Drawing.Size(188, 38);
            this.lblVuelto.TabIndex = 28;
            this.lblVuelto.Text = "Vuelto";
            this.lblVuelto.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtVuelto
            // 
            this.txtVuelto.AccessibleName = "Vuelto";
            this.txtVuelto.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtVuelto.BackColor = System.Drawing.Color.White;
            this.txtVuelto.Location = new System.Drawing.Point(888, 332);
            this.txtVuelto.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtVuelto.Name = "txtVuelto";
            this.txtVuelto.ReadOnly = true;
            this.txtVuelto.Size = new System.Drawing.Size(436, 30);
            this.txtVuelto.TabIndex = 29;
            this.txtVuelto.TabStop = false;
            this.txtVuelto.Text = "—";
            // 
            // tablaDetalle
            // 
            this.tablaDetalle.AccessibleName = "Productos de la factura";
            this.tablaDetalle.AllowUserToAddRows = false;
            this.tablaDetalle.AllowUserToDeleteRows = false;
            this.tablaDetalle.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tablaDetalle.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.tablaDetalle.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.tablaDetalle.BackgroundColor = System.Drawing.Color.White;
            this.tablaDetalle.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 10F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.tablaDetalle.DefaultCellStyle = dataGridViewCellStyle1;
            this.tablaDetalle.Location = new System.Drawing.Point(18, 570);
            this.tablaDetalle.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tablaDetalle.Name = "tablaDetalle";
            this.tablaDetalle.ReadOnly = true;
            this.tablaDetalle.RowHeadersVisible = false;
            this.tablaDetalle.RowHeadersWidth = 51;
            this.tablaDetalle.Size = new System.Drawing.Size(1365, 262);
            this.tablaDetalle.TabIndex = 2;
            this.tablaDetalle.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.tablaDetalle_DataBindingComplete);
            // 
            // acciones
            // 
            this.acciones.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.acciones.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.acciones.Controls.Add(this.btnPdf);
            this.acciones.Controls.Add(this.btnExcel);
            this.acciones.Controls.Add(this.btnCerrar);
            this.acciones.Location = new System.Drawing.Point(18, 850);
            this.acciones.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.acciones.Name = "acciones";
            this.acciones.Size = new System.Drawing.Size(1365, 70);
            this.acciones.TabIndex = 3;
            // 
            // btnPdf
            // 
            this.btnPdf.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnPdf.Location = new System.Drawing.Point(15, 12);
            this.btnPdf.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnPdf.Name = "btnPdf";
            this.btnPdf.Size = new System.Drawing.Size(192, 45);
            this.btnPdf.TabIndex = 0;
            this.btnPdf.Text = "Guardar PDF";
            this.btnPdf.UseVisualStyleBackColor = false;
            this.btnPdf.Click += new System.EventHandler(this.btnPdf_Click);
            // 
            // btnExcel
            // 
            this.btnExcel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnExcel.Location = new System.Drawing.Point(228, 12);
            this.btnExcel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(205, 45);
            this.btnExcel.TabIndex = 1;
            this.btnExcel.Text = "Guardar Excel";
            this.btnExcel.UseVisualStyleBackColor = false;
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // btnCerrar
            // 
            this.btnCerrar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnCerrar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCerrar.Location = new System.Drawing.Point(452, 12);
            this.btnCerrar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(150, 45);
            this.btnCerrar.TabIndex = 2;
            this.btnCerrar.Text = "Cerrar";
            this.btnCerrar.UseVisualStyleBackColor = false;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // lblMetodo
            // 
            this.lblMetodo.Location = new System.Drawing.Point(20, 175);
            this.lblMetodo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMetodo.Name = "lblMetodo";
            this.lblMetodo.Size = new System.Drawing.Size(138, 38);
            this.lblMetodo.TabIndex = 6;
            this.lblMetodo.Text = "Forma de pago";
            this.lblMetodo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtMetodo
            // 
            this.txtMetodo.AccessibleName = "Forma de pago";
            this.txtMetodo.BackColor = System.Drawing.Color.White;
            this.txtMetodo.Location = new System.Drawing.Point(162, 175);
            this.txtMetodo.Margin = new System.Windows.Forms.Padding(4);
            this.txtMetodo.Name = "txtMetodo";
            this.txtMetodo.ReadOnly = true;
            this.txtMetodo.Size = new System.Drawing.Size(362, 30);
            this.txtMetodo.TabIndex = 7;
            this.txtMetodo.TabStop = false;
            this.txtMetodo.Text = "—";
            // 
            // lblRegistrado
            // 
            this.lblRegistrado.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblRegistrado.Location = new System.Drawing.Point(688, 385);
            this.lblRegistrado.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRegistrado.Name = "lblRegistrado";
            this.lblRegistrado.Size = new System.Drawing.Size(188, 38);
            this.lblRegistrado.TabIndex = 30;
            this.lblRegistrado.Text = "Registrado por";
            this.lblRegistrado.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtRegistrado
            // 
            this.txtRegistrado.AccessibleName = "Registrado por";
            this.txtRegistrado.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRegistrado.BackColor = System.Drawing.Color.White;
            this.txtRegistrado.Location = new System.Drawing.Point(888, 385);
            this.txtRegistrado.Margin = new System.Windows.Forms.Padding(4);
            this.txtRegistrado.Name = "txtRegistrado";
            this.txtRegistrado.ReadOnly = true;
            this.txtRegistrado.Size = new System.Drawing.Size(436, 30);
            this.txtRegistrado.TabIndex = 31;
            this.txtRegistrado.TabStop = false;
            this.txtRegistrado.Text = "—";
            // 
            // lblRestaurante
            // 
            this.lblRestaurante.Location = new System.Drawing.Point(20, 385);
            this.lblRestaurante.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRestaurante.Name = "lblRestaurante";
            this.lblRestaurante.Size = new System.Drawing.Size(138, 38);
            this.lblRestaurante.TabIndex = 14;
            this.lblRestaurante.Text = "Restaurante";
            this.lblRestaurante.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtRestaurante
            // 
            this.txtRestaurante.AccessibleName = "Restaurante";
            this.txtRestaurante.BackColor = System.Drawing.Color.White;
            this.txtRestaurante.Location = new System.Drawing.Point(162, 385);
            this.txtRestaurante.Margin = new System.Windows.Forms.Padding(4);
            this.txtRestaurante.Name = "txtRestaurante";
            this.txtRestaurante.ReadOnly = true;
            this.txtRestaurante.Size = new System.Drawing.Size(362, 30);
            this.txtRestaurante.TabIndex = 15;
            this.txtRestaurante.TabStop = false;
            this.txtRestaurante.Text = "—";
            // 
            // FrmVistaPrevia
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.CancelButton = this.btnCerrar;
            this.ClientSize = new System.Drawing.Size(1400, 938);
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.panelDatos);
            this.Controls.Add(this.tablaDetalle);
            this.Controls.Add(this.acciones);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MinimumSize = new System.Drawing.Size(1266, 887);
            this.Name = "FrmVistaPrevia";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Vista previa";
            this.panelDatos.ResumeLayout(false);
            this.panelDatos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tablaDetalle)).EndInit();
            this.acciones.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Label lblMetodo;
        private System.Windows.Forms.TextBox txtMetodo;
        private System.Windows.Forms.Label lblRestaurante;
        private System.Windows.Forms.TextBox txtRestaurante;
        private System.Windows.Forms.Label lblRegistrado;
        private System.Windows.Forms.TextBox txtRegistrado;
    }
}
