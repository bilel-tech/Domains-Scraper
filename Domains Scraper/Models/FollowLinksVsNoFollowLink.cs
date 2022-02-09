using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains_Scraper.Models
{
    public class FollowLinksVsNoFollowLink
    {
        public int Id { get; set; }
        public long FollowLinks { get; set; }
        public long NotFollowLinks { get; set; }
    }
}
