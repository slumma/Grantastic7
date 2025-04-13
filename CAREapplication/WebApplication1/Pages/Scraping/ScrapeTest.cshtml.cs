using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using CAREapplication.Pages.Scraping;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace CAREapplication.Pages.Scraping
{
    public class ScrapeTestModel : PageModel
    {
        private readonly IWebHostEnvironment _env;

        public ScrapeTestModel(IWebHostEnvironment env)
        {
            _env = env;
        }

        public List<NSFGrant> ScrapedGrants { get; set; } = new List<NSFGrant>();

        public void OnGet()
        {
            string filePath = Path.Combine(_env.WebRootPath, "Resources", "Prospects", "nsf_funding.csv");

            if (System.IO.File.Exists(filePath))
            {
                ScrapedGrants = NFSGrantScraper.LoadGrantsFromCsv(filePath);
            }
        }
    }
}
