using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using HealthDeptApp.Models;

namespace HealthDeptApp.Pages.Inspections
{
    public class CreateModel : PageModel
    {
        private readonly HealthDeptApp.Models.AppDbContext _context;

        public CreateModel(HealthDeptApp.Models.AppDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["AgentId"] = new SelectList(_context.Agents, "Id", "Id");
        ViewData["EstablishmentId"] = new SelectList(_context.Establishments, "Id", "Id");
            return Page();
        }

        [BindProperty]
        public Inspection Inspection { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Inspections.Add(Inspection);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}