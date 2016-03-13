namespace Party_Buffalo.Forms
{
    partial class DeviceSelector
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DeviceSelector));
            this.DriveList = new System.Windows.Forms.ListView();
            this.ch_DriveIcon = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ch_DriveId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ch_DriveSize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.b_ConfirmDevice = new System.Windows.Forms.Button();
            this.b_CancelDevice = new System.Windows.Forms.Button();
            this.l_Message = new System.Windows.Forms.Label();
            this.b_RefreshDrive = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // DriveList
            // 
            this.DriveList.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.DriveList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ch_DriveIcon,
            this.ch_DriveId,
            this.ch_DriveSize});
            this.DriveList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DriveList.FullRowSelect = true;
            this.DriveList.GridLines = true;
            this.DriveList.Location = new System.Drawing.Point(0, 0);
            this.DriveList.MultiSelect = false;
            this.DriveList.Name = "DriveList";
            this.DriveList.Size = new System.Drawing.Size(354, 211);
            this.DriveList.TabIndex = 0;
            this.DriveList.UseCompatibleStateImageBehavior = false;
            this.DriveList.View = System.Windows.Forms.View.Details;
            this.DriveList.SelectedIndexChanged += new System.EventHandler(this.DriveList_SelectedIndexChanged);
            // 
            // ch_DriveIcon
            // 
            this.ch_DriveIcon.Text = "驱动器图标";
            this.ch_DriveIcon.Width = 140;
            // 
            // ch_DriveId
            // 
            this.ch_DriveId.Text = "驱动器ID";
            this.ch_DriveId.Width = 90;
            // 
            // ch_DriveSize
            // 
            this.ch_DriveSize.Text = "驱动器容量";
            this.ch_DriveSize.Width = 120;
            // 
            // b_ConfirmDevice
            // 
            this.b_ConfirmDevice.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.b_ConfirmDevice.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.b_ConfirmDevice.Enabled = false;
            this.b_ConfirmDevice.Location = new System.Drawing.Point(216, 233);
            this.b_ConfirmDevice.Name = "b_ConfirmDevice";
            this.b_ConfirmDevice.Size = new System.Drawing.Size(75, 21);
            this.b_ConfirmDevice.TabIndex = 1;
            this.b_ConfirmDevice.Text = "确定";
            this.b_ConfirmDevice.UseVisualStyleBackColor = true;
            this.b_ConfirmDevice.Click += new System.EventHandler(this.b_ConfirmDrive_Click);
            // 
            // b_CancelDevice
            // 
            this.b_CancelDevice.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.b_CancelDevice.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.b_CancelDevice.Location = new System.Drawing.Point(298, 233);
            this.b_CancelDevice.Name = "b_CancelDevice";
            this.b_CancelDevice.Size = new System.Drawing.Size(75, 21);
            this.b_CancelDevice.TabIndex = 1;
            this.b_CancelDevice.Text = "取消";
            this.b_CancelDevice.UseVisualStyleBackColor = true;
            this.b_CancelDevice.Click += new System.EventHandler(this.b_CancelDrive_Click);
            // 
            // l_Message
            // 
            this.l_Message.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.l_Message.AutoSize = true;
            this.l_Message.Location = new System.Drawing.Point(12, 237);
            this.l_Message.Name = "l_Message";
            this.l_Message.Size = new System.Drawing.Size(0, 12);
            this.l_Message.TabIndex = 2;
            // 
            // b_RefreshDrive
            // 
            this.b_RefreshDrive.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.b_RefreshDrive.Location = new System.Drawing.Point(135, 233);
            this.b_RefreshDrive.Name = "b_RefreshDrive";
            this.b_RefreshDrive.Size = new System.Drawing.Size(75, 21);
            this.b_RefreshDrive.TabIndex = 3;
            this.b_RefreshDrive.Text = "刷新";
            this.b_RefreshDrive.UseVisualStyleBackColor = true;
            this.b_RefreshDrive.Click += new System.EventHandler(this.b_RefreshDrive_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.DriveList);
            this.panel1.Location = new System.Drawing.Point(14, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(358, 215);
            this.panel1.TabIndex = 4;
            // 
            // DeviceSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 261);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.b_RefreshDrive);
            this.Controls.Add(this.l_Message);
            this.Controls.Add(this.b_CancelDevice);
            this.Controls.Add(this.b_ConfirmDevice);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DeviceSelector";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "选择储存设备";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView DriveList;
        private System.Windows.Forms.Button b_ConfirmDevice;
        private System.Windows.Forms.Button b_CancelDevice;
        private System.Windows.Forms.ColumnHeader ch_DriveIcon;
        private System.Windows.Forms.ColumnHeader ch_DriveId;
        private System.Windows.Forms.Label l_Message;
        private System.Windows.Forms.ColumnHeader ch_DriveSize;
        private System.Windows.Forms.Button b_RefreshDrive;
        private System.Windows.Forms.Panel panel1;
    }
}