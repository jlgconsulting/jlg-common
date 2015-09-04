using JlgCommon.Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JlgCommonTests.Logic
{
    [TestClass]
    public class HtmlToImagePrinterTests
    {
        [TestMethod]
        public void GetJpegImage()
        {
            var htmlToImagePrinter = new HtmlToImagePrinter();
            var htmlText = "<div><b>Dan Misailescu</b></div>";

            var byteArray = htmlToImagePrinter.GetJpegImage(htmlText, 800, 600);

            Assert.IsTrue(byteArray.Length > 0);
        }
    }
}
