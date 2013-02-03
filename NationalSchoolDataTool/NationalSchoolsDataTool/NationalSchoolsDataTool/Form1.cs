using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;

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
            //选择文件夹后,遍历文件夹下文件,生成文件路径
            string folderPath = UtilsHelper.ReadFolderPath(folderBrowserDialog1);
            if (string.IsNullOrEmpty(folderPath)) return;

            AcessDBUser.DBPath = UtilsHelper.GetDBPath();

            if (string.IsNullOrEmpty(AcessDBUser.DBPath)) return;

            List<string> fileList = UtilsHelper.SelectFolder(folderPath);

            if (!UtilsHelper.CheckFileList(fileList)) return;

            fileList.ForEach((filePath) =>
            {
                try
                {
                    Province obj = ObjBulider.CreateProvinceObject(XMLHelper.LoadFileData(filePath));

                    //读取文件数据,生成xml文件,然后将xml文件映射到数据库中
                    if (checkBox1.Checked)
                        XMLHelper.WriteObjToXML(obj, folderPath);

                    //if (!string.Equals(obj.LocationName, "安徽"))
                    //{

                    if (AcessDBUser.InsertDataToDB(obj))
                    {
                        MessageBox.Show("完成!");
                    }
                    else
                    {
                        //失败提示
                    }
                    // }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    throw ex;
                }

            });
        }

    }
}
