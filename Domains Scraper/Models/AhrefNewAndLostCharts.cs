using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains_Scraper.Models
{
    public class AhrefNewAndLostCharts
    {
        public int Id { get; set; }
        public List<AhrefChartPointNewAndLostReferringDomain> NewAndLostReferringDomainAllTimeChart { get; set; } = new List<AhrefChartPointNewAndLostReferringDomain>();
        //public List<AhrefChartPointNewAndLostReferringDomain> NewAndLostReferringDomainChartOneYearChart { get; set; } = new List<AhrefChartPointNewAndLostReferringDomain>();
        public List<AhrefChartPointNewAndLostReferringDomain> NewAndLostReferringDomain30DaysChart { get; set; } = new List<AhrefChartPointNewAndLostReferringDomain>();
        public List<AhrefChartPointNewAndLostBacklinksDomain> NewAndBacklinksDomainChartAllTime { get; set; } = new List<AhrefChartPointNewAndLostBacklinksDomain>();
        //public List<AhrefChartPointNewAndLostBacklinksDomain> NewAndBacklinksDomainChartOneYear { get; set; } = new List<AhrefChartPointNewAndLostBacklinksDomain>();
        public List<AhrefChartPointNewAndLostBacklinksDomain> NewAndBacklinksDomain30DaysChart { get; set; } = new List<AhrefChartPointNewAndLostBacklinksDomain>();
    }
}
