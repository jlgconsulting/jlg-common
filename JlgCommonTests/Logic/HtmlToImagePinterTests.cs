using JlgCommon.Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JlgCommonTests.Logic
{
    [TestClass]
    public class HtmlToImagePinterTests
    {
        [TestMethod]
        public void GetJpegImage()
        {
            var htmlToImagePinter = new HtmlToImagePinter();
            var htmlText = "<div><b>Dan Misailescu</b></div>";

            var byteArray = htmlToImagePinter.GetJpegImage(htmlText, 800, 600);

            Assert.IsTrue(byteArray.Length > 0);
        }
    }
}
