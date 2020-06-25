using NationalLibrary.Constants;
using NationalLibrary.Model;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NationalLibrary.Helper
{
    public class BuildingStoredProcHelper
    {
        public static StoredProcedure BuildStoredStrSearchByTitle(string paramInValue)
        {
            var paramIn = new StoredProcedureParamIn
            {
                ParamName = "p_title",
                ParamType = OracleDbType.NVarchar2,
                ParamValue = paramInValue.ToLower()
            };
            var paramOut = new StoredProcedureParamOut
            {
                ParamName = "p_result",
                ParamType = OracleDbType.RefCursor
            };

            return new StoredProcedure
            {
                Name = TableConstants.SearchLibraryTypeByTitle,
                ListParamsIn = new List<StoredProcedureParamIn>
                    {
                        paramIn
                    },
                ListParamsOut = new List<StoredProcedureParamOut>
                    {
                        paramOut
                    }
            };
        }

        public static StoredProcedure BuildStoredStrSearchByID(string paramInValue)
        {
            var paramIn = new StoredProcedureParamIn
            {
                ParamName = "p_typeId",
                ParamType = OracleDbType.Int32,
                ParamValue = paramInValue.ToLower()
            };
            var paramOut = new StoredProcedureParamOut
            {
                ParamName = "p_result",
                ParamType = OracleDbType.RefCursor
            };

            return new StoredProcedure
            {
                Name = TableConstants.GetLibraryTypeByID,
                ListParamsIn = new List<StoredProcedureParamIn>
                    {
                        paramIn
                    },
                ListParamsOut = new List<StoredProcedureParamOut>
                    {
                        paramOut
                    }
            };
        }

        public static StoredProcedure BuildStoredStrSearchByKeyword(string paramInValue)
        {
            var paramIn = new StoredProcedureParamIn
            {
                ParamName = "p_keyword",
                ParamType = OracleDbType.NVarchar2,
                ParamValue = paramInValue.ToLower()
            };
            var paramOut = new StoredProcedureParamOut
            {
                ParamName = "p_result",
                ParamType = OracleDbType.RefCursor
            };

            return new StoredProcedure
            {
                Name = TableConstants.SearchLibraryTypeByKeyword,
                ListParamsIn = new List<StoredProcedureParamIn>
                    {
                        paramIn
                    },
                ListParamsOut = new List<StoredProcedureParamOut>
                    {
                        paramOut
                    }
            };
        }
    }
}
