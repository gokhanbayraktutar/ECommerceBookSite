using BookSite.Data.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookSite.Data.Entities
{
    public class PaymentType : Entity
    {
        public string Header { get; set; }
        public string Content { get; set; }
    }
}
