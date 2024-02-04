using LinkBox.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Linq.Expressions;

namespace LinkBox.Contexts
{
    public class LinkboxDbContext : DbContext
    {

        public DbSet<UserEntity> Users { get; set; }

        public DbSet<LinkEntity> Links { get; set; }

		public DbSet<CategoryEntity> Categories { get; set; }

		public DbSet<ConfigEntity> Configs { get; set; }


		protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var dbPath = Path.Combine(Directory.GetCurrentDirectory(), "data", "linkbox.db");
            optionsBuilder.UseSqlite($"Data Source = {dbPath}");
        }


        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return base.SaveChangesAsync();
        }
    }
}
