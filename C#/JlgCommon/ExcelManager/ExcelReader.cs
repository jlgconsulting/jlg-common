using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using JlgCommon.Extensions;
using SpreadsheetLight;
using DocumentFormat.OpenXml.Spreadsheet;
using JlgCommon.Common;
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
            var columnIndexes = new SortedSet<int>();
            var cells = _excelDocument.GetCells();
            foreach (var row in cells)
            {
                foreach (var columnIndex in row.Value.Keys)
                {
                    if (!columnIndexes.Contains(columnIndex))
                    {
                        columnIndexes.Add(columnIndex);
                    }
                }
            }

//            var columnIndexes = _excelDocument.GetCells()
//                                    .Select(coll => coll.Value.Keys)
//                                    .Distinct()
//                                    .ToList();

            return columnIndexes.ToList();
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
            var nrRows = _excelDocument.GetCells().Select(t => t.Key).Distinct().Count();
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
            var lastRowIndex =
                _excelDocument.GetCells()
                    .Where(row => row.Value.Values.All(cell => !string.IsNullOrEmpty(cell.CellText)))
                    .Max(t => t.Key);

//            var lastRowIndex = _excelDocument.GetCells()
//                .Where(coll => !coll.Value.Va.IsEmpty && 
//                    !_excelDocument.GetCellValueAsString(coll.Key.RowIndex, coll.Key.ColumnIndex).Equals(string.Empty))
//                .Max(coll => coll.Key.RowIndex);

            return lastRowIndex;
        }

        public int GetLastNonEmptyRowIndex(string worksheetName)
        {
            var worksheet = _excelPackage.Workbook.Worksheets[worksheetName];
            var cells = worksheet.Cells.Where(cell=>!string.IsNullOrEmpty(cell.Text)).ToList();

            return cells.Max(cell => cell.Start.Row);
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

        private SortedDictionary<int, SortedDictionary<int, string>> GetSortedDictionary(List<ExcelRangeBase> cells)
        {
            var rowValuesDictionary = new SortedDictionary<int, SortedDictionary<int, string>>();
            foreach (var cell in cells)
            {
                if (!rowValuesDictionary.ContainsKey(cell.Start.Row))
                {
                    rowValuesDictionary.Add(cell.Start.Row, new SortedDictionary<int, string>());
                }
                rowValuesDictionary[cell.Start.Row].Add(cell.Start.Column, cell.Text.Trim());
            }
            return rowValuesDictionary;
        }

        /// <summary>
        /// Gets all values from worksheet at once
        /// Returns dictionary:
        /// Key: Row Index
        /// Value: dictionary: 
        ///     Key = Column Index
        ///     Value = value of cell as a string
        /// </summary>
        /// <param name="worksheetName">the name of the worksheet where to read from</param>
        /// <returns>Sorted dictionary of values</returns>
        public SortedDictionary<int, SortedDictionary<int, string>> GetValuesForWorksheet(string worksheetName)
        {
            var cells = GetWorksheetContent(worksheetName);

            var valuesDictionary = GetSortedDictionary(cells);
            
            //remove empty rows (rows that contain only empty values)
            var emptyRowKeys =
                valuesDictionary.Where(row => row.Value.Values.All(string.IsNullOrEmpty))
                    .Select(t => t.Key)
                    .ToList();
            foreach (var rowKey in emptyRowKeys)
            {
                valuesDictionary.Remove(rowKey);
            }

            return valuesDictionary;
        }

        /// <summary>
        /// Gets all not empty values from worksheet at once
        /// Returns dictionary:
        /// Key: Row Index
        /// Value: dictionary: 
        ///     Key = Column Index
        ///     Value = value of cell as a string
        /// </summary>
        /// <param name="worksheetName">the name of the worksheet where to read from</param>
        /// <returns>Sorted dictionary of values</returns>
        public SortedDictionary<int, SortedDictionary<int, string>> GetNotEmptyValuesForWorksheet(string worksheetName)
        {
            var cells = GetWorksheetNotEmptyContent(worksheetName);

            return GetSortedDictionary(cells);
        }   

        public Dictionary<int, Dictionary<int, string>> GetNonEmptyRowsForWorksheet(string worksheetName)
        {

            var worksheet = _excelPackage.Workbook.Worksheets[worksheetName];
            var cells = worksheet.Cells.Where(cell=>!string.IsNullOrEmpty(cell.Text)).ToList();

            var rowValuesDictionary = new Dictionary<int, Dictionary<int, string>>();
            foreach (var cell in cells)
            {
                if (!rowValuesDictionary.ContainsKey(cell.Start.Row))
                {
                    rowValuesDictionary.Add(cell.Start.Row, new Dictionary<int, string>());
                }
                rowValuesDictionary[cell.Start.Row].Add(cell.Start.Column, cell.Text);
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

//        public List<KeyValuePair<SLCellPoint, SLCell>> GetRowCells(int rowIndex)
//        {
//            var cells = _excelDocument.GetCells()
//                                    .Where(coll => coll.Key.RowIndex == rowIndex)
//                                    .ToList();
//            return cells;
//        }

        public List<SLMergeCell> GetMergedCells()
        {
            return _excelDocument.GetWorksheetMergeCells();
        }

        /// <summary>
        /// Gets the first row index of a merged cells structure
        /// </summary>
        /// <returns>the first row index or -1 if no cells have been merged</returns>
        public int GetMergedCellsStartingRow()
        {
            var mergedCells = _excelDocument.GetWorksheetMergeCells();
            if (mergedCells.Any())
                return mergedCells.Min(t => t.StartRowIndex);

            return -1;
        }

        /// <summary>
        /// Gets the last row index of a merged cells structure
        /// </summary>
        /// <returns>the last row index or -1 if no cells have been merged</returns>
        public int GetMergedCellsEndingRow()
        {
            var mergedCells = _excelDocument.GetWorksheetMergeCells();
            if (mergedCells.Any())
                return mergedCells.Max(t => t.EndRowIndex);

            return -1;
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

        public List<int> GetColumnIndexesContainingStringValue(int rowIndex, string value)
        {
            var columnIndexes = new List<int>();
            foreach (var columnIndex in GetColumnOrderedIndexes())
            {
                var cellValue = _excelDocument.GetCellValueAsString(rowIndex, columnIndex);
                if (cellValue.Contains(value))
                {
                    columnIndexes.Add(columnIndex);
                }
            }
            return columnIndexes;
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

//        /// <summary>
//        /// Get data from worksheet as list of rows, each row being a list of string
//        /// </summary>
//        /// <param name="worksheetName">the name of the worksheet</param>
//        /// <returns></returns>
//        public Dictionary<int, Dictionary<int, string>> GetWorksheetDataAsStringList(string worksheetName)
//        {
//            var worksheetContent = GetWorksheetContent(worksheetName);
//            var nonEmptyContent = worksheetContent.Where(t => !string.IsNullOrEmpty(t.Value.ToString())).ToList();
//            if (!nonEmptyContent.Any())
//                return null;
//
//            var emptyStart = nonEmptyContent.Where(t => t.Start == null).ToList();
//            if (emptyStart.Any())
//                throw new Exception(nonEmptyContent.Select(t=>t.FullAddress).ToList().ToString());
//
//            int minRowIndex = nonEmptyContent.Min(t => t.Start.Row);
//            int minColumnIndex = nonEmptyContent.Min(t => t.Start.Column);
//            int maxRowIndex = nonEmptyContent.Max(t => t.End.Row);
//            int maxColumnIndex = nonEmptyContent.Max(t => t.End.Column);
//
//            var worksheetData = new Dictionary<int, Dictionary<int, string>>();
//            for (int rowIndex = minRowIndex; rowIndex <= maxRowIndex; rowIndex++)
//            {
//                var rowContent = worksheetContent.Where(t => t.Start.Row == rowIndex).ToList();
//
//                var rowData =
//                    rowContent.Where(t => t.Start.Column >= minColumnIndex && t.End.Column <= maxColumnIndex)
//                        .OrderBy(t => t.Start.Column)
//                        .ToDictionary(t => t.Start.Column, t => t.Value.ToString().Trim());
//
//                worksheetData.Add(rowIndex, rowData);
//            }
//
//            return worksheetData;
//        } 

        private ExcelRangeBase GetWorksheetCells(string worksheetName)
        {
            var worksheetNames = _excelPackage.Workbook.Worksheets.Select(t => t.Name).ToList();
            if (!worksheetNames.Contains(worksheetName))
            {
                throw new Exception(string.Format(Constants.WorksheetNotFound, worksheetName));
            }

            var worksheet = _excelPackage.Workbook.Worksheets[worksheetName];
            return worksheet.Cells;
        }

        public List<ExcelRangeBase> GetWorksheetContent(string worksheetName)
        {
            return GetWorksheetCells(worksheetName).Where(cell => cell.Text != null).ToList();
        }

        public List<ExcelRangeBase> GetWorksheetNotEmptyContent(string worksheetName)
        {
           return GetWorksheetCells(worksheetName).Where(cell => !string.IsNullOrEmpty(cell.Text.Trim())).ToList();
        } 

        public Tuple<int, int> GetRowAndColumnContainingStringValue(string value)
        {
            int firstRow = 1;
            int nRows = GetNumberOfRows();

            for (int i = firstRow; i <= nRows; ++i)
            {
                var columnIndexList = GetColumnIndexesContainingStringValue(i, value);
                if (!columnIndexList.Any())
                {
                    continue;
                }

                int j = columnIndexList.First();
                string cellValue = GetCellValueAsString(i, j);

                if (cellValue.Contains(value))
                {
                    return new Tuple<int, int>(i, j);
                }
            }
            return null;
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
                var columnIndexesForValue = GetColumnIndexesForSpecificStringValue(rowIndex, value);
                if(!columnIndexesForValue.Any())
                    throw new Exception(string.Format("Value not found: {0}, row index: {1}", value, rowIndex));

                columnIndexes.Add(columnIndexesForValue.First());
            }
            return columnIndexes;
        }

        /*
         * receives a dictionary of the values to search for and the rowIndex where to search that values
         * returns a dictionary of the values and the columnIndex where the values were found
         */
        public Dictionary<string, int> GetColumnIndexesForStringList(Dictionary<string, int> dictionary)
        {
            var columnIndexes = new Dictionary<string, int>();
            foreach (var item in dictionary)
            {
                var value = item.Key;
                var rowIndex = item.Value;
                var columnIndexesForValue = GetColumnIndexesForSpecificStringValue(rowIndex, value);
                if (!columnIndexesForValue.Any())
                    continue;

                columnIndexes.Add(value, columnIndexesForValue.First());
            }
            return columnIndexes;
        }
    }
}
