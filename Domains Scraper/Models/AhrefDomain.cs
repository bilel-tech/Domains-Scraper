using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains_Scraper.Models
{
    public class AhrefDomain
    {
        public string Name { get; set; }
        public int Ur { get; set; }
        public int Dr { get; set; }
        public long TotalBacklinks { get; set; }
        public long ReferringPages { get; set; }
        public long ReferringIPs { get; set; }
        public long ReferringSubnets { get; set; }
        public List<BacklinksType> BacklinksType { get; set; } = new List<BacklinksType>();
        public long TotalReferringDomains { get; set; }
        public List<BacklinksType> ReferringDomainsTypes { get; set; } = new List<BacklinksType>();
        public List<AhrefChartPointDomain> ReferringDomainPoints { get; set; } = new List<AhrefChartPointDomain>();
        public List<AhrefChartPointDomain> ReferringPagesPoints { get; set; } = new List<AhrefChartPointDomain>();
        public List<AhrefChartPointDomain> DomainRatingPoints { get; set; } = new List<AhrefChartPointDomain>();
        public long OrganicTraffic { get; set; }
        public long OrganicKeyWords { get; set; }
        public List<OrganicChart> OrganicCharts { get; set; } = new List<OrganicChart>(); public long OrganicKeywords { get; set; }
    }
}
