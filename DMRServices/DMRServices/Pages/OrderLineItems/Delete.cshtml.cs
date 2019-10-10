using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DMRServices.Models;

namespace DMRServices.Pages.OrderLineItems
{
    public class DeleteModel : PageModel
    {
        private readonly DMRServices.Models.DMRServicesContext _context;

        public DeleteModel(DMRServices.Models.DMRServicesContext context)
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
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            OrderLineItem = await _context.OrderLineItem.FindAsync(id);

            if (OrderLineItem != null)
            {
                _context.OrderLineItem.Remove(OrderLineItem);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
