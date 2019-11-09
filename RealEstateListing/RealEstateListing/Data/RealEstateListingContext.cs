using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace RealEstateListing.Models
{
    public class RealEstateListingContext : DbContext
    {
        public RealEstateListingContext (DbContextOptions<RealEstateListingContext> options)
            : base(options)
        {
        }

        public DbSet<RealEstateListing.Models.Asset> Asset { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            {
                modelBuilder.Entity<Asset>().HasData(
                    new Asset
                    {
                        Id = 1,
                        Type = "Open Land",
                        Location = "Cincinnati"
                    });

                modelBuilder.Entity<Asset>().HasData(
                    new Asset
                    {
                        Id = 2,
                        Type = "Apartment",
                        Location = "Columbus"
                    });

                modelBuilder.Entity<Asset>().HasData(
                    new Asset
                    {
                        Id = 3,
                        Type = "Farm House",
                        Location = "Cincinnati"
                    });

                modelBuilder.Entity<Asset>().HasData(
                    new Asset
                    {
                        Id = 4,
                        Type = "Building",
                        Location = "Dayton"
                    });

                base.OnModelCreating(modelBuilder);
            }
        }
    }
}
