namespace SistemaPedidoRestaurante.UIAdministracion
{
    partial class FrmInventario
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
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.agregarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eliminarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.desactivarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.volverToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ayudaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.grpAcciones = new System.Windows.Forms.GroupBox();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.lblBuscar = new System.Windows.Forms.Label();
            this.btnGuardarProducto = new System.Windows.Forms.Button();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.grpListaProductos = new System.Windows.Forms.GroupBox();
            this.dgvProductoInventario = new System.Windows.Forms.DataGridView();
            this.ColProducto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColCategoria = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColUnidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColPrecioVenta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColStockActual = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColSrockMinimo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grpAgregarProducto = new System.Windows.Forms.GroupBox();
            this.cmbCategoriaInventario = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPrecioVenta = new System.Windows.Forms.TextBox();
            this.lblPrecioVenta = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.lblStockMinimo = new System.Windows.Forms.Label();
            this.txtUnidad = new System.Windows.Forms.TextBox();
            this.lblUnidad = new System.Windows.Forms.Label();
            this.lblStockActual = new System.Windows.Forms.Label();
            this.txtStockActual = new System.Windows.Forms.TextBox();
            this.cmbProductoInventario = new System.Windows.Forms.ComboBox();
            this.lblProducto = new System.Windows.Forms.Label();
            this.chkControlActivo = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.grpAcciones.SuspendLayout();
            this.grpListaProductos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductoInventario)).BeginInit();
            this.grpAgregarProducto.SuspendLayout();
            this.panel1.SuspendLayout();
            this.movimientos = new System.Windows.Forms.ToolStripMenuItem();
            this.entradaSalida = new System.Windows.Forms.ToolStripMenuItem();
            this.historial = new System.Windows.Forms.ToolStripMenuItem();
            this.bajos = new System.Windows.Forms.ToolStripMenuItem();
            this.actualizar = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlOperacion = new System.Windows.Forms.Panel();
            this.lblOperacion = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.agregarToolStripMenuItem,
                this.eliminarToolStripMenuItem,
                this.desactivarToolStripMenuItem,
                this.editarToolStripMenuItem,
                this.volverToolStripMenuItem,
                this.ayudaToolStripMenuItem,
                this.bajos,
                this.movimientos,
                this.actualizar });
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1025, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // agregarToolStripMenuItem
            // 
            this.agregarToolStripMenuItem.Name = "agregarToolStripMenuItem";
            this.agregarToolStripMenuItem.Size = new System.Drawing.Size(77, 24);
            this.agregarToolStripMenuItem.Text = "Agregar";
            // 
            // eliminarToolStripMenuItem
            // 
            this.eliminarToolStripMenuItem.Name = "eliminarToolStripMenuItem";
            this.eliminarToolStripMenuItem.Size = new System.Drawing.Size(77, 24);
            this.eliminarToolStripMenuItem.Text = "Eliminar";
            this.eliminarToolStripMenuItem.Click += new System.EventHandler(this.eliminarToolStripMenuItem_Click);
            // 
            // desactivarToolStripMenuItem
            // 
            this.desactivarToolStripMenuItem.Name = "desactivarToolStripMenuItem";
            this.desactivarToolStripMenuItem.Size = new System.Drawing.Size(118, 24);
            this.desactivarToolStripMenuItem.Text = "Retirar control";
            this.desactivarToolStripMenuItem.Click += new System.EventHandler(this.desactivarToolStripMenuItem_Click);
            // 
            // editarToolStripMenuItem
            // 
            this.editarToolStripMenuItem.Name = "editarToolStripMenuItem";
            this.editarToolStripMenuItem.Size = new System.Drawing.Size(62, 24);
            this.editarToolStripMenuItem.Text = "Editar";
            // 
            // volverToolStripMenuItem
            // 
            this.volverToolStripMenuItem.Name = "volverToolStripMenuItem";
            this.volverToolStripMenuItem.Size = new System.Drawing.Size(64, 24);
            this.volverToolStripMenuItem.Text = "Volver";
            // 
            // ayudaToolStripMenuItem
            // 
            this.ayudaToolStripMenuItem.Name = "ayudaToolStripMenuItem";
            this.ayudaToolStripMenuItem.Size = new System.Drawing.Size(65, 24);
            this.ayudaToolStripMenuItem.Text = "Ayuda";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // grpAcciones
            // 
            this.grpAcciones.Controls.Add(this.btnBuscar);
            this.grpAcciones.Controls.Add(this.textBox2);
            this.grpAcciones.Controls.Add(this.lblBuscar);
            this.grpAcciones.Controls.Add(this.btnGuardarProducto);
            this.grpAcciones.Controls.Add(this.btnLimpiar);
            this.grpAcciones.Controls.Add(this.btnCancelar);
            this.grpAcciones.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpAcciones.ForeColor = System.Drawing.Color.DarkSlateBlue;
            this.grpAcciones.Location = new System.Drawing.Point(665, 174);
            this.grpAcciones.Margin = new System.Windows.Forms.Padding(4);
            this.grpAcciones.Name = "grpAcciones";
            this.grpAcciones.Padding = new System.Windows.Forms.Padding(4);
            this.grpAcciones.Size = new System.Drawing.Size(343, 219);
            this.grpAcciones.TabIndex = 20;
            this.grpAcciones.TabStop = false;
            this.grpAcciones.Text = "Acciones";
            // 
            // btnBuscar
            // 
            this.btnBuscar.BackColor = System.Drawing.Color.Cyan;
            this.btnBuscar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscar.Font = new System.Drawing.Font("Arial Black", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscar.ForeColor = System.Drawing.Color.Black;
            this.btnBuscar.Location = new System.Drawing.Point(228, 156);
            this.btnBuscar.Margin = new System.Windows.Forms.Padding(4);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(89, 36);
            this.btnBuscar.TabIndex = 12;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = false;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(84, 124);
            this.textBox2.Margin = new System.Windows.Forms.Padding(4);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(232, 25);
            this.textBox2.TabIndex = 11;
            // 
            // lblBuscar
            // 
            this.lblBuscar.AutoSize = true;
            this.lblBuscar.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBuscar.ForeColor = System.Drawing.Color.Black;
            this.lblBuscar.Location = new System.Drawing.Point(8, 127);
            this.lblBuscar.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBuscar.Name = "lblBuscar";
            this.lblBuscar.Size = new System.Drawing.Size(62, 18);
            this.lblBuscar.TabIndex = 10;
            this.lblBuscar.Text = "Buscar:";
            // 
            // btnGuardarProducto
            // 
            this.btnGuardarProducto.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnGuardarProducto.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardarProducto.Font = new System.Drawing.Font("Arial Black", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardarProducto.ForeColor = System.Drawing.Color.Black;
            this.btnGuardarProducto.Location = new System.Drawing.Point(12, 25);
            this.btnGuardarProducto.Margin = new System.Windows.Forms.Padding(4);
            this.btnGuardarProducto.Name = "btnGuardarProducto";
            this.btnGuardarProducto.Size = new System.Drawing.Size(100, 41);
            this.btnGuardarProducto.TabIndex = 2;
            this.btnGuardarProducto.Text = "Guardar";
            this.btnGuardarProducto.UseVisualStyleBackColor = false;
            this.btnGuardarProducto.Click += new System.EventHandler(this.btnGuardarProducto_Click);
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnLimpiar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLimpiar.Font = new System.Drawing.Font("Arial Black", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLimpiar.ForeColor = System.Drawing.Color.Black;
            this.btnLimpiar.Location = new System.Drawing.Point(120, 25);
            this.btnLimpiar.Margin = new System.Windows.Forms.Padding(4);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(100, 41);
            this.btnLimpiar.TabIndex = 4;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.UseVisualStyleBackColor = false;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.Color.Red;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Font = new System.Drawing.Font("Arial Black", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.ForeColor = System.Drawing.Color.Black;
            this.btnCancelar.Location = new System.Drawing.Point(228, 25);
            this.btnCancelar.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(103, 41);
            this.btnCancelar.TabIndex = 5;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // grpListaProductos
            // 
            this.grpListaProductos.Controls.Add(this.dgvProductoInventario);
            this.grpListaProductos.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpListaProductos.ForeColor = System.Drawing.Color.DarkSlateBlue;
            this.grpListaProductos.Location = new System.Drawing.Point(19, 400);
            this.grpListaProductos.Margin = new System.Windows.Forms.Padding(4);
            this.grpListaProductos.Name = "grpListaProductos";
            this.grpListaProductos.Padding = new System.Windows.Forms.Padding(4);
            this.grpListaProductos.Size = new System.Drawing.Size(989, 138);
            this.grpListaProductos.TabIndex = 18;
            this.grpListaProductos.TabStop = false;
            this.grpListaProductos.Text = "Lista de productos";
            // 
            // dgvProductoInventario
            // 
            this.dgvProductoInventario.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProductoInventario.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColProducto,
            this.ColCategoria,
            this.ColUnidad,
            this.ColPrecioVenta,
            this.ColStockActual,
            this.ColSrockMinimo});
            this.dgvProductoInventario.Location = new System.Drawing.Point(27, 25);
            this.dgvProductoInventario.Margin = new System.Windows.Forms.Padding(4);
            this.dgvProductoInventario.Name = "dgvProductoInventario";
            this.dgvProductoInventario.RowHeadersWidth = 51;
            this.dgvProductoInventario.Size = new System.Drawing.Size(944, 89);
            this.dgvProductoInventario.TabIndex = 0;
            // 
            // ColProducto
            // 
            this.ColProducto.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColProducto.HeaderText = "Producto";
            this.ColProducto.MinimumWidth = 6;
            this.ColProducto.Name = "ColProducto";
            // 
            // ColCategoria
            // 
            this.ColCategoria.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColCategoria.HeaderText = "Categoría";
            this.ColCategoria.MinimumWidth = 6;
            this.ColCategoria.Name = "ColCategoria";
            // 
            // ColUnidad
            // 
            this.ColUnidad.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColUnidad.HeaderText = "Unidad";
            this.ColUnidad.MinimumWidth = 6;
            this.ColUnidad.Name = "ColUnidad";
            // 
            // ColPrecioVenta
            // 
            this.ColPrecioVenta.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColPrecioVenta.HeaderText = "Precio de venta";
            this.ColPrecioVenta.MinimumWidth = 6;
            this.ColPrecioVenta.Name = "ColPrecioVenta";
            // 
            // ColStockActual
            // 
            this.ColStockActual.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColStockActual.HeaderText = "Stock Actual";
            this.ColStockActual.MinimumWidth = 6;
            this.ColStockActual.Name = "ColStockActual";
            // 
            // ColSrockMinimo
            // 
            this.ColSrockMinimo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColSrockMinimo.HeaderText = "Stock Mínimo";
            this.ColSrockMinimo.MinimumWidth = 6;
            this.ColSrockMinimo.Name = "ColSrockMinimo";
            // 
            // grpAgregarProducto
            // 
            this.grpAgregarProducto.Controls.Add(this.cmbCategoriaInventario);
            this.grpAgregarProducto.Controls.Add(this.label1);
            this.grpAgregarProducto.Controls.Add(this.txtPrecioVenta);
            this.grpAgregarProducto.Controls.Add(this.lblPrecioVenta);
            this.grpAgregarProducto.Controls.Add(this.textBox1);
            this.grpAgregarProducto.Controls.Add(this.lblStockMinimo);
            this.grpAgregarProducto.Controls.Add(this.txtUnidad);
            this.grpAgregarProducto.Controls.Add(this.lblUnidad);
            this.grpAgregarProducto.Controls.Add(this.lblStockActual);
            this.grpAgregarProducto.Controls.Add(this.txtStockActual);
            this.grpAgregarProducto.Controls.Add(this.cmbProductoInventario);
            this.grpAgregarProducto.Controls.Add(this.lblProducto);
            this.grpAgregarProducto.Controls.Add(this.chkControlActivo);
            this.grpAgregarProducto.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpAgregarProducto.ForeColor = System.Drawing.Color.DarkSlateBlue;
            this.grpAgregarProducto.Location = new System.Drawing.Point(19, 174);
            this.grpAgregarProducto.Margin = new System.Windows.Forms.Padding(4);
            this.grpAgregarProducto.Name = "grpAgregarProducto";
            this.grpAgregarProducto.Padding = new System.Windows.Forms.Padding(4);
            this.grpAgregarProducto.Size = new System.Drawing.Size(627, 219);
            this.grpAgregarProducto.TabIndex = 17;
            this.grpAgregarProducto.TabStop = false;
            this.grpAgregarProducto.Text = "Agregar Producto";
            // 
            // cmbCategoriaInventario
            // 
            this.cmbCategoriaInventario.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCategoriaInventario.FormattingEnabled = true;
            this.cmbCategoriaInventario.Location = new System.Drawing.Point(161, 89);
            this.cmbCategoriaInventario.Margin = new System.Windows.Forms.Padding(4);
            this.cmbCategoriaInventario.Name = "cmbCategoriaInventario";
            this.cmbCategoriaInventario.Size = new System.Drawing.Size(151, 26);
            this.cmbCategoriaInventario.TabIndex = 14;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(19, 92);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 18);
            this.label1.TabIndex = 13;
            this.label1.Text = "Categoría:";
            // 
            // txtPrecioVenta
            // 
            this.txtPrecioVenta.Location = new System.Drawing.Point(161, 182);
            this.txtPrecioVenta.Margin = new System.Windows.Forms.Padding(4);
            this.txtPrecioVenta.Name = "txtPrecioVenta";
            this.txtPrecioVenta.Size = new System.Drawing.Size(148, 25);
            this.txtPrecioVenta.TabIndex = 12;
            // 
            // lblPrecioVenta
            // 
            this.lblPrecioVenta.AutoSize = true;
            this.lblPrecioVenta.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrecioVenta.ForeColor = System.Drawing.Color.Black;
            this.lblPrecioVenta.Location = new System.Drawing.Point(23, 186);
            this.lblPrecioVenta.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPrecioVenta.Name = "lblPrecioVenta";
            this.lblPrecioVenta.Size = new System.Drawing.Size(124, 18);
            this.lblPrecioVenta.TabIndex = 11;
            this.lblPrecioVenta.Text = "Precio de venta:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(457, 85);
            this.textBox1.Margin = new System.Windows.Forms.Padding(4);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(148, 25);
            this.textBox1.TabIndex = 10;
            // 
            // lblStockMinimo
            // 
            this.lblStockMinimo.AutoSize = true;
            this.lblStockMinimo.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStockMinimo.ForeColor = System.Drawing.Color.Black;
            this.lblStockMinimo.Location = new System.Drawing.Point(333, 89);
            this.lblStockMinimo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStockMinimo.Name = "lblStockMinimo";
            this.lblStockMinimo.Size = new System.Drawing.Size(107, 18);
            this.lblStockMinimo.TabIndex = 9;
            this.lblStockMinimo.Text = "Stock Mínimo:";
            // 
            // txtUnidad
            // 
            this.txtUnidad.Location = new System.Drawing.Point(159, 138);
            this.txtUnidad.Margin = new System.Windows.Forms.Padding(4);
            this.txtUnidad.Name = "txtUnidad";
            this.txtUnidad.Size = new System.Drawing.Size(151, 25);
            this.txtUnidad.TabIndex = 8;
            // 
            // lblUnidad
            // 
            this.lblUnidad.AutoSize = true;
            this.lblUnidad.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUnidad.ForeColor = System.Drawing.Color.Black;
            this.lblUnidad.Location = new System.Drawing.Point(23, 142);
            this.lblUnidad.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblUnidad.Name = "lblUnidad";
            this.lblUnidad.Size = new System.Drawing.Size(62, 18);
            this.lblUnidad.TabIndex = 7;
            this.lblUnidad.Text = "Unidad:";
            // 
            // lblStockActual
            // 
            this.lblStockActual.AutoSize = true;
            this.lblStockActual.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStockActual.ForeColor = System.Drawing.Color.Black;
            this.lblStockActual.Location = new System.Drawing.Point(333, 47);
            this.lblStockActual.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStockActual.Name = "lblStockActual";
            this.lblStockActual.Size = new System.Drawing.Size(98, 18);
            this.lblStockActual.TabIndex = 6;
            this.lblStockActual.Text = "Stock Actual:";
            // 
            // txtStockActual
            // 
            this.txtStockActual.Location = new System.Drawing.Point(457, 43);
            this.txtStockActual.Margin = new System.Windows.Forms.Padding(4);
            this.txtStockActual.Name = "txtStockActual";
            this.txtStockActual.Size = new System.Drawing.Size(148, 25);
            this.txtStockActual.TabIndex = 5;
            // 
            // cmbProductoInventario
            // 
            this.cmbProductoInventario.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProductoInventario.Location = new System.Drawing.Point(155, 43);
            this.cmbProductoInventario.Margin = new System.Windows.Forms.Padding(4);
            this.cmbProductoInventario.Name = "cmbProductoInventario";
            this.cmbProductoInventario.Size = new System.Drawing.Size(155, 26);
            this.cmbProductoInventario.TabIndex = 4;
            // 
            // lblProducto
            // 
            this.lblProducto.AutoSize = true;
            this.lblProducto.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProducto.ForeColor = System.Drawing.Color.Black;
            this.lblProducto.Location = new System.Drawing.Point(23, 47);
            this.lblProducto.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblProducto.Name = "lblProducto";
            this.lblProducto.Size = new System.Drawing.Size(77, 18);
            this.lblProducto.TabIndex = 2;
            this.lblProducto.Text = "Producto:";
            // 
            // chkControlActivo
            // 
            this.chkControlActivo.Checked = true;
            this.chkControlActivo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkControlActivo.Location = new System.Drawing.Point(350, 135);
            this.chkControlActivo.Name = "chkControlActivo";
            this.chkControlActivo.Size = new System.Drawing.Size(220, 24);
            this.chkControlActivo.TabIndex = 15;
            this.chkControlActivo.Text = "Control activo";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.DarkSlateBlue;
            this.panel1.Controls.Add(this.lblTitulo);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 28);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1025, 136);
            this.panel1.TabIndex = 16;
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblTitulo.Location = new System.Drawing.Point(80, 41);
            this.lblTitulo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(389, 35);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "GESTION DE INVENTARIO";
            // 
            // FrmInventario
            // 
            // movimientos
            this.movimientos.Name = "movimientos";
            this.movimientos.Text = "Movimientos";
            // entradaSalida
            this.entradaSalida.Name = "entradaSalida";
            this.entradaSalida.Text = "Entrada o salida";
            this.entradaSalida.Click += new System.EventHandler(this.entradaSalida_Click);
            // historial
            this.historial.Name = "historial";
            this.historial.Text = "Historial";
            this.historial.Click += new System.EventHandler(this.historial_Click);
            // bajos
            this.bajos.Name = "bajos";
            this.bajos.Text = "Solo existencias bajas";
            this.bajos.CheckOnClick = true;
            this.bajos.CheckedChanged += new System.EventHandler(this.bajos_CheckedChanged);
            // actualizar
            this.actualizar.Name = "actualizar";
            this.actualizar.Text = "Recargar";
            this.actualizar.Click += new System.EventHandler(this.actualizar_Click);
            this.movimientos.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { this.entradaSalida, this.historial });
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(1025, 549);
            this.Controls.Add(this.grpListaProductos);
            this.Controls.Add(this.grpAcciones);
            this.Controls.Add(this.grpAgregarProducto);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmInventario";
            this.Text = "GESTION DE INVENTARIO";
            this.Load += new System.EventHandler(this.FrmInventario_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.grpAcciones.ResumeLayout(false);
            this.grpAcciones.PerformLayout();
            this.grpListaProductos.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductoInventario)).EndInit();
            this.grpAgregarProducto.ResumeLayout(false);
            this.grpAgregarProducto.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.agregarToolStripMenuItem.Click += new System.EventHandler(this.agregarToolStripMenuItem_Click);
            this.editarToolStripMenuItem.Click += new System.EventHandler(this.editarToolStripMenuItem_Click);
            this.volverToolStripMenuItem.Click += new System.EventHandler(this.volverToolStripMenuItem_Click);
            this.ayudaToolStripMenuItem.Click += new System.EventHandler(this.ayudaToolStripMenuItem_Click);
            this.pnlOperacion.Name = "pnlOperacion";
            this.pnlOperacion.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlOperacion.Height = 26;
            this.pnlOperacion.BackColor = System.Drawing.Color.Gainsboro;
            this.lblOperacion.Name = "lblOperacion";
            this.lblOperacion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblOperacion.Text = "Operación actual: Seleccione una operación";
            this.lblOperacion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.pnlOperacion.Controls.Add(this.lblOperacion);
            this.panel1.Controls.Add(this.pnlOperacion);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkControlActivo;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem agregarToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.GroupBox grpAcciones;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label lblBuscar;
        private System.Windows.Forms.Button btnGuardarProducto;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.GroupBox grpListaProductos;
        private System.Windows.Forms.DataGridView dgvProductoInventario;
        private System.Windows.Forms.GroupBox grpAgregarProducto;
        private System.Windows.Forms.ComboBox cmbCategoriaInventario;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPrecioVenta;
        private System.Windows.Forms.Label lblPrecioVenta;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label lblStockMinimo;
        private System.Windows.Forms.TextBox txtUnidad;
        private System.Windows.Forms.Label lblUnidad;
        private System.Windows.Forms.Label lblStockActual;
        private System.Windows.Forms.TextBox txtStockActual;
        private System.Windows.Forms.ComboBox cmbProductoInventario;
        private System.Windows.Forms.Label lblProducto;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.ToolStripMenuItem eliminarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem desactivarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem volverToolStripMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColProducto;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColCategoria;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColUnidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColPrecioVenta;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColStockActual;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColSrockMinimo;
        private System.Windows.Forms.ToolStripMenuItem ayudaToolStripMenuItem;

        private System.Windows.Forms.ToolStripMenuItem movimientos;
        private System.Windows.Forms.ToolStripMenuItem entradaSalida;
        private System.Windows.Forms.ToolStripMenuItem historial;
        private System.Windows.Forms.ToolStripMenuItem bajos;
        private System.Windows.Forms.ToolStripMenuItem actualizar;

        private System.Windows.Forms.Panel pnlOperacion;
        private System.Windows.Forms.Label lblOperacion;
    }
}