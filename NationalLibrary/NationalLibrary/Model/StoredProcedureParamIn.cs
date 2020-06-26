using Oracle.ManagedDataAccess.Client;
using System;

namespace NationalLibrary.Model
{
    public class StoredProcedureParamIn
    {
        public string ParamName { get; set; }
        public object ParamValue { get; set; }
        public OracleDbType ParamType { get; set; }
    }
}
