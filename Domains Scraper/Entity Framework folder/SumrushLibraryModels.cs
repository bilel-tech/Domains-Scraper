using Domains_Scraper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains_Scraper.Entity_Framework_folder
{
    public class SumrushLibraryModels
    {
        public class DomainTable
        {
            public string Name { get; set; }
            public int AuthorityScore { get; set; }
            public int Backlinks { get; set; }
            public List<OrganicTrafficChartData> OrganicTrafficChartData { get; set; }
            public List<OrganicChartData> OrganicChartData { get; set; }
            public List<OrganicTrafficAndKeywordsByCountry> OrganicTrafficAndKeywordsByCountry { get; set; }
        }
        public class OrganicTrafficChartDataTable
        {

        }

        //public OrganicChartData OrganicChartData { get; set; } = new OrganicChartData();
        //public OrganicTrafficAndKeywordsByCountry OrganicTrafficAndKeywordsByCountry { get; set; } = new OrganicTrafficAndKeywordsByCountry();
    }
}
