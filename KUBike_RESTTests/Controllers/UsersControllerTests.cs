using Microsoft.VisualStudio.TestTools.UnitTesting;
using KUBike_REST.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using lib;
using System.Linq;
using KUBike_REST_Core_5.Controllers;

namespace KUBike_REST.Controllers.Tests
{
    [TestClass()]
    public class UsersControllerTests
    {
        UsersController cmd = new UsersController();
        [TestMethod()]
        //For at testet GETALL funktionen i unittest. Vi tester dette igennem når vi tjekker for mange items der er i listen. Derfor bruger vi Assert.AreEqual, til at se om de har samme antal af items i listen. 
        public void GetTest()
        {
            Assert.AreEqual(4, cmd.Get().Count());
        }
        //Her tester vi GetId funktionen.Vi tester dette igennem en Assert.AreEqual metode og Assert.IsNotNull.Igennem disse to requests finder vi ud af om itemet exist og om den har id'et 2.

        [TestMethod()]
        public void GetEmailTest()
        {
            string email = "adm@ku.dk";
            Assert.AreEqual(1, cmd.Get(email));
            Assert.IsNotNull(cmd.Get(email));
        }

        //Her tester vi Post funktionen. Vi tester dette igennem en Assert.AreEqual metode. Men før vi benytter os af denne metode opretter vi et objekt som skal bruges i testen. I er objektet vi benytte til at tjekke og telefonnummet stå over ens med telefonnummet vi har angivet i vores objekt.              
        [TestMethod()]
        public void PostTest()
        {
            User i = new User(13, "Benjamin", "Curovic", "Benj5174@ku.dk", "Secret1!", 50505050, 2);
            cmd.Post(i);
            Assert.AreEqual(13, cmd.Get("Benj5174@ku.dk"));
        }

        [TestMethod()]
        public void LoginTest()
        {
            string email = "adm@ku.dk";
            string passwordTrue = "Secret1!";
            string passwordFalse = "Secret";

            Assert.AreEqual(true, cmd.Login(email, passwordTrue));
            Assert.AreEqual(false, cmd.Login(email, passwordFalse));


        }
    }
}
      