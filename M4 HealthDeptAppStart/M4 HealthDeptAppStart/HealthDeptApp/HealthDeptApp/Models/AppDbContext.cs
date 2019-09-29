using Microsoft.EntityFrameworkCore;
using HealthDeptApp.Models;

namespace HealthDeptApp.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
                : base(options)
        {
        }

        public DbSet<Agent> Agents { get; set; }
        public DbSet<Establishment> Establishments { get; set; }
        public DbSet<EstablishmentCategory> EstablishmentCategories { get; set; }
        public DbSet<Inspection> Inspections { get; set; }
        public DbSet<HealthCode> HealthCodes { get; set; }
    }
}