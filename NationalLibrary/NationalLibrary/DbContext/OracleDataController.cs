using NationalLibrary.Model;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NationalLibrary.DbContext
{
    public class OracleDataController
    {
        public static OracleConnection conn = null;
        public static string GetDBConnection(string host, int port, string sid, string user, string password)
        {
            // 'Connection String' connects to Oracle.
            string connString = "Data Source=(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = "
                 + host + ")(PORT = " + port + "))(CONNECT_DATA = (SERVER = DEDICATED)(SERVICE_NAME = "
                 + sid + ")));Password=" + password + ";User ID=" + user;
            return connString;
        }

        public static string GetDBConnection()
        {
            var host = "localhost";
            var port = 1521;
            var sid = "orcl12c";
            var userName = "huy";
            var pass = "Pa$$w0rd";
            return GetDBConnection(host, port, sid, userName, pass);
        }

        public static void ConnectDB(string connectionString)
        {
            conn = new OracleConnection
            {
                ConnectionString = connectionString
            };
            conn.Open();
        }

        public static void DisconnectDB()
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            else
            {
                conn.Dispose();
            }
        }

        public static void ExcuteQuery(string query)
        {
            try
            {
                OracleCommand oracleCommand = new OracleCommand(query, conn);
                oracleCommand.ExecuteNonQuery();
                DisconnectDB();
            }
            catch (Exception ex)
            {
                throw new Exception("Error: " + ex.Message);
            }
        }

        public static void Insert(string query)
        {
            try
            {
                ExcuteQuery(query);
            }
            catch (Exception e)
            {
                throw new Exception("Insert Error: " + e.Message);
            }
        }

        public static void Update(string query)
        {
            try
            {
                ExcuteQuery(query);
            }
            catch (Exception e)
            {
                throw new Exception("Update Error: " + e.Message);
            }
        }

        public static void Delete(string query)
        {
            try
            {
                ExcuteQuery(query);
            }
            catch (Exception e)
            {
                throw new Exception("Delete Error: " + e.Message);
            }
        }

        public static DataTable GetDatabase(string strQuery)
        {
            OracleDataAdapter oba = new OracleDataAdapter(strQuery, conn);
            DataTable dt = new DataTable();
            oba.Fill(dt);
            return dt;
        }

        public static DataTable GetDataGridView(string query)
        {
            DataTable dt = GetDatabase(query);
            return dt;
        }

        public static DataTable ExecuteStoredProcForDataTable(StoredProcedure storedProcedure)
        {
            DataTable dt = new DataTable();

            if (string.IsNullOrEmpty(storedProcedure.Name) || !storedProcedure.ListParamsOut.Any())
            {
                return dt;
            }

            try
            {
                var cmd = ExecuteStoredProc(storedProcedure);
                if (cmd == null)
                {
                    return dt;
                }
                var cursorResult = cmd.Parameters[storedProcedure.ListParamsOut[0].ParamName];
                using (OracleDataReader reader = ((OracleRefCursor)cursorResult.Value).GetDataReader())
                {
                    if (reader.HasRows)
                    {
                        dt.Load(reader);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Executre store procedure Error: " + ex.Message);
            }

            return dt;
        }

        public static string ExecuteStoredProcForResult(StoredProcedure storedProcedure)
        {
            var str = string.Empty;

            if (string.IsNullOrEmpty(storedProcedure.Name) || !storedProcedure.ListParamsOut.Any())
            {
                return str;
            }

            try
            {
                var cmd = ExecuteStoredProc(storedProcedure);
                if (cmd == null)
                {
                    return str;
                }
                return cmd == null ? str : cmd.Parameters[storedProcedure.ListParamsOut[1].ParamName].Value.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception("Executre store procedure Error: " + ex.Message);
            }
        }

        public static OracleCommand ExecuteStoredProc(StoredProcedure storedProcedure)
        {
            try
            {
                OracleCommand cmd = conn.CreateCommand();

                foreach (var item in storedProcedure.ListParamsIn)
                {
                    List<OracleParameter> paramIn = new List<OracleParameter>()
                    {
                        new OracleParameter
                        {
                            ParameterName = item.ParamName,
                            Direction = ParameterDirection.Input,
                            OracleDbType = item.ParamType,
                            Value = item.ParamValue
                        }
                    };

                    cmd.Parameters.AddRange(paramIn.ToArray());
                }

                foreach (var item in storedProcedure.ListParamsOut)
                {
                    List<OracleParameter> paramOut = new List<OracleParameter>()
                    {
                        new OracleParameter
                        {
                            ParameterName = item.ParamName,
                            Direction = item.ParamDirection,
                            OracleDbType = item.ParamType,
                            Size = 32767
                        }
                    };
                    cmd.Parameters.AddRange(paramOut.ToArray());
                }

                cmd.CommandText = storedProcedure.Name;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();

                return cmd;
            }
            catch (Exception ex)
            {
                throw new Exception("Executre store procedure Error: " + ex.Message);
            }
        }
    }
}
