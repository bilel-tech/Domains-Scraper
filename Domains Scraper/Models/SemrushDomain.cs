using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains_Scraper.Models
{
    public class SemrushDomain
    {
        public string Name { get; set; }
        public int AuthorityScore { get; set; }
        public int Backlinks { get; set; }
        public OrganicData? OrganicData { get; set; }
        public FollowLinksVsNoFollowLink? FollowLinksVsNotFollowLink { get; set; }
        public BacklinkType? BacklinkType { get; set; }
    }
}
