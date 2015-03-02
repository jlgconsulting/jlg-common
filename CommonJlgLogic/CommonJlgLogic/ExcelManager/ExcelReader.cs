using System;
using System.Collections.Generic;
using System.Linq;
using SpreadsheetLight;
using CommonJlgLogic.Extensions;

namespace Domain
{
    //http://spreadsheetlight.com/sample-code/
    //https://erictummers.wordpress.com/2014/08/28/get-spreadsheetlight-working/
    public class ExcelReader
    {
        public const int INVALID_COLUMN_INDEX = -1;

        public ExcelReader()
        {
            
        }

        public List<int> GetColumnOrderedIndexes(SLDocument excelDocument)
        {
            var columnIndexes = excelDocument.GetCells()
                                    .OrderBy(coll => coll.Key.ColumnIndex)
                                    .Select(coll => coll.Key.ColumnIndex)
                                    .Distinct()
                                    .ToList();
            return columnIndexes;
        }
        public int GetNumberOfRows(SLDocument excelDocument)
        {
            var rowsIndexes = excelDocument.GetCells()
                                    .Select(coll => coll.Key.RowIndex)
                                    .Distinct()
                                    .ToList();
            return rowsIndexes.Count;
        }

        public List<string> GetRowValues(SLDocument excelDocument, int rowIndex)
        {
            var rowValues = new List<string>();
            foreach (var columnIndex in GetColumnOrderedIndexes(excelDocument))
            {
                var cellValue = excelDocument.GetCellValueAsString(rowIndex, columnIndex);
                rowValues.Add(cellValue);
            }
            return rowValues;
        }

        public List<string> GetColumnDistinctValues(SLDocument excelDocument, int columnIndex)
        {
            var columnUniqueValues = new Dictionary<string, bool>();

            var numberOfRowsInSheet = GetNumberOfRows(excelDocument);
            //the first row is the column title
            var cells = excelDocument.GetCells();
            for (int i = 2; i <= numberOfRowsInSheet; i++)
            {
                var cellValue = excelDocument.GetCellValueAsString(i, columnIndex);
                if (!columnUniqueValues.ContainsKey(cellValue))
                {
                    columnUniqueValues.Add(cellValue, true);
                }
            }

            return columnUniqueValues.Keys.ToList();
        }

        public List<DateTime> GetColumnUniqueDates(SLDocument excelDocument, int dateColumnIndex)
        {
            var columnUniqueValues = new Dictionary<DateTime, bool>();

            var numberOfRowsInSheet = GetNumberOfRows(excelDocument);
            //the first row is the column title
            for (int i = 2; i <= numberOfRowsInSheet; i++)
            {

                if (string.IsNullOrEmpty(excelDocument.GetCellValueAsString(i, dateColumnIndex)))
                {
                    continue;
                }
                var cellValue = excelDocument.GetCellValueAsDateTime(i, dateColumnIndex);
                if (!columnUniqueValues.ContainsKey(cellValue))
                {
                    columnUniqueValues.Add(cellValue, true);
                }
            }

            return columnUniqueValues.Keys.ToList();
        }

        public int GetIndexOfColumnByName(SLDocument excelDocument, string columnName)
        {
            var columnIndexes = GetColumnOrderedIndexes(excelDocument);

            int cellIndex = INVALID_COLUMN_INDEX;
            foreach (var columnIndex in columnIndexes)
            {
                var cellValue = excelDocument.GetCellValueAsString(1, columnIndex);
                if (cellValue.LowerCaseAndIgnoreSpaces() == columnName.LowerCaseAndIgnoreSpaces())
                {
                    cellIndex = columnIndex;
                    break;
                }
            }

            if (cellIndex == INVALID_COLUMN_INDEX)
            {
                throw new Exception(string.Format("Could not find a columnName index for columnName {0}", columnName));
            }
            return cellIndex;
        }      

    }
}
