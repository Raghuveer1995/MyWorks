using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CoreResume.Models;

namespace CoreResume.Pages.Cases
{
    public class DetailsModel : PageModel
    {
        private readonly CoreResume.Models.CoreResumeContext _context;

        public DetailsModel(CoreResume.Models.CoreResumeContext context)
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
                .Include(@ => @.Customer).FirstOrDefaultAsync(m => m.ID == id);

            if (Case == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
