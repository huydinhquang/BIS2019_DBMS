using Oracle.ManagedDataAccess.Client;

namespace NationalLibrary.Model
{
    public class StoredProcedureParamOut
    {
        public string ParamName { get; set; }
        public OracleDbType ParamType { get; set; }
    }
}
