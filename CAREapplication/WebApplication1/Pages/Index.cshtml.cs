using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApplication1.Pages;

public class IndexModel : PageModel
{
    [BindProperty]
    public string Username { get; set; }

    [BindProperty]
    public string Password { get; set; }

    public IActionResult OnGet(string logout)
    {
        if (logout == "true")
        {
            HttpContext.Session.Clear();
            ViewData["LoginMessage"] = "Successfully Logged Out!";
        }

        string loginError = HttpContext.Session.GetString("LoginError");
        if (!string.IsNullOrEmpty(loginError))
        {
            ViewData["LoginMessage"] = loginError;
            HttpContext.Session.Remove("LoginError"); // Clear after displaying
        }

        return Page();
    }
}
