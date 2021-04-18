using System;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Data
{
    public class MyDbContext : DbContext
    {
        private IConfigurationRoot _config;
        public string ConnectionString { get; set; }

        public DbSet<SMS> SMSs { get; set; }
        public DbSet<ApiKey> ApiKeys { get; set; }

        public MyDbContext(IConfigurationRoot config, DbContextOptions options) : base(options)
        {
            _config = config;
            ConnectionString = _config["ConnectionString"];
        }
        public MyDbContext(string connectionString, DbContextOptions options) : base(options)
        {
            ConnectionString = connectionString;
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseNpgsql(ConnectionString);
            optionsBuilder.EnableSensitiveDataLogging(true);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
