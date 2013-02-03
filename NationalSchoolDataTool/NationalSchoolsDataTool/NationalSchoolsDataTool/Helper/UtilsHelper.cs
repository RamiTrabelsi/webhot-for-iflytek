using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace NationalSchoolsDataTool
{
    class UtilsHelper
    {

        /// <summary>
        /// 读取文件路径
        /// </summary>
        /// <param name="diaglog"></param>
        /// <returns></returns>
        internal static string ReadFolderPath(System.Windows.Forms.FolderBrowserDialog diaglog)
        {
            return diaglog != null && diaglog.ShowDialog() == System.Windows.Forms.DialogResult.OK ? diaglog.SelectedPath : string.Empty;
        }

        /// <summary>
        /// 读取文件列表
        /// </summary>
        /// <param name="folderPath">文件夹路径</param>
        /// <returns>文件夹下文件列表</returns>
        internal static List<string> SelectFolder(string folderPath)
        {
            List<string> strlist = new List<string>();
            if (!CheckDirectoryExist(folderPath)) return null;

            FileInfo[] fileInfos = new DirectoryInfo(folderPath).GetFiles();

            foreach (FileInfo info in fileInfos)
            {
                strlist.Add(info.FullName);
            }

            strlist.Sort();

            return strlist;
        }

        /// <summary>
        /// 检测列表是否满足条件
        /// </summary>
        /// <param name="fileList"></param>
        /// <returns></returns>
        internal static bool CheckFileList(List<string> fileList)
        {
            return fileList != null && fileList.Count > 0;
        }

        /// <summary>
        /// 从路径读取文件
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        internal static StringBuilder ReadFileFromPath(string filePath)
        {
            return GetFileContent(filePath);
        }

        /// <summary>
        /// 从路径中读取txt文档的文件流
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private static StringBuilder GetFileContent(string path)
        {
            StringBuilder sb = new StringBuilder();
            try
            {
                FileStream aFile = new FileStream(path, FileMode.Open);
                StreamReader sr = new StreamReader(aFile);
                string strLine = sr.ReadLine();

                while (strLine != null)
                {
                    sb.AppendLine(strLine);
                    strLine = sr.ReadLine();
                }

                sr.Close();
            }
            catch (IOException ex)
            {
                throw ex;
            }

            return sb;
        }

        /// <summary>
        /// 检查文件夹是否存在
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        internal static bool CheckDirectoryExist(string filePath)
        {
            return !string.IsNullOrEmpty(filePath) && Directory.Exists(filePath);
        }

        /// <summary>
        /// 检查文件是否满足条件
        /// </summary>
        /// <param name="filePath">路径</param>
        /// <param name="filter">后缀</param>
        /// <returns></returns>
        internal static bool CheckFileExistOrFilter(string filePath, string filter)
        {
            if (!string.IsNullOrEmpty(filePath) &&
                !string.IsNullOrEmpty(filter) &&
                File.Exists(filePath))
            {
                return new FileInfo(filePath).Name.TrimEnd().EndsWith(filter);
            }

            return false;
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
            strData = SelectExcessCondition(strData);

            #region 省份处理

            //省份信息处理
            string provinceName = GetProvinceName(strData);
            string provinceID = GetProvinceId(strData, provinceName);

            Province province = new Province() { LocationID = provinceID, LocationName = provinceName };

            #endregion

            //分割内容
            string[] contentsSplit = GetContents(strData, @"\[三级目录\]");

            string cityOptions = GetContents(contentsSplit[0], @"\[二级目录\]")[1];    //二级目录内容: <option value="1301">石家庄</option><option value="1302">唐山</option>...
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
                    string[] strContents = GetContents(contentsSplit[i], @"\[学校列表\]"); //以学校列表分开
                    if (strContents.Length < 2) continue;

                    string[] areaList = GetAreaInfo(strContents[0]);    //  strContents[0]:广东;广州;荔湾区;<option value="440103">荔湾区   
                    if (areaList.Length < 2) continue;

                    #region 区域处理


                    //区域级别处理
                    string villageName = areaList[areaList.Length - 1].Split('>')[1];    // 荔湾区  
                    string villageID = GetAreaID(strContents[0]);  //解析txt所得到的ID 如 : 东城区 110101

                    #endregion

                    //市级信息处理,用于匹配
                    string cityNameByArea = areaList[1];
                    string cityIDByArea = GetCityID(strData, cityNameByArea);   //取出来三级目录中的市名称,再匹配二级目录的id就是市级id

                    //根据城市的id和区域的名称去数据库中找区域的名称
                    string vID = AcessDBUser.QureyIDFromVillageDS(villageID, cityIDByArea);
                     
                    if (!string.IsNullOrEmpty(vID))  //找到的话 赋值给areaID
                    {
                        villageID = vID;
                    }
                    else //找不到,说明数据库中没有此城市对应的区域
                    {
                        //那么,将更新数据库区域列表:将此条区域记录加入数据库中
                        AcessDBUser.InsertVillageInfoToDB(villageID,villageName, cityIDByArea);
                    }

                    Village village = new Village() { VillageID = villageID, VillageName = villageName, DistrictID = cityIDByArea };

                    #region 学校信息处理

                    //学校信息处理
                    string[] schoolNames = GetSchoolsInfo(strContents[1]);
                    int j = 1;
                    foreach (string schoolName in schoolNames)
                    {
                        try
                        {
                            if (string.IsNullOrEmpty(schoolName)) continue;

                            string shcoolID = string.Format("{0}{1}", villageID, j++.ToString().PadLeft(3, '0'));   // 如 : 安徽蚌埠美佛儿国际学校 340305012

                            School school = new School(shcoolID, villageID, cityIDByArea, schoolName, string.Empty, string.Empty);

                            village.Schools.Add(school);
                        }
                        catch (System.Exception ex)
                        {
                            throw ex;
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
                    throw ex;
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
                    throw ex;
                }
            }

            return province;

        }

        /// <summary>
        /// 筛选多余的信息
        /// </summary>
        /// <param name="strData"></param>
        /// <returns></returns>
        private static string SelectExcessCondition(string strData)
        {
            //将换行符,"人气最高","该地区暂时没有收录学校"和"其他"过滤
            strData = Regex.Replace(strData, "\\r\\n", string.Empty, RegexOptions.IgnoreCase | RegexOptions.Multiline);
            strData = Regex.Replace(strData, "人气最高", string.Empty, RegexOptions.IgnoreCase | RegexOptions.Multiline);
            strData = Regex.Replace(strData, "该地区暂时没有收录学校", string.Empty, RegexOptions.IgnoreCase | RegexOptions.Multiline);
            strData = Regex.Replace(strData, "其他", string.Empty, RegexOptions.IgnoreCase | RegexOptions.Multiline);

            return strData;
        }



        /// <summary>
        /// 获取省份id  
        /// 格式 :  option value="44"广东/option
        /// </summary>
        /// <param name="strData"></param>
        /// <param name="provinceName"></param>
        /// <returns></returns>
        private static string GetProvinceId(string strData, string provinceName)
        {
            string provincePattern = @"<\w+\s+\w+..\d+..>" + provinceName + @"<\/\w+>";  // <option value="44">广东</option>
            string provinceID = Regex.Match(strData, provincePattern, RegexOptions.IgnoreCase | RegexOptions.Multiline).Value.Split('"')[1];  //44
            return provinceID.PadRight(6, '0'); // 如 : 北京 110000
        }

        /// <summary>
        /// 获取省份名称
        /// </summary>
        /// <param name="strData"></param>
        /// <returns></returns>
        private static string GetProvinceName(string strData)
        {
            string provincePattern = @"Test:\sType\d\s(.[^#]+)";  //Test: Type6 广东
            string provinceName = Regex.Match(strData, provincePattern, RegexOptions.IgnoreCase | RegexOptions.Multiline).Value.Split(' ')[2];  //广东
            return provinceName;
        }


        /// <summary>
        /// 获取市级ID
        /// </summary>
        /// <param name="strData"></param>
        /// <param name="cityName"></param>
        /// <returns></returns>
        private static string GetCityID(string strData, string cityName)
        {
            string cityPattern = @"<\w+\s+\w+..\d+..>" + cityName + @"<\/\w+>";  // <option value="4401">广州</option>
            string cityID = Regex.Match(strData, cityPattern, RegexOptions.IgnoreCase | RegexOptions.Multiline).Value.Split('"')[1];  //4401
            return cityID.PadRight(6, '0');
        }


        /// <summary>
        /// 处理地区信息
        /// 格式 :  ·廣州市荔灣區東沙小學·...
        /// </summary>
        /// <param name="strContents"></param>
        private static string[] GetSchoolsInfo(string strContents)
        {
            return strContents.Split('·');

        }

        /// <summary>
        /// 处理学校信息
        /// 格式: 广东;广州;荔湾区;option value="440103"荔湾区
        /// </summary>
        /// <param name="strContents"></param>
        private static string[] GetAreaInfo(string strContents)
        {
            string realContents = strContents.Trim();    //生成: 广东;广州;荔湾区;<option value="440103">荔湾区
            return realContents.Split(';');
        }

        /// <summary>
        /// 处理地区id
        /// </summary>
        /// <param name="strContents"></param>
        private static string GetAreaID(string strContents)
        {
            Match match = Regex.Match(strContents, @"<\w+\s+\w+..\d+..>", RegexOptions.IgnoreCase | RegexOptions.Multiline);  //匹配<option value="440103">内容

            return match.Value.Split('"')[1];   // 获取 440103

        }

        /// <summary>
        /// 获取以指定格式分割的字符串数组,如果是三级目录内容以#开头
        /// </summary>
        /// <param name="strData"></param>
        /// <param name="partten"></param>
        /// <returns></returns>
        private static string[] GetContents(string strData, string partten)
        {
            if (string.Equals(partten, @"\[三级目录\]") && !strData.StartsWith("#")) return null;
            string[] tempStrs = Regex.Split(strData, partten, RegexOptions.IgnoreCase | RegexOptions.Multiline);

            List<string> strs = new List<string>();
            foreach (string s in tempStrs)
            {
                if (!string.IsNullOrEmpty(s))
                    strs.Add(s.Trim());
            }

            string[] result = strs.ToArray();

            return result; //获取三级目录
        }

        /// <summary>
        /// 获取数据库文件路径
        /// </summary>
        /// <returns></returns>
        internal static string GetDBPath()
        {
            System.Windows.Forms.OpenFileDialog dialog = new System.Windows.Forms.OpenFileDialog();
            dialog.CheckFileExists = true;
            dialog.AddExtension = true;
            dialog.Filter = "Acess数据库文件|*.mdb";
            dialog.Title = "打开Acess数据库";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                return dialog.FileName;
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("操作取消.");
            }
            return string.Empty;
        }
    }
}
