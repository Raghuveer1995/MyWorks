using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreCrud.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CoreCrud.Pages
{
    public class IndexModel : PageModel
    {
        private CoreCrudContext _context;
        public int NumberOfIndiaDestinations;
        public int NumberOfUSADestinations;
        public int NumberOfLongTermDestinations;
        public int NumberOfExpensiveDestinations;
        public int NumberOfCheapDestinations;
        public int NumberOfFiveStarRatingDestinations;
        public IndexModel(CoreCrudContext context)
        {
            _context = context;
        }
        public void OnGet()
        {
            NumberOfIndiaDestinations = _context.Destination
                                                        .Where(x => x.Location.Name == "India")
                                                        .Count();
            NumberOfUSADestinations = _context.Destination
                                                        .Where(x => x.Location.Name == "USA")
                                                        .Count();
            NumberOfLongTermDestinations = _context.Destination
                                                        .Where(x => x.LongVacationDestination == true)
                                                        .Count();
            NumberOfExpensiveDestinations = _context.Destination
                                                        .Where(x => x.PricePerPerson > 500)
                                                        .Count();
            NumberOfCheapDestinations = _context.Destination
                                                        .Where(x => x.PricePerPerson <= 150)
                                                        .Count();
            NumberOfFiveStarRatingDestinations = _context.Destination
                                                        .Where(x => x.Rating == 5)
                                                        .Count();
        }
    }
}
