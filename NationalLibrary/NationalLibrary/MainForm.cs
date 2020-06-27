using NationalLibrary.Constants;
using NationalLibrary.Helper;
using NationalLibrary.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using System.IO;
using CsvHelper;
using System.Globalization;

namespace NationalLibrary
{
    public partial class formNationalLibrary : Form
    {

        #region Constants

        private const string formatDateTime = "dd-MM-yyyy";
        
        #endregion

        #region Property

        List<TableName> listOfTables = new List<TableName>();
        bool firstLoad = false;

        #endregion

        public formNationalLibrary()
        {
            InitializeComponent();
            PrepareData();
            SearchByTitle();
        }

        #region Form Control

        private void lklDragonBall_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("This tool is developed by Dragon Ball Team - VGU - BIS2019.", "Copyright", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void txtSearchByTitle_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSearchByTitle.PerformClick();
            }
        }

        private void txtSearchByKeyword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSearchByKeyword.PerformClick();
            }
        }

        private void tcSearchBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!firstLoad)
            {
                SearchByKeyword();
                firstLoad = true;
            }
        }

        private void tcNationalLibrary_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (tcNationalLibrary.SelectedTab.Name)
            {
                case TabControlConstants.TabSearchInfo:
                    break;
                case TabControlConstants.TabCreateNew:
                    HandlingDataHelper.LoadDataForNewType(listOfTables);
                    break;
                case TabControlConstants.TabUpdateInfo:
                    HandlingDataHelper.LoadDataForNewType(listOfTables);
                    break;
                case TabControlConstants.TabAddData:
                    tcAddData_SelectedIndexChanged(sender, e);
                    break;
                default:
                    break;
            }
        }

        private void tcAddData_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (tcAddData.SelectedTab.Name)
            {
                case TabControlConstants.TabAddDataPublisher:
                    HandlingDataHelper.LoadDataToGrid(dgvPublisher, DataGridViewHeader.listOfColumnsPublisher, TableConstants.LibraryTypePublisher);
                    break;
                case TabControlConstants.TabAddDataEdition:
                    HandlingDataHelper.LoadDataToGrid(dgvEdition, DataGridViewHeader.listOfColumnsEdition, TableConstants.LibraryTypeEdition);
                    break;
                case TabControlConstants.TabAddDataEditor:
                    HandlingDataHelper.LoadDataToGrid(dgvEditor, DataGridViewHeader.listOfColumnsEditor, TableConstants.LibraryTypeEditor);
                    break;
                case TabControlConstants.TabAddDataFormat:
                    HandlingDataHelper.LoadDataToGrid(dgvFormat, DataGridViewHeader.listOfColumnsFormat, TableConstants.LibraryTypeFormat);
                    break;
                case TabControlConstants.TabAddDataLanguage:
                    HandlingDataHelper.LoadDataToGrid(dgvLanguage, DataGridViewHeader.listOfColumnsLanguage, TableConstants.LibraryTypeLanguage);
                    break;
                case TabControlConstants.TabAddDataCategory:
                    HandlingDataHelper.LoadDataToGrid(dgvCategory, DataGridViewHeader.listOfColumnsCategory, TableConstants.LibraryTypeCategory);
                    break;
                case TabControlConstants.TabAddDataCopyright:
                    HandlingDataHelper.LoadDataToGrid(dgvCopyright, DataGridViewHeader.listOfColumnsCopyright, TableConstants.LibraryTypeCopyright);
                    break;
                case TabControlConstants.TabAddDataAuthor:
                    HandlingDataHelper.LoadDataToGrid(dgvAuthor, DataGridViewHeader.listOfColumnsAuthor, TableConstants.LibraryTypeAuthor);
                    break;
                default:
                    break;
            }
        }

        #endregion

        #region Book

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTitle.Text))
            {
                MessageBox.Show("Title cannot be empty!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var title = txtTitle.Text;
            var query = $"INSERT INTO LibraryType (LibraryTypeTitle," +
                $" LibraryTypeSKU," +
                $" LibraryTypePrice," +
                $" LibraryTypeQuantity," +
                $" LibraryTypeISBNCode," +
                $" LibraryTypePublishDate," +
                $" LibraryTypePublisherID," +
                $" LibraryTypeEditionID," +
                $" LibraryTypeEditorID," +
                $" LibraryTypeFormatID," +
                $" LibraryTypeLanguageID," +
                $" LibraryTypeCategoryID," +
                $" LibraryTypeCopyrightID," +
                $" LibraryTypeAuthorID) VALUES ('{title}', '{txtSKU.Text}', '{txtPrice.Text}', " +
                $"'{txtQuantity.Text}', '{txtISBNCode.Text}', TO_DATE('{dtpPublishDate.Value.ToString(formatDateTime)}', '{formatDateTime}'), " +
                $" '{cbxPublisher.SelectedValue}', '{cbxEdition.SelectedValue}', '{cbxEditor.SelectedValue}', " +
                $" '{cbxFormat.SelectedValue}', '{cbxLanguage.SelectedValue}', '{cbxCategory.SelectedValue}', " +
                $" '{cbxCopyright.SelectedValue}', '{cbxAuthor.SelectedValue}')";
            HandlingDataHelper.AddData(null, null, title, "Library Type Title", query, "LibraryType", false);

            // Load Search tab to search the created book by the created Title
            LoadSearchTab(title);
        }


        private void btnUpdate_Click(object sender, EventArgs e)
        {
            var title = txtTitleUpdate.Text;
            if (string.IsNullOrEmpty(txtID.Text) || string.IsNullOrEmpty(title))
            {
                MessageBox.Show("ID or Title cannot be empty!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var query = $"UPDATE {TableConstants.LibraryType} SET {TableConstants.LibraryTypeTitleStr} = '{title}'," +
                $" {TableConstants.LibraryTypeSKUStr} = '{txtSKUUpdate.Text}'," +
                $" {TableConstants.LibraryTypePriceStr} = '{txtPriceUpdate.Text}'," +
                $" {TableConstants.LibraryTypeQuantityStr} = '{txtQuantityUpdate.Text}'," +
                $" {TableConstants.LibraryTypeQuantityBrokenStr} = '{txtQuantityBrokenUpdate.Text}'," +
                $" {TableConstants.LibraryTypeISBNCodeStr} = '{txtISBNCodeUpdate.Text}'," +
                $" {TableConstants.LibraryTypePublishDateStr} = TO_DATE('{dtpPublishDateUpdate.Value.ToString(formatDateTime)}', '{formatDateTime}')," +
                $" {TableConstants.LibraryTypePublisherIDStr} = '{cbxPublisherUpdate.SelectedValue}'," +
                $" {TableConstants.LibraryTypeEditionIDStr} = '{cbxEditionUpdate.SelectedValue}'," +
                $" {TableConstants.LibraryTypeEditorIDStr} = '{cbxEditorUpdate.SelectedValue}'," +
                $" {TableConstants.LibraryTypeFormatIDStr} = '{cbxFormatUpdate.SelectedValue}'," +
                $" {TableConstants.LibraryTypeLanguageIDStr} = '{cbxLanguageUpdate.SelectedValue}'," +
                $" {TableConstants.LibraryTypeCategoryIDStr} = '{cbxCategoryUpdate.SelectedValue}'," +
                $" {TableConstants.LibraryTypeCopyrightIDStr} = '{cbxCopyrightUpdate.SelectedValue}'," +
                $" {TableConstants.LibraryTypeAuthorIDStr} = '{cbxAuthorUpdate.SelectedValue}', " +
                $" {TableConstants.LibraryTypeStatusIDStr} = '{cbxStatusUpdate.SelectedValue}' " +
                $" WHERE {TableConstants.LibraryTypeIDStr} = '{txtID.Text}'";
            HandlingDataHelper.UpdateData(null, null, title, "Library Type Title", query, "LibraryType", false);

            // Load Search tab to search the update book by the updated Title
            LoadSearchTab(title);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtID.Text))
            {
                MessageBox.Show("ID cannot be empty!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var query = $"DELETE {TableConstants.LibraryType} WHERE {TableConstants.LibraryTypeIDStr} = '{txtID.Text}";

            // Clear all textbox
            txtID.Clear();
            txtTitleUpdate.Clear();
            txtSKUUpdate.Clear();
            txtPriceUpdate.Clear();
            txtISBNCodeUpdate.Clear();
            txtQuantityUpdate.Clear();
            txtQuantityBrokenUpdate.Clear();
            dtpPublishDateUpdate.Value = DateTime.Now;
            SearchByTitle();
        }

        #endregion

        #region Publisher

        private void btnAddPublisher_Click(object sender, EventArgs e)
        {
            var query = $"INSERT INTO LibraryTypePublisher (LibraryTypePublisherName, LibraryTypePublisherLocation) VALUES ('{txtPublisherName.Text}', '{txtPublisherLocation.Text}')";
            HandlingDataHelper.AddData(dgvPublisher, DataGridViewHeader.listOfColumnsPublisher, txtPublisherName.Text, "Publisher Name", query, "LibraryTypePublisher");
        }

        private void btnUpdatePublisher_Click(object sender, EventArgs e)
        {
            var query = $"UPDATE LibraryTypePublisher SET LibraryTypePublisherName = '{txtPublisherNameUpdate.Text}', LibraryTypePublisherLocation = '{txtPublisherLocationUpdate.Text}' WHERE LibraryTypePublisherID = {txtPublisherID.Text}";
            HandlingDataHelper.UpdateData(dgvPublisher, DataGridViewHeader.listOfColumnsPublisher, txtPublisherID.Text, "Publisher ID", query, "LibraryTypePublisher");
        }

        private void btnDeletePublisher_Click(object sender, EventArgs e)
        {
            var query = $"DELETE LibraryTypePublisher WHERE LibraryTypePublisherID = {txtPublisherID.Text}";
            HandlingDataHelper.DeleteData(dgvPublisher, DataGridViewHeader.listOfColumnsPublisher, txtPublisherID.Text, "Publisher ID", query, "LibraryTypePublisher");
        }

        private void dgvPublisher_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Get the Row Index
            var index = e.RowIndex;
            if (index < 0)
            {
                return;
            }
            var selectedRow = dgvPublisher.Rows[index];
            txtPublisherID.Text = selectedRow.Cells[0].Value.ToString();
            txtPublisherNameUpdate.Text = selectedRow.Cells[1].Value.ToString();
            txtPublisherLocationUpdate.Text = selectedRow.Cells[2].Value.ToString();
        }

        #endregion

        #region Edition

        private void btnAddEdition_Click(object sender, EventArgs e)
        {
            var query = $"INSERT INTO LibraryTypeEdition (LibraryTypeEditionNumber, LibraryTypeEditionDate) VALUES ('{txtEditionNumber.Text}', TO_DATE('{dtpEditionDate.Value.ToString(formatDateTime)}', '{formatDateTime}'))";
            HandlingDataHelper.AddData(dgvEdition, DataGridViewHeader.listOfColumnsEdition, txtEditionNumber.Text, "Edition Name", query, "LibraryTypeEdition");
        }

        private void btnUpdateEdition_Click(object sender, EventArgs e)
        {
            var query = $"UPDATE LibraryTypeEdition SET LibraryTypeEditionNumber = '{txtEditionNumberUpdate.Text}', LibraryTypeEditionDate = TO_DATE('{dtpEditionDateUpdate.Value.ToString(formatDateTime)}', '{formatDateTime}') WHERE LibraryTypeEditionID = {txtEditionID.Text}";
            HandlingDataHelper.UpdateData(dgvEdition, DataGridViewHeader.listOfColumnsEdition, txtEditionID.Text, "Edition ID", query, "LibraryTypeEdition");
        }

        private void btnDeleteEdition_Click(object sender, EventArgs e)
        {
            var query = $"DELETE LibraryTypeEdition WHERE LibraryTypeEditionID = {txtEditionID.Text}";
            HandlingDataHelper.DeleteData(dgvEdition, DataGridViewHeader.listOfColumnsEdition, txtEditionID.Text, "Edition ID", query, "LibraryTypeEdition");
        }

        private void dgvEdition_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Get the Row Index
            var index = e.RowIndex;
            if (index < 0)
            {
                return;
            }
            var selectedRow = dgvEdition.Rows[index];
            txtEditionID.Text = selectedRow.Cells[0].Value.ToString();
            txtEditionNumberUpdate.Text = selectedRow.Cells[1].Value.ToString();
            dtpEditionDateUpdate.Value = Convert.ToDateTime(selectedRow.Cells[2].Value);
        }

        #endregion

        #region Editor

        private void btnAddEditor_Click(object sender, EventArgs e)
        {
            var query = $"INSERT INTO LibraryTypeEditor (LibraryTypeEditorName, LibraryTypeEditorEmail, LibraryTypeEditorLocation) VALUES ('{txtEditorName.Text}', '{txtEditorEmail.Text}', '{txtEditorLocation.Text}')";
            HandlingDataHelper.AddData(dgvEditor, DataGridViewHeader.listOfColumnsEditor, txtEditorName.Text, "Editor Name", query, "LibraryTypeEditor");
        }

        private void btnUpdateEditor_Click(object sender, EventArgs e)
        {
            var query = $"UPDATE LibraryTypeEditor SET LibraryTypeEditorName = '{txtEditorNameUpdate.Text}', LibraryTypeEditorEmail = '{txtEditorEmailUpdate.Text}', LibraryTypeEditorLocation = '{txtEditorLocationUpdate.Text}' WHERE LibraryTypeEditorID = {txtEditorID.Text}";
            HandlingDataHelper.UpdateData(dgvEditor, DataGridViewHeader.listOfColumnsEditor, txtEditorID.Text, "Editor ID", query, "LibraryTypeEditor");
        }

        private void btnDeleteEditor_Click(object sender, EventArgs e)
        {
            var query = $"DELETE LibraryTypeEditor WHERE LibraryTypeEditorID = {txtEditorID.Text}";
            HandlingDataHelper.DeleteData(dgvEditor, DataGridViewHeader.listOfColumnsEditor, txtEditorID.Text, "Editor ID", query, "LibraryTypeEditor");
        }

        private void dgvEditor_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Get the Row Index
            var index = e.RowIndex;
            if (index < 0)
            {
                return;
            }
            var selectedRow = dgvEditor.Rows[index];
            txtEditorID.Text = selectedRow.Cells[0].Value.ToString();
            txtEditorNameUpdate.Text = selectedRow.Cells[1].Value.ToString();
            txtEditorEmailUpdate.Text = selectedRow.Cells[2].Value.ToString();
            txtEditorLocationUpdate.Text = selectedRow.Cells[3].Value.ToString();
        }

        #endregion

        #region Format

        private void btnAddFormat_Click(object sender, EventArgs e)
        {
            var query = $"INSERT INTO LibraryTypeFormat (LibraryTypeFormatType) VALUES ('{txtFormatType.Text}')";
            HandlingDataHelper.AddData(dgvFormat, DataGridViewHeader.listOfColumnsFormat, txtFormatType.Text, "Format Name", query, "LibraryTypeFormat");
        }

        private void btnUpdateFormat_Click(object sender, EventArgs e)
        {
            var query = $"UPDATE LibraryTypeFormat SET LibraryTypeFormatType = '{txtFormatTypeUpdate.Text}' WHERE LibraryTypeFormatID = {txtFormatID.Text}";
            HandlingDataHelper.UpdateData(dgvFormat, DataGridViewHeader.listOfColumnsFormat, txtFormatID.Text, "Format ID", query, "LibraryTypeFormat");
        }

        private void btnDeleteFormat_Click(object sender, EventArgs e)
        {
            var query = $"DELETE LibraryTypeFormat WHERE LibraryTypeFormatID = {txtFormatID.Text}";
            HandlingDataHelper.DeleteData(dgvFormat, DataGridViewHeader.listOfColumnsFormat, txtFormatID.Text, "Format ID", query, "LibraryTypeFormat");
        }

        private void dgvFormat_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Get the Row Index
            var index = e.RowIndex;
            if (index < 0)
            {
                return;
            }
            var selectedRow = dgvFormat.Rows[index];
            txtFormatID.Text = selectedRow.Cells[0].Value.ToString();
            txtFormatTypeUpdate.Text = selectedRow.Cells[1].Value.ToString();
        }

        #endregion

        #region Language

        private void btnAddLanguage_Click(object sender, EventArgs e)
        {
            var query = $"INSERT INTO LibraryTypeLanguage (LibraryTypeLanguageShortCode, LibraryTypeLanguageLongCode) VALUES ('{txtLanguageShortCode.Text}', '{txtLanguageLongCode.Text}')";
            HandlingDataHelper.AddData(dgvLanguage, DataGridViewHeader.listOfColumnsLanguage, txtLanguageShortCode.Text, "Language Short Code", query, "LibraryTypeLanguage");
        }

        private void btnUpdateLanguage_Click(object sender, EventArgs e)
        {
            var query = $"UPDATE LibraryTypeLanguage SET LibraryTypeLanguageShortCode = '{txtLanguageShortCodeUpdate.Text}', LibraryTypeLanguageLongCode = '{txtLanguageLongCodeUpdate.Text}' WHERE LibraryTypeLanguageID = {txtLanguageID.Text}";
            HandlingDataHelper.UpdateData(dgvLanguage, DataGridViewHeader.listOfColumnsLanguage, txtLanguageID.Text, "Language ID", query, "LibraryTypeLanguage");
        }

        private void btnDeleteLanguage_Click(object sender, EventArgs e)
        {
            var query = $"DELETE LibraryTypeLanguage WHERE LibraryTypeLanguageID = {txtLanguageID.Text}";
            HandlingDataHelper.DeleteData(dgvLanguage, DataGridViewHeader.listOfColumnsLanguage, txtLanguageID.Text, "Language ID", query, "LibraryTypeLanguage");
        }

        private void dgvLanguage_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Get the Row Index
            var index = e.RowIndex;
            if (index < 0)
            {
                return;
            }
            var selectedRow = dgvLanguage.Rows[index];
            txtLanguageID.Text = selectedRow.Cells[0].Value.ToString();
            txtLanguageShortCodeUpdate.Text = selectedRow.Cells[1].Value.ToString();
            txtLanguageLongCodeUpdate.Text = selectedRow.Cells[2].Value.ToString();
        }

        #endregion

        #region Category

        private void btnAddCategory_Click(object sender, EventArgs e)
        {
            var query = $"INSERT INTO LibraryTypeCategory (LibraryTypeCategoryName) VALUES ('{txtCategoryName.Text}')";
            HandlingDataHelper.AddData(dgvCategory, DataGridViewHeader.listOfColumnsCategory, txtCategoryName.Text, "Language Name", query, "LibraryTypeCategory");
        }

        private void btnUpdateCategory_Click(object sender, EventArgs e)
        {
            var query = $"UPDATE LibraryTypeCategory SET LibraryTypeCategoryName = '{txtCategoryNameUpdate.Text}' WHERE LibraryTypeCategoryID = {txtCategoryID.Text}";
            HandlingDataHelper.UpdateData(dgvCategory, DataGridViewHeader.listOfColumnsCategory, txtCategoryID.Text, "Language ID", query, "LibraryTypeCategory");
        }

        private void btnDeleteCategory_Click(object sender, EventArgs e)
        {
            var query = $"DELETE LibraryTypeCategory WHERE LibraryTypeCategoryID = {txtCategoryID.Text}";
            HandlingDataHelper.DeleteData(dgvCategory, DataGridViewHeader.listOfColumnsCategory, txtCategoryID.Text, "Language ID", query, "LibraryTypeCategory");
        }

        private void dgvCategory_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Get the Row Index
            var index = e.RowIndex;
            if (index < 0)
            {
                return;
            }
            var selectedRow = dgvCategory.Rows[index];
            txtCategoryID.Text = selectedRow.Cells[0].Value.ToString();
            txtCategoryNameUpdate.Text = selectedRow.Cells[1].Value.ToString();
        }

        #endregion

        #region Copyright

        private void btnAddCopyright_Click(object sender, EventArgs e)
        {
            var query = $"INSERT INTO LibraryTypeCopyright (LibraryTypeCopyrightName) VALUES ('{txtCopyrightName.Text}')";
            HandlingDataHelper.AddData(dgvCopyright, DataGridViewHeader.listOfColumnsCopyright, txtCopyrightName.Text, "Copyright Name", query, "LibraryTypeCopyright");
        }

        private void btnUpdateCopyright_Click(object sender, EventArgs e)
        {
            var query = $"UPDATE LibraryTypeCopyright SET LibraryTypeCopyrightName = '{txtCopyrightNameUpdate.Text}' WHERE LibraryTypeCopyrightID = {txtCopyrightID.Text}";
            HandlingDataHelper.UpdateData(dgvCopyright, DataGridViewHeader.listOfColumnsCopyright, txtCopyrightID.Text, "Copyright ID", query, "LibraryTypeCopyright");
        }

        private void btnDeleteCopyright_Click(object sender, EventArgs e)
        {
            var query = $"DELETE LibraryTypeCopyright WHERE LibraryTypeCopyrightID = {txtCopyrightID.Text}";
            HandlingDataHelper.DeleteData(dgvCopyright, DataGridViewHeader.listOfColumnsCopyright, txtCopyrightID.Text, "Copyright ID", query, "LibraryTypeCopyright");
        }

        private void dgvCopyright_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Get the Row Index
            var index = e.RowIndex;
            if (index < 0)
            {
                return;
            }
            var selectedRow = dgvCopyright.Rows[index];
            txtCopyrightID.Text = selectedRow.Cells[0].Value.ToString();
            txtCopyrightNameUpdate.Text = selectedRow.Cells[1].Value.ToString();
        }

        #endregion

        #region Author

        private void btnAddAuthor_Click(object sender, EventArgs e)
        {
            var query = $"INSERT INTO LibraryTypeAuthor (LibraryTypeAuthorName, LibraryTypeAuthorEmail, LibraryTypeAuthorCountry) VALUES ('{txtAuthorName.Text}', '{txtAuthorEmail.Text}', '{txtAuthorCountry.Text}')";
            HandlingDataHelper.AddData(dgvAuthor, DataGridViewHeader.listOfColumnsAuthor, txtAuthorName.Text, "Author Name", query, "LibraryTypeAuthor");
        }

        private void btnUpdateAuthor_Click(object sender, EventArgs e)
        {
            var query = $"UPDATE LibraryTypeAuthor SET LibraryTypeAuthorName = '{txtAuthorNameUpdate.Text}', LibraryTypeAuthorEmail = '{txtAuthorEmailUpdate.Text}', LibraryTypeAuthorCountry = '{txtAuthorCountryUpdate.Text}' WHERE LibraryTypeAuthorID = {txtAuthorID.Text}";
            HandlingDataHelper.UpdateData(dgvAuthor, DataGridViewHeader.listOfColumnsAuthor, txtAuthorID.Text, "Author ID", query, "LibraryTypeAuthor");
        }

        private void btnDeleteAuthor_Click(object sender, EventArgs e)
        {
            var query = $"DELETE LibraryTypeAuthor WHERE LibraryTypeAuthorID = {txtAuthorID.Text}";
            HandlingDataHelper.DeleteData(dgvAuthor, DataGridViewHeader.listOfColumnsAuthor, txtAuthorID.Text, "Author ID", query, "LibraryTypeAuthor");
        }

        private void dgvAuthor_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Get the Row Index
            var index = e.RowIndex;
            if (index < 0)
            {
                return;
            }
            var selectedRow = dgvAuthor.Rows[index];
            txtAuthorID.Text = selectedRow.Cells[0].Value.ToString();
            txtAuthorNameUpdate.Text = selectedRow.Cells[1].Value.ToString();
            txtAuthorEmailUpdate.Text = selectedRow.Cells[2].Value.ToString();
            txtAuthorCountryUpdate.Text = selectedRow.Cells[3].Value.ToString();
        }

        #endregion

        #region Search

        private void SearchByTitle()
        {
            var storedGetLibraryType = BuildingStoredProcHelper.BuildStoredStrSearchByTitle(txtSearchByTitle.Text);
            HandlingDataHelper.LoadDataToGrid(dgvSearchByTitle, DataGridViewHeader.listOfColumnsType, storedGetLibraryType);
            lblSearchByTitleCount.Text = $"Total: {dgvSearchByTitle.RowCount} record(s)";
        }

        private void SearchByKeyword()
        {
            var storedGetLibraryType = BuildingStoredProcHelper.BuildStoredStrSearchByKeyword(txtSearchByKeyword.Text);
            HandlingDataHelper.LoadDataToGrid(dgvSearchByKeyword, DataGridViewHeader.listOfColumnsTypeByKeyword, storedGetLibraryType);
            lblSearchByKeywordCount.Text = $"Total: {dgvSearchByKeyword.RowCount} record(s)";
        }

        private void btnSearchByTitle_Click(object sender, EventArgs e)
        {
            SearchByTitle();
        }

        private void dgvSearchByTitle_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Get the Row Index
            var index = e.RowIndex;
            if (index < 0)
            {
                return;
            }
            var selectedRow = dgvSearchByTitle.Rows[index];
            tcNationalLibrary.SelectedIndex = 2;
            var libraryTypeID = selectedRow.Cells[0].Value.ToString();
            var storedGetLibraryType = BuildingStoredProcHelper.BuildStoredStrSearchByID(libraryTypeID);
            BindDataToForm(HandlingDataHelper.LoadDataToForm(storedGetLibraryType));
        }

        private void btnSearchByKeyword_Click(object sender, EventArgs e)
        {
            SearchByKeyword();
        }

        #endregion

        #region Methods

        private void PrepareData()
        {
            listOfTables = new List<TableName>
            {
                new TableName {
                    ComboBox = cbxPublisher,
                    ComboBoxUpdate = cbxPublisherUpdate,
                    Name = TableConstants.LibraryTypePublisher,
                    ValueColStr = TableConstants.LibraryTypePublisherIDStr,
                    DisplayColStr = TableConstants.LibraryTypePublisherNameStr
                },
                new TableName {
                    ComboBox = cbxEdition,
                    ComboBoxUpdate = cbxEditionUpdate,
                    Name = TableConstants.LibraryTypeEdition,
                    ValueColStr = TableConstants.LibraryTypeEditionIDStr,
                    DisplayColStr = TableConstants.LibraryTypeEditionNumberStr
                },
                new TableName {
                    ComboBox = cbxEditor,
                    ComboBoxUpdate = cbxEditorUpdate,
                    Name = TableConstants.LibraryTypeEditor,
                    ValueColStr = TableConstants.LibraryTypeEditorIDStr,
                    DisplayColStr = TableConstants.LibraryTypeEditorNameStr
                },
                new TableName {
                    ComboBox = cbxFormat,
                    ComboBoxUpdate = cbxFormatUpdate,
                    Name = TableConstants.LibraryTypeFormat,
                    ValueColStr = TableConstants.LibraryTypeFormatIDStr,
                    DisplayColStr = TableConstants.LibraryTypeFormatTypeStr
                },
                new TableName {
                    ComboBox = cbxLanguage,
                    ComboBoxUpdate = cbxLanguageUpdate,
                    Name = TableConstants.LibraryTypeLanguage,
                    ValueColStr = TableConstants.LibraryTypeLanguageIDStr,
                    DisplayColStr = TableConstants.LibraryTypeLanguageShortCodeStr
                },
                new TableName {
                    ComboBox = cbxCategory,
                    ComboBoxUpdate = cbxCategoryUpdate,
                    Name = TableConstants.LibraryTypeCategory,
                    ValueColStr = TableConstants.LibraryTypeCategoryIDStr,
                    DisplayColStr = TableConstants.LibraryTypeCategoryNameStr
                },
                new TableName {
                    ComboBox = cbxCopyright,
                    ComboBoxUpdate = cbxCopyrightUpdate,
                    Name = TableConstants.LibraryTypeCopyright,
                    ValueColStr = TableConstants.LibraryTypeCopyrightIDStr,
                    DisplayColStr = TableConstants.LibraryTypeCopyrightNameStr
                },
                new TableName {
                    ComboBox = cbxAuthor,
                    ComboBoxUpdate = cbxAuthorUpdate,
                    Name = TableConstants.LibraryTypeAuthor,
                    ValueColStr = TableConstants.LibraryTypeAuthorIDStr,
                    DisplayColStr = TableConstants.LibraryTypeAuthorNameStr
                },
                new TableName {
                    ComboBox = new ComboBox(),
                    ComboBoxUpdate = cbxStatusUpdate,
                    Name = TableConstants.LibraryTypeStatus,
                    ValueColStr = TableConstants.LibraryTypeStatusIDStr,
                    DisplayColStr = TableConstants.LibraryTypeStatusNameStr
                }
            };

        }

        private void BindDataToForm(DataTable dt)
        {
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    txtID.Text = row[TableConstants.LibraryTypeIDStr].ToString();
                    txtTitleUpdate.Text = row[TableConstants.LibraryTypeTitleStr].ToString();
                    txtSKUUpdate.Text = row[TableConstants.LibraryTypeSKUStr].ToString();
                    txtPriceUpdate.Text = row[TableConstants.LibraryTypePriceStr].ToString();
                    txtISBNCodeUpdate.Text = row[TableConstants.LibraryTypeISBNCodeStr].ToString();
                    txtQuantityUpdate.Text = row[TableConstants.LibraryTypeQuantityStr].ToString();
                    txtQuantityBrokenUpdate.Text = row[TableConstants.LibraryTypeQuantityBrokenStr].ToString();
                    cbxAuthorUpdate.SelectedValue = row[TableConstants.LibraryTypeAuthorIDStr];
                    cbxPublisherUpdate.SelectedValue = row[TableConstants.LibraryTypePublisherIDStr];
                    cbxEditionUpdate.SelectedValue = row[TableConstants.LibraryTypeEditionIDStr];
                    cbxEditorUpdate.SelectedValue = row[TableConstants.LibraryTypeEditorIDStr];
                    cbxFormatUpdate.SelectedValue = row[TableConstants.LibraryTypeFormatIDStr];
                    cbxLanguageUpdate.SelectedValue = row[TableConstants.LibraryTypeLanguageIDStr];
                    cbxCategoryUpdate.SelectedValue = row[TableConstants.LibraryTypeCategoryIDStr];
                    cbxCopyrightUpdate.SelectedValue = row[TableConstants.LibraryTypeCopyrightIDStr];
                    cbxStatusUpdate.SelectedValue = row[TableConstants.LibraryTypeStatusIDStr];
                    dtpPublishDateUpdate.Value = Convert.ToDateTime(row[TableConstants.LibraryTypePublishDateStr]);
                }
            }
        }

        private void LoadSearchTab(string title)
        {
            tcNationalLibrary.SelectedIndex = 0;
            txtSearchByTitle.Text = title;
            var storedGetLibraryType = BuildingStoredProcHelper.BuildStoredStrSearchByTitle(title);
            HandlingDataHelper.LoadDataToGrid(dgvSearchByTitle, DataGridViewHeader.listOfColumnsType, storedGetLibraryType);
            lblSearchByTitleCount.Text = $"Total: {dgvSearchByTitle.RowCount} record(s)";
        }

        #endregion

        #region Import

        private void btnImport_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtFile.Text))
            {
                MessageBox.Show("File cannot be empty!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                rtbImportData.Text = $"Starting...{Environment.NewLine}{Environment.NewLine}";
                using (var reader = new StreamReader(txtFile.Text))
                {
                    using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                    {
                        var records = csv.GetRecords<CSVImportHeader>();
                        foreach (var record in records)
                        {
                            var recordTemp = record;
                            var strSeparators = new string[] { "\n" };
                            var listAuthorNames = record.AuthorName.Split(strSeparators, StringSplitOptions.None);
                            var listAuthorEmails = record.AuthorEmail.Split(strSeparators, StringSplitOptions.None);
                            var author = new Dictionary<string, string>();
                            for (int i = 0; i < listAuthorNames.Length; i++)
                            {
                                // Write process
                                rtbImportData.Text += $"Processing... ID: {recordTemp.ID} | Title: {recordTemp.Title} | Publisher: {recordTemp.PublisherName} | ISBN: {recordTemp.ISBNCode}{Environment.NewLine}";

                                recordTemp.AuthorName = listAuthorNames[i];
                                recordTemp.AuthorEmail = listAuthorEmails[i];
                                var storedImportCSV = BuildingStoredProcHelper.ImportDataFromCSV(recordTemp);
                                var result = HandlingDataHelper.ImportData(storedImportCSV);

                                // Write result
                                rtbImportData.Text += $"{result}{Environment.NewLine}{Environment.NewLine}";
                                rtbImportData.Text += $"=================================================={Environment.NewLine}{Environment.NewLine}";
                            }
                        }
                    }
                }
                rtbImportData.Text += $"Finished!";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error when importing. Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                InitialDirectory = @"D:\",
                Title = "Browse Text Files",

                CheckFileExists = true,
                CheckPathExists = true,

                DefaultExt = "csv",
                Filter = "csv files (*.csv)|*.csv",
                FilterIndex = 2,
                RestoreDirectory = true,

                ReadOnlyChecked = true,
                ShowReadOnly = true
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                txtFile.Text = openFileDialog.FileName;
                txtFile.Enabled = false;
            }
        }

        #endregion
    }
}
