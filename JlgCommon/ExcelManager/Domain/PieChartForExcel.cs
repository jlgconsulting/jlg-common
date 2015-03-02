using System.Collections.Generic;

namespace ExcelManager.Domain
{
    public class PieChartForExcel
    {
        public string ColumnName { get; set; }
        public string ColumnValue { get; set; }
        public List<StringDoublePair> Values { get; set; }

        public PieChartForExcel()
        {
            Values = new List<StringDoublePair>();
        }
    }
}
