using BookSite.Data.Entities.Base;

namespace BookSite.Data.Entities
{
    public class ContactPage : Entity
    {
        public string Phone { get; set; }
        public string Faks { get; set; }
        public string Gsm { get; set; }
        public string Email1 { get; set; }
        public string Email2 { get; set; }
        public string Address { get; set; }
    }
}
