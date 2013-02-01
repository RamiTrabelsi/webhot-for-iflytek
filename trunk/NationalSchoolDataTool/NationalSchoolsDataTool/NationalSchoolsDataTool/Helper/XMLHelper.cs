using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Runtime.Serialization;

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
            if (!UtilsHelper.CheckFileExistOrFilter(filePath,".txt")) return null;

           return UtilsHelper.ReadFileFromPath(filePath);
        }


        /// <summary>
        /// 建立实体-xml映射关系,写xml文档
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        internal static bool WriteObjToXML<T>(T obj) where T : new()
        {
            throw new NotImplementedException();
        }
    }
}
