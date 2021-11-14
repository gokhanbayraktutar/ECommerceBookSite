using BookSite.Data.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookSite.Data.Entities
{
    public class Cargo : Entity
    {
        public string CompanyName { get; set; }
        public string Picture { get; set; }
        public double Price { get; set; }
       
    }
}
