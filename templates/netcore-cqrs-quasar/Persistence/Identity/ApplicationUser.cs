using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Persistence.Identity
{
    public class ApplicationUser : IdentityUser
    {

        public string FullName { get; set; }
        [NotMapped]
        public string Token { get; set; }

        [NotMapped]
        public string Role { get; set; }

        [NotMapped]
        public string Password { get; set; }
    }
}
