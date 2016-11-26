namespace zaehlerNS
{
    partial class Form1
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.zaehlerControl1 = new zaehlerNS.ZaehlerControl();
            this.zaehlerControl2 = new zaehlerNS.ZaehlerControl();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.zaehlerControl1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.zaehlerControl2);
            this.splitContainer1.Size = new System.Drawing.Size(743, 612);
            this.splitContainer1.SplitterDistance = 298;
            this.splitContainer1.TabIndex = 2;
            // 
            // zaehlerControl1
            // 
            this.zaehlerControl1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.zaehlerControl1.CalcMode = zaehlerNS.CalcModeEnum.average;
            this.zaehlerControl1.Cursor = System.Windows.Forms.Cursors.Default;
            this.zaehlerControl1.DataOnIntervalBoundarys = true;
            this.zaehlerControl1.DisplayAnzTage = 0.125D;
            this.zaehlerControl1.DisplayIntervall = zaehlerNS.ZeitIntervall.Minute;
            this.zaehlerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.zaehlerControl1.Location = new System.Drawing.Point(0, 0);
            this.zaehlerControl1.Name = "zaehlerControl1";
            this.zaehlerControl1.ShowMarker = true;
            this.zaehlerControl1.Size = new System.Drawing.Size(743, 298);
            this.zaehlerControl1.TabIndex = 0;
            // 
            // zaehlerControl2
            // 
            this.zaehlerControl2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.zaehlerControl2.CalcMode = zaehlerNS.CalcModeEnum.difference;
            this.zaehlerControl2.Cursor = System.Windows.Forms.Cursors.Default;
            this.zaehlerControl2.DataOnIntervalBoundarys = false;
            this.zaehlerControl2.DisplayAnzTage = 0.125D;
            this.zaehlerControl2.DisplayIntervall = zaehlerNS.ZeitIntervall.all;
            this.zaehlerControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.zaehlerControl2.Location = new System.Drawing.Point(0, 0);
            this.zaehlerControl2.Name = "zaehlerControl2";
            this.zaehlerControl2.ShowMarker = false;
            this.zaehlerControl2.Size = new System.Drawing.Size(743, 310);
            this.zaehlerControl2.TabIndex = 1;
            // 
            // timer1
            // 
            this.timer1.Interval = 10000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(743, 612);
            this.Controls.Add(this.splitContainer1);
            this.Name = "Form1";
            this.Text = "ZaehlerControlTest";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private zaehlerNS.ZaehlerControl zaehlerControl1;
        private ZaehlerControl zaehlerControl2;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Timer timer1;
    }
}

