
using BCP.Core.Entities.user;
using Microsoft.EntityFrameworkCore;
namespace BCP.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserDocument> UserDocuments { get; set; }
        public virtual DbSet<Registration> Registrations { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("user");
            modelBuilder.Entity<UserDocument>().ToTable("documents");
            modelBuilder.Entity<Registration>().ToTable("registration");
            modelBuilder.Entity<Registration>().HasIndex(r => r.Email).IsUnique();
        }
    }
}