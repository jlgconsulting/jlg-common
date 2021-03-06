﻿using System.IO;
using SpreadsheetLight;
using OfficeOpenXml;

namespace JlgCommon.ExcelManager
{
    //http://spreadsheetlight.com/sample-code/
    //https://erictummers.wordpress.com/2014/08/28/get-spreadsheetlight-working/
        //Install-Package DocumentFormat.OpenXml -Version 1.0.0
        //Remove the assemblyBinding from the app.config
    public class ExcelManager
    {
        public const string FormatCodePercent = "General\"%\"";
        public const string FormatCodeGeneral = "General";

        public ExcelReader Reader { get; set; }
        public ExcelWriter Writer { get; set; }

        public string _excelFilePath;
        public string ExcelFilePath
        {
            get
            {
                return _excelFilePath;
            }
            set
            {
                _excelFilePath = value;
                Reader.ExcelFilePath = _excelFilePath;
                Writer.ExcelFilePath = _excelFilePath;
            }
        }
        private SLDocument _excelDocument;
        private ExcelPackage _excelPackage;

        public ExcelManager()
        {
            _excelDocument = new SLDocument();
            Reader = new ExcelReader(_excelDocument, null);
            Writer = new ExcelWriter(_excelDocument);
        }

        public ExcelManager(string excelFilePath)
        {          

            if (File.Exists(excelFilePath))
            {
                _excelDocument = new SLDocument(excelFilePath);
                FileInfo newFile = new FileInfo(excelFilePath);
                _excelPackage = new ExcelPackage(newFile);
                Reader = new ExcelReader(_excelDocument, _excelPackage);
                Writer = new ExcelWriter(_excelDocument);
            }
            else
            {
                _excelDocument = new SLDocument();
                Reader = new ExcelReader(_excelDocument, null);
                Writer = new ExcelWriter(_excelDocument);
            }

            ExcelFilePath = excelFilePath;
        }

    }
}
