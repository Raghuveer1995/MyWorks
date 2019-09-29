using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthDeptApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HealthDeptApp.Pages
{
    public class HealthCodeDisplayModel : PageModel
    {
        private AppDbContext _context;

        public HealthCodeDisplayModel(AppDbContext context)
        {
            _context = context;
        }

        public ICollection<HealthCode> HealthCodes { get; set; }


        public void OnGet()
        {
            HealthCodes = _context.HealthCodes.OrderBy(x => x.Position).ToList();
        }
    }
}