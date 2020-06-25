using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NationalLibrary.DbContext
{
    public class DataProvider
    {
        private static OracleConnection _con = null;

        public static string connect(string host, int port, string sid, string user, string password)
        {
            try
            {
                // 'Connection String' kết nối trực tiếp tới Oracle.
                string connString = "Data Source=(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = "
                     + host + ")(PORT = " + port + "))(CONNECT_DATA = (SERVER = DEDICATED)(SERVICE_NAME = "
                     + sid + ")));Password=" + password + ";User ID=" + user + ";DBA Privilege=SYSDBA";
                _con = new OracleConnection(connString);
                _con.Open();
                _con.Close();
                return string.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }
        public static DataTable ExecuteQuery(string strQuery)
        {
            DataTable resTable = new DataTable();
            try
            {
                _con.Open();
                OracleDataAdapter adapter = new OracleDataAdapter(strQuery, _con);

                adapter.Fill(resTable);
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                _con.Close();
            }

            return resTable;
        }
        //Số dòng được thực thi:Insert,Delete,Update
        public static int ExecuteQueryIDU(string strquery)
        {
            int data = 0;
            //Cho du van de gi nua khi ket thuc khoi lenh thi tu giai phong
            try
            {
                _con.Open();
                OracleCommand cmd = new OracleCommand(strquery, _con);
                data = cmd.ExecuteNonQuery();
                _con.Close();
            }
            catch (Exception ex)
            {
                data = 0;
                _con.Close();

            }
            return data;
        }
        public static DataTable ExecuteStoreProc(string storeProcName, IList<string> arrParameterName, ArrayList arrParameterValue)
        {
            DataTable resTable = new DataTable();

            try
            {
                _con.Open();

                OracleCommand cmd = _con.CreateCommand();
                cmd.CommandText = storeProcName;
                cmd.CommandType = CommandType.StoredProcedure;

                //Lay so parameter
                int N = arrParameterName.Count;
                for (int i = 0; i < N; i++)
                {
                    OracleParameter OracleParam = new OracleParameter(arrParameterName[i], arrParameterValue[i]);
                    cmd.Parameters.Add(OracleParam);
                }

                OracleDataAdapter adapter = new OracleDataAdapter(cmd);
                adapter.Fill(resTable);

                _con.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Loi khi thuc thi store procedure: " + ex.Message);
            }

            return resTable;
        }

        //Không trả về giá trị 
        public static void ExecuteStoreProc(string storeProcName, List<OracleParameter> listinput)
        {
            _con.Open();
            OracleCommand Cmd = _con.CreateCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = storeProcName;
            int len = listinput.Count;
            for (int i = 0; i < len; i++)
            {
                Cmd.Parameters.Add(listinput[i]);
            }
            Cmd.ExecuteNonQuery();
            _con.Close();
        }

        // Trả về một giá trị trong ASP.NET
        public static object ExecuteStoreProc(string storeProcName, List<OracleParameter> listinput, OracleParameter output)
        {
            _con.Open();
            OracleCommand Cmd = _con.CreateCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = storeProcName;
            int len = listinput.Count;
            for (int i = 0; i < len; i++)
            {
                Cmd.Parameters.Add(listinput[i]);
            }
            Cmd.Parameters.Add(output);
            Cmd.ExecuteNonQuery();
            _con.Close();
            return output.Value;
        }
        // Trả về 1 bảng không có tham số nhận vào 
        public static DataTable ExecuteQueryTable(string query, object[] parameter = null)
        {
            DataTable dt = new DataTable();
            //Cho du van de gi nua khi ket thuc khoi lenh thi tu giai phong
            _con.Open();
            OracleCommand command = new OracleCommand(query, _con);
            if (parameter != null)
            {
                string[] listPara = query.Split(' ');
                int i = 0;
                foreach (string item in listPara)
                {
                    if (item.Contains('@'))
                    {
                        command.Parameters.Add(item, parameter[i]);
                        i++;

                    }
                }
            }
            OracleDataAdapter adapter = new OracleDataAdapter(command);

            adapter.Fill(dt);
            _con.Close();
            return dt;
        }
        //Tra ve 1 bang có nhận tham số @ vào bảng
        public static DataTable ExecuteStoreProcReturnTable(string storeProcName, List<OracleParameter> listinput)
        {
            DataTable resTable = new DataTable();
            try
            {
                _con.Open();

                OracleCommand cmd = _con.CreateCommand();
                cmd.CommandText = storeProcName;
                cmd.CommandType = CommandType.StoredProcedure;

                //Lay so parameter
                int N = listinput.Count;
                for (int i = 0; i < N; i++)
                {
                    cmd.Parameters.Add(listinput[i]);
                }
                OracleDataAdapter adapter = new OracleDataAdapter(cmd);
                adapter.Fill(resTable);

                _con.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Loi khi thuc thi store procedure: " + ex.Message);
            }

            return resTable;
        }
        // select nhieu bang cung luc 
        public static DataSet ExecuteSelectQuery(String storeProcName, List<OracleParameter> listinput)
        {
            DataSet ds = new DataSet();
            try
            {
                _con.Open();
                OracleCommand cmd = _con.CreateCommand();
                cmd.CommandText = storeProcName;
                cmd.CommandType = CommandType.StoredProcedure;
                //Lay so parameter
                int N = listinput.Count;
                for (int i = 0; i < N; i++)
                {
                    cmd.Parameters.Add(listinput[i]);
                }
                OracleDataAdapter adapter = new OracleDataAdapter(cmd);
                adapter.SelectCommand = cmd;
                adapter.Fill(ds);
                _con.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Loi khi thuc thi store procedure: " + ex.Message);
            }
            return ds;
        }
    }
}
