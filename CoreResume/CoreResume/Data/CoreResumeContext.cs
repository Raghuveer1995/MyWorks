using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CoreResume.Models;

namespace CoreResume.Models
{
    public class CoreResumeContext : DbContext
    {
        public CoreResumeContext (DbContextOptions<CoreResumeContext> options)
            : base(options)
        {
        }

        public DbSet<CoreResume.Models.Customer> Customer { get; set; }

        public DbSet<CoreResume.Models.Case> Case { get; set; }
    }
}
