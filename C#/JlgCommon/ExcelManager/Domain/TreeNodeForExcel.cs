using System.Collections.Generic;
using System.Drawing;

namespace JlgCommon.ExcelManager.Domain
{
    public class TreeNodeForExcel
    {
        public string Name { get; set; }
        public string ExtraInfo { get; set; }        
        public Color? Color { get; set; }
        public List<TreeNodeForExcel> Children { get; set; }
        public TreeNodeForExcel()
        {
            Children = new List<TreeNodeForExcel>();
        }
    }
}
