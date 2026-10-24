namespace SistemaPedidoRestaurante.UICliente
{
    partial class FrmMisDatos
    {
        private System.ComponentModel.IContainer components=null;
        protected override void Dispose(bool disposing){if(disposing&&components!=null)components.Dispose();base.Dispose(disposing);}
        private System.Windows.Forms.FlowLayoutPanel acciones;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnVolver;
        private System.Windows.Forms.TableLayoutPanel campos;
        private System.Windows.Forms.ComboBox cmbGenero;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.TextBox txtSegundoNombre;
        private System.Windows.Forms.TextBox txtApellido;
        private System.Windows.Forms.TextBox txtSegundoApellido;
        private System.Windows.Forms.TextBox txtCedula;
        private System.Windows.Forms.TextBox txtTelefono;
        private System.Windows.Forms.TextBox txtEmail;

        private void InitializeComponent()
        {
            this.raiz = new System.Windows.Forms.TableLayoutPanel();
            this.titulo = new System.Windows.Forms.Label();
            this.campos = new System.Windows.Forms.TableLayoutPanel();
            this.lblCampo0 = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.lblCampo1 = new System.Windows.Forms.Label();
            this.txtSegundoNombre = new System.Windows.Forms.TextBox();
            this.lblCampo2 = new System.Windows.Forms.Label();
            this.txtApellido = new System.Windows.Forms.TextBox();
            this.lblCampo3 = new System.Windows.Forms.Label();
            this.txtSegundoApellido = new System.Windows.Forms.TextBox();
            this.lblCampo4 = new System.Windows.Forms.Label();
            this.txtCedula = new System.Windows.Forms.TextBox();
            this.lblCampo5 = new System.Windows.Forms.Label();
            this.txtTelefono = new System.Windows.Forms.TextBox();
            this.lblCampo6 = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.lblCampo7 = new System.Windows.Forms.Label();
            this.cmbGenero = new System.Windows.Forms.ComboBox();
            this.acciones = new System.Windows.Forms.FlowLayoutPanel();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnVolver = new System.Windows.Forms.Button();
            this.raiz.SuspendLayout();
            this.campos.SuspendLayout();
            this.acciones.SuspendLayout();
            this.SuspendLayout();
            // 
            // raiz
            // 
            this.raiz.ColumnCount = 1;
            this.raiz.Controls.Add(this.titulo, 0, 0);
            this.raiz.Controls.Add(this.campos, 0, 1);
            this.raiz.Controls.Add(this.acciones, 0, 2);
            this.raiz.Dock = System.Windows.Forms.DockStyle.Fill;
            this.raiz.Location = new System.Drawing.Point(0, 0);
            this.raiz.Margin = new System.Windows.Forms.Padding(4);
            this.raiz.Name = "raiz";
            this.raiz.Padding = new System.Windows.Forms.Padding(16, 15, 16, 15);
            this.raiz.RowCount = 3;
            this.raiz.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.raiz.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.raiz.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 59F));
            this.raiz.Size = new System.Drawing.Size(1093, 738);
            this.raiz.TabIndex = 0;
            // 
            // titulo
            // 
            this.titulo.BackColor = System.Drawing.Color.DarkSlateBlue;
            this.titulo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.titulo.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold);
            this.titulo.ForeColor = System.Drawing.Color.White;
            this.titulo.Location = new System.Drawing.Point(20, 15);
            this.titulo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.titulo.Name = "titulo";
            this.titulo.Size = new System.Drawing.Size(1053, 80);
            this.titulo.TabIndex = 0;
            this.titulo.Text = "MIS DATOS";
            this.titulo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // campos
            // 
            this.campos.AutoScroll = true;
            this.campos.ColumnCount = 2;
            this.campos.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.campos.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.campos.Controls.Add(this.lblCampo0, 0, 0);
            this.campos.Controls.Add(this.txtNombre, 1, 0);
            this.campos.Controls.Add(this.lblCampo1, 0, 1);
            this.campos.Controls.Add(this.txtSegundoNombre, 1, 1);
            this.campos.Controls.Add(this.lblCampo2, 0, 2);
            this.campos.Controls.Add(this.txtApellido, 1, 2);
            this.campos.Controls.Add(this.lblCampo3, 0, 3);
            this.campos.Controls.Add(this.txtSegundoApellido, 1, 3);
            this.campos.Controls.Add(this.lblCampo4, 0, 4);
            this.campos.Controls.Add(this.txtCedula, 1, 4);
            this.campos.Controls.Add(this.lblCampo5, 0, 5);
            this.campos.Controls.Add(this.txtTelefono, 1, 5);
            this.campos.Controls.Add(this.lblCampo6, 0, 6);
            this.campos.Controls.Add(this.txtEmail, 1, 6);
            this.campos.Controls.Add(this.lblCampo7, 0, 7);
            this.campos.Controls.Add(this.cmbGenero, 1, 7);
            this.campos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.campos.Location = new System.Drawing.Point(20, 99);
            this.campos.Margin = new System.Windows.Forms.Padding(4);
            this.campos.Name = "campos";
            this.campos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 49F));
            this.campos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 49F));
            this.campos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 49F));
            this.campos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 49F));
            this.campos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 49F));
            this.campos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 49F));
            this.campos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 49F));
            this.campos.Size = new System.Drawing.Size(1053, 561);
            this.campos.TabIndex = 1;
            // 
            // lblCampo0
            // 
            this.lblCampo0.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblCampo0.AutoSize = true;
            this.lblCampo0.Location = new System.Drawing.Point(4, 16);
            this.lblCampo0.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCampo0.Name = "lblCampo0";
            this.lblCampo0.Size = new System.Drawing.Size(95, 16);
            this.lblCampo0.TabIndex = 0;
            this.lblCampo0.Text = "Primer nombre";
            // 
            // txtNombre
            // 
            this.txtNombre.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtNombre.Location = new System.Drawing.Point(204, 4);
            this.txtNombre.Margin = new System.Windows.Forms.Padding(4);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(845, 22);
            this.txtNombre.TabIndex = 1;
            // 
            // lblCampo1
            // 
            this.lblCampo1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblCampo1.AutoSize = true;
            this.lblCampo1.Location = new System.Drawing.Point(4, 65);
            this.lblCampo1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCampo1.Name = "lblCampo1";
            this.lblCampo1.Size = new System.Drawing.Size(111, 16);
            this.lblCampo1.TabIndex = 2;
            this.lblCampo1.Text = "Segundo nombre";
            // 
            // txtSegundoNombre
            // 
            this.txtSegundoNombre.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSegundoNombre.Location = new System.Drawing.Point(204, 53);
            this.txtSegundoNombre.Margin = new System.Windows.Forms.Padding(4);
            this.txtSegundoNombre.Name = "txtSegundoNombre";
            this.txtSegundoNombre.Size = new System.Drawing.Size(845, 22);
            this.txtSegundoNombre.TabIndex = 3;
            // 
            // lblCampo2
            // 
            this.lblCampo2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblCampo2.AutoSize = true;
            this.lblCampo2.Location = new System.Drawing.Point(4, 114);
            this.lblCampo2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCampo2.Name = "lblCampo2";
            this.lblCampo2.Size = new System.Drawing.Size(98, 16);
            this.lblCampo2.TabIndex = 4;
            this.lblCampo2.Text = "Primer apellido";
            // 
            // txtApellido
            // 
            this.txtApellido.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtApellido.Location = new System.Drawing.Point(204, 102);
            this.txtApellido.Margin = new System.Windows.Forms.Padding(4);
            this.txtApellido.Name = "txtApellido";
            this.txtApellido.Size = new System.Drawing.Size(845, 22);
            this.txtApellido.TabIndex = 5;
            // 
            // lblCampo3
            // 
            this.lblCampo3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblCampo3.AutoSize = true;
            this.lblCampo3.Location = new System.Drawing.Point(4, 163);
            this.lblCampo3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCampo3.Name = "lblCampo3";
            this.lblCampo3.Size = new System.Drawing.Size(114, 16);
            this.lblCampo3.TabIndex = 6;
            this.lblCampo3.Text = "Segundo apellido";
            // 
            // txtSegundoApellido
            // 
            this.txtSegundoApellido.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSegundoApellido.Location = new System.Drawing.Point(204, 151);
            this.txtSegundoApellido.Margin = new System.Windows.Forms.Padding(4);
            this.txtSegundoApellido.Name = "txtSegundoApellido";
            this.txtSegundoApellido.Size = new System.Drawing.Size(845, 22);
            this.txtSegundoApellido.TabIndex = 7;
            // 
            // lblCampo4
            // 
            this.lblCampo4.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblCampo4.AutoSize = true;
            this.lblCampo4.Location = new System.Drawing.Point(4, 212);
            this.lblCampo4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCampo4.Name = "lblCampo4";
            this.lblCampo4.Size = new System.Drawing.Size(50, 16);
            this.lblCampo4.TabIndex = 8;
            this.lblCampo4.Text = "Cédula";
            // 
            // txtCedula
            // 
            this.txtCedula.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtCedula.Location = new System.Drawing.Point(204, 200);
            this.txtCedula.Margin = new System.Windows.Forms.Padding(4);
            this.txtCedula.Name = "txtCedula";
            this.txtCedula.Size = new System.Drawing.Size(845, 22);
            this.txtCedula.TabIndex = 9;
            // 
            // lblCampo5
            // 
            this.lblCampo5.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblCampo5.AutoSize = true;
            this.lblCampo5.Location = new System.Drawing.Point(4, 261);
            this.lblCampo5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCampo5.Name = "lblCampo5";
            this.lblCampo5.Size = new System.Drawing.Size(61, 16);
            this.lblCampo5.TabIndex = 10;
            this.lblCampo5.Text = "Teléfono";
            // 
            // txtTelefono
            // 
            this.txtTelefono.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtTelefono.Location = new System.Drawing.Point(204, 249);
            this.txtTelefono.Margin = new System.Windows.Forms.Padding(4);
            this.txtTelefono.Name = "txtTelefono";
            this.txtTelefono.Size = new System.Drawing.Size(845, 22);
            this.txtTelefono.TabIndex = 11;
            // 
            // lblCampo6
            // 
            this.lblCampo6.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblCampo6.AutoSize = true;
            this.lblCampo6.Location = new System.Drawing.Point(4, 310);
            this.lblCampo6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCampo6.Name = "lblCampo6";
            this.lblCampo6.Size = new System.Drawing.Size(48, 16);
            this.lblCampo6.TabIndex = 12;
            this.lblCampo6.Text = "Correo";
            // 
            // txtEmail
            // 
            this.txtEmail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtEmail.Location = new System.Drawing.Point(204, 298);
            this.txtEmail.Margin = new System.Windows.Forms.Padding(4);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(845, 22);
            this.txtEmail.TabIndex = 13;
            // 
            // lblCampo7
            // 
            this.lblCampo7.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblCampo7.AutoSize = true;
            this.lblCampo7.Location = new System.Drawing.Point(4, 444);
            this.lblCampo7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCampo7.Name = "lblCampo7";
            this.lblCampo7.Size = new System.Drawing.Size(52, 16);
            this.lblCampo7.TabIndex = 14;
            this.lblCampo7.Text = "Género";
            // 
            // cmbGenero
            // 
            this.cmbGenero.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGenero.Items.AddRange(new object[] {
            "M",
            "F",
            "O"});
            this.cmbGenero.Location = new System.Drawing.Point(204, 347);
            this.cmbGenero.Margin = new System.Windows.Forms.Padding(4);
            this.cmbGenero.Name = "cmbGenero";
            this.cmbGenero.Size = new System.Drawing.Size(57, 24);
            this.cmbGenero.TabIndex = 15;
            // 
            // acciones
            // 
            this.acciones.AutoScroll = true;
            this.acciones.Controls.Add(this.btnGuardar);
            this.acciones.Controls.Add(this.btnVolver);
            this.acciones.Dock = System.Windows.Forms.DockStyle.Fill;
            this.acciones.Location = new System.Drawing.Point(20, 668);
            this.acciones.Margin = new System.Windows.Forms.Padding(4);
            this.acciones.Name = "acciones";
            this.acciones.Size = new System.Drawing.Size(1053, 51);
            this.acciones.TabIndex = 2;
            // 
            // btnGuardar
            // 
            this.btnGuardar.Location = new System.Drawing.Point(4, 4);
            this.btnGuardar.Margin = new System.Windows.Forms.Padding(4);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(167, 39);
            this.btnGuardar.TabIndex = 0;
            this.btnGuardar.Text = "Guardar datos";
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnVolver
            // 
            this.btnVolver.Location = new System.Drawing.Point(179, 4);
            this.btnVolver.Margin = new System.Windows.Forms.Padding(4);
            this.btnVolver.Name = "btnVolver";
            this.btnVolver.Size = new System.Drawing.Size(167, 39);
            this.btnVolver.TabIndex = 1;
            this.btnVolver.Text = "Volver";
            this.btnVolver.Click += new System.EventHandler(this.btnVolver_Click);
            // 
            // FrmMisDatos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGray;
            this.ClientSize = new System.Drawing.Size(1093, 738);
            this.Controls.Add(this.raiz);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(994, 678);
            this.Name = "FrmMisDatos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "MIS DATOS";
            this.raiz.ResumeLayout(false);
            this.campos.ResumeLayout(false);
            this.campos.PerformLayout();
            this.acciones.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        private System.Windows.Forms.Label lblCampo0;
        private System.Windows.Forms.Label lblCampo1;
        private System.Windows.Forms.Label lblCampo2;
        private System.Windows.Forms.Label lblCampo3;
        private System.Windows.Forms.Label lblCampo4;
        private System.Windows.Forms.Label lblCampo5;
        private System.Windows.Forms.Label lblCampo6;
        private System.Windows.Forms.Label lblCampo7;
        private System.Windows.Forms.TableLayoutPanel raiz;
        private System.Windows.Forms.Label titulo;
    }
}
