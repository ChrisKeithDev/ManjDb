using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ManjDb.DataModels
{
    public class MyDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        #region Properties
        public DbSet<ChildInfo> ChildInfos { get; set; }
        public DbSet<FormTemplateIds> FormTemplateIds { get; set; }
        public DbSet<Classrooms> Classrooms { get; set; }
        public string? DatabaseFullPath { get; private set; }
        
        private readonly ILogger<MyDbContext> _logger;
        #endregion

        public MyDbContext(DbContextOptions<MyDbContext> options, ILogger<MyDbContext> logger)
            : base(options)
        {
            _logger = logger;  // <-- Initialize the logger
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Load configuration
                var config = new ConfigurationBuilder()
                    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                    .AddJsonFile("dbappsettings.json")
                    .Build();

                // Get the database path from the configuration
                string? dbPath = config["Database:DbPath"];

                // Check for null before combining paths
                if (dbPath != null)
                {
                    string fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, dbPath);
                    DatabaseFullPath = fullPath;

                    // Check for null before creating directory
                    var dirName = Path.GetDirectoryName(fullPath);
                    if (dirName != null)
                    {
                        Directory.CreateDirectory(dirName);
                    }

                    optionsBuilder.UseSqlite($"Data Source={fullPath}");
                }
                else
                {
                    _logger.LogWarning("Database path is not set in configuration.");
                }
            }
        }
    }
}
