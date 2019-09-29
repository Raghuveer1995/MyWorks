using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HealthDeptApp.Models;

namespace HealthDeptApp.Pages.HealthCodes
{
    public class DetailsModel : PageModel
    {
        private readonly HealthDeptApp.Models.AppDbContext _context;

        public DetailsModel(HealthDeptApp.Models.AppDbContext context)
        {
            _context = context;
        }

        public HealthCode HealthCode { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            HealthCode = await _context.HealthCodes.FirstOrDefaultAsync(m => m.Id == id);

            if (HealthCode == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
