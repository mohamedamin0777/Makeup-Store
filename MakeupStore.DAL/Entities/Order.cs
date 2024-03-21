using System.ComponentModel.DataAnnotations;

namespace MakeupStore.DAL.Entities
{
    public class Order : BaseEntity
    {
        public Product Product { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        [EmailAddress]
        public string BuyerEmail { get; set; }
        public string PhoneNumber { get; set; }
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
        public DateTimeOffset DeliveryDate() => OrderDate.AddDays(3);
        public string ShippingAddress { get; set; }
        public decimal ShippingPrice { get; set; } = 20;
        public decimal SubTotal { get; set; } 
        public decimal TotalPrice()
                    => SubTotal + ShippingPrice;
    }
}
