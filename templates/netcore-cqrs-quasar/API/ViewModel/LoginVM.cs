using System;
using System.ComponentModel.DataAnnotations;

namespace API.ViewModels
{
    public class LoginVM
    {
        [Required]
        public string username { get; set; }

        [Required]
        public string password { get; set; }
    }
}
