using System.ComponentModel.DataAnnotations;

namespace MakeupStore.PL.Models
{
    public class OrderViewModel
    {
        [Required]
        public int Quantity { get; set; }

        [MinLength(11)]
        [MaxLength(11)]
        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string ShippingAddress { get; set; }

        [Required]
        public string BuyerEmail { get; set;}
        public string Username { get; set;}

        public decimal SubTotal { get; set; }
        public decimal ShippingPrice { get; set; } = 20;
        public decimal TotalPrice { get; set; }
        public DateOnly OrderDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);

        public DateOnly DeliveryDate { get; set; }
        public int ProductId { get; set; }

    }
}
