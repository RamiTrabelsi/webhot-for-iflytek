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
        /// 遍历文件列表,返回文件路径
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>文件流文档</returns>
        internal static string LoadFileData(string filePath)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 建立实体-xml映射关系
        /// </summary>
        /// <param name="fileData">文件流</param>
        /// <returns></returns>
        internal static EntityBase BuildObject(string fileData)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 写xml文档
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        internal static bool WriteObjToXML<T>(T obj) where T : ISerializable, new()
        {
            throw new NotImplementedException();
        }
    }
}
