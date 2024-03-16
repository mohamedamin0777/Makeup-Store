using Microsoft.AspNetCore.Identity;

namespace MakeupStore.DAL.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public bool IsAgree { get; set; }
    }
}
