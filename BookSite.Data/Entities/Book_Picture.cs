using BookSite.Data.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookSite.Data.Entities
{
    public class Book_Picture : Entity
    {
        public int BookId { get; set; }
        public string Picture { get; set; }
        public virtual Book Book { get; set; }

    }
}
