using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains_Scraper.Models
{
    public class AhrefChartPointNewAndLostBacklinksDomain
    {
        public DateTime Date1 { get; set; }
        public DateTime Date2 { get; set; }
        public long NewDoFolloow { get; set; }
        public long LostDoFolloow { get; set; }
        public long NewNoFolloow { get; set; }
        public long LostNoFolloow { get; set; }
        public long NewRedirect { get; set; }
        public long LostRedirect { get; set; }
        public long NewOther { get; set; }
        public long LostOther { get; set; }
        public long TotalNew { get; set; }
        public long TotalLost { get; set; }
    }
}
