using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using JlgCommon.Extensions;
using SpreadsheetLight;
using DocumentFormat.OpenXml.Spreadsheet;
using OfficeOpenXml;

namespace JlgCommon.ExcelManager
{   
    public class ExcelReader
    {
        public const int INVALID_COLUMN_INDEX = -1;
        public string ExcelFilePath { get; set; }
        private SLDocument _excelDocument;
        //for using EPPlus library
        private ExcelPackage _excelPackage;

        internal ExcelReader()
        {
            _excelDocument = new SLDocument();       
        }

        internal ExcelReader(SLDocument excelDocument, ExcelPackage excelPackage)
        {
            _excelDocument = excelDocument;
            _excelPackage = excelPackage;
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

        public bool CellIsValidDateAfter1900(int rowIndex, int columnIndex)
        {
            return _excelDocument.GetCellValueAsDateTime(rowIndex, columnIndex) != new DateTime(1900, 1, 1);
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

        public int GetFirstNotEmptyRowIndex()
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

        public KeyValuePair<int, int> GetFirstRowIndexAndColumnContainingSpecificValue(string value)
        {
            for (int rowIndex = GetFirstNotEmptyRowIndex(); rowIndex<GetLastRowIndex(); rowIndex++)
            {
                var distinctValues = GetRowNotEmptyValuesWithColumnIndexes(rowIndex);
                foreach (var distinctValue in distinctValues)
                {
                    if (distinctValue.Value.Trim().ToLower().Equals(value.ToLower()))
                    {
                        return new KeyValuePair<int, int>(rowIndex, distinctValue.Key);
                    }
                }

            }
            return new KeyValuePair<int, int>(0,0);
        }

        public int GetLastRowIndex()
        {
            var lastRowIndex = _excelDocument.GetCells()
                .Where(coll => !coll.Value.IsEmpty && 
                    !_excelDocument.GetCellValueAsString(coll.Key.RowIndex, coll.Key.ColumnIndex).Equals(string.Empty))
                .Max(coll => coll.Key.RowIndex);

            return lastRowIndex;
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

        public Dictionary<int, string> GetRowValuesWithColumnIndexes(int rowIndex)
        {
            return GetColumnOrderedIndexes().
                ToDictionary(columnIndex => columnIndex, columnIndex => _excelDocument.GetCellValueAsString(rowIndex, columnIndex));
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

        public Dictionary<int, Dictionary<int, string>> GetValuesForWorksheet(string worksheetName)
        {
           
            var worksheet = _excelPackage.Workbook.Worksheets[worksheetName];            
            var cells = worksheet.Cells.ToList();        

            var rowValuesDictionary = new Dictionary<int, Dictionary<int, string>>();
            foreach (var cell in cells)
            {
                if (!rowValuesDictionary.ContainsKey(cell.Start.Row))
                {
                    rowValuesDictionary.Add(cell.Start.Row, new Dictionary<int, string>());
                }
                rowValuesDictionary[cell.Start.Row].Add(cell.Start.Column,cell.Text);
            }              
            return rowValuesDictionary;
        }

        public List<string> GetColumnDistinctStringValues(int columnIndex, int startRowIndex)
        {
            var columnUniqueValues = new Dictionary<string, bool>();

            var numberOfRowsInSheet = GetNumberOfRows();
            //the first row is the column title
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

        public Dictionary<string, int> GetColumnDistinctStringValuesWithRowIndexes(int columnIndex, int startRowIndex)
        {
            var values = new Dictionary<string , int>();

            var numberOfRowsInSheet = GetNumberOfRows();
            //the first row is the column title
            for (int i = startRowIndex; i <= numberOfRowsInSheet; i++)
            {
                var cellValue = _excelDocument.GetCellValueAsString(i, columnIndex);
                if (string.IsNullOrEmpty(cellValue))
                {
                    continue;
                }

                if (!values.ContainsKey(cellValue))
                {
                    values.Add(cellValue, i);
                }
            }

            return values;
        }

        public List<DateTime> GetColumnDistinctDates(int dateColumnIndex, int startRowIndex)
        {
            var columnUniqueValues = new Dictionary<DateTime, bool>();

            var numberOfRowsInSheet = GetNumberOfRows();
            for (int i = startRowIndex; i <= numberOfRowsInSheet; i++)
            {
                if (!CellIsValidDateAfter1900(i, dateColumnIndex))
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

        public SLStyle GetRowStyle(int rowIndex)
        {
            return _excelDocument.GetRowStyle(rowIndex);
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

        /*
         * creates a dictionary with the values of the first row as Keys and values of the second row as Values
         */

        public Dictionary<string, string> GetTwoRowValuesDictionary(int firstRowIndex, int secondRowIndex)
        {
            var dictionary = new Dictionary<string, string>();
            foreach (var columnIndex in GetColumnOrderedIndexes())
            {
                var firstValue = _excelDocument.GetCellValueAsString(firstRowIndex, columnIndex);
                var secondValue = _excelDocument.GetCellValueAsString(secondRowIndex, columnIndex);
                if (!string.IsNullOrEmpty(firstValue))
                {
                    dictionary.Add(firstValue, secondValue);
                }
            }

            return dictionary;
        }

        /*
         * receives a tuple of the value to search for and the rowIndex where to search that value
         * returns the columnIndexes of the values
         */

        public List<int> GetColumnIndexesForStringTuple(List<Tuple<string, int>> tuples)
        {
            var columnIndexes = new List<int>();
            foreach (var tuple in tuples)
            {
                var value = tuple.Item1;
                var rowIndex = tuple.Item2;
                columnIndexes.Add(GetColumnIndexesForSpecificStringValue(rowIndex, value).First());
            }
            return columnIndexes;
        }
    }
}
