﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains_Scraper.Models
{
    public class AhrefChartPointDomain
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public long? Value { get; set; }
    }
}
