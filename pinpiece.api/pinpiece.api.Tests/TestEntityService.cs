namespace pinpiece.api.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using pinpiece.service.Interface;
    using pinpiece.service.Services;
    using pinpiece.data.DbContext;
    using pinpiece.data.Interface;
    using System.Threading.Tasks;
    using pinpiece.domain.Entity;
    using System.Collections.Generic;

    [TestClass]
    public class TestEntityService
    {
        [TestMethod]
        public async Task TestAllPins()
        {
            using (IGeoGoDb _db = new GeoGoDb())
            using (IEntityService _svc = new EntityService(_db)) {

                IList<tbPin> list = await _svc.GetAllPins();

                Assert.IsNotNull(list);
            }

        
        }
    }
}
