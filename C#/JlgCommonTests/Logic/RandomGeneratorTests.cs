using System;
using JlgCommon.Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
        public void GetRandomNullableIntList()
        {
            for (int i = 0; i < 90; i++)
            {
                var randIntList = _randomGenerator.GetRandomNullableIntList(47, -1000, 900);
                Assert.IsTrue(randIntList.Count == 47);
                foreach (var randIntNullable in randIntList)
                {
                    if (randIntNullable.HasValue)
                    {
                        Assert.IsTrue(randIntNullable >= -1000 && randIntNullable < 900);
                    }                    
                }
            }
        }        

        [TestMethod]
        public void GetRandomDoubleList()
        {
            for (int i = 0; i < 70; i++)
            {
                var randDoubleList = _randomGenerator.GetRandomDoubleList(17, -3, 9);
                Assert.IsTrue(randDoubleList.Count == 17);
                foreach (var randDouble in randDoubleList)
                {
                    Assert.IsTrue(randDouble >= -3 && randDouble < 9);
                }
            }
        }

        [TestMethod]
        public void GetRandomNullableDoubleList()
        {
            for (int i = 0; i < 60; i++)
            {
                var randDoubleList = _randomGenerator.GetRandomNullableDoubleList(50, 56, 134);
                Assert.IsTrue(randDoubleList.Count == 50);
                foreach (var randIntNullable in randDoubleList)
                {
                    if (randIntNullable.HasValue)
                    {
                        Assert.IsTrue(randIntNullable >= 56 && randIntNullable < 134);
                    }                    
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
            var first2002 = new DateTime(2002, 1, 1);
            var first2015 = new DateTime(2015, 1, 1);
            for (int i = 0; i < 100; i++)
            {
                var randDateTime = _randomGenerator.GetRandomDateTime(2002, 2015);
                Assert.IsTrue(randDateTime >= first2002 && randDateTime < first2015);
            }
        }

        [TestMethod]
        public void GetRandomDateTimeList()
        {
            var first1900 = new DateTime(1900, 1, 1);
            var first2250 = new DateTime(2250, 1, 1);
            for (int i = 0; i < 50; i++)
            {
                var randDateTimeList = _randomGenerator.GetRandomDateTimeList(30, 1900, 2250);
                Assert.IsTrue(randDateTimeList.Count == 30);
                foreach (var randDateTime in randDateTimeList)
                {
                    Assert.IsTrue(randDateTime >= first1900 && randDateTime < first2250);
                }                
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
