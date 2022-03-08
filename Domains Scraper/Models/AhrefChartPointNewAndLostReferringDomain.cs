using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains_Scraper.Models
{
    public class AhrefChartPointNewAndLostReferringDomain
    {
        public DateTime Date1 { get; set; }
        public DateTime Date2 { get; set; }
        public long New { get; set; }
        public long Lost { get; set; }
    }
}
