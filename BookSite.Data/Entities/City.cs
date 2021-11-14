using BookSite.Data.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSite.Data.Entities
{
    public class City : Entity
    {
        public string Name { get; set; }
        public virtual ICollection<District> Districts { get; set; }
    }
}
