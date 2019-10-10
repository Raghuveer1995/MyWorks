using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreCrud.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CoreCrud.Pages
{
    public class TestModel : PageModel
    {
        private readonly CoreCrud.Models.CoreCrudContext _context;

        public TestModel(CoreCrud.Models.CoreCrudContext context)
        {
            _context = context;
        }
        public IActionResult OnGet()
        {
            ViewData["CountryID"] = new SelectList(_context.Country, "ID", "Name");
            //Destination.CountryID = id;
            return Page();
        }
        public Destination Destination { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                ViewData["CountryID"] = new SelectList(_context.Country, "ID", "Name");
                return Page();
            }
            _context.Destination.Add(Destination);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}