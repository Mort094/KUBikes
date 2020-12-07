using Microsoft.VisualStudio.TestTools.UnitTesting;
// using KUBike_REST.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using KUBike_REST_Core_5.Controllers;
using lib;

namespace KUBike_REST.Controllers.Tests
{
    [TestClass()]
    public class TripControllerTests
    {
        TripController cmd = new TripController();

        [TestMethod()]
        //For at testet GETALL funktionen i unittest. Vi tester dette igennem når vi tjekker for mange items der er i listen. Derfor bruger vi Assert.AreEqual, til at se om de har samme antal af items i listen. 
        public void GetTest()
        {
            Assert.AreEqual(2, cmd.Get().Count());
        }
        [TestMethod()]
        public void GetUser()
        {
            Assert.AreEqual(1, cmd.GetUser(1));
            Assert.IsNotNull(cmd.GetUser(1));
        }
      
        [TestMethod()]
        public void GetIdTest()
        {
            Assert.AreEqual(2, cmd.Get(2).Cycle_id);
            Assert.IsNotNull(cmd.Get(2));
        }
        [TestMethod()]
        public void HentEnBruger()
        {
          //  Assert.AreEqual(12, cmd.Get(55,1).Cycle_id);
            Assert.IsNotNull(cmd.Get(55,1));
        }

        [TestMethod()]
        public void PostTest()
        {
            Trip i = new Trip(100, "string", "string", "string", 1, 1);
            cmd.Post(i);
            Assert.AreEqual(13, cmd.Get(100));
        }
        [TestMethod()]
        public void SlutTripTest()
        {
           // Assert.AreEqual(true, cmd.AfslutTrip(1));
        }
    }

}