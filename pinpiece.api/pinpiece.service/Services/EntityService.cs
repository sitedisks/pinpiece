namespace pinpiece.service.Services
{
    using NLog;
    using pinpiece.data.Interface;
    using pinpiece.domain.Entity;
    using pinpiece.service.Interface;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Threading.Tasks;

    public class EntityService : IEntityService
    {
        private readonly IGeoGoDb _db;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public EntityService(IGeoGoDb db)
        {
            _db = db;
        }

        public async Task<IList<tbPin>> GetAllPins()
        {
            IList<tbPin> list = new List<tbPin>();

            try
            {
                list = await _db.tbPins.ToListAsync();
            }
            catch (Exception ex)
            {
                logger.Error("Error at " + DateTime.UtcNow + " >>> ", ex.Message);
            }

            return list;
        }

        #region dispose
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_db != null)
                    _db.Dispose();
            }
        }
        #endregion
    }
}
