using Microsoft.EntityFrameworkCore;

namespace WebApi.Models
{
    internal class UserContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserNumber> UserNumbers { get; set; }

        public UserContext(DbContextOptions<UserContext> options) 
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(x => x.UserNumbers)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserIdNumber)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasKey(x => x.IdNumber);
            modelBuilder.Entity<UserNumber>()
                .HasKey(x => x.IdNumber);
        }
    }
}
