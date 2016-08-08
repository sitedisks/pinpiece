namespace pinpiece.service.Interface
{
    using pinpiece.domain.Entity;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IEntityService : IDisposable
    {
        Task<IList<tbPin>> GetAllPins();
    }
}
