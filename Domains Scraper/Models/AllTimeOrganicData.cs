using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains_Scraper.Models
{
    public class AllTimeOrganicData
    {
        public int Id { get; set; }
        public OrganicData OrganicData { get; set; }
        public int? OrganicDataId { get; set; }
        public List<OrganicTrafficChartData> AllTimeOrganicTrafficChartData { get; set; } = new List<OrganicTrafficChartData>();
        public List<OrganicChartData> AllTimeOrganicKeyWordsChartData { get; set; } = new List<OrganicChartData>();
    }
}
