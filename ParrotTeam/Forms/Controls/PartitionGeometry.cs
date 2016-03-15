using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ParrotLibs.Structs;

namespace Party_Buffalo.Forms
{
    public partial class PartitionGeometry : UserControl
    {
        string text = "";
        public PartitionGeometry(ParrotLibs.Folder e)
        {
            InitializeComponent();
            ParrotLibs.Structs.PartitionInfo PI = e.PartitionInfo;
            propertyGrid1.SelectedObject = PI;
        }
    }
}
