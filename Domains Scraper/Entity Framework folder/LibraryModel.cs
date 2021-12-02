using Domains_Scraper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains_Scraper.Entity_Framework_folder
{
    public class LibraryModel
    {
        public class Book
        {
            public string ISBN { get; set; }
            public string Title { get; set; }
            public string Author { get; set; }
            public string Language { get; set; }
            public int Pages { get; set; }
            public virtual Publisher Publisher { get; set; }
        }

        public class Publisher
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public virtual List<Book> Books { get; set; }
        }
    }
}
