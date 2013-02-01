using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace NationalSchoolsDataTool
{
    /// <summary>
    /// 可序列化实体基类
    /// </summary>
    class EntityBase : ISerializable
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            throw new NotImplementedException();
        }
    }
}
