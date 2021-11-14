using BookSite.Data.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookSite.Data.Entities
{
    public class Customer : Entity
    {
        
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public string Adress { get; set; }

        public string MobilePhone { get; set; }

        public string Phone { get; set; }

        public string PostCode { get; set; }
    }
}
