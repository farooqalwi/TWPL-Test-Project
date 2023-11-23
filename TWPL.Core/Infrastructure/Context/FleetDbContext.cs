using Microsoft.EntityFrameworkCore;
using TWPL.Common.Model.Entities;

namespace TWPL.Core.Infrastructure.Context
{
    public class FleetDbContext : DbContext
    {
        public FleetDbContext(DbContextOptions<FleetDbContext> options) : base(options)
        {
            Database.EnsureCreated();
            EnsureTablesCreated();
        }

        public DbSet<Vehicle> Vehicle { get; set; }

        private void EnsureTablesCreated()
        {
            if (!Database.GetPendingMigrations().Any())
            {
                // Apply migrations if there are any pending
                Database.Migrate();
            }
            else
            {
                // Create tables if not already created
                Database.EnsureCreated();
            }
        }
    }
}
