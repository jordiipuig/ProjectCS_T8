namespace Interface_form_
{
    partial class FlightPlanForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.FlightBox1       = new System.Windows.Forms.GroupBox();
            this.label7           = new System.Windows.Forms.Label();
            this.velocity1box     = new System.Windows.Forms.TextBox();
            this.label3           = new System.Windows.Forms.Label();
            this.label2           = new System.Windows.Forms.Label();
            this.label1           = new System.Windows.Forms.Label();
            this.destination1box  = new System.Windows.Forms.TextBox();
            this.origin1box       = new System.Windows.Forms.TextBox();
            this.id1box           = new System.Windows.Forms.TextBox();
            this.lblCompany1      = new System.Windows.Forms.Label();
            this.company1box      = new System.Windows.Forms.TextBox();
            this.FlightBox2       = new System.Windows.Forms.GroupBox();
            this.label8           = new System.Windows.Forms.Label();
            this.velocity2box     = new System.Windows.Forms.TextBox();
            this.label6           = new System.Windows.Forms.Label();
            this.label5           = new System.Windows.Forms.Label();
            this.label4           = new System.Windows.Forms.Label();
            this.destination2box  = new System.Windows.Forms.TextBox();
            this.origin2box       = new System.Windows.Forms.TextBox();
            this.id2box           = new System.Windows.Forms.TextBox();
            this.lblCompany2      = new System.Windows.Forms.Label();
            this.company2box      = new System.Windows.Forms.TextBox();
            this.acceptButton     = new System.Windows.Forms.Button();
            this.cancelButton     = new System.Windows.Forms.Button();
            this.nonConflictbtn   = new System.Windows.Forms.Button();
            this.conflictbtn      = new System.Windows.Forms.Button();
            this.Test_Plans       = new System.Windows.Forms.Label();
            this.resetbtn         = new System.Windows.Forms.Button();
            this.FlightBox1.SuspendLayout();
            this.FlightBox2.SuspendLayout();
            this.SuspendLayout();

            // ── FlightBox1 ─────────────────────────────────────────────────────
            this.FlightBox1.Controls.Add(this.label7);
            this.FlightBox1.Controls.Add(this.velocity1box);
            this.FlightBox1.Controls.Add(this.label3);
            this.FlightBox1.Controls.Add(this.label2);
            this.FlightBox1.Controls.Add(this.label1);
            this.FlightBox1.Controls.Add(this.destination1box);
            this.FlightBox1.Controls.Add(this.origin1box);
            this.FlightBox1.Controls.Add(this.id1box);
            this.FlightBox1.Controls.Add(this.lblCompany1);
            this.FlightBox1.Controls.Add(this.company1box);
            this.FlightBox1.Font     = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.FlightBox1.Location = new System.Drawing.Point(100, 39);
            this.FlightBox1.Name     = "FlightBox1";
            this.FlightBox1.Size     = new System.Drawing.Size(595, 230);
            this.FlightBox1.TabIndex = 0;
            this.FlightBox1.TabStop  = false;
            this.FlightBox1.Text     = "FlightPlan1";

            // label1 – ID
            this.label1.AutoSize = true;
            this.label1.Font     = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.label1.Location = new System.Drawing.Point(34, 60);
            this.label1.Name     = "label1"; this.label1.TabIndex = 3; this.label1.Text = "ID";

            // id1box
            this.id1box.Location = new System.Drawing.Point(29, 92);
            this.id1box.Name     = "id1box"; this.id1box.Size = new System.Drawing.Size(100, 34); this.id1box.TabIndex = 0;

            // label2 – Origin (formato X,Y)
            this.label2.AutoSize = true;
            this.label2.Font     = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.label2.Location = new System.Drawing.Point(176, 55);
            this.label2.Name     = "label2"; this.label2.TabIndex = 4; this.label2.Text = "Origen X,Y  (ej: 100,450)";

            // origin1box
            this.origin1box.Location = new System.Drawing.Point(169, 92);
            this.origin1box.Name     = "origin1box"; this.origin1box.Size = new System.Drawing.Size(110, 34); this.origin1box.TabIndex = 1;

            // label3 – Destination (formato X,Y)
            this.label3.AutoSize = true;
            this.label3.Font     = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.label3.Location = new System.Drawing.Point(302, 55);
            this.label3.Name     = "label3"; this.label3.TabIndex = 5; this.label3.Text = "Destino X,Y  (ej: 900,450)";

            // destination1box
            this.destination1box.Location = new System.Drawing.Point(304, 92);
            this.destination1box.Name     = "destination1box"; this.destination1box.Size = new System.Drawing.Size(110, 34); this.destination1box.TabIndex = 2;

            // label7 – Velocity
            this.label7.AutoSize = true;
            this.label7.Font     = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.label7.Location = new System.Drawing.Point(447, 55);
            this.label7.Name     = "label7"; this.label7.TabIndex = 7; this.label7.Text = "Vel (1-1000 u/min)";

            // velocity1box
            this.velocity1box.Location = new System.Drawing.Point(450, 92);
            this.velocity1box.Name     = "velocity1box"; this.velocity1box.Size = new System.Drawing.Size(100, 34); this.velocity1box.TabIndex = 6;

            // lblCompany1
            this.lblCompany1.AutoSize = true;
            this.lblCompany1.Font     = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.lblCompany1.Location = new System.Drawing.Point(34, 140);
            this.lblCompany1.Name     = "lblCompany1"; this.lblCompany1.TabIndex = 8; this.lblCompany1.Text = "Empresa";

            // company1box
            this.company1box.Location = new System.Drawing.Point(29, 160);
            this.company1box.Name     = "company1box"; this.company1box.Size = new System.Drawing.Size(200, 34); this.company1box.TabIndex = 9;

            // ── FlightBox2 ─────────────────────────────────────────────────────
            this.FlightBox2.Controls.Add(this.label8);
            this.FlightBox2.Controls.Add(this.velocity2box);
            this.FlightBox2.Controls.Add(this.label6);
            this.FlightBox2.Controls.Add(this.label5);
            this.FlightBox2.Controls.Add(this.label4);
            this.FlightBox2.Controls.Add(this.destination2box);
            this.FlightBox2.Controls.Add(this.origin2box);
            this.FlightBox2.Controls.Add(this.id2box);
            this.FlightBox2.Controls.Add(this.lblCompany2);
            this.FlightBox2.Controls.Add(this.company2box);
            this.FlightBox2.Font     = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.FlightBox2.Location = new System.Drawing.Point(100, 310);
            this.FlightBox2.Name     = "FlightBox2";
            this.FlightBox2.Size     = new System.Drawing.Size(595, 230);
            this.FlightBox2.TabIndex = 1;
            this.FlightBox2.TabStop  = false;
            this.FlightBox2.Text     = "FlightPlan2";

            // label4 – ID
            this.label4.AutoSize = true;
            this.label4.Font     = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.label4.Location = new System.Drawing.Point(34, 60);
            this.label4.Name     = "label4"; this.label4.TabIndex = 4; this.label4.Text = "ID";

            // id2box
            this.id2box.Location = new System.Drawing.Point(29, 92);
            this.id2box.Name     = "id2box"; this.id2box.Size = new System.Drawing.Size(100, 34); this.id2box.TabIndex = 0;

            // label5 – Origin
            this.label5.AutoSize = true;
            this.label5.Font     = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.label5.Location = new System.Drawing.Point(176, 55);
            this.label5.Name     = "label5"; this.label5.TabIndex = 5; this.label5.Text = "Origen X,Y  (ej: 100,450)";

            // origin2box
            this.origin2box.Location = new System.Drawing.Point(169, 92);
            this.origin2box.Name     = "origin2box"; this.origin2box.Size = new System.Drawing.Size(110, 34); this.origin2box.TabIndex = 1;

            // label6 – Destination
            this.label6.AutoSize = true;
            this.label6.Font     = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.label6.Location = new System.Drawing.Point(300, 55);
            this.label6.Name     = "label6"; this.label6.TabIndex = 6; this.label6.Text = "Destino X,Y  (ej: 900,450)";

            // destination2box
            this.destination2box.Location = new System.Drawing.Point(304, 92);
            this.destination2box.Name     = "destination2box"; this.destination2box.Size = new System.Drawing.Size(110, 34); this.destination2box.TabIndex = 2;

            // label8 – Velocity
            this.label8.AutoSize = true;
            this.label8.Font     = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.label8.Location = new System.Drawing.Point(450, 55);
            this.label8.Name     = "label8"; this.label8.TabIndex = 8; this.label8.Text = "Vel (1-1000 u/min)";

            // velocity2box
            this.velocity2box.Location = new System.Drawing.Point(451, 92);
            this.velocity2box.Name     = "velocity2box"; this.velocity2box.Size = new System.Drawing.Size(100, 34); this.velocity2box.TabIndex = 7;

            // lblCompany2
            this.lblCompany2.AutoSize = true;
            this.lblCompany2.Font     = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.lblCompany2.Location = new System.Drawing.Point(34, 140);
            this.lblCompany2.Name     = "lblCompany2"; this.lblCompany2.TabIndex = 9; this.lblCompany2.Text = "Empresa";

            // company2box
            this.company2box.Location = new System.Drawing.Point(29, 160);
            this.company2box.Name     = "company2box"; this.company2box.Size = new System.Drawing.Size(200, 34); this.company2box.TabIndex = 10;

            // ── acceptButton ───────────────────────────────────────────────────
            this.acceptButton.Location            = new System.Drawing.Point(727, 220);
            this.acceptButton.Name                = "acceptButton";
            this.acceptButton.Size                = new System.Drawing.Size(128, 50);
            this.acceptButton.TabIndex            = 2;
            this.acceptButton.Text                = "Accept";
            this.acceptButton.UseVisualStyleBackColor = true;
            this.acceptButton.Click              += new System.EventHandler(this.acceptButton_Click);

            // ── cancelButton ───────────────────────────────────────────────────
            this.cancelButton.Location            = new System.Drawing.Point(727, 300);
            this.cancelButton.Name                = "cancelButton";
            this.cancelButton.Size                = new System.Drawing.Size(128, 50);
            this.cancelButton.TabIndex            = 3;
            this.cancelButton.Text                = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;

            // ── nonConflictbtn ─────────────────────────────────────────────────
            this.nonConflictbtn.Location          = new System.Drawing.Point(100, 596);
            this.nonConflictbtn.Name              = "nonConflictbtn";
            this.nonConflictbtn.Size              = new System.Drawing.Size(129, 51);
            this.nonConflictbtn.TabIndex          = 4;
            this.nonConflictbtn.Text              = "Sin conflicto";
            this.nonConflictbtn.UseVisualStyleBackColor = true;
            this.nonConflictbtn.Click            += new System.EventHandler(this.nonConflictbtn_Click);

            // ── conflictbtn ────────────────────────────────────────────────────
            this.conflictbtn.Location             = new System.Drawing.Point(280, 596);
            this.conflictbtn.Name                 = "conflictbtn";
            this.conflictbtn.Size                 = new System.Drawing.Size(122, 51);
            this.conflictbtn.TabIndex             = 5;
            this.conflictbtn.Text                 = "Con conflicto";
            this.conflictbtn.UseVisualStyleBackColor = true;
            this.conflictbtn.Click               += new System.EventHandler(this.conflictbtn_Click);

            // ── Test_Plans label ───────────────────────────────────────────────
            this.Test_Plans.AutoSize = true;
            this.Test_Plans.Location = new System.Drawing.Point(108, 560);
            this.Test_Plans.Name     = "Test_Plans"; this.Test_Plans.TabIndex = 6; this.Test_Plans.Text = "Vuelos de prueba:";

            // ── resetbtn ───────────────────────────────────────────────────────
            this.resetbtn.Location            = new System.Drawing.Point(727, 390);
            this.resetbtn.Name                = "resetbtn";
            this.resetbtn.Size                = new System.Drawing.Size(128, 50);
            this.resetbtn.TabIndex            = 7;
            this.resetbtn.Text                = "Reset";
            this.resetbtn.UseVisualStyleBackColor = true;
            this.resetbtn.Click              += new System.EventHandler(this.resetbtn_Click);

            // ── FlightPlanForm ─────────────────────────────────────────────────
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode       = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize          = new System.Drawing.Size(1103, 720);
            this.Controls.Add(this.resetbtn);
            this.Controls.Add(this.Test_Plans);
            this.Controls.Add(this.conflictbtn);
            this.Controls.Add(this.nonConflictbtn);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.acceptButton);
            this.Controls.Add(this.FlightBox2);
            this.Controls.Add(this.FlightBox1);
            this.Name = "FlightPlanForm";
            this.Text = "FlightplanForm - v2";
            this.FlightBox1.ResumeLayout(false);
            this.FlightBox1.PerformLayout();
            this.FlightBox2.ResumeLayout(false);
            this.FlightBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.GroupBox  FlightBox1;
        private System.Windows.Forms.TextBox   id1box;
        private System.Windows.Forms.TextBox   destination1box;
        private System.Windows.Forms.TextBox   origin1box;
        private System.Windows.Forms.Label     lblCompany1;
        private System.Windows.Forms.TextBox   company1box;
        private System.Windows.Forms.GroupBox  FlightBox2;
        private System.Windows.Forms.TextBox   destination2box;
        private System.Windows.Forms.TextBox   origin2box;
        private System.Windows.Forms.TextBox   id2box;
        private System.Windows.Forms.Label     lblCompany2;
        private System.Windows.Forms.TextBox   company2box;
        private System.Windows.Forms.Button    acceptButton;
        private System.Windows.Forms.Button    cancelButton;
        private System.Windows.Forms.Label     label3;
        private System.Windows.Forms.Label     label2;
        private System.Windows.Forms.Label     label1;
        private System.Windows.Forms.Label     label6;
        private System.Windows.Forms.Label     label5;
        private System.Windows.Forms.Label     label4;
        private System.Windows.Forms.Label     label7;
        private System.Windows.Forms.TextBox   velocity1box;
        private System.Windows.Forms.Label     label8;
        private System.Windows.Forms.TextBox   velocity2box;
        private System.Windows.Forms.Button    nonConflictbtn;
        private System.Windows.Forms.Button    conflictbtn;
        private System.Windows.Forms.Label     Test_Plans;
        private System.Windows.Forms.Button    resetbtn;
    }
}
