namespace Interface_form_
{
    partial class LoginForm
    {
        /// <summary>
        /// Variable requerida por el diseñador.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Libera los recursos usados.
        /// </summary>
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
        /// Método requerido por el diseñador. No modificar con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblHeader       = new System.Windows.Forms.Label();
            this.grpLogin        = new System.Windows.Forms.GroupBox();
            this.lblLoginUser    = new System.Windows.Forms.Label();
            this.txtLoginUser    = new System.Windows.Forms.TextBox();
            this.lblLoginPass    = new System.Windows.Forms.Label();
            this.txtLoginPass    = new System.Windows.Forms.TextBox();
            this.btnLogin        = new System.Windows.Forms.Button();
            this.grpRegister     = new System.Windows.Forms.GroupBox();
            this.lblRegUser      = new System.Windows.Forms.Label();
            this.txtRegUser      = new System.Windows.Forms.TextBox();
            this.lblRegPass      = new System.Windows.Forms.Label();
            this.txtRegPass      = new System.Windows.Forms.TextBox();
            this.btnRegister     = new System.Windows.Forms.Button();
            this.btnExit         = new System.Windows.Forms.Button();
            this.grpLogin.SuspendLayout();
            this.grpRegister.SuspendLayout();
            this.SuspendLayout();

            // ── lblHeader ──────────────────────────────────────────────────────
            this.lblHeader.Dock      = System.Windows.Forms.DockStyle.None;
            this.lblHeader.Location  = new System.Drawing.Point(0, 15);
            this.lblHeader.Name      = "lblHeader";
            this.lblHeader.Size      = new System.Drawing.Size(460, 35);
            this.lblHeader.TabIndex  = 0;
            this.lblHeader.Text      = "ATC Deconflicting Tool v2";
            this.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblHeader.Font      = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold);

            // ── grpLogin ───────────────────────────────────────────────────────
            this.grpLogin.Controls.Add(this.lblLoginUser);
            this.grpLogin.Controls.Add(this.txtLoginUser);
            this.grpLogin.Controls.Add(this.lblLoginPass);
            this.grpLogin.Controls.Add(this.txtLoginPass);
            this.grpLogin.Controls.Add(this.btnLogin);
            this.grpLogin.Location  = new System.Drawing.Point(20, 60);
            this.grpLogin.Name      = "grpLogin";
            this.grpLogin.Size      = new System.Drawing.Size(420, 190);
            this.grpLogin.TabIndex  = 1;
            this.grpLogin.TabStop   = false;
            this.grpLogin.Text      = "Iniciar sesión";

            // lblLoginUser
            this.lblLoginUser.AutoSize = true;
            this.lblLoginUser.Location = new System.Drawing.Point(20, 30);
            this.lblLoginUser.Name     = "lblLoginUser";
            this.lblLoginUser.TabIndex = 0;
            this.lblLoginUser.Text     = "Usuario:";

            // txtLoginUser
            this.txtLoginUser.Location = new System.Drawing.Point(20, 55);
            this.txtLoginUser.Name     = "txtLoginUser";
            this.txtLoginUser.Size     = new System.Drawing.Size(360, 29);
            this.txtLoginUser.TabIndex = 1;

            // lblLoginPass
            this.lblLoginPass.AutoSize = true;
            this.lblLoginPass.Location = new System.Drawing.Point(20, 95);
            this.lblLoginPass.Name     = "lblLoginPass";
            this.lblLoginPass.TabIndex = 2;
            this.lblLoginPass.Text     = "Contraseña:";

            // txtLoginPass
            this.txtLoginPass.Location     = new System.Drawing.Point(20, 120);
            this.txtLoginPass.Name         = "txtLoginPass";
            this.txtLoginPass.PasswordChar = '*';
            this.txtLoginPass.Size         = new System.Drawing.Size(360, 29);
            this.txtLoginPass.TabIndex     = 3;

            // btnLogin
            this.btnLogin.Location            = new System.Drawing.Point(20, 155);
            this.btnLogin.Name                = "btnLogin";
            this.btnLogin.Size                = new System.Drawing.Size(360, 38);
            this.btnLogin.TabIndex            = 4;
            this.btnLogin.Text                = "Entrar";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click              += new System.EventHandler(this.btnLogin_Click);

            // ── grpRegister ────────────────────────────────────────────────────
            this.grpRegister.Controls.Add(this.lblRegUser);
            this.grpRegister.Controls.Add(this.txtRegUser);
            this.grpRegister.Controls.Add(this.lblRegPass);
            this.grpRegister.Controls.Add(this.txtRegPass);
            this.grpRegister.Controls.Add(this.btnRegister);
            this.grpRegister.Location  = new System.Drawing.Point(20, 270);
            this.grpRegister.Name      = "grpRegister";
            this.grpRegister.Size      = new System.Drawing.Size(420, 190);
            this.grpRegister.TabIndex  = 2;
            this.grpRegister.TabStop   = false;
            this.grpRegister.Text      = "Nuevo usuario";

            // lblRegUser
            this.lblRegUser.AutoSize = true;
            this.lblRegUser.Location = new System.Drawing.Point(20, 30);
            this.lblRegUser.Name     = "lblRegUser";
            this.lblRegUser.TabIndex = 0;
            this.lblRegUser.Text     = "Usuario:";

            // txtRegUser
            this.txtRegUser.Location = new System.Drawing.Point(20, 55);
            this.txtRegUser.Name     = "txtRegUser";
            this.txtRegUser.Size     = new System.Drawing.Size(360, 29);
            this.txtRegUser.TabIndex = 1;

            // lblRegPass
            this.lblRegPass.AutoSize = true;
            this.lblRegPass.Location = new System.Drawing.Point(20, 95);
            this.lblRegPass.Name     = "lblRegPass";
            this.lblRegPass.TabIndex = 2;
            this.lblRegPass.Text     = "Contraseña:";

            // txtRegPass
            this.txtRegPass.Location     = new System.Drawing.Point(20, 120);
            this.txtRegPass.Name         = "txtRegPass";
            this.txtRegPass.PasswordChar = '*';
            this.txtRegPass.Size         = new System.Drawing.Size(360, 29);
            this.txtRegPass.TabIndex     = 3;

            // btnRegister
            this.btnRegister.Location            = new System.Drawing.Point(20, 155);
            this.btnRegister.Name                = "btnRegister";
            this.btnRegister.Size                = new System.Drawing.Size(360, 38);
            this.btnRegister.TabIndex            = 4;
            this.btnRegister.Text                = "Registrarse";
            this.btnRegister.UseVisualStyleBackColor = true;
            this.btnRegister.Click              += new System.EventHandler(this.btnRegister_Click);

            // ── btnExit ────────────────────────────────────────────────────────
            this.btnExit.Location            = new System.Drawing.Point(130, 480);
            this.btnExit.Name                = "btnExit";
            this.btnExit.Size                = new System.Drawing.Size(200, 38);
            this.btnExit.TabIndex            = 3;
            this.btnExit.Text                = "Salir";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click              += new System.EventHandler(this.btnExit_Click);

            // ── LoginForm ──────────────────────────────────────────────────────
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode       = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize          = new System.Drawing.Size(460, 540);
            this.Controls.Add(this.lblHeader);
            this.Controls.Add(this.grpLogin);
            this.Controls.Add(this.grpRegister);
            this.Controls.Add(this.btnExit);
            this.FormBorderStyle     = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox         = false;
            this.MinimizeBox         = false;
            this.Name                = "LoginForm";
            this.StartPosition       = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text                = "ATC Deconflicting Tool v2 - Acceso";
            this.Load               += new System.EventHandler(this.LoginForm_Load);
            this.grpLogin.ResumeLayout(false);
            this.grpLogin.PerformLayout();
            this.grpRegister.ResumeLayout(false);
            this.grpRegister.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label     lblHeader;
        private System.Windows.Forms.GroupBox  grpLogin;
        private System.Windows.Forms.Label     lblLoginUser;
        private System.Windows.Forms.TextBox   txtLoginUser;
        private System.Windows.Forms.Label     lblLoginPass;
        private System.Windows.Forms.TextBox   txtLoginPass;
        private System.Windows.Forms.Button    btnLogin;
        private System.Windows.Forms.GroupBox  grpRegister;
        private System.Windows.Forms.Label     lblRegUser;
        private System.Windows.Forms.TextBox   txtRegUser;
        private System.Windows.Forms.Label     lblRegPass;
        private System.Windows.Forms.TextBox   txtRegPass;
        private System.Windows.Forms.Button    btnRegister;
        private System.Windows.Forms.Button    btnExit;
    }
}
