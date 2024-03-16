using Microsoft.AspNetCore.Identity;

namespace MakeupStore.DAL.Entities
{
    public class ApplicationRole : IdentityRole
    {
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
