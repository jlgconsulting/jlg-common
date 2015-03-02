using DocumentFormat.OpenXml.Drawing.Charts;
using System.Collections.Generic;
using System.Drawing;

namespace JlgCommon.ExcelManager.Domain
{
    public class LineOrColumnChartForExcel
    {
        public List<SerieForExcel> Series { get; set; }
        //Dictionary<seriesIndex, Dictionary<dataPointIndex, label>>
        public Dictionary<int, Dictionary<int, string>> LabelsForSeriesAndDataPoints { get; set; }
        public ChartType ChartType { get; set; }
        public bool ShowMajorGridlines { get; set; }
        public string PrimaryValueAxisFormatCode { get; set; }
        public double? MaxValueAxis { get; set; }
        public string CellsFormatCode { get; set; }
        public List<StringDoublePair> OtherInfo { get; set; }

        public LineOrColumnChartForExcel()
        {
            Series = new List<SerieForExcel>();
            LabelsForSeriesAndDataPoints = new Dictionary<int, Dictionary<int, string>>();
            OtherInfo = new List<StringDoublePair>();
            ChartType = ChartType.LineWithMarkers;
        }
    }    

}
