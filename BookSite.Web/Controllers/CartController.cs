using BookSite.Data.Context;
using BookSite.Data.Entities;
using BookSite.Web.Models;
using Stripe;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace BookSite.Web.Controllers
{
    [RoutePrefix("")]
    public class CartController : Controller
    {
        DataContext db = new DataContext();

        HomeVM vm = new HomeVM();

        [Route("SepeteEkle")]
        public ActionResult AddtoCart(int bookid)
        {
            var book = db.Books.FirstOrDefault(x => x.Id == bookid);
            var cart = new BookSite.Data.Entities.Cart();
            var cartContents = new List<BookSite.Data.Entities.CartContent>();
            var user = db.Users.FirstOrDefault(x => x.Email == User.Identity.Name);
            if (user != null)
            {
                cart = db.Carts.FirstOrDefault(x => x.UserID == user.Id && x.OrderStatus == "In Cart");
                var cartItem = db.CartContents.FirstOrDefault(x => x.CartId == cart.Id && x.BookId == bookid);
                if (cartItem != null)
                {
                    cartItem.Count += 1;
                    cartItem.TotalPrice = cartItem.Count * cartItem.Price;
                    db.Entry(cartItem).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
                else
                {
                    cartItem = new CartContent();
                    cartItem.BookId = bookid;
                    cartItem.CartId = cart.Id;
                    cartItem.Price = Convert.ToDecimal(book.DiscountPrice);
                    cartItem.Count = 1;
                    cartItem.TotalPrice = cartItem.Price * cartItem.Count;
                    db.CartContents.Add(cartItem);
                    db.SaveChanges();
                }
                cart.TotalPrice = 0;
                cartContents = db.CartContents.Where(x => x.CartId == cart.Id).ToList();
                foreach (var item in cartContents)
                {
                    cart.TotalPrice += (double)item.TotalPrice;

                }
                db.Entry(cart).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            else
            {
                cart = db.Carts.FirstOrDefault(x => x.SessionId == Session.SessionID && x.OrderStatus == "In Cart");
                var cartItem = db.CartContents.FirstOrDefault(x => x.CartId == cart.Id && x.BookId == bookid);
                if (cartItem != null)
                {
                    cartItem.Count += 1;
                    cartItem.TotalPrice = cartItem.Count * cartItem.Price;
                    db.Entry(cartItem).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
                else
                {
                    cartItem = new CartContent();
                    cartItem.BookId = bookid;
                    cartItem.CartId = cart.Id;
                    cartItem.Price = Convert.ToDecimal(book.DiscountPrice);
                    cartItem.Count = 1;
                    cartItem.TotalPrice = cartItem.Price * cartItem.Count;
                    db.CartContents.Add(cartItem);
                    db.SaveChanges();
                }
                cart.TotalPrice = 0;
                cartContents = db.CartContents.Where(x => x.CartId == cart.Id).ToList();
                foreach (var item in cartContents)
                {
                    cart.TotalPrice += (double)item.TotalPrice;

                }
                db.Entry(cart).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }


            return Json("", JsonRequestBehavior.AllowGet);
        }

        [Route("Sepeticeriksil")]

        public ActionResult DeletetoCart(int CartContentId)
        {
            var user = db.Users.FirstOrDefault(x => x.Email == User.Identity.Name);

            var cart = new BookSite.Data.Entities.Cart();

            if (user != null)
            {

                cart = db.Carts.FirstOrDefault(x => x.UserID == user.Id && x.OrderStatus == "In Cart");

                var cartItem = db.CartContents.FirstOrDefault(x => x.Id == CartContentId);

                db.CartContents.Remove(cartItem);

                db.SaveChanges();

                cart.TotalPrice = 0;

                var cartContents = db.CartContents.Where(x => x.CartId == cart.Id).ToList();

                foreach (var item in cartContents)
                {
                    cart.TotalPrice += (double)item.TotalPrice;

                }

                db.Carts.AddOrUpdate(cart);

                db.SaveChanges();


            }

            else
            {
                cart = db.Carts.FirstOrDefault(x => x.SessionId == Session.SessionID && x.OrderStatus == "In Cart");

                var cartItem = db.CartContents.FirstOrDefault(x => x.Id == CartContentId);

                db.CartContents.Remove(cartItem);

                db.SaveChanges();

                cart.TotalPrice = 0;

                var cartContents = db.CartContents.Where(x => x.CartId == cart.Id).ToList();

                foreach (var item in cartContents)
                {
                    cart.TotalPrice += (double)item.TotalPrice;

                }

                db.Carts.AddOrUpdate(cart);

                db.SaveChanges();
            }

            return Json("", JsonRequestBehavior.AllowGet);
        }

        [Route("sepetim")]
        public ActionResult Index()
        {
            var user = db.Users.FirstOrDefault(x => x.Email == HttpContext.User.Identity.Name);

            if (user != null)
            {
                vm.Cart = db.Carts.FirstOrDefault(x => x.UserID == user.Id && x.OrderStatus == "In Cart");

                vm.CartContents = db.CartContents.Where(x => x.CartId == vm.Cart.Id).ToList();
            }
            else
            {
                vm.Cart = db.Carts.FirstOrDefault(x => x.SessionId == Session.SessionID && x.OrderStatus == "In Cart");

                vm.CartContents = db.CartContents.Where(x => x.CartId == vm.Cart.Id).ToList();
            }
            return View(vm);
        }

        [Route("sepetAdetDegistir")]
        public ActionResult CartItemCountChange(int CartContentId, int count)
        {
            var user = db.Users.FirstOrDefault(x => x.Email == HttpContext.User.Identity.Name);

            if (user != null)
            {
                var cart = db.Carts.FirstOrDefault(x => x.UserID == user.Id && x.OrderStatus == "In Cart");

                var cartItem = db.CartContents.FirstOrDefault(x => x.Id == CartContentId);

                cartItem.Count = count;

                cartItem.TotalPrice = cartItem.Price * cartItem.Count;

                db.CartContents.AddOrUpdate(cartItem);

                db.SaveChanges();

                var cartContents = db.CartContents.Where(x => x.CartId == cart.Id).ToList();

                cart.TotalPrice = 0;

                foreach (var item in cartContents)
                {
                    cart.TotalPrice += (double)item.TotalPrice;

                }
                db.Carts.AddOrUpdate(cart);

                db.SaveChanges();
            }

            else
            {
                var cart = db.Carts.FirstOrDefault(x => x.SessionId == Session.SessionID && x.OrderStatus == "In Cart");

                var cartItem = db.CartContents.FirstOrDefault(x => x.Id == CartContentId);

                cartItem.Count = count;

                cartItem.TotalPrice = cartItem.Price * cartItem.Count;

                db.CartContents.AddOrUpdate(cartItem);

                db.SaveChanges();

                var cartContents = db.CartContents.Where(x => x.CartId == cart.Id).ToList();

                cart.TotalPrice = 0;

                foreach (var item in cartContents)
                {
                    cart.TotalPrice += (double)item.TotalPrice;

                }
                db.Carts.AddOrUpdate(cart);

                db.SaveChanges();
            }



            return Json("", JsonRequestBehavior.AllowGet);
        }


        [Route("odeme")]
        public ActionResult Checkout()
        {

            var user = db.Users.FirstOrDefault(x => x.Email == HttpContext.User.Identity.Name);

            if (user != null)
            {
                vm.Cities = db.Cities.ToList();
                vm.Districts = db.Districts.ToList();
                vm.User = user;
                vm.PaymentTypes = db.PaymentTypes.ToList();
                vm.Cargos = db.Cargos.ToList();
                var cart = db.Carts.FirstOrDefault(x => x.UserID == user.Id && x.OrderStatus == "In Cart");
                vm.CartContents = db.CartContents.Where(x => x.CartId == cart.Id).ToList();
                vm.Cart = cart;
                return View(vm);


            }
            else
            {
                return Redirect("/giris");
            }



        }

        [Route("ilceGetir")]
        public ActionResult GetDistricts(int cityId)
        {
            var districts = db.Districts.Where(x => x.CityId == cityId).ToList();
            return PartialView("_DistrictList", districts);
        }

        [Route("kargoGetir")]
        public ActionResult GetCargos(int cargoId, string totalPrice)
        {
            Cargo cargo = db.Cargos.FirstOrDefault(x => x.Id == cargoId);

            var TotalPaymentPrice = Convert.ToDecimal(cargo.Price) + Convert.ToDecimal(totalPrice);

            ViewBag.TotalPaymentPrice = TotalPaymentPrice;
            var user = db.Users.FirstOrDefault(x => x.Email == HttpContext.User.Identity.Name);
            var cart = db.Carts.FirstOrDefault(x => x.UserID == user.Id && x.OrderStatus == "In Cart");
            cart.CargoID = cargoId;
            cart.CargoPrice = (decimal)cargo.Price;
            cart.TotalPaymentPrice = (decimal)cart.TotalPrice + cart.CargoPrice;
            db.Entry(cart).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return PartialView("_CargoList", cargo);
        }


        [Route("PaymentType")]
        public ActionResult PaymentType(int paymenttypeId)
        {
            var user = db.Users.FirstOrDefault(x => x.Email == HttpContext.User.Identity.Name);
            var cart = db.Carts.FirstOrDefault(x => x.UserID == user.Id && x.OrderStatus == "In Cart");
            if (paymenttypeId == 2)
            {
                ViewBag.PaymentType = "Kredi Kartı";
                ViewBag.StripePublishKey = ConfigurationManager.AppSettings["stripePublishableKey"];

                ViewBag.TotalPrice = cart.TotalPaymentPrice;
            }

            else if (paymenttypeId == 3)
            {
                ViewBag.PaymentType = "Kapıda Ödeme";

            }


            return PartialView("_PaymentType", cart);
        }

        [Route("siparisver")]
        [Obsolete]
        [HttpPost]
        public ActionResult Order(Cart cart, string agree, string stripeToken, string stripeEmail)
        {
            if (cart.CargoID == 0)
            {
                TempData["error"] = "Lütfen Kargo Seçiniz!";
                TempData.Keep();
                return RedirectToAction("Checkout");
            }

            try
            {
                if (agree == "true")
                {
                    var dbCart = db.Carts.FirstOrDefault(x => x.Id == cart.Id);
                    dbCart.CargoID = cart.CargoID;
                    var cargoPrice = db.Cargos.FirstOrDefault(x => x.Id == cart.CargoID).Price;
                    dbCart.CargoPrice = (decimal)cargoPrice;
                    dbCart.City = cart.City;
                    dbCart.DeliveryAdress = cart.DeliveryAdress;
                    dbCart.District = cart.District;
                    dbCart.PhoneNumber = cart.PhoneNumber;
                    dbCart.OrderDate = DateTime.Now;
                    dbCart.OrderNo = Guid.NewGuid().ToString().Substring(0, 9).Replace("-", "").ToUpper();
                    dbCart.PaymentType = cart.PaymentType;
                    dbCart.TotalPaymentPrice = (decimal)dbCart.TotalPrice + dbCart.CargoPrice;
                    dbCart.OrderStatus = "Order Completed";
                    dbCart.OrderNote = cart.OrderNote;
                    var user = db.Users.FirstOrDefault(x => x.Id == dbCart.UserID);
                    dbCart.Name = user.Name;
                    dbCart.LastName = user.Lastname;
                    dbCart.TotalPaymentPrice = Convert.ToDecimal(dbCart.TotalPrice) + dbCart.CargoPrice;
                    //Cart cart1 = null;
                    db.Entry(dbCart).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();

                    if (cart.PaymentType == "Kredi Kartı")
                    {
                        Stripe.StripeConfiguration.SetApiKey("pk_test_51HwulVL9nW7sdhKB4QAVgIibx3sx6cQbG1CESA4R8C5RUlTCq9Pg8FYhlluYVDQn2M90aPufa03hQMtHplPigS2K00LmrGzfF3");
                        Stripe.StripeConfiguration.ApiKey = "sk_test_51HwulVL9nW7sdhKBonrtCOOJE2KDB5ZBU6W0nVt7gvpnLipTYPxUdJOC7YrJaYx1XJOmP2J75tliAnizm7wFys5u00BG3Zg9HF";

                        var myCharge = new Stripe.ChargeCreateOptions();
                        // always set these properties
                        myCharge.Amount = (long)(dbCart.TotalPaymentPrice * 100);
                        myCharge.Currency = "TRY";
                        myCharge.ReceiptEmail = stripeEmail;
                        myCharge.Description = "Sample Charge";
                        myCharge.Source = stripeToken;
                        myCharge.Capture = true;
                        var chargeService = new Stripe.ChargeService();
                        Charge stripeCharge = chargeService.Create(myCharge);
                        ViewBag.token = "Ödeme başarıl!";
                        ViewBag.mail = stripeEmail;

                        return View("Success", dbCart);

                    }
                    else
                    {
                        return View("Success", dbCart);
                    }

                }
                else
                {
                    TempData["error"] = "Lütfen ŞartlarıKabul Etmek İçin Kutuyu İşaretleyiniz!";
                    TempData.Keep();
                    return RedirectToAction("Checkout");
                }

            }
            catch (Exception)
            {
                return View("Error");
            }

        }


        [Route("siparislerim")]
        public ActionResult MyOrders()
        {
            var user = db.Users.FirstOrDefault(x => x.Email == HttpContext.User.Identity.Name);
            if(user != null)
            {
                vm.Carts = db.Carts.Where(x => x.UserID == user.Id && x.OrderStatus == "Order Completed").OrderByDescending(X => X.OrderDate).ToList();

                return View(vm);
            }
            else
            {
                return Redirect("/giris");
            }

          
        }



        [Route("siparisdetay/{id}")]
        public ActionResult OrderDetail(int id)
        {
            vm.CartContents = db.CartContents.Where(x => x.CartId == id).ToList();

            return View(vm);
        }


        [Route("sepeteEkleKitap-Detay")]
        public ActionResult AddtoCartBookDetail(int bookid, int count)
        {
            var book = db.Books.FirstOrDefault(x => x.Id == bookid);
            var cart = new BookSite.Data.Entities.Cart();
            var cartContents = new List<BookSite.Data.Entities.CartContent>();
            var user = db.Users.FirstOrDefault(x => x.Email == User.Identity.Name);
            if (user != null)
            {
                cart = db.Carts.FirstOrDefault(x => x.UserID == user.Id && x.OrderStatus == "In Cart");
                var cartItem = db.CartContents.FirstOrDefault(x => x.CartId == cart.Id && x.BookId == bookid);
                if (cartItem != null)
                {
                    cartItem.Count += count;
                    cartItem.TotalPrice = cartItem.Count * cartItem.Price;
                    db.Entry(cartItem).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
                else
                {
                    cartItem = new CartContent();
                    cartItem.BookId = bookid;
                    cartItem.CartId = cart.Id;
                    cartItem.Price = Convert.ToDecimal(book.DiscountPrice);
                    cartItem.Count = count;
                    cartItem.TotalPrice = cartItem.Price * cartItem.Count;
                    db.CartContents.Add(cartItem);
                    db.SaveChanges();
                }
                cart.TotalPrice = 0;
                cartContents = db.CartContents.Where(x => x.CartId == cart.Id).ToList();
                foreach (var item in cartContents)
                {
                    cart.TotalPrice += (double)item.TotalPrice;

                }
                db.Entry(cart).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            else
            {
                cart = db.Carts.FirstOrDefault(x => x.SessionId == Session.SessionID && x.OrderStatus == "In Cart");
                var cartItem = db.CartContents.FirstOrDefault(x => x.CartId == cart.Id && x.BookId == bookid);
                if (cartItem != null)
                {
                    cartItem.Count += count;
                    cartItem.TotalPrice = cartItem.Count * cartItem.Price;
                    db.Entry(cartItem).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
                else
                {
                    cartItem = new CartContent();
                    cartItem.BookId = bookid;
                    cartItem.CartId = cart.Id;
                    cartItem.Price = Convert.ToDecimal(book.DiscountPrice);
                    cartItem.Count = count;
                    cartItem.TotalPrice = cartItem.Price * cartItem.Count;
                    db.CartContents.Add(cartItem);
                    db.SaveChanges();
                }
                cart.TotalPrice = 0;
                cartContents = db.CartContents.Where(x => x.CartId == cart.Id).ToList();
                foreach (var item in cartContents)
                {
                    cart.TotalPrice += (double)item.TotalPrice;

                }
                db.Entry(cart).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }


            return Json("", JsonRequestBehavior.AllowGet);
        }
    }
}