using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Spreadsheet;
using JlgCommon.ExcelManager.Domain;
using SpreadsheetLight;
using SpreadsheetLight.Charts;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Color = System.Drawing.Color;

namespace JlgCommon.ExcelManager
{
    public static class SLDocumentExtensions
    {
        public static void SetCellBold(this SLDocument excelDocument, int rowIndex, int columnIndex)
        {
            var cellStyle = excelDocument.GetCellStyle(rowIndex, columnIndex);
            cellStyle.SetFontBold(true);

            excelDocument.SetCellStyle(rowIndex, columnIndex, cellStyle);
        }

        public static void SetCellItalic(this SLDocument excelDocument, int rowIndex, int columnIndex)
        {
            var cellStyle = excelDocument.GetCellStyle(rowIndex, columnIndex);
            cellStyle.SetFontItalic(true);

            excelDocument.SetCellStyle(rowIndex, columnIndex, cellStyle);
        }

        public static void SetCellFormatCode(this SLDocument excelDocument, int rowIndex, int columnIndex, string formatCode)
        {
            var cellStyle = excelDocument.GetCellStyle(rowIndex, columnIndex);
            cellStyle.FormatCode = formatCode;

            excelDocument.SetCellStyle(rowIndex, columnIndex, cellStyle);
        }

        public static void SetCellFontColor(this SLDocument excelDocument, int rowIndex, int columnIndex, Color color)
        {
            var cellStyle = excelDocument.GetCellStyle(rowIndex, columnIndex);
            cellStyle.SetFontColor(color);

            excelDocument.SetCellStyle(rowIndex, columnIndex, cellStyle);
        }

        public static void SetCellBackgroundColor(this SLDocument excelDocument, int rowIndex, int columnIndex, Color color)
        {
            var cellStyle = excelDocument.GetCellStyle(rowIndex, columnIndex);
            cellStyle.SetGradientFill(SLGradientShadingStyleValues.Horizontal1, color, color);

            excelDocument.SetCellStyle(rowIndex, columnIndex, cellStyle);
        }

        public static void SetCellHorizoltalAllign(this SLDocument excelDocument, int rowIndex, int columnIndex, HorizontalAlignmentType type) 
        {
            var cellStyle = excelDocument.GetCellStyle(rowIndex, columnIndex);
            switch (type)
	        {
                case HorizontalAlignmentType.Left:
                    cellStyle.SetHorizontalAlignment(HorizontalAlignmentValues.Left);
                    break;
                case HorizontalAlignmentType.Right:
                    cellStyle.SetHorizontalAlignment(HorizontalAlignmentValues.Right);
                    break;
                case HorizontalAlignmentType.Center:
                    cellStyle.SetHorizontalAlignment(HorizontalAlignmentValues.Center);
                    break;
                case HorizontalAlignmentType.Justify:
                    cellStyle.SetHorizontalAlignment(HorizontalAlignmentValues.Justify);
                    break;		    
	        }       
       
            excelDocument.SetCellStyle(rowIndex, columnIndex, cellStyle);
        }
       
        public static void HideRowButLetChartsSeeIt(this SLDocument excelDocument, int rowIndex)
        {
            excelDocument.SetRowHeight(rowIndex, 0.001);
        }
                
    }
}
