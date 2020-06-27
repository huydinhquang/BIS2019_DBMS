using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace NationalLibrary.Model
{
    public class StoredProcedureParamOut
    {
        public string ParamName { get; set; }
        public ParameterDirection ParamDirection { get; set; }
        public OracleDbType ParamType { get; set; }
    }
}
