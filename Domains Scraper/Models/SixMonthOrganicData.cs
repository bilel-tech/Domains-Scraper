using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains_Scraper.Models
{
    public class SixMonthOrganicData
    {
        public List<OrganicTrafficChartData> SixMonthOrganicTrafficChartData { get; set; } = new List<OrganicTrafficChartData>();
        public List<OrganicChartData> SixMonthOrganicKeyWordsChartData { get; set; } = new List<OrganicChartData>();
    }
}
