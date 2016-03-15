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
    public partial class CustomBackup : Form
    {
        public CustomBackup()
        {
            InitializeComponent();
            sBackupTypeList.SetExplorerTheme();
            sBackupTypeList.ContextMenu = contextMenu1;

            for (int i = 0; i < ParrotLibs.VariousFunctions.Known.Length; i++)
            {
                ListViewItem sListItem = new ListViewItem(ParrotLibs.VariousFunctions.KnownEquivilent[i] + " (" + ParrotLibs.VariousFunctions.Known[i] + ")");
                sListItem.BackColor = Color.White;
                sListItem.UseItemStyleForSubItems = true;
                sListItem.Tag = ParrotLibs.VariousFunctions.Known[i];
                sBackupTypeList.Items.Add(sListItem);
            }
        }

        private void b_BeginBackup_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            List<string> sToSkipItemList = new List<string>();
            foreach (ListViewItem sListItem in sBackupTypeList.Items)
            {
                if (sListItem.Checked == true)
                {
                    sToSkipItemList.Add((string)sListItem.Tag);
                }
            }
            FoldersToSkip = sToSkipItemList.ToArray();
            this.Close();
        }

        public string[] FoldersToSkip
        {
            get;
            set;
        }

        private void m_AllCheck_Click(object sender, EventArgs e)
        {
            foreach(ListViewItem sListItem in sBackupTypeList.Items)
            {
                sListItem.Checked = true;
            }
        }

        private void m_AllUncheck_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem sListItem in sBackupTypeList.Items)
            {
                sListItem.Checked = false;
            }
        }
    }
}
