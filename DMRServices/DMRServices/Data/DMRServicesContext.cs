using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DMRServices.Models;

namespace DMRServices.Models
{
    public class DMRServicesContext : DbContext
    {
        public DMRServicesContext (DbContextOptions<DMRServicesContext> options)
            : base(options)
        {
        }

        public DbSet<DMRServices.Models.Case> Case { get; set; }

        public DbSet<DMRServices.Models.Customer> Customer { get; set; }

        public DbSet<DMRServices.Models.OrderLineItem> OrderLineItem { get; set; }

        public DbSet<DMRServices.Models.Order> Order { get; set; }

        public DbSet<DMRServices.Models.Product> Product { get; set; }
    }
}
