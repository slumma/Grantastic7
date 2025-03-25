using lab484.Pages.DB;
using lab484.Pages.Data_Classes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;

namespace lab484.Pages
{
    public class addUserModel : PageModel
    {
        [BindProperty]
        public User NewUser { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPostAdd()
        {
            if (ModelState.IsValid)
            {
                DBClass.InsertUser(NewUser);

                return RedirectToPage("Index");
            }

            return Page();

        }

        public IActionResult OnPostClear()
        {
            ModelState.Clear();
            NewUser = new User
            {
                UserID = NewUser.UserID, // Keep the UserID the same
                UserName = string.Empty,
                Password = string.Empty,
                FirstName = string.Empty,
                LastName = string.Empty,
                Email = string.Empty,
                Phone = string.Empty,
                HomeAddress = string.Empty
            };

            // Return the page with the cleared model
            return Page();
        }

        public IActionResult OnPostPopulate()
        {
            ModelState.Clear();
            NewUser = new User
            {
                UserID = NewUser.UserID, // Keep the UserID the same
                UserName = "ILove484SoMuch",
                Password = "12345",
                FirstName = "sam",
                LastName = "ogden",
                Email = "thesamogden@gmail.com",
                Phone = "12345567",
                HomeAddress = "11 NUN YA BUSINESS"
            };

            return Page();
        }
    }


}
