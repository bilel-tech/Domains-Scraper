﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains_Scraper.Models
{
    public class OrganicTrafficAndKeywordsByCountry
    {
        public int Id { get; set; }
        public OrganicData OrganicData { get; set; }
        public int? OrganicDataId { get; set; }
        public string Country { get; set; }
        public long OranicTraficValue { get; set; }
        public long KeyWordsValue { get; set; }
    }
}
