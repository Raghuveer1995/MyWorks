using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DMRServices.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DMRServices.Pages
{
    public class CaseClosureModel : PageModel
    {

        private DMRServicesContext _context;
        public CaseClosureModel(DMRServicesContext context)
        {
            _context = context;
        }

        [BindProperty]
        public CaseClosureForm CaseClosureForm { get; set; }
        public Case Case;       
        public IActionResult OnGet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Case = _context.Case.Find(id);
            if (Case == null)
            {
                return NotFound();
            }
            CaseClosureForm = new CaseClosureForm();
            CaseClosureForm.CaseId = id.Value;
            
            return Page();
        }

        public IActionResult OnPost()
        {
            Case = _context.Case.Find(CaseClosureForm.CaseId);
            if (!ModelState.IsValid)
            {
                return Page();
            }
            Case.Status = "Closed";
            Case.ClosedDate = CaseClosureForm.ClosureDate;
            _context.SaveChanges();
            return RedirectToPage("/Cases/Index");
        }
    }
}