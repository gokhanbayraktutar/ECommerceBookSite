using BookSite.Data.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookSite.Data.Entities
{
    public class Book : Entity
    {
        public string ISBN { get; set; }

        public string Name { get; set; }

        public string PublishDate { get; set; }

        public int PageCount { get; set; }

        public string Size { get; set; }

        public double Price { get; set; }

        public double DiscountPrice { get; set; }

        public int DiscountPercentage { get; set; }

        public string Content { get; set; }

        public bool BestSellerStatus { get; set; }

        public bool NewReleasesStatus { get; set; }

        public bool DealsStatus { get; set; }
  
    }
}
