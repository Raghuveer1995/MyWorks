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
    public class IndexModel : PageModel
    {
        private readonly CoreResume.Models.CoreResumeContext _context;

        public IndexModel(CoreResume.Models.CoreResumeContext context)
        {
            _context = context;
        }

        public IList<Case> Case { get;set; }

        public async Task OnGetAsync()
        {
            Case = await _context.Case
                .Include(@ => @.Customer).ToListAsync();
        }
    }
}
