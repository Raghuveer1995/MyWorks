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
    public class IndexModel : PageModel
    {
        private readonly DMRServices.Models.DMRServicesContext _context;

        public IndexModel(DMRServices.Models.DMRServicesContext context)
        {
            _context = context;
        }

        public IList<Order> Order { get;set; }

        public async Task OnGetAsync()
        {
            Order = await _context.Order
                .Include(o => o.Customer).ToListAsync();
        }
    }
}
