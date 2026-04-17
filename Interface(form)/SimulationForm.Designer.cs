namespace Interface_form_
{
    partial class SimulationForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components    = new System.ComponentModel.Container();
            this.panel1        = new System.Windows.Forms.Panel();
            this.cyclebtn      = new System.Windows.Forms.Button();
            this.groupBox1     = new System.Windows.Forms.GroupBox();
            this.startbtn      = new System.Windows.Forms.Button();
            this.stopbtn       = new System.Windows.Forms.Button();
            this.timer1        = new System.Windows.Forms.Timer(this.components);
            this.infobtn       = new System.Windows.Forms.Button();
            this.conflictbtn   = new System.Windows.Forms.Button();
            this.restartbtn    = new System.Windows.Forms.Button();
            this.undobtn       = new System.Windows.Forms.Button();
            this.savebtn       = new System.Windows.Forms.Button();
            this.speedGroupBox = new System.Windows.Forms.GroupBox();
            this.speedGrid     = new System.Windows.Forms.DataGridView();
            this.colId         = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSpeed      = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            this.speedGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.speedGrid)).BeginInit();
            this.SuspendLayout();

            // ── panel1 (espacio de vuelo) ──────────────────────────────────────
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Location    = new System.Drawing.Point(77, 71);
            this.panel1.Name        = "panel1";
            this.panel1.Size        = new System.Drawing.Size(1200, 900);
            this.panel1.TabIndex    = 0;

            // ── cyclebtn ───────────────────────────────────────────────────────
            this.cyclebtn.Location            = new System.Drawing.Point(1355, 71);
            this.cyclebtn.Name                = "cyclebtn";
            this.cyclebtn.Size                = new System.Drawing.Size(156, 56);
            this.cyclebtn.TabIndex            = 1;
            this.cyclebtn.Text                = "Cycle";
            this.cyclebtn.UseVisualStyleBackColor = true;
            this.cyclebtn.Click              += new System.EventHandler(this.cyclebtn_Click);

            // ── groupBox1 (AutoSim) ────────────────────────────────────────────
            this.groupBox1.Controls.Add(this.stopbtn);
            this.groupBox1.Controls.Add(this.startbtn);
            this.groupBox1.Location  = new System.Drawing.Point(1355, 166);
            this.groupBox1.Name      = "groupBox1";
            this.groupBox1.Size      = new System.Drawing.Size(156, 210);
            this.groupBox1.TabIndex  = 2;
            this.groupBox1.TabStop   = false;
            this.groupBox1.Text      = "AutoSim";

            // startbtn
            this.startbtn.Location            = new System.Drawing.Point(16, 43);
            this.startbtn.Name                = "startbtn";
            this.startbtn.Size                = new System.Drawing.Size(118, 52);
            this.startbtn.TabIndex            = 0;
            this.startbtn.Text                = "Start";
            this.startbtn.UseVisualStyleBackColor = true;
            this.startbtn.Click              += new System.EventHandler(this.startbtn_Click);

            // stopbtn
            this.stopbtn.Location             = new System.Drawing.Point(16, 122);
            this.stopbtn.Name                 = "stopbtn";
            this.stopbtn.Size                 = new System.Drawing.Size(118, 52);
            this.stopbtn.TabIndex             = 1;
            this.stopbtn.Text                 = "Stop";
            this.stopbtn.UseVisualStyleBackColor = true;
            this.stopbtn.Click               += new System.EventHandler(this.stopbtn_Click);

            // ── timer1 ─────────────────────────────────────────────────────────
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);

            // ── infobtn ────────────────────────────────────────────────────────
            this.infobtn.Location             = new System.Drawing.Point(1355, 419);
            this.infobtn.Name                 = "infobtn";
            this.infobtn.Size                 = new System.Drawing.Size(156, 56);
            this.infobtn.TabIndex             = 3;
            this.infobtn.Text                 = "Space Info";
            this.infobtn.UseVisualStyleBackColor = true;
            this.infobtn.Click               += new System.EventHandler(this.infobtn_Click);

            // ── conflictbtn ────────────────────────────────────────────────────
            this.conflictbtn.Location         = new System.Drawing.Point(1355, 505);
            this.conflictbtn.Name             = "conflictbtn";
            this.conflictbtn.Size             = new System.Drawing.Size(156, 56);
            this.conflictbtn.TabIndex         = 4;
            this.conflictbtn.Text             = "Check Conflict";
            this.conflictbtn.UseVisualStyleBackColor = true;
            this.conflictbtn.Click           += new System.EventHandler(this.conflictbtn_Click);

            // ── restartbtn ─────────────────────────────────────────────────────
            this.restartbtn.Location          = new System.Drawing.Point(1355, 591);
            this.restartbtn.Name              = "restartbtn";
            this.restartbtn.Size              = new System.Drawing.Size(156, 56);
            this.restartbtn.TabIndex          = 5;
            this.restartbtn.Text              = "Restart";
            this.restartbtn.UseVisualStyleBackColor = true;
            this.restartbtn.Click            += new System.EventHandler(this.restartbtn_Click);

            // ── undobtn (nuevo v2) ─────────────────────────────────────────────
            this.undobtn.Location             = new System.Drawing.Point(1355, 657);
            this.undobtn.Name                 = "undobtn";
            this.undobtn.Size                 = new System.Drawing.Size(156, 56);
            this.undobtn.TabIndex             = 7;
            this.undobtn.Text                 = "Undo Step";
            this.undobtn.UseVisualStyleBackColor = true;
            this.undobtn.Click               += new System.EventHandler(this.undobtn_Click);

            // ── savebtn (nuevo v2) ─────────────────────────────────────────────
            this.savebtn.Location             = new System.Drawing.Point(1355, 723);
            this.savebtn.Name                 = "savebtn";
            this.savebtn.Size                 = new System.Drawing.Size(156, 56);
            this.savebtn.TabIndex             = 8;
            this.savebtn.Text                 = "Save State";
            this.savebtn.UseVisualStyleBackColor = true;
            this.savebtn.Click               += new System.EventHandler(this.savebtn_Click);

            // ── speedGroupBox (movido a y=789 en v2) ──────────────────────────
            this.speedGroupBox.Controls.Add(this.speedGrid);
            this.speedGroupBox.Location  = new System.Drawing.Point(1355, 789);
            this.speedGroupBox.Name      = "speedGroupBox";
            this.speedGroupBox.Size      = new System.Drawing.Size(160, 260);
            this.speedGroupBox.TabIndex  = 6;
            this.speedGroupBox.TabStop   = false;
            this.speedGroupBox.Text      = "Velocidades";

            // speedGrid
            this.speedGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.speedGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                this.colId,
                this.colSpeed });
            this.speedGrid.Location          = new System.Drawing.Point(6, 28);
            this.speedGrid.Name              = "speedGrid";
            this.speedGrid.RowHeadersVisible = false;
            this.speedGrid.Size              = new System.Drawing.Size(146, 220);
            this.speedGrid.TabIndex          = 0;
            this.speedGrid.CellEndEdit      += new System.Windows.Forms.DataGridViewCellEventHandler(this.speedGrid_CellEndEdit);

            // colId
            this.colId.HeaderText = "Vuelo";
            this.colId.Name       = "colId";
            this.colId.ReadOnly   = true;
            this.colId.Width      = 55;

            // colSpeed
            this.colSpeed.HeaderText = "Vel (u/min)";
            this.colSpeed.Name       = "colSpeed";
            this.colSpeed.Width      = 80;

            // ── SimulationForm ─────────────────────────────────────────────────
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode       = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize          = new System.Drawing.Size(1747, 1100);
            this.Controls.Add(this.savebtn);
            this.Controls.Add(this.undobtn);
            this.Controls.Add(this.speedGroupBox);
            this.Controls.Add(this.restartbtn);
            this.Controls.Add(this.conflictbtn);
            this.Controls.Add(this.infobtn);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cyclebtn);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name   = "SimulationForm";
            this.Text   = "SimulationForm - v2";
            this.Load  += new System.EventHandler(this.SimulationForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.speedGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.speedGrid)).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button cyclebtn;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button stopbtn;
        private System.Windows.Forms.Button startbtn;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button infobtn;
        private System.Windows.Forms.Button conflictbtn;
        private System.Windows.Forms.Button restartbtn;
        private System.Windows.Forms.Button undobtn;
        private System.Windows.Forms.Button savebtn;
        private System.Windows.Forms.GroupBox speedGroupBox;
        private System.Windows.Forms.DataGridView speedGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn colId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSpeed;
    }
}
