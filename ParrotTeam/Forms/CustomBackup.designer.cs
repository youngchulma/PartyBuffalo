namespace Party_Buffalo.Forms
{
    partial class CustomBackup
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CustomBackup));
            this.b_BeginBackup = new System.Windows.Forms.Button();
            this.sBackupTypeList = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.contextMenu1 = new System.Windows.Forms.ContextMenu();
            this.m_AllCheck = new System.Windows.Forms.MenuItem();
            this.m_AllUncheck = new System.Windows.Forms.MenuItem();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // b_BeginBackup
            // 
            this.b_BeginBackup.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.b_BeginBackup.Location = new System.Drawing.Point(0, 419);
            this.b_BeginBackup.Name = "b_BeginBackup";
            this.b_BeginBackup.Size = new System.Drawing.Size(284, 42);
            this.b_BeginBackup.TabIndex = 3;
            this.b_BeginBackup.Text = "下一步";
            this.b_BeginBackup.UseVisualStyleBackColor = true;
            this.b_BeginBackup.Click += new System.EventHandler(this.b_BeginBackup_Click);
            // 
            // sBackupTypeList
            // 
            this.sBackupTypeList.CheckBoxes = true;
            this.sBackupTypeList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.sBackupTypeList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sBackupTypeList.FullRowSelect = true;
            this.sBackupTypeList.GridLines = true;
            this.sBackupTypeList.Location = new System.Drawing.Point(0, 90);
            this.sBackupTypeList.Name = "sBackupTypeList";
            this.sBackupTypeList.ShowItemToolTips = true;
            this.sBackupTypeList.Size = new System.Drawing.Size(280, 325);
            this.sBackupTypeList.TabIndex = 0;
            this.sBackupTypeList.UseCompatibleStateImageBehavior = false;
            this.sBackupTypeList.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "名称";
            this.columnHeader1.Width = 265;
            // 
            // textBox1
            // 
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.textBox1.Enabled = false;
            this.textBox1.Location = new System.Drawing.Point(0, 0);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(280, 90);
            this.textBox1.TabIndex = 4;
            this.textBox1.Text = "\r\n自定义备份说明：\r\n1. 用户可以设置哪些文件不备份！\r\n2. 从清单中选取不想备份的内容(可以多选)；\r\n3. 点击“下一步”，开始进行自定义备份；\r\n4." +
    " 鼠标右键，可以”全选“或”取消全选“；";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.sBackupTypeList);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(284, 419);
            this.panel1.TabIndex = 5;
            // 
            // contextMenu1
            // 
            this.contextMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.m_AllCheck,
            this.m_AllUncheck});
            // 
            // m_AllCheck
            // 
            this.m_AllCheck.Index = 0;
            this.m_AllCheck.Text = "全选";
            this.m_AllCheck.Click += new System.EventHandler(this.m_AllCheck_Click);
            // 
            // m_AllUncheck
            // 
            this.m_AllUncheck.Index = 1;
            this.m_AllUncheck.Text = "取消全选";
            this.m_AllUncheck.Click += new System.EventHandler(this.m_AllUncheck_Click);
            // 
            // CustomBackup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 461);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.b_BeginBackup);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "CustomBackup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "自定义备份";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button b_BeginBackup;
        private System.Windows.Forms.ListView sBackupTypeList;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ContextMenu contextMenu1;
        private System.Windows.Forms.MenuItem m_AllCheck;
        private System.Windows.Forms.MenuItem m_AllUncheck;
    }
}