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
using CsvHelper.Configuration.Attributes;

namespace CAREapplication.Pages.Scraping
{
    public class NSFGrant
    {
        public string Title { get; set; }
        public string GrantDescription { get; set; }
        public string AwardTypes { get; set; }
        public string DueDate { get; set; }
        public string PostedDate { get; set; }
        public string Link { get; set; }
    }


    public class NSFGrantCsvRow
    {
        [Name("Title")]
        public string Title { get; set; }

        [Name("Synopsis")]
        public string Synopsis { get; set; }

        [Name("Award Type")]
        public string AwardType { get; set; }

        [Name("Next due date (Y-m-d)")]
        public string NextDueDateYmd { get; set; }

        [Name("Posted date (Y-m-d)")]
        public string PostedDateYmd { get; set; }

        [Name("URL")]
        public string URL { get; set; }
    }


    public class NFSGrantScraper
    {
        public static List<NSFGrant> LoadGrantsFromCsv(string filePath)
        {
            var grants = new List<NSFGrant>();

            using var reader = new StreamReader(filePath);
            using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HeaderValidated = null,
                MissingFieldFound = null
            });

            var records = csv.GetRecords<NSFGrantCsvRow>();
            foreach (var row in records)
            {


                string formattedPostedDate = row.PostedDateYmd;

                if (DateTime.TryParse(row.PostedDateYmd, out DateTime parsedPosted))
                {
                    formattedPostedDate = parsedPosted.ToString("MMMM dd, yyyy");
                }


                grants.Add(new NSFGrant
                {
                    Title = row.Title?.Trim(),
                    GrantDescription = row.Synopsis?.Trim(),
                    AwardTypes = row.AwardType?.Trim(),
                    DueDate = row.NextDueDateYmd?.Trim(),
                    PostedDate = formattedPostedDate,
                    Link = row.URL?.Trim()
                });
            }

            return grants;
        }
    }
}
