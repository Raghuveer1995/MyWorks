using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HealthDeptApp.Models;

namespace HealthDeptApp.Pages.EstablishmentCategories
{
    public class EditModel : PageModel
    {
        private readonly HealthDeptApp.Models.AppDbContext _context;

        public EditModel(HealthDeptApp.Models.AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(EstablishmentCategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EstablishmentCategoryExists(EstablishmentCategory.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool EstablishmentCategoryExists(int id)
        {
            return _context.EstablishmentCategories.Any(e => e.Id == id);
        }
    }
}
