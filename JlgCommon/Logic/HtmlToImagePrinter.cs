using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JlgCommon.Logic
{
    public class HtmlToImagePrinter
    {
        public string CssFilePath { get; set; }
        public string ScriptFilePath { get; set; }

        private string _css;
        public string Css
        {
            get
            {
                if (!string.IsNullOrEmpty(CssFilePath))
                {
                    var cssNeededBuilder = new StringBuilder();
                    var css = ReadFromFile(CssFilePath);

                    cssNeededBuilder.AppendLine("<style type='text/css'>");
                    cssNeededBuilder.AppendLine(css);
                    cssNeededBuilder.AppendLine("</style>");                   

                    return cssNeededBuilder.ToString();

                }
                else
                {
                    return _css;
                }                
            }
            set
            {
                _css = value;
            }
        }

        private string _script;
        public string Script
        {
            get
            {
                if (!string.IsNullOrEmpty(ScriptFilePath))
                {
                    var scriptNeededBuilder = new StringBuilder();
                    var script = ReadFromFile(ScriptFilePath);

                    scriptNeededBuilder.AppendLine("<script type='text/javascript'>");
                    scriptNeededBuilder.AppendLine(script);
                    scriptNeededBuilder.AppendLine("</script>");

                    return scriptNeededBuilder.ToString();

                }
                else
                {
                    return _script;
                }                
            }
            set
            {
                _script = value;
            }
        }
        
        private static string ReadFromFile(string fileName)
        {
            using (var sr = new StreamReader(fileName))
            {
                return sr.ReadToEnd();
            }
        }

        private Thread _browserThread;

        private bool _getImageFromBrowserFinished;
        private bool GetImageFromBrowserFinished
        {
            get
            {
                return _getImageFromBrowserFinished;
            }
            set
            {
                _getImageFromBrowserFinished = value;
                if (_getImageFromBrowserFinished)
                {
                    _browserThread.Abort();
                }

            }
        }

        public byte[] GetJpegImage(string html, int width, int height)
        {
            GetImageFromBrowserFinished = false;
            var printedImageMemoryStream = new MemoryStream();
            var browserSize = new Size(width, height);
            var htmlToPrint = String.Format(@"
                <!DOCTYPE html>
                <html>
                    <head>
                        <meta http-equiv=""X-UA-Compatible"" content=""IE=9""/>
                         {0}                    
                    </head>

                    <body>
                        {1}                         
                    </body>
                        {2}                        
                </html>", Css, html, Script);


            //You have to create an STA thread that pumps a message loop. That's the only hospitable environment for an ActiveX component like WebBrowser. You won't get the DocumentCompleted event otherwise. 
            //http://stackoverflow.com/questions/4269800/webbrowser-control-in-a-new-thread/4271581#4271581

            _browserThread = new Thread(() =>
            {
                var webBrowserWinForms = new WebBrowser();
                webBrowserWinForms.Size = browserSize;

                webBrowserWinForms.DocumentCompleted +=
                    delegate(object sender, WebBrowserDocumentCompletedEventArgs e)
                    {
                        var webBrowser = (WebBrowser)sender;
                        using (var bitmap = new Bitmap(webBrowser.Width, webBrowser.Height))
                        {
                            webBrowser
                                .DrawToBitmap(
                                bitmap,
                                new System.Drawing
                                    .Rectangle(0, 0, bitmap.Width, bitmap.Height));

                            bitmap.Save(printedImageMemoryStream, ImageFormat.Jpeg);
                            GetImageFromBrowserFinished = true;
                        }
                    };

                webBrowserWinForms.DocumentText = htmlToPrint;
                Application.Run();
            });
            _browserThread.SetApartmentState(ApartmentState.STA);
            _browserThread.Start();


            var waitPrintTask = Task.Factory.StartNew(() =>
            {
                var timesWaited = 0;
                while (!GetImageFromBrowserFinished
                    && timesWaited < 50)
                {
                    timesWaited++;
                    Thread.Sleep(300);
                }

            });

            waitPrintTask.Wait();

            return printedImageMemoryStream.ToArray();
        }
    }
}
