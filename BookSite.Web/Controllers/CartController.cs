using BookSite.Data.Context;
using BookSite.Data.Entities;
using BookSite.Web.Models;
using Stripe;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;
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
                ViewBag.TotalPrice = cart.TotalPaymentPrice;
            }

            else if (paymenttypeId == 3)
            {
                ViewBag.PaymentType = "Kapıda Ödeme";

            }
            return PartialView("_PaymentType", cart);
        }

        [Route("siparisver")]
        [HttpPost]
        public async Task<ActionResult> Order(Cart cart, string agree, string CardHolderName, string CardNumber, string ExpireMonth, string ExpireYear, string Cvc)
        {
            if (cart.CargoID == 0)
            {
                TempData["error"] = "Lütfen Kargo Seçiniz!";
                TempData.Keep();
                return RedirectToAction("Checkout");
            }

            try
            {
                if (agree != "true")
                {
                    TempData["error"] = "Lütfen Şartları Kabul Etmek İçin Kutuyu İşaretleyiniz!";
                    TempData.Keep();
                    return RedirectToAction("Checkout");
                }

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
                dbCart.OrderStatus = "Sipariş Oluşturuldu";
                dbCart.OrderNote = cart.OrderNote;

                var user = db.Users.FirstOrDefault(x => x.Id == dbCart.UserID);
                dbCart.Name = user.Name;
                dbCart.LastName = user.Lastname;

                db.Entry(dbCart).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                // ---- IYZICO BAŞLANGIÇ ----
                var paymentTypeValue = cart.PaymentType ?? dbCart.PaymentType;
                if (string.Equals(paymentTypeValue, "2", StringComparison.OrdinalIgnoreCase))
                {
                    // Basit input temizleme (kart numarasından boşlukları kaldır)
                    var rawCard = (CardNumber ?? "").Replace(" ", "").Trim();

                    // Doğrudan formdan boş kart geliyorsa işlemi durdur
                    if (string.IsNullOrEmpty(rawCard) || string.IsNullOrEmpty(Cvc) || string.IsNullOrEmpty(ExpireMonth) || string.IsNullOrEmpty(ExpireYear))
                    {
                        ViewBag.Message = "Kart bilgileri eksik gönderildi.";
                        ViewBag.Debug = new { CardNumber = rawCard, Cvc, ExpireMonth, ExpireYear };
                        return View("Error");
                    }

                    var options = new Iyzipay.Options
                    {
                        ApiKey = ConfigurationManager.AppSettings["IyzipayApiKey"],
                        SecretKey = ConfigurationManager.AppSettings["IyzipaySecretKey"],
                        BaseUrl = "https://sandbox-api.iyzipay.com"
                    };

                    var request = new Iyzipay.Request.CreatePaymentRequest
                    {
                        Locale = Iyzipay.Model.Locale.TR.ToString(),
                        ConversationId = Guid.NewGuid().ToString(),
                        Price = dbCart.TotalPaymentPrice.ToString("F2", System.Globalization.CultureInfo.InvariantCulture),
                        PaidPrice = dbCart.TotalPaymentPrice.ToString("F2", System.Globalization.CultureInfo.InvariantCulture),
                        Currency = Iyzipay.Model.Currency.TRY.ToString(),
                        Installment = 1,
                        BasketId = dbCart.Id.ToString(),
                        PaymentChannel = Iyzipay.Model.PaymentChannel.WEB.ToString(),
                        PaymentGroup = Iyzipay.Model.PaymentGroup.PRODUCT.ToString()
                    };

                    request.PaymentCard = new Iyzipay.Model.PaymentCard
                    {
                        CardHolderName = CardHolderName,
                        CardNumber = rawCard,
                        ExpireMonth = ExpireMonth,
                        ExpireYear = ExpireYear,
                        Cvc = Cvc,
                        RegisterCard = 0
                    };

                    request.Buyer = new Iyzipay.Model.Buyer
                    {
                        Id = user?.Id.ToString() ?? "0",
                        Name = user?.Name ?? "Guest",
                        Surname = user?.Lastname ?? "",
                        Email = user?.Email ?? "test@example.com",
                        GsmNumber = user?.Phone ?? "+905551234567",
                        IdentityNumber = "11111111111",
                        City = dbCart.City, // <-- BU SATIRI EKLEYİN
                        Country = "Turkey",
                        RegistrationAddress = dbCart.DeliveryAdress,
                        Ip = Request.UserHostAddress
                    };

                    var address = new Iyzipay.Model.Address
                    {
                        ContactName = (user?.Name ?? "") + " " + (user?.Lastname ?? ""),
                        City = dbCart.City,
                        Country = "Turkey",
                        Description = dbCart.DeliveryAdress
                    };
                    request.ShippingAddress = address;
                    request.BillingAddress = address;

                    request.BasketItems = new List<Iyzipay.Model.BasketItem>
            {
                new Iyzipay.Model.BasketItem
                {
                    Id = "BI101",
                    Name = "Kitap Siparişi",
                    Category1 = "Kitap",
                    ItemType = Iyzipay.Model.BasketItemType.PHYSICAL.ToString(),
                    Price = dbCart.TotalPaymentPrice.ToString("F2", System.Globalization.CultureInfo.InvariantCulture)
                }
            };

                    // Çağrıyı yap ve dönen objeyi debug ile göster
                    var payment = await Iyzipay.Model.Payment.Create(request, options);

                    // Debug: tüm payment objesini view'e ver (geçici, test için)
                    ViewBag.PaymentRaw = payment;

                    // İyzico'nun döndürdüğü status değişkeninin tam değerine bak — büyük küçük harf duyarlı olabilir
                    var status = (payment?.Status ?? "").ToString();
                    // Normalize et (büyük harf)
                    var normalizedStatus = status.ToUpperInvariant();

                    // Eğer SUCCESS ise devam et
                    if (normalizedStatus == "SUCCESS")
                    {
                        // Ödeme başarılıysa gerekli sipariş güncellemelerini yap
                        dbCart.OrderStatus = "Sipariş Oluşturuldu";
                        db.Entry(dbCart).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();

                        ViewBag.Message = "Ödeme Başarılı!";
                        ViewBag.Payment = payment;
                        return View("Success", dbCart);
                    }
                    else
                    {
                        // Hata ayrıntılarını göster
                        ViewBag.Message = "İyzico Ödeme Hatası";
                        ViewBag.Payment = payment;
                        ViewBag.ErrorPayment = new Dictionary<string, string>
                        {
                            { "Status", payment?.Status },
                            { "ErrorCode", payment?.ErrorCode },
                            { "ErrorGroup", payment?.ErrorGroup },
                            { "ErrorMessage", payment?.ErrorMessage }
                        };
                        return View("Error");
                    }
                }
                // -------------------- İYZICO BLOĞU BİTİŞ --------------------

                // Eğer farklı bir ödeme tipi ise (ör. Kapıda Ödeme), normal success dönebilir
                return View("Success", dbCart);
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Bir hata oluştu: " + ex.Message + " | " + ex.InnerException?.Message;
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