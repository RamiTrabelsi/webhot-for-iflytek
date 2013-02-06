using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace NationalSchoolsDataTool
{
    class UtilsHelper
    {

        /// <summary>
        /// 读取文件路径
        /// </summary>
        /// <param name="diaglog"></param>
        /// <returns></returns>
        internal static string ReadFolderPath(System.Windows.Forms.FolderBrowserDialog diaglog)
        {
            return diaglog != null && diaglog.ShowDialog() == System.Windows.Forms.DialogResult.OK ? diaglog.SelectedPath : string.Empty;
        }

        /// <summary>
        /// 读取文件列表
        /// </summary>
        /// <param name="folderPath">文件夹路径</param>
        /// <returns>文件夹下文件列表</returns>
        internal static List<string> SelectFolder(string folderPath)
        {
            List<string> strlist = new List<string>();
            if (!CheckDirectoryExist(folderPath)) return null;

            FileInfo[] fileInfos = new DirectoryInfo(folderPath).GetFiles();

            foreach (FileInfo info in fileInfos)
            {
                if (info.Extension.EndsWith(".txt"))
                {
                    strlist.Add(info.FullName);
                }
            }

            strlist.Sort();

            return strlist;
        }

        /// <summary>
        /// 检测列表是否满足条件
        /// </summary>
        /// <param name="fileList"></param>
        /// <returns></returns>
        internal static bool CheckFileList(List<string> fileList)
        {
            return fileList != null && fileList.Count > 0;
        }

        /// <summary>
        /// 从路径读取文件
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        internal static StringBuilder ReadFileFromPath(string filePath)
        {
            return GetFileContent(filePath);
        }

        /// <summary>
        /// 从路径中读取txt文档的文件流
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private static StringBuilder GetFileContent(string path)
        {
            StringBuilder sb = new StringBuilder();
            try
            {
                FileStream aFile = new FileStream(path, FileMode.Open);
                StreamReader sr = new StreamReader(aFile);
                string strLine = sr.ReadLine();

                while (strLine != null)
                {
                    sb.AppendLine(strLine);
                    strLine = sr.ReadLine();
                }

                sr.Close();
            }
            catch (IOException ex)
            {
                ProcessHelper.MsgEventHandle(string.Format("GetFileContent 发生错误: {0}", ex.InnerException));
                System.Windows.Forms.MessageBox.Show(ex.Message);
                throw ex;
            }

            return sb;
        }

        /// <summary>
        /// 检查文件夹是否存在
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        internal static bool CheckDirectoryExist(string filePath)
        {
            return !string.IsNullOrEmpty(filePath) && Directory.Exists(filePath);
        }

        /// <summary>
        /// 检查文件是否满足条件
        /// </summary>
        /// <param name="filePath">路径</param>
        /// <param name="filter">后缀</param>
        /// <returns></returns>
        internal static bool CheckFileExistOrFilter(string filePath, string filter)
        {
            if (!string.IsNullOrEmpty(filePath) &&
                !string.IsNullOrEmpty(filter) &&
                File.Exists(filePath))
            {
                return new FileInfo(filePath).Name.TrimEnd().EndsWith(filter);
            }

            return false;
        }


        /// <summary>
        /// 筛选多余的信息
        /// </summary>
        /// <param name="strData"></param>
        /// <returns></returns>
        public static string SelectExcessCondition(string strData)
        {
            //将换行符,"人气最高","该地区暂时没有收录学校"和"其他"过滤
            strData = Regex.Replace(strData, "\\r\\n", string.Empty, RegexOptions.IgnoreCase | RegexOptions.Multiline);
            strData = Regex.Replace(strData, "人气最高", string.Empty, RegexOptions.IgnoreCase | RegexOptions.Multiline);
            strData = Regex.Replace(strData, "该地区暂时没有收录学校", string.Empty, RegexOptions.IgnoreCase | RegexOptions.Multiline);
            strData = Regex.Replace(strData, "其他", string.Empty, RegexOptions.IgnoreCase | RegexOptions.Multiline);

            return strData;
        }



        /// <summary>
        /// 获取省份id  
        /// 格式 :  option value="44"广东/option
        /// </summary>
        /// <param name="strData"></param>
        /// <param name="provinceName"></param>
        /// <returns></returns>
        public static string GetProvinceId(string strData, string provinceName)
        {
            string provincePattern = @"<\w+\s+\w+..\d+..>" + provinceName + @"<\/\w+>";  // <option value="44">广东</option>
            string provinceID = Regex.Match(strData, provincePattern, RegexOptions.IgnoreCase | RegexOptions.Multiline).Value.Split('"')[1];  //44
            return provinceID.PadRight(6, '0'); // 如 : 北京 110000
        }

        /// <summary>
        /// 获取省份名称
        /// </summary>
        /// <param name="strData"></param>
        /// <returns></returns>
        public static string GetProvinceName(string strData)
        {
            string provincePattern = @"Test:\sType\d\s(.[^#]+)";  //Test: Type6 广东
            string provinceName = Regex.Match(strData, provincePattern, RegexOptions.IgnoreCase | RegexOptions.Multiline).Value.Split(' ')[2];  //广东
            return provinceName;
        }


        /// <summary>
        /// 获取市级ID
        /// </summary>
        /// <param name="strData"></param>
        /// <param name="cityName"></param>
        /// <returns></returns>
        public static string GetCityID(string strData, string cityName)
        {
            string cityPattern = @"<\w+\s+\w+..\d+..>" + cityName + @"<\/\w+>";  // <option value="4401">广州</option>
            string cityID = Regex.Match(strData, cityPattern, RegexOptions.IgnoreCase | RegexOptions.Multiline).Value.Split('"')[1];  //4401
            return cityID.PadRight(6, '0');
        }


        /// <summary>
        /// 处理地区信息
        /// 格式 :  ·廣州市荔灣區東沙小學·...
        /// </summary>
        /// <param name="strContents"></param>
        public static string[] GetSchoolsInfo(string strContents)
        {
            return strContents.Split('·');

        }

        /// <summary>
        /// 处理学校信息
        /// 格式: 广东;广州;荔湾区;option value="440103"荔湾区
        /// </summary>
        /// <param name="strContents"></param>
        public static string[] GetAreaInfo(string strContents)
        {
            string realContents = strContents.Trim();    //生成: 广东;广州;荔湾区;<option value="440103">荔湾区
            return realContents.Split(';');
        }

        /// <summary>
        /// 处理地区id
        /// </summary>
        /// <param name="strContents"></param>
        public static string GetAreaID(string strContents)
        {
            Match match = Regex.Match(strContents, @"<\w+\s+\w+..\d+..>", RegexOptions.IgnoreCase | RegexOptions.Multiline);  //匹配<option value="440103">内容

            return match.Value.Split('"')[1];   // 获取 440103

        }

        /// <summary>
        /// 获取以指定格式分割的字符串数组,如果是三级目录内容以#开头
        /// </summary>
        /// <param name="strData"></param>
        /// <param name="partten"></param>
        /// <returns></returns>
        public static string[] GetContents(string strData, string partten)
        {
            if (string.Equals(partten, @"\[三级目录\]") && !strData.StartsWith("#")) return null;
            string[] tempStrs = Regex.Split(strData, partten, RegexOptions.IgnoreCase | RegexOptions.Multiline);

            List<string> strs = new List<string>();
            foreach (string s in tempStrs)
            {
                if (!string.IsNullOrEmpty(s))
                    strs.Add(s.Trim());
            }

            string[] result = strs.ToArray();

            return result; //获取三级目录
        }

        /// <summary>
        /// 获取数据库文件路径
        /// </summary>
        /// <returns></returns>
        public static string GetDBPath()
        {
            System.Windows.Forms.OpenFileDialog dialog = new System.Windows.Forms.OpenFileDialog();
            dialog.CheckFileExists = true;
            dialog.AddExtension = true;
            dialog.Filter = "Acess数据库文件|*.mdb";
            dialog.Title = "打开Acess数据库";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                return dialog.FileName;
            }
            else
            {

                System.Windows.Forms.MessageBox.Show("操作取消.");
            }
            return string.Empty;
        }

        /// <summary>
        /// 获取学校类型
        /// </summary>
        /// <param name="strData"></param>
        /// <returns>大学,小学,中学</returns>
        internal static string GetSchoolType(string strData)
        {
            string schoolPattern = @"Type\d";  //Test: Type6 
            string schoolName = Regex.Match(strData, schoolPattern, RegexOptions.IgnoreCase | RegexOptions.Multiline).Value;  //广东
            string typeNum = Regex.Match(schoolName, @"\d").Value;
            string schoolType = string.Empty;
            switch (typeNum)
            {
                case "1":
                case "2":
                case "3":
                case "7":
                default:
                    schoolType = "大学";
                    break;
                case "4":
                case "5":
                case "8":
                    schoolType = "中学";
                    break;
                case "6":
                    schoolType = "小学";
                    break;
            }

            return schoolType;
        }

        /// <summary>
        /// 处理特殊符号
        /// </summary>
        /// <param name="schoolNames"></param>
        /// <returns></returns>
        internal static string HandleSpecialSymbol(string schoolNames)
        {
            return LanguageTextHelper.GetChCharsAndNumsWithoutPunctuation(schoolNames).Trim();
        }

        /// <summary>
        ///// 获取学校
        /// </summary>
        /// <param name="cityOptions"></param>
        /// <returns></returns>
        internal static string[] HandleCityInfos(string cityOptions)
        {
            return Regex.Split(cityOptions, @"</option>", RegexOptions.IgnoreCase | RegexOptions.Multiline);
        }

        /// <summary>
        /// 是否是直辖市
        /// </summary>
        /// <returns></returns>
        internal static bool IsMuitCity(string provinceName)
        {
            provinceName = provinceName.Trim();

            return string.Equals(provinceName, "北京") ||
                            string.Equals(provinceName, "上海") ||
                            string.Equals(provinceName, "天津") ||
                            string.Equals(provinceName, "重庆");
        }


    }
}
