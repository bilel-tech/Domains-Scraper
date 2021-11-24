using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains_Scraper.Models
{
    public class AllTimeOrganicData
    {
        public List<OrganicTrafficChartData> AllTimeOrganicTrafficChartData { get; set; } = new List<OrganicTrafficChartData>();
        public List<OrganicChartData> AllTimeOrganicKeyWordsChartData { get; set; } = new List<OrganicChartData>();
    }
}
