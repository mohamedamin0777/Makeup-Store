namespace MakeupStore.PL.Models
{
    public class OrderDetailsViewModel
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public string BuyerEmail { get; set; }
        public string PhoneNumber { get; set; }
        public DateOnly OrderDate { get; set; } 
        public DateOnly DeliveryDate { get; set; }
        public string ShippingAddress { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
