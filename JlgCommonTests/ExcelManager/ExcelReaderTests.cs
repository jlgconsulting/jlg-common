using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JlgCommon.ExcelManager;

namespace JlgCommonTests.Extensions
{
    [TestClass]
    public class ExcelReaderTests
    {
        private ExcelManager _excelManager = new ExcelManager(ExcelManagerTests.ExcelReaderTestsFilePath);
        
        [TestMethod]
        public void GetCellValue()
        {
            Assert.AreEqual("dan", _excelManager.Reader.GetCellValueAsString(2, 2), false);
            Assert.AreEqual("misailescu", _excelManager.Reader.GetCellValueAsString(2, 3), false);
            Assert.AreEqual(new DateTime(2015, 4, 7), _excelManager.Reader.GetCellValueAsDateTime(3, 5));
            Assert.AreEqual(9, _excelManager.Reader.GetCellValueAsInt(2, 7));
            Assert.AreEqual(7.3245, _excelManager.Reader.GetCellValueAsDouble(3, 7));
        }

        [TestMethod]
        public void GetColumnOrderedIndexes()
        {
            CollectionAssert.AreEquivalent(new List<int>() { 1, 2, 3, 4, 5, 6, 7 },
                _excelManager.Reader.GetColumnOrderedIndexes());         
        }

        [TestMethod]
        public void GetNumberOfRows()
        {
            Assert.AreEqual(6, _excelManager.Reader.GetNumberOfRows());
        }

        [TestMethod]
        public void GetColumnDistinctStringValues()
        {
            CollectionAssert.AreEquivalent(new List<string>() { "Library", "excel", "manager", "tests" },
                _excelManager.Reader.GetColumnDistinctStringValues(4, 1));

            CollectionAssert.AreEquivalent(new List<string>() { "excel", "manager", "tests" },
                  _excelManager.Reader.GetColumnDistinctStringValues(4, 2));            
        }

        [TestMethod]
        public void GetColumnDistinctDates()
        {
            CollectionAssert.AreEquivalent(new List<DateTime>() { new DateTime(2015, 3, 2), new DateTime(2015, 4, 7), new DateTime(2015, 5, 25) },
                _excelManager.Reader.GetColumnDistinctDates(5, 2));

            CollectionAssert.AreEquivalent(new List<DateTime>() { new DateTime(2015, 4, 7), new DateTime(2015, 3, 2) },
               _excelManager.Reader.GetColumnDistinctDates(5, 5));               
        }

        [TestMethod]
        public void GetIndexOfColumnByCellContentString()
        {
            Assert.AreEqual(5, _excelManager.Reader.GetIndexOfColumnByCellContentString("Dates"));
            Assert.AreEqual(4, _excelManager.Reader.GetIndexOfColumnByCellContentString("excel", 2));
            Assert.AreEqual(4, _excelManager.Reader.GetIndexOfColumnByCellContentString("manager", 3));
        }

        [TestMethod]
        public void GetWorksheetNames()
        {
            CollectionAssert.AreEquivalent(new List<string>() { "Page1", "Page2" },
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
    }
}
