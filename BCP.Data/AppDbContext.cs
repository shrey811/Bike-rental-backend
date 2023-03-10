using BCP.Core.Entities;
using BCP.Core.Entities.user;
using BCP.Core.Enums;
using BCP.Data.Configurations;
using BCP.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Npgsql;

namespace BCP.Infrastructure
{
    public class AppDbContext : DbContext
    {
        private readonly string _connectionString;

        public DbSet<OtpCode> Otp { get; set; }
        public DbSet<RentEntry> Rent { get; set; }
        // add more DbSets for other entities

        public AppDbContext(DbContextOptions<AppDbContext> options, IOptions<DbConfig> dbConfig) : base(options)
        {
            _connectionString = GetConnectionString(dbConfig.Value);
        }

        private static string GetConnectionString(DbConfig dbConfig)
        {
            var builder = new NpgsqlConnectionStringBuilder()
            {
                Host = dbConfig.Host,
                Database = dbConfig.Database,
                Username = dbConfig.User,
                Password = dbConfig.Password,
                Port = dbConfig.Port,
                IncludeErrorDetail = true
            };
            return builder.ConnectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(_connectionString).UseSnakeCaseNamingConvention();
            }
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
        
            // Apply other configurations for other entities
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new BikeConfiguration());
            modelBuilder.ApplyConfiguration(new RentEntryConfiguration());

        }
    }
}
