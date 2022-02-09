using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains_Scraper.Models
{
    public class OrganicChartData
    {
        public int Id { get; set; }
        public int OneYearOrganicDataId { get; set; }
        public int AllTimeOrganicDataId { get; set; }
        public DateTime Date { get; set; }
        public long TopThree { get; set; }
        public long FourToTen { get; set; }
        public long ElevenToTwenty { get; set; }
        public long TwentyOneToFifty { get; set; }
        public long FiftyOneToOneHundred { get; set; }
        public long Total { get; set; }
    }
}
