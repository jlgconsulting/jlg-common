using System;
using System.Collections.Generic;
using System.Linq;
using SpreadsheetLight;
using JlgCommon.Extensions;
using System.IO;

namespace JlgCommon.ExcelManager
{   
    public class ExcelReader
    {
        public const int INVALID_COLUMN_INDEX = -1;
        private SLDocument _excelDocument;
        
        public ExcelReader()
        {
            _excelDocument = new SLDocument();
        }

        public ExcelReader(SLDocument excelDocument)
        {
            _excelDocument = excelDocument;
        }

        public byte[] ReadExcelFileAsByteArray(string excelFilePath, bool deleteExcelFileAfterReading = false)
        {
            var excelFileByteArray = File.ReadAllBytes(excelFilePath);
            if (deleteExcelFileAfterReading)
            {
                File.Delete(excelFilePath);
            }
            return excelFileByteArray;
        }

        public string GetCellValueAsString(int rowIndex, int columnIndex)
        {
            return _excelDocument.GetCellValueAsString(rowIndex, columnIndex);
        }

        public double GetCellValueAsDouble(int rowIndex, int columnIndex)
        {
            return _excelDocument.GetCellValueAsDouble(rowIndex, columnIndex);
        }

        public int GetCellValueAsInt(int rowIndex, int columnIndex)
        {
            return _excelDocument.GetCellValueAsInt32(rowIndex, columnIndex);
        }

        public DateTime GetCellValueAsDateTime(int rowIndex, int columnIndex)
        {
            return _excelDocument.GetCellValueAsDateTime(rowIndex, columnIndex);
        }

        public List<int> GetColumnOrderedIndexes()
        {
            var columnIndexes = _excelDocument.GetCells()
                                    .OrderBy(coll => coll.Key.ColumnIndex)
                                    .Select(coll => coll.Key.ColumnIndex)
                                    .Distinct()
                                    .ToList();
            return columnIndexes;
        }
        
        public int GetNumberOfRows()
        {
            var rowsIndexes = _excelDocument.GetCells()
                                    .Select(coll => coll.Key.RowIndex)
                                    .Distinct()
                                    .ToList();
            return rowsIndexes.Count;
        }

        public List<string> GetRowValues(int rowIndex)
        {
            var rowValues = new List<string>();
            foreach (var columnIndex in GetColumnOrderedIndexes())
            {
                var cellValue = _excelDocument.GetCellValueAsString(rowIndex, columnIndex);
                rowValues.Add(cellValue);
            }
            return rowValues;
        }

        public List<string> GetColumnDistinctValues(int columnIndex)
        {
            var columnUniqueValues = new Dictionary<string, bool>();

            var numberOfRowsInSheet = GetNumberOfRows();
            //the first row is the column title
            var cells = _excelDocument.GetCells();
            for (int i = 2; i <= numberOfRowsInSheet; i++)
            {
                var cellValue = _excelDocument.GetCellValueAsString(i, columnIndex);
                if (!columnUniqueValues.ContainsKey(cellValue))
                {
                    columnUniqueValues.Add(cellValue, true);
                }
            }

            return columnUniqueValues.Keys.ToList();
        }

        public List<DateTime> GetColumnUniqueDates(int dateColumnIndex)
        {
            var columnUniqueValues = new Dictionary<DateTime, bool>();

            var numberOfRowsInSheet = GetNumberOfRows();
            //the first row is the column title
            for (int i = 2; i <= numberOfRowsInSheet; i++)
            {

                if (string.IsNullOrEmpty(_excelDocument.GetCellValueAsString(i, dateColumnIndex)))
                {
                    continue;
                }
                var cellValue = _excelDocument.GetCellValueAsDateTime(i, dateColumnIndex);
                if (!columnUniqueValues.ContainsKey(cellValue))
                {
                    columnUniqueValues.Add(cellValue, true);
                }
            }

            return columnUniqueValues.Keys.ToList();
        }

        public int GetIndexOfColumnByName(string columnName)
        {
            var columnIndexes = GetColumnOrderedIndexes();

            int cellIndex = INVALID_COLUMN_INDEX;
            foreach (var columnIndex in columnIndexes)
            {
                var cellValue = _excelDocument.GetCellValueAsString(1, columnIndex);
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

        public void SelectWorksheet(string worksheetName)
        {
            _excelDocument.SelectWorksheet(worksheetName);
        }

        public List<string> GetWorksheetNames()
        {
            return _excelDocument.GetWorksheetNames();
        } 

    }
}
