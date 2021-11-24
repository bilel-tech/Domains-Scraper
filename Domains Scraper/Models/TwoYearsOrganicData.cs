using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains_Scraper.Models
{
    public class TwoYearsOrganicData
    {
        public List<OrganicTrafficChartData> TowYearOrganicTrafficChartData { get; set; } = new List<OrganicTrafficChartData>();
        public List<OrganicChartData> TwoYearsOrganicKeyWordsChartData { get; set; } = new List<OrganicChartData>();
    }
}
