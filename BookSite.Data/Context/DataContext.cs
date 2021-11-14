using BookSite.Data.Entities;
using System.Data.Entity;
using System.IO;

namespace BookSite.Data.Context
{
    public class DataContext : DbContext

    {
        public DataContext() : base("DataContext")
        {
        }

        public DbSet<Book> Books { get; set; }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Admin> Admins { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Publisher> Publishers { get; set; }

        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartContent> CartContents { get; set; }
        public DbSet<Cargo> Cargos { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Book_Stock> Book_Stocks { get; set; }

        public DbSet<Book_Author> Book_Authors { get; set; }

        public DbSet<Book_Category> Book_Categories { get; set; }

        public DbSet<Book_Comment> Book_Comments { get; set; }

        public DbSet<Book_Picture> Book_Pictures { get; set; }

        public DbSet<Book_Publisher> Book_Publishers { get; set; }

        public DbSet<PaymentType> PaymentTypes { get; set; }

        public DbSet<Slider> Sliders { get; set; }

        public DbSet<ContactPage> ContactPages { get; set; }

        public DbSet<SiteConstant> SiteConstants { get; set; }

        public DbSet<Inbox> Inboxes { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<CompareList> CompareLists { get; set; }



    }
}
