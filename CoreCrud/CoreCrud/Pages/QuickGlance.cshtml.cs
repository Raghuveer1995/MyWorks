using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreCrud.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CoreCrud.Pages
{
    public class QuickGlanceModel : PageModel
    {
        private CoreCrudContext _context;
        public ICollection<Destination> Destinations;
        public QuickGlanceModel(CoreCrudContext context)
        {
            _context = context;
        }
        public void OnGet()
        {
            Destinations = _context.Destination
                                    .Include(des => des.Location)
                                    .OrderBy(x => x.Name)
                                    .ToList();
        }
    }
    
}