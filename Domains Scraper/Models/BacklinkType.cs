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
        public int TextLinks { get; set; }
        public int FrameLinks { get; set; }
        public int FormLinks { get; set; }
        public int ImageLinks { get; set; }
    }
}
