﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
// using KUBike_REST.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using KUBike_REST_Core_5.Controllers;

namespace KUBike_REST.Controllers.Tests
{
    [TestClass()]
    public class CyclesControllerTests
    {
        CyclesController cmd = new CyclesController();

        [TestMethod()]
        //For at testet GETALL funktionen i unittest. Vi tester dette igennem når vi tjekker for mange items der er i listen. Derfor bruger vi Assert.AreEqual, til at se om de har samme antal af items i listen. 
        public void GetTest()
        {
            Assert.AreEqual(2, cmd.Get().Count());
        }
        [TestMethod()]
        public void GetAdminTest()
        {
            Assert.AreEqual(2, cmd.Get2().Count());
        }
        [TestMethod()]
        public void GetIdTest()
        {
            Assert.AreEqual(2, cmd.Get(2).Cycle_id);
            Assert.IsNotNull(cmd.Get(2));
        }
        [TestMethod()]
        public void GetLedigeIdTest()
        {
            Assert.AreEqual(1, cmd.Get2(1).Cycle_id);
            Assert.IsNotNull(cmd.Get2(1));
        }

        [TestMethod()]
        public void StartRuteTest()
        {
            Assert.AreEqual(true, cmd.Start(1));
        }
        [TestMethod()]
        public void SlutRuteTest()
        {
            cmd.Start(1);
            Assert.AreEqual(true, cmd.Slut(1));
        }
    }
}