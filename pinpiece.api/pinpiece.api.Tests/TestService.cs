using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using pinpiece.api.Services;
using pinpiece.api.Models;
using System.Collections.Generic;
using System;

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
        public async Task TestInsertReload() { 
            Reload testRecord = new Reload{
                UserId = 322331423,
                Token= "sdnhfwsesefsghtdsfef",
                Coord = new Coord{ lng = 144.96653, lat = -37.81756 }
            };

            int result = await srv.InsertReloadData(testRecord);
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public async Task TestInsertPin() {
            Pin testPin = new Pin {
                PinId = 32322,
                UserId = 45235342,
                Token = "ghdf-ssfed-sheih-fhg",
                Gender = "F",
                Coord = new Coord { lng = 144.963223, lat = -37.84422 },
                CreatedDate = DateTime.UtcNow,
                IsPrivate = false
            };

            Pin result = await srv.InsertPinPostData(testPin);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task TestNearPin() {
            Coord coord = new Coord { 
                lng = 144.96333, 
                lat = -37.84222
            };

            IList<Pin> list = await srv.RetreiveNearByPins(coord);

            Assert.IsNotNull(list);
        }
    }
}
