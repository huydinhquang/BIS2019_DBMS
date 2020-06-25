using System.Collections.Generic;

namespace NationalLibrary.Model
{
    public class StoredProcedure
    {
        public string Name { get; set; }
        public List<StoredProcedureParamIn> ListParamsIn { get; set; }
        public List<StoredProcedureParamOut> ListParamsOut { get; set; }
    }
}
