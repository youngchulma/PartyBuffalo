using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Party_Buffalo.Forms
{
    public partial class AddLabel : Form
    {
        public AddLabel(string Path, string FolderName)
        {
            InitializeComponent();
            t_FolderPath.Text = Path;
            t_FolderName.Text = FolderName;
        }
    
        public string Label
        {
            get
            {
                return t_FolderName.Text;
            }
        }

        private void b_Confirm_Click(object sender, EventArgs e)
        {
            if (Party_Buffalo.Cache.CheckCache(t_FolderPath.Text) != null)
            {
                if (MessageBox.Show(t_FolderName.Text, "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk) == DialogResult.OK)
                {
                    for (int i = 0; i < Properties.Settings.Default.Label.Count; i++)
                    {
                        if (Properties.Settings.Default.LabelPath[i].ToLower() == t_FolderPath.Text.ToLower())
                        {
                            Properties.Settings.Default.Label[i] = t_FolderName.Text;
                            Properties.Settings.Default.Save();
                            break;
                        }
                    }
                }
            }
            else
            {
                if (MessageBox.Show(t_FolderName.Text, "", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk) == DialogResult.OK)
                {
                    Party_Buffalo.Cache.AddLabel(t_FolderName.Text, t_FolderPath.Text);
                }
            }
        }

        private void b_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (t_FolderPath.Text == "" || t_FolderName.Text == "")
            {
                b_Confirm.Enabled = false;
            }
            else
            {
                b_Confirm.Enabled = true;
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (t_FolderPath.Text == "" || t_FolderName.Text == "")
            {
                b_Confirm.Enabled = false;
            }
            else
            {
                b_Confirm.Enabled = true;
            }
        }
    }
}
