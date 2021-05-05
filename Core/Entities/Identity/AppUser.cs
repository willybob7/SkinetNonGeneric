using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Core.Entities.Identity
{
    public class AppUser : IdentityUser
    {
        [Required]
        [MaxLength(100)]
        public string DisplayName { get; set; }
        public Address Address { get; set; }
    }
}
