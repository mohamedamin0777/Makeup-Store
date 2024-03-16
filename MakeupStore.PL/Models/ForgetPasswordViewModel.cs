using System.ComponentModel.DataAnnotations;

namespace MakeupStore.PL.Models
{
    public class ForgetPasswordViewModel
    {
        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Format")]
        public string Email { get; set; }
    }
}
