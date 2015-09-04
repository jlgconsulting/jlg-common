using System.Collections.Generic;
using System.Linq;
using JlgCommon.Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JlgCommonTests.Logic
{
    [TestClass]
    public class ReflectionManagerTests
    {
        private ReflectionManager _reflectionHelper = new ReflectionManager();

        [TestMethod]
        public void BuildPropertyGetter()
        {
            //It is much more efficient to compile a getter function using expression trees and reuse it (instead of directly using reflection each time you need this).

            var author = new Author();
            author.FirstName = "Dan";
            author.LastName = "Misailescu";
            author.Age = 29;

            var author2 = new Author();
            author2.FirstName = "Gheorghe";
            author2.LastName = "Popescu";
            author2.Age = 20;    
                        
            var firstNameGetter = _reflectionHelper.BuildPropertyGetter(author.GetType(), "FirstName");
            Assert.AreEqual("Dan", firstNameGetter(author));

            var lastNameGetter = _reflectionHelper.BuildPropertyGetter(author.GetType(), "LastName");
            Assert.AreEqual("Misailescu", lastNameGetter(author));

            var ageGetter = _reflectionHelper.BuildPropertyGetter(author.GetType(), "Age");
            Assert.AreEqual(29, ageGetter(author));

            var authors = new List<Author>() { author, author2 };

            var authorsByAge = authors.OrderBy(o => ageGetter(o)).ToList();
            Assert.AreEqual(20, authorsByAge[0].Age);
            Assert.AreEqual(29, authorsByAge[1].Age);

            var authorsByFirstName = authors.OrderBy(o => firstNameGetter(o)).ToList();
            Assert.AreEqual("Dan", authorsByFirstName[0].FirstName);
            Assert.AreEqual("Gheorghe", authorsByFirstName[1].FirstName);
            
        }
    }
}
