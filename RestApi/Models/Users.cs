using Microsoft.AspNetCore.Identity;

namespace RestApi.Models
{
    public class Users : IdentityUser
    {
        public string? FullName { get; set; }
    }
}
