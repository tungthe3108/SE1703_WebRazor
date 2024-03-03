using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using SE1703_WebRazor.Models;

namespace SE1703_WebRazor.Pages.Members
{
    public class UpdateModel : PageModel
    {
        private readonly SE1703_WebRazor.Models.eStoreContext _context;
        //private readonly IHubContext<ISignalRServerBuilder> _signalRHub;

        public UpdateModel(SE1703_WebRazor.Models.eStoreContext context)
        {
            _context = context;
            //_signalRHub = signalRHub;
        }

        [BindProperty]
        public Member Member { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Members == null)
            {
                return NotFound();
            }

            var member = await _context.Members.FirstOrDefaultAsync(m => m.MemberId == id);

            if (member == null)
            {
                return NotFound();
            }
            else
            {
                Member = member;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Members == null)
            {
                return NotFound();
            }
            var member = await _context.Members.FindAsync(id);

            if (member != null)
            {
                try
                {
                    Member = Member;
                    _context.Members.Update(Member);
                    await _context.SaveChangesAsync();
                }
                catch
                {
                    return Page();
                }
            }
            return RedirectToPage("./Index");

        }
    }
}
