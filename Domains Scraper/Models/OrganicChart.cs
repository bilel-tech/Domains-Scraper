using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains_Scraper.Models
{
    public class OrganicChart
    {
        public DateTime Date { get; set; }
        public long OrganicTrafficTotal { get; set; }
        public long OrganicKeyWordsTotal { get; set; }
        public long Position_1_3 { get; set; }
        public long Position_4_10 { get; set; }
        public long Position_11_100 { get; set; }
    }
}
