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
    public partial class Existing : Form
    {

        List<ParrotLibs.Structs.ExistingEntry> FilesWithFolders;
        List<ParrotLibs.Structs.ExistingEntry> FoldersWithFiles;
        public Existing(List<ParrotLibs.Structs.ExistingEntry> FilesWithFolders, List<ParrotLibs.Structs.ExistingEntry> FoldersWithFiles)
        {
            InitializeComponent();
            listView1.SetExplorerTheme();
            listView2.SetExplorerTheme();
            this.FilesWithFolders = FilesWithFolders;
            this.FoldersWithFiles = FoldersWithFiles;
            this.Load += new EventHandler(Existing_Load);
        }

        void Existing_Load(object sender, EventArgs e)
        {
            foreach (ParrotLibs.Structs.ExistingEntry ex in FilesWithFolders)
            {
                ListViewItem li = new ListViewItem(ex.NewPath);
                li.SubItems.Add(ex.Existing.FullPath);
                listView1.Items.Add(li);
            }

            foreach (ParrotLibs.Structs.ExistingEntry ex in FoldersWithFiles)
            {
                ListViewItem li = new ListViewItem(ex.NewPath);
                li.SubItems.Add(ex.Existing.FullPath);
                listView2.Items.Add(li);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
