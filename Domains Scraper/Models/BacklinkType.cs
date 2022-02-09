using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains_Scraper.Models
{
    public class BacklinkType
    {
        public int Id { get; set; }
        public long TextLinks { get; set; }
        public long FrameLinks { get; set; }
        public long FormLinks { get; set; }
        public long ImageLinks { get; set; }
    }
}
