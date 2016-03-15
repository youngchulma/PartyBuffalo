namespace Party_Buffalo.Forms
{
    partial class Cache
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Cache));
            this.GameCacheList = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cb_AllCheck = new System.Windows.Forms.CheckBox();
            this.b_SaveToTxt = new System.Windows.Forms.Button();
            this.b_CleanCache = new System.Windows.Forms.Button();
            this.b_RemoveItem = new System.Windows.Forms.Button();
            this.contextMenu1 = new System.Windows.Forms.ContextMenu();
            this.m_Copy = new System.Windows.Forms.MenuItem();
            this.m_Remove = new System.Windows.Forms.MenuItem();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // GameCacheList
            // 
            this.GameCacheList.CheckBoxes = true;
            this.GameCacheList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.GameCacheList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GameCacheList.FullRowSelect = true;
            this.GameCacheList.GridLines = true;
            this.GameCacheList.LabelEdit = true;
            this.GameCacheList.Location = new System.Drawing.Point(0, 0);
            this.GameCacheList.Name = "GameCacheList";
            this.GameCacheList.ShowItemToolTips = true;
            this.GameCacheList.Size = new System.Drawing.Size(380, 206);
            this.GameCacheList.TabIndex = 0;
            this.GameCacheList.UseCompatibleStateImageBehavior = false;
            this.GameCacheList.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Title ID";
            this.columnHeader1.Width = 150;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "游戏名称";
            this.columnHeader2.Width = 220;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.Window;
            this.groupBox1.Controls.Add(this.cb_AllCheck);
            this.groupBox1.Controls.Add(this.b_SaveToTxt);
            this.groupBox1.Controls.Add(this.b_CleanCache);
            this.groupBox1.Controls.Add(this.b_RemoveItem);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Location = new System.Drawing.Point(0, 206);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(380, 51);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "缓存选项";
            // 
            // cb_AllCheck
            // 
            this.cb_AllCheck.AutoSize = true;
            this.cb_AllCheck.Location = new System.Drawing.Point(296, 21);
            this.cb_AllCheck.Name = "cb_AllCheck";
            this.cb_AllCheck.Size = new System.Drawing.Size(72, 16);
            this.cb_AllCheck.TabIndex = 1;
            this.cb_AllCheck.Text = "全部选择";
            this.cb_AllCheck.UseVisualStyleBackColor = true;
            this.cb_AllCheck.CheckedChanged += new System.EventHandler(this.cb_AllCheck_CheckedChanged);
            // 
            // b_SaveToTxt
            // 
            this.b_SaveToTxt.Location = new System.Drawing.Point(184, 18);
            this.b_SaveToTxt.Name = "b_SaveToTxt";
            this.b_SaveToTxt.Size = new System.Drawing.Size(80, 21);
            this.b_SaveToTxt.TabIndex = 0;
            this.b_SaveToTxt.Text = "保存文本";
            this.b_SaveToTxt.UseVisualStyleBackColor = true;
            this.b_SaveToTxt.Click += new System.EventHandler(this.b_SaveToTxt_Click);
            // 
            // b_CleanCache
            // 
            this.b_CleanCache.Location = new System.Drawing.Point(12, 18);
            this.b_CleanCache.Name = "b_CleanCache";
            this.b_CleanCache.Size = new System.Drawing.Size(80, 21);
            this.b_CleanCache.TabIndex = 0;
            this.b_CleanCache.Text = "清空缓存";
            this.b_CleanCache.UseVisualStyleBackColor = true;
            this.b_CleanCache.Click += new System.EventHandler(this.b_CleanCache_Click);
            // 
            // b_RemoveItem
            // 
            this.b_RemoveItem.Location = new System.Drawing.Point(98, 18);
            this.b_RemoveItem.Name = "b_RemoveItem";
            this.b_RemoveItem.Size = new System.Drawing.Size(80, 21);
            this.b_RemoveItem.TabIndex = 0;
            this.b_RemoveItem.Text = "移除选定";
            this.b_RemoveItem.UseVisualStyleBackColor = true;
            this.b_RemoveItem.Click += new System.EventHandler(this.b_RemoveItem_Click);
            // 
            // contextMenu1
            // 
            this.contextMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.m_Copy,
            this.m_Remove});
            // 
            // m_Copy
            // 
            this.m_Copy.Index = 0;
            this.m_Copy.Text = "复制选定行";
            this.m_Copy.Click += new System.EventHandler(this.m_Copy_Click);
            // 
            // m_Remove
            // 
            this.m_Remove.Index = 1;
            this.m_Remove.Text = "删除选定行";
            this.m_Remove.Click += new System.EventHandler(this.m_Remove_Click);
            // 
            // Cache
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(380, 257);
            this.Controls.Add(this.GameCacheList);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Cache";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Tag = "";
            this.Text = "游戏缓存列表";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView GameCacheList;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button b_SaveToTxt;
        private System.Windows.Forms.Button b_RemoveItem;
        private System.Windows.Forms.Button b_CleanCache;
        private System.Windows.Forms.CheckBox cb_AllCheck;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ContextMenu contextMenu1;
        private System.Windows.Forms.MenuItem m_Copy;
        private System.Windows.Forms.MenuItem m_Remove;
    }
}