using JlgCommon.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JlgCommonTests.Extensions
{
    [TestClass]
    public class StringExtensionsTests
    {
        [TestMethod]
        public void Capitalize()
        {
            Assert.AreEqual("Dan",
                            "dan".Capitalize(), false);

            Assert.AreEqual("Dan Misailescu",
                            " dan misailescu   ".Capitalize(), false);

            Assert.AreEqual("This Is A Very Long Text",
                            " thIs    is a   vERy  LONG texT  ".Capitalize(), false);

            Assert.AreEqual("Dan.Misailescu",
                           "   dan.misailescu ".Capitalize(), false);

            Assert.AreEqual("A/Ba Ap\\Ca A.Ddd A;E Ahi,Fgj",
                            " a/bA   ap\\ca  a.dDd a;e     ahi,fgj   ".Capitalize(), false);
        }

        
        [TestMethod]
        public void ReplaceMultipleSpacesWithSingleSpace()
        {
            Assert.AreEqual(" Dan Misailescu ",
                            "      Dan     Misailescu    ".ReplaceMultipleSpacesWithSingleSpace(), false);
        }


        [TestMethod]
        public void LowerCaseAndIgnoreSpaces()
        {
            Assert.AreEqual("thisisaverylongtext",
                            " thIs    is a   vERy  LONG texT  ".LowerCaseAndIgnoreSpaces(), false);
        }


        [TestMethod]
        public void ToValidSqlTableOrColumnName()
        {
            Assert.AreEqual("dan_misailescu",
                            "Dan Misailescu".ToSqlValidTableOrColumnName(), false);

            Assert.AreEqual("name_and_1_surename",
                            " name - And   1 $& surename ".ToSqlValidTableOrColumnName(), false);            
        }

        [TestMethod]
        public void IsEmail()
        {
            Assert.IsTrue("some.mail@provider.ro".IsEmail());
            Assert.IsFalse("some.mail@.com".IsEmail());
        }

    }
}
