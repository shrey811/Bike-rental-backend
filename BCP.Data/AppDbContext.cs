
using BCP.Core.Entities.user;
using BCP.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Npgsql;

namespace BCP.Infrastructure
{
    public class AppDbContext : DbContext
    {
        private readonly string _connectionString;
        public AppDbContext(DbContextOptions<AppDbContext> options,IOptions<DbConfig> dbConfig) : base(options)
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
                Port = dbConfig.Port
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
            modelBuilder.ApplyConfiguration(new UserConfiguration());
        }
    }
}