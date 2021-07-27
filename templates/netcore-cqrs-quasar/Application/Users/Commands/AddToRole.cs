using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Persistence.Identity;

namespace Application.Users
{
    public class AddToRole
    {
        public class Command : IRequest
        {
            public ApplicationUser User { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly UserManager<ApplicationUser> _userManager;
            private readonly RoleManager<IdentityRole> _roleManager;
            public Handler(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
            {
                _roleManager = roleManager;
                _userManager = userManager;

            }
            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                await _userManager.AddToRoleAsync(request.User, request.User.Role);
                return Unit.Value;
            }
        }
    }
}