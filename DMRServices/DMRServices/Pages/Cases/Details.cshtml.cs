using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DMRServices.Models;

namespace DMRServices.Pages.Cases
{
    public class DetailsModel : PageModel
    {
        private readonly DMRServices.Models.DMRServicesContext _context;

        public DetailsModel(DMRServices.Models.DMRServicesContext context)
        {
            _context = context;
        }

        public Case Case { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Case = await _context.Case
                .Include(cs => cs.Customer).FirstOrDefaultAsync(m => m.ID == id);

            if (Case == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
