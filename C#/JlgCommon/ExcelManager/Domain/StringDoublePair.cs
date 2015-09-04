namespace JlgCommon.ExcelManager.Domain
{
    public class StringDoublePair
    {
        public virtual string Name { get; set; }
        public virtual double Value { get; set; }
        public virtual bool IsForcedLastElementForPieCharts { get; set; }

        public StringDoublePair(string name, double value)
        {
            Name = name;
            Value = value;
        }

        public StringDoublePair(string name, double value, bool isForcedLastElement)
        {
            Name = name;
            Value = value;
            IsForcedLastElementForPieCharts = isForcedLastElement; 
        }
    }
}
