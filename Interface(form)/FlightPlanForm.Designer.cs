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

        private void InitializeComponent()
        {
            this.FlightBox1      = new System.Windows.Forms.GroupBox();
            this.label1          = new System.Windows.Forms.Label();
            this.id1box          = new System.Windows.Forms.TextBox();
            this.lblCompany1     = new System.Windows.Forms.Label();
            this.company1box     = new System.Windows.Forms.TextBox();
            this.label7          = new System.Windows.Forms.Label();
            this.velocity1box    = new System.Windows.Forms.TextBox();
            this.labelOx1        = new System.Windows.Forms.Label();
            this.origin1Xbox     = new System.Windows.Forms.TextBox();
            this.labelOy1        = new System.Windows.Forms.Label();
            this.origin1Ybox     = new System.Windows.Forms.TextBox();
            this.labelDx1        = new System.Windows.Forms.Label();
            this.dest1Xbox       = new System.Windows.Forms.TextBox();
            this.labelDy1        = new System.Windows.Forms.Label();
            this.dest1Ybox       = new System.Windows.Forms.TextBox();

            this.FlightBox2      = new System.Windows.Forms.GroupBox();
            this.label4          = new System.Windows.Forms.Label();
            this.id2box          = new System.Windows.Forms.TextBox();
            this.lblCompany2     = new System.Windows.Forms.Label();
            this.company2box     = new System.Windows.Forms.TextBox();
            this.label8          = new System.Windows.Forms.Label();
            this.velocity2box    = new System.Windows.Forms.TextBox();
            this.labelOx2        = new System.Windows.Forms.Label();
            this.origin2Xbox     = new System.Windows.Forms.TextBox();
            this.labelOy2        = new System.Windows.Forms.Label();
            this.origin2Ybox     = new System.Windows.Forms.TextBox();
            this.labelDx2        = new System.Windows.Forms.Label();
            this.dest2Xbox       = new System.Windows.Forms.TextBox();
            this.labelDy2        = new System.Windows.Forms.Label();
            this.dest2Ybox       = new System.Windows.Forms.TextBox();

            this.acceptButton    = new System.Windows.Forms.Button();
            this.cancelButton    = new System.Windows.Forms.Button();
            this.resetbtn        = new System.Windows.Forms.Button();
            this.nonConflictbtn  = new System.Windows.Forms.Button();
            this.conflictbtn     = new System.Windows.Forms.Button();
            this.Test_Plans      = new System.Windows.Forms.Label();

            this.FlightBox1.SuspendLayout();
            this.FlightBox2.SuspendLayout();
            this.SuspendLayout();

            // ── FlightBox1 ─────────────────────────────────────────────────────
            this.FlightBox1.Controls.Add(this.label1);
            this.FlightBox1.Controls.Add(this.id1box);
            this.FlightBox1.Controls.Add(this.lblCompany1);
            this.FlightBox1.Controls.Add(this.company1box);
            this.FlightBox1.Controls.Add(this.label7);
            this.FlightBox1.Controls.Add(this.velocity1box);
            this.FlightBox1.Controls.Add(this.labelOx1);
            this.FlightBox1.Controls.Add(this.origin1Xbox);
            this.FlightBox1.Controls.Add(this.labelOy1);
            this.FlightBox1.Controls.Add(this.origin1Ybox);
            this.FlightBox1.Controls.Add(this.labelDx1);
            this.FlightBox1.Controls.Add(this.dest1Xbox);
            this.FlightBox1.Controls.Add(this.labelDy1);
            this.FlightBox1.Controls.Add(this.dest1Ybox);
            this.FlightBox1.Location = new System.Drawing.Point(20, 20);
            this.FlightBox1.Name     = "FlightBox1";
            this.FlightBox1.Size     = new System.Drawing.Size(700, 185);
            this.FlightBox1.TabIndex = 0;
            this.FlightBox1.TabStop  = false;
            this.FlightBox1.Text     = "FlightPlan 1";

            // Fila 1: ID | Empresa | Velocidad
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 28); this.label1.Text = "ID";
            this.id1box.Location = new System.Drawing.Point(10, 48); this.id1box.Size = new System.Drawing.Size(110, 28); this.id1box.TabIndex = 0;

            this.lblCompany1.AutoSize = true;
            this.lblCompany1.Location = new System.Drawing.Point(135, 28); this.lblCompany1.Text = "Empresa";
            this.company1box.Location = new System.Drawing.Point(135, 48); this.company1box.Size = new System.Drawing.Size(180, 28); this.company1box.TabIndex = 1;

            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(530, 28); this.label7.Text = "Velocidad (u/min)";
            this.velocity1box.Location = new System.Drawing.Point(530, 48); this.velocity1box.Size = new System.Drawing.Size(130, 28); this.velocity1box.TabIndex = 2;

            // Fila 2: Origen X | Origen Y | Destino X | Destino Y
            this.labelOx1.AutoSize = true;
            this.labelOx1.Location = new System.Drawing.Point(10, 100); this.labelOx1.Text = "Origen X";
            this.origin1Xbox.Location = new System.Drawing.Point(10, 120); this.origin1Xbox.Size = new System.Drawing.Size(110, 28); this.origin1Xbox.TabIndex = 3;

            this.labelOy1.AutoSize = true;
            this.labelOy1.Location = new System.Drawing.Point(135, 100); this.labelOy1.Text = "Origen Y";
            this.origin1Ybox.Location = new System.Drawing.Point(135, 120); this.origin1Ybox.Size = new System.Drawing.Size(110, 28); this.origin1Ybox.TabIndex = 4;

            this.labelDx1.AutoSize = true;
            this.labelDx1.Location = new System.Drawing.Point(270, 100); this.labelDx1.Text = "Destino X";
            this.dest1Xbox.Location = new System.Drawing.Point(270, 120); this.dest1Xbox.Size = new System.Drawing.Size(110, 28); this.dest1Xbox.TabIndex = 5;

            this.labelDy1.AutoSize = true;
            this.labelDy1.Location = new System.Drawing.Point(400, 100); this.labelDy1.Text = "Destino Y";
            this.dest1Ybox.Location = new System.Drawing.Point(400, 120); this.dest1Ybox.Size = new System.Drawing.Size(110, 28); this.dest1Ybox.TabIndex = 6;

            // ── FlightBox2 ─────────────────────────────────────────────────────
            this.FlightBox2.Controls.Add(this.label4);
            this.FlightBox2.Controls.Add(this.id2box);
            this.FlightBox2.Controls.Add(this.lblCompany2);
            this.FlightBox2.Controls.Add(this.company2box);
            this.FlightBox2.Controls.Add(this.label8);
            this.FlightBox2.Controls.Add(this.velocity2box);
            this.FlightBox2.Controls.Add(this.labelOx2);
            this.FlightBox2.Controls.Add(this.origin2Xbox);
            this.FlightBox2.Controls.Add(this.labelOy2);
            this.FlightBox2.Controls.Add(this.origin2Ybox);
            this.FlightBox2.Controls.Add(this.labelDx2);
            this.FlightBox2.Controls.Add(this.dest2Xbox);
            this.FlightBox2.Controls.Add(this.labelDy2);
            this.FlightBox2.Controls.Add(this.dest2Ybox);
            this.FlightBox2.Location = new System.Drawing.Point(20, 225);
            this.FlightBox2.Name     = "FlightBox2";
            this.FlightBox2.Size     = new System.Drawing.Size(700, 185);
            this.FlightBox2.TabIndex = 1;
            this.FlightBox2.TabStop  = false;
            this.FlightBox2.Text     = "FlightPlan 2";

            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 28); this.label4.Text = "ID";
            this.id2box.Location = new System.Drawing.Point(10, 48); this.id2box.Size = new System.Drawing.Size(110, 28); this.id2box.TabIndex = 7;

            this.lblCompany2.AutoSize = true;
            this.lblCompany2.Location = new System.Drawing.Point(135, 28); this.lblCompany2.Text = "Empresa";
            this.company2box.Location = new System.Drawing.Point(135, 48); this.company2box.Size = new System.Drawing.Size(180, 28); this.company2box.TabIndex = 8;

            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(530, 28); this.label8.Text = "Velocidad (u/min)";
            this.velocity2box.Location = new System.Drawing.Point(530, 48); this.velocity2box.Size = new System.Drawing.Size(130, 28); this.velocity2box.TabIndex = 9;

            this.labelOx2.AutoSize = true;
            this.labelOx2.Location = new System.Drawing.Point(10, 100); this.labelOx2.Text = "Origen X";
            this.origin2Xbox.Location = new System.Drawing.Point(10, 120); this.origin2Xbox.Size = new System.Drawing.Size(110, 28); this.origin2Xbox.TabIndex = 10;

            this.labelOy2.AutoSize = true;
            this.labelOy2.Location = new System.Drawing.Point(135, 100); this.labelOy2.Text = "Origen Y";
            this.origin2Ybox.Location = new System.Drawing.Point(135, 120); this.origin2Ybox.Size = new System.Drawing.Size(110, 28); this.origin2Ybox.TabIndex = 11;

            this.labelDx2.AutoSize = true;
            this.labelDx2.Location = new System.Drawing.Point(270, 100); this.labelDx2.Text = "Destino X";
            this.dest2Xbox.Location = new System.Drawing.Point(270, 120); this.dest2Xbox.Size = new System.Drawing.Size(110, 28); this.dest2Xbox.TabIndex = 12;

            this.labelDy2.AutoSize = true;
            this.labelDy2.Location = new System.Drawing.Point(400, 100); this.labelDy2.Text = "Destino Y";
            this.dest2Ybox.Location = new System.Drawing.Point(400, 120); this.dest2Ybox.Size = new System.Drawing.Size(110, 28); this.dest2Ybox.TabIndex = 13;

            // ── Botones principales ────────────────────────────────────────────
            this.acceptButton.Location = new System.Drawing.Point(760, 100);
            this.acceptButton.Name     = "acceptButton";
            this.acceptButton.Size     = new System.Drawing.Size(130, 45);
            this.acceptButton.TabIndex = 14;
            this.acceptButton.Text     = "Accept";
            this.acceptButton.UseVisualStyleBackColor = true;
            this.acceptButton.Click   += new System.EventHandler(this.acceptButton_Click);

            this.cancelButton.Location = new System.Drawing.Point(760, 160);
            this.cancelButton.Name     = "cancelButton";
            this.cancelButton.Size     = new System.Drawing.Size(130, 45);
            this.cancelButton.TabIndex = 15;
            this.cancelButton.Text     = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;

            this.resetbtn.Location = new System.Drawing.Point(760, 220);
            this.resetbtn.Name     = "resetbtn";
            this.resetbtn.Size     = new System.Drawing.Size(130, 45);
            this.resetbtn.TabIndex = 16;
            this.resetbtn.Text     = "Reset";
            this.resetbtn.UseVisualStyleBackColor = true;
            this.resetbtn.Click   += new System.EventHandler(this.resetbtn_Click);

            // ── Test plans ─────────────────────────────────────────────────────
            this.Test_Plans.AutoSize = true;
            this.Test_Plans.Location = new System.Drawing.Point(20, 428);
            this.Test_Plans.Text     = "Vuelos de prueba:";

            this.nonConflictbtn.Location = new System.Drawing.Point(20, 450);
            this.nonConflictbtn.Name     = "nonConflictbtn";
            this.nonConflictbtn.Size     = new System.Drawing.Size(150, 45);
            this.nonConflictbtn.TabIndex = 17;
            this.nonConflictbtn.Text     = "Sin conflicto";
            this.nonConflictbtn.UseVisualStyleBackColor = true;
            this.nonConflictbtn.Click   += new System.EventHandler(this.nonConflictbtn_Click);

            this.conflictbtn.Location = new System.Drawing.Point(190, 450);
            this.conflictbtn.Name     = "conflictbtn";
            this.conflictbtn.Size     = new System.Drawing.Size(150, 45);
            this.conflictbtn.TabIndex = 18;
            this.conflictbtn.Text     = "Con conflicto";
            this.conflictbtn.UseVisualStyleBackColor = true;
            this.conflictbtn.Click   += new System.EventHandler(this.conflictbtn_Click);

            // ── FlightPlanForm ─────────────────────────────────────────────────
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode       = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize          = new System.Drawing.Size(940, 520);
            this.Controls.Add(this.FlightBox1);
            this.Controls.Add(this.FlightBox2);
            this.Controls.Add(this.acceptButton);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.resetbtn);
            this.Controls.Add(this.Test_Plans);
            this.Controls.Add(this.nonConflictbtn);
            this.Controls.Add(this.conflictbtn);
            this.Name = "FlightPlanForm";
            this.Text = "Introducir planes de vuelo";
            this.FlightBox1.ResumeLayout(false);
            this.FlightBox1.PerformLayout();
            this.FlightBox2.ResumeLayout(false);
            this.FlightBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.GroupBox FlightBox1;
        private System.Windows.Forms.Label    label1;
        private System.Windows.Forms.TextBox  id1box;
        private System.Windows.Forms.Label    lblCompany1;
        private System.Windows.Forms.TextBox  company1box;
        private System.Windows.Forms.Label    label7;
        private System.Windows.Forms.TextBox  velocity1box;
        private System.Windows.Forms.Label    labelOx1;
        private System.Windows.Forms.TextBox  origin1Xbox;
        private System.Windows.Forms.Label    labelOy1;
        private System.Windows.Forms.TextBox  origin1Ybox;
        private System.Windows.Forms.Label    labelDx1;
        private System.Windows.Forms.TextBox  dest1Xbox;
        private System.Windows.Forms.Label    labelDy1;
        private System.Windows.Forms.TextBox  dest1Ybox;

        private System.Windows.Forms.GroupBox FlightBox2;
        private System.Windows.Forms.Label    label4;
        private System.Windows.Forms.TextBox  id2box;
        private System.Windows.Forms.Label    lblCompany2;
        private System.Windows.Forms.TextBox  company2box;
        private System.Windows.Forms.Label    label8;
        private System.Windows.Forms.TextBox  velocity2box;
        private System.Windows.Forms.Label    labelOx2;
        private System.Windows.Forms.TextBox  origin2Xbox;
        private System.Windows.Forms.Label    labelOy2;
        private System.Windows.Forms.TextBox  origin2Ybox;
        private System.Windows.Forms.Label    labelDx2;
        private System.Windows.Forms.TextBox  dest2Xbox;
        private System.Windows.Forms.Label    labelDy2;
        private System.Windows.Forms.TextBox  dest2Ybox;

        private System.Windows.Forms.Button   acceptButton;
        private System.Windows.Forms.Button   cancelButton;
        private System.Windows.Forms.Button   resetbtn;
        private System.Windows.Forms.Button   nonConflictbtn;
        private System.Windows.Forms.Button   conflictbtn;
        private System.Windows.Forms.Label    Test_Plans;
    }
}
