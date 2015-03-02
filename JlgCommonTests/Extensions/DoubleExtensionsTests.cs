using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JlgCommon.Extensions;

namespace JlgCommonTests.Extensions
{
    [TestClass]
    public class DoubleExtensionsTests
    {
        [TestMethod]
        public void DifferenceInPercents()
        {
            double nr1 = 10;
            double nr2 = 15;
            Assert.AreEqual(50, nr2.DifferenceInPercents(nr1));
            double nr3 = 20;
            Assert.AreEqual(100, nr3.DifferenceInPercents(nr1));
            double nr4 = 5;
            Assert.AreEqual(-50, nr4.DifferenceInPercents(nr1));
            double nr5 = -5;
            Assert.AreEqual(-150, nr5.DifferenceInPercents(nr1));
        }

        [TestMethod]
        public void DifferenceInPercents_Nullable()
        {
            double? nr1 = 10;
            double? nr2 = 15;
            Assert.AreEqual(50, nr2.DifferenceInPercents(nr1));
            double? nr3 = 20;
            Assert.AreEqual(100, nr3.DifferenceInPercents(nr1));
            double? nr4 = 5;
            Assert.AreEqual(-50, nr4.DifferenceInPercents(nr1));
            double? nr5 = -5;
            Assert.AreEqual(-150, nr5.DifferenceInPercents(nr1));
            double? nr6 = null;
            Assert.AreEqual(null, nr6.DifferenceInPercents(nr1));
            Assert.AreEqual(null, nr4.DifferenceInPercents(nr6));
        }
    }
}
