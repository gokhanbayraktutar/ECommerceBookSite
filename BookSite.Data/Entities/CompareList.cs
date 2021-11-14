using BookSite.Data.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSite.Data.Entities
{
    public class CompareList : Entity
    {
        public int BookId { get; set; }
        public int UserId { get; set; }
        public virtual Book Book { get; set; }
        public virtual User User { get; set; }
    }
}
