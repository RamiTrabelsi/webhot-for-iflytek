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
        internal static bool HandleVilliageQueryList(List<string> sList)
        {
            if (sList.Count == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        /// <summary>
        /// 处理school的查询结果是否唯一
        /// </summary>
        /// <param name="sList"></param>
        internal static bool HandleSchoolQueryList(List<string> sList)
        {
            if (sList.Count == 0)
            {
                return true;
            }
            else  
            {
                return false;
            }
            
        }
    }
}
