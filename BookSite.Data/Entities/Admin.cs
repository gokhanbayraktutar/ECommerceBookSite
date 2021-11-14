using BookSite.Data.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookSite.Data.Entities
{
   public class Admin: Entity
    {
        public string UserName { get; set; }
        public string Password { get; set; }        
        public string Email { get; set; }
    }
}
