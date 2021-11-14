using BookSite.Data.Entities;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookSite.Web.Models
{
    public class HomeVM
    {
        public List <Slider> Sliders { get; set; } = new List<Slider>();
        public IPagedList<Book> Books { get; set; }

        public int BookCount;

        public int PageCount;
        public List<Category> Categories { get; set; } = new List<Category>();

        public Book Book { get; set; }
        public Category Category { get; set; }

        public List<Book> BestSellerBooks { get; set; } = new List<Book>();
        public List<Book> NewReleasesBooks { get; set; } = new List<Book>();
        public List<Book> DealsBooks { get; set; } = new List<Book>();
        public List<Book> ChildrenBooks { get; set; } = new List<Book>();
        public List<Book> FavouriteBooks { get; set; } = new List<Book>();
        public List<Book> CatBooks { get; set; } = new List<Book>();
        public List<Author> Authors { get; set; } = new List<Author>();
        public List<Publisher> Publishers { get; set; } = new List<Publisher>();

        public List<CartContent> CartContents { get; set; }
        public List<City> Cities { get; set; }
        public List<District> Districts { get; set; }
        public Cart Cart { get; set; }
        public List<Cart> Carts { get; set; }
        public User User { get; set; }
        public Cargo Cargo { get; set; }
        public Author Author { get; set; }

        public List<PaymentType> PaymentTypes { get; set; }
        public List<Favorite> Favorites { get; set; }
        public List<Cargo> Cargos { get; set; }
        public IPagedList<Author> PagedListAuthors { get; set; }

        public List<Book> CompareBooks { get; set; } = new List<Book>();


    }
}