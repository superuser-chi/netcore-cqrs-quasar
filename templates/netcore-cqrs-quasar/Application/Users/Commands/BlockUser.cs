using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Persistence.Identity;

namespace Application.Users
{
    public class BlockUser
    {
        public class Command : IRequest
        {
            public string Id;
        }

        public class Handler : IRequestHandler<Command>
        {
            private UserManager<ApplicationUser> _userManager;
            public Handler(UserManager<ApplicationUser> userManager)
            {
                _userManager = userManager;

            }
            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var user = await _userManager.FindByIdAsync(request.Id);

                user.LockoutEnabled = true;
                if (user.LockoutEnd == DateTimeOffset.MaxValue)
                {
                    // unblock user
                    user.LockoutEnd = null;
                }
                else
                {
                    // block user
                    user.LockoutEnd = DateTimeOffset.MaxValue;
                }

                var result = await _userManager.UpdateAsync(user);
                return Unit.Value;
            }
        }
    }
}