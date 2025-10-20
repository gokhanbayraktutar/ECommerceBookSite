using BookSite.Data.Context;
using BookSite.Data.Entities;
using BookSite.Web.Models;
using Iyzipay;
using Iyzipay.Model;
using Iyzipay.Request;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BookSite.Web.Controllers
{
    public class PaymentController : Controller
    {


        // GET: Payment/Form
        public ActionResult Form()
        {
            return View();
        }

        // POST: Payment/Pay
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Pay(string cardNumber, string expireMonth, string expireYear, string cvc, string cardHolderName)
        {
            // Sandbox credential'ları web.config'ten al
            var options = new Options
            {
                ApiKey = ConfigurationManager.AppSettings["IyzipayApiKey"],
                SecretKey = ConfigurationManager.AppSettings["IyzipaySecretKey"],
                BaseUrl = "https://sandbox-api.iyzipay.com"
            };

            // Basit sabit sipariş detayı (test amaçlı)
            var price = "100.00";

            var request = new CreatePaymentRequest
            {
                Locale = Locale.TR.ToString(),
                ConversationId = Guid.NewGuid().ToString(),
                Price = price,
                PaidPrice = price,
                Currency = Currency.TRY.ToString(),
                Installment = 1,
                BasketId = "B" + DateTime.Now.Ticks,
                PaymentChannel = PaymentChannel.WEB.ToString(),
                PaymentGroup = PaymentGroup.PRODUCT.ToString()
            };

            request.PaymentCard = new PaymentCard
            {
                CardHolderName = cardHolderName,
                CardNumber = cardNumber?.Replace(" ", ""),
                ExpireMonth = expireMonth,
                ExpireYear = expireYear,
                Cvc = cvc,
                RegisterCard = 0
            };

            request.Buyer = new Buyer
            {
                Id = "BY" + DateTime.Now.Ticks,
                Name = "Test",
                Surname = "User",
                GsmNumber = "+905551234567",
                Email = "test@example.com",
                IdentityNumber = "11111111111",
                RegistrationAddress = "Test Address",
                Ip = Request.UserHostAddress
            };

            var address = new Address
            {
                ContactName = "Test User",
                City = "Istanbul",
                Country = "Turkey",
                Description = "Test address"
            };
            request.ShippingAddress = address;
            request.BillingAddress = address;

            request.BasketItems = new List<BasketItem>
            {
                new BasketItem
                {
                    Id = "BI101",
                    Name = "Kitap - Deneme",
                    Category1 = "Kitap",
                    ItemType = BasketItemType.PHYSICAL.ToString(),
                    Price = price
                }
            };

            var payment = await Payment.Create(request, options);

            // Sonucu View'a gönder
            ViewBag.Payment = payment;
            return View("Result");
        }
    }
}