

namespace pinpiece.data.Interface
{
    using pinpiece.domain.Entity;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Core.Objects;
    using System.Data.Entity.Infrastructure;
    using System.Threading.Tasks;
    public interface IGeoGoDb : IDisposable
    {
        Database Database { get; }
        int SaveChanges();
        Task<int> SaveChangesAsync();
        DbEntityEntry Entry(object entity);
        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
        ObjectContext BaseContext { get; }

        #region entities
        DbSet<tbPin> tbPins { get; set; }
        DbSet<tbPinComment> tbPinComments { get; set; }
        DbSet<tbUser> tbUsers { get; set; }
        DbSet<tbPinUser> tbPinUsers { get; set; }
        #endregion
    }
}
