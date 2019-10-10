using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DMRServices.Models;

namespace DMRServices.Pages.OrderLineItems
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
        ViewData["OrderId"] = new SelectList(_context.Set<Order>(), "ID", "ID");
        ViewData["ProductId"] = new SelectList(_context.Set<Product>(), "ID", "Name");
            return Page();
        }

        [BindProperty]
        public OrderLineItem OrderLineItem { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.OrderLineItem.Add(OrderLineItem);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}