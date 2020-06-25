/*
variable rc refcursor;
EXECUTE ImportDataFromCSV('Publisher Name 12', :rc)
print rc;
*/
CREATE OR REPLACE PROCEDURE ImportDataFromCSV (
publisherName IN NVARCHAR2,
publisherLocation IN NVARCHAR2,
p_result OUT NVARCHAR2 )
AS
PublisherID NUMBER;
BEGIN
    OPEN p_result FOR
        SELECT LibraryTypePublisherID
        INTO PublisherID
        FROM LibraryTypePublisher
        WHERE LibraryTypePublisherName = publisherName;
        
        IF (PublisherID IS NULL)
        THEN
            INSERT INTO LibraryTypePublisher (LibraryTypePublisherName, LibraryTypePublisherLocation) VALUES (publisherName, publisherLocation)
            RETURNING LibraryTypePublisherID INTO PublisherID;
        END IF;
        
        
        /*
        SELECT type.LibraryTypeTitle,
            type.LibraryTypeSKU,
            type.LibraryTypePrice,
            type.LibraryTypeISBNCode,
            SUM(type.LibraryTypeQuantity) AS LibraryTypeQuantity,
            SUM(type.LibraryTypeQuantityBroken) AS LibraryTypeQuantityBroken,
            CASE
                WHEN SUM(type.LibraryTypeQuantity) - SUM(type.LibraryTypeQuantityBroken) = 0 THEN 'OUT OF STOCK'
                WHEN SUM(type.LibraryTypeQuantity) - SUM(type.LibraryTypeQuantityBroken) < 10 THEN 'WARNING'
                WHEN SUM(type.LibraryTypeQuantity) - SUM(type.LibraryTypeQuantityBroken) > SUM(type.LibraryTypeQuantity) / 2 THEN 'GOOD'
            END AS LibraryTypeCondition
        FROM LibraryType type
        INNER JOIN LibraryTypePublisher publisher ON type.LibraryTypePublisherID = publisher.LibraryTypePublisherID
        INNER JOIN LibraryTypeEdition edition ON type.LibraryTypeEditionID = edition.LibraryTypeEditionID
        INNER JOIN LibraryTypeEditor editor ON type.LibraryTypeEditorID = editor.LibraryTypeEditorID
        INNER JOIN LibraryTypeFormat format ON type.LibraryTypeFormatID = format.LibraryTypeFormatID
        INNER JOIN LibraryTypeLanguage language ON type.LibraryTypeLanguageID = language.LibraryTypeLanguageID
        INNER JOIN LibraryTypeCategory category ON type.LibraryTypeCategoryID = category.LibraryTypeCategoryID
        INNER JOIN LibraryTypeCopyright copyright ON type.LibraryTypeCopyrightID = copyright.LibraryTypeCopyrightID
        INNER JOIN LibraryTypeStatus status ON type.LibraryTypeStatusID = status.LibraryTypeStatusID
        INNER JOIN LibraryTypeAuthor author ON type.LibraryTypeAuthorID = author.LibraryTypeAuthorID
        WHERE (type.LibraryTypeTitle LIKE '%' || p_keyword || '%' OR
                author.LibraryTypeAuthorName LIKE '%' || p_keyword || '%' OR
                publisher.LibraryTypePublisherName LIKE '%' || p_keyword || '%' OR
                editor.LibraryTypeEditorName LIKE '%' || p_keyword || '%' OR
                format.LibraryTypeFormatType LIKE '%' || p_keyword || '%' OR
                language.LibraryTypeLanguageShortCode LIKE '%' || p_keyword || '%' OR
                category.LibraryTypeCategoryName LIKE '%' || p_keyword || '%' OR
                copyright.LibraryTypeCopyrightName LIKE '%' || p_keyword || '%')
            AND type.LibraryTypeStatusID = 1
        GROUP BY type.LibraryTypeTitle,
            type.LibraryTypeSKU,
            type.LibraryTypePrice,
            type.LibraryTypeISBNCode;
            */
END;
