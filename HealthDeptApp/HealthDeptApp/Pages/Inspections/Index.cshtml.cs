using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HealthDeptApp.Models;

namespace HealthDeptApp.Pages.Inspections
{
    public class IndexModel : PageModel
    {
        private readonly HealthDeptApp.Models.AppDbContext _context;

        public IndexModel(HealthDeptApp.Models.AppDbContext context)
        {
            _context = context;
        }

        public IList<Inspection> Inspection { get;set; }

        public async Task OnGetAsync()
        {
            Inspection = await _context.Inspections
                .Include(i => i.Agent)
                .Include(i => i.Establishment).Take(500).ToListAsync();
        }
    }
}
