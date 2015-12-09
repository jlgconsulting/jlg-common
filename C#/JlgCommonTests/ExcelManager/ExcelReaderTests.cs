using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JlgCommonTests.ExcelManager
{
    [TestClass]
    public class ExcelReaderTests
    {
        private JlgCommon.ExcelManager.ExcelManager _excelManager = new JlgCommon.ExcelManager.ExcelManager(ExcelManagerTests.ExcelReaderTestsFilePath);

        [TestMethod]
        public void GetCellValue()
        {
            _excelManager.Reader.SelectWorksheet("Page1");
            Assert.AreEqual("dan", _excelManager.Reader.GetCellValueAsString(2, 2), false);
            Assert.AreEqual("misailescu", _excelManager.Reader.GetCellValueAsString(2, 3), false);
            Assert.AreEqual(new DateTime(2015, 4, 7), _excelManager.Reader.GetCellValueAsDateTime(3, 5));
            Assert.AreEqual(9, _excelManager.Reader.GetCellValueAsInt(2, 7));
            Assert.AreEqual(7.3245, _excelManager.Reader.GetCellValueAsDouble(3, 7));
        }

        [TestMethod]
        public void GetColumnOrderedIndexes()
        {
            _excelManager.Reader.SelectWorksheet("Page1");
            CollectionAssert.AreEquivalent(new List<int>() { 1, 2, 3, 4, 5, 6, 7 },
                _excelManager.Reader.GetColumnOrderedIndexes());
        }

        [TestMethod]
        public void GetNumberOfRows()
        {
            _excelManager.Reader.SelectWorksheet("Page1");
            Assert.AreEqual(6, _excelManager.Reader.GetNumberOfRows());
        }

        [TestMethod]
        public void GetRowValues()
        {
            //42065 is the string for "02/03/2015"
            _excelManager.Reader.SelectWorksheet("Page1");
            CollectionAssert.AreEquivalent(
                new List<string> { "", "dan", "misailescu", "excel", "42065", "", "9" },
                _excelManager.Reader.GetRowValues(2));
        }

        [TestMethod]
        public void GetRowNotEmptyValues()
        {
            //42065 is the string for "02/03/2015"
            _excelManager.Reader.SelectWorksheet("Page1");
            CollectionAssert.AreEquivalent(
                new List<string> { "dan", "misailescu", "excel", "42065", "9" },
                _excelManager.Reader.GetRowNotEmptyValues(2));
        }

        [TestMethod]
        public void GetRowNotEmptyValuesWithColumnIndexes()
        {
            _excelManager.Reader.SelectWorksheet("Page3");
            var valuesDictionary = _excelManager.Reader.GetRowNotEmptyValuesWithColumnIndexes(3);
            CollectionAssert.AreEquivalent(valuesDictionary,
                new Dictionary<int, string>
                {
                    {2, "searchedString"},
                    {6, "searchedString"}
                });
        }

        [TestMethod]
        public void GetColumnDistinctStringValues()
        {
            _excelManager.Reader.SelectWorksheet("Page1");
            CollectionAssert.AreEquivalent(new List<string>() { "Library", "excel", "manager", "tests" },
                _excelManager.Reader.GetColumnDistinctStringValues(4, 1));

            CollectionAssert.AreEquivalent(new List<string>() { "excel", "manager", "tests" },
                  _excelManager.Reader.GetColumnDistinctStringValues(4, 2));
        }

        [TestMethod]
        public void GetColumnDistinctDates()
        {
            _excelManager.Reader.SelectWorksheet("Page1");
            CollectionAssert.AreEquivalent(new List<DateTime>() { new DateTime(2015, 3, 2), new DateTime(2015, 4, 7), new DateTime(2015, 5, 25) },
                _excelManager.Reader.GetColumnDistinctDates(5, 2));

            CollectionAssert.AreEquivalent(new List<DateTime>() { new DateTime(2015, 4, 7), new DateTime(2015, 3, 2) },
               _excelManager.Reader.GetColumnDistinctDates(5, 5));
        }

        [TestMethod]
        public void GetIndexOfColumnByCellContentString()
        {
            _excelManager.Reader.SelectWorksheet("Page1");
            Assert.AreEqual(5, _excelManager.Reader.GetIndexOfColumnByCellContentString("Dates"));
            Assert.AreEqual(4, _excelManager.Reader.GetIndexOfColumnByCellContentString("excel", 2));
            Assert.AreEqual(4, _excelManager.Reader.GetIndexOfColumnByCellContentString("manager", 3));
        }

        [TestMethod]
        public void GetWorksheetNames()
        {
            CollectionAssert.AreEquivalent(new List<string>() { "Page1", "Page2", "Page3" },
                  _excelManager.Reader.GetWorksheetNames());
        }

        [TestMethod]
        public void SelectWorksheet()
        {
            _excelManager.Reader.SelectWorksheet("Page2");
            Assert.AreEqual("Values", _excelManager.Reader.GetCellValueAsString(2, 1), false);
            Assert.AreEqual(3, _excelManager.Reader.GetCellValueAsInt(3, 1));
            Assert.AreEqual(14, _excelManager.Reader.GetCellValueAsInt(4, 1));
            Assert.AreEqual(7, _excelManager.Reader.GetCellValueAsInt(7, 1));
        }

        [TestMethod]
        public void GetFirstRowContainingValuesIndex()
        {
            _excelManager.Reader.SelectWorksheet("Page2");
            Assert.AreEqual(2, _excelManager.Reader.GetFirstNotEmptyRowIndex());
        }

        //[TestMethod]
        //public void GetRowCells()
        //{
        //    _excelManager.Reader.SelectWorksheet("Page3");
        //    var cells = _excelManager.Reader.GetRowCells(1);
        //    //Assert.AreEqual(2, _excelManager.Reader.GetFirstRowContainingValuesIndex());
        //}

        [TestMethod]
        public void GetMergedCells()
        {
            _excelManager.Reader.SelectWorksheet("Page3");
            var mergedCells = _excelManager.Reader.GetMergedCells();
            Assert.AreEqual(mergedCells.Count, 1);

            var firstMergedCells = mergedCells.First();
            Assert.AreEqual(firstMergedCells.IsValid, true);
            Assert.AreEqual(firstMergedCells.StartRowIndex, 1);
            Assert.AreEqual(firstMergedCells.EndRowIndex, 2);
            Assert.AreEqual(firstMergedCells.StartColumnIndex, 1);
            Assert.AreEqual(firstMergedCells.EndColumnIndex, 3);
        }

        [TestMethod]
        public void GetMergedCellsStartingRow()
        {
            _excelManager.Reader.SelectWorksheet("Page3");
            Assert.AreEqual(_excelManager.Reader.GetMergedCellsStartingRow(), 1);
        }

        [TestMethod]
        public void GetMergedCellsEndingRow()
        {
            _excelManager.Reader.SelectWorksheet("Page3");
            Assert.AreEqual(_excelManager.Reader.GetMergedCellsEndingRow(), 2);
        }

        [TestMethod]
        public void GetCellStyle()
        {
            _excelManager.Reader.SelectWorksheet("Page3");
            var style = _excelManager.Reader.GetCellStyle(1, 1);
            Assert.AreEqual(style.Font.FontName, "Arial");
            Assert.AreEqual(style.Font.FontSize, 10);
            Assert.AreEqual(style.Font.Bold, true);
            Assert.AreEqual(style.Alignment.Horizontal.ToString(), "Center");
            Assert.AreEqual(style.Alignment.Vertical.ToString(), "Bottom");
        }

        [TestMethod]
        public void GetColumnIndexesForSpecificStringValue()
        {
            _excelManager.Reader.SelectWorksheet("Page3");
            var columnIndexes = _excelManager.Reader.GetColumnIndexesForSpecificStringValue(3, "searchedString");
            CollectionAssert.AreEquivalent(columnIndexes, new List<int>() {2, 6});
        }

        [TestMethod]
        public void GetValuesForWorksheet()
        {
            var valuesDictionary = _excelManager.Reader.GetValuesForWorksheet("Page3");

            Assert.AreEqual(valuesDictionary[1][1], "Merged cells");
            Assert.AreEqual(valuesDictionary[3][2], "searchedString");
            Assert.AreEqual(valuesDictionary[3][6], "searchedString");            
        }

        [TestMethod]
        public void GetLastNonEmptyRowIndex()
        {
            var lastIndex = _excelManager.Reader.GetLastNonEmptyRowIndex("Page2");
            Assert.AreEqual(lastIndex, 7);
        }

        [TestMethod]
        public void GetNonEmptyRowsForWorksheet()
        {
            var valuesDictionary = _excelManager.Reader.GetValuesForWorksheet("Page3");

            Assert.AreEqual(valuesDictionary[1][1], "Merged cells");
            Assert.AreEqual(valuesDictionary[3][2], "searchedString");
            Assert.AreEqual(valuesDictionary[3][6], "searchedString");
        }

        [TestMethod]
        public void GetRowAndColumnForSpecificStringValue()
        {
            _excelManager.Reader.SelectWorksheet("Page3");
            var indexes = _excelManager.Reader.GetRowAndColumnForSpecificStringValue("searchedString");
            Assert.AreEqual(indexes.Item1, 3);
            Assert.AreEqual(indexes.Item2, 2);
        }

        [TestMethod]
        public void CellIsValidDateAfter1900()
        {
            _excelManager.Reader.SelectWorksheet("Page1");
            Assert.IsFalse(_excelManager.Reader.CellIsValidDateAfter1900(2, 3));
            Assert.IsTrue(_excelManager.Reader.CellIsValidDateAfter1900(4, 5));
        }

        [TestMethod]
        public void GetColumnDistinctStringValuesWithRowIndexes()
        {
            _excelManager.Reader.SelectWorksheet("Page2");
            var values = new Dictionary<string, int>
            {
                {"Values", 2} ,
                {"3", 3},
                {"14", 4},
                {"8", 5},
                {"10", 6},
                {"7", 7}
            };
           CollectionAssert.AreEqual(_excelManager.Reader.GetColumnDistinctStringValuesWithRowIndexes(1, 2), values);
        }


    }
}
