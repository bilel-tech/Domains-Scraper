using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains_Scraper.Models
{
    public class URLRatingDistribution
    {
        //public long From91_100 { get; set; }
        //public long From81_90 { get; set; }
        //public long From71_80 { get; set; }
        //public long From61_70 { get; set; }
        //public long From51_60 { get; set; }
        //public long From41_50 { get; set; }
        //public long From31_40 { get; set; }
        //public long From21_30 { get; set; }
        //public long From11_20 { get; set; }
        //public long From0_10 { get; set; }
        public string Rank { get; set; }
        public long Value { get; set; }
        public double Percent { get; set; }
    }
}
