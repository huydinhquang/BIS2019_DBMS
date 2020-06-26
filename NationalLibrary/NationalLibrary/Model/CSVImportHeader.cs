using System;

namespace NationalLibrary.Model
{
    public class CSVImportHeader
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string SKU { get; set; }
        public string AuthorName { get; set; }
        public string AuthorEmail { get; set; }
        public float Price { get; set; }
        public DateTime PublishDate { get; set; }
        public string ISBNCode { get; set; }
        public int Quantity { get; set; }
        public int QuantityBroken { get; set; }
        public string PublisherName { get; set; }
        public string PublisherLocation { get; set; }
        public string EditionNumber { get; set; }
        public DateTime EditionDate { get; set; }
        public string EditorName { get; set; }
        public string EditorEmail { get; set; }
        public string EditorLocation { get; set; }
        public string Format { get; set; }
        public string Language { get; set; }
        public string LanguageLongCode { get; set; }
        public string Category { get; set; }
        public string Copyright { get; set; }
    }
}
