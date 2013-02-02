using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OleDb;
using System.Data;


namespace NationalSchoolsDataTool
{
    class AcessDBUser
    {
        /// <summary>
        /// 连接数据库
        /// </summary>
        /// <param name="dbFilePath"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        internal static bool ConnectDB(string dbFilePath, Province obj)
        {
            return true;
            if (string.IsNullOrEmpty(dbFilePath))
            {
                return false;
            }
            OleDbConnection connection = null;
            string connectionString = string.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0}", dbFilePath);
            string cmdString = GetCommandString(CommanType.Insert, obj);
            try
            {
                connection = new OleDbConnection(connectionString);

                OleDbCommand cmd = new OleDbCommand(cmdString, connection);

                connection.Open();
                cmd.ExecuteNonQuery();

                return true;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (connection != null)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                }
            }
        }

        private static string GetCommandString(CommanType type,Province province)
        {
            string cmdStr = string.Empty;

            cmdStr = "INSERT INTO [School]() VALUES()";

            return cmdStr;
        }

        private enum CommanType
        {
            Insert,
            Update,
            Delete

        }
    }
}
