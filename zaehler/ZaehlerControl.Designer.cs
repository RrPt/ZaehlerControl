namespace zaehlerNS
{
    partial class ZaehlerControl
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

        #region Vom Komponenten-Designer generierter Code

        /// <summary> 
        /// Erforderliche Methode für die Designerunterstützung. 
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.cBIntervall = new System.Windows.Forms.ComboBox();
            this.cBAnzTage = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cBMarker = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.cBIntervallgrenzen = new System.Windows.Forms.CheckBox();
            this.cBCalcMode = new System.Windows.Forms.ComboBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // chart1
            // 
            this.chart1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea1.AxisX.LabelStyle.Format = "d.M.yy HH:mm";
            chartArea1.AxisY.IsStartedFromZero = false;
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(13, 49);
            this.chart1.Name = "chart1";
            this.chart1.Size = new System.Drawing.Size(808, 403);
            this.chart1.TabIndex = 3;
            this.chart1.Text = "chart1";
            // 
            // cBIntervall
            // 
            this.cBIntervall.FormattingEnabled = true;
            this.cBIntervall.Location = new System.Drawing.Point(188, 16);
            this.cBIntervall.Name = "cBIntervall";
            this.cBIntervall.Size = new System.Drawing.Size(79, 21);
            this.cBIntervall.TabIndex = 4;
            this.cBIntervall.Text = "all";
            this.cBIntervall.SelectedIndexChanged += new System.EventHandler(this.cBIntervall_SelectedIndexChanged);
            // 
            // cBAnzTage
            // 
            this.cBAnzTage.FormattingEnabled = true;
            this.cBAnzTage.Items.AddRange(new object[] {
            "1",
            "7",
            "30",
            "365",
            "3650"});
            this.cBAnzTage.Location = new System.Drawing.Point(74, 16);
            this.cBAnzTage.Name = "cBAnzTage";
            this.cBAnzTage.Size = new System.Drawing.Size(58, 21);
            this.cBAnzTage.TabIndex = 5;
            this.cBAnzTage.Text = "0.1";
            this.cBAnzTage.TextChanged += new System.EventHandler(this.cBAnzahlTage_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Anzahl Tage";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(138, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Intervall";
            // 
            // cBMarker
            // 
            this.cBMarker.AutoSize = true;
            this.cBMarker.Checked = true;
            this.cBMarker.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cBMarker.Location = new System.Drawing.Point(376, 18);
            this.cBMarker.Name = "cBMarker";
            this.cBMarker.Size = new System.Drawing.Size(59, 17);
            this.cBMarker.TabIndex = 9;
            this.cBMarker.Text = "Marker";
            this.cBMarker.UseVisualStyleBackColor = true;
            this.cBMarker.CheckedChanged += new System.EventHandler(this.cBMarker_CheckedChanged);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(771, 14);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(47, 23);
            this.button1.TabIndex = 13;
            this.button1.Text = "Einst";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // cBIntervallgrenzen
            // 
            this.cBIntervallgrenzen.AutoSize = true;
            this.cBIntervallgrenzen.Location = new System.Drawing.Point(441, 18);
            this.cBIntervallgrenzen.Name = "cBIntervallgrenzen";
            this.cBIntervallgrenzen.Size = new System.Drawing.Size(101, 17);
            this.cBIntervallgrenzen.TabIndex = 14;
            this.cBIntervallgrenzen.Text = "Intervallgrenzen";
            this.cBIntervallgrenzen.UseVisualStyleBackColor = true;
            this.cBIntervallgrenzen.CheckStateChanged += new System.EventHandler(this.cBIntervallgrenzen_CheckStateChanged);
            // 
            // cBCalcMode
            // 
            this.cBCalcMode.FormattingEnabled = true;
            this.cBCalcMode.Location = new System.Drawing.Point(282, 16);
            this.cBCalcMode.Name = "cBCalcMode";
            this.cBCalcMode.Size = new System.Drawing.Size(72, 21);
            this.cBCalcMode.TabIndex = 15;
            this.cBCalcMode.SelectedIndexChanged += new System.EventHandler(this.cBCalcMode_SelectedIndexChanged);
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.ForeColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.progressBar1.Location = new System.Drawing.Point(-2, 3);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(825, 40);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar1.TabIndex = 16;
            // 
            // ZaehlerControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Controls.Add(this.cBCalcMode);
            this.Controls.Add(this.cBIntervallgrenzen);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.cBMarker);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cBAnzTage);
            this.Controls.Add(this.cBIntervall);
            this.Controls.Add(this.chart1);
            this.Controls.Add(this.progressBar1);
            this.Name = "ZaehlerControl";
            this.Size = new System.Drawing.Size(821, 469);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.ComboBox cBIntervall;
        private System.Windows.Forms.ComboBox cBAnzTage;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox cBMarker;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox cBIntervallgrenzen;
        private System.Windows.Forms.ComboBox cBCalcMode;
        public System.Windows.Forms.ProgressBar progressBar1;
    }
}
