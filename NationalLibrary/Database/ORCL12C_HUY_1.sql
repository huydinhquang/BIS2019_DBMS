/*
variable p_result NVARCHAR2;
EXECUTE ImportDataFromCSV('Publisher Name 12', p_result)
print p_result;
*/

CREATE OR REPLACE PROCEDURE ImportDataFromCSV (
typeTitle IN NVARCHAR2,
typeSKU IN NVARCHAR2,
typePrice IN NUMBER,
typePublishDate DATE,
typeISBNCode IN NVARCHAR2,
typeQuantity IN NUMBER,
typeQuantityBroken IN NUMBER,
publisherName IN NVARCHAR2,
publisherLocation IN NVARCHAR2,
editionNumber IN NVARCHAR2,
editionDate IN DATE,
editorName IN NVARCHAR2,
editorEmail IN NVARCHAR2,
editorLocation IN NVARCHAR2,
formatType IN NVARCHAR2,
languageShortCode IN NVARCHAR2,
languageLongCode IN NVARCHAR2,
categoryName IN NVARCHAR2,
copyrightName IN NVARCHAR2,
authorName IN NVARCHAR2,
authorEmail IN NVARCHAR2,
p_resultNumber OUT NUMBER,
p_result OUT NVARCHAR2)
AS
TypeID NUMBER;
PublisherID NUMBER;
EditionID NUMBER;
EditorID NUMBER;
FormatID NUMBER;
LanguageID NUMBER;
CategoryID NUMBER;
CopyrightID NUMBER;
AuthorID NUMBER;
BEGIN
    BEGIN
        -- Check Publisher --
        BEGIN
            SELECT LibraryTypePublisherID
            INTO PublisherID
            FROM LibraryTypePublisher
            WHERE LOWER(LibraryTypePublisherName) = LOWER(publisherName);
            
        EXCEPTION
          WHEN NO_DATA_FOUND THEN
            PublisherID := 0;
        END;
        
        IF (PublisherID = 0)
        THEN
            INSERT INTO LibraryTypePublisher (LibraryTypePublisherName, LibraryTypePublisherLocation) VALUES (publisherName, publisherLocation)
            RETURNING LibraryTypePublisherID INTO PublisherID;
        END IF;
        
        -- Check Edition --
        BEGIN
            SELECT LibraryTypeEditionID
            INTO EditionID
            FROM LibraryTypeEdition
            WHERE LOWER(LibraryTypeEditionNumber) = LOWER(editionNumber)
                AND LibraryTypeEditionDate = editionDate;
        EXCEPTION
          WHEN NO_DATA_FOUND THEN
            EditionID := 0;
        END;
        
        IF (EditionID = 0)
        THEN
            INSERT INTO LibraryTypeEdition (LibraryTypeEditionNumber, LibraryTypeEditionDate) VALUES (editionNumber, editionDate)
            RETURNING LibraryTypeEditionID INTO EditionID;
        END IF;
        
        -- Check Editor --
        BEGIN
            SELECT LibraryTypeEditorID
            INTO EditorID
            FROM LibraryTypeEditor
            WHERE LOWER(LibraryTypeEditorName) = LOWER(editorName)
                AND LOWER(LibraryTypeEditorEmail) = LOWER(editorEmail)
                AND LOWER(LibraryTypeEditorLocation) = LOWER(editorLocation);
        EXCEPTION
          WHEN NO_DATA_FOUND THEN
            EditorID := 0;
        END;
        
        IF (EditorID = 0)
        THEN
            INSERT INTO LibraryTypeEditor (LibraryTypeEditorName, LibraryTypeEditorEmail, LibraryTypeEditorLocation) VALUES (editorName, editorEmail, editorLocation)
            RETURNING LibraryTypeEditorID INTO EditorID;
        END IF;
        
        -- Check Format --
        BEGIN
            SELECT LibraryTypeFormatID
            INTO FormatID
            FROM LibraryTypeFormat
            WHERE LOWER(LibraryTypeFormatType) = LOWER(formatType);
        EXCEPTION
          WHEN NO_DATA_FOUND THEN
            FormatID := 0;
        END;
        
        IF (FormatID = 0)
        THEN
            INSERT INTO LibraryTypeFormat (LibraryTypeFormatType) VALUES (formatType)
            RETURNING LibraryTypeFormatID INTO FormatID;
        END IF;
        
        -- Check Language --
        BEGIN
            SELECT LibraryTypeLanguageID
            INTO LanguageID
            FROM LibraryTypeLanguage
            WHERE LOWER(LibraryTypeLanguageShortCode) = LOWER(languageShortCode);
        EXCEPTION
          WHEN NO_DATA_FOUND THEN
            LanguageID := 0;
        END;
        
        IF (LanguageID = 0)
        THEN
            INSERT INTO LibraryTypeLanguage (LibraryTypeLanguageShortCode, LibraryTypeLanguageLongCode) VALUES (languageShortCode, languageLongCode)
            RETURNING LibraryTypeLanguageID INTO LanguageID;
        END IF;
        
        -- Check Category --
        BEGIN
            SELECT LibraryTypeCategoryID
            INTO CategoryID
            FROM LibraryTypeCategory
            WHERE LOWER(LibraryTypeCategoryName) = LOWER(categoryName);
        EXCEPTION
          WHEN NO_DATA_FOUND THEN
            CategoryID := 0;
        END;
        
        IF (CategoryID = 0)
        THEN
            INSERT INTO LibraryTypeCategory (LibraryTypeCategoryName) VALUES (categoryName)
            RETURNING LibraryTypeCategoryID INTO CategoryID;
        END IF;
        
        -- Check Copyright --
        BEGIN
            SELECT LibraryTypeCopyrightID
            INTO CopyrightID
            FROM LibraryTypeCopyright
            WHERE LOWER(LibraryTypeCopyrightName) = LOWER(copyrightName);
        EXCEPTION
          WHEN NO_DATA_FOUND THEN
            CopyrightID := 0;
        END;
        
        IF (CopyrightID = 0)
        THEN
            INSERT INTO LibraryTypeCopyright (LibraryTypeCopyrightName) VALUES (copyrightName)
            RETURNING LibraryTypeCopyrightID INTO CopyrightID;
        END IF;
        
        -- Check Author --
        BEGIN
            SELECT LibraryTypeAuthorID
            INTO AuthorID
            FROM LibraryTypeAuthor
            WHERE LOWER(LibraryTypeAuthorName) = LOWER(authorName)
                AND LOWER(LibraryTypeAuthorEmail) = LOWER(authorEmail);
        EXCEPTION
          WHEN NO_DATA_FOUND THEN
            AuthorID := 0;
        END;
        
        IF (AuthorID = 0)
        THEN
            INSERT INTO LibraryTypeAuthor (LibraryTypeAuthorName, LibraryTypeAuthorEmail) VALUES (authorName, authorEmail)
            RETURNING LibraryTypeAuthorID INTO AuthorID;
        END IF;
        
        -- Check Type --
        BEGIN
            SELECT LibraryTypeID
            INTO TypeID
            FROM LibraryType
            WHERE LOWER(LibraryTypeTitle) = LOWER(typeTitle)
                AND LOWER(LibraryTypeISBNCode) = LOWER(typeISBNCode)
                AND LibraryTypePublisherID = PublisherID
                AND LibraryTypeEditionID = EditionID
                AND LibraryTypeLanguageID = LanguageID
                AND LibraryTypeFormatID = FormatID;
        EXCEPTION
          WHEN NO_DATA_FOUND THEN
            TypeID := 0;
        END;
        
        IF (TypeID = 0)
        THEN
            INSERT INTO LibraryType (
                LibraryTypeTitle, 
                LibraryTypeSKU, 
                LibraryTypeAuthorID,
                LibraryTypePrice,
                LibraryTypePublishDate,
                LibraryTypeISBNCode,
                LibraryTypePublisherID,
                LibraryTypeEditionID,
                LibraryTypeEditorID,
                LibraryTypeFormatID,
                LibraryTypeLanguageID,
                LibraryTypeCategoryID,
                LibraryTypeCopyrightID,
                LibraryTypeQuantity,
                LibraryTypeQuantityBroken) VALUES (
                typeTitle, typeSKU, AuthorID, typePrice, typePublishDate,
                typeISBNCode, PublisherID, EditionID, EditorID, FormatID,
                LanguageID, CategoryID, CopyrightID, typeQuantity, typeQuantityBroken)
            RETURNING LibraryTypeID INTO TypeID;
        ELSE
            UPDATE LibraryType SET 
                LibraryTypeTitle = typeTitle, 
                LibraryTypeSKU = typeSKU, 
                LibraryTypeAuthorID = AuthorID,
                LibraryTypePrice = typePrice,
                LibraryTypePublishDate = typePublishDate,
                LibraryTypeISBNCode = typeISBNCode,
                LibraryTypePublisherID = PublisherID,
                LibraryTypeEditionID = EditionID,
                LibraryTypeEditorID = EditorID,
                LibraryTypeFormatID = FormatID,
                LibraryTypeLanguageID = LanguageID,
                LibraryTypeCategoryID = CategoryID,
                LibraryTypeCopyrightID = CopyrightID,
                LibraryTypeQuantity = typeQuantity,
                LibraryTypeQuantityBroken = typeQuantityBroken
            WHERE LibraryTypeID = TypeID;
        END IF;
        
        p_resultNumber := 1;
        p_result := 'SUCCESS';
        
    EXCEPTION
        WHEN OTHERS THEN
        p_resultNumber := 0;
        DBMS_OUTPUT.PUT_LINE(p_result);
    END;
END;
