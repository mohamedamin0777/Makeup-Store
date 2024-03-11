using System.ComponentModel.DataAnnotations;

namespace MakeupStore.PL.Models
{
    public class ProductCategoryViewModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
    }
}
