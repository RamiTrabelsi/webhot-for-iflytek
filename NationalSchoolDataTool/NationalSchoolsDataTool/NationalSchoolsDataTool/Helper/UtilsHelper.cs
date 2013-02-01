using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

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
            return diaglog != null && diaglog.ShowDialog() == System.Windows.Forms.DialogResult.OK ? diaglog.SelectedPath : "";
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
                strlist.Add(info.FullName);
            }
            return strlist;
        }

        /// <summary>
        /// 坚持列表是否满足条件
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
    }
}
