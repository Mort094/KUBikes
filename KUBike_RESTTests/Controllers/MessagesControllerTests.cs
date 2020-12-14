using Microsoft.VisualStudio.TestTools.UnitTesting;
// using KUBike_REST.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using KUBike_REST_Core_5.Controllers;

namespace KUBike_RESTTests.Controllers
{
    [TestClass()]
    public class MessagesControllerTests
    {
        MessagesController cmd = new MessagesController();

        [TestMethod()]
        public void GetTest()
        {
            Assert.AreEqual(22, cmd.Get().Count());
        }
        [TestMethod()]
        public void GetBikeTest()
        {
            Assert.AreEqual(12, cmd.GetWithBike(1).Count());
        }
        [TestMethod()]
        public void GetIdTest()
        {
            Assert.AreEqual(12, cmd.Get(12).messages_Id);
            Assert.IsNotNull(cmd.Get(12));
        }
        [TestMethod()]
        public void PostTest()
        {
            lib.Message i = new lib.Message(100, 1, 1, "string", "string", 1);
            cmd.Post(i);
            Assert.AreEqual(100, cmd.GetWithBike(1));
        }
        [TestMethod()]
        public void oneTest()
        {
            Assert.AreEqual(true, cmd.one(1));
        }
        [TestMethod()]
        public void twoTest()
        {
            Assert.AreEqual(true, cmd.two(1));
        }
        [TestMethod()]
        public void threeTest()
        {
            Assert.AreEqual(true, cmd.three(1));
        }
        [TestMethod()]
        public void stolenTest()
        {
            Assert.AreEqual(true, cmd.stolen(1));
        }
    }
}
