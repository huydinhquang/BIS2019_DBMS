using NationalLibrary.Constants;
using NationalLibrary.Model;
using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;
using System.Data;

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
                ParamDirection = ParameterDirection.Output,
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
                ParamDirection = ParameterDirection.Output,
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
                ParamDirection = ParameterDirection.Output,
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

        public static StoredProcedure ImportDataFromCSV(CSVImportHeader paramInValue)
        {
            var paramIn = new List<StoredProcedureParamIn>
            {
                new StoredProcedureParamIn
                {
                    ParamName = "typeTitle",
                    ParamType = OracleDbType.NVarchar2,
                    ParamValue = paramInValue.Title
                },
                new StoredProcedureParamIn
                {
                    ParamName = "typeSKU",
                    ParamType = OracleDbType.NVarchar2,
                    ParamValue = paramInValue.SKU
                },
                new StoredProcedureParamIn
                {
                    ParamName = "typePrice",
                    ParamType = OracleDbType.Double,
                    ParamValue = paramInValue.Price
                },
                new StoredProcedureParamIn
                {
                    ParamName = "typePublishDate",
                    ParamType = OracleDbType.Date,
                    ParamValue = paramInValue.PublishDate
                },
                new StoredProcedureParamIn
                {
                    ParamName = "typeISBNCode",
                    ParamType = OracleDbType.NVarchar2,
                    ParamValue = paramInValue.ISBNCode
                },
                new StoredProcedureParamIn
                {
                    ParamName = "typeQuantity",
                    ParamType = OracleDbType.Int32,
                    ParamValue = paramInValue.Quantity
                },
                new StoredProcedureParamIn
                {
                    ParamName = "typeQuantityBroken",
                    ParamType = OracleDbType.Int32,
                    ParamValue = paramInValue.QuantityBroken
                },
                new StoredProcedureParamIn
                {
                    ParamName = "publisherName",
                    ParamType = OracleDbType.NVarchar2,
                    ParamValue = paramInValue.PublisherName
                },
                new StoredProcedureParamIn
                {
                    ParamName = "publisherLocation",
                    ParamType = OracleDbType.NVarchar2,
                    ParamValue = paramInValue.PublisherLocation
                },
                new StoredProcedureParamIn
                {
                    ParamName = "editionNumber",
                    ParamType = OracleDbType.NVarchar2,
                    ParamValue = paramInValue.EditionNumber
                },
                new StoredProcedureParamIn
                {
                    ParamName = "editionDate",
                    ParamType = OracleDbType.Date,
                    ParamValue = paramInValue.EditionDate
                },
                new StoredProcedureParamIn
                {
                    ParamName = "editorName",
                    ParamType = OracleDbType.NVarchar2,
                    ParamValue = paramInValue.EditorName
                },
                new StoredProcedureParamIn
                {
                    ParamName = "editorEmail",
                    ParamType = OracleDbType.NVarchar2,
                    ParamValue = paramInValue.EditorEmail
                },
                new StoredProcedureParamIn
                {
                    ParamName = "editorLocation",
                    ParamType = OracleDbType.NVarchar2,
                    ParamValue = paramInValue.EditorLocation
                },
                new StoredProcedureParamIn
                {
                    ParamName = "formatType",
                    ParamType = OracleDbType.NVarchar2,
                    ParamValue = paramInValue.Format
                },
                new StoredProcedureParamIn
                {
                    ParamName = "languageShortCode",
                    ParamType = OracleDbType.NVarchar2,
                    ParamValue = paramInValue.Language
                },
                new StoredProcedureParamIn
                {
                    ParamName = "languageLongCode",
                    ParamType = OracleDbType.NVarchar2,
                    ParamValue = paramInValue.LanguageLongCode
                },
                new StoredProcedureParamIn
                {
                    ParamName = "categoryName",
                    ParamType = OracleDbType.NVarchar2,
                    ParamValue = paramInValue.Category
                },
                new StoredProcedureParamIn
                {
                    ParamName = "copyrightName",
                    ParamType = OracleDbType.NVarchar2,
                    ParamValue = paramInValue.Copyright
                },
                new StoredProcedureParamIn
                {
                    ParamName = "authorName",
                    ParamType = OracleDbType.NVarchar2,
                    ParamValue = paramInValue.AuthorName
                },
                new StoredProcedureParamIn
                {
                    ParamName = "authorEmail",
                    ParamType = OracleDbType.NVarchar2,
                    ParamValue = paramInValue.AuthorEmail
                }
            };

            var paramOut = new List<StoredProcedureParamOut>
            {
                new StoredProcedureParamOut
                {
                    ParamName = "p_resultNumber",
                    ParamDirection = ParameterDirection.Output,
                    ParamType = OracleDbType.Int32
                },
                new StoredProcedureParamOut
                {
                    ParamName = "p_result",
                    ParamDirection = ParameterDirection.Output,
                    ParamType = OracleDbType.NVarchar2
                }
            };

            return new StoredProcedure
            {
                Name = TableConstants.ImportDataFromCSV,
                ListParamsIn = paramIn,
                ListParamsOut = paramOut
            };
        }
    }
}
