namespace pinpiece.api.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using pinpiece.domain.Models;
    using pinpiece.domain.Dto;
    using pinpiece.service.Interface;
    using pinpiece.service.Services;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    [TestClass]
    public class TestMongoService
    {
        IMongoService srv = new MongoService();

        [TestMethod]
        public async Task TestmLabConnection()
        {
            IList<Restaurant> list = await srv.MLabConnection();

            Assert.AreEqual(40, list.Count);
        }

        [TestMethod]
        public async Task TestReturnAllPins() {
            IList<dtoPin> list = await srv.RetreiveAllPins();

            Assert.IsNotNull(list);
        }

        [TestMethod]
        public async Task TestInsertReload() { 
            Reload testRecord = new Reload{
                UserId = 242344234,
                Token= "sdfsdfheisefhie",
                Coord = new Coord{ lng = 144.966222, lat = -37.8175323 }
            };

            int result = await srv.InsertReloadData(testRecord);
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public async Task TestInsertPin() {
            Pin testPin = new Pin {
                PinId = 324234,
                UserId = 3424234234,
                Token = "deas-3hhr-seef-sef",
                Gender = "F",
                Coord = new Coord { lng = 144.963226622, lat = -37.8442124 },
                CreatedDate = DateTime.UtcNow,
                IsPrivate = false
            };

            dtoPin result = await srv.InsertPinPostData(testPin);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task TestNearPin() {
            Coord coord = new Coord {
                lng = 144.96116772485357,
                lat = -37.8137466139751
            };

            IList<dtoPin> list = await srv.RetreiveNearByPins(coord);

            Assert.IsNotNull(list);
        }

        [TestMethod]
        public async Task TestNearPinWithDistance()
        {
            Coord coord = new Coord
            {
                lng = 144.96116772485357,
                lat = -37.8137466139751
            };

            IList<dtoPin> list = await srv.RetreiveNearByWithDistancePins(coord);

            Assert.IsNotNull(list);
        }
    }
}
