using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SE1703_WebRazor.Models;
using System;
using System.Runtime.CompilerServices;

public class LoginModel : PageModel
{
    private readonly IConfiguration _config;
    private readonly eStoreContext _storeContext;

    public LoginModel(IConfiguration config, eStoreContext storeContext)
    {
        _config = config;
        _storeContext = storeContext;
    }
    public IList<Member> Members { get; set; } = default!;

    [BindProperty]
    public string Username { get; set; }

    [BindProperty]
    public string Password { get; set; }

    public IActionResult OnPost(string email, string password )
    {
        var appSettings = _config.GetSection("LoginCredentials");
        var storedUsername = appSettings["Username"];
        var storedPassword = appSettings["Password"];

        if (Username == storedUsername && Password == storedPassword)
        {
            // Redirect to success page or perform other actions
            return Redirect("/Menu/Menu");
        }

        var account = _storeContext.Members.FirstOrDefault(x => x.Email == Username
            && x.Password == Password);
        if (account == null)
        {
            TempData["ErrorMessage"] = "Tài khoản hoặc mật khẩu sai!";
            return Page();
        }
        else
        {
            return Redirect("/Menu/MemberMenu");
        }
    }
}
