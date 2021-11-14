using BookSite.Data.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSite.Data.Entities
{
    public class District : Entity
    {
        public string Name { get; set; }
        public int CityId { get; set; }
        public virtual City City { get; set; }
    }
}
