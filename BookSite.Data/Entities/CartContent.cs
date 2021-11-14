using BookSite.Data.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookSite.Data.Entities
{
    public class CartContent : Entity
    {
        public int CartId { get; set; }
        public int BookId { get; set; }
        public int Count { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice { get; set; }
        public virtual Cart Cart { get; set; }
        public virtual Book Book { get; set; }
    }
}
