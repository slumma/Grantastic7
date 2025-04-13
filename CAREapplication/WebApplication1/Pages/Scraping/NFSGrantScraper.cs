using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using CsvHelper;
using CAREapplication.Pages.DB;
using CsvHelper.Configuration;

namespace CAREapplication.Pages.Scraping
{
    public class NSFGrant
    {
        public string Title { get; set; }
        public string Link { get; set; }
        public string GrantDescription { get; set; }
        public DateTime? PostedDate { get; set; }
        public string DueDate { get; set; }
        public string AwardTypes { get; set; }
    }

    public class NSFGrantCsvRow
    {
        public string title { get; set; }
        public string url { get; set; }
        public string synopsis { get; set; }
        public string posted_date { get; set; }
        public string due_dates { get; set; }
        public string award_type { get; set; }
    }

    public class NFSGrantScraper
    {
        public static List<NSFGrant> ScrapeGrants()
        {
            var grants = new List<NSFGrant>();
            string csvUrl = "https://www.nsf.gov/funding/opps/csvexport?page&_format=csv";

            using var httpClient = new HttpClient();
            using var stream = httpClient.GetStreamAsync(csvUrl).Result;
            using var reader = new StreamReader(stream);
            using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HeaderValidated = null,
                MissingFieldFound = null
            });

            var records = csv.GetRecords<NSFGrantCsvRow>();

            foreach (var row in records)
            {
                DateTime? postedDate = null;
                if (DateTime.TryParse(row.posted_date, out DateTime parsedDate))
                    postedDate = parsedDate;

                var grant = new NSFGrant
                {
                    Title = row.title?.Trim(),
                    Link = row.url?.Trim(),
                    GrantDescription = row.synopsis?.Trim(),
                    PostedDate = postedDate,
                    DueDate = row.due_dates?.Trim(),
                    AwardTypes = row.award_type?.Trim()
                };

                if (!DBGrant.NSFGrantExists(grant.Link))
                {
                    DBGrant.InsertNSFGrant(grant);
                }

                grants.Add(grant);
            }

            return grants;
        }
    }
}
