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
    public partial class Cache : Form
    {
        /// <summary>
        /// 
        /// </summary>
        public Cache()
        {
            InitializeComponent();
            GameCacheList.SetExplorerTheme();
            GameCacheList.ContextMenu = contextMenu1;
            try
            {
                for (int i = 0; i < Properties.Settings.Default.CachedID.Count; i++)
                {
                    ListViewItem sListItem = new ListViewItem(Properties.Settings.Default.CachedID[i]);
                    ListViewItem.ListViewSubItem sListSubItem = new ListViewItem.ListViewSubItem(sListItem, Properties.Settings.Default.CachedIDName[i]);
                    sListItem.SubItems.Add(sListSubItem);
                    GameCacheList.Items.Add(sListItem);
                }
            }
            catch
            {
                if (Properties.Settings.Default.CachedID == null)
                {
                    Properties.Settings.Default.CachedID = new System.Collections.Specialized.StringCollection();
                    Properties.Settings.Default.CachedIDName = new System.Collections.Specialized.StringCollection();
                    Properties.Settings.Default.Save();
                }
            }

            if (Properties.Settings.Default.Label == null)
            {
                Properties.Settings.Default.Label = new System.Collections.Specialized.StringCollection();
                Properties.Settings.Default.LabelPath = new System.Collections.Specialized.StringCollection();
                Properties.Settings.Default.Save();
            }

            for (int i = 0; i < Properties.Settings.Default.Label.Count; i++)
            {
                ListViewItem sListItem = new ListViewItem(Properties.Settings.Default.LabelPath[i]);
                ListViewItem.ListViewSubItem sListSubItem = new ListViewItem.ListViewSubItem(sListItem, Properties.Settings.Default.Label[i]);
                sListItem.SubItems.Add(sListSubItem);
                sListItem.Tag = true;
                GameCacheList.Items.Add(sListItem);
            }
        }

        private void cb_AllCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_AllCheck.Checked)
            {
                foreach (ListViewItem sListItem in GameCacheList.Items)
                {
                    sListItem.Checked = true;
                }
            }
            else
            {
                foreach (ListViewItem sListItem in GameCacheList.Items)
                {
                    sListItem.Checked = false;
                }
            }
        }
        
        private void b_CleanCache_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.CachedID = null;
            Properties.Settings.Default.CachedIDName = null;
            Properties.Settings.Default.Label = null;
            Properties.Settings.Default.LabelPath = null;
            Properties.Settings.Default.Save();
            GameCacheList.Items.Clear();
        }

        private void b_RemoveItem_Click(object sender, EventArgs e)
        {
            for(int i = 0; i < GameCacheList.Items.Count; i++)
            {
                if (GameCacheList.Items[i].Checked)
                {
                    if (GameCacheList.Items[i].Tag == null)
                    {
                        Properties.Settings.Default.CachedID.RemoveAt(i);
                        Properties.Settings.Default.CachedIDName.RemoveAt(i);
                        Properties.Settings.Default.Save();
                    }
                    else
                    {
                        Properties.Settings.Default.LabelPath.Remove(GameCacheList.Items[i].Text);
                        Properties.Settings.Default.Label.Remove(GameCacheList.Items[i].SubItems[1].Text);
                        Properties.Settings.Default.Save();
                    }
                    GameCacheList.Items[i].Remove();
                    i--;
                }
            }
        }

        private void b_SaveToTxt_Click(object sender, EventArgs e)
        {
            SaveFileDialog sSaveFileDialog = new SaveFileDialog();
            sSaveFileDialog.Filter = "Text Document(*.txt)|*.txt";
            if (sSaveFileDialog.ShowDialog() == DialogResult.OK)
            {
                System.IO.BinaryWriter sBinaryWriter;
                if (System.IO.File.Exists(sSaveFileDialog.FileName) == true)
                {
                    sBinaryWriter = new System.IO.BinaryWriter(new System.IO.FileStream(sSaveFileDialog.FileName, System.IO.FileMode.Append));
                }
                else
                {
                    sBinaryWriter = new System.IO.BinaryWriter(new System.IO.FileStream(sSaveFileDialog.FileName, System.IO.FileMode.OpenOrCreate));
                }

                for (int i = 0; i < Properties.Settings.Default.CachedID.Count; i++)
                {
                    byte[] toWrite = Encoding.ASCII.GetBytes("Title ID: " + Properties.Settings.Default.CachedID[i] + " Game Name: " + Properties.Settings.Default.CachedIDName[i] + "\r\n");
                    sBinaryWriter.Write(toWrite);
                }
                sBinaryWriter.Close();

                MessageBox.Show("文本保存成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void m_Copy_Click(object sender, EventArgs e)
        {

            if (GameCacheList.SelectedItems.Count == 1)
            {
                Clipboard.SetDataObject("Title ID: " + GameCacheList.SelectedItems[0].SubItems[1].Text + " Game Name: " + GameCacheList.SelectedItems[0].Text, true, 5, 250);
            }
        }

        private void m_Remove_Click(object sender, EventArgs e)
        {
            // lolcheat
            b_RemoveItem.PerformClick();
        }
    }
}
