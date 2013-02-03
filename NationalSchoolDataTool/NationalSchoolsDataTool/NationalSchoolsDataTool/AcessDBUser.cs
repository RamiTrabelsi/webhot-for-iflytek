using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OleDb;
using System.Data;
using System.Data.Common;

namespace NationalSchoolsDataTool
{
    class AcessDBUser
    {
        private const string ACESSDBPWD = "iflytek_BBT@2012!";

        public static string DBPath { get; set; }

        private static DataSet _villageDS = new DataSet();

        public static DataSet VillageDS
        {
            get { return _villageDS; }
            set { _villageDS = value; }
        }
        /// <summary>
        /// 连接数据库
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        internal static bool InsertDataToDB(Province obj)
        {

            OleDbConnection connection = null;
            OleDbCommand mycmd = null;
            CreatConn(DBPath, ref connection, ref mycmd);

            List<string> cmdString = GetCommandString(obj);
            try
            {
                connection.Open();
                mycmd.Transaction = connection.BeginTransaction(IsolationLevel.ReadCommitted);

                for (int i = 0; i < cmdString.Count; i++)
                {
                    mycmd.CommandText = cmdString[i];
                    mycmd.ExecuteNonQuery();
                }

                mycmd.Transaction.Commit();

                return true;
            }
            catch (Exception ex)
            {
                if (mycmd.Transaction != null)
                    mycmd.Transaction.Rollback();
                return false;
            }
            finally
            {
                //关闭连接 
                if (connection != null && connection.State == ConnectionState.Open)
                    connection.Close();
            }

        }

        public static string QureyFromVillages(string villiageName, string districtID)
        {
            villiageName = villiageName.Length > 1 ? villiageName.Substring(0, 1) : villiageName;
            List<string> sList = new List<string>();
            try
            {

                if (VillageDS.Tables.Count == 0)
                {
                    FillVillageDSFromDB();
                }
                DataView dv = new DataView(VillageDS.Tables[0]);
                dv.RowFilter = "[villagename] like '%" + villiageName + "%'";
                DataTable dt = dv.ToTable();
                foreach (DataRow r in dt.Rows)
                {
                    if (string.Equals(r["districtid"].ToString(), districtID))
                    {
                        sList.Add(r["districtid"].ToString());
                    }
                }
                if (sList.Count != 1)
                {
                    throw new Exception("QureyFromVillages()方法的查询集合Count!=1,有多条数据匹配或没有查到对应的记录.");
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            return sList[0];
        }

        /// <summary>
        /// 查询区域信息,填充到数据集
        /// </summary>
        /// <returns></returns>
        private static void FillVillageDSFromDB()
        {

            OleDbConnection connection = null;
            OleDbCommand mycmd = null;
            CreatConn(DBPath, ref connection, ref mycmd);

            mycmd.CommandText = QueryAllVilliageInfo();
            try
            {
                connection.Open();
                GetDataSet(mycmd);
            }
            catch (Exception ex)
            {
                if (mycmd.Transaction != null)
                    mycmd.Transaction.Rollback();
            }
            finally
            {
                //关闭连接 
                if (connection != null && connection.State == ConnectionState.Open)
                    connection.Close();
            }

        }

        /// <summary>
        /// 查询字符串 :villiage表
        /// </summary>
        /// <returns></returns>
        private static string QueryAllVilliageInfo()
        {
            return "SELECT * FROM [village]";
        }

        private static void CreatConn(string dbFilePath, ref OleDbConnection connection, ref OleDbCommand mycmd)
        {
            if (string.IsNullOrEmpty(dbFilePath)) return;

            string connectionString = string.Format("Provider=Microsoft.Jet.OLEDB.4.0;Persist Security Info=False; Data Source={0};Jet OLEDB:Database Password={1}", dbFilePath, ACESSDBPWD);
            mycmd = new OleDbCommand();

            connection = new OleDbConnection(connectionString);
            mycmd.Connection = connection;
        }


        /// <summary>
        /// 执行数据库查询
        /// </summary>
        /// <param name="cmd"></param>
        private static void GetDataSet(OleDbCommand cmd)
        {
            OleDbDataAdapter dataAdapter = new OleDbDataAdapter(cmd);
            dataAdapter.Fill(VillageDS, "Village");
            cmd.Dispose();
            dataAdapter.Dispose();
        }

        /// <summary>
        /// 获取villageid
        /// </summary>
        /// <param name="villiageName"></param>
        /// <param name="districtID"></param>
        /// <returns></returns>
        internal static string GetVillageIDFromDS(string villiageName, string districtID)
        {
            string queryStr = "SELECT [villageid] FROM [village] where [villagename] like '%{0}%' AND [districtid]={1}";
            string splicStr = string.Format(queryStr, villiageName.Substring(0, 2), districtID);
            return splicStr;
        }

        /// <summary>
        /// 获取插入命令字符串
        /// </summary>
        /// <param name="province"></param>
        /// <returns></returns>
        private static List<string> GetCommandString(Province province)
        {
            List<string> cmdList = new List<string>();
            province.Citys.ForEach((c) =>
            {
                c.Villages.ForEach((v) =>
                {
                    v.Schools.ForEach((s) =>
                    {
                        cmdList.Add(string.Format("INSERT INTO [School]([schoolid],[villageid],[districtid],[schoolname]) VALUES('{0}','{1}','{2}','{3}');",
                                                                     s.SchoolID, s.VilliageID, s.DistrictID, s.SchoolName.Replace('\'', ' ')));
                    });
                });
            });

            return cmdList;
        }

    }
}
