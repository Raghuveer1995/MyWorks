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
    public class IndexModel : PageModel
    {
        private readonly DMRServices.Models.DMRServicesContext _context;

        public IndexModel(DMRServices.Models.DMRServicesContext context)
        {
            _context = context;
        }

        public IList<OrderLineItem> OrderLineItem { get;set; }

        public async Task OnGetAsync()
        {
            OrderLineItem = await _context.OrderLineItem
                .Include(o => o.Order)
                .Include(o => o.Product).ToListAsync();
        }
    }
}
