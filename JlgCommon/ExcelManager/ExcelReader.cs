using System;
using System.Collections.Generic;
using System.Linq;
using SpreadsheetLight;
using JlgCommon.Extensions;
using System.IO;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Spreadsheet;

namespace JlgCommon.ExcelManager
{   
    public class ExcelReader
    {
        public const int INVALID_COLUMN_INDEX = -1;
        public string ExcelFilePath { get; set; }
        private SLDocument _excelDocument;
        
        internal ExcelReader()
        {
            _excelDocument = new SLDocument();
        }

        internal ExcelReader(SLDocument excelDocument)
        {
            _excelDocument = excelDocument;            
        }

        public byte[] ReadExcelFileAsByteArray(bool deleteExcelFileAfterReading = false)
        {
            var excelFileByteArray = File.ReadAllBytes(ExcelFilePath);
            if (deleteExcelFileAfterReading)
            {
                File.Delete(ExcelFilePath);
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

            columnIndexes.Sort();

            return columnIndexes;
        }

        public int GetFirstRowContainingValuesIndex()
        {
            int rowIndex = 1;
            List<string> rowValues = GetRowNotEmptyValues(rowIndex);
            while (!rowValues.Any())
            {
                rowIndex++;
                rowValues = GetRowNotEmptyValues(rowIndex);
            }
            return rowIndex;
        }

        public int GetNumberOfRows()
        {
            var nrRows = _excelDocument.GetCells()
                                    .Select(coll => coll.Key.RowIndex)
                                    .Distinct()
                                    .Count();
            return nrRows;
        }

        public List<string> GetRowNotEmptyValues(int rowIndex)
        {
            var rowValues = new List<string>();
            foreach (var columnIndex in GetColumnOrderedIndexes())
            {
                var cellValue = _excelDocument.GetCellValueAsString(rowIndex, columnIndex);
                if (!cellValue.Equals(""))
                {
                    rowValues.Add(cellValue);
                }
            }
            return rowValues;
        }

        public Dictionary<int, string> GetRowNotEmptyValuesWithColumnIndexes(int rowIndex)
        {
            var rowValues = new Dictionary<int, string>();
            foreach (var columnIndex in GetColumnOrderedIndexes())
            {
                var cellValue = _excelDocument.GetCellValueAsString(rowIndex, columnIndex);
                if (!cellValue.Equals(""))
                {
                    rowValues.Add(columnIndex, cellValue);
                }
            }
            return rowValues;
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

        public List<string> GetColumnDistinctStringValues(int columnIndex, int startRowIndex)
        {
            var columnUniqueValues = new Dictionary<string, bool>();

            var numberOfRowsInSheet = GetNumberOfRows();
            //the first row is the column title
            var cells = _excelDocument.GetCells();
            for (int i = startRowIndex; i <= numberOfRowsInSheet; i++)
            {   
                var cellValue = _excelDocument.GetCellValueAsString(i, columnIndex);
                if (string.IsNullOrEmpty(cellValue))
                {
                    continue;
                }

                if (!columnUniqueValues.ContainsKey(cellValue))
                {
                    columnUniqueValues.Add(cellValue, true);
                }
            }

            return columnUniqueValues.Keys.ToList();
        }

        public List<DateTime> GetColumnDistinctDates(int dateColumnIndex, int startRowIndex)
        {
            var columnUniqueValues = new Dictionary<DateTime, bool>();

            var numberOfRowsInSheet = GetNumberOfRows();
            for (int i = startRowIndex; i <= numberOfRowsInSheet; i++)
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

        public int GetIndexOfColumnByCellContentString(string cellContent, int rowIndex=1)
        {
            var columnIndexes = GetColumnOrderedIndexes();

            int cellIndex = INVALID_COLUMN_INDEX;
            foreach (var columnIndex in columnIndexes)
            {
                var cellValue = _excelDocument.GetCellValueAsString(rowIndex, columnIndex);
                if (cellValue.LowerCaseAndIgnoreSpaces() == cellContent.LowerCaseAndIgnoreSpaces())
                {
                    cellIndex = columnIndex;
                    break;
                }
            }

            if (cellIndex == INVALID_COLUMN_INDEX)
            {
                throw new Exception(string.Format("Could not find a columnName index for columnName {0}", cellContent));
            }
            return cellIndex;
        }

        public bool SelectWorksheet(string worksheetName)
        {
            return _excelDocument.SelectWorksheet(worksheetName);
        }

        public List<string> GetWorksheetNames()
        {
            return _excelDocument.GetWorksheetNames();
        }

        public List<KeyValuePair<SLCellPoint, SLCell>> GetRowCells(int rowIndex)
        {
            var cells = _excelDocument.GetCells()
                                    .Where(coll => coll.Key.RowIndex == rowIndex)
                                    .ToList();
            return cells;
        }

        public List<SLMergeCell> GetMergedCells()
        {
            return _excelDocument.GetWorksheetMergeCells();
        }

        public int GetMergedCellsStartingRow()
        {
            return _excelDocument.GetWorksheetMergeCells().Min(t => t.StartRowIndex);
        }

        public int GetMergedCellsEndingRow()
        {
            return _excelDocument.GetWorksheetMergeCells().Max(t => t.EndRowIndex);
        }

        public SLStyle GetCellStyle(int rowIndex, int columnIndex)
        {
           return _excelDocument.GetCellStyle(rowIndex, columnIndex);
        }

        public double GetCellWidth(int columnIndex)
        {
            return _excelDocument.GetColumnWidth(columnIndex);
        }

        public List<int> GetColumnIndexesForSpecificStringValue(int rowIndex, string value)
        {
            var columnIndexes = new List<int>();
            foreach (var columnIndex in GetColumnOrderedIndexes())
            {
                var cellValue = _excelDocument.GetCellValueAsString(rowIndex, columnIndex);
                if (cellValue.Equals(value))
                {
                    columnIndexes.Add(columnIndex);
                }
            }
            return columnIndexes;
        }

        public List<List<string>> GetNonEmptyValuesForWorksheet(string worksheetName)
        {
            var values = new List<List<string>>();

            var worksheetFound = _excelDocument.SelectWorksheet(worksheetName);
            if (!worksheetFound)
                throw new Exception("Could not found worksheet" + worksheetName);
            int rowCount = GetNumberOfRows();

            for (int i = 0; i < rowCount; i++)
            {
                var rowValues = GetRowNotEmptyValues(i + 1);
                values.Add(rowValues);
            }

            return values;
        }

        public Tuple<int, int> GetRowAndColumnForSpecificStringValue(string field)
        {
            int firstRow = 1;
            int nRows = GetNumberOfRows();

            for (int i = firstRow; i <= nRows; ++i)
            {
                var columnIndexList = GetColumnIndexesForSpecificStringValue(i, field);
                if (!columnIndexList.Any())
                {
                    continue;
                }
                
                int j = columnIndexList.First();
                string cellValue = GetCellValueAsString(i, j);

                if (field.Equals(cellValue))
                {
                    return new Tuple<int, int>(i, j);
                }
            }
            return null;

        } 


    }
}
