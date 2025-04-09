using CAREapplication.Pages.DataClasses;
using CAREapplication.Pages.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Diagnostics;

namespace CAREapplication.Pages
{
    public class SearchModel : PageModel
    {
        [BindProperty]
        public string searchWord { get; set; }

        public required List<SearchResult> searchList { get; set; } = new List<SearchResult>();

        public IActionResult OnGet()
        {
            // control validating if the user is an admin trying to access the page 
            if (HttpContext.Session.GetInt32("loggedIn") != 1)
            {
                HttpContext.Session.SetString("LoginError", "You must login to access that page!");
                return RedirectToPage("../Index"); // Redirect to login page
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            SqlDataReader searchReader = DBClass.SearchFunction(searchWord);
            while (searchReader.Read())
            {
                searchList.Add(new SearchResult
                {
                    TableName = searchReader["TableName"].ToString(),
                    ColumnName = searchReader["ColumnName"].ToString(),
                    foundValue = searchReader["FoundValue"].ToString()
                });
            }
            DBClass.DBConnection.Close();
            Trace.WriteLine(searchList.Count);
            return Page();
        }
    }
}
