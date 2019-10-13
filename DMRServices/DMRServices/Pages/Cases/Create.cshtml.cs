using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DMRServices.Models;

namespace DMRServices.Pages.Cases
{
    public class CreateModel : PageModel
    {
        private readonly DMRServices.Models.DMRServicesContext _context;

        public CreateModel(DMRServices.Models.DMRServicesContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["CustomerId"] = new SelectList(_context.Customer, "ID", "LastName");                      
            return Page();
        }

        [BindProperty]
        public Case Case { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                ViewData["CustomerId"] = new SelectList(_context.Customer, "ID", "LastName");
                return Page();
            }

            _context.Case.Add(Case);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}