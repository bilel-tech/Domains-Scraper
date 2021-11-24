using Domains_Scraper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains_Scraper.Services
{
    public static class Singleton
    {
        public static List<SemrushDomain> SemrushDomains = new List<SemrushDomain>();
    }
}
