using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using System.IO;

namespace NationalSchoolsDataTool
{
    class XMLHelper:MessageInfo
    {
        private static XMLHelper _instance;

        internal static XMLHelper Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new XMLHelper();
                }
                return _instance;
            }
        }

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
        internal  void  WriteObjToXML<T>(T obj, string xmlfolderPath, bool isCreatXML) where T : new()
        {
            if (!isCreatXML) return ;

            Province province = obj as Province;

            MsgEventHandle(string.Format(TipInfos.Begin_XMLWrite, province.LocationName, xmlfolderPath), MessageLV.Default);
             
            if (province == null) throw new Exception(TipInfos.WrongType_XMLWrite);


            if (!Directory.Exists(xmlfolderPath)) throw new Exception(TipInfos.FoldeNotFound_XMLWrite);

            string xmlDirectory = Path.Combine(xmlfolderPath, "XMLFolder");

            if (!Directory.Exists(xmlDirectory))
            {
                Directory.CreateDirectory(xmlDirectory);
            }

            string xmlFilePath = Path.Combine(xmlDirectory, string.Format("{0}.xml", province.LocationName));
            try
            {
                XmlSerializer xs = new XmlSerializer(typeof(T));
                Stream stream = new FileStream(xmlFilePath, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
                xs.Serialize(stream, province);
                stream.Close();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
                throw ex;
            }

        }
    }
}
