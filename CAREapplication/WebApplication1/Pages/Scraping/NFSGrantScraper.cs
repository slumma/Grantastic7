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
        public string GrantDescription { get; set; }
        public string AwardTypes { get; set; }
        public string DueDate { get; set; }
        public string PostedDate { get; set; }
        public string Link { get; set; }
    }


    public class NSFGrantCsvRow
    {
        public string Title { get; set; }
        public string Synopsis { get; set; }
        public string AwardType { get; set; }
        public string NextDueDateYmd { get; set; }
        public string PostedDateYmd { get; set; }
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
                grants.Add(new NSFGrant
                {
                    Title = row.Title?.Trim(),
                    GrantDescription = row.Synopsis?.Trim(),
                    AwardTypes = row.AwardType?.Trim(),
                    DueDate = row.NextDueDateYmd?.Trim(),
                    PostedDate = row.PostedDateYmd?.Trim(),
                    Link = row.URL?.Trim()
                });
            }

            return grants;
        }
    }
}
