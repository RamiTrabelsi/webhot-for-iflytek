using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace NationalSchoolsDataTool
{
    class ProcessHelper
    {
        private static ProcessHelper _instance;

        internal static ProcessHelper Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ProcessHelper();
                }
                return _instance;
            }
        }

        /// <summary>
        /// 操作初始化
        /// </summary>
        /// <param name="folderBrowserDialog1"></param>
        /// <param name="isCreateXML"></param>
        /// <param name="isGenerateDBData"></param>
        public void IniOpeart(FolderBrowserDialog folderBrowserDialog1, bool isCreateXML, bool isGenerateDBData)
        {
            string folderPath = UtilsHelper.ReadFolderPath(folderBrowserDialog1);    //选择文件夹后,遍历文件夹下文件,生成文件路径
            if (string.IsNullOrEmpty(folderPath))
            {
                MessageHelper.ShowMessage("选择文件夹取消,操作取消.", MessageLV.Mid);
                return;
            }

            AcessDBUser.DBPath = UtilsHelper.GetDBPath();     //读取数据库文件路径
            if (string.IsNullOrEmpty(AcessDBUser.DBPath))
            {
                MessageHelper.ShowMessage("选择数据库文件取消,操作取消.", MessageLV.Mid);
                return;
            }

            List<string> fileList = UtilsHelper.SelectFolder(folderPath);
            if (!UtilsHelper.CheckFileList(fileList))
            {
                MessageHelper.ShowMessage("文件下,不存在txt格式文件,操作取消.", MessageLV.Mid);
                return;
            }

            Thread thread = new Thread(new ThreadStart(() =>
               {
                   MessageHelper.ShowMessage("开始数据读写操作.", MessageLV.Default);

                   StartProcess(isCreateXML, isGenerateDBData, folderPath, fileList);
               }));
            thread.Start();
        }

        /// <summary>
        /// 开始数据读写操作
        /// </summary>
        /// <param name="isCreateXML"></param>
        /// <param name="isCreateDBData"></param>
        /// <param name="folderPath"></param>
        /// <param name="fileList"></param>
        private void StartProcess(bool isCreateXML, bool isCreateDBData, string folderPath, List<string> fileList)
        {
            fileList.ForEach((filePath) =>
            {
                try
                {
                    Province obj = ObjBulider.CreateProvinceObject(XMLHelper.LoadFileData(filePath));

                    MessageHelper.ShowMessage(string.Format("创建 '{0}'省 实体成功.", obj.LocationName), MessageLV.Default);

                    //读取文件数据,生成xml文件,然后将xml文件映射到数据库中
                    if (isCreateXML)
                    {
                        MessageHelper.ShowMessage(string.Format("开始将 '{0}'省 数据实体写入到 '{1}\\XML文件'.", obj.LocationName, folderPath), MessageLV.Default);

                        if (XMLHelper.WriteObjToXML(obj, folderPath))
                        {
                            MessageHelper.ShowMessage("数据实体写入成功.", MessageLV.Default);
                        }
                        else
                        {
                            MessageHelper.ShowMessage("数据实体写入失败.", MessageLV.High);
                        }
                    }

                    //if (!string.Equals(obj.LocationName, "安徽"))
                    //{

                    if (isCreateDBData && AcessDBUser.InsertProvinceObjToDB(obj))
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
                    MessageHelper.ShowMessage(string.Format("操作失败 - {0}", ex.InnerException), MessageLV.High);
                    MessageBox.Show(ex.Message);
                }

            });
        }

        
    }


}
