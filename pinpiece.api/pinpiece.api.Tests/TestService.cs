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
        [TestMethod]
        public async Task TestmLabConnection()
        {
            DBAccessService srv = new DBAccessService();
            IList<Restaurant> list = await srv.MLabConnection();

            Assert.AreEqual(40, list.Count);
        }
    }
}
