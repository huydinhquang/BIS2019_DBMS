using Oracle.ManagedDataAccess.Client;

namespace NationalLibrary.Model
{
    public class StoredProcedureParamIn
    {
        public string ParamName { get; set; }
        public string ParamValue { get; set; }
        public OracleDbType ParamType { get; set; }
    }
}
