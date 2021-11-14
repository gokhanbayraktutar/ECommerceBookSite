using BookSite.Data.Entities.Base;
using System.Web;

namespace BookSite.Data.Entities
{
    public class SiteConstant : Entity
    {
        public string Header { get; set; }

        public string Logo { get; set; }

        public string Content { get; set; }

        public string WorkTime { get; set; }

    }
}
