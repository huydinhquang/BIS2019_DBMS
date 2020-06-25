using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NationalLibrary.Constants
{
    public class TableConstants
    {
        #region Table Property

        public const string LibraryTypePublisher = "LibraryTypePublisher";
        public const string LibraryTypeEdition = "LibraryTypeEdition";
        public const string LibraryTypeEditor = "LibraryTypeEditor";
        public const string LibraryTypeFormat = "LibraryTypeFormat";
        public const string LibraryTypeLanguage = "LibraryTypeLanguage";
        public const string LibraryTypeCategory = "LibraryTypeCategory";
        public const string LibraryTypeCopyright = "LibraryTypeCopyright";
        public const string LibraryTypeAuthor = "LibraryTypeAuthor";
        public const string LibraryTypeStatus = "LibraryTypeStatus";
        public const string LibraryType = "LibraryType";

        #endregion

        #region Column Property

        public const string LibraryTypePublisherIDStr = "LibraryTypePublisherID";
        public const string LibraryTypePublisherNameStr = "LibraryTypePublisherName";

        public const string LibraryTypeEditionIDStr = "LibraryTypeEditionID";
        public const string LibraryTypeEditionNumberStr = "LibraryTypeEditionNumber";

        public const string LibraryTypeEditorIDStr = "LibraryTypeEditorID";
        public const string LibraryTypeEditorNameStr = "LibraryTypeEditorName";

        public const string LibraryTypeFormatIDStr = "LibraryTypeFormatID";
        public const string LibraryTypeFormatTypeStr = "LibraryTypeFormatType";

        public const string LibraryTypeLanguageIDStr = "LibraryTypeLanguageID";
        public const string LibraryTypeLanguageShortCodeStr = "LibraryTypeLanguageShortCode";

        public const string LibraryTypeCategoryIDStr = "LibraryTypeCategoryID";
        public const string LibraryTypeCategoryNameStr = "LibraryTypeCategoryName";

        public const string LibraryTypeCopyrightIDStr = "LibraryTypeCopyrightID";
        public const string LibraryTypeCopyrightNameStr = "LibraryTypeCopyrightName";

        public const string LibraryTypeAuthorIDStr = "LibraryTypeAuthorID";
        public const string LibraryTypeAuthorNameStr = "LibraryTypeAuthorName";

        public const string LibraryTypeStatusIDStr = "LibraryTypeStatusID";
        public const string LibraryTypeStatusNameStr = "LibraryTypeStatusName";

        public const string LibraryTypeIDStr = "LibraryTypeID";
        public const string LibraryTypeTitleStr = "LibraryTypeTitle";
        public const string LibraryTypeSKUStr = "LibraryTypeSKU";
        public const string LibraryTypePriceStr = "LibraryTypePrice";
        public const string LibraryTypePublishDateStr = "LibraryTypePublishDate";
        public const string LibraryTypeISBNCodeStr = "LibraryTypeISBNCode";
        public const string LibraryTypeQuantityStr = "LibraryTypeQuantity";
        public const string LibraryTypeQuantityBrokenStr = "LibraryTypeQuantityBroken";

        #endregion

        #region Stored Procedure Property

        public const string SearchLibraryTypeByTitle = "SearchLibraryTypeByTitle";
        public const string SearchLibraryTypeByKeyword = "SearchLibraryTypeByKeyword";
        public const string GetLibraryTypeByID = "GetLibraryTypeByID";

        #endregion

    }
}
