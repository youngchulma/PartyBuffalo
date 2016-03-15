namespace Party_Buffalo.Forms
{
    partial class AddLabel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddLabel));
            this.b_Confirm = new System.Windows.Forms.Button();
            this.b_Cancel = new System.Windows.Forms.Button();
            this.l_FolderPath = new System.Windows.Forms.Label();
            this.t_FolderPath = new System.Windows.Forms.TextBox();
            this.t_FolderName = new System.Windows.Forms.TextBox();
            this.l_FolderName = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // b_Confirm
            // 
            this.b_Confirm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.b_Confirm.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.b_Confirm.Location = new System.Drawing.Point(201, 72);
            this.b_Confirm.Name = "b_Confirm";
            this.b_Confirm.Size = new System.Drawing.Size(75, 21);
            this.b_Confirm.TabIndex = 2;
            this.b_Confirm.Text = "确认";
            this.b_Confirm.UseVisualStyleBackColor = true;
            this.b_Confirm.Click += new System.EventHandler(this.b_Confirm_Click);
            // 
            // b_Cancel
            // 
            this.b_Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.b_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.b_Cancel.Location = new System.Drawing.Point(282, 72);
            this.b_Cancel.Name = "b_Cancel";
            this.b_Cancel.Size = new System.Drawing.Size(75, 21);
            this.b_Cancel.TabIndex = 3;
            this.b_Cancel.Text = "取消";
            this.b_Cancel.UseVisualStyleBackColor = true;
            this.b_Cancel.Click += new System.EventHandler(this.b_Cancel_Click);
            // 
            // l_FolderPath
            // 
            this.l_FolderPath.AutoSize = true;
            this.l_FolderPath.Location = new System.Drawing.Point(17, 17);
            this.l_FolderPath.Name = "l_FolderPath";
            this.l_FolderPath.Size = new System.Drawing.Size(35, 12);
            this.l_FolderPath.TabIndex = 1;
            this.l_FolderPath.Text = "目录:";
            // 
            // t_FolderPath
            // 
            this.t_FolderPath.Location = new System.Drawing.Point(67, 14);
            this.t_FolderPath.Name = "t_FolderPath";
            this.t_FolderPath.Size = new System.Drawing.Size(290, 21);
            this.t_FolderPath.TabIndex = 1;
            this.t_FolderPath.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // t_FolderName
            // 
            this.t_FolderName.Location = new System.Drawing.Point(67, 38);
            this.t_FolderName.Name = "t_FolderName";
            this.t_FolderName.Size = new System.Drawing.Size(290, 21);
            this.t_FolderName.TabIndex = 0;
            this.t_FolderName.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // l_FolderName
            // 
            this.l_FolderName.AutoSize = true;
            this.l_FolderName.Location = new System.Drawing.Point(17, 41);
            this.l_FolderName.Name = "l_FolderName";
            this.l_FolderName.Size = new System.Drawing.Size(35, 12);
            this.l_FolderName.TabIndex = 3;
            this.l_FolderName.Text = "标签:";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.l_FolderPath);
            this.panel1.Controls.Add(this.b_Cancel);
            this.panel1.Controls.Add(this.l_FolderName);
            this.panel1.Controls.Add(this.b_Confirm);
            this.panel1.Controls.Add(this.t_FolderPath);
            this.panel1.Controls.Add(this.t_FolderName);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(380, 107);
            this.panel1.TabIndex = 4;
            // 
            // AddLabel
            // 
            this.AcceptButton = this.b_Confirm;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(380, 107);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddLabel";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "添加标签";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button b_Cancel;
        private System.Windows.Forms.Label l_FolderPath;
        private System.Windows.Forms.TextBox t_FolderPath;
        private System.Windows.Forms.TextBox t_FolderName;
        private System.Windows.Forms.Label l_FolderName;
        internal System.Windows.Forms.Button b_Confirm;
        private System.Windows.Forms.Panel panel1;
    }
}