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
    public class CaseCollectionModel : PageModel
    {
        private DMRServicesContext _context;
        public ICollection<Case> Cases;
        public CaseCollectionModel(DMRServicesContext context)
        {
            _context = context;
        }
        public void OnGet()
        {
            Cases = _context.Case
                            .Include(cs => cs.Customer)
                            .OrderBy(x => x.CreatedDate)
                            .ToList();
        }
    }
}