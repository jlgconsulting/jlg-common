using System;
using System.Linq;
using DocumentFormat.OpenXml.Spreadsheet;
using SpreadsheetLight;
using SpreadsheetLight.Charts;
using DocumentFormat.OpenXml.Drawing.Charts;
using Color = System.Drawing.Color;
using JlgCommon.ExcelManager;
using JlgCommon.ExcelManager.Domain;
using System.Collections.Generic;

namespace JlgCommon.ExcelManager
{
    public class ExcelWriter
    {       
        public const string FormatColon = "{0}: {1}";
        public string ExcelFilePath { get; set; }
        private SLDocument _excelDocument;
        
        internal ExcelWriter()
        {
            _excelDocument = new SLDocument();
        }

        internal ExcelWriter(SLDocument excelDocument)
        {
            _excelDocument = excelDocument;
        }

        public int AddLineOrColumnChart(LineOrColumnChartForExcel lineChart, int startingRow, double width, double height, bool isMixt = false, int startingColumn = 1)
        {
            if (lineChart.Series == null
                || lineChart.Series.Count == 0)
            {
                return startingRow + 1;
            }

            if (!string.IsNullOrEmpty(lineChart.ChartName))
            {
                _excelDocument.SetCellValue(startingRow, startingColumn, lineChart.ChartName);
                _excelDocument.SetCellBold(startingRow, startingColumn);
                _excelDocument.SetCellItalic(startingRow, startingColumn);
                startingRow++;
            }

            var xAxis = lineChart.Series[0].Values;
            for (int i = 0; i < xAxis.Count; i++)
            {
                var column = i + startingColumn + 1;
                _excelDocument.SetCellValue(startingRow, column, xAxis[i].Name);
                _excelDocument.SetCellBold(startingRow, column);
            }

            for (int i = 0; i < lineChart.Series.Count; i++)
            {
                var row = startingRow + 1 + i;
                var serie = lineChart.Series[i];
                if (serie.HideFromTable)
                {
                    _excelDocument.HideRowButLetChartsSeeIt(row);
                }
                _excelDocument.SetCellValue(row, startingColumn, serie.Name);
                _excelDocument.SetCellBold(row, startingColumn);

                for (int j = 0; j < serie.Values.Count; j++)
                {
                    _excelDocument.SetCellValue(row, j + startingColumn + 1, serie.Values[j].Value);

                    if (!string.IsNullOrEmpty(serie.CellsFormatCode))
                    {
                        _excelDocument.SetCellFormatCode(row, j + startingColumn + 1, serie.CellsFormatCode);
                    }
                    else if (!string.IsNullOrEmpty(lineChart.CellsFormatCode))
                    {
                        _excelDocument.SetCellFormatCode(row, j + startingColumn + 1, lineChart.CellsFormatCode);
                    }
                    _excelDocument.SetCellHorizoltalAllign(row, j + startingColumn + 1, HorizontalAlignmentType.Left);
                }
            }

            for (int i = 0; i < lineChart.OtherInfo.Count; i++)
            {
                var othInfo = lineChart.OtherInfo[i];
                _excelDocument.SetCellValue(startingRow + lineChart.Series.Count + i + 1, startingColumn, othInfo.Name);
                _excelDocument.SetCellItalic(startingRow + lineChart.Series.Count + i + 1, startingColumn);
                _excelDocument.SetCellValue(startingRow + lineChart.Series.Count + i + 1, startingColumn + 1, othInfo.Value);
                _excelDocument.SetCellItalic(startingRow + lineChart.Series.Count + i + 1, startingColumn + 1);
            }

            var chartCreateOptions = new SLCreateChartOptions();
            chartCreateOptions.RowsAsDataSeries = true;

            var chart = _excelDocument.CreateChart(startingRow, startingColumn, startingRow + lineChart.Series.Count, xAxis.Count + startingColumn, chartCreateOptions);
            switch (lineChart.ChartType)
            {
                case ChartType.ClusteredColumn:
                    chart.SetChartType(SLColumnChartType.ClusteredColumn);
                    break;
                case ChartType.LineWithMarkers:
                    chart.SetChartType(SLLineChartType.LineWithMarkers);
                    break;
            }

            if (isMixt)
            {
                chart.SecondaryValueAxis.ShowMajorGridlines = false;
                chart.SecondaryValueAxis.TickLabelPosition = TickLabelPositionValues.None;
            }

            for (int i = 0; i < lineChart.Series.Count; i++)
            {
                var serie = lineChart.Series[i];

                if (serie.Color.HasValue)
                {
                    chart.SetSerieColor(i + 1, serie.Color.Value, serie.ColorTransparencyPercent);
                }

                if (serie.MarkerStyle.HasValue)
                {
                    chart.SetMarkerForChartSerie(i + 1, serie.MarkerStyle.Value);
                }

                if (isMixt)
                {
                    if (!serie.IsColumn)
                    {
                        chart.PlotDataSeriesAsSecondaryLineChart(i + 1, SLChartDataDisplayType.Normal, true);
                        chart.SetMarkerForChartSerie(i + 1, MarkerStyleType.Circle);
                    }
                }
            }

            chart.SetChartPosition(startingRow + 1 + lineChart.Series.Count + lineChart.OtherInfo.Count, startingColumn - 1, startingRow + 1 + lineChart.Series.Count + height, startingColumn + width - 1);

            foreach (var lineKvp in lineChart.LabelsForSeriesAndDataPoints)
            {
                foreach (var pointKvp in lineKvp.Value)
                {
                    var dloptions = chart.CreateDataLabelOptions();
                    dloptions.FormatCode = pointKvp.Value;
                    dloptions.ShowValue = true;
                    dloptions.SourceLinked = false;
                    dloptions.SetLabelPosition(DataLabelPositionValues.Top);
                    chart.SetDataLabelOptions(lineKvp.Key, pointKvp.Key, dloptions);
                }
            }

            if (!lineChart.ShowMajorGridlines)
            {
                chart.PrimaryValueAxis.ShowMajorGridlines = false;
                chart.PrimaryValueAxis.TickLabelPosition = TickLabelPositionValues.None;
            }
            else
            {
                chart.PrimaryValueAxis.Maximum = lineChart.MaxValueAxis;

                if (!string.IsNullOrEmpty(lineChart.PrimaryValueAxisFormatCode))
                {
                    chart.PrimaryValueAxis.FormatCode = lineChart.PrimaryValueAxisFormatCode;
                }
            }

            _excelDocument.InsertChart(chart);
                  
            return startingRow + 1 + lineChart.Series.Count + Convert.ToInt32(height);
        }

        public int AddPieChart(PieChartForExcel pieChart, int startingRow, double width, double height, bool? sortDescending = true, int startingColumn = 1)
        {
            if (sortDescending.HasValue)
            {
                if (sortDescending.Value)
                {
                    pieChart.Values = pieChart.Values.OrderByDescending(x => x.Value).ToList();
                }
                else
                {
                    pieChart.Values = pieChart.Values.OrderBy(x => x.Value).ToList();
                }
            }

            var forcedLastElement = pieChart.Values.FirstOrDefault(x => x.IsForcedLastElementForPieCharts);
            if (forcedLastElement != null)
            {
                pieChart.Values.Remove(forcedLastElement);
                pieChart.Values.Add(forcedLastElement);
            }

            if (!string.IsNullOrEmpty(pieChart.ChartName))
            {
                _excelDocument.SetCellValue(startingRow, startingColumn, pieChart.ChartName);
                _excelDocument.SetCellBold(startingRow, startingColumn);
                _excelDocument.SetCellItalic(startingRow, startingColumn);
                startingRow++;
            }

            _excelDocument.SetCellValue(startingRow, startingColumn, pieChart.ColumnName);
            _excelDocument.SetCellBold(startingRow, startingColumn);
            _excelDocument.SetCellValue(startingRow, startingColumn + 1, pieChart.ColumnValue);
            _excelDocument.SetCellBold(startingRow, startingColumn + 1);
            for (int i = 0; i < pieChart.Values.Count; i++)
            {
                var row = startingRow + i + 1;
                _excelDocument.SetCellValue(row, startingColumn, pieChart.Values[i].Name);
                _excelDocument.SetCellValue(row, startingColumn + 1, pieChart.Values[i].Value);
                _excelDocument.SetCellHorizoltalAllign(row, startingColumn + 1, HorizontalAlignmentType.Left);
            }

            var chart = _excelDocument.CreateChart(startingRow, startingColumn, startingRow + pieChart.Values.Count, startingColumn + 1);
            chart.SetChartType(SLPieChartType.Pie);
            chart.SetChartPosition(startingRow - 1, startingColumn + 1, startingRow + height, startingColumn + width);
            _excelDocument.InsertChart(chart);

            int max = pieChart.Values.Count;
            int heightInt = Convert.ToInt32(height);
            if (max < heightInt)
            {
                max = heightInt;
            }

            return startingRow + max + 1;
        }

        public int AddTree(TreeNodeForExcel rootTreeNode, int startingRow, int startingColumn = 1)
        {            
            if (rootTreeNode.Color.HasValue)
            {
                _excelDocument.SetCellBackgroundColor(startingRow, startingColumn, rootTreeNode.Color.Value);
                _excelDocument.SetColumnWidth(startingColumn, 3);
                startingColumn++;
                _excelDocument.SetCellValue(startingRow, startingColumn, rootTreeNode.Name);
            }
            else
            {
                _excelDocument.SetCellValue(startingRow, startingColumn, rootTreeNode.Name);
            }    
        
            if (!string.IsNullOrEmpty(rootTreeNode.ExtraInfo))
            {
                startingRow++;
                _excelDocument.SetCellValue(startingRow, startingColumn, rootTreeNode.ExtraInfo);
                _excelDocument.SetCellItalic(startingRow, startingColumn);
            }
            startingRow++;
            startingColumn++;

            foreach (var childNode in rootTreeNode.Children)
            {
                startingRow = AddTree(childNode, startingRow, startingColumn);
            }

            return startingRow;
        }
        
        public void SetCellValue(int rowIndex, int columnIndex, dynamic value)
        {
            try
            {
                _excelDocument.SetCellValue(rowIndex, columnIndex, value);
            }
            catch
            {
                _excelDocument.SetCellValue(rowIndex, columnIndex, (string)value);
            }            
            
        }

        public void MergeCells(int startingRow, int startingColumn, int endingRow, int endingColumn)
        {
            _excelDocument.MergeWorksheetCells(startingRow, startingColumn, endingRow, endingColumn);
        }

        public void SetCellBold(int rowIndex, int columnIndex)
        {
            _excelDocument.SetCellBold(rowIndex, columnIndex);
        }

        public void SetCellItalic(int rowIndex, int columnIndex)
        {
            _excelDocument.SetCellItalic(rowIndex, columnIndex);
        }
          
        public void SetCellHorizoltalAllign(int rowIndex, int columnIndex, HorizontalAlignmentType type)
        {
            _excelDocument.SetCellHorizoltalAllign(rowIndex, columnIndex, type);
        }        

        public void SetCellFontColor(int rowIndex, int columnIndex, Color color)
        {
            _excelDocument.SetCellFontColor(rowIndex, columnIndex, color);
        }       

        public void AddWorksheet(string worksheetName)
        {
            _excelDocument.AddWorksheet(worksheetName);            
        }

        public void RenameWorksheet(string oldWorksheetName, string newWorksheetName)
        {
            _excelDocument.RenameWorksheet(oldWorksheetName, newWorksheetName);  
        }       

        public void DeleteWorksheet(string worksheetName)
        {
            _excelDocument.DeleteWorksheet(worksheetName);
        }

        public bool SelectWorksheet(string worksheetName)
        {
            return _excelDocument.SelectWorksheet(worksheetName);
        }

        public void SaveToDisk()
        {
            _excelDocument.SaveAs(ExcelFilePath);
        }

        public void SetCellStyle(int rowIndex, int columnIndex, SLStyle style)
        {
            _excelDocument.SetCellStyle(rowIndex, columnIndex, style);
        }

        public void SetCellBackGroundColor(int rowIndex, int columnIndex, Color color)
        {
            _excelDocument.SetCellBackgroundColor(rowIndex, columnIndex, color);
        }

        public void SetCellWidth(int columnIndex, double width)
        {
            _excelDocument.SetColumnWidth(columnIndex, width);
        }

        public void SetRowHeight(int rowIndex, double width)
        {
            _excelDocument.SetRowHeight(rowIndex, width);
        }

        public void AutoFitColumn(int columnIndex)
        {
            _excelDocument.AutoFitColumn(columnIndex);
        }

        public void AutoFitRow(int rowIndex)
        {
            _excelDocument.AutoFitRow(rowIndex);
        }

        public void AddUpperBorder(int rowIndex, int columnIndex)
        {
            SLStyle upperStyle = new SLStyle();
            upperStyle.Border.TopBorder.BorderStyle = BorderStyleValues.Thin;
            upperStyle.Border.TopBorder.Color = Color.Black;
            
            _excelDocument.SetCellStyle(rowIndex, columnIndex, upperStyle);
        }

        public void AddLowerBorder(int rowIndex, int columnIndex)
        {
            SLStyle lowerStyle = new SLStyle();
            lowerStyle.Border.BottomBorder.BorderStyle = BorderStyleValues.Thin;
            lowerStyle.Border.BottomBorder.Color = Color.Black;
            
            _excelDocument.SetCellStyle(rowIndex, columnIndex, lowerStyle);
        }

        public void AddLeftBorder(int rowIndex, int columnIndex)
        {
            SLStyle leftStyle = new SLStyle();
            leftStyle.Border.LeftBorder.BorderStyle = BorderStyleValues.Thin;
            leftStyle.Border.LeftBorder.Color = Color.Black;

            _excelDocument.SetCellStyle(rowIndex, columnIndex, leftStyle);
        }

        public void AddRightBorder(int rowIndex, int columnIndex)
        {
            SLStyle leftStyle = new SLStyle();
            leftStyle.Border.RightBorder.BorderStyle = BorderStyleValues.Thin;
            leftStyle.Border.RightBorder.Color = Color.Black;

            _excelDocument.SetCellStyle(rowIndex, columnIndex, leftStyle);
        }
        
        public void SurroundRowsWithBorderUpperLower(int firstRowIndex, int firstColumnIndex, int lastRowIndex, int lastColumnIndex)
        {
            for (int columnIndex = firstColumnIndex; columnIndex <= lastColumnIndex; columnIndex++)
            {
                AddUpperBorder(firstRowIndex, columnIndex);
                AddLowerBorder(lastRowIndex, columnIndex); 
            }
            
        }
        
    }
}
