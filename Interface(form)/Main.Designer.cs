namespace Interface_form_
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1                        = new System.Windows.Forms.MenuStrip();
            this.optionsToolStripMenuItem          = new System.Windows.Forms.ToolStripMenuItem();
            this.flightPlansToolStripMenuItem1     = new System.Windows.Forms.ToolStripMenuItem();
            this.loadFromFileToolStripMenuItem     = new System.Windows.Forms.ToolStripMenuItem();
            this.securitySettingsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.simulationAirspaceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showToolStripMenuItem             = new System.Windows.Forms.ToolStripMenuItem();
            this.lblWelcome                        = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();

            // ── menuStrip1 ────────────────────────────────────────────────────────
            this.menuStrip1.GripMargin       = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(28, 28);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.optionsToolStripMenuItem,
                this.simulationAirspaceToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name     = "menuStrip1";
            this.menuStrip1.Size     = new System.Drawing.Size(2366, 38);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text     = "menuStrip1";

            // ── optionsToolStripMenuItem ──────────────────────────────────────────
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.flightPlansToolStripMenuItem1,
                this.loadFromFileToolStripMenuItem,
                this.securitySettingsToolStripMenuItem1});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(104, 34);
            this.optionsToolStripMenuItem.Text = "Options";

            // ── flightPlansToolStripMenuItem1 ─────────────────────────────────────
            this.flightPlansToolStripMenuItem1.Name  = "flightPlansToolStripMenuItem1";
            this.flightPlansToolStripMenuItem1.Size  = new System.Drawing.Size(315, 40);
            this.flightPlansToolStripMenuItem1.Text  = "FlightPlans";
            this.flightPlansToolStripMenuItem1.Click += new System.EventHandler(this.flightPlansToolStripMenuItem1_Click);

            // ── loadFromFileToolStripMenuItem (nuevo en v2) ───────────────────────
            this.loadFromFileToolStripMenuItem.Name  = "loadFromFileToolStripMenuItem";
            this.loadFromFileToolStripMenuItem.Size  = new System.Drawing.Size(315, 40);
            this.loadFromFileToolStripMenuItem.Text  = "Cargar desde archivo...";
            this.loadFromFileToolStripMenuItem.Click += new System.EventHandler(this.loadFromFileToolStripMenuItem_Click);

            // ── securitySettingsToolStripMenuItem1 ────────────────────────────────
            this.securitySettingsToolStripMenuItem1.Name  = "securitySettingsToolStripMenuItem1";
            this.securitySettingsToolStripMenuItem1.Size  = new System.Drawing.Size(315, 40);
            this.securitySettingsToolStripMenuItem1.Text  = "Security Settings";
            this.securitySettingsToolStripMenuItem1.Click += new System.EventHandler(this.securitySettingsToolStripMenuItem1_Click);

            // ── simulationAirspaceToolStripMenuItem ───────────────────────────────
            this.simulationAirspaceToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.showToolStripMenuItem});
            this.simulationAirspaceToolStripMenuItem.Name = "simulationAirspaceToolStripMenuItem";
            this.simulationAirspaceToolStripMenuItem.Size = new System.Drawing.Size(214, 34);
            this.simulationAirspaceToolStripMenuItem.Text = "Simulation Airspace";

            // ── showToolStripMenuItem ─────────────────────────────────────────────
            this.showToolStripMenuItem.Name  = "showToolStripMenuItem";
            this.showToolStripMenuItem.Size  = new System.Drawing.Size(315, 40);
            this.showToolStripMenuItem.Text  = "show";
            this.showToolStripMenuItem.Click += new System.EventHandler(this.showToolStripMenuItem_Click);

            // ── lblWelcome (nuevo en v2) ──────────────────────────────────────────
            this.lblWelcome.AutoSize  = false;
            this.lblWelcome.Location  = new System.Drawing.Point(20, 48);
            this.lblWelcome.Name      = "lblWelcome";
            this.lblWelcome.Size      = new System.Drawing.Size(500, 30);
            this.lblWelcome.TabIndex  = 1;
            this.lblWelcome.Text      = "Bienvenido";
            this.lblWelcome.Font      = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);

            // ── Main ──────────────────────────────────────────────────────────────
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode       = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize          = new System.Drawing.Size(2366, 1206);
            this.Controls.Add(this.lblWelcome);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin        = new System.Windows.Forms.Padding(4);
            this.Name          = "Main";
            this.Text          = "ATC Deconflicting Tool v2";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem flightPlansToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem loadFromFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem securitySettingsToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem simulationAirspaceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showToolStripMenuItem;
        private System.Windows.Forms.Label lblWelcome;
    }
}
