namespace goesrecv_monitor
{
    partial class Big
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Big));
            this.labelVitErr = new System.Windows.Forms.Label();
            this.labelQuality = new System.Windows.Forms.Label();
            this.labelRsErr = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnFontColour = new System.Windows.Forms.Button();
            this.toolTipFontColourToggle = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // labelVitErr
            // 
            this.labelVitErr.AutoSize = true;
            this.labelVitErr.BackColor = System.Drawing.Color.Transparent;
            this.labelVitErr.Font = new System.Drawing.Font("Consolas", 200.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelVitErr.Location = new System.Drawing.Point(-2, -22);
            this.labelVitErr.Name = "labelVitErr";
            this.labelVitErr.Size = new System.Drawing.Size(720, 313);
            this.labelVitErr.TabIndex = 1;
            this.labelVitErr.Text = "----";
            // 
            // labelQuality
            // 
            this.labelQuality.AutoSize = true;
            this.labelQuality.BackColor = System.Drawing.Color.Transparent;
            this.labelQuality.Font = new System.Drawing.Font("Consolas", 200.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelQuality.Location = new System.Drawing.Point(-2, 266);
            this.labelQuality.Name = "labelQuality";
            this.labelQuality.Size = new System.Drawing.Size(720, 313);
            this.labelQuality.TabIndex = 3;
            this.labelQuality.Text = "----";
            // 
            // labelRsErr
            // 
            this.labelRsErr.AutoSize = true;
            this.labelRsErr.BackColor = System.Drawing.Color.Transparent;
            this.labelRsErr.Font = new System.Drawing.Font("Consolas", 200.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelRsErr.Location = new System.Drawing.Point(-2, 553);
            this.labelRsErr.Name = "labelRsErr";
            this.labelRsErr.Size = new System.Drawing.Size(720, 313);
            this.labelRsErr.TabIndex = 5;
            this.labelRsErr.Text = "----";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(540, 237);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 31);
            this.label1.TabIndex = 2;
            this.label1.Text = "Viterbi";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(532, 524);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(106, 31);
            this.label2.TabIndex = 4;
            this.label2.Text = "Quality";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(432, 814);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(206, 31);
            this.label3.TabIndex = 5;
            this.label3.Text = "Reed-Solomon";
            // 
            // btnFontColour
            // 
            this.btnFontColour.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(1)))), ((int)(((byte)(1)))));
            this.btnFontColour.Location = new System.Drawing.Point(13, 553);
            this.btnFontColour.Name = "btnFontColour";
            this.btnFontColour.Size = new System.Drawing.Size(38, 36);
            this.btnFontColour.TabIndex = 0;
            this.btnFontColour.Text = " ";
            this.toolTipFontColourToggle.SetToolTip(this.btnFontColour, "Toggle text colour");
            this.btnFontColour.UseVisualStyleBackColor = false;
            this.btnFontColour.Click += new System.EventHandler(this.btnFontColour_Click);
            // 
            // Big
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(706, 601);
            this.Controls.Add(this.btnFontColour);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labelRsErr);
            this.Controls.Add(this.labelQuality);
            this.Controls.Add(this.labelVitErr);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Big";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Large Statistics";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelVitErr;
        private System.Windows.Forms.Label labelQuality;
        private System.Windows.Forms.Label labelRsErr;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnFontColour;
        private System.Windows.Forms.ToolTip toolTipFontColourToggle;
    }
}