using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Users.Commons;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Persistence.Identity;

namespace Application.Users.Commands
{
    public class ResetPassword
    {
        public class Command : IRequest
        {
            public string Username { get; set; }

            public string NewPassword { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly UserManager<ApplicationUser> _userManager;
            public Handler(UserManager<ApplicationUser> userManager)
            {
                _userManager = userManager;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                await _userManager.ResetPassword(request.Username, request.NewPassword);

                return Unit.Value;
            }
        }
    }
}