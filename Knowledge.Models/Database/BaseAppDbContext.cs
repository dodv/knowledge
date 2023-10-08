using Knowledge.Models.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Knowledge.Models.Database
{
    public class BaseAppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public BaseAppDbContext(
            DbContextOptions options
        ) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // keep the table names in db matching the model names instead of the DbSet names
            // as per: https://github.com/aspnet/Announcements/issues/167
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                entity.SetTableName(entity.DisplayName());
            }

            modelBuilder.Entity<User>().HasQueryFilter(p => !p.Deleted.HasValue);
            
            #region User

            modelBuilder.Entity<User>()
                .HasMany(t => t.Roles)
                .WithOne()
                .HasForeignKey(t => t.UserId);

            #endregion

        }
    }
}
