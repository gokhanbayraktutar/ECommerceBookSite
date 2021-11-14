using BookSite.Data.Context;
using BookSite.Data.Entities;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web.Mvc;

namespace BookSite.Web.Areas.Panel.Controllers
{
    public class Book_StockController : BaseController
    {
        DataContext db = new DataContext();

        public ActionResult Index()
        {
            var book_stock = db.Book_Stocks.ToList();

            return View(book_stock);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Create(int bookid, int stock )
        {
            var book_stock = db.Book_Stocks.FirstOrDefault(x => x.BookId == bookid);

            if(book_stock != null)
            {
                int stockCount = book_stock.StockCount;

                book_stock.StockCount = stockCount + stock;

                db.Book_Stocks.AddOrUpdate(book_stock);

                db.SaveChanges();

                return Json("", JsonRequestBehavior.AllowGet);
            }

            else
            {
                Book_Stock book_Stock = new Book_Stock();

                book_Stock.BookId = bookid;
                
                book_Stock.StockCount = stock;

                db.Book_Stocks.Add(book_Stock);

                db.SaveChanges();

                return Json("", JsonRequestBehavior.AllowGet);
            }
            
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var book_stock = db.Book_Stocks.FirstOrDefault(x => x.Id == id);

            return View(book_stock);

        }
        [HttpPost]
        public JsonResult Edit(int Id, int stock )
        {

            Book_Stock book_stock = db.Book_Stocks.FirstOrDefault(x => x.Id == Id);

            book_stock.StockCount = stock;

            book_stock.BookId = book_stock.BookId;

            db.Book_Stocks.AddOrUpdate(book_stock);

            db.SaveChanges();

            return Json("", JsonRequestBehavior.AllowGet);


        }

        public ActionResult Delete(int id)
        {
            var book_stock = db.Book_Stocks.FirstOrDefault(x => x.Id == id);

            if (book_stock != null)
            {
                db.Book_Stocks.Remove(book_stock);

                db.SaveChanges();

                return Json("", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }

        }


    }
}