using Microsoft.AspNetCore.Identity;

namespace AD_Coursework.Models
{
    public class Role : IdentityRole<Guid>
    {
        public virtual ICollection<User> Users { get; set; } = new List<User>();
    }
}
