using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HealthDeptApp.Models;

namespace HealthDeptApp.Pages.EstablishmentCategories
{
    public class DetailsModel : PageModel
    {
        private readonly HealthDeptApp.Models.AppDbContext _context;

        public DetailsModel(HealthDeptApp.Models.AppDbContext context)
        {
            _context = context;
        }

        public EstablishmentCategory EstablishmentCategory { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            EstablishmentCategory = await _context.EstablishmentCategories.FirstOrDefaultAsync(m => m.Id == id);

            if (EstablishmentCategory == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
