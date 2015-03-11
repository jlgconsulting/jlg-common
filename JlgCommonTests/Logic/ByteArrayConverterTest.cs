using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JlgCommon.Extensions;
using JlgCommon.Logic;

namespace JlgCommonTests.Logic
{
    [TestClass]
    public class ByteArrayConverterTest
    {
        private ByteArrayConverter _byteArrayConverter = new ByteArrayConverter();

        [TestMethod]
        public void ObjectToByteArrayAndByteArrayToObject()
        {
            var author = new Author();
            author.FirstName = "Dan";
            author.LastName = "Misailescu";
            author.Age = 29;

            var authorByteArray = _byteArrayConverter.ObjectToByteArray(author);
            var authorDeserialized = (Author)_byteArrayConverter.ByteArrayToObject(authorByteArray);

            Assert.AreEqual("Dan", authorDeserialized.FirstName);
            Assert.AreEqual("Misailescu", authorDeserialized.LastName);
            Assert.AreEqual(29, authorDeserialized.Age);

        }

        

    }
}
