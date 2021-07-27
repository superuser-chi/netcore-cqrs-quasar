using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Persistence.Identity;

namespace Application.Users
{
    public class UpdateUser
    {
        public class Command : IRequest
        {
            public ApplicationUser User { get; set; }
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

                var user = await _userManager.FindByIdAsync(request.User.Id);

                user.PhoneNumber = request.User.PhoneNumber;
                user.FullName = request.User.FullName;

                await _userManager.UpdateAsync(user);

                return Unit.Value;
            }
        }
    }
}