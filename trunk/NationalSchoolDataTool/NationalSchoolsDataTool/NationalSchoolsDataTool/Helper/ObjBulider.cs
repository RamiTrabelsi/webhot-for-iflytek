using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace NationalSchoolsDataTool
{
    class ObjBulider:MessageInfo
    {
        private static ObjBulider _instance;

        internal static ObjBulider Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ObjBulider();
                }
                return _instance;
            }
        }

        /// <summary>
        /// 创建省的实体
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        internal  Province GetProvince(StringBuilder data)
        {
            Province p = AnaliseStrData(data.ToString());
            if (p.Citys.Count==0)
            {
                MsgEventHandle(string.Format(TipInfos.Create_ProvinceModle_Success, p.LocationName));

            } 
            return p;
        }

        /// <summary>
        /// 解析文件内容
        /// -- 格式 : ##...##Test: Type6 广东##...##  ...  [三级目录]广东;广州;荔湾区;option value="440103"荔湾区   [学校列表]·廣州市荔灣區東沙小學·...
        /// 44 - 省 , 01 - 市 , 03 - 区
        /// </summary>
        /// <param name="strProvinceData"></param>
        /// <returns></returns>
        internal static Province AnaliseStrData(string strProvinceData)
        {
            strProvinceData = UtilsHelper.SelectExcessCondition(strProvinceData);   //处理多余信息

            string schoolType = UtilsHelper.GetSchoolType(strProvinceData); //获取学校类型:大,中,小学

            return AnaliseChinaDatas(strProvinceData, schoolType);

        }

        /// <summary>
        /// 处理省份信息
        /// </summary>
        /// <param name="strProvinceData"></param>
        /// <param name="schoolType"></param>
        /// <returns></returns>
        private static Province AnaliseChinaDatas(string strProvinceData, string schoolType)
        {
            Province province = null;

            bool isMuitCity = false;

            CreateProvinceModle(strProvinceData,
                                schoolType,
                                out province,
                                out isMuitCity);

            RemoveInvalidDatas(province, isMuitCity);

            return province;
        }

        /// <summary>
        /// 创建省的实体
        /// </summary>
        /// <param name="strProvinceData"></param>
        /// <param name="schoolType"></param>
        /// <param name="province"></param>
        /// <param name="isMuitCity"></param>
        private static void CreateProvinceModle(string strProvinceData, string schoolType, out Province province, out bool isMuitCity)
        {
            #region 省份处理
            //解析txt所得到省份信息处理
            string provinceName = UtilsHelper.GetProvinceName(strProvinceData);
            string provinceID = UtilsHelper.GetProvinceId(strProvinceData, provinceName);

            province = new Province() { LocationID = provinceID, LocationName = provinceName };
            #endregion

            //分割内容
            string[] contentsSplit = UtilsHelper.GetContents(strProvinceData, TipInfos.ThirdContent);

            //直辖市
            isMuitCity = HandleMunicipalities(provinceName, provinceID, province, contentsSplit);

            for (int i = 1; i < contentsSplit.Length; i++)     //三级目录的内容 :  广东;广州;荔湾区;<option value="440103">荔湾区   [学校列表]·廣州市荔灣區東沙小學·...
            {
                try
                {
                    string[] strContents = UtilsHelper.GetContents(contentsSplit[i], TipInfos.School_List); //以学校列表分开
                    if (strContents.Length < 2) continue;

                    string[] areaList = UtilsHelper.GetAreaInfo(strContents[0]);    //  strContents[0]:广东;广州;荔湾区;<option value="440103">荔湾区   
                    if (areaList.Length < 2) continue;

                    CreatVillageModle(strProvinceData, schoolType, province, isMuitCity, strContents, areaList);
                }
                catch (System.Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.Message);
                    throw ex;
                }
            }
        }

        private static bool HandleMunicipalities(string provinceName, string provinceID, Province province, string[] contentsSplit)
        {
            bool isMuitCity = false;

            if (UtilsHelper.IsMuitCity(provinceName))   //直辖市处理
            {
                isMuitCity = true;

                City city1 = new City() { DistrictID = string.Format("{0}0100", provinceID.Substring(0, 2)), DistrictName = "市辖区", LocationID = provinceID };
                City city2 = new City() { DistrictID = string.Format("{0}0200", provinceID.Substring(0, 2)), DistrictName = "县", LocationID = provinceID };

                province.Citys.Add(city1);
                province.Citys.Add(city2);
            }
            else  //普通省市处理
            {
                string cityOptions = UtilsHelper.GetContents(contentsSplit[0], TipInfos.SecondaryContent)[1];
                string[] cityInfos = UtilsHelper.HandleCityInfos(cityOptions);   //二级目录内容: <option value="1301">石家庄</option><option value="1302">唐山</option>...

                foreach (string cityInfo in cityInfos)
                {
                    if (string.IsNullOrEmpty(cityInfo)) continue;

                    #region 市级处理

                    string cityName = cityInfo.Split('>')[1];   //处理 <option value="1301">石家庄</option>
                    string cityID = cityInfo.Split('"')[1].PadRight(6, '0'); // 如 : 合肥 340100
                    City city = new City() { DistrictID = cityID, DistrictName = cityName, LocationID = provinceID };
                    province.Citys.Add(city);

                    //因为数据库中每个县都有冗余的"市辖区",判断是否是直辖市,如果不是直辖市,则删除
                    AcessDBUser.Instance.DeleteInvideVillageInfos(cityID);

                    #endregion
                }
            }
            return isMuitCity;
        }

        /// <summary>
        /// 过滤无效信息(如:省份下没有城市,城市下没有地区,地区下没有学校)
        /// </summary>
        /// <param name="province"></param>
        /// <param name="isMuitCity"></param>
        private static void RemoveInvalidDatas(Province province, bool isMuitCity)
        {
            try
            {
                //移除多余信息
                for (int i = province.Citys.Count - 1; i >= 0; i--)
                {
                    for (int j = province.Citys[i].Villages.Count - 1; j >= 0; j--)
                    {
                        if (province.Citys[i].Villages[j].Schools.Count == 0 || (!isMuitCity && province.Citys[i].Villages[j].VillageName == "市辖区"))     //移除学校列表为空的
                        {
                            province.Citys[i].Villages.Remove(province.Citys[i].Villages[j]);

                        }
                    }

                    if (province.Citys[i].Villages.Count == 0)  //移除地区列表为空的
                    {
                        province.Citys.Remove(province.Citys[i]);
                    }
                }
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
               throw ex;
            }
        }

        private static void CreatVillageModle(string strProvinceData, string schoolType, Province province, bool isMuitCity, string[] strContents, string[] areaList)
        {
            #region 区域处理
            string cityNameByArea;
            string cityIDByArea;
            Village village;
            CreateVillageModle(strProvinceData, schoolType, isMuitCity, strContents, areaList, out cityNameByArea, out cityIDByArea, out village);

            #endregion

            City city = province.Citys.Find((c) => { return c.DistrictName == cityNameByArea && c.DistrictID == cityIDByArea; });

            if (city != null)
            {
                city.Villages.Add(village);
            }
        }

        private static void CreateVillageModle(string strProvinceData, string schoolType, bool isMuitCity, string[] strContents, string[] areaList, out string cityNameByArea, out string cityIDByArea, out Village village)
        {
            //解析txt所得到区域级别处理
            string villageName = areaList[areaList.Length - 1].Split('>')[1].Replace(" ", string.Empty);    // 荔湾区  
            string villageID = UtilsHelper.GetAreaID(strContents[0]);  //解析txt所得到的ID 如 : 东城区 110101

            //解析txt所得到市级信息处理,用于匹配
            cityNameByArea = areaList[1];
            cityIDByArea = UtilsHelper.GetCityID(strProvinceData, cityNameByArea);   //取出来三级目录中的市名称,再匹配二级目录的id就是市级id

            //根据城市的id和区域的名称去数据库中找区域的名称
            if (isMuitCity)
            {
                UtilsHelper.DealMuitCityArea(villageName, ref cityNameByArea, ref cityIDByArea);
            }

            //从数据库中获取villiageid,找不到则插入
            string vID = AcessDBUser.Instance.QureyIDFromVillageDS(villageName, cityIDByArea, isMuitCity);

            if (!string.IsNullOrEmpty(vID))  //找到的话,赋值给areaID
            {
                villageID = vID;
            }
            else //找不到,说明数据库中没有此城市对应的区域
            {
                //那么,将更新数据库区域列表:将此条区域记录加入数据库中
                AcessDBUser.Instance.InsertVillageInfoToDB(villageID, villageName, cityIDByArea);

                AcessDBUser.Instance.ClearVillageDS();
            }

            village = new Village() { VillageID = villageID, VillageName = villageName, DistrictID = cityIDByArea };

            #region 学校信息处理

            CreatSchoolModle(schoolType, strContents, villageID, cityIDByArea, village);
            #endregion
        }

        private static void CreatSchoolModle(string schoolType, string[] strContents, string villageID, string cityIDByArea, Village village)
        {
            //解析txt所得到学校信息处理
            string[] schoolNames = UtilsHelper.GetSchoolsInfo(strContents[1]);

            int j = AcessDBUser.Instance.ReadMaxShcoolID(villageID);

            foreach (string schoolName in schoolNames)
            {
                try
                {
                    //删除特殊符号
                    string dealSchoolName = UtilsHelper.HandleSpecialSymbol(schoolName).Trim();

                    if (string.IsNullOrEmpty(dealSchoolName)) continue;
                    j++;
                    string shcoolID = string.Format("{0}{1}", villageID, j.ToString().PadLeft(3, '0'));   // 如 : 安徽蚌埠美佛儿国际学校 340305012

                    School school = new School(shcoolID, villageID, cityIDByArea, dealSchoolName, schoolType, string.Empty);

                    //直接插入插入,因为判断的话太耗时了
                    village.Schools.Add(school);
                }
                catch (System.Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.Message);
                   throw ex;
                }
            }
        }

    }
}