using JlgCommon.Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JlgCommonTests.Logic
{
    [TestClass]
    public class NormalizatorTests
    {
        private RandomGenerator _randomGenerator = new RandomGenerator();
        private Normalizator _normalizator = new Normalizator();
        
        [TestMethod]
        public void NormalizeValuesDoubleList()
        {
            for (int i = 0; i < 70; i++)
            {
                var randDoubleList = _randomGenerator.GetRandomDoubleList(100, -300, 900);
                var normalizedDoubleList1 = _normalizator.NormalizeValues(randDoubleList);
                Assert.AreEqual(randDoubleList.Count, normalizedDoubleList1.Count);
                foreach (var normalizedNr in normalizedDoubleList1)
                {
                    Assert.IsTrue(normalizedNr >= 0 && normalizedNr <= 1);
                }

                var normalizedDoubleList2 = _normalizator.NormalizeValues(randDoubleList, 50, 7000);
                Assert.AreEqual(randDoubleList.Count, normalizedDoubleList2.Count);
                foreach (var normalizedNr in normalizedDoubleList2)
                {
                    Assert.IsTrue(normalizedNr >= 50 && normalizedNr <= 7000);
                }
                
                var normalizedDoubleList3 = _normalizator.NormalizeValues(randDoubleList, -22, 19);
                Assert.AreEqual(randDoubleList.Count, normalizedDoubleList3.Count);
                foreach (var normalizedNr in normalizedDoubleList3)
                {
                    Assert.IsTrue(normalizedNr >= -22 && normalizedNr <= 19);
                }
            }
        }

        [TestMethod]
        public void NormalizeValuesNullableDoubleList()
        {
            for (int i = 0; i < 390; i++)
            {
                var randNullableDoubleList = _randomGenerator.GetRandomNullableDoubleList(130, -230, 170);
                var normalizedDoubleList1 = _normalizator.NormalizeValues(randNullableDoubleList);
                Assert.AreEqual(randNullableDoubleList.Count, normalizedDoubleList1.Count);
                foreach (var normalizedNr in normalizedDoubleList1)
                {
                    if (normalizedNr.HasValue)
                    {
                        Assert.IsTrue(normalizedNr >= 0 && normalizedNr <= 1);
                    }                    
                }

                var normalizedDoubleList2 = _normalizator.NormalizeValues(randNullableDoubleList, 10, 60);
                Assert.AreEqual(randNullableDoubleList.Count, normalizedDoubleList2.Count);
                foreach (var normalizedNr in normalizedDoubleList2)
                {
                    if (normalizedNr.HasValue)
                    {
                        Assert.IsTrue(normalizedNr >= 10 && normalizedNr <= 60);
                    }
                }

                var normalizedDoubleList3 = _normalizator.NormalizeValues(randNullableDoubleList, -14, 578);
                Assert.AreEqual(randNullableDoubleList.Count, normalizedDoubleList3.Count);
                foreach (var normalizedNr in normalizedDoubleList3)
                {
                    if (normalizedNr.HasValue)
                    {
                        Assert.IsTrue(normalizedNr >= -14 && normalizedNr <= 578);
                    }
                }
            }
        }
    }
}
