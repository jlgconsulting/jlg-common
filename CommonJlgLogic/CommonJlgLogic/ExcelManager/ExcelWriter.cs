using System;
using System.Linq;
using DocumentFormat.OpenXml.Spreadsheet;
using SpreadsheetLight;
using SpreadsheetLight.Charts;
using DocumentFormat.OpenXml.Drawing.Charts;
using Color = System.Drawing.Color;
using ExcelManager.Domain;

namespace Domain
{
    public class ExcelWriter
    {
        //http://spreadsheetlight.com/sample-code/
        //https://erictummers.wordpress.com/2014/08/28/get-spreadsheetlight-working/
        public const string FormatCodePercent = "General\"%\"";
        public const string FormatColon = "{0}: {1}";

        public ExcelWriter()
        {
            
        }

        public int InsertPieChart(SLDocument excelDocument, PieChartForExcel pieChart, int startingRow, double width, double height, bool? sortDescending = true, int startingColumn = 1)
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

            excelDocument.SetCellValue(startingRow, startingColumn, pieChart.ColumnName);
            SetCellBold(excelDocument, startingRow, startingColumn);
            excelDocument.SetCellValue(startingRow, startingColumn + 1, pieChart.ColumnValue);
            SetCellBold(excelDocument, startingRow, startingColumn + 1);
            for (int i = 0; i < pieChart.Values.Count; i++)
            {
                var row = startingRow + i + 1;
                excelDocument.SetCellValue(row, startingColumn, pieChart.Values[i].Name);
                excelDocument.SetCellValue(row, startingColumn + 1, pieChart.Values[i].Value);
                SetCellHorizoltalAllign(excelDocument, row, startingColumn + 1, HorizontalAlignmentValues.Left);
            }

            var chart = excelDocument.CreateChart(startingRow, startingColumn, startingRow + pieChart.Values.Count, startingColumn + 1);
            chart.SetChartType(SLPieChartType.Pie);
            chart.SetChartPosition(startingRow - 1, startingColumn + 1, startingRow + height, startingColumn + width);
            excelDocument.InsertChart(chart);

            int max = pieChart.Values.Count;
            int heightInt = Convert.ToInt32(height);
            if (max < heightInt)
            {
                max = heightInt;
            }

            return startingRow + max + 1;
        }

        public int InsertLineOrColumnChart(SLDocument excelDocument, LineOrColumnChartForExcel lineChart, int startingRow, double width, double height, bool isMixt = false, int startingColumn = 1)
        {
            if (lineChart.Series == null
                || lineChart.Series.Count == 0)
            {
                return startingRow + 1;
            }           

            var xAxis = lineChart.Series[0].Values;
            for (int i = 0; i < xAxis.Count; i++)
            {
                var column = i + startingColumn + 1;
                excelDocument.SetCellValue(startingRow, column, xAxis[i].Name);
                SetCellBold(excelDocument, startingRow, column);
            }          

            for (int i = 0; i < lineChart.Series.Count; i++)
            {
                var row = startingRow + 1 + i;
                var serie = lineChart.Series[i];
                if (serie.HideFromTable)
                {
                    HideRow(excelDocument, row);
                }
                excelDocument.SetCellValue(row, startingColumn, serie.Name);
                SetCellBold(excelDocument, row, startingColumn);

                for (int j = 0; j < serie.Values.Count; j++)
                {
                    excelDocument.SetCellValue(row, j + startingColumn + 1, serie.Values[j].Value);
                    if (!string.IsNullOrEmpty(lineChart.CellsFormatCode))
                    {
                        SetCellFormatCode(excelDocument, row, j + startingColumn + 1, lineChart.CellsFormatCode);
                    }
                    SetCellHorizoltalAllign(excelDocument, row, j + startingColumn + 1, HorizontalAlignmentValues.Left);
                }
            }

            for(int i=0; i<lineChart.OtherInfo.Count; i++)
            {
                var othInfo = lineChart.OtherInfo[i];
                excelDocument.SetCellValue(startingRow + lineChart.Series.Count + i + 1, startingColumn, othInfo.Name);
                SetCellItalic(excelDocument, startingRow + lineChart.Series.Count + i + 1, startingColumn);
                excelDocument.SetCellValue(startingRow + lineChart.Series.Count + i + 1, startingColumn + 1, othInfo.Value);
                SetCellItalic(excelDocument, startingRow + lineChart.Series.Count + i + 1, startingColumn + 1);
            }

            var chartCreateOptions = new SLCreateChartOptions();
            chartCreateOptions.RowsAsDataSeries = true;

            var chart = excelDocument.CreateChart(startingRow, startingColumn, startingRow + lineChart.Series.Count, xAxis.Count + startingColumn, chartCreateOptions);
            chart.SetChartType(lineChart.ChartType);
            
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
                    SetSerieColor(chart, i+1, serie.Color.Value, serie.ColorTransparencyPercent);                    
                }

                if (serie.MarkerStyle.HasValue)
                {
                    SetMarkerForChartSerie(chart, i + 1, serie.MarkerStyle.Value);
                }

                if (isMixt)
                {
                    if (!serie.IsColumn)
                    {
                        chart.PlotDataSeriesAsSecondaryLineChart(i + 1, SLChartDataDisplayType.Normal, true);
                        SetMarkerForChartSerie(chart, i + 1, MarkerStyleValues.Circle);
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

            excelDocument.InsertChart(chart);

            SetSheetColumnsWidths(excelDocument, xAxis.Count + 1);

            return startingRow + 1 + lineChart.Series.Count + Convert.ToInt32(height);
        }

        public int InsertTree(SLDocument excelDocument, TreeNodeForExcel rootTreeNode, int startingRow, int startingColumn = 1)
        {
            
            if (rootTreeNode.Color.HasValue)
            {
                SetCellBackgroundColor(excelDocument, startingRow, startingColumn, rootTreeNode.Color.Value);
                excelDocument.SetColumnWidth(startingColumn, 3);
                startingColumn++;
                excelDocument.SetCellValue(startingRow, startingColumn, rootTreeNode.Name);
            }
            else
            {
                excelDocument.SetCellValue(startingRow, startingColumn, rootTreeNode.Name);
            }
            startingRow++;
            startingColumn++;          

            foreach (var childNode in rootTreeNode.Children)
            {
                startingRow = InsertTree(excelDocument, childNode, startingRow, startingColumn);
            }

            return startingRow;
        }        

        public void SetCellBold(SLDocument excelDocument, int rowIndex, int columnIndex)
        {
            var cellStyle = excelDocument.GetCellStyle(rowIndex, columnIndex);
            cellStyle.SetFontBold(true);

            excelDocument.SetCellStyle(rowIndex, columnIndex, cellStyle);
        }

        public void SetCellItalic(SLDocument excelDocument, int rowIndex, int columnIndex)
        {
            var cellStyle = excelDocument.GetCellStyle(rowIndex, columnIndex);
            cellStyle.SetFontItalic(true);

            excelDocument.SetCellStyle(rowIndex, columnIndex, cellStyle);
        }

        public void SetCellFormatCode(SLDocument excelDocument, int rowIndex, int columnIndex, string formatCode)
        {
            var cellStyle = excelDocument.GetCellStyle(rowIndex, columnIndex);
            cellStyle.FormatCode = formatCode;

            excelDocument.SetCellStyle(rowIndex, columnIndex, cellStyle);
        }

        public void SetCellFontColor(SLDocument excelDocument, int rowIndex, int columnIndex, Color color)
        {
            var cellStyle = excelDocument.GetCellStyle(rowIndex, columnIndex);
            cellStyle.SetFontColor(color);

            excelDocument.SetCellStyle(rowIndex, columnIndex, cellStyle);
        }

        public void SetCellBackgroundColor(SLDocument excelDocument, int rowIndex, int columnIndex, Color color)
        {
            var cellStyle = excelDocument.GetCellStyle(rowIndex, columnIndex);
            cellStyle.SetGradientFill(SLGradientShadingStyleValues.Horizontal1, color, color);

            excelDocument.SetCellStyle(rowIndex, columnIndex, cellStyle);
        }

        public void SetCellHorizoltalAllign(SLDocument excelDocument, int rowIndex, int columnIndex, HorizontalAlignmentValues type)
        {
            var cellStyle = excelDocument.GetCellStyle(rowIndex, columnIndex);
            cellStyle.SetHorizontalAlignment(type);
            excelDocument.SetCellStyle(rowIndex, columnIndex, cellStyle);
        }

        public void SetSheetColumnsWidths(SLDocument excelDocument, int nrColumns, double columnWidth = 17, bool firstColumnIsBigger = true, int startingColumn = 1)
        {
            if (nrColumns <= 0)
            {
                return;
            }

            if (firstColumnIsBigger)
            {
                excelDocument.SetColumnWidth(startingColumn, columnWidth + 10);
            }
            else
            {
                excelDocument.SetColumnWidth(startingColumn, columnWidth);
            }

            for (int i = startingColumn + 1; i <= nrColumns; i++)
            {
                excelDocument.SetColumnWidth(i, columnWidth);
            }
        }

        public void HideRow(SLDocument excelDocument, int rowIndex)
        {
            excelDocument.SetRowHeight(rowIndex, 0.001);
        }
        
        public void SetMarkerForChartSerie(SLChart chart, int serieIndex, MarkerStyleValues markerStyle)
        {
            var dso = chart.GetDataSeriesOptions(serieIndex);
            dso.Marker.Symbol = markerStyle;
            chart.SetDataSeriesOptions(serieIndex, dso);
        }
        
        public void SetSerieColor(SLChart chart, int serieIndex, Color color, int colorTransparencyPercent = 0)
        {
            var dso = chart.GetDataSeriesOptions(serieIndex);
            dso.Line.SetSolidLine(color, colorTransparencyPercent);
            chart.SetDataSeriesOptions(serieIndex, dso);
        }
    }
}
