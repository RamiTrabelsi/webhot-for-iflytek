using System;
using System.Collections.Generic;
using System.Text;

namespace NationalSchoolsDataTool
{
    public class DBHelper
    {
        /// <summary>
        /// 处理village的查询结果是否唯一
        /// </summary>
        /// <param name="sList"></param>
        internal static bool HandleQueryList(List<string> sList)
        {
            if (sList.Count == 0)
            {
                return false;
            }
            else if (sList.Count > 1)
            {
                ProcessHelper.MsgEventHandle("HandleQueryList 错误 : QureyFromVillages()方法的查询集合Count!=1,有多条数据匹配或没有查到对应的记录. ");
                
                throw new Exception("QureyFromVillages()方法的查询集合Count!=1,有多条数据匹配或没有查到对应的记录.");
            }
            return true;
        }

    }
}
