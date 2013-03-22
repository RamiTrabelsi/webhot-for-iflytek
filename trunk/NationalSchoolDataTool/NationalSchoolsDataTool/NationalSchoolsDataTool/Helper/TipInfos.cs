using System;
using System.Collections.Generic;
using System.Text;

namespace NationalSchoolsDataTool
{
    class TipInfos
    {
        public const string Create_ProvinceModle_Success = @"创建 '{0}'省 实体成功.";
        public const string Contains_No_CityData = @"{0} 的城市下没有相关数据.";
        public const string Opeart_Failed = @"操作失败 - {0}";
        public const string Opeart_Finished = @"完成!";
        public const string Begin_XMLWrite = @"开始将 '{0}'省 数据实体写入到 '{1}\\XML文件'.";
        public const string WrongType_XMLWrite = @"类型错误!在: WriteObjToXML<T>(T obj)方法";
        public const string FoldeNotFound_XMLWrite = @"文件夹不存在! : WriteObjToXML<T>(T obj)方法";
        public const string ThirdContent = @"\[三级目录\]";
        public const string School_List = @"\[学校列表\]";
        public const string SecondaryContent = @"\[二级目录\]";
        public const string CurrentRecord = @"共有数据: {0} 条,当前插入:第 {1} 条,插入数据:{2} ";
    }
}
