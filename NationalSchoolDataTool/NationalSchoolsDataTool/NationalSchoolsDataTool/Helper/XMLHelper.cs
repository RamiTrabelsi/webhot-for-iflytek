using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using System.IO;

namespace NationalSchoolsDataTool
{
    class XMLHelper
    {
        /// <summary>
        /// 遍历文件列表,返回文件流文档
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>文件流文档</returns>
        internal static StringBuilder LoadFileData(string filePath)
        {
            if (!UtilsHelper.CheckFileExistOrFilter(filePath, ".txt")) return null;

            return UtilsHelper.ReadFileFromPath(filePath);
        }

        /// <summary>
        /// 建立实体-xml映射关系,写xml文档
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">对象</param>
        /// <param name="xmlfolderPath">文件夹路径</param>
        /// <returns></returns>
        internal static bool WriteObjToXML<T>(T obj, string xmlfolderPath) where T : new()
        {
            
            Province province = obj as Province;

            if (province == null) throw new Exception("类型错误!在: WriteObjToXML<T>(T obj)方法");

          
            if (!Directory.Exists(xmlfolderPath)) throw new Exception("文件夹不存在! : WriteObjToXML<T>(T obj)方法");
           
            string xmlFilePath = Path.Combine(new DirectoryInfo(xmlfolderPath).Parent.FullName, string.Format("{0}.xml", province.LocationName));
            try
            {
                XmlSerializer xs = new XmlSerializer(typeof(T));
                Stream stream = new FileStream(xmlFilePath, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
                xs.Serialize(stream, province);
                stream.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return true;
        }


    }
}
