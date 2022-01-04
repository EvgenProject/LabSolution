using Core.Models;
using GemBox.Spreadsheet;
using System.Data;
using System.IO;

namespace Core.Helper
{
    public class ExcelHelper : DocomentHelper
    {
        static ExcelHelper()
        {
            SpreadsheetInfo.SetLicense("FREE-LIMITED-KEY");
        }

        public static DataTable GetTableFromExcelFile(
            string filePath,
            int sheetNumber = 0,
            int numberOfRows = 10)
        {

            var workbook = ExcelFile.Load(Path.GetFullPath(filePath));
            var worksheet = workbook.Worksheets[sheetNumber];

            var options = new ExtractToDataTableOptions(0, 0, numberOfRows);
            options.ExtractDataOptions = ExtractDataOptions.StopAtFirstEmptyRow;
            options.ExcelCellToDataTableCellConverting += (sender, e) =>
            {
                if (!e.IsDataTableValueValid)
                {
                    e.DataTableValue = e.ExcelCell.Value == null ? null : e.ExcelCell.Value.ToString();
                    e.Action = ExtractDataEventAction.Continue;
                }
            };
            var dataTable = GetTableWithCustomColumns();
            worksheet.ExtractToDataTable(dataTable, options);
            return dataTable;
        }

        public static void SetDataToExcelFile(
            string filePath, 
            UserDto user,
            int startRow = 1,
            bool columnHeaders = true)
        {
            var workbook = ExcelFile.Load(Path.GetFullPath(filePath));
            var worksheet = workbook.Worksheets[0];
            var dataTable = GetTableWithCustomColumns();
            dataTable.Rows.Add(new object[] { user.FirstName, user.LastName, user.Age,user.Email});

            var options = new InsertDataTableOptions()
            {
                ColumnHeaders = columnHeaders,
                StartRow = startRow - 1
            };

            worksheet.InsertDataTable(dataTable, options);
            workbook.Save(filePath);
        }

        private static DataTable GetTableWithCustomColumns()
        {
            return new DataTable
            {
                Columns =
                {
                    { "First Name", typeof(string) },
                    { "Last Name", typeof(string) },
                    { "Age", typeof(string) },
                    { "Email", typeof(string) },
                }
            };
        }
    }
}
