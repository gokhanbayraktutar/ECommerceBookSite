using BookSite.Data.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookSite.Data.Entities
{
    public class Cart : Entity
    {
        public string OrderNo { get; set; }
        public DateTime OrderDate { get; set; }

        public string OrderStatus { get; set; }
        public int? UserID { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string DeliveryAdress { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string PaymentType { get; set; }
        public string OrderNote { get; set; }
        public double TotalPrice { get; set; }
        public int? CargoID { get; set; }
        public string CargoTracikingNo { get; set; }
        public decimal CargoPrice { get; set; }

        public decimal TotalPaymentPrice { get; set; }

        public string SessionId { get; set; }

        public virtual User User { get; set; }
        public virtual Cargo Cargo { get; set; }

    }
}
