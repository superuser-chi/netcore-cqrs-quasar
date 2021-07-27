using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Persistence.Identity;

namespace Application.Users
{
    public class Details
    {
        public class Query : IRequest<ApplicationUser>
        {
            public string Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, ApplicationUser>
        {
            private readonly UserManager<ApplicationUser> _userManager;
            public Handler(UserManager<ApplicationUser> userManager)
            {
                _userManager = userManager;
            }

            public Task<ApplicationUser> Handle(Query request, CancellationToken cancellationToken)
            {
                return _userManager.FindByNameAsync(request.Id);
            }
        }
    }
}