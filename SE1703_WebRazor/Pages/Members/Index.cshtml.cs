using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using SE1703_WebRazor.Models;
using System.Text.Json;

namespace SE1703_WebRazor.Pages.Members
{
    public class IndexModel : PageModel
    {
        private readonly SE1703_WebRazor.Models.eStoreContext _context;
        /*private readonly IHubContext<ISignalRServerBuilder> _signalRHub;*/

        public IndexModel(SE1703_WebRazor.Models.eStoreContext context)
        {
            _context = context;
            //_signalRHub = signalRHub;
        }

        public IList<Member> Members { get; set; } = default!;

        [BindProperty]
        public string Email { get; set; } = null!;
        public async Task OnGetAsync()
        {
            if (_context.Members != null)
            {
                Members = await _context.Members
                    .Where(c => c.Email.Contains(Email ?? "")).ToListAsync();
            }
        }

        public async Task OnPostAsync()
        {
            if (_context.Categories != null)
            {
                Members = await _context.Members
                    .Where(c => c.Email.Contains(Email ?? "")).ToListAsync();
            }
        }

        public ContentResult OnGetGetCategories()
        {
            List<Category> categories = _context.Categories.ToList();
            string strData = JsonSerializer.Serialize(categories);
            return Content(strData);
        }
    }
}
