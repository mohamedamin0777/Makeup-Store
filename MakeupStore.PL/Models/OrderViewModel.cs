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

        public decimal SubTotal { get; set; }
        public int ProductId { get; set; }

    }
}
