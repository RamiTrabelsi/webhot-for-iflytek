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
            List<string> fileList = UtilsHelper.SelectFolder(UtilsHelper.ReadFolderPath(folderBrowserDialog1));

            if (!UtilsHelper.CheckFileList(fileList)) return;

            fileList.ForEach((filePath) =>
            {
                Province obj = ObjBulider.CreateProvinceObject(XMLHelper.LoadFileData(filePath));

                //读取文件数据,生成xml文件,然后将xml文件映射到数据库中
                if (XMLHelper.WriteObjToXML(obj))
                {
                    AcessDBUser.WirteObjectToAcess(obj);
                }

            });
        }
    }
}
