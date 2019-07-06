﻿namespace goesrecv_monitor
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
            this.labelVersion = new System.Windows.Forms.Label();
            this.pboxConstellation = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.labelDemodStatus = new System.Windows.Forms.Label();
            this.progressSignalQ = new System.Windows.Forms.ProgressBar();
            this.labelFreqOffset = new System.Windows.Forms.Label();
            this.labelVitErr = new System.Windows.Forms.Label();
            this.labelRsErr = new System.Windows.Forms.Label();
            this.textIP = new System.Windows.Forms.TextBox();
            this.btnConnct = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pboxConstellation)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelVersion
            // 
            this.labelVersion.AutoSize = true;
            this.labelVersion.BackColor = System.Drawing.Color.Black;
            this.labelVersion.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelVersion.ForeColor = System.Drawing.Color.DimGray;
            this.labelVersion.Location = new System.Drawing.Point(3, 381);
            this.labelVersion.Name = "labelVersion";
            this.labelVersion.Size = new System.Drawing.Size(28, 15);
            this.labelVersion.TabIndex = 15;
            this.labelVersion.Text = "VER";
            // 
            // pboxConstellation
            // 
            this.pboxConstellation.Location = new System.Drawing.Point(0, 0);
            this.pboxConstellation.Name = "pboxConstellation";
            this.pboxConstellation.Size = new System.Drawing.Size(250, 400);
            this.pboxConstellation.TabIndex = 1;
            this.pboxConstellation.TabStop = false;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.labelRsErr, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.labelVitErr, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.labelDemodStatus, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.progressSignalQ, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.labelFreqOffset, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(265, 12);
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.label1.Size = new System.Drawing.Size(133, 21);
            this.label1.TabIndex = 3;
            this.label1.Text = "Demodulator Status";
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
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(3, 128);
            this.label4.Name = "label4";
            this.label4.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.label4.Size = new System.Drawing.Size(102, 21);
            this.label4.TabIndex = 11;
            this.label4.Text = "Reed Solomon";
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
            // labelDemodStatus
            // 
            this.labelDemodStatus.AutoSize = true;
            this.labelDemodStatus.BackColor = System.Drawing.Color.Red;
            this.labelDemodStatus.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDemodStatus.ForeColor = System.Drawing.Color.White;
            this.labelDemodStatus.Location = new System.Drawing.Point(156, 0);
            this.labelDemodStatus.Name = "labelDemodStatus";
            this.labelDemodStatus.Padding = new System.Windows.Forms.Padding(34, 5, 34, 5);
            this.labelDemodStatus.Size = new System.Drawing.Size(147, 26);
            this.labelDemodStatus.TabIndex = 4;
            this.labelDemodStatus.Text = "UNLOCKED";
            // 
            // progressSignalQ
            // 
            this.progressSignalQ.Location = new System.Drawing.Point(156, 67);
            this.progressSignalQ.Name = "progressSignalQ";
            this.progressSignalQ.Size = new System.Drawing.Size(147, 23);
            this.progressSignalQ.TabIndex = 8;
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
            // textIP
            // 
            this.textIP.BackColor = System.Drawing.Color.White;
            this.textIP.ForeColor = System.Drawing.Color.White;
            this.textIP.Location = new System.Drawing.Point(267, 331);
            this.textIP.MaxLength = 15;
            this.textIP.Name = "textIP";
            this.textIP.Size = new System.Drawing.Size(96, 20);
            this.textIP.TabIndex = 2;
            this.textIP.Text = "192.168.1.";
            // 
            // btnConnct
            // 
            this.btnConnct.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConnct.ForeColor = System.Drawing.Color.White;
            this.btnConnct.Location = new System.Drawing.Point(265, 357);
            this.btnConnct.Name = "btnConnct";
            this.btnConnct.Size = new System.Drawing.Size(100, 27);
            this.btnConnct.TabIndex = 3;
            this.btnConnct.Text = "Connect";
            this.btnConnct.UseVisualStyleBackColor = true;
            this.btnConnct.Click += new System.EventHandler(this.btnConnct_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(584, 400);
            this.Controls.Add(this.textIP);
            this.Controls.Add(this.btnConnct);
            this.Controls.Add(this.labelVersion);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.pboxConstellation);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Main";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "goesrecv Monitor";
            ((System.ComponentModel.ISupportInitialize)(this.pboxConstellation)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelVersion;
        private System.Windows.Forms.PictureBox pboxConstellation;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label labelDemodStatus;
        private System.Windows.Forms.ProgressBar progressSignalQ;
        private System.Windows.Forms.Label labelFreqOffset;
        private System.Windows.Forms.Label labelRsErr;
        private System.Windows.Forms.Label labelVitErr;
        private System.Windows.Forms.TextBox textIP;
        private System.Windows.Forms.Button btnConnct;
    }
}

