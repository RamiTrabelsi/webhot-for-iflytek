using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Drawing;

namespace NationalSchoolsDataTool
{
    class ProcessHelper:MessageInfo
    {
        private string _folderPath = string.Empty;

        public string FolderPath
        {
            get
            {
                if (string.IsNullOrEmpty(_folderPath))
                {
                    _folderPath = UtilsHelper.ReadFolderPath();
                }
                return _folderPath;
            }
        }

        private List<string> _fileList;

        public List<string> FileList
        {
            get
            {
                if (_fileList == null)
                {
                    _fileList = UtilsHelper.SelectFolder(FolderPath);
                }
                return _fileList;
            }
        }


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
        private static string msg = string.Empty;


        private bool _isCreateXML = false;

        private bool _isGenerateDBData = false;

        /// <summary>
        /// 操作初始化
        /// </summary> 
        /// <param name="isCreateXML"></param>
        /// <param name="isGenerateDBData"></param>
        public void IniOpeart(bool isCreateXML, bool isGenerateDBData)
        {
            this._isCreateXML = isCreateXML;
            this._isGenerateDBData = isGenerateDBData;

            if (string.IsNullOrEmpty(FolderPath))
            {
                MsgEventHandle("选择文件夹取消,操作取消 .");
                return;
            }

            if (string.IsNullOrEmpty(AcessDBUser.DBPath))
            {
                MsgEventHandle("选择数据库文件取消,操作取消.");
                return;
            }

            if (FileList == null || FileList.Count == 0)
            {
                MsgEventHandle("文件下,不存在txt格式文件,操作取消.", MessageLV.Mid);
                return;
            }
             
            StartProcess();

        }

        /// <summary>
        /// 开始数据读写操作
        /// </summary>
        private void StartProcess()
        {  
            FileList.ForEach((filePath) =>
            {
                try
                {
                    Province obj = ObjBulider.Instance.GetProvince(XMLHelper.LoadFileData(filePath));

                    if (obj.Citys.Count != 0)
                    {
                        XMLHelper.Instance.WriteObjToXML(obj, FolderPath,_isCreateXML);

                        AcessDBUser.Instance.InsertProvinceObjToDB(obj, _isGenerateDBData);
                    }
                    else
                    {
                        MsgEventHandle(string.Format(TipInfos.Contains_No_CityData, obj.LocationName));
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });

            MsgEventHandle(TipInfos.Opeart_Finished);
        }


     
    }

}
