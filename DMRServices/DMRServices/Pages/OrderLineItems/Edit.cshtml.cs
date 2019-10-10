using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DMRServices.Models;

namespace DMRServices.Pages.OrderLineItems
{
    public class EditModel : PageModel
    {
        private readonly DMRServices.Models.DMRServicesContext _context;

        public EditModel(DMRServices.Models.DMRServicesContext context)
        {
            _context = context;
        }

        [BindProperty]
        public OrderLineItem OrderLineItem { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            OrderLineItem = await _context.OrderLineItem
                .Include(o => o.Order)
                .Include(o => o.Product).FirstOrDefaultAsync(m => m.ID == id);

            if (OrderLineItem == null)
            {
                return NotFound();
            }
           ViewData["OrderId"] = new SelectList(_context.Set<Order>(), "ID", "ID");
           ViewData["ProductId"] = new SelectList(_context.Set<Product>(), "ID", "Name");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(OrderLineItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderLineItemExists(OrderLineItem.ID))
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

        private bool OrderLineItemExists(int id)
        {
            return _context.OrderLineItem.Any(e => e.ID == id);
        }
    }
}
