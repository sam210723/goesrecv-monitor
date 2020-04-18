namespace goesrecv_monitor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.labelRsErr = new System.Windows.Forms.Label();
            this.labelVitErr = new System.Windows.Forms.Label();
            this.labelSignalLock = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.progressSignalQ = new System.Windows.Forms.ProgressBar();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.labelFreqOffset = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textIP = new System.Windows.Forms.TextBox();
            this.btnConnct = new System.Windows.Forms.Button();
            this.constellationPanel = new goesrecv_monitor.ConstellationPanel();
            this.labelVersion = new System.Windows.Forms.LinkLabel();
            this.labelSite = new System.Windows.Forms.LinkLabel();
            this.tableLayoutPanel1.SuspendLayout();
            this.constellationPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.labelRsErr, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.labelVitErr, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.labelSignalLock, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.progressSignalQ, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.labelFreqOffset, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(361, 15);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(306, 160);
            this.tableLayoutPanel1.TabIndex = 13;
            // 
            // labelRsErr
            // 
            this.labelRsErr.AutoSize = true;
            this.labelRsErr.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelRsErr.ForeColor = System.Drawing.Color.White;
            this.labelRsErr.Location = new System.Drawing.Point(156, 128);
            this.labelRsErr.Name = "labelRsErr";
            this.labelRsErr.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.labelRsErr.Size = new System.Drawing.Size(12, 21);
            this.labelRsErr.TabIndex = 12;
            this.labelRsErr.Text = "-";
            // 
            // labelVitErr
            // 
            this.labelVitErr.AutoSize = true;
            this.labelVitErr.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelVitErr.ForeColor = System.Drawing.Color.White;
            this.labelVitErr.Location = new System.Drawing.Point(156, 96);
            this.labelVitErr.Name = "labelVitErr";
            this.labelVitErr.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.labelVitErr.Size = new System.Drawing.Size(12, 21);
            this.labelVitErr.TabIndex = 10;
            this.labelVitErr.Text = "-";
            // 
            // labelSignalLock
            // 
            this.labelSignalLock.AutoSize = true;
            this.labelSignalLock.BackColor = System.Drawing.Color.Black;
            this.labelSignalLock.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSignalLock.ForeColor = System.Drawing.Color.White;
            this.labelSignalLock.Location = new System.Drawing.Point(156, 0);
            this.labelSignalLock.Name = "labelSignalLock";
            this.labelSignalLock.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.labelSignalLock.Size = new System.Drawing.Size(12, 21);
            this.labelSignalLock.TabIndex = 4;
            this.labelSignalLock.Text = "-";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(3, 64);
            this.label5.Name = "label5";
            this.label5.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.label5.Size = new System.Drawing.Size(98, 21);
            this.label5.TabIndex = 7;
            this.label5.Text = "Signal Quality";
            // 
            // progressSignalQ
            // 
            this.progressSignalQ.Location = new System.Drawing.Point(156, 67);
            this.progressSignalQ.Name = "progressSignalQ";
            this.progressSignalQ.Size = new System.Drawing.Size(147, 23);
            this.progressSignalQ.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(3, 128);
            this.label4.Name = "label4";
            this.label4.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.label4.Size = new System.Drawing.Size(67, 21);
            this.label4.TabIndex = 11;
            this.label4.Text = "RS Errors";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(3, 96);
            this.label3.Name = "label3";
            this.label3.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.label3.Size = new System.Drawing.Size(91, 21);
            this.label3.TabIndex = 9;
            this.label3.Text = "Viterbi Errors";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.label1.Size = new System.Drawing.Size(83, 21);
            this.label1.TabIndex = 3;
            this.label1.Text = "Signal Lock";
            // 
            // labelFreqOffset
            // 
            this.labelFreqOffset.AutoSize = true;
            this.labelFreqOffset.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelFreqOffset.ForeColor = System.Drawing.Color.White;
            this.labelFreqOffset.Location = new System.Drawing.Point(156, 32);
            this.labelFreqOffset.Name = "labelFreqOffset";
            this.labelFreqOffset.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.labelFreqOffset.Size = new System.Drawing.Size(12, 21);
            this.labelFreqOffset.TabIndex = 6;
            this.labelFreqOffset.Text = "-";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(3, 32);
            this.label2.Name = "label2";
            this.label2.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.label2.Size = new System.Drawing.Size(115, 21);
            this.label2.TabIndex = 5;
            this.label2.Text = "Frequency Offset";
            // 
            // textIP
            // 
            this.textIP.BackColor = System.Drawing.Color.White;
            this.textIP.ForeColor = System.Drawing.Color.White;
            this.textIP.Location = new System.Drawing.Point(574, 115);
            this.textIP.MaxLength = 15;
            this.textIP.Multiline = true;
            this.textIP.Name = "textIP";
            this.textIP.Size = new System.Drawing.Size(90, 20);
            this.textIP.TabIndex = 2;
            this.textIP.Text = "192.168.1.";
            this.textIP.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textIP_KeyPress);
            // 
            // btnConnct
            // 
            this.btnConnct.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConnct.ForeColor = System.Drawing.Color.White;
            this.btnConnct.Location = new System.Drawing.Point(573, 143);
            this.btnConnct.Name = "btnConnct";
            this.btnConnct.Size = new System.Drawing.Size(92, 27);
            this.btnConnct.TabIndex = 3;
            this.btnConnct.Text = "Connect";
            this.btnConnct.UseVisualStyleBackColor = true;
            this.btnConnct.Click += new System.EventHandler(this.btnConnct_Click);
            // 
            // constellationPanel
            // 
            this.constellationPanel.Controls.Add(this.labelVersion);
            this.constellationPanel.Controls.Add(this.labelSite);
            this.constellationPanel.LineColor = System.Drawing.Color.DarkSlateGray;
            this.constellationPanel.Location = new System.Drawing.Point(0, 0);
            this.constellationPanel.Name = "constellationPanel";
            this.constellationPanel.Size = new System.Drawing.Size(350, 183);
            this.constellationPanel.SymbolColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))));
            this.constellationPanel.SymbolScale = 1.75F;
            this.constellationPanel.SymbolSize = 5;
            this.constellationPanel.TabIndex = 16;
            // 
            // labelVersion
            // 
            this.labelVersion.ActiveLinkColor = System.Drawing.Color.Silver;
            this.labelVersion.AutoSize = true;
            this.labelVersion.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelVersion.LinkColor = System.Drawing.Color.DimGray;
            this.labelVersion.Location = new System.Drawing.Point(2, 167);
            this.labelVersion.Name = "labelVersion";
            this.labelVersion.Size = new System.Drawing.Size(25, 13);
            this.labelVersion.TabIndex = 18;
            this.labelVersion.TabStop = true;
            this.labelVersion.Text = "VER";
            this.labelVersion.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.labelVersion_LinkClicked);
            // 
            // labelSite
            // 
            this.labelSite.ActiveLinkColor = System.Drawing.Color.Silver;
            this.labelSite.AutoSize = true;
            this.labelSite.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSite.LinkColor = System.Drawing.Color.DimGray;
            this.labelSite.Location = new System.Drawing.Point(286, 167);
            this.labelSite.Name = "labelSite";
            this.labelSite.Size = new System.Drawing.Size(61, 13);
            this.labelSite.TabIndex = 17;
            this.labelSite.TabStop = true;
            this.labelSite.Text = "vksdr.com";
            this.labelSite.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.labelSite_LinkClicked);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(678, 183);
            this.Controls.Add(this.constellationPanel);
            this.Controls.Add(this.textIP);
            this.Controls.Add(this.btnConnct);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "goesrecv monitor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.constellationPanel.ResumeLayout(false);
            this.constellationPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label labelSignalLock;
        private System.Windows.Forms.ProgressBar progressSignalQ;
        private System.Windows.Forms.Label labelFreqOffset;
        private System.Windows.Forms.Label labelRsErr;
        private System.Windows.Forms.Label labelVitErr;
        private System.Windows.Forms.TextBox textIP;
        private System.Windows.Forms.Button btnConnct;
        private ConstellationPanel constellationPanel;
        private System.Windows.Forms.LinkLabel labelSite;
        private System.Windows.Forms.LinkLabel labelVersion;
    }
}

