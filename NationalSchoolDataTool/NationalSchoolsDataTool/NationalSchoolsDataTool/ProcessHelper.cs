using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Drawing;

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
        private static string msg = string.Empty;

        public static Label MesContainer { get; set; }

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
                ProcessHelper.MsgEventHandle("选择文件夹取消,操作取消 .");
                return;
            }

            //MessageHelper.ShowMessage(string.Format("文件夹选择 : {0}.", folderPath), MessageLV.Mid);

            AcessDBUser.DBPath = UtilsHelper.GetDBPath();     //读取数据库文件路径
            if (string.IsNullOrEmpty(AcessDBUser.DBPath))
            {
                ProcessHelper.MsgEventHandle("选择数据库文件取消,操作取消.");
                return;
            }

            //MessageHelper.ShowMessage(string.Format("数据库文件选择: {0}.", AcessDBUser.DBPath), MessageLV.Default);

            List<string> fileList = UtilsHelper.SelectFolder(folderPath);
            if (!UtilsHelper.CheckFileList(fileList))
            {
                ProcessHelper.MsgEventHandle("文件下,不存在txt格式文件,操作取消.", MessageLV.Mid);
                return;
            }

            //MessageHelper.ShowMessage("开始数据读写操作.", MessageLV.Default);

            //StartProcess(isCreateXML, isGenerateDBData, folderPath, fileList);

            StartProcessHandler handler = new StartProcessHandler(StartProcess);
            IAsyncResult result = handler.BeginInvoke(isCreateXML, isGenerateDBData, folderPath, fileList, new AsyncCallback(CallBack), "AsycState:OK");

            while (!result.IsCompleted)
            {
                MesContainer.Text = msg;
                Thread.Sleep(1000);
                continue;
            }
        }

        static void CallBack(IAsyncResult result)
        {
            StartProcessHandler handler = (StartProcessHandler)((System.Runtime.Remoting.Messaging.AsyncResult)result).AsyncDelegate;
            handler.EndInvoke(result);
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
                    if (obj.Citys.Count != 0)
                    {
                        ProcessHelper.MsgEventHandle(string.Format("创建 '{0}'省 实体成功.", obj.LocationName), MessageLV.Default);

                        //读取文件数据,生成xml文件,然后将xml文件映射到数据库中
                        if (isCreateXML)
                        {
                            ProcessHelper.MsgEventHandle(string.Format("开始将 '{0}'省 数据实体写入到 '{1}\\XML文件'.", obj.LocationName, folderPath), MessageLV.Default);

                            if (XMLHelper.WriteObjToXML(obj, folderPath))
                            {
                                ProcessHelper.MsgEventHandle("数据实体写入成功.", MessageLV.Default);
                            }
                            else
                            {
                                ProcessHelper.MsgEventHandle("数据实体写入失败.", MessageLV.High);
                            }
                        }

                        if (isCreateDBData && AcessDBUser.Instance.InsertProvinceObjToDB(obj))
                        {
                            MessageBox.Show("完成!");
                        }
                        else
                        {
                            MessageBox.Show("失败!");
                            //失败提示
                        }
                    }
                    else
                    {
                        MessageBox.Show(string.Format("{0} 的城市下没有相关数据.",obj.LocationName));
                    }
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.Message);
                    MsgEventHandle(string.Format("操作失败 - {0}", ex.InnerException), MessageLV.High);
                    throw ex;
                }

            });
        }


        public static void MsgEventHandle(string e, MessageLV lv = MessageLV.Default)
        {

            msg = e;
            //Label lbl = new Label();
            //lbl.Text = args.Message;
            //lbl.Font = new System.Drawing.Font(FontFamily.GenericMonospace, 9);
            //lbl.Dock = DockStyle.Top;
            //if (args != null)
            //{
            //    switch (args.MessageLV)
            //    {
            //        case MessageLV.Default:
            //            lbl.ForeColor = System.Drawing.Color.LightGreen;
            //            break;
            //        case MessageLV.Low:
            //            lbl.ForeColor = System.Drawing.Color.Black;
            //            break;
            //        case MessageLV.Mid:
            //            lbl.ForeColor = System.Drawing.Color.Orange;
            //            break;
            //        case MessageLV.High:
            //            lbl.ForeColor = System.Drawing.Color.Red;
            //            break;
            //        default:
            //            break;
            //    }
            //MesContainer.Invoke(new Action<Label>((l) => { 
            //    MesContainer.Controls.Add(l);
            //}), lbl); 
        }

    }

    public enum MessageLV
    {
        Low = 0,
        Default,
        Mid,
        High,

    }

    public delegate void StartProcessHandler(bool isCreateXML, bool isCreateDBData, string folderPath, List<string> fileList);

}
