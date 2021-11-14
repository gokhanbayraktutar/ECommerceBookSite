using BookSite.Data.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookSite.Data.Entities
{
    public class Book_Publisher : Entity
    {
        public int BookId { get; set; }
        public int PublisherId { get; set; }
        public virtual Book Book { get; set; }
        public virtual Publisher Publisher { get; set; }

    }
}
