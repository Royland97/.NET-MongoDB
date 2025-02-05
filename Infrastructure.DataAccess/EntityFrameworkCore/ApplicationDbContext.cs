using Core.Domain.Yelp;
using Microsoft.EntityFrameworkCore;
using MongoDB.EntityFrameworkCore.Extensions;

namespace Infrastructure.DataAccess.EntityFrameworkCore
{
    /// <summary>
    /// Application DbContext
    /// </summary>                                   
    public class ApplicationDbContext: DbContext
    {
        #region Constructor

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        
        public DbSet<User> Users { get; set; }
        public DbSet<Business> Businesses { get; set; }
        public DbSet<Chekin> Chekins { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Tip> Tips { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().ToCollection("user");
            modelBuilder.Entity<Business>().ToCollection("business");
            modelBuilder.Entity<Chekin>().ToCollection("chekin");
            modelBuilder.Entity<Review>().ToCollection("review");
            modelBuilder.Entity<Tip>().ToCollection("tips");
        }

        #endregion
    }
}
