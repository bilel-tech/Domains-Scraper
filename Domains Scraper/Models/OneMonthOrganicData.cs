using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains_Scraper.Models
{
    public class OneMonthOrganicData
    {
        public List<OrganicChartData> OneMonthOrganicKeyWordsChartData { get; set; } = new List<OrganicChartData>();
        public List<OrganicTrafficChartData> OneMonthOrganicTrafficChartData { get; set; } = new List<OrganicTrafficChartData>();
    }
}
