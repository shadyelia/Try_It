using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TryIt.Core.Entities;
using TryIt.SharedKernel.Data;

namespace TryIt.Infrastructure.Data
{
    public class SqliteDataContext : DataContext
    {
        public SqliteDataContext(IConfiguration configuration) : base(configuration)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to sql server database
            options.UseSqlite(Configuration.GetConnectionString("WebApiDatabase"));
        }

        public DbSet<User> Users { get; set; }
    }
}
