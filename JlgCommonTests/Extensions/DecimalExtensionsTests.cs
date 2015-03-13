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
    public class DecimalExtensionsTests
    {
        [TestMethod]
        public void DifferenceInPercents()
        {
            decimal nr1 = 10;
            decimal nr2 = 15;
            Assert.AreEqual(50, nr2.DifferenceInPercents(nr1));
            decimal nr3 = 20;
            Assert.AreEqual(100, nr3.DifferenceInPercents(nr1));
            decimal nr4 = 5;
            Assert.AreEqual(-50, nr4.DifferenceInPercents(nr1));
            decimal nr5 = -5;
            Assert.AreEqual(-150, nr5.DifferenceInPercents(nr1));
        }

        [TestMethod]
        public void DifferenceInPercents_Nullable()
        {
            decimal? nr1 = 10;
            decimal? nr2 = 15;
            Assert.AreEqual(50, nr2.DifferenceInPercents(nr1));
            decimal? nr3 = 20;
            Assert.AreEqual(100, nr3.DifferenceInPercents(nr1));
            decimal? nr4 = 5;
            Assert.AreEqual(-50, nr4.DifferenceInPercents(nr1));
            decimal? nr5 = -5;
            Assert.AreEqual(-150, nr5.DifferenceInPercents(nr1));
            decimal? nr6 = null;
            Assert.AreEqual(null, nr6.DifferenceInPercents(nr1));
            Assert.AreEqual(null, nr4.DifferenceInPercents(nr6));
        }
        
        [TestMethod]
        public void PercentValue()
        {
            decimal nr1 = 10;
            Assert.AreEqual(5, nr1.PercentValue(50));
            decimal nr3 = 50;
            Assert.AreEqual(5, nr3.PercentValue(10));
            decimal nr4 = 70;
            Assert.AreEqual(3.64m, nr4.PercentValue(5.2m));
            decimal nr5 = 100;
            Assert.AreEqual(-20, nr5.PercentValue(-20));
            decimal nr6 = 30;
            Assert.AreEqual(-0.6m, nr6.PercentValue(-2));
            decimal nr7 = 25.3m;
            Assert.AreEqual(-1.8722m, nr7.PercentValue(-7.4m));
        }    

        [TestMethod]
        public void IncreaseByPercent()
        {
            decimal nr1 = 10;
            Assert.AreEqual(15, nr1.IncreaseByPercent(50));
            decimal nr3 = 50;
            Assert.AreEqual(55, nr3.IncreaseByPercent(10));
            decimal nr4 = 70;
            Assert.AreEqual(73.64m, nr4.IncreaseByPercent(5.2m));
            decimal nr5 = 100;
            Assert.AreEqual(80, nr5.IncreaseByPercent(-20));
            decimal nr6 = 30;
            Assert.AreEqual(29.4m, nr6.IncreaseByPercent(-2));
            decimal nr7 = 25.3m;
            Assert.AreEqual(23.4278m, nr7.IncreaseByPercent(-7.4m));
        }    
    
    }
}
