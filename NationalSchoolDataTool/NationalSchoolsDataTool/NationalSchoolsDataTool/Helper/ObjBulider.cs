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
            Province p = AnaliseStrData(data.ToString());
            return p;
        }

        /// <summary>
        /// 解析文件内容
        /// -- 格式 : ##...##Test: Type6 广东##...##  ...  [三级目录]广东;广州;荔湾区;option value="440103"荔湾区   [学校列表]·廣州市荔灣區東沙小學·...
        /// 44 - 省 , 01 - 市 , 03 - 区
        /// </summary>
        /// <param name="strData"></param>
        /// <returns></returns>
        internal static Province AnaliseStrData(string strData)
        {
            strData = UtilsHelper.SelectExcessCondition(strData);

            #region 省份处理

            //解析txt所得到省份信息处理
            string provinceName = UtilsHelper.GetProvinceName(strData);
            string provinceID = UtilsHelper.GetProvinceId(strData, provinceName);

            Province province = new Province() { LocationID = provinceID, LocationName = provinceName };

            #endregion

            //分割内容
            string[] contentsSplit = UtilsHelper.GetContents(strData, @"\[三级目录\]");

            string cityOptions = UtilsHelper.GetContents(contentsSplit[0], @"\[二级目录\]")[1];    //二级目录内容: <option value="1301">石家庄</option><option value="1302">唐山</option>...
            string[] cityInfos = Regex.Split(cityOptions, @"</option>", RegexOptions.IgnoreCase | RegexOptions.Multiline);

            foreach (string cityInfo in cityInfos)
            {
                if (string.IsNullOrEmpty(cityInfo)) continue;

                #region 市级处理

                string cityName = cityInfo.Split('>')[1];
                string cityID = cityInfo.Split('"')[1].PadRight(6, '0'); // 如 : 合肥 340100
                City city = new City() { DistrictID = cityID, DistrictName = cityName, LocationID = provinceID };
                province.Citys.Add(city);

                #endregion

            }

            for (int i = 1; i < contentsSplit.Length; i++)     //三级目录的内容 :  广东;广州;荔湾区;<option value="440103">荔湾区   [学校列表]·廣州市荔灣區東沙小學·...
            {
                try
                {
                    string[] strContents = UtilsHelper.GetContents(contentsSplit[i], @"\[学校列表\]"); //以学校列表分开
                    if (strContents.Length < 2) continue;

                    string[] areaList = UtilsHelper.GetAreaInfo(strContents[0]);    //  strContents[0]:广东;广州;荔湾区;<option value="440103">荔湾区   
                    if (areaList.Length < 2) continue;

                    #region 区域处理


                    //解析txt所得到区域级别处理
                    string villageName = areaList[areaList.Length - 1].Split('>')[1];    // 荔湾区  
                    string villageID = UtilsHelper.GetAreaID(strContents[0]);  //解析txt所得到的ID 如 : 东城区 110101

                    #endregion

                    //解析txt所得到市级信息处理,用于匹配
                    string cityNameByArea = areaList[1];
                    string cityIDByArea = UtilsHelper.GetCityID(strData, cityNameByArea);   //取出来三级目录中的市名称,再匹配二级目录的id就是市级id

                    //根据城市的id和区域的名称去数据库中找区域的名称
                    string vID = AcessDBUser.Instance.QureyIDFromVillageDS(villageName, cityIDByArea);

                    if (!string.IsNullOrEmpty(vID))  //找到的话,赋值给areaID
                    {
                        villageID = vID;
                    }
                    else //找不到,说明数据库中没有此城市对应的区域
                    {
                        //那么,将更新数据库区域列表:将此条区域记录加入数据库中
                        AcessDBUser.Instance.InsertVillageInfoToDB(villageID, villageName, cityIDByArea);
                    }

                    Village village = new Village() { VillageID = villageID, VillageName = villageName, DistrictID = cityIDByArea };

                    #region 学校信息处理

                    //解析txt所得到学校信息处理
                    string[] schoolNames = UtilsHelper.GetSchoolsInfo(strContents[1]);
                    int j = 1;
                    foreach (string schoolName in schoolNames)
                    {
                        try
                        {
                            if (string.IsNullOrEmpty(schoolName)) continue;

                            string shcoolID = string.Format("{0}{1}", villageID, j++.ToString().PadLeft(3, '0'));   // 如 : 安徽蚌埠美佛儿国际学校 340305012

                            School school = new School(shcoolID, villageID, cityIDByArea, schoolName, UtilsHelper.GetSchoolType(strData), string.Empty);

                            village.Schools.Add(school);
                        }
                        catch (System.Exception ex)
                        {
                            ProcessHelper.MsgEventHandle(string.Format("AnaliseStrData(string strData) 错误 : {0} ", ex.InnerException));
                        }
                    }

                    #endregion

                    City city = province.Citys.Find((c) => { return c.DistrictName == cityNameByArea && c.DistrictID == cityIDByArea; });

                    if (city != null)
                    {
                        city.Villages.Add(village);
                    }
                }
                catch (System.Exception ex)
                {
                    ProcessHelper.MsgEventHandle(string.Format("AnaliseStrData(string strData) 错误 : {0} ", ex.InnerException));

                }
            }

            //去除空项
            for (int i = 0; i < province.Citys.Count; i++)
            {
                try
                {
                    for (int j = 0; j < province.Citys[i].Villages.Count; j++)
                    {
                        if (province.Citys[i].Villages[j].Schools == null ||
                          province.Citys[i].Villages[j].Schools.Count == 0)     //移除学校列表为空的
                        {
                            province.Citys.Remove(province.Citys[i]);
                        }
                    }

                    if (province.Citys[i].Villages == null ||
                        province.Citys[i].Villages.Count == 0)  //移除地区列表为空的
                    {
                        province.Citys.Remove(province.Citys[i]);
                    }

                }
                catch (System.Exception ex)
                {
                    ProcessHelper.MsgEventHandle(string.Format("AnaliseStrData(string strData) 错误 : {0} ", ex.InnerException));
                    throw ex;
                }
            }

            return province;

        }

    }
}
