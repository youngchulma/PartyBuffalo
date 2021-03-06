﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Party_Buffalo.Forms
{
    public partial class Drive_Properties : Form
    {
        ParrotLibs.Drive mDrive;
        System.Threading.Thread gettingSpace;
        public Drive_Properties(ParrotLibs.Drive FATXDrive)
        {
            InitializeComponent();

            mDrive = FATXDrive;
            this.Load += new EventHandler(Drive_Properties_Load);
            this.FormClosing += new FormClosingEventHandler(Drive_Properties_FormClosing);
        }

        void Drive_Properties_Load(object sender, EventArgs e)
        {
            l_DiskName.Text = mDrive.Name;

            if(mDrive.DriveType == ParrotLibs.DriveType.HardDisk)
            {
                l_drivePath.Text = @"\\.\PhysicalDrive" + mDrive.DeviceIndex.ToString();
            }
            else if(mDrive.DriveType == ParrotLibs.DriveType.USB)
            {
                l_drivePath.Text = System.IO.Path.GetPathRoot(mDrive.USBPaths[0]);
            }
            else
            {
                l_drivePath.Text = mDrive.FilePath;
            }

            Do();
            foreach (ParrotLibs.Folder f in mDrive.Partitions)
            {
                TabPage tp = new TabPage(f.Name);
                tp.Controls.Add(new Forms.PartitionGeometry(f));
                tp.Controls[0].Dock = DockStyle.Fill;
                tabControl1.TabPages.Add(tp);
            }
        }

        void Drive_Properties_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (gettingSpace.IsAlive)
                {
                    e.Cancel = true;
                }
            }
            catch { }
        }

        void Do()
        {
            // Start our thread to get the space
            System.Threading.ThreadStart ts = delegate
            {
                //button1.Invoke((MethodInvoker)delegate { button1.Enabled = false; });
                this.Invoke((MethodInvoker)delegate { Cursor = Cursors.WaitCursor; });
                l_used.Invoke((MethodInvoker)delegate { l_used.Text = "Getting remaining storage..."; });
                l_Remaining.Invoke((MethodInvoker)delegate { l_Remaining.Text = "Getting remaining storage..."; });
                l_totalUsable.Invoke((MethodInvoker)delegate { l_totalUsable.Text = "Getting remaining storage..."; });
                long left = mDrive.RemainingSpace();
                long total = mDrive.PartitionSizeTotal();
                MessageBox.Show(left.ToString());
               
                //l_Remaining.Invoke((MethodInvoker)delegate { l_Remaining.Text = CLKsFATXLib.VariousFunctions.ByteConversion(left); });
                //l_used.Invoke((MethodInvoker)delegate { l_used.Text = CLKsFATXLib.VariousFunctions.ByteConversion(mDirve.PartitionSizeTotal() - left); });
                //l_totalUsable.Invoke((MethodInvoker)delegate { l_totalUsable.Text = CLKsFATXLib.VariousFunctions.ByteConversion(mDirve.PartitionSizeTotal()); });
                //progressBar1.Invoke((MethodInvoker)delegate { progressBar1.Maximum = CLKsFATXLib.VariousFunctions.UpToNearestGigabyte(mDirve.PartitionSizeTotal()); progressBar1.Value = CLKsFATXLib.VariousFunctions.UpToNearestGigabyte(mDirve.PartitionSizeTotal() - left); });
                this.Invoke((MethodInvoker)delegate { Cursor = Cursors.Default; });
            };
            gettingSpace = new System.Threading.Thread(ts);
            gettingSpace.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Do();
        }
    }
}
