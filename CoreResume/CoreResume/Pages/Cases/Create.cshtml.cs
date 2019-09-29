using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using CoreResume.Models;

namespace CoreResume.Pages.Cases
{
    public class CreateModel : PageModel
    {
        private readonly CoreResume.Models.CoreResumeContext _context;

        public CreateModel(CoreResume.Models.CoreResumeContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["CustomerId"] = new SelectList(_context.Customer, "ID", "ID");
            return Page();
        }

        [BindProperty]
        public Case Case { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Case.Add(Case);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}