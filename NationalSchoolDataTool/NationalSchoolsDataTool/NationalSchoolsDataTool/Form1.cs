using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Threading;

namespace NationalSchoolsDataTool
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            MessageHelper.MsgEvent += new EventHandler(Instance_MsgEvent);
        }

        
        private void btnSelectFolder_Click(object sender, EventArgs e)
        {
            ProcessHelper.Instance.IniOpeart(folderBrowserDialog1, checkBox1.Checked, checkBox2.Checked);
        }

        void Instance_MsgEvent(object sender, EventArgs e)
        {

        }
    }
}
