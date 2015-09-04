using System.Drawing;
using DocumentFormat.OpenXml.Drawing.Charts;
using JlgCommon.ExcelManager.Domain;
using SpreadsheetLight.Charts;

namespace JlgCommon.ExcelManager
{
    public static class SLChartExtensions
    {
        public static void SetMarkerForChartSerie(this SLChart chart, int serieIndex, MarkerStyleType markerStyle)
        {
            var dso = chart.GetDataSeriesOptions(serieIndex);
            switch (markerStyle)
            {
                case MarkerStyleType.Circle:
                    dso.Marker.Symbol = MarkerStyleValues.Circle;
                    break;
                case MarkerStyleType.None:
                    dso.Marker.Symbol = MarkerStyleValues.None;
                    break;
                default:
                    break;
            }
            
            chart.SetDataSeriesOptions(serieIndex, dso);
        }

        public static void SetSerieColor(this SLChart chart, int serieIndex, Color color, int colorTransparencyPercent = 0)
        {
            var dso = chart.GetDataSeriesOptions(serieIndex);
            dso.Line.SetSolidLine(color, colorTransparencyPercent);
            chart.SetDataSeriesOptions(serieIndex, dso);
        }
    }
}
