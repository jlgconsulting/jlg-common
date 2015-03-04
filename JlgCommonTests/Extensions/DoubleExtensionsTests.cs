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
        
        [TestMethod]
        public void IncreaseByPercent()
        {
            double nr1 = 10;
            Assert.AreEqual(15, nr1.IncreaseByPercent(50));
            double nr3 = 50;
            Assert.AreEqual(55, nr3.IncreaseByPercent(10));
            double nr4 = 70;
            Assert.AreEqual(73.64, nr4.IncreaseByPercent(5.2));
            double nr5 = 100;
            Assert.AreEqual(80, nr5.IncreaseByPercent(-20));
            double nr6 = 30;
            Assert.AreEqual(29.4, nr6.IncreaseByPercent(-2));
            double nr7 = 25.3;
            Assert.AreEqual(23.4278, nr7.IncreaseByPercent(-7.4));
        }    
    
    }
}
