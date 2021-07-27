using System;
using Domain.Models;
using Persistence.Identity;

namespace API.ViewModels
{
    public class ProfileVM
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Token { get; set; }


        public ProfileVM(ApplicationUser user)
        {
            Id = user.Id;
            UserName = user.UserName;
            Token = user.Token;
        }
    }
}
