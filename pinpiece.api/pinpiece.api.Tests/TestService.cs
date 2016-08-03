using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using pinpiece.api.Services;
using pinpiece.api.Models;
using System.Collections.Generic;

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
        public async Task TestInsertMLab() { 
            Reload testRecord = new Reload{
                UserId = 322324423,
                Token= "sdfsfssesefsghtdsfef",
                Coord = new Coord{ lng = 144.9633, lat = -37.8141 }
            };

            int result = await srv.InsertReloadData(testRecord);
            Assert.AreEqual(1, result);
        }
    }
}
