namespace Party_Buffalo.Forms
{
    partial class EntryAction
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
            this.b_Cancel = new System.Windows.Forms.Button();
            this.l_Percent = new System.Windows.Forms.Label();
            this.lPercent = new System.Windows.Forms.Label();
            this.p_ProgressBar = new Extensions.NyanCatBar();
            this.SuspendLayout();
            // 
            // b_Cancel
            // 
            this.b_Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.b_Cancel.Location = new System.Drawing.Point(315, 99);
            this.b_Cancel.Name = "b_Cancel";
            this.b_Cancel.Size = new System.Drawing.Size(74, 21);
            this.b_Cancel.TabIndex = 5;
            this.b_Cancel.Text = "取消";
            this.b_Cancel.UseVisualStyleBackColor = true;
            this.b_Cancel.Click += new System.EventHandler(this.b_Cancel_Click);
            // 
            // l_Percent
            // 
            this.l_Percent.AutoEllipsis = true;
            this.l_Percent.Location = new System.Drawing.Point(12, 8);
            this.l_Percent.Name = "l_Percent";
            this.l_Percent.Size = new System.Drawing.Size(377, 14);
            this.l_Percent.TabIndex = 4;
            // 
            // lPercent
            // 
            this.lPercent.AutoSize = true;
            this.lPercent.Location = new System.Drawing.Point(12, 61);
            this.lPercent.Name = "lPercent";
            this.lPercent.Size = new System.Drawing.Size(0, 12);
            this.lPercent.TabIndex = 7;
            // 
            // p_ProgressBar
            // 
            this.p_ProgressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.p_ProgressBar.Location = new System.Drawing.Point(15, 47);
            this.p_ProgressBar.Name = "p_ProgressBar";
            this.p_ProgressBar.Size = new System.Drawing.Size(374, 21);
            this.p_ProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.p_ProgressBar.TabIndex = 6;
            // 
            // EntryAction
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(401, 123);
            this.Controls.Add(this.lPercent);
            this.Controls.Add(this.p_ProgressBar);
            this.Controls.Add(this.b_Cancel);
            this.Controls.Add(this.l_Percent);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(400, 100);
            this.Name = "EntryAction";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Extensions.NyanCatBar p_ProgressBar;
        private System.Windows.Forms.Button b_Cancel;
        private System.Windows.Forms.Label l_Percent;
        private System.Windows.Forms.Label lPercent;
    }
}