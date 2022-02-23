using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains_Scraper.Models
{
    public class OrganicTrafficChartData
    {
        public int Id { get; set; }
        public int? AllTimeOrganicDataId { get; set; }
        public int? OneYearOrganicDataId { get; set; }
        public AllTimeOrganicData AllTimeOrganicData { get; set; }
        public OneYearOrganicData OneYearOrganicData { get; set; }
        public DateTime Date { get; set; }
        public long OrganicTrafficValue { get; set; }
        public long PaidTrafficValue { get; set; }
    }
}
