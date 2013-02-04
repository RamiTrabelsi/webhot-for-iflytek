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
        private static AcessDBUser _instance;

        internal static AcessDBUser Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new AcessDBUser();
                }
                return _instance;
            }
            set { _instance = value; }
        }


        private const string ACESSDBPWD = "iflytek_BBT@2012!";

        public static string DBPath { get; set; }

        private DataSet _villageDS = new DataSet();
        private DataSet _schoolDS = new DataSet();

        public DataSet VillageDS
        {
            get { return _villageDS; }
            set { _villageDS = value; }
        }

        public DataSet SchoolDS
        {
            get { return _schoolDS; }
            set { _schoolDS = value; }
        }
        /// <summary>
        /// 向数据库插入省份信息
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool InsertProvinceObjToDB(Province obj)
        {

            OleDbConnection connection = null;
            OleDbCommand mycmd = null;
            CreatConn(DBPath, ref connection, ref mycmd);

            List<string> cmdString = InsertSchoolCmdString(obj);
            try
            {
                connection.Open();
                mycmd.Transaction = connection.BeginTransaction(IsolationLevel.ReadCommitted);

                for (int i = 0; i < cmdString.Count; i++)
                {
                    ProcessHelper.MsgEventHandle(string.Format(",共有数据: {0} 条,当前插入:第 {1} 条,插入数据:{2} ", cmdString.Count, i, cmdString[i]));

                    mycmd.CommandText = cmdString[i];
                    mycmd.ExecuteNonQuery();
                }

                mycmd.Transaction.Commit();

                return true;
            }
            catch (Exception ex)
            {
                ProcessHelper.MsgEventHandle(string.Format("InsertProvinceObjToDB(Province obj) 错误 : {0} ", ex.InnerException));
                System.Windows.Forms.MessageBox.Show(ex.Message);
                if (mycmd.Transaction != null)
                    mycmd.Transaction.Rollback();
                System.Windows.Forms.MessageBox.Show(ex.Message);
                throw ex;
            }
            finally
            {
                //关闭连接 
                if (connection != null && connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }

        /// <summary>
        /// 从数据集中获取区域id
        /// </summary>
        /// <param name="villiageName"></param>
        /// <param name="districtID"></param>
        /// <returns></returns>
        public string QureyIDFromVillageDS(string villiageName, string districtID)
        {
            villiageName = villiageName.Length > 1 ? villiageName.Substring(0, villiageName.Length - 1) : villiageName;
            List<string> sList = new List<string>();
            try
            {
                FillDSFromDB(QueryAllTableInfo("village"), ref _villageDS, "Village");

                DataView dv = new DataView(VillageDS.Tables[0]);
                dv.RowFilter = "[villagename] like '%" + villiageName + "%'";
                DataTable dt = dv.ToTable();
                foreach (DataRow r in dt.Rows)
                {
                    if (string.Equals(r["districtid"].ToString(), districtID))
                    {
                        sList.Add(r["villageid"].ToString());
                    }
                }
                return DBHelper.HandleVilliageQueryList(sList) ? sList[0] : string.Empty; 
            }
            catch (System.Exception ex)
            {
                ProcessHelper.MsgEventHandle(string.Format("QureyIDFromVillageDS(string villiageName, string districtID) 错误 : {0} ", ex.InnerException), MessageLV.High);
                System.Windows.Forms.MessageBox.Show(ex.Message);
                throw ex;
            }
        }

        /// <summary>
        /// 查询区域信息,填充到数据集
        /// </summary>
        /// <returns></returns>
        private void FillDSFromDB(string cmdText, ref DataSet ds, string tableName)
        {
            if (ds.Tables.Count > 0) return;

            OleDbConnection connection = null;
            OleDbCommand mycmd = null;
            CreatConn(DBPath, ref connection, ref mycmd);

            mycmd.CommandText = cmdText;
            try
            {
                connection.Open();
                GetDataSet(mycmd, ref ds, tableName);
            }
            catch (Exception ex)
            {
                ProcessHelper.MsgEventHandle(string.Format("FillVillageDSFromDB() 错误 : {0} ", ex.InnerException), MessageLV.High);

                if (mycmd.Transaction != null)
                    mycmd.Transaction.Rollback();
                System.Windows.Forms.MessageBox.Show(ex.Message);
                throw ex;
            }
            finally
            {
                //关闭连接 
                if (connection != null && connection.State == ConnectionState.Open)
                    connection.Close();
            }

        }

        /// <summary>
        /// 查询字符串 :[tableName]表
        /// </summary>
        /// <returns></returns>
        private string QueryAllTableInfo(string tableName)
        {
            return "SELECT * FROM [" + tableName + "]";
        }

        private void CreatConn(string dbFilePath, ref OleDbConnection connection, ref OleDbCommand mycmd)
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
        private void GetDataSet(OleDbCommand cmd, ref DataSet ds, string tableName)
        {
            OleDbDataAdapter dataAdapter = new OleDbDataAdapter(cmd);
            dataAdapter.Fill(ds, tableName);           
            cmd.Dispose();
            dataAdapter.Dispose();
        }

        /// <summary>
        /// 获取villageid
        /// </summary>
        /// <param name="villiageName"></param>
        /// <param name="districtID"></param>
        /// <returns></returns>
        private static string GetVillageIDFromDS(string villiageName, string districtID)
        {
            string queryStr = "SELECT [villageid] FROM [village] where [villagename] like '%{0}%' AND [districtid]={1}";
            string splicStr = string.Format(queryStr, villiageName.Substring(0, 2), districtID);
            return splicStr;
        }

        /// <summary>
        /// 获取插入学校命令字符串
        /// </summary>
        /// <param name="province"></param>
        /// <returns></returns>
        private static List<string> InsertSchoolCmdString(Province province)
        {
            List<string> cmdList = new List<string>();
            province.Citys.ForEach((c) =>
            {
                c.Villages.ForEach((v) =>
                {
                    v.Schools.ForEach((s) =>
                    {
                        cmdList.Add(string.Format("INSERT INTO [School]([schoolid],[villageid],[districtid],[schoolname],[schoolprop]) VALUES('{0}','{1}','{2}','{3}',{4});",
                                                                     s.SchoolID, s.VilliageID, s.DistrictID, s.SchoolName.Replace('\'', ' ')));
                    });
                });
            });

            return cmdList;
        }

        /// <summary>
        /// 插入新的区域记录
        /// </summary>
        /// <param name="villageID"></param>
        /// <param name="villageName"></param>
        /// <param name="cityIDByArea"></param>
        /// <returns></returns>
        internal void InsertVillageInfoToDB(string villageID, string villageName, string cityIDByArea)
        {
            OleDbConnection connection = null;
            OleDbCommand mycmd = null;
            CreatConn(DBPath, ref connection, ref mycmd);

            string cmdString = string.Format("INSERT INTO  [Village]([villageid],[villagename],[districtid]) VALUES('{0}','{1}','{2}');", villageID, villageName, cityIDByArea);
            try
            {
                connection.Open();
                mycmd.Transaction = connection.BeginTransaction(IsolationLevel.ReadCommitted);

                mycmd.CommandText = cmdString;
                mycmd.ExecuteNonQuery();

                mycmd.Transaction.Commit();

            }
            catch (Exception ex)
            {
                ProcessHelper.MsgEventHandle(string.Format("InsertVillageInfoToDB(string villageID, string villageName, string cityIDByArea) 错误 : {0} ", ex.InnerException), MessageLV.High);
                System.Windows.Forms.MessageBox.Show(ex.Message);
                if (mycmd.Transaction != null)
                    mycmd.Transaction.Rollback();
                throw ex;
            }
            finally
            {
                //关闭连接 
                if (connection != null && connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }

        internal bool QuerySchoolFromDBByCondition(string schoolName, string villageID)
        {
            schoolName = schoolName.Length > 2 ? schoolName.Substring(0, schoolName.Length - 2) : schoolName;
            List<string> sList = new List<string>();
            try
            {

                FillDSFromDB(QueryAllTableInfo("school"), ref _schoolDS, "school");

                DataView dv = new DataView(_schoolDS.Tables[0]);
                dv.RowFilter = "[schoolname] like '%" + schoolName.Replace('\'', ' ').Trim() + "%'";
                DataTable dt = dv.ToTable();
                foreach (DataRow r in dt.Rows)
                {
                    if (string.Equals(r["villageid"].ToString(), villageID))
                    {
                        sList.Add(r["villageid"].ToString());
                    }
                }
                return DBHelper.HandleSchoolQueryList(sList);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
                ProcessHelper.MsgEventHandle(string.Format("QureyIDFromVillageDS(string villiageName, string districtID) 错误 : {0} ", ex.InnerException), MessageLV.High);
                throw ex;
            }
        }

        internal void ClearVillageDS()
        {
            VillageDS.Clear();
        }

        internal void ClearSchoolDS()
        {
            SchoolDS.Clear();
        }
    }
}
