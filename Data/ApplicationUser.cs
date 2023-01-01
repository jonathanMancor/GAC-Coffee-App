using Microsoft.AspNetCore.Identity;

namespace Coffee_Project.Data
{
    public class ApplicationUser :IdentityUser
    {
        public decimal Credit { get; set; }
    }
}
