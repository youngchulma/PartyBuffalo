using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Extensions;

namespace Party_Buffalo.Forms
{
    public partial class DeviceSelector : Form
    {
        Main mMain;
        System.Threading.Thread mWorker;
        List<CLKsFATXLib.Drive> mDriveList = null;

        public DeviceSelector(Main m)
        {
            InitializeComponent();
            DriveList.LargeImageList = LargeList;
            DriveList.SmallImageList = LargeList;
            DriveList.SetExplorerTheme();
            this.mMain = m;
            this.Load += new EventHandler(DeviceSelector_Load);
            this.DriveList.DoubleClick += new EventHandler(DriveList_DoubleClick);
            this.FormClosing += new FormClosingEventHandler(DeviceSelector_FormClosing);
        }

        public DeviceSelector()
        {
            InitializeComponent();
            DriveList.LargeImageList = LargeList;
            DriveList.SmallImageList = LargeList;
            DriveList.SetExplorerTheme();
            this.Load += new EventHandler(DeviceSelector_Load);
            this.DriveList.DoubleClick += new EventHandler(DriveList_DoubleClick);
            this.FormClosing += new FormClosingEventHandler(DeviceSelector_FormClosing);
        }

        ImageList LargeList
        {
            get
            {
                ImageList sImageList = new ImageList();
                sImageList.ImageSize = new Size(49, 64);
                sImageList.ColorDepth = ColorDepth.Depth32Bit;
                sImageList.Images.Add(Properties.Resources.HDD);
                sImageList.Images.Add(Properties.Resources.USB);
                sImageList.Images.Add(Properties.Resources.BACKUP);

                return sImageList;
            }
        }

        void DeviceSelector_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (mWorker != null)
            {
                while (mWorker.IsAlive)
                {
                    // Do nothing
                }
            }
        }

        void DeviceSelector_Load(object sender, EventArgs e)
        {
            LoadDeivce();
        }

        void LoadDeivce()
        {
            this.mWorker = new System.Threading.Thread((System.Threading.ThreadStart)delegate
            {
                try
                {
                    if (mDriveList != null)
                    {
                        for (int i = 0; i < mDriveList.Count; i++)
                        {
                            mDriveList[i].Close();
                        }
                    }

                    DriveList.Invoke((MethodInvoker)delegate
                    {
                        DriveList.Items.Clear();
                    });

                    l_Message.Invoke((MethodInvoker)delegate {
                        l_Message.Text = "正在加载驱动器...";
                    });

                    b_RefreshDrive.Invoke((MethodInvoker)delegate 
                    {
                        b_RefreshDrive.Enabled = false;
                    });

                    mDriveList = CLKsFATXLib.StartHere.GetFATXDrives().ToList();
                    if (Properties.Settings.Default.RecentLoadFiles != null)
                    {
                        foreach (string sPathToFile in Properties.Settings.Default.RecentLoadFiles)
                        {
                            if (System.IO.File.Exists(sPathToFile))
                            {
                                try
                                {
                                    CLKsFATXLib.Drive sDrive = new CLKsFATXLib.Drive(sPathToFile);
                                    if (sDrive.IsFATXDrive())
                                    {
                                        mDriveList.Add(sDrive);
                                    }
                                }
                                catch (Exception e)
                                {
                                    if (!e.Message.Contains("being used"))
                                    {
                                        MessageBox.Show("An exception was thrown: " + e.Message + "\r\n\r\nStack Trace:\r\n" + e.StackTrace);
                                    }
                                    else
                                    {
                                        continue;
                                    }
                                }
                            }
                        }
                    }

                    List<ListViewItem> sListView = new List<ListViewItem>();
                    for (int i = 0; i < mDriveList.Count; i++)
                    {
                        try
                        {
                            ListViewItem sDriveItem = new ListViewItem(mDriveList[i].Name);
                            if (mDriveList[i].DriveType == CLKsFATXLib.DriveType.HardDisk)
                            {
                                sDriveItem.ImageIndex = 0;
                                sDriveItem.SubItems.Add(mDriveList[i].DeviceIndex.ToString());
                            }
                            else if (mDriveList[i].DriveType == CLKsFATXLib.DriveType.USB)
                            {
                                sDriveItem.ImageIndex = 1;
                                sDriveItem.SubItems.Add(System.IO.Path.GetPathRoot(mDriveList[i].USBPaths[0]));
                            }
                            else
                            {
                                sDriveItem.ImageIndex = 2;
                                sDriveItem.SubItems.Add(System.IO.Path.GetFileName(mDriveList[i].FilePath));
                            }

                            sDriveItem.SubItems.Add(mDriveList[i].LengthFriendly);
                            sDriveItem.Tag = mDriveList[i];
                            sListView.Add(sDriveItem);
                        }
                        catch (Exception e)
                        {
                            if (!e.Message.Contains("being used"))
                            {
                                MessageBox.Show("An exception was thrown: " + e.Message + "\r\n\r\nStack Trace:\r\n" + e.StackTrace);
                            }
                            else
                            {
                                continue;
                            }
                        }
                    }

                    DriveList.Invoke((MethodInvoker)delegate
                    {
                        DriveList.Items.AddRange(sListView.ToArray());
                    });

                    l_Message.Invoke((MethodInvoker)delegate
                    {
                        if (sListView.Count == 0)
                        {
                            l_Message.Text = "未发现驱动器...";
                        }
                        else
                        {
                            l_Message.Text = sListView.Count.ToString() + "个驱动器被加载！";
                        }
                    });
                    b_RefreshDrive.Invoke((MethodInvoker)delegate { b_RefreshDrive.Enabled = true; });
                }
                catch (Exception e)
                {
                    if (!e.Message.Contains("being used"))
                    {
                        MessageBox.Show("An exception was thrown: " + e.Message + "\r\n\r\nStack Trace:\r\n" + e.StackTrace);
                    }
                }
            });

            mWorker.Start();
        }

        private void b_RefreshDrive_Click(object sender, EventArgs e)
        {
            LoadDeivce();
        }

        private void b_ConfirmDrive_Click(object sender, EventArgs e)
        {
            if (mMain != null)
            {
                if (mMain.Drive != null)
                {
                    mMain.Drive.Close();
                }
                mMain.Drive = (CLKsFATXLib.Drive)DriveList.FocusedItem.Tag;
            }

            SelectedDrive = (CLKsFATXLib.Drive)DriveList.FocusedItem.Tag;
            foreach (ListViewItem sDrive in DriveList.Items)
            {
                if (sDrive != DriveList.FocusedItem)
                {
                    ((CLKsFATXLib.Drive)sDrive.Tag).Close();
                }
            }
        }

        private void b_CancelDrive_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem li in DriveList.Items)
            {
                ((CLKsFATXLib.Drive)li.Tag).Close();
            }
        }

        private void DriveList_SelectedIndexChanged(object sender, EventArgs e)
        {
            b_ConfirmDevice.Enabled = true;
        }

        private void DriveList_DoubleClick(object sender, EventArgs e)
        {
            if (DriveList.FocusedItem != null)
            {
                b_ConfirmDevice.PerformClick();
            }
        }

        public CLKsFATXLib.Drive SelectedDrive
        {
            get;
            private set;
        }
    }
}
