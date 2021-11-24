using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains_Scraper.Models
{
    public class OrganicChartData
    {
        public DateTime Date { get; set; }
        public int TopThree { get; set; }
        public int FourToTen { get; set; }
        public int ElevenToTwenty { get; set; }
        public int TwentyOneToFifty { get; set; }
        public int FiftyOneToOneHundred { get; set; }
        public int Total { get; set; }
    }
}
