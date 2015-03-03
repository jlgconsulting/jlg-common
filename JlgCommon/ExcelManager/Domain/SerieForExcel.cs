using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using DocumentFormat.OpenXml.Drawing.Charts;

namespace JlgCommon.ExcelManager.Domain
{
    public class SerieForExcel
    {
        public string Name { get; set; }
        public bool IsColumn { get; set; }
        public List<StringDoublePair> Values { get; set; }
        public Color? Color { get; set; }
        public int ColorTransparencyPercent { get; set; }
        public MarkerStyleType? MarkerStyle { get; set; }
        public bool HideFromTable { get; set; }
        public string CellsFormatCode { get; set; }

        public SerieForExcel()
        {
            Values = new List<StringDoublePair>();
        }
    }
}
