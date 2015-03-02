using SpreadsheetLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JlgCommon.ExcelManager
{
    //http://spreadsheetlight.com/sample-code/
    //https://erictummers.wordpress.com/2014/08/28/get-spreadsheetlight-working/
    public class ExcelManager
    {
        public ExcelReader Reader { get; set; }
        public ExcelWriter Writer { get; set; }
        public string ExcelFilePath { get; set; }
        private SLDocument _excelDocument;


        public ExcelManager()
        {
            _excelDocument = new SLDocument();
            Reader = new ExcelReader(_excelDocument);
            Writer = new ExcelWriter(_excelDocument);
        }

        public ExcelManager(string excelFilePath)
        {
            _excelDocument = new SLDocument(excelFilePath);
            ExcelFilePath = excelFilePath;
            Reader = new ExcelReader(_excelDocument);
            Writer = new ExcelWriter(_excelDocument);
        }

    }
}
