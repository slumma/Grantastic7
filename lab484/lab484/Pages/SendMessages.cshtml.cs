using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace lab484.Pages
{
    public class SendMessagesModel : PageModel
    {
        // not done yet 
        public int UserID { get; set; }
        public IActionResult OnGet(int userID)
        {
            if (HttpContext.Session.GetString("username") == null)
            {
                HttpContext.Session.SetString("LoginError", "You must login to access that page!");
                return RedirectToPage("Index"); // Redirect to login page
            }

            this.UserID = userID;

            return Page();
        }
    }
}
