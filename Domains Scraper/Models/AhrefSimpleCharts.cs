using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains_Scraper.Models
{
    public class AhrefSimpleCharts
    {
        public int Id { get; set; }
        public List<AhrefChartPointDomain> ReferringDomaiChart { get; set; } = new List<AhrefChartPointDomain>();
        public List<AhrefChartPointDomain> ReferringPagesChart { get; set; } = new List<AhrefChartPointDomain>();
        public List<AhrefChartPointDomain> DomainRatingChart { get; set; } = new List<AhrefChartPointDomain>();
    }
}
