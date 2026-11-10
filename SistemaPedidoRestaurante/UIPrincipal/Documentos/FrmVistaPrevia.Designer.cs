namespace SistemaPedidoRestaurante.UIPrincipal
{
    partial class FrmVistaPrevia
    {
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.TableLayoutPanel panelDatos;
        private System.Windows.Forms.TextBox txtDatos;
        private System.Windows.Forms.TextBox txtImportes;
        private System.Windows.Forms.DataGridView tablaDetalle;
        private System.Windows.Forms.FlowLayoutPanel acciones;
        private System.Windows.Forms.Button btnPdf;
        private System.Windows.Forms.Button btnExcel;
        private System.Windows.Forms.Button btnCerrar;

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmVistaPrevia));
            this.lblTitulo = new System.Windows.Forms.Label();
            this.panelDatos = new System.Windows.Forms.TableLayoutPanel();
            this.txtDatos = new System.Windows.Forms.TextBox();
            this.txtImportes = new System.Windows.Forms.TextBox();
            this.tablaDetalle = new System.Windows.Forms.DataGridView();
            this.acciones = new System.Windows.Forms.FlowLayoutPanel();
            this.btnPdf = new System.Windows.Forms.Button();
            this.btnExcel = new System.Windows.Forms.Button();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.panelDatos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tablaDetalle)).BeginInit();
            this.acciones.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitulo
            // 
            this.lblTitulo.BackColor = System.Drawing.Color.Red;
            this.lblTitulo.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Location = new System.Drawing.Point(0, 0);
            this.lblTitulo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Padding = new System.Windows.Forms.Padding(16, 0, 16, 0);
            this.lblTitulo.Size = new System.Drawing.Size(1093, 59);
            this.lblTitulo.TabIndex = 2;
            this.lblTitulo.Text = "Vista previa";
            this.lblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panelDatos
            // 
            this.panelDatos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.panelDatos.ColumnCount = 2;
            this.panelDatos.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 63F));
            this.panelDatos.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 37F));
            this.panelDatos.Controls.Add(this.txtDatos, 0, 0);
            this.panelDatos.Controls.Add(this.txtImportes, 1, 0);
            this.panelDatos.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelDatos.Location = new System.Drawing.Point(0, 59);
            this.panelDatos.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelDatos.Name = "panelDatos";
            this.panelDatos.Padding = new System.Windows.Forms.Padding(11, 10, 11, 10);
            this.panelDatos.RowCount = 1;
            this.panelDatos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.panelDatos.Size = new System.Drawing.Size(1093, 234);
            this.panelDatos.TabIndex = 1;
            // 
            // txtDatos
            // 
            this.txtDatos.AccessibleName = "Datos del documento";
            this.txtDatos.BackColor = System.Drawing.Color.White;
            this.txtDatos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDatos.Location = new System.Drawing.Point(15, 14);
            this.txtDatos.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtDatos.Multiline = true;
            this.txtDatos.Name = "txtDatos";
            this.txtDatos.ReadOnly = true;
            this.txtDatos.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDatos.Size = new System.Drawing.Size(666, 206);
            this.txtDatos.TabIndex = 0;
            // 
            // txtImportes
            // 
            this.txtImportes.AccessibleName = "Importes del documento";
            this.txtImportes.BackColor = System.Drawing.Color.White;
            this.txtImportes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtImportes.Location = new System.Drawing.Point(689, 14);
            this.txtImportes.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtImportes.Multiline = true;
            this.txtImportes.Name = "txtImportes";
            this.txtImportes.ReadOnly = true;
            this.txtImportes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtImportes.Size = new System.Drawing.Size(389, 206);
            this.txtImportes.TabIndex = 1;
            // 
            // tablaDetalle
            // 
            this.tablaDetalle.AccessibleName = "Detalle del documento";
            this.tablaDetalle.AllowUserToAddRows = false;
            this.tablaDetalle.AllowUserToDeleteRows = false;
            this.tablaDetalle.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.tablaDetalle.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.tablaDetalle.BackgroundColor = System.Drawing.Color.White;
            this.tablaDetalle.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.tablaDetalle.DefaultCellStyle = dataGridViewCellStyle1;
            this.tablaDetalle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tablaDetalle.Location = new System.Drawing.Point(0, 293);
            this.tablaDetalle.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tablaDetalle.Name = "tablaDetalle";
            this.tablaDetalle.ReadOnly = true;
            this.tablaDetalle.RowHeadersVisible = false;
            this.tablaDetalle.RowHeadersWidth = 51;
            this.tablaDetalle.Size = new System.Drawing.Size(1093, 398);
            this.tablaDetalle.TabIndex = 0;
            this.tablaDetalle.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.tablaDetalle_DataBindingComplete);
            // 
            // acciones
            // 
            this.acciones.AutoSize = true;
            this.acciones.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.acciones.Controls.Add(this.btnPdf);
            this.acciones.Controls.Add(this.btnExcel);
            this.acciones.Controls.Add(this.btnCerrar);
            this.acciones.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.acciones.Location = new System.Drawing.Point(0, 691);
            this.acciones.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.acciones.Name = "acciones";
            this.acciones.Padding = new System.Windows.Forms.Padding(11, 10, 11, 10);
            this.acciones.Size = new System.Drawing.Size(1093, 60);
            this.acciones.TabIndex = 3;
            // 
            // btnPdf
            // 
            this.btnPdf.AutoSize = true;
            this.btnPdf.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnPdf.Location = new System.Drawing.Point(15, 14);
            this.btnPdf.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnPdf.Name = "btnPdf";
            this.btnPdf.Size = new System.Drawing.Size(128, 32);
            this.btnPdf.TabIndex = 0;
            this.btnPdf.Text = "Guardar PDF";
            this.btnPdf.UseVisualStyleBackColor = false;
            this.btnPdf.Click += new System.EventHandler(this.btnPdf_Click);
            // 
            // btnExcel
            // 
            this.btnExcel.AutoSize = true;
            this.btnExcel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnExcel.Location = new System.Drawing.Point(151, 14);
            this.btnExcel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(136, 32);
            this.btnExcel.TabIndex = 1;
            this.btnExcel.Text = "Guardar Excel";
            this.btnExcel.UseVisualStyleBackColor = false;
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // btnCerrar
            // 
            this.btnCerrar.AutoSize = true;
            this.btnCerrar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnCerrar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCerrar.Location = new System.Drawing.Point(295, 14);
            this.btnCerrar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(100, 32);
            this.btnCerrar.TabIndex = 2;
            this.btnCerrar.Text = "Cerrar";
            this.btnCerrar.UseVisualStyleBackColor = false;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // FrmVistaPrevia
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCerrar;
            this.ClientSize = new System.Drawing.Size(1093, 751);
            this.Controls.Add(this.tablaDetalle);
            this.Controls.Add(this.panelDatos);
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.acciones);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MinimumSize = new System.Drawing.Size(821, 568);
            this.Name = "FrmVistaPrevia";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Vista previa";
            this.panelDatos.ResumeLayout(false);
            this.panelDatos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tablaDetalle)).EndInit();
            this.acciones.ResumeLayout(false);
            this.acciones.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
