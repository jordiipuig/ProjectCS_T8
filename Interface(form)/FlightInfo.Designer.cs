namespace Interface_form_
{
    partial class FlightInfo
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.Idbox      = new System.Windows.Forms.TextBox();
            this.label1     = new System.Windows.Forms.Label();
            this.lblCompany = new System.Windows.Forms.Label();
            this.companybox = new System.Windows.Forms.TextBox();
            this.label2     = new System.Windows.Forms.Label();
            this.xbox       = new System.Windows.Forms.TextBox();
            this.ybox       = new System.Windows.Forms.TextBox();
            this.speedlbl   = new System.Windows.Forms.Label();
            this.speedbox   = new System.Windows.Forms.TextBox();
            this.closebtn   = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // ── Idbox ──────────────────────────────────────────────────────────
            this.Idbox.Location = new System.Drawing.Point(43, 53);
            this.Idbox.Name     = "Idbox";
            this.Idbox.Size     = new System.Drawing.Size(200, 29);
            this.Idbox.TabIndex = 0;
            this.Idbox.ReadOnly = true;

            // label1 – ID
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(43, 25);
            this.label1.Name     = "label1";
            this.label1.TabIndex = 1;
            this.label1.Text     = "ID";

            // lblCompany – Empresa (nuevo v2)
            this.lblCompany.AutoSize = true;
            this.lblCompany.Location = new System.Drawing.Point(43, 95);
            this.lblCompany.Name     = "lblCompany";
            this.lblCompany.TabIndex = 2;
            this.lblCompany.Text     = "Empresa";

            // companybox (nuevo v2)
            this.companybox.Location = new System.Drawing.Point(43, 120);
            this.companybox.Name     = "companybox";
            this.companybox.Size     = new System.Drawing.Size(200, 29);
            this.companybox.TabIndex = 3;
            this.companybox.ReadOnly = true;

            // label2 – Position
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(43, 165);
            this.label2.Name     = "label2";
            this.label2.TabIndex = 4;
            this.label2.Text     = "Posición";

            // xbox
            this.xbox.Location = new System.Drawing.Point(43, 190);
            this.xbox.Name     = "xbox";
            this.xbox.Size     = new System.Drawing.Size(90, 29);
            this.xbox.TabIndex = 5;
            this.xbox.ReadOnly = true;

            // ybox
            this.ybox.Location = new System.Drawing.Point(154, 190);
            this.ybox.Name     = "ybox";
            this.ybox.Size     = new System.Drawing.Size(90, 29);
            this.ybox.TabIndex = 6;
            this.ybox.ReadOnly = true;

            // speedlbl
            this.speedlbl.AutoSize = true;
            this.speedlbl.Location = new System.Drawing.Point(43, 235);
            this.speedlbl.Name     = "speedlbl";
            this.speedlbl.TabIndex = 7;
            this.speedlbl.Text     = "Velocidad";

            // speedbox
            this.speedbox.Location = new System.Drawing.Point(43, 260);
            this.speedbox.Name     = "speedbox";
            this.speedbox.Size     = new System.Drawing.Size(90, 29);
            this.speedbox.TabIndex = 8;
            this.speedbox.ReadOnly = true;

            // closebtn
            this.closebtn.Location            = new System.Drawing.Point(154, 260);
            this.closebtn.Name                = "closebtn";
            this.closebtn.Size                = new System.Drawing.Size(104, 39);
            this.closebtn.TabIndex            = 9;
            this.closebtn.Text                = "Cerrar";
            this.closebtn.UseVisualStyleBackColor = true;
            this.closebtn.Click              += new System.EventHandler(this.closebtn_Click);

            // ── FlightInfo ─────────────────────────────────────────────────────
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode       = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize          = new System.Drawing.Size(320, 380);
            this.Controls.Add(this.closebtn);
            this.Controls.Add(this.speedbox);
            this.Controls.Add(this.speedlbl);
            this.Controls.Add(this.ybox);
            this.Controls.Add(this.xbox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.companybox);
            this.Controls.Add(this.lblCompany);
            this.Controls.Add(this.Idbox);
            this.Controls.Add(this.label1);
            this.Name = "FlightInfo";
            this.Text = "Información del vuelo";
            this.Load += new System.EventHandler(this.FlightInfo_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TextBox  Idbox;
        private System.Windows.Forms.Label    label1;
        private System.Windows.Forms.Label    lblCompany;
        private System.Windows.Forms.TextBox  companybox;
        private System.Windows.Forms.Label    label2;
        private System.Windows.Forms.TextBox  xbox;
        private System.Windows.Forms.TextBox  ybox;
        private System.Windows.Forms.Label    speedlbl;
        private System.Windows.Forms.TextBox  speedbox;
        private System.Windows.Forms.Button   closebtn;
    }
}
