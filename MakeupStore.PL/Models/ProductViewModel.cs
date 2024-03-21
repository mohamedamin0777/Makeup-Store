using System.ComponentModel.DataAnnotations;

namespace MakeupStore.PL.Models
{
    public class ProductViewModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }
        public string? PictureUrl { get; set; }
        public IFormFile? Image { get; set; }
        public int ProductCategoryId { get; set; }
        public int ProductBrandId { get; set; }
    }
}
