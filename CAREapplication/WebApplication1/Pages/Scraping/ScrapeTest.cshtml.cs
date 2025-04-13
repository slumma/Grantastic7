using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CAREapplication.Pages.Scraping;
using CAREapplication.Pages.DB;
using System.Collections.Generic;

namespace CAREapplication.Pages.Scraping
{
    public class ScrapeTestModel : PageModel
    {
        public List<NSFGrant> ScrapedGrants { get; set; } = new List<NSFGrant>();

        public void OnGet()
        {
            ScrapedGrants = NFSGrantScraper.ScrapeGrants();

            foreach (var grant in ScrapedGrants)
            {
                if (!DBGrant.NSFGrantExists(grant.Link))
                {
                    DBGrant.InsertNSFGrant(grant);
                }
            }
        }
    }
}
