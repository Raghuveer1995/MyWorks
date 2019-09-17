using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthDeptApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace HealthDeptApp.Pages
{
    public class EstablishmentProfileModel : PageModel
    {
        private AppDbContext _context;
        public Establishment Establishment;
        public EstablishmentProfileModel(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult OnGet(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            Establishment = _context.Establishments
                                    .Include(est => est.Category)
                                    .Include(est => est.Inspections)
                                    .ThenInclude(ins => ins.Agent)
                                    .FirstOrDefault(x => x.Id == id);
            return Page();
        }
       

    }
}