using System;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Spreadsheet;
using JlgCommon.ExcelManager.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpreadsheetLight;
using Color = System.Drawing.Color;

namespace JlgCommonTests.ExcelManager
{
    [TestClass]
    public class ExcelWriterTests
    {
        private string _author = "Dan Misailescu";

        [TestMethod]
        public void SetCellValue()
        {
            var excelManager = new JlgCommon.ExcelManager.ExcelManager();
            excelManager.Writer.SetCellValue(1, 1, _author);
            Assert.AreEqual(_author, excelManager.Reader.GetCellValueAsString(1, 1));

            var date = new DateTime(2015, 3, 1);
            excelManager.Writer.SetCellValue(2, 4, date);
            Assert.AreEqual(date, excelManager.Reader.GetCellValueAsDateTime(2, 4));

            double nr1 = 7.823;
            excelManager.Writer.SetCellValue(3, 2, nr1);
            Assert.AreEqual(nr1, excelManager.Reader.GetCellValueAsDouble(3, 2));

            int nr2 = 3;
            excelManager.Writer.SetCellValue(4, 1, nr2);
            Assert.AreEqual(nr2, excelManager.Reader.GetCellValueAsDouble(4, 1));
        }

        [TestMethod]
        public void AddLineOrColumnChart()
        {
            var excelManager = new JlgCommon.ExcelManager.ExcelManager();

            var chartName = "Custom line chart";
            var lineChart = new LineOrColumnChartForExcel();
            lineChart.ChartName = chartName;
            lineChart.Series.Add(new SerieForExcel()
            {
                Name = "Delevopment",
                Values = new List<StringDoublePair>(){
                    new StringDoublePair("x1", 3.2),
                    new StringDoublePair("x2", 1.1),
                    new StringDoublePair("x3", 3.9),
                    new StringDoublePair("x4", 8.2),
                    new StringDoublePair("x5", 4.6),
                }
            });

            lineChart.Series.Add(new SerieForExcel()
            {
                Name = "Maintenance",
                Values = new List<StringDoublePair>(){
                    new StringDoublePair("x1", 12.2),
                    new StringDoublePair("x2", 9.1),
                    new StringDoublePair("x3", 1.9),
                    new StringDoublePair("x4", 8.2),
                    new StringDoublePair("x5", 3.6),
                }
            });

            lineChart.OtherInfo.Add(new StringDoublePair("Hours", 234));
            lineChart.OtherInfo.Add(new StringDoublePair("Other", 5.4));

            excelManager.Writer.AddLineOrColumnChart(lineChart, 1, 100, 100);

            Assert.AreEqual(chartName, excelManager.Reader.GetCellValueAsString(1, 1));
            Assert.AreEqual("x1", excelManager.Reader.GetCellValueAsString(2, 2));
            Assert.AreEqual("x2", excelManager.Reader.GetCellValueAsString(2, 3));
            Assert.AreEqual("x3", excelManager.Reader.GetCellValueAsString(2, 4));
            Assert.AreEqual("x4", excelManager.Reader.GetCellValueAsString(2, 5));
            Assert.AreEqual("x5", excelManager.Reader.GetCellValueAsString(2, 6));

            Assert.AreEqual("Delevopment", excelManager.Reader.GetCellValueAsString(3, 1));
            Assert.AreEqual("Maintenance", excelManager.Reader.GetCellValueAsString(4, 1));

            Assert.AreEqual(3.2, excelManager.Reader.GetCellValueAsDouble(3, 2));
            Assert.AreEqual(1.1, excelManager.Reader.GetCellValueAsDouble(3, 3));
            Assert.AreEqual(3.9, excelManager.Reader.GetCellValueAsDouble(3, 4));
            Assert.AreEqual(8.2, excelManager.Reader.GetCellValueAsDouble(3, 5));
            Assert.AreEqual(4.6, excelManager.Reader.GetCellValueAsDouble(3, 6));

            Assert.AreEqual(12.2, excelManager.Reader.GetCellValueAsDouble(4, 2));
            Assert.AreEqual(9.1, excelManager.Reader.GetCellValueAsDouble(4, 3));
            Assert.AreEqual(1.9, excelManager.Reader.GetCellValueAsDouble(4, 4));
            Assert.AreEqual(8.2, excelManager.Reader.GetCellValueAsDouble(4, 5));
            Assert.AreEqual(3.6, excelManager.Reader.GetCellValueAsDouble(4, 6));

            Assert.AreEqual("Hours", excelManager.Reader.GetCellValueAsString(5, 1));
            Assert.AreEqual(234, excelManager.Reader.GetCellValueAsDouble(5, 2));

            Assert.AreEqual("Other", excelManager.Reader.GetCellValueAsString(6, 1));
            Assert.AreEqual(5.4, excelManager.Reader.GetCellValueAsDouble(6, 2));

        }

        [TestMethod]
        public void AddPieChart()
        {
            var excelManager = new JlgCommon.ExcelManager.ExcelManager();

            var chartName = "Costs share per department";
            var pieChart = new PieChartForExcel();
            pieChart.ChartName = chartName;
            pieChart.ColumnName = "Department";
            pieChart.ColumnValue = "Cost";
            pieChart.Values.Add(new StringDoublePair("Research", 3500));
            pieChart.Values.Add(new StringDoublePair("Development", 15000.37));
            pieChart.Values.Add(new StringDoublePair("Marketing", 8000));

            excelManager.Writer.AddPieChart(pieChart, 1, 100, 100);

            Assert.AreEqual(chartName, excelManager.Reader.GetCellValueAsString(1, 1));
            Assert.AreEqual("Department", excelManager.Reader.GetCellValueAsString(2, 1));
            Assert.AreEqual("Cost", excelManager.Reader.GetCellValueAsString(2, 2));

            //This is the order because the pie chart sorts the shares descending
            Assert.AreEqual("Development", excelManager.Reader.GetCellValueAsString(3, 1));
            Assert.AreEqual(15000.37, excelManager.Reader.GetCellValueAsDouble(3, 2));
            Assert.AreEqual("Marketing", excelManager.Reader.GetCellValueAsString(4, 1));
            Assert.AreEqual(8000, excelManager.Reader.GetCellValueAsDouble(4, 2));
            Assert.AreEqual("Research", excelManager.Reader.GetCellValueAsString(5, 1));
            Assert.AreEqual(3500, excelManager.Reader.GetCellValueAsDouble(5, 2));

        }

        [TestMethod]
        public void AddTree()
        {
            var excelManager = new JlgCommon.ExcelManager.ExcelManager();

            var rootNode = new TreeNodeForExcel()
            {
                Name = "Root"
            };

            rootNode.Children = new List<TreeNodeForExcel>() { 
                new TreeNodeForExcel
                {
                    Name = "Level 1 - Child 1",
                    Children = new List<TreeNodeForExcel>()
                    {
                        new TreeNodeForExcel()
                        {
                            Name = "Level 2 - Child 1"
                        },
                        new TreeNodeForExcel()
                        {
                            Name = "Level 2 - Child 2"
                        }
                    }
                }, 
                new TreeNodeForExcel
                {
                    Name = "Level 1 - Child 2",
                },
                new TreeNodeForExcel
                {
                    Name = "Level 1 - Child 3",
                }};

            excelManager.Writer.AddTree(rootNode, 1);

            Assert.AreEqual("Root", excelManager.Reader.GetCellValueAsString(1, 1));
            Assert.AreEqual("Level 1 - Child 1", excelManager.Reader.GetCellValueAsString(2, 2));
                Assert.AreEqual("Level 2 - Child 1", excelManager.Reader.GetCellValueAsString(3, 3));
                Assert.AreEqual("Level 2 - Child 2", excelManager.Reader.GetCellValueAsString(4, 3));
            Assert.AreEqual("Level 1 - Child 2", excelManager.Reader.GetCellValueAsString(5, 2));
            Assert.AreEqual("Level 1 - Child 3", excelManager.Reader.GetCellValueAsString(6, 2));
        }

        [TestMethod]
        public void SaveToDisk()
        {
            var excelManager1 = new JlgCommon.ExcelManager.ExcelManager();
            excelManager1.ExcelFilePath = ExcelManagerTests.ExcelManagerTestsFolder + Guid.NewGuid().ToString("N") + ".xlsx";
            excelManager1.Writer.SaveToDisk();
            var byteArray1 = excelManager1.Reader.ReadExcelFileAsByteArray(true);

            var excelManager2 = new JlgCommon.ExcelManager.ExcelManager();
            excelManager2.ExcelFilePath = ExcelManagerTests.ExcelManagerTestsFolder + Guid.NewGuid().ToString("N") + ".xlsx";
            excelManager2.Writer.SetCellValue(1, 1, _author);
            excelManager2.Writer.SaveToDisk();
            var byteArray2 = excelManager2.Reader.ReadExcelFileAsByteArray(true);

            Assert.IsTrue(byteArray1.Length < byteArray2.Length);
        }
        

        private string _newWorksheetName1 = "New Worksheet 1";
        [TestMethod]
        public void RenameWorksheet()
        {
            var excelManager = new JlgCommon.ExcelManager.ExcelManager();
            Assert.AreEqual(1, excelManager.Reader.GetWorksheetNames().Count);
            var oldWorksheetName = excelManager.Reader.GetWorksheetNames()[0];
            excelManager.Writer.RenameWorksheet(oldWorksheetName, _newWorksheetName1);

            Assert.AreEqual(_newWorksheetName1, excelManager.Reader.GetWorksheetNames()[0]);
            Assert.AreEqual(1, excelManager.Reader.GetWorksheetNames().Count);
        }

        [TestMethod]
        public void AddWorksheet()
        {
            var excelManager = new JlgCommon.ExcelManager.ExcelManager();

            Assert.AreEqual(1, excelManager.Reader.GetWorksheetNames().Count);

            excelManager.Writer.AddWorksheet(_newWorksheetName1);

            Assert.AreEqual(_newWorksheetName1, excelManager.Reader.GetWorksheetNames()[1]);
            Assert.AreEqual(2, excelManager.Reader.GetWorksheetNames().Count);
        }

        [TestMethod]
        public void DeleteWorksheet()
        {
            var excelManager = new JlgCommon.ExcelManager.ExcelManager();

            Assert.AreEqual(1, excelManager.Reader.GetWorksheetNames().Count);

            excelManager.Writer.AddWorksheet(_newWorksheetName1);

            Assert.AreEqual(_newWorksheetName1, excelManager.Reader.GetWorksheetNames()[1]);
            Assert.AreEqual(2, excelManager.Reader.GetWorksheetNames().Count);


            excelManager.Reader.SelectWorksheet(excelManager.Reader.GetWorksheetNames()[0]);
            excelManager.Writer.DeleteWorksheet(_newWorksheetName1);
            Assert.AreEqual(1, excelManager.Reader.GetWorksheetNames().Count);
        }

        [TestMethod]
        public void SetCellStyle()
        {
            var excelManager = new JlgCommon.ExcelManager.ExcelManager();
            var style = new SLStyle();
            style.Alignment.Horizontal = HorizontalAlignmentValues.Center;
            style.Alignment.Vertical = VerticalAlignmentValues.Bottom;
            style.Font.FontName = "Arial";
            style.Font.FontSize = 10;
            excelManager.Writer.SetCellStyle(1, 1,
                style);
            var newStyle = excelManager.Reader.GetCellStyle(1, 1);
            Assert.AreEqual(style.Alignment.Horizontal, newStyle.Alignment.Horizontal);
            Assert.AreEqual(style.Alignment.Vertical, newStyle.Alignment.Vertical);
            Assert.AreEqual(style.Font.FontName, newStyle.Font.FontName);
            Assert.AreEqual(style.Font.FontSize, newStyle.Font.FontSize);
        }

        [TestMethod]
        public void SetCellWidth()
        {
            var excelManager = new JlgCommon.ExcelManager.ExcelManager();
            excelManager.Writer.SetCellWidth(1,10.0);
            Assert.AreEqual(excelManager.Reader.GetCellWidth(1), 10.0);
        }

        [TestMethod]
        public void AutoFitColumn()
        {
            var excelManager = new JlgCommon.ExcelManager.ExcelManager();
            const string text = "test for autofit method";
            excelManager.Writer.SetCellValue(1,1, text);
            excelManager.Writer.AutoFitColumn(1);
            var width = excelManager.Reader.GetCellWidth(1);
            
            //8.43 chars equals to a width of 9.14
            var conversionFactor = 8.43/9.14;
            
            Assert.IsTrue(width - text.Length * conversionFactor < 0.1);
        }

        [TestMethod]
        public void MergeCells()
        {
            var excelManager = new JlgCommon.ExcelManager.ExcelManager();

            excelManager.Writer.MergeCells(2, 2, 3, 5);
            var mergedCellsList = excelManager.Reader.GetMergedCells();

            bool assertion = false;

            foreach (SLMergeCell mergedCell in mergedCellsList)
            {
                if ((mergedCell.StartRowIndex == 2) && (mergedCell.StartColumnIndex == 2) && (mergedCell.EndRowIndex == 3) && (mergedCell.EndColumnIndex == 5))
                {
                    assertion = true;
                    break;
                }
            }

            Assert.IsTrue(assertion);
        }

        [TestMethod]
        public void SurroundRowsWithBorder()
        {
            var excelManager = new JlgCommon.ExcelManager.ExcelManager();
            excelManager.Writer.SelectWorksheet("Page2");
            
            excelManager.Writer.AddUpperBorder(2, 1);
            excelManager.Writer.AddLowerBorder(7, 1);

            SLStyle upperStyle = excelManager.Reader.GetCellStyle(2, 1);
            Assert.AreEqual(upperStyle.Border.TopBorder.BorderStyle, BorderStyleValues.Thin);
            Assert.AreEqual(upperStyle.Border.TopBorder.Color.ToArgb(), Color.Black.ToArgb());

            SLStyle lowerStyle = excelManager.Reader.GetCellStyle(7, 1);
            Assert.AreEqual(lowerStyle.Border.BottomBorder.BorderStyle, BorderStyleValues.Thin);
            Assert.AreEqual(lowerStyle.Border.BottomBorder.Color.ToArgb(), Color.Black.ToArgb());
        }
    }
}
