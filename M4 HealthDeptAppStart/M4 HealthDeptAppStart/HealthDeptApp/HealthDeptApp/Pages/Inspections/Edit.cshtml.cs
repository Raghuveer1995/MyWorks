using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HealthDeptApp.Models;

namespace HealthDeptApp.Pages.Inspections
{
    public class EditModel : PageModel
    {
        private readonly HealthDeptApp.Models.AppDbContext _context;

        public EditModel(HealthDeptApp.Models.AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
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
           ViewData["AgentId"] = new SelectList(_context.Agents, "Id", "Id");
           ViewData["EstablishmentId"] = new SelectList(_context.Establishments, "Id", "Id");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Inspection).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InspectionExists(Inspection.Id))
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

        private bool InspectionExists(int id)
        {
            return _context.Inspections.Any(e => e.Id == id);
        }
    }
}
