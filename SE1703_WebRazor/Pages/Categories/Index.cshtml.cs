using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SE1703_WebRazor.Models;

namespace SE1703_WebRazor.Pages.Categories
{
    public class IndexModel : PageModel
    {
        private readonly SE1703_WebRazor.Models.eStoreContext _context;

        public IndexModel(SE1703_WebRazor.Models.eStoreContext context)
        {
            _context = context;
        }

        public IList<Category> Category { get;set; } = default!;
        [BindProperty]
        public string CategoryName {  get; set; }
        public async Task OnGetAsync(string CategoryName)
        {
            /*ViewData["CategoryName"] = CategoryName;*/
            if (_context.Categories != null)
            {
                Category = await _context.Categories
                    .Where(c => c.CategoryName.Contains(CategoryName??"")).ToListAsync();
            }
        }

        public async Task OnPostAsync(string CategoryName)
        {
            /*ViewData["CategoryName"] = CategoryName;*/
            if (_context.Categories != null)
            {
                Category = await _context.Categories
                    .Where(c => c.CategoryName.Contains(CategoryName ?? "")).ToListAsync();
            }
        }
    }
}
