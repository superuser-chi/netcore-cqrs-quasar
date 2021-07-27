using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Users.Commons;
using Domain.Configuration;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Persistence;
using Persistence.Identity;

namespace Application.Users
{
    public class Register
    {
        public class Command : IRequest
        {
            public ApplicationUser User { get; set; }
            public bool CheckAD { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly UserManager<ApplicationUser> _userManager;
            private readonly AppConfig _appConfig;

            public Handler(UserManager<ApplicationUser> userManager, IOptions<AppConfig> options)
            {
                _appConfig = options.Value;
                _userManager = userManager;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                if (request.CheckAD && !ADHelper.ValidateAgainstAD(_appConfig, request.User.UserName, request.User.Password))
                {
                    throw new InvalidOperationException("The user is locked on active directory");
                }

                var user = await _userManager.FindByNameAsync(request.User.Email);

                // User exists return.
                if (user != null) return Unit.Value;

                // Create the user.
                var result = await _userManager.CreateAsync(request.User, request.User.Password);

                if (!result.Succeeded) throw new InvalidOperationException("The user could not be created");

                // Get newly created user
                var newUser = await _userManager.FindByNameAsync(request.User.Email);

                return Unit.Value;
            }
        }
    }
}