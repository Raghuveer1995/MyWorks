using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DMRServices.Models;

namespace DMRServices.Pages.Orders
{
    public class DetailsModel : PageModel
    {
        private readonly DMRServices.Models.DMRServicesContext _context;

        public DetailsModel(DMRServices.Models.DMRServicesContext context)
        {
            _context = context;
        }

        public Order Order { get; set; }
        public decimal ToatalAmount;
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Order = await _context.Order                
                .Include(o => o.Customer)
                .Include(o => o.OrderLineItems)
                .ThenInclude(ordItem => ordItem.Product)
                .FirstOrDefaultAsync(m => m.ID == id);

            ToatalAmount = getTotalAmount(Order);


            if (Order == null)
            {
                return NotFound();
            }
            return Page();
        }

        public decimal getTotalAmount(Order ord)
        {
            decimal amount = 0;
            if (ord != null)
            {
                foreach (var ordLineItem in ord.OrderLineItems)
                {
                    amount += ordLineItem.Product.Price * ordLineItem.Quantity;                    
                }
                return amount;
            }
            else
            {
                return 0;
            }
        }
    }
}
