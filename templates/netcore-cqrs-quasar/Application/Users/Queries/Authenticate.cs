using Persistence.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Threading;
using System;
using Domain.Configuration;
using Application.Users.Commons;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;

namespace Application.Users
{
    public class Authenticate
    {
        public class Query : IRequest<ApplicationUser>
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }

        public class Handler : IRequestHandler<Query, ApplicationUser>
        {
            private readonly UserManager<ApplicationUser> _userManager;
            private readonly SignInManager<ApplicationUser> _signInManager;
            private readonly AppConfig _appConfig;
            private readonly ILogger<Authenticate> _logger;
            public Handler(UserManager<ApplicationUser> userManager, ILogger<Authenticate> logger, SignInManager<ApplicationUser> signInManager, IOptions<AppConfig> options)
            {
                _logger = logger;
                _appConfig = options.Value;
                _signInManager = signInManager;
                _userManager = userManager;
            }

            public async Task<ApplicationUser> Handle(Query query, CancellationToken cancellationToken)
            {
                return await _userManager.Authenticate<Authenticate>(_logger, _signInManager, _appConfig, query.Username, query.Password);
            }

        }
    }
}