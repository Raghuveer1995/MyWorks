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
    public class DetailsModel : PageModel
    {
        private readonly HealthDeptApp.Models.AppDbContext _context;

        public DetailsModel(HealthDeptApp.Models.AppDbContext context)
        {
            _context = context;
        }

        public Inspection Inspection { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Inspection = await _context.Inspections
                .Include(i => i.Agent)
                .Include(i => i.Establishment).FirstOrDefaultAsync(m => m.Id == id);

            if (Inspection == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
