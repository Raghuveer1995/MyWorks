using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DMRServices.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace DMRServices.Pages
{
    public class CustomerProfileModel : PageModel
    {
        private DMRServicesContext _context;
        public Customer customer;
        public CustomerProfileModel(DMRServicesContext context)
        {
            _context = context;
        }
        public IActionResult OnGet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            customer = _context.Customer
                                    .Include(cust => cust.Cases)
                                    .Include(cust => cust.Orders)
                                    .ThenInclude(ord => ord.OrderLineItems)
                                    .ThenInclude(prd => prd.Product)
                                    .FirstOrDefault(cust => cust.ID == id);
            return Page();
        }
    }
}