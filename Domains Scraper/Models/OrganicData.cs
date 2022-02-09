using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains_Scraper.Models
{
    public class OrganicData
    {
        public int Id { get; set; }
        public long OrganicTraffic { get; set; }
        public long OrganicKeywords { get; set; }
        public OneYearOrganicData OneYearOrganicData { get; set; } = new OneYearOrganicData();
        public AllTimeOrganicData AllTimeOrganicData { get; set; } = new AllTimeOrganicData();
        public List<OrganicChartData> OrganicPositionsDistrubution { get; set; } = new List<OrganicChartData>();
        public List<OrganicTrafficAndKeywordsByCountry> OrganicTrafficAndKeywordsByCountry { get; set; } = new List<OrganicTrafficAndKeywordsByCountry>();
    }
}
