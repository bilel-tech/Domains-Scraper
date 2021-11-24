using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains_Scraper.Models
{
    public class OrganicTrafficChartData
    {
        public DateTime Date { get; set; }
        public int OrganicTrafficValue { get; set; }
        public int PaidTrafficValue { get; set; }
    }
}
