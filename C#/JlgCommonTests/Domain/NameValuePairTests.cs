using JlgCommon.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JlgCommonTests.Domain
{
    [TestClass]
    public class NameValuePairTests
    {

        [TestMethod]
        public void ConstructorsInitializePropertiesCorrectly()
        {
            var ownerId = Guid.NewGuid();
            var nvp1 = new NameValuePair<int, string>(4, "value3", ownerId);
            Assert.IsTrue(nvp1.Id != Guid.Empty);
            Assert.IsTrue(nvp1.Name == 4);
            Assert.IsTrue(nvp1.Value == "value3");
            Assert.IsTrue(nvp1.OwnerId == ownerId);
                        
            var nvp2 = new NameValuePair<string, int>("name2", 5);
            Assert.IsTrue(nvp2.Id != Guid.Empty);
            Assert.IsTrue(nvp2.Name == "name2");
            Assert.IsTrue(nvp2.Value == 5);
            Assert.IsTrue(nvp2.OwnerId == null);

            var nvp3 = new NameValuePair<string, string>();
            Assert.IsTrue(nvp3.Id != Guid.Empty);
            Assert.IsTrue(nvp3.Name == string.Empty);
            Assert.IsTrue(nvp3.Value == string.Empty);
            Assert.IsTrue(nvp3.OwnerId == Guid.Empty);
        }
    }
}
