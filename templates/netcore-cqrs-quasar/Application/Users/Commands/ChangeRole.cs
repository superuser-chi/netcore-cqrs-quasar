using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Persistence.Identity;

namespace Application.Users
{
    public class ChangeRole
    {
        public class Command : IRequest
        {
            public string Id { get; set; }
            public string NewRole { get; set; }
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

                var user = await _userManager.FindByIdAsync(request.Id);

                // remove from previous roles
                await RemoveFromAllRoles(user);

                // add to new roles
                var result = await _userManager.AddToRoleAsync(user, request.NewRole);

                return Unit.Value;
            }

            private async Task RemoveFromAllRoles(ApplicationUser user)
            {
                var oldRoles = await _userManager.GetRolesAsync(user);
                foreach (var r in oldRoles)
                {
                    await _userManager.RemoveFromRoleAsync(user, r);
                }
            }
        }
    }
}