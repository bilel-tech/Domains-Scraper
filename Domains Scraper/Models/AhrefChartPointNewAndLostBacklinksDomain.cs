using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains_Scraper.Models
{
    public class AhrefChartPointNewAndLostBacklinksDomain
    {
        public int Id { get; set; }
        public int? AhrefNewAndLostChartsId { get; set; }
        public DateTime Date1 { get; set; }
        public DateTime? Date2 { get; set; }
        public long NewDoFollow { get; set; }
        public long LostDoFollow { get; set; }
        public long NewNoFollow { get; set; }
        public long LostNoFollow { get; set; }
        public long NewRedirect { get; set; }
        public long LostRedirect { get; set; }
        public long NewOther { get; set; }
        public long LostOther { get; set; }
        public long TotalNew { get; set; }
        public long TotalLost { get; set; }
    }
}
