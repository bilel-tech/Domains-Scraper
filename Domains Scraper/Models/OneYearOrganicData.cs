using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains_Scraper.Models
{
    public class OneYearOrganicData
    {
        public int Id { get; set; }
        public List<OrganicTrafficChartData> OneYearOrganicTrafficChartData { get; set; } = new List<OrganicTrafficChartData>();
        public List<OrganicChartData> OneYearOrganicKeyWordsChartData { get; set; } = new List<OrganicChartData>();
    }
}
