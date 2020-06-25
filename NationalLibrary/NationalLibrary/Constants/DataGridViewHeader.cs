using System.Collections.Generic;

namespace NationalLibrary.Constants
{
    public class DataGridViewHeader
    {
        public const string IDHeader = "ID";
        public const string NameHeader = "Name";
        public const string LocationHeader = "Location";
        public const string CreatedDateHeader = "Created Date";
        public const string ModifiedDateHeader = "Modified Date";
        public const string StatusHeader = "Status";
        public const string NumberHeader = "Number";
        public const string DateHeader = "Date";
        public const string EmailHeader = "Email";
        public const string TypeHeader = "Type";
        public const string ShortCodeHeader = "Short Code";
        public const string LongCodeHeader = "Long Code";
        public const string CountryHeader = "Country";

        public const string TitleHeader = "Title";
        public const string SKUHeader = "SKU";
        public const string AuthorHeader = "Author";
        public const string PriceHeader = "Price";
        public const string PublishDateHeader = "Publish Date";
        public const string ISBNCodeHeader = "ISBN Code";
        public const string PublisherHeader = "Publisher";
        public const string EditionHeader = "Edition";
        public const string EditorHeader = "Editor";
        public const string FormatHeader = "Format";
        public const string LanguageHeader = "Language";
        public const string CategoryHeader = "Category";
        public const string CopyrightHeader = "Copyright";
        public const string QuantityHeader = "Qty";
        public const string QuantityBrokenHeader = "QtyBroken";
        public const string ConditionHeader = "Condition";
        public const string TotalPriceHeader = "Total Price";
        public const string TotalLossHeader = "Total Loss";

        public static List<string> listOfColumnsPublisher = new List<string>
        {
            IDHeader,
            NameHeader,
            LocationHeader,
            CreatedDateHeader,
            ModifiedDateHeader,
            StatusHeader
        };

        public static List<string> listOfColumnsEdition = new List<string>
        {
            IDHeader,
            NumberHeader,
            DateHeader,
            CreatedDateHeader,
            ModifiedDateHeader,
            StatusHeader
        };

        public static List<string> listOfColumnsEditor = new List<string>
        {
            IDHeader,
            NameHeader,
            EmailHeader,
            LocationHeader,
            CreatedDateHeader,
            ModifiedDateHeader,
            StatusHeader
        };

        public static List<string> listOfColumnsFormat = new List<string>
        {
            IDHeader,
            TypeHeader,
            CreatedDateHeader,
            ModifiedDateHeader,
            StatusHeader
        };

        public static List<string> listOfColumnsLanguage = new List<string>
        {
            IDHeader,
            ShortCodeHeader,
            LongCodeHeader,
            CreatedDateHeader,
            ModifiedDateHeader,
            StatusHeader
        };

        public static List<string> listOfColumnsCategory = new List<string>
        {
            IDHeader,
            NameHeader,
            CreatedDateHeader,
            ModifiedDateHeader,
            StatusHeader
        };

        public static List<string> listOfColumnsCopyright = new List<string>
        {
            IDHeader,
            NameHeader,
            CreatedDateHeader,
            ModifiedDateHeader,
            StatusHeader
        };

        public static List<string> listOfColumnsAuthor = new List<string>
        {
            IDHeader,
            NameHeader,
            EmailHeader,
            CountryHeader,
            CreatedDateHeader,
            ModifiedDateHeader,
            StatusHeader
        };

        public static List<string> listOfColumnsType = new List<string>
        {
            IDHeader,
            TitleHeader,
            SKUHeader,
            AuthorHeader,
            PriceHeader,
            PublishDateHeader,
            ISBNCodeHeader,
            PublisherHeader,
            EditionHeader,
            EditorHeader,
            FormatHeader,
            LanguageHeader,
            CategoryHeader,
            CopyrightHeader,
            QuantityHeader,
            QuantityBrokenHeader,
            CreatedDateHeader,
            ModifiedDateHeader,
            StatusHeader
        };

        public static List<string> listOfColumnsTypeByKeyword = new List<string>
        {
            TitleHeader,
            SKUHeader,
            ISBNCodeHeader,
            PriceHeader,
            QuantityHeader,
            QuantityBrokenHeader,
            ConditionHeader,
            TotalPriceHeader,
            TotalLossHeader
        };
    }
}
