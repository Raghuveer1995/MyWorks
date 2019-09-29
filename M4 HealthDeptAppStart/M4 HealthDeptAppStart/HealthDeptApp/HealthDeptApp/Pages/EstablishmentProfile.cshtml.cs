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
        public EstablishmentProfileModel(AppDbContext context)
        {
            _context = context;
        }

        public Establishment Establishment { get; set; }

        public IActionResult OnGet(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            Establishment = _context.Establishments
                                    .Include(est => est.Category)
                                    .Include(est => est.Inspections)
                                    .ThenInclude(ins => ins.Agent)
                                    .FirstOrDefault(est => est.Id == id);


            if (Establishment == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}