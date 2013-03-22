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
        }

        private void btnSelectFolder_Click(object sender, EventArgs e)
        {
            bool isCreateXML = checkBox1.Checked;
            bool isCreatDB = checkBox2.Checked;
            try
            {
                ProcessHelper.Instance.ShowMessaged += new MsgHandle(Instance_ShowMessaged);

                ProcessHelper.Instance.IniOpeart(isCreateXML, isCreatDB);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void Instance_ShowMessaged(string e, string messLV, params object[] args)
        {
            try
            {
                BeginInvoke(new Action<string>((msg) =>
                {
                    label2.Text = e;
                }));
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

    }
}
