using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace KTB.Core.Entities.Common
{
    public class KTBContext: DbContext
    {
        private readonly string _connectionString;
        public KTBContext(IConfiguration configuration)
        {
            _connectionString = configuration.GetSection("ConnectionStrings:DefaultConnection").Value;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
          
        }
    }
}
