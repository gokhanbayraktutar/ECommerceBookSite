using BookSite.Data.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookSite.Data.Entities
{
    public class Book_Comment : Entity
    {
        public int BookId { get; set; }
        public int UserId  { get; set; }
        public virtual User User   { get; set; }
        public virtual Book Book   { get; set; }
        public string IP  { get; set; }
        public DateTime Date  { get; set; }
        public string CommentHeader  { get; set; }
        public string Comment  { get; set; }
        public bool Confirm  { get; set; }
        public bool ReadStatus  { get; set; }

        public int StarCount { get; set; }
    }
}
