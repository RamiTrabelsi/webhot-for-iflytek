using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace NationalSchoolsDataTool
{
    class ObjBulider
    {

        /// <summary>
        /// 创建省的实体
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        internal static Province CreateProvinceObject(StringBuilder data)
        { 
            Province p = UtilsHelper.AnaliseStrData(data.ToString());
            return p;
        }
    }
}
