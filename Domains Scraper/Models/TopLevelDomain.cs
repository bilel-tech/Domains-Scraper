using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains_Scraper.Models
{
    public class TopLevelDomain
    {
        public List<TldsDistribution> TldsDistributionByCountry { get; set; } = new List<TldsDistribution>();
        public List<TldsDistribution> TldsDistribution { get; set; } = new List<TldsDistribution>();
    }
}
