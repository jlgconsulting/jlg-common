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
    public class RandomGeneratorTests
    {
        private RandomGenerator _randomGenerator = new RandomGenerator();
       
        [TestMethod]
        public void GetRandomIntBetween()
        {
            for (int i = 0; i < 100; i++)
            {
                var randInt = _randomGenerator.GetRandomIntBetween(1342, 34520);
                Assert.IsTrue(randInt >= 1342 && randInt < 34520);
            }
        }

        [TestMethod]
        public void GetRandomString()
        {
            for (int i = 0; i < 100; i++)
            {
                var randString = _randomGenerator.GetRandomString(30);
                Assert.IsTrue(randString.Length==30);
            }
        }

        [TestMethod]
        public void GetRandomIntList()
        {
            for (int i = 0; i < 100; i++)
            {
                var randIntList = _randomGenerator.GetRandomIntList(20, -245, 700);
                Assert.IsTrue(randIntList.Count == 20);
                foreach (var randInt in randIntList)
                {
                    Assert.IsTrue(randInt >= -245 && randInt < 700);
                }
            }
        }
        
        [TestMethod]
        public void GetRandomGuidList()
        {
            for (int i = 0; i < 100; i++)
            {
                var randIntList = _randomGenerator.GetRandomGuidList(15);
                Assert.IsTrue(randIntList.Count == 15);                
            }
        }  
    
        [TestMethod]
        public void GetRandomDateTime()
        {
            var first1800 = new DateTime(1800, 1, 1);
            var first2250 = new DateTime(2250, 1, 1);
            for (int i = 0; i < 100; i++)
            {
                var randDate = _randomGenerator.GetRandomDateTime(1800, 2250);
                Assert.IsTrue(randDate >= first1800 && randDate < first2250);
            }
        }

        [TestMethod]
        public void GetRandomTimeSpan()
        {
            for (int i = 0; i < 100; i++)
            {
                var randTimeSpan = _randomGenerator.GetRandomTimeSpan();
                Assert.IsTrue(randTimeSpan.Ticks > 0);
            }
        }     
        
    }
}
