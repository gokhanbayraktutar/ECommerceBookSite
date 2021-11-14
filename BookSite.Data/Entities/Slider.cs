using BookSite.Data.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookSite.Data.Entities
{
    public class Slider : Entity
    {
        public string Picture { get; set; }

        public string Header { get; set; }

        public string Content { get; set; }

        public bool Active { get; set; }

        public int Sorting { get; set; }
    }
}
