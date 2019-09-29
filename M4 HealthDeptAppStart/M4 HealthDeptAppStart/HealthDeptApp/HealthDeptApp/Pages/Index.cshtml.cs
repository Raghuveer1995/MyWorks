using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthDeptApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HealthDeptApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _context;

        public IndexModel(AppDbContext context)
        {
            _context = context;
        }


        public int CountOfActiveAgents { get; set; }
        public int CountOfAgentsOnLeave { get; set; }
        public int CountOfOpenEstablishments { get; set; }
        public int CountOfInspectionsCompletedLastWeek { get; set; }
        public int CountOfOpenInspections { get; set; }
        public int CountOfFoodServiceEmployees { get; set; }

        public void OnGet()
        {

            CountOfActiveAgents = _context.Agents
                                        .Where(x => x.OnLongTermLeave == false)
                                        .Where(x => x.SeparationDate == null)
                                        .Count();

            CountOfAgentsOnLeave = _context.Agents
                                    .Where(x => x.OnLongTermLeave == true)
                                    .Where(x => x.SeparationDate == null)
                                    .Count();

            CountOfOpenEstablishments = _context.Establishments
                                        .Where(x => x.ClosedDate == null)
                                        .Count();

            DateTime weekAgo = DateTime.Today.AddDays(-7);
            CountOfInspectionsCompletedLastWeek = _context.Inspections
                                                   .Where(x => x.DateCompleted >= weekAgo)
                                                   .Count();

            CountOfOpenInspections = _context.Inspections
                                      .Where(x => x.DateCompleted == null)
                                      .Count();

            CountOfFoodServiceEmployees = _context.Establishments
                                           // ONLY OPEN BUSINESSES
                                           .Where(x => x.ClosedDate == null)
                                           // SUM EMPLOYEES
                                           .Sum(x => x.EmployeeCount);
        }
    }
}
