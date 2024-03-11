using System.ComponentModel.DataAnnotations;

namespace MakeupStore.PL.Models
{
    public class BrandViewModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
    }
}
