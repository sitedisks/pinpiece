using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using pinpiece.api.Services;
using pinpiece.api.Models;
using System.Collections.Generic;
using System;
using pinpiece.api.Models.Dto;

namespace pinpiece.api.Tests
{
    [TestClass]
    public class TestService
    {
        DBAccessService srv = new DBAccessService();

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
                lng = 144.96333, 
                lat = -37.84222
            };

            IList<dtoPin> list = await srv.RetreiveNearByPins(coord);

            Assert.IsNotNull(list);
        }
    }
}
