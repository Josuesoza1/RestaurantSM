namespace SistemaPedidoRestaurante.Loggin
{
    partial class FrmLogin
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
            this.pnlEncabezado = new System.Windows.Forms.Panel();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblSubtitulo = new System.Windows.Forms.Label();
            this.grpAcciones = new System.Windows.Forms.GroupBox();
            this.btnIniciarSesion = new System.Windows.Forms.Button();
            this.txtUsuario = new System.Windows.Forms.TextBox();
            this.txtContrasena = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lblSnombre = new System.Windows.Forms.Label();
            this.btnSalir = new System.Windows.Forms.Button();
            this.lblPregunta = new System.Windows.Forms.Label();
            this.btnCrearCuenta = new System.Windows.Forms.Button();
            this.pnlEncabezado.SuspendLayout();
            this.grpAcciones.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlEncabezado
            // 
            this.pnlEncabezado.AutoSize = true;
            this.pnlEncabezado.BackColor = System.Drawing.Color.MidnightBlue;
            this.pnlEncabezado.Controls.Add(this.lblTitulo);
            this.pnlEncabezado.Location = new System.Drawing.Point(0, 0);
            this.pnlEncabezado.Name = "pnlEncabezado";
            this.pnlEncabezado.Size = new System.Drawing.Size(533, 70);
            this.pnlEncabezado.TabIndex = 2;
            // 
            // lblTitulo
            // 
            this.lblTitulo.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Location = new System.Drawing.Point(104, 23);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(305, 29);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "SISTEMA RESTAURANTE";
            this.lblTitulo.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblSubtitulo
            // 
            this.lblSubtitulo.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblSubtitulo.AutoSize = true;
            this.lblSubtitulo.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubtitulo.ForeColor = System.Drawing.Color.White;
            this.lblSubtitulo.Location = new System.Drawing.Point(189, 73);
            this.lblSubtitulo.Name = "lblSubtitulo";
            this.lblSubtitulo.Size = new System.Drawing.Size(133, 19);
            this.lblSubtitulo.TabIndex = 1;
            this.lblSubtitulo.Text = "INICIAR SESIÓN";
            // 
            // grpAcciones
            // 
            this.grpAcciones.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.grpAcciones.Controls.Add(this.btnIniciarSesion);
            this.grpAcciones.Controls.Add(this.txtUsuario);
            this.grpAcciones.Controls.Add(this.txtContrasena);
            this.grpAcciones.Controls.Add(this.label3);
            this.grpAcciones.Controls.Add(this.lblSnombre);
            this.grpAcciones.Controls.Add(this.btnSalir);
            this.grpAcciones.ForeColor = System.Drawing.Color.Green;
            this.grpAcciones.Location = new System.Drawing.Point(122, 95);
            this.grpAcciones.Name = "grpAcciones";
            this.grpAcciones.Size = new System.Drawing.Size(267, 251);
            this.grpAcciones.TabIndex = 7;
            this.grpAcciones.TabStop = false;
            // 
            // btnIniciarSesion
            // 
            this.btnIniciarSesion.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnIniciarSesion.BackColor = System.Drawing.Color.Aquamarine;
            this.btnIniciarSesion.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnIniciarSesion.ForeColor = System.Drawing.Color.Black;
            this.btnIniciarSesion.Location = new System.Drawing.Point(54, 140);
            this.btnIniciarSesion.Name = "btnIniciarSesion";
            this.btnIniciarSesion.Size = new System.Drawing.Size(158, 36);
            this.btnIniciarSesion.TabIndex = 19;
            this.btnIniciarSesion.Text = "INICIAR SESIÓN";
            this.btnIniciarSesion.UseVisualStyleBackColor = false;
            this.btnIniciarSesion.Click += new System.EventHandler(this.btnIniciarSesion_Click);
            // 
            // txtUsuario
            // 
            this.txtUsuario.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtUsuario.Location = new System.Drawing.Point(56, 54);
            this.txtUsuario.Name = "txtUsuario";
            this.txtUsuario.Size = new System.Drawing.Size(157, 20);
            this.txtUsuario.TabIndex = 18;
            // 
            // txtContrasena
            // 
            this.txtContrasena.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtContrasena.Location = new System.Drawing.Point(56, 102);
            this.txtContrasena.Name = "txtContrasena";
            this.txtContrasena.Size = new System.Drawing.Size(157, 20);
            this.txtContrasena.TabIndex = 17;
            this.txtContrasena.UseSystemPasswordChar = true;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label3.Location = new System.Drawing.Point(98, 81);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 14);
            this.label3.TabIndex = 15;
            this.label3.Text = "Contraseña:";
            // 
            // lblSnombre
            // 
            this.lblSnombre.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblSnombre.AutoSize = true;
            this.lblSnombre.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblSnombre.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lblSnombre.Location = new System.Drawing.Point(110, 30);
            this.lblSnombre.Name = "lblSnombre";
            this.lblSnombre.Size = new System.Drawing.Size(52, 14);
            this.lblSnombre.TabIndex = 14;
            this.lblSnombre.Text = "Usuario:";
            // 
            // btnSalir
            // 
            this.btnSalir.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnSalir.BackColor = System.Drawing.Color.Aquamarine;
            this.btnSalir.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSalir.ForeColor = System.Drawing.Color.Black;
            this.btnSalir.Location = new System.Drawing.Point(54, 185);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(158, 36);
            this.btnSalir.TabIndex = 4;
            this.btnSalir.Text = "SALIR";
            this.btnSalir.UseVisualStyleBackColor = false;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // lblPregunta
            // 
            this.lblPregunta.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblPregunta.AutoSize = true;
            this.lblPregunta.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.lblPregunta.Location = new System.Drawing.Point(189, 361);
            this.lblPregunta.Name = "lblPregunta";
            this.lblPregunta.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblPregunta.Size = new System.Drawing.Size(121, 13);
            this.lblPregunta.TabIndex = 14;
            this.lblPregunta.Text = "¿No tienes una cuenta?";
            this.lblPregunta.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btnCrearCuenta
            // 
            this.btnCrearCuenta.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnCrearCuenta.BackColor = System.Drawing.Color.Aquamarine;
            this.btnCrearCuenta.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCrearCuenta.ForeColor = System.Drawing.Color.Black;
            this.btnCrearCuenta.Location = new System.Drawing.Point(176, 386);
            this.btnCrearCuenta.Name = "btnCrearCuenta";
            this.btnCrearCuenta.Size = new System.Drawing.Size(158, 36);
            this.btnCrearCuenta.TabIndex = 23;
            this.btnCrearCuenta.Text = "CREAR CUENTA";
            this.btnCrearCuenta.UseVisualStyleBackColor = false;
            this.btnCrearCuenta.Click += new System.EventHandler(this.btnCrearCuenta_Click);
            // 
            // FrmLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkSlateBlue;
            this.ClientSize = new System.Drawing.Size(533, 445);
            this.Controls.Add(this.btnCrearCuenta);
            this.Controls.Add(this.lblPregunta);
            this.Controls.Add(this.grpAcciones);
            this.Controls.Add(this.lblSubtitulo);
            this.Controls.Add(this.pnlEncabezado);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "FrmLogin";
            this.Text = "Login - Iniciar Sesión";
            this.AutoSizeChanged += new System.EventHandler(this.FrmLogin_AutoSizeChanged);
            this.MaximumSizeChanged += new System.EventHandler(this.FrmLogin_MaximumSizeChanged);
            this.Resize += new System.EventHandler(this.FrmLogin_Resize_1);
            this.pnlEncabezado.ResumeLayout(false);
            this.pnlEncabezado.PerformLayout();
            this.grpAcciones.ResumeLayout(false);
            this.grpAcciones.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlEncabezado;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblSubtitulo;
        private System.Windows.Forms.GroupBox grpAcciones;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.Button btnIniciarSesion;
        private System.Windows.Forms.TextBox txtUsuario;
        private System.Windows.Forms.TextBox txtContrasena;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblSnombre;
        private System.Windows.Forms.Label lblPregunta;
        private System.Windows.Forms.Button btnCrearCuenta;
    }
}