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

        [Serializable]
        class Author
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public int Age { get; set; }
        }

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

            Assert.AreNotEqual(author, authorDeserialized);
            Assert.AreEqual(author.FirstName, authorDeserialized.FirstName);
            Assert.AreEqual(author.LastName, authorDeserialized.LastName);
            Assert.AreEqual(author.Age, authorDeserialized.Age);
        }

        

    }
}
