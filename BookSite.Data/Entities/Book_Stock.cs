using BookSite.Data.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookSite.Data.Entities
{
    public class Book_Stock : Entity
    {
        public int BookId { get; set; }
        public int StockCount { get; set; }
        public virtual Book Book { get; set; }
    }
}
