using LinkBox.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LinkBox.Contexts
{
    public class LinkboxDbContext : DbContext
    {

        public DbSet<UserEntity> Users { get; set; }

        public DbSet<LinkEntity> Links { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }


        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return base.SaveChangesAsync();
        }
    }
}
