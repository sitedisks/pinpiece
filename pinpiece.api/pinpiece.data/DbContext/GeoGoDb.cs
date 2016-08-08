namespace pinpiece.data.DbContext
{
    using Interface;
    using pinpiece.domain.Entity;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    public partial class GeoGoDb : DbContext, IGeoGoDb
    {
        public GeoGoDb() : base("name=GeoGoDbContext") { }
        public GeoGoDb(string connectionString) : base(connectionString) { }

        #region entities
        public virtual DbSet<tbPin> tbPins { get; set; }
        public virtual DbSet<tbPinComment> tbPinComments { get; set; }
        public virtual DbSet<tbUser> tbUsers { get; set; }
        public virtual DbSet<tbPinUser> tbPinUsers { get; set; }
        #endregion

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<tbPin>()
                .Property(e => e.ImageUri)
                .IsUnicode(false);

            modelBuilder.Entity<tbPin>()
                .Property(e => e.Text)
                .IsUnicode(false);

            modelBuilder.Entity<tbPin>()
                .HasMany(e => e.tbPinComments)
                .WithRequired(e => e.tbPin)
                .HasForeignKey(e => e.PinId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbPin>()
                .HasMany(e => e.tbPinUsers)
                .WithRequired(e => e.tbPin)
                .HasForeignKey(e => e.PinId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbPinComment>()
                .Property(e => e.Comment)
                .IsUnicode(false);

            modelBuilder.Entity<tbPinUser>()
                .Property(e => e.MobileNumber)
                .IsUnicode(false);

            modelBuilder.Entity<tbUser>()
                .Property(e => e.DisplayName)
                .IsUnicode(false);

            modelBuilder.Entity<tbUser>()
                .Property(e => e.Gender)
                .IsUnicode(false);

            modelBuilder.Entity<tbUser>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<tbUser>()
                .Property(e => e.MobileNumber)
                .IsUnicode(false);

            modelBuilder.Entity<tbUser>()
                .Property(e => e.AuthToken)
                .IsUnicode(false);

            modelBuilder.Entity<tbUser>()
                .Property(e => e.DeviceToken)
                .IsUnicode(false);

            modelBuilder.Entity<tbUser>()
                .HasMany(e => e.tbPins)
                .WithRequired(e => e.tbUser)
                .HasForeignKey(e => e.UserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbUser>()
                .HasMany(e => e.tbPinComments)
                .WithRequired(e => e.tbUser)
                .HasForeignKey(e => e.UserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbUser>()
                .HasMany(e => e.tbPinUsers)
                .WithOptional(e => e.tbUser)
                .HasForeignKey(e => e.UserId);
        }

        public System.Data.Entity.Core.Objects.ObjectContext BaseContext
        {
            get { return ((IObjectContextAdapter)this).ObjectContext; }
        }
    }
}
