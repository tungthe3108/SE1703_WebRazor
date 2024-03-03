using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SE1703_WebRazor.Pages
{
    public class MenuModel : PageModel
    {
        public IActionResult OnGet()
        {
            return Page();
        }
    }
}