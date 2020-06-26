using NationalLibrary.Constants;
using NationalLibrary.DbContext;
using NationalLibrary.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace NationalLibrary.Helper
{
    public class HandlingDataHelper
    {
        public static void AddData(DataGridView dgv, List<string> listOfColumns, string textboxValue, string errorMess, string query, string tableName, bool loadGrid = true)
        {
            if (string.IsNullOrEmpty(textboxValue))
            {
                MessageBox.Show($"{errorMess} cannot be empty!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                OracleDataController.ConnectDB(OracleDataController.GetDBConnection());
                OracleDataController.Insert(query);
                MessageBox.Show("Insert successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (loadGrid)
                {
                    LoadDataToGrid(dgv, listOfColumns, tableName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void UpdateData(DataGridView dgv, List<string> listOfColumns, string textboxValue, string errorMess, string query, string tableName, bool loadGrid = true)
        {
            if (string.IsNullOrEmpty(textboxValue))
            {
                MessageBox.Show($"{errorMess} cannot be empty!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                OracleDataController.ConnectDB(OracleDataController.GetDBConnection());
                OracleDataController.Update(query);
                MessageBox.Show("Update successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (loadGrid)
                {
                    LoadDataToGrid(dgv, listOfColumns, tableName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void DeleteData(DataGridView dgv, List<string> listOfColumns, string textboxValue, string errorMess, string query, string tableName, bool loadGrid = true)
        {
            if (string.IsNullOrEmpty(textboxValue))
            {
                MessageBox.Show($"{errorMess} cannot be empty!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                OracleDataController.ConnectDB(OracleDataController.GetDBConnection());
                OracleDataController.Delete(query);
                MessageBox.Show("Delete successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (loadGrid)
                {
                    LoadDataToGrid(dgv, listOfColumns, tableName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        public static void LoadDataToGrid(DataGridView dgv, List<string> listOfColumns, string tableName)
        {
            OracleDataController.ConnectDB(OracleDataController.GetDBConnection());
            var query = $"SELECT * FROM {tableName}";
            dgv.DataSource = OracleDataController.GetDataGridView(query);
            for (int i = 0; i < dgv.Columns.Count; i++)
            {
                dgv.Columns[i].HeaderText = listOfColumns[i].ToString();
            }
        }

        public static void LoadDataToGrid(DataGridView dgv, List<string> listOfColumns, StoredProcedure storedProcedure)
        {
            OracleDataController.ConnectDB(OracleDataController.GetDBConnection());
            dgv.DataSource = OracleDataController.ExecuteStoreProc(storedProcedure);
            for (int i = 0; i < dgv.Columns.Count; i++)
            {
                dgv.Columns[i].HeaderText = listOfColumns[i].ToString();
            }
        }

        public static void ImportData(StoredProcedure storedProcedure)
        {
            OracleDataController.ConnectDB(OracleDataController.GetDBConnection());
            var result = OracleDataController.ExecuteStoreProc(storedProcedure);
        }

        public static DataTable LoadDataToForm(StoredProcedure storedProcedure)
        {
            OracleDataController.ConnectDB(OracleDataController.GetDBConnection());
            return OracleDataController.ExecuteStoreProc(storedProcedure);
        }

        public static void LoadDataForNewType(List<TableName> listOfTables)
        {
            foreach (var item in listOfTables)
            {
                LoadDataToCombobox(item.ComboBox, item.ComboBoxUpdate, item.Name, item.ValueColStr, item.DisplayColStr);
            }
        }

        public static void LoadDataToCombobox(ComboBox cbx, ComboBox cbxUpdate, string tableName, string valueName, string displayName)
        {
            OracleDataController.ConnectDB(OracleDataController.GetDBConnection());
            var query = $"SELECT * FROM {tableName} WHERE LibraryTypeStatusID = 1";
            if (tableName.Equals(TableConstants.LibraryTypeStatus))
            {
                query = $"SELECT * FROM {tableName}";
            }

            // Bind data to Combobox
            var dt = OracleDataController.GetDataGridView(query);
            cbx.DataSource = dt;
            cbx.ValueMember = valueName;
            cbx.DisplayMember = displayName;

            cbxUpdate.DataSource = dt;
            cbxUpdate.ValueMember = valueName;
            cbxUpdate.DisplayMember = displayName;
        }
    }
}
